using System.Collections.Generic;
using AutoMapper;
using HospitalManagementCRUD.CommonFunctions;
using HospitalManagementCRUD.DTOs;
using HospitalManagementCRUD.Models;
using HospitalManagementCRUD.RepositoryLayer.Interfaces;
using HospitalManagementCRUD.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementCRUD.ServiceLayer.Implementations
{
    public class AppointmentService:IAppointmentService
    {
        private readonly IAppointmentRepo _appointmentRepo;
        private readonly IDoctorRepo _doctorRepo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ISecLoginUserRepo _secLoginUserRepo;
        public AppointmentService(IAppointmentRepo appointmentRepo, IDoctorRepo doctorRepo, IMapper mapper, IUserService userService, ISecLoginUserRepo secLoginUserRepo) { 
            _appointmentRepo = appointmentRepo;
            _doctorRepo = doctorRepo;
            _mapper = mapper;   
            _userService = userService;
            _secLoginUserRepo = secLoginUserRepo;
        }

        public async Task<ApiResponse<bool>> BookAppointment(AppointmentDTO appointmentDTO)
        {
            ApiResponse<bool> apiResponse=new ApiResponse<bool>();

            try
            {
                Appointment appointment=_mapper.Map<Appointment>(appointmentDTO);

                Doctor? doctor = await _doctorRepo.GetDoctorByDoctorIdAsync(appointmentDTO.DoctorId);

                if (doctor == null)
                {
                    apiResponse.Success=false;
                    apiResponse.Message="Doctor not found";
                    return apiResponse;
                }
                if (doctor.IsActive == false || doctor.IsAvailable == false)
                {
                    apiResponse.Success=false;
                    apiResponse.Message = "Doctor not availabe";
                    return apiResponse;
                }
                
                //AppointmentDate : DateOnly
                //AppointmentTime: TimeOnly
                appointment.UserId=_userService.UserId;
                appointment.Status = (int)MyEnum.Status.Booked;

                await _appointmentRepo.BookAppointment(appointment);

                apiResponse.Success=true;
                apiResponse.Message = "Appointment booked successfully.";

                if(doctor.ConsultationFee > appointment.FeesPaid)
                {
                    apiResponse.Message += $"\n Remaining fees:{doctor.ConsultationFee - appointment.FeesPaid}";
                }

                return apiResponse;
            }
            catch(Exception ex)
            {
                apiResponse.Success=false;
                apiResponse.Message=ex.Message;
                return apiResponse;
            }
        }

        public async Task<ApiResponse<List<AppointmentDetailsDTO>>> GetAllMyAppointmentAsDoctor()
        {
            ApiResponse<List<AppointmentDetailsDTO>> apiResponse = new ApiResponse<List<AppointmentDetailsDTO>>();
            try
            {
                int userId = _userService.UserId;
                int doctorId = await _doctorRepo.GetDoctorIdByUserIdAsync(userId);

                List<Appointment> appointments = await _appointmentRepo.GetAllAppointmentsByDoctorIdAsync(doctorId);


                List<AppointmentDetailsDTO> appointmentDetailsDTO = new List<AppointmentDetailsDTO>();

                foreach (Appointment app in appointments)
                {
                    AppointmentDetailsDTO detailsDTO = _mapper.Map<AppointmentDetailsDTO>(app);
                    detailsDTO.StatusValue = app.Status.ToString();

                    SecLoginUser? secLoginUser = await _secLoginUserRepo.GetSecLoginUser(app.UserId);
                    if (secLoginUser != null)
                    {
                        detailsDTO.UserName = secLoginUser.UserName;
                        detailsDTO.PhoneNumber = secLoginUser.PhoneNumber;
                        detailsDTO.Email = secLoginUser.Email;
                        detailsDTO.GenderValue = secLoginUser.Gender.ToString();
                    }
                    appointmentDetailsDTO.Add(detailsDTO);
                }
                apiResponse.Success = true;
                apiResponse.Data = appointmentDetailsDTO;
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }
            return apiResponse;
        }

        public async Task<ApiResponse<List<AppointmentDetailsDTO>>> GetMyUpcomingAppointmentAsDoctor()
        {
            ApiResponse<List<AppointmentDetailsDTO>> apiResponse = new ApiResponse<List<AppointmentDetailsDTO>>();
            try
            {
                int userId = _userService.UserId;
                int doctorId = await _doctorRepo.GetDoctorIdByUserIdAsync(userId);

                List<Appointment> appointments = await _appointmentRepo.GetUpcomingAppointmentsByDoctorIdAsync(doctorId);


                List<AppointmentDetailsDTO> appointmentDetailsDTO = new List<AppointmentDetailsDTO>();

                foreach(Appointment app in appointments)
                {
                    AppointmentDetailsDTO detailsDTO = _mapper.Map<AppointmentDetailsDTO>(app);
                    detailsDTO.StatusValue = app.Status.ToString();

                    SecLoginUser? secLoginUser = await _secLoginUserRepo.GetSecLoginUser(app.UserId);
                    if (secLoginUser != null)
                    {
                        detailsDTO.UserName = secLoginUser.UserName;
                        detailsDTO.PhoneNumber= secLoginUser.PhoneNumber;
                        detailsDTO.Email= secLoginUser.Email;
                        detailsDTO.GenderValue=secLoginUser.Gender.ToString();
                    }
                    appointmentDetailsDTO.Add(detailsDTO);
                }
                apiResponse.Success = true;
                apiResponse.Data = appointmentDetailsDTO;
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
            }
            return apiResponse;
        }
    }
}
