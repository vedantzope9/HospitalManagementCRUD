using AutoMapper;
using HospitalManagementCRUD.DTOs;
using HospitalManagementCRUD.Models;

namespace HospitalManagementCRUD.CommonFunctions
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<SecLoginUser , SecLoginUserDTO>().ReverseMap();
            CreateMap<SecLoginUser , LoginDTO>().ReverseMap();
            CreateMap<Doctor , DoctorDTO>().ReverseMap();
            CreateMap<Hospital , HospitalDTO>().ReverseMap();
        }
    }
}
