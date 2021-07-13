using LeetcodeApi;
using LeetcodeApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SubmissionSync
{
    public class FolderSync : ISubmissionSync
    {
        private readonly ILogger<FolderSync> _logger;
        private readonly SubmissionSyncConfiguration _config;
        private readonly LeetcodeClient _leetcodeClient;

        public FolderSync(ILogger<FolderSync> logger,
            SubmissionSyncConfiguration config,
            LeetcodeClient leetcodeClient)
        {
            _logger = logger;
            _config = config;
            _leetcodeClient = leetcodeClient;
        }

        public async Task SyncSubmissionsAsync()
        {
            var folder = _config.TargetFolder;
            _logger.LogInformation("Syncing submissions into: {path}", folder);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
                _logger.LogInformation("Created folder: {path}", folder);
            }

            var submissionsSince = DateTime.Today.AddMonths(-_config.SinceDaysAgo);
            await foreach (var summary in _leetcodeClient.GetSubmissionEntriesAsync(submissionsSince))
                await SaveSummary(summary);

            async Task SaveSummary(SubmissionHistoryEntry summary)
            {
                var safeTitle = string.Concat(summary.Title.Split(Path.GetInvalidFileNameChars()));
                var submissionFolder = Path.Combine(folder, safeTitle);
                if (!Directory.Exists(submissionFolder))
                {
                    Directory.CreateDirectory(submissionFolder);
                    _logger.LogInformation("Created folder: {path}", submissionFolder);
                }

                var fileName = $"{summary.DateTime:yyyy-MM-dd HH-mm-ss} - {summary.StatusDisplay}{GetExtension(summary.Lang)}";
                var filePath = Path.Combine(submissionFolder, fileName);
                if (File.Exists(filePath))
                {
                    _logger.LogInformation("File already exists: {path}", filePath);
                    return;
                }

                var contents = GetFileContent(summary);
                await File.WriteAllTextAsync(filePath, contents, Encoding.UTF8);
                _logger.LogInformation("Saved submission: {path}", filePath);
            }

            static string GetFileContent(SubmissionHistoryEntry summary)
            {
                var sb = new StringBuilder();
                CommentStart(sb, summary.Lang);
                Summary(sb, summary);
                CommentEnd(sb, summary.Lang);
                sb.AppendLine(summary.Code);
                return sb.ToString();
            }

            static void Summary(StringBuilder sb, SubmissionHistoryEntry summary)
            {
                sb.Append("Status: ");
                sb.AppendLine(summary.StatusDisplay);
                sb.Append("Runtime: ");
                sb.AppendLine(summary.Runtime);
                sb.Append("Memory: ");
                sb.AppendLine(summary.Memory);
                sb.Append("URL: ");
                sb.Append("http://leetcode.com");
                sb.AppendLine(summary.Url);
                sb.Append("Submission DateTime: ");
                sb.AppendLine(summary.DateTime.ToString("F"));
            }

            static void CommentStart(StringBuilder sb, string lang)
                => sb.AppendLine(lang switch
                {
                    "csharp" => "/*",
                    "python3" => "'''",
                    "python" => "'''",
                    "kotlin" => "/*",
                    _ => "/*"
                });

            static void CommentEnd(StringBuilder sb, string lang)
                => sb.AppendLine(lang switch
                {
                    "csharp" => "*/",
                    "python3" => "'''",
                    "python" => "'''",
                    "kotlin" => "*/",
                    _ => "/*"
                });

            static string GetExtension(string lang)
                => lang switch
                {
                    "csharp" => ".cs",
                    "python3" => ".py",
                    "python" => ".py",
                    "kotlin" => ".kt",
                    _ => ".txt"
                };
        }
    }
}
