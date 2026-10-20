using HospitalManagementCRUD.CommonFunctions;
using HospitalManagementCRUD.DTOs;
using HospitalManagementCRUD.ServiceLayer.Interfaces;

namespace HospitalManagementCRUD.ServiceLayer.Implementations
{
    public class CommonServiceCalls: ICommonServiceCalls
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CommonServiceCalls(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task Log(MyEnum.LoggerType loggerType, string exceptionMessage, string sourceMethod, string stackTrace)
        {
            LoggerDTO loggerDTO = new LoggerDTO
            {
                ExceptionMessage = exceptionMessage,
                SourceMethod = sourceMethod,
                StackTrace = stackTrace
            };

            using var client = _httpClientFactory.CreateClient("LoggerService");

            if (loggerType == MyEnum.LoggerType.WriteLog)
            {
                var response = await client.PostAsJsonAsync("api/Logger/writeLog", loggerDTO);
                response.EnsureSuccessStatusCode();
            }
            else if (loggerType == MyEnum.LoggerType.SaveLog)
            {
                var response = await client.PostAsJsonAsync("api/Logger/saveLog", loggerDTO);
                response.EnsureSuccessStatusCode();
            }
            else if(loggerType == MyEnum.LoggerType.WriteAndSaveLog)
            {
                var responseWrite = await client.PostAsJsonAsync("api/Logger/writeLog", loggerDTO);
                responseWrite.EnsureSuccessStatusCode();
                var responseSave = await client.PostAsJsonAsync("api/Logger/saveLog", loggerDTO);
                responseSave.EnsureSuccessStatusCode();
            }
        }

    }
}
