using System.Collections.Generic;

namespace GoogleApi
{
    public class CompanySheet
    {
        public string Title { get; set; }

        public int? SheetId { get; set; }

        public int? Index { get; set; }

        public List<SpreadsheetQuestion> Questions { get; set; }
    }
}
