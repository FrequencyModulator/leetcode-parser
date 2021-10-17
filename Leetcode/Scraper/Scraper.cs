using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleApi;
using LeetcodeApi;
using LeetcodeApi.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Scraper.Abstractions;

namespace Scraper
{
	public class Scraper
    {
        private const string HyperlinkSlug = "=HYPERLINK(\"https://leetcode.com/problems/{0}\", \"{0}\")";

        private readonly GoogleSpreadsheetClient _googleSpreadsheetClient;
        private readonly LeetcodeClient _leetcodeClient;
        private readonly ICompanyProvider _companyProvider;
        private readonly ILogger<Scraper> _logger;

        public Scraper(
            GoogleSpreadsheetClient googleSpreadsheetClient,
            LeetcodeClient leetcodeClient,
            ICompanyProvider companyProvider,
            ILogger<Scraper> logger)
        {
            _googleSpreadsheetClient = googleSpreadsheetClient;
            _leetcodeClient = leetcodeClient;
            _companyProvider = companyProvider;
            _logger = logger;
        }

        public async Task UpdateAllLastSubmittedAsync()
        {
            var companies = await _companyProvider.GetCompanies();
            foreach (var company in companies)
                await UpdateLastSubmittedAsync(company);
        }

        public async Task UpdateLastSubmittedAsync(string company)
        {
            _logger.LogInformation("Updating last submitted dates for {company}.", company);
            var companySheet = await _googleSpreadsheetClient.LoadCompanySheet(company);
            var spreadsheetQuestions = companySheet.Questions;
            var lastSyncSubmissionDateTime = spreadsheetQuestions.Max(x => x.LastSubmittedDateTime) ?? DateTime.Now.AddMonths(-4);
            _logger.LogInformation("Last sync submission date: {date}.", lastSyncSubmissionDateTime);

            var submissionSummaries = await _leetcodeClient.GetSubmissionSummariesAsync(lastSyncSubmissionDateTime);
            _logger.LogInformation("Found new submissions: {count}.", submissionSummaries.Count);

            if (!submissionSummaries.Any())
                return;

            var dups = spreadsheetQuestions
                .GroupBy(s => s.Title)
                .Where(g => g.Count() > 1)
                .SelectMany(q => q);

            foreach (var dup in dups)
            {
                _logger.LogInformation("Found duplicates {id} {title}", dup.Id, dup.Title);
            }

            var googleSpreadsheetQuestionsDictionary = spreadsheetQuestions.ToDictionary(x => x.Title);

            foreach (var submissionSummary in submissionSummaries)
            {
                if (!googleSpreadsheetQuestionsDictionary.TryGetValue(submissionSummary.QuestionTitle, out var spreadsheetQuestion))
                    continue;

                await _googleSpreadsheetClient.UpdateLastSubmittedDateTime(
                    spreadsheetQuestion.RowNumber,
                    submissionSummary.LastSubmittedDateTime,
                    companySheet.SheetId);
                _logger.LogInformation("Updated the problem {title} with last submission date {date}", submissionSummary.QuestionTitle, submissionSummary.LastSubmittedDateTime);
            }

            // await _googleSpreadsheetClient.SortRange();
        }

        public async Task UpdateAllCompanySheetsQuestionsAsync()
        {
            var companies = await _companyProvider.GetCompanies();
            foreach (var company in companies)
                await UpdateQuestionsAsync(company);
        }

