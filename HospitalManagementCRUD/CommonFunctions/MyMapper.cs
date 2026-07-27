namespace HospitalManagementCRUD.CommonFunctions
{
    public class MyMapper
    {
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
            catch(Exception ex) { }

            return dto;
        }
    }
}
