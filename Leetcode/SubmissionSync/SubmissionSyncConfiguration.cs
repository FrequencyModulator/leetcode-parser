namespace SubmissionSync
{
    public class SubmissionSyncConfiguration
    {
        public string TargetFolder { get; set; }
        public int SinceDaysAgo { get; set; } = 5;
    }
}