using FluentValidation;
using MediatR;
using SNET.Framework.Domain.Authentications;
using SNET.Framework.Domain.Authentications.Jwt;
using SNET.Framework.Domain.Repositories;
using SNET.Framework.Domain.Shared;
using SNET.Framework.Domain.UnitOfWork;

namespace SNET.Framework.Features.Users.Commands;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result>
{
    private readonly IUserRepository _userRepository;
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
        _validator = validator;
        _mediator = mediator;
        this._managerToken = managerToken;
    }

    public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(request);
        if (!result.IsValid)
        {
            return Result.Failure(new Error("Autentication.ValidationError", "Datos no válidos"));
        }

        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
        {
            return Result.Failure(new Error("Autentication.NotUserMatch", "Credenciales de acceso no validas"));
        }

       var resLogin = user.Login(request.Password);

        if (resLogin.IsSuccess)
        {
             var token = _managerToken.GenerateToken(user);
            return Result<TokenModel>.Success(token, "Autenticado correctamente");
        } 
        else
        {
            return Result.Failure(new Error("Autentication.errorToken", "Error al generar el token"));
        }

    }
}
