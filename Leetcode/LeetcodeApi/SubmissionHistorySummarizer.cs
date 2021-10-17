using LeetcodeApi.Models;
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
            var entries = await GetSubmissionEntriesAsync(leetcodeClient, sinceDateTime);

            return entries
                .GroupBy(x => x.Title)
                .Select(x =>
                    new SubmissionHistorySummary
                    {
                        QuestionTitle = x.Key,
                        LastSubmittedDateTime = DateTimeOffset.FromUnixTimeSeconds(x.Max(y => y.Timestamp)).LocalDateTime,
                    })
                .Where(x => x.LastSubmittedDateTime > sinceDateTime)
                .OrderBy(x => x.LastSubmittedDateTime)
                .ToList();
        }

        private static async Task<List<SubmissionHistoryEntry>> GetSubmissionEntriesAsync(
            LeetcodeClient leetcodeClient,
            DateTime sinceDateTime)
        {
            var submissionHistoryEntries = new List<SubmissionHistoryEntry>();
            const int Limit = 20;
            var offset = 0;
            var lastKey = string.Empty;
            while (true)
            {
                var submissionHistory = await leetcodeClient.GetSubmissionHistoryAsync(offset, Limit, lastKey);
                var entries = submissionHistory.Entries?.Where(x => x.StatusDisplay == "Accepted");
                if(entries != null)
                    submissionHistoryEntries.AddRange(entries);

                var lastEntry = submissionHistory.Entries?.LastOrDefault();
                if (lastEntry == null)
                    break;

                var lastEntryDateTime = DateTimeOffset.FromUnixTimeSeconds(lastEntry.Timestamp).LocalDateTime;
                if (lastEntryDateTime < sinceDateTime)
                    break;

                offset += Limit;
                lastKey = submissionHistory.LastKey;
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            return submissionHistoryEntries;
        }
    }
}
