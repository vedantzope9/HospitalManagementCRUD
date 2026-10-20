using HospitalManagementCRUD.ServiceLayer.Interfaces;

namespace HospitalManagementCRUD.CommonFunctions
{
    public class MyMapper
    {
        private readonly ICommonServiceCalls _serviceCalls;
        public MyMapper(ICommonServiceCalls serviceCalls)
        {
            _serviceCalls = serviceCalls;
        }
        internal D MapEntityToDto<E,D>(E entity , D dto)
        {
            try
            {
                var entityProps = typeof(E).GetProperties();

                foreach (var prop in entityProps)
                {
                    var dtoProperty = typeof(D).GetProperty(prop.Name);

                    if (dtoProperty != null)
                    {
                        dtoProperty.SetValue(dto, prop.GetValue(entity));
                    }
                }
            }
            catch(Exception ex) {
                 _serviceCalls.Log(MyEnum.LoggerType.WriteAndSaveLog, ex.Message, "MyMapper", ex.StackTrace);
            }

            return dto;
        }
    }
}
