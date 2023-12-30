using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enitites;
using Talabat.Core.IRepositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TalabatDbContext _dbContext;
        private Hashtable _repositories;
        public UnitOfWork(TalabatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Complete()
         => await _dbContext.SaveChangesAsync();

        public void Dispose()
        {
            _dbContext.Dispose();
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if( _repositories == null )
                _repositories = new Hashtable();
            var type = typeof(TEntity).Name;
            if(! _repositories.ContainsKey( type ) )
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add( type, repository );
            }
            return (IGenericRepository<TEntity>) _repositories[ type ];
        }
    }
}
