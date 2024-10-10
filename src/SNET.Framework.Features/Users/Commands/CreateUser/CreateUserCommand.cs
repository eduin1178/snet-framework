using FluentValidation;
using MediatR;
using SNET.Framework.Domain.Shared;

namespace SNET.Framework.Features.Users.Commands;

public record CreateUserCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string PhoneNumber { get; init; }
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        
        RuleFor(x => x.Password).NotEmpty()
            .WithMessage("El campo contraseña es obligatorio")
            .MinimumLength(6)
            .WithMessage("Mimimo 6 caracrteres");

        RuleFor(x => x.PhoneNumber).NotEmpty();
    }
}
