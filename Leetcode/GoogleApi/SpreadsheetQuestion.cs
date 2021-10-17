using System;

namespace GoogleApi
{
    public class SpreadsheetQuestion
    {
        public int RowNumber { get; set; }

        public string Id { get; set; }

        public string FrontendId { get; set; }

        public string Title { get; set; }

        public string Difficulty { get; set; }

        public string Status { get; set; }

        public double Frequency6Months { get; set; }

        public double Frequency1Year { get; set; }

        public double Frequency2Years { get; set; }

        public double FrequencyAllTime { get; set; }
        public double CalculatedFrequency6Months { get; set; }
        public double CalculatedFrequency1Year { get; set; }
        public double CalculatedFrequency2Years { get; set; }
        public double CalculatedFrequencyAllTime { get; set; }

        public string Slug { get; set; }

        public string Tags { get; set; }

        public DateTime? AddedDateTime { get; set; }

        public DateTime? LastSubmittedDateTime { get; set; }
    }
}