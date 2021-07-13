using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeetcodeApi
{
    public static class LeetcodeClientExtensions
    {
        public static Task<List<SubmissionHistorySummary>> GetSubmissionSummariesAsync(this LeetcodeClient leetcodeClient, DateTime sinceDateTime) =>
            SubmissionHistorySummarizer.GetSubmissionSummaries(leetcodeClient, sinceDateTime);
    }
}
