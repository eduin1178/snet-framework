using FluentValidation;
using MediatR;
using SNET.Framework.Domain.DomainEvents.Users;
using SNET.Framework.Domain.Entities;
using SNET.Framework.Domain.Repositories;
using SNET.Framework.Domain.Shared;
using SNET.Framework.Domain.UnitOfWork;

namespace SNET.Framework.Features.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    private readonly IValidator<CreateUserCommand> _validator;
    public CreateUserCommandHandler(
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

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(request);
        if (!result.IsValid)
        {            
            return Result.Failure(new Error("CreateUser.ValidationError", "Datos no válidos"));
        }
        
        var user = User.Create(request.Id,
            request.FirstName,
            request.LastName, 
            request.Email,
            request.PhoneNumber, 
            request.Password);

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        user.AddDomainEvent(new UserCreatedDomainEvent(Guid.NewGuid(), request.Id, request.FirstName, request.LastName));

        return Result.Success("Usuario creado correctamente");
    }
}
