using HospitalManagementCRUD.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementCRUD.ServiceLayer.Interfaces
{
    public interface IAppointmentService
    {
        Task<ApiResponse<bool>> BookAppointment(AppointmentDTO appointmentDTO);
        Task<ApiResponse<List<AppointmentDetailsDTO>>> GetMyUpcomingAppointmentAsDoctor();
        Task<ApiResponse<List<AppointmentDetailsDTO>>> GetAllMyAppointmentAsDoctor();
    }
}
