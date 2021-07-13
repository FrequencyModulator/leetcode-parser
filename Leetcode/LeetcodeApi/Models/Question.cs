using System.Collections.Generic;

namespace LeetcodeApi.Models
{
    public class Question
    {
        public string Title { get; set; }

        public string TitleSlug { get; set; }

        public string Status { get; set; }

        public string QuestionId { get; set; }

        public string QuestionFrontendId { get; set; }

        public string Difficulty { get; set; }

        public List<TopicTag> TopicTags { get; set; }
    }
}