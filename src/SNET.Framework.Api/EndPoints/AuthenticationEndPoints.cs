using Carter;
using MediatR;
using SNET.Framework.Domain.Shared;
using SNET.Framework.Features.Users.Commands;

namespace SNET.Framework.Api.EndPoints
{
    public class AuthenticationEndPoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapPost("Login", async (LoginUserCommand command, IMediator mediator) =>
            {
                var res = await mediator.Send(command);

                if (res.IsSuccess)
                {
                    return Results.Ok(res);
                }
                else
                {
                    return Results.BadRequest(res);
                }
            })
            .WithName("Autentication")
            .WithTags("Login")
            .Produces<Result>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}
