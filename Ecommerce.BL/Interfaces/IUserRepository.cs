using Ecommerce.Core.Entities;

namespace Ecommerce.BL.Services
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetByEmail(string email);
    }
}