        public async Task UpdateQuestionsAsync(string company)
        {
            _logger.LogInformation("Loading {company} questions.", company);
            var companySheet = await _googleSpreadsheetClient.LoadCompanySheet(company);
            var oldSpreadsheetQuestions = companySheet.Questions;
            var test = oldSpreadsheetQuestions.Where(x => x.Id == "1781");
            var oldSpreadsheetQuestionsDictionary = oldSpreadsheetQuestions.Distinct().ToDictionary(x => x.Id);

            var companyTag = await _leetcodeClient.LoadCompanyTagAsync(company);

            var newSpreadsheetQuestions = ConvertToSpreadsheetQuestions(companyTag);
            _logger.LogInformation("Found questions: {count}.", newSpreadsheetQuestions.Count);

            LogChangedQuestions(newSpreadsheetQuestions, oldSpreadsheetQuestionsDictionary);

            var updatedQuestions = newSpreadsheetQuestions
                .Where(x => x.RowNumber > 0)
                .OrderBy(x => x.RowNumber)
                .ToList();

            await _googleSpreadsheetClient.UpdateQuestionsAsync(updatedQuestions, companySheet.SheetId);
            _logger.LogInformation("Updated questions: {count}.", updatedQuestions.Count);

            var newQuestions = newSpreadsheetQuestions
                .Where(x => x.RowNumber == 0)
                .ToList();

            var dateTimeNow = DateTime.Now;
            newQuestions.ForEach(x => x.AddedDateTime = dateTimeNow);
            await _googleSpreadsheetClient.AddQuestionsAsync(newQuestions, companySheet.SheetId);
            _logger.LogInformation("Added questions: {count}.", newQuestions.Count);
        }

        private void LogChangedQuestions(List<SpreadsheetQuestion> newSpreadsheetQuestions, Dictionary<string, SpreadsheetQuestion> oldSpreadsheetQuestionsDictionary)
        {
            var changedQuestions = new List<(double delta, SpreadsheetQuestion question)>();
            foreach (var question in newSpreadsheetQuestions)
            {
                if (oldSpreadsheetQuestionsDictionary.TryGetValue(question.Id, out var oldQuestion))
                {
                    question.RowNumber = oldQuestion.RowNumber;
                    var delta = question.Frequency6Months - oldQuestion.Frequency6Months;
                    if (delta != 0)
                        changedQuestions.Add((delta, question));
                }
            }

            if (!changedQuestions.Any())
            {
                _logger.LogInformation("There are no question updates.");
                return;
            }

            foreach (var changedQuestion in changedQuestions.OrderByDescending(x => x.delta))
            {
                _logger.LogInformation(
                    "Changed question {value:+#;-#} {title} from {count1} to {count2}.",
                    changedQuestion.delta,
                    changedQuestion.question.Title,
                    changedQuestion.question.Frequency6Months - changedQuestion.delta,
                    changedQuestion.question.Frequency6Months);
            }
        }

        private static List<SpreadsheetQuestion> ConvertToSpreadsheetQuestions(CompanyTag companyTag)
        {
            var frequencyDictionary = JsonConvert.DeserializeObject<Dictionary<string, double[]>>(companyTag.Frequencies);
            return companyTag.Questions
                .Select(x =>
                    new
                    {
                        Question = x,
                        Frequencies = frequencyDictionary.TryGetValue(x.QuestionId, out var frequencies) ? frequencies : new double[4]
                    })
                .Select(x =>
                    new SpreadsheetQuestion
                    {
                        Id = x.Question.QuestionId,
                        FrontendId = x.Question.QuestionFrontendId,
                        Title = x.Question.Title,
                        Difficulty = x.Question.Difficulty,
                        Status = x.Question.Status,
                        Frequency6Months = x.Frequencies[0],
                        Frequency1Year = x.Frequencies[1],
                        Frequency2Years = x.Frequencies[2],
                        FrequencyAllTime = x.Frequencies[3],
                        CalculatedFrequency6Months = x.Frequencies[4],
                        CalculatedFrequency1Year = x.Frequencies[5],
                        CalculatedFrequency2Years = x.Frequencies[6],
                        CalculatedFrequencyAllTime = x.Frequencies[7],
                        Slug = string.Format(HyperlinkSlug, x.Question.TitleSlug),
                        Tags = string.Join(", ", x.Question.TopicTags.Select(y => y.Name))
                    })
                .ToList();
        }
    }
}
