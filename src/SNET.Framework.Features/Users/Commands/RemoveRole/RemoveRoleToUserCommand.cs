using MediatR;
using SNET.Framework.Domain.Shared;

namespace SNET.Framework.Features.Users.Commands.RemoveRole;

public record RemoveRoleToUserCommand(Guid UserId, Guid RoleId) : IRequest<Result>;