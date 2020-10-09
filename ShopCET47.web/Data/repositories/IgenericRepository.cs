using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET47.web.Data.repositories
{
    public interface IgenericRepository<T> where T: class
    {
        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(int id);

        Task CreateAsync(T identity);

        Task UpdateAsync(T identity);

        Task DeleteAsync(T identity);

        Task<bool> ExistAsync(int id);



    }
}
