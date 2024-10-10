using MediatR;
using SNET.Framework.Domain.Shared;

namespace SNET.Framework.Features.Users.Commands.AssignRole;

public record AssignRoleToUserCommand : IRequest<Result>
{
    public Guid UserId { get; init; }
    public Guid RoleId { get; init; }
}
