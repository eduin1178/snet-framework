using FluentValidation;
using MediatR;
using SNET.Framework.Domain.Autentications;
using SNET.Framework.Domain.Entities;
using SNET.Framework.Domain.Repositories;
using SNET.Framework.Domain.Shared;
using SNET.Framework.Domain.UnitOfWork;

namespace SNET.Framework.Features.Users.Commands;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    private readonly IManagerToken _managerToken;
    private readonly IValidator<LoginUserCommand> _validator;
    public LoginUserCommandHandler(
        IUserRepository repository, 
        IUnitOfWork unitOfWork,
        IValidator<LoginUserCommand> validator, 
        IMediator mediator,
        IManagerToken managerToken)
    {
        _userRepository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _mediator = mediator;
        this._managerToken = managerToken;
    }

    public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(request);
        if (!result.IsValid)
        {            
            return Result.Failure(new Error("LoginUser.ValidationError", "Datos no válidos"));
        }      

        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null) {
            return Result.Failure(new Error("LoginUser.ValidationError", "Credenciales de acceso no validas, el email no existe"));
        }

        if (!user.PasswordMatch(request.Password)) {
            return Result.Failure(new Error("LoginUser.ValidationError", "Credenciales de acceso no validas, contraseña no valida"));
        }

        user.Login();

        var token = _managerToken.GenerateToken(user);

        return Result.Success(token, "Usuario creado correctamente");
    }
}
