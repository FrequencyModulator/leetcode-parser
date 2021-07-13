using GoogleApi;
using LeetcodeApi;
using LeetcodeApi.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Scraper.Abstractions;
using SubmissionSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Scraper
{
    public class Scraper
    {
        private readonly GoogleSpreadsheetClient _googleSpreadsheetClient;
        private readonly LeetcodeClient _leetcodeClient;
        private readonly ISubmissionSync _submissionSync;
        private readonly ICompanyProvider _companyProvider;
        private readonly ILogger<Scraper> _logger;

        public Scraper(
            GoogleSpreadsheetClient googleSpreadsheetClient,
            LeetcodeClient leetcodeClient,
            ISubmissionSync submissionSync,
            ICompanyProvider companyProvider,
            ILogger<Scraper> logger)
        {
            _googleSpreadsheetClient = googleSpreadsheetClient;
            _leetcodeClient = leetcodeClient;
            _submissionSync = submissionSync;
            _companyProvider = companyProvider;
            _logger = logger;
        }

        public Task SyncSubmissionsAsync()
            => _submissionSync.SyncSubmissionsAsync();

        public async Task UpdateMergedView()
        {            
            var companies = new HashSet<string> { "Uber", "Facebook" };
            _logger.LogInformation("Merging companies {companies}.", companies);

            var merged = new Dictionary<string, SpreadsheetQuestion>();            
            foreach (var company in companies)
            {
                _logger.LogInformation("Merging company {company}.", company);
                var companySheet = await _googleSpreadsheetClient.LoadCompanySheet(company);
                foreach (var question in companySheet.Questions)
                {
                    question.Companies += company + ", ";
                    if (merged.TryGetValue(question.Slug, out var q))
                    {
                        q.Frequency6Months += question.Frequency6Months;
                        q.Frequency1Year += question.Frequency1Year;
                        q.Frequency2Years += question.Frequency2Years;
                        q.FrequencyAllTime += question.FrequencyAllTime;
                        q.DuplicatesCount++;
                    }
                    else
                    {
                        question.DuplicatesCount = 1;
                        merged[question.Slug] = question;
                    }
                }                                
            }

            var mergedCompany = new CompanySheet
            {
                Title = $"[Merged] {string.Join(',', companies)}",
                Questions = merged.Values.ToList()                    
            };

                                    
            _logger.LogInformation("Updating merged spreadsheet for {companies}.", companies);

            var mergedCompanySheet = await _googleSpreadsheetClient.LoadCompanySheet(mergedCompany.Title);
            await _googleSpreadsheetClient.UpdateQuestionsAsync(mergedCompany.Questions, mergedCompanySheet.SheetId);                       
        }

        public async Task UpdateAllLastSubmittedAsync()
        {
            var companies = _companyProvider.GetCompanies();
            await foreach (var company in companies)
                await UpdateLastSubmittedAsync(company);
        }

        public async Task UpdateLastSubmittedAsync(string company)
        {
            _logger.LogInformation("Updating last submitted dates for {company}.", company);
            var companySheet = await _googleSpreadsheetClient.LoadCompanySheet(company);
            var spreadsheetQuestions = companySheet.Questions;
            var lastSyncSubmissionDateTime = spreadsheetQuestions.Max(x => x.LastSubmittedDateTime) ?? DateTime.Now.AddMonths(-1);
            _logger.LogInformation("Last sync submission date: {date}.", lastSyncSubmissionDateTime);

            var submissionSummaries = await _leetcodeClient.GetSubmissionSummariesAsync(lastSyncSubmissionDateTime);
            _logger.LogInformation("Found new submissions: {count}.", submissionSummaries.Count);

            if (!submissionSummaries.Any())
                return;

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
        }

        public async Task UpdateAllCompanySheetsQuestionsAsync()
        {
            var companies = _companyProvider.GetCompanies();
            await foreach (var company in companies)
                await UpdateQuestionsAsync(company);
        }

        public async Task UpdateQuestionsAsync(string company)
        {
            _logger.LogInformation("Loading {company} questions.", company);
            var companySheet = await _googleSpreadsheetClient.LoadCompanySheet(company);
            var oldSpreadsheetQuestions = companySheet.Questions;
            EnsureNoDuplicates(oldSpreadsheetQuestions);
            var oldSpreadsheetQuestionsDictionary = oldSpreadsheetQuestions.ToDictionary(x => x.Id);

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

        private void EnsureNoDuplicates(List<SpreadsheetQuestion> questions)
        {
            var duplicates = questions.GroupBy(q => q.Id)
                .Where(g => g.Count() > 1)
                .ToList();

            if (!duplicates.Any())
                return;

            foreach (var duplicateGroup in duplicates)
            {
                var duplicateQuestions = duplicateGroup.ToList();
                _logger.LogError("Found duplicate questions with id {id}: {@duplicateQuestions}.", duplicateGroup.Key, duplicateQuestions);
            }

            throw new InvalidDataContractException(
                $"Duplicated question found: {string.Join(',', duplicates.Select(d => d.Key))}.");
        }

        private void LogChangedQuestions(List<SpreadsheetQuestion> newSpreadsheetQuestions, Dictionary<string, SpreadsheetQuestion> oldSpreadsheetQuestionsDictionary)
        {
            var changedQuestions = new List<(int delta, SpreadsheetQuestion question)>();
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

            foreach (var (delta, question) in changedQuestions.OrderByDescending(x => x.delta))
            {
                _logger.LogInformation(
                    "Changed question {value:+#;-#} {title} from {count1} to {count2}.",
                    delta,
                    question.Title,
                    question.Frequency6Months - delta,
                    question.Frequency6Months);
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
                        Frequencies = frequencyDictionary[x.QuestionId]
                    })
                .Select(x =>
                    new SpreadsheetQuestion
                    {
                        Id = x.Question.QuestionId,
                        FrontendId = x.Question.QuestionFrontendId,
                        Title = x.Question.Title,
                        Difficulty = x.Question.Difficulty,
                        Status = x.Question.Status,
                        Frequency6Months = (int)x.Frequencies[0],
                        Frequency1Year = (int)x.Frequencies[1],
                        Frequency2Years = (int)x.Frequencies[2],
                        FrequencyAllTime = (int)x.Frequencies[3],
                        Slug = x.Question.TitleSlug,
                        Tags = string.Join(", ", x.Question.TopicTags.Select(y => y.Name))
                    })
                .ToList();
        }
    }
}
