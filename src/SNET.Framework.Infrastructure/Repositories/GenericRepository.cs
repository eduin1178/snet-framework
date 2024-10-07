using SNET.Framework.Domain.Primitives;
using SNET.Framework.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNET.Framework.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly ApiDbContext _context;

        protected GenericRepository(ApiDbContext context)
        {
            this._context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
           _context.Remove(entity);
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }
    }
}
