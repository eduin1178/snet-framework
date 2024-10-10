using FluentValidation;
using MediatR;
using SNET.Framework.Domain.Repositories;
using SNET.Framework.Domain.Shared;
using SNET.Framework.Domain.UnitOfWork;

namespace SNET.Framework.Features.Users.Commands.RemoveRole;

public class RemoveRoleToUserHandler : IRequestHandler<RemoveRoleToUserCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    private readonly IValidator<CreateUserCommand> _validator;
    public RemoveRoleToUserHandler(
        IUserRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<CreateUserCommand> validator,
        IMediator mediator)
    {
        _userRepository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _mediator = mediator;
    }
    public async Task<Result> Handle(RemoveRoleToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository
        .GetByIdWithRoles(request.UserId);

        user.RemoveRole(request.RoleId);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success("Rol removido correctamente");
    }
}
