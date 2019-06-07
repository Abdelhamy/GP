using System.Threading.Tasks;

namespace CollageSystem.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
