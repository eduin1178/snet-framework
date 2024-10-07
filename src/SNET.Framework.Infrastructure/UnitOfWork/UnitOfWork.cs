using SNET.Framework.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNET.Framework.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiDbContext _context;

        public UnitOfWork(ApiDbContext context)
        {
            this._context = context;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
