using AutoMapper;
using HospitalManagementCRUD.DTOs;
using HospitalManagementCRUD.Models;
using HospitalManagementCRUD.RepositoryLayer.Interfaces;
using HospitalManagementCRUD.ServiceLayer.Interfaces;

namespace HospitalManagementCRUD.ServiceLayer.Implementations
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepo _hospitalRepo;
        private readonly IMapper _mapper;
        private readonly IDoctorRepo _doctorRepo;
        public HospitalService(IHospitalRepo hospitalRepo , IDoctorRepo doctorRepo ,IMapper mapper) 
        {
            _hospitalRepo = hospitalRepo;
            _doctorRepo = doctorRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<HospitalDTO?>> GetHospitalById(int id)
        {
            ApiResponse<HospitalDTO?> apiResponse= new ApiResponse<HospitalDTO?>();

            Hospital? hospital = await _hospitalRepo.GetHospitalById(id);

            if (hospital == null)
            {
                apiResponse.Success = false;
                apiResponse.Message = "Hospital Not Found.";
                return apiResponse;
            }

            HospitalDTO hospitalDTO= _mapper.Map<HospitalDTO>(hospital);

            apiResponse.Success = true;
            apiResponse.Message = "Hospital fetched successfully.";
            apiResponse.Data = hospitalDTO;

            return apiResponse;     
        }

        public async Task<ApiResponse<bool?>> SaveHospital(HospitalDTO hospitalDTO)
        {
            ApiResponse<bool?> apiResponse=new ApiResponse<bool?>();

            Hospital hospital=_mapper.Map<Hospital>(hospitalDTO);

            try
            {
                await _hospitalRepo.SaveHospital(hospital);
                apiResponse.Success = true;
                apiResponse.Message = "Hospital saved successfully.";
                return apiResponse;
            }
            catch(Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = "Failed to save Hospital "+ex.Message;
                return apiResponse;
            }            
        }

        public async Task<ApiResponse<List<DoctorDTO>>> GetAllDoctorsOfHospital(int hospitalId)
        {
            ApiResponse<List<DoctorDTO>> apiResponse=new ApiResponse<List<DoctorDTO>>();

            try
            {
                List<Doctor> doctorList = await _doctorRepo.GetAllDoctorsOfHospital(hospitalId);

                List<DoctorDTO> doctorDTOs = new List<DoctorDTO>();

                foreach (var item in doctorList)
                {
                    DoctorDTO dto = _mapper.Map<DoctorDTO>(item);
                    dto.SpecializationValue = item.Specialization.ToString();

                    doctorDTOs.Add(dto);
                }
                apiResponse.Success = true;
                apiResponse.Data = doctorDTOs;
                return apiResponse;
            }
            catch (Exception ex)
            {
                apiResponse.Success= false;
                apiResponse.Message= ex.Message;
            }

            return null;
        }
    }
}
