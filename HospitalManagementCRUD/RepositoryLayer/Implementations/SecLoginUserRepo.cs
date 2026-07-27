using HospitalManagementCRUD.Models;
using HospitalManagementCRUD.RepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementCRUD.RepositoryLayer.Implementations
{
    public class SecLoginUserRepo: ISecLoginUserRepo
    {
        private readonly HospitalDbContext _context;
        public SecLoginUserRepo(HospitalDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<SecLoginUser?> GetSecLoginUser(int userId)
        {
            return await _context.SecLoginUsers.FindAsync(userId);
        }

        public async Task SaveSecLoginUser(SecLoginUser secLoginUser)
        {
            await _context.SecLoginUsers.AddAsync(secLoginUser);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckUsernameExists(string username)
        {
            return await _context.SecLoginUsers.AnyAsync(t=>t.UserName.Equals(username));
        }
    }
}
