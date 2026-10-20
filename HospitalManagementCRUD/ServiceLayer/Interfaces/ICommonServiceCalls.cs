using HospitalManagementCRUD.CommonFunctions;

namespace HospitalManagementCRUD.ServiceLayer.Interfaces
{
    public interface ICommonServiceCalls
    {
        Task Log(MyEnum.LoggerType loggerType, string exceptionMessage, string sourceMethod, string stackTrace);
    }
}
