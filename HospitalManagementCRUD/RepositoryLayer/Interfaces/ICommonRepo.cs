using Microsoft.EntityFrameworkCore.Storage;

namespace HospitalManagementCRUD.RepositoryLayer.Interfaces
{
    public interface ICommonRepo
    {
        Task SaveChangesAsyncContext();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollBackTransactionAsync();
    }
}
