using System.Threading.Tasks;

namespace SubmissionSync
{
    public interface ISubmissionSync
    {
        Task SyncSubmissionsAsync();
    }
}