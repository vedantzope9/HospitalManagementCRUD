using System.Threading.Tasks;
using LoggerService.Models;
using LoggerService.Repository;
using Microsoft.IdentityModel.Tokens;

namespace LoggerService.Services
{
    public class LoggerService
    {
        private readonly LoggerRepo _loggerRepo;
        private readonly IConfiguration configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public LoggerService(LoggerRepo loggerRepo , IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            _loggerRepo = loggerRepo;
            _serviceScopeFactory = serviceScopeFactory;
            this.configuration = configuration;
        }
        public async Task SaveLog(AppExceptionLog appExceptionLog)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<LoggerDbContext>();
                await _loggerRepo.SaveLog(appExceptionLog , dbContext);
            }                
        }

        public async Task WriteLog(AppExceptionLog appExceptionLog) {
            try
            {
                string logDirectory = configuration["LogSettings:LogDirectory"] ?? throw new InvalidOperationException("Log directory is not configured.");

                Directory.CreateDirectory(logDirectory);

                string fileName = $"{DateTime.UtcNow:yyyy-MM-dd}.txt";
                string filePath = Path.Combine(logDirectory, fileName);

                string logEntry = $"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] || {appExceptionLog.ExceptionMessage} | Source: {appExceptionLog.SourceMethod} | StackTrace: {appExceptionLog.StackTrace}{Environment.NewLine}";

                await File.AppendAllTextAsync(filePath, logEntry);
            }
            catch (Exception ex)
            {
                appExceptionLog.LogId = 0;
                appExceptionLog.ExceptionMessage += ex.Message;
                appExceptionLog.SourceMethod += " LoggerService/WriteLog";
                appExceptionLog.StackTrace += ex.StackTrace;
                await SaveLog(appExceptionLog);
            }
        }
    }
}
