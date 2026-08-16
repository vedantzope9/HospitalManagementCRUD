using System.Security.Claims;
using AutoMapper;
using HospitalManagementCRUD.DTOs;
using HospitalManagementCRUD.Models;
using HospitalManagementCRUD.RepositoryLayer.Interfaces;
using HospitalManagementCRUD.ServiceLayer.Interfaces;

namespace HospitalManagementCRUD.ServiceLayer.Implementations
{
    public class DoctorService:IDoctorService
    {
        private readonly IDoctorRepo _doctorRepo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public DoctorService(IDoctorRepo doctorRepo, IMapper mapper) 
        { 
            _doctorRepo = doctorRepo;
            _mapper = mapper;
        }

        public async Task RegisterAsDoctor(DoctorDTO doctorDTO)
        {
            Doctor doctor = _mapper.Map<Doctor>(doctorDTO);
            doctor.IsActive = true;
            doctor.UserId = _userService.UserId;

            await _doctorRepo.SaveDoctorAsync(doctor);
        }
    }
}
