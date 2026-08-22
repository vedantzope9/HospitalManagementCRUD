using HospitalManagementCRUD.Models;
using HospitalManagementCRUD.RepositoryLayer.Interfaces;

namespace HospitalManagementCRUD.RepositoryLayer.Implementations
{
    public class HospitalRepo : IHospitalRepo
    {
        private readonly HospitalDbContext _context;
        public HospitalRepo(HospitalDbContext context) 
        {
            _context = context;
        }

        public async Task SaveHospital(Hospital hospital)
        {
            await _context.Hospitals.AddAsync(hospital);
            await _context.SaveChangesAsync();
        }
        
        public async Task<Hospital?> GetHospitalById(int id)
        {
            return await _context.Hospitals.FindAsync(id);
        }
    }
}
