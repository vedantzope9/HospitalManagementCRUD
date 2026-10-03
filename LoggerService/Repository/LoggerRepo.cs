using LoggerService.Models;

namespace LoggerService.Repository
{
    public class LoggerRepo
    {
        //private readonly LoggerDbContext _dbContext;

        //public LoggerRepo(LoggerDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        public async Task SaveLog(AppExceptionLog appExceptionLog , LoggerDbContext context)
        {
            await context.AppExceptionLogs.AddAsync(appExceptionLog);
            await context.SaveChangesAsync();
        }
    }
}
