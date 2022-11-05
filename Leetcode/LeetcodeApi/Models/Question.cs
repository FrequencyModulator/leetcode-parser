using Newtonsoft.Json;
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

        public bool IsPaidOnly { get; set; }

        public string SimilarQuestions { get; set; }

        private List<QuestionReference> _similarQuestionsList;
        public List<QuestionReference> SimilarQuestionsList
            => _similarQuestionsList ??= JsonConvert.DeserializeObject<List<QuestionReference>>(SimilarQuestions);

        public List<TopicTag> TopicTags { get; set; }
    }
}