using Microsoft.EntityFrameworkCore;
using SNET.Framework.Domain.Entities;
using SNET.Framework.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNET.Framework.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(ApiDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetAllAsync()
        {
           return await _context.Set<User>().ToListAsync();
        }

        public async Task<User> GetByIdWithRoles(Guid userId)
        {
            return await _context.Set<User>()
                .Include(x=>x.Roles)
                .Where(x=>x.Id == userId)
                .FirstOrDefaultAsync();
        }
    }
}
