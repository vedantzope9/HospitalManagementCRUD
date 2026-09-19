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

        public async Task<Doctor?> GetDoctorByDoctorIdAsync(int doctorId)
        {
            return await _context.Doctors.FindAsync(doctorId);
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

        public async Task<int> GetDoctorIdByUserIdAsync(int userId)
        {
            return await _context.Doctors.Where(t=>t.UserId==userId).Select(t=>t.DoctorId).SingleOrDefaultAsync();
        }

        public async Task<List<Doctor>> GetAllDoctorsOfHospital(int hospitalId)
        {
            return await _context.Doctors.Where(t=>t.HospitalId==hospitalId).ToListAsync();
        }
    }
}
