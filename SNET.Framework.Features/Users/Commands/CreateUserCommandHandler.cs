using FluentValidation;
using MediatR;
using SNET.Framework.Domain.Shared;

namespace SNET.Framework.Features.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{
    private readonly IValidator<CreateUserCommand> _validator;
    public CreateUserCommandHandler(IValidator<CreateUserCommand> validator)
    {
        _validator = validator;
    }

    public Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(request);
        if (!result.IsValid)
        {
            
        }

        throw new NotImplementedException();
    }
}
