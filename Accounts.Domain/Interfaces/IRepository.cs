using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int ID);
    }

}
