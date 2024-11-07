using FluentValidation;
using MediatR;
using SNET.Framework.Domain.Shared;

namespace SNET.Framework.Features.Users.Commands;

public record LoginUserCommand : IRequest<Result>
{
    public string Email { get; init; }
    public string Password { get; init; }
}

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
 
        RuleFor(x => x.Email).NotEmpty()
            .WithMessage("El campo email es obligatorio")
            .EmailAddress()
            .WithMessage("Debe ingresar un email valido");

        RuleFor(x => x.Password).NotEmpty()
            .WithMessage("El campo contraseña es obligatorio")
            .MinimumLength(6)
            .WithMessage("Mimimo 6 caracrteres");

    }
}
