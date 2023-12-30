using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enitites;
using Talabat.Core.IRepositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly TalabatDbContext _context;
        public GenericRepository(TalabatDbContext context)
        {
             _context = context;
        }


        async Task<IReadOnlyList<T>> IGenericRepository<T>.GetAllAsync()
         => await _context.Set<T>().ToListAsync();

        async Task<T> IGenericRepository<T>.GetByIdAsync(int id)
         => await _context.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
         => await ApplySpecification(spec).ToListAsync();

        public async Task<T> GetByIdWithSpecAsync(ISpecifications<T> spec)
        => await ApplySpecification(spec).FirstOrDefaultAsync();
        public async Task<int> GetCountAsync(ISpecifications<T> spec)
        => await ApplySpecification(spec).CountAsync();
        private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
         => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);

        public async Task CreateAsync(T entity)
        => await _context.Set<T>().AddAsync(entity);
        

        public void Updater(T entity)
      // => _context.Entry(entity).State = EntityState.Modified;
        => _context.Set<T>().Update(entity);

        public void Delete(T entity)
        => _context.Set<T>().Remove(entity);

    }
}
