using System.Threading.Tasks;
using LoggerService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoggerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoggerController : ControllerBase
    {
        private readonly LoggerService.Services.LoggerService _loggerService;

        public LoggerController(LoggerService.Services.LoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        [HttpPost("saveLog")]
        public async Task SaveLog(AppExceptionLog appExceptionLog)
        {
            await _loggerService.SaveLog(appExceptionLog);
        }

        [HttpPost("writeLog")]
        public async Task WriteLog(AppExceptionLog appExceptionLog)
        {
            await _loggerService.WriteLog(appExceptionLog);
        }
    }
}
