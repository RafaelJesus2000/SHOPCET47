using Microsoft.EntityFrameworkCore;
using ShopCET47.web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET47.web.Data.repositories
{
    public class GenericRepository<T> : IgenericRepository<T> where T : class, identity
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task CreateAsync(T identity)
        {
            await _context.Set<T>().AddAsync(identity);
            await SaveAllAsync();
        }

        public async Task UpdateAsync(T identity)
        {
            _context.Set<T>().Update(identity);
            await SaveAllAsync();
        }

        public async Task DeleteAsync(T identity)
        {
            _context.Set<T>().Remove(identity);
            await SaveAllAsync();
        }

        public async Task<bool> ExistAsync(int Id)
        {
            return await _context.Set<T>().AnyAsync(e => e.Id == Id);
            
        }




        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
