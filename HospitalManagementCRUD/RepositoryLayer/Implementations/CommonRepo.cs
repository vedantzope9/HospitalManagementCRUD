using HospitalManagementCRUD.Models;
using HospitalManagementCRUD.RepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementCRUD.RepositoryLayer.Implementations
{
    public class CommonRepo:ICommonRepo
    {
        private readonly HospitalDbContext _dbContext;
        public CommonRepo(HospitalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SaveChangesAsyncContext()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task RollBackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }
    }
}
