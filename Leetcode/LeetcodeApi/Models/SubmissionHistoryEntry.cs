using System;
using System.Text.Json.Serialization;

namespace LeetcodeApi.Models
{
    public class SubmissionHistoryEntry
    {
        [JsonPropertyName("status_display")]
        public string StatusDisplay { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        public DateTime DateTime => DateTimeOffset.FromUnixTimeSeconds(Timestamp).LocalDateTime;

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("lang")]
        public string Lang { get; set; }

        [JsonPropertyName("runtime")]
        public string Runtime { get; set; }

        [JsonPropertyName("memory")]
        public string Memory { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
