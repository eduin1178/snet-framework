using Microsoft.EntityFrameworkCore;
using SNET.Framework.Domain.Entities;
using SNET.Framework.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNET.Framework.Infrastructure.Repositories
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

    }
}
