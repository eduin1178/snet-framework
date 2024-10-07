using SNET.Framework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNET.Framework.Domain.Repositories
{
    public interface IUserRolRepository : IGenericRepository<UserRoles>
    {
        Task<UserRoles> GetById(Guid id);
        Task<List<UserRoles>> GetAll();
    }
}
