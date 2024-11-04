using SNET.Framework.Domain.Autentications.Jwt;
using SNET.Framework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNET.Framework.Domain.Autentications
{
    public interface IManagerToken
    {
        public TokenModel GenerateToken(User user);
    }
}
