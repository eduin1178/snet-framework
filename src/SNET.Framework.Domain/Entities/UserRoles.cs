namespace SNET.Framework.Domain.Entities;

public class UserRoles
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public DateTime AssigmentDateTime { get; set; }

    public User User { get; set; }
}
