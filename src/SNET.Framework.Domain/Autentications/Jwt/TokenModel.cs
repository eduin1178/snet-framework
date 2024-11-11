using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNET.Framework.Domain.Autentications.Jwt;

public record TokenModel(string Token, DateTime Expires, Dictionary<string, string> Claims);

