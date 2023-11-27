using Accounts.Domain.Entities;


namespace Accounts.Domain.Interfaces
{
    public interface IUserRepository : IRepository<Users>
    {
       
        Task<Users> GetByUsername(string username);
        Task CreateAsync(Users users);
        Task UpdateAsync(Users users);
        Task<IEnumerable<Users>> GetAllAsync();
        Task<Users> GetByIdAsync(int ID);
        Task<Users> GetUserByUsernameAndPasswordAsync(string username, string password);

    }

}
