using Microsoft.EntityFrameworkCore;
using UserAndUnitManagement.Domain.Entities;
using UserAndUnitManagement.Domain.Interfaces;
using UserAndUnitManagement.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace UserAndUnitManagement.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}