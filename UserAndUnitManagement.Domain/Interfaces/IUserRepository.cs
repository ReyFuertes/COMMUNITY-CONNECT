using UserAndUnitManagement.Domain.Entities;
using System.Threading.Tasks;

namespace UserAndUnitManagement.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByEmail(string email);
    }
}