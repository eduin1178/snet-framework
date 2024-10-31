using SNET.Framework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNET.Framework.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByIdWithRoles(Guid userId);
        Task<User> GetByEmailAsync(string email);
        Task<List<User>> GetAllAsync();
    }
}
