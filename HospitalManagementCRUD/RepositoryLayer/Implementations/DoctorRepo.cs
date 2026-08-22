using HospitalManagementCRUD.Models;
using HospitalManagementCRUD.RepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementCRUD.RepositoryLayer.Implementations
{
    public class DoctorRepo : IDoctorRepo
    {
        private readonly HospitalDbContext _context;
        public DoctorRepo(HospitalDbContext context)
        {
            _context = context;
        }
        public async Task<Doctor?> GetDoctorByUserIdAsync(int userId)
        {
            return await _context.Doctors.FirstOrDefaultAsync(t => t.UserId == userId);
        }

        public async Task SaveDoctorAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
