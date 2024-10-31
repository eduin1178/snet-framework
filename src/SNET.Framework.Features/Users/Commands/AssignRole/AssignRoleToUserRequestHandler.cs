using FluentValidation;
using MediatR;
using SNET.Framework.Domain.Repositories;
using SNET.Framework.Domain.Shared;
using SNET.Framework.Domain.UnitOfWork;

namespace SNET.Framework.Features.Users.Commands.AssignRole;

public class AssignRoleToUserRequestHandler : IRequestHandler<AssignRoleToUserCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignRoleToUserRequestHandler(
        IUserRepository repository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetByIdWithRoles(request.UserId);

        user.AssignRole(request.RoleId);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success("Rol asignado correctamente");
    }
}
