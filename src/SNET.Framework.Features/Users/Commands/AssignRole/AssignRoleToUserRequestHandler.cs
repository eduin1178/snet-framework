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
    private readonly IMediator _mediator;
    private readonly IValidator<CreateUserCommand> _validator;
    public AssignRoleToUserRequestHandler(
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

    public async Task<Result> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository
                .GetByIdWithRoles(request.UserId);

            user.AssignRole(request.RoleId);
        }
        catch (Exception ex)
        {

        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success("Rol asignado correctamente");
    }
}
