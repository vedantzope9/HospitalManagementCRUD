using HospitalManagementCRUD.Models;
using HospitalManagementCRUD.RepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementCRUD.RepositoryLayer.Implementations
{
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly HospitalDbContext _context;
        public AppointmentRepo(HospitalDbContext context) { 
            _context = context;
        }

        public async Task BookAppointment(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Appointment>> GetAllAppointmentsByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments.Where(t=>t.DoctorId==doctorId).ToListAsync();
        }

        public async Task<List<Appointment>> GetUpcomingAppointmentsByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments.Where(t => t.DoctorId == doctorId && t.AppointmentDate >= DateOnly.FromDateTime(DateTime.UtcNow) && t.AppointmentTime >= TimeOnly.FromDateTime(DateTime.UtcNow) ).ToListAsync();
        }
    }
}
