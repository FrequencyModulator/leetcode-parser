using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeetcodeApi
{
    internal static class SubmissionHistorySummarizer
    {
        internal static async Task<List<SubmissionHistorySummary>> GetSubmissionSummaries(
            LeetcodeClient leetcodeClient,
            DateTime sinceDateTime)
        {
            var entries = leetcodeClient.GetSubmissionEntriesAsync(sinceDateTime);

            return await entries
                .Where(x => x.StatusDisplay == "Accepted")
                .GroupBy(x => x.Title)
                .SelectAwait(async x =>
                    new SubmissionHistorySummary
                    {
                        QuestionTitle = x.Key,
                        LastSubmittedDateTime = await x.MaxAsync(y => y.DateTime),
                    })
                .Where(x => x.LastSubmittedDateTime > sinceDateTime)
                .OrderBy(x => x.LastSubmittedDateTime)
                .ToListAsync();
        }
    }
}
