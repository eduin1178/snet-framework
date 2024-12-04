using SNET.Framework.Domain.Authentications.Jwt;
using SNET.Framework.Domain.Entities;

namespace SNET.Framework.Domain.Authentications
{
    public interface IManagerToken
    {
        public TokenModel GenerateToken(User user);
    }
}
