using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNET.Framework.Domain;

public enum UserStatus
{
    Active = 1,
    Inactive = 2,
    Locked = 3
}

public enum UserRole
{
    Guest = 1,
    Admin = 2,
    Super = 3
}
