namespace HospitalManagementCRUD.RepositoryLayer.Interfaces
{
    public interface ICommonRepo
    {
        Task SaveChangesAsyncContext();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollBackTransactionAsync();
    }
}
