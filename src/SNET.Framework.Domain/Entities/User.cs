using SNET.Framework.Domain.Extensions;
using SNET.Framework.Domain.Primitives;

namespace SNET.Framework.Domain.Entities
{
    public class User : AggregateRoot
    {
        private User(Guid id,
            string firstName,
            string lastName,
            string email,
            string address,
            string phoneNumber,
            string passwordHash) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Addresss = address;
            PhoneNumber = phoneNumber;
            PasswordHash = passwordHash;
            StatusId = 1;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string Email { get; private set; }
        public bool EmailVerified { get; private set; }
        public string EmailVerificationCode { get; private set; }

        public string PasswordHash { get; private set; }
        public string Addresss { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool PhoneNumberVerified { get; private set; }
        public string PhoneNumberVerificationCode { get; private set; }
        public int StatusId { get; private set; }
        public List<UserRoles> Roles { get; private set; } = new();

        public static User Create(Guid id,
            string firstName,
            string lastName,
            string email,
            string address,
            string phoneNumber,
            string password)
        {
            var passwordHash = password.EncryptPassword();
            return new User(id, firstName, lastName, email, address, phoneNumber, passwordHash);
        }

        public void StatusChange(int newStatus)
        {
            StatusId = newStatus;
            //AddDomainEvent(AddDomainEvent(new UserStatusChangedDomainEvent(Id, StatusId, newStatus));
        }

        public void AssignRole(Guid roleId)
        {
            Roles.Add(new UserRoles
            {
                RoleId = roleId,
                UserId = Id,
                AssigmentDateTime = DateTime.Now
            });
        }

        public void RemoveRole(Guid roleId)
        {
            var role = Roles.FirstOrDefault(x => x.RoleId == roleId);
            Roles.Remove(role);
        }
    }
}
