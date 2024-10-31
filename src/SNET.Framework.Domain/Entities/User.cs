using SNET.Framework.Domain.DomainEvents.Users;
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
            string phoneNumber,
            string passwordHash) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;

            PasswordHash = passwordHash;

            PhoneNumber = phoneNumber;

            CreatedAt = DateTime.UtcNow;
            StatusId = 1;

            AddDomainEvent(new UserCreatedDomainEvent(Guid.NewGuid(), id, firstName, lastName, email));
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string Email { get; private set; }
        public bool EmailVerified { get; private set; }
        public string EmailVerificationCode { get; private set; }

        public string PasswordHash { get; private set; }
        public bool ForcePasswordChange { get; private set; }
        public string PasswordRecoveryCode { get; private set; }
        public DateTime? PasswordRecoveryDate { get; private set; }

        public string PhoneNumber { get; private set; }
        public bool PhoneNumberVerified { get; private set; }
        public string PhoneNumberVerificationCode { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public int StatusId { get; private set; }
        public List<UserRoles> Roles { get; private set; } = new();

        public static User Create(Guid id,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            string password)
        {
            var passwordHash = password.EncryptPassword();
            return new User(id, firstName, lastName, email,  phoneNumber, passwordHash);
        }

        public void Update(string firstName, string lastName, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            AddDomainEvent(new UserUpdatedDomainEvent(Guid.NewGuid(), this));
        }

        public void StatusChange(int newStatus)
        {
            StatusId = newStatus;
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

        public bool PasswordMatch(string password)
        {
            return this.PasswordHash == password.EncryptPassword();

        }

        public void Login()
        {
            AddDomainEvent(new UserLoginDomainEvent(Guid.NewGuid(), this));
        }


    }
}
