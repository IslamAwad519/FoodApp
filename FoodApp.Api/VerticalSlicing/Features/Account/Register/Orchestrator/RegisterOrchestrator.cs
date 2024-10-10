using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Features.Account.Register.Commands;
using FoodApp.Api.VerticalSlicing.Features.Account.ResendVerificationCode.Commands;
using MediatR;

namespace FoodApp.Api.VerticalSlicing.Features.Account.Register.Orchestrator
{
    public record RegisterOrchestrator(
   string UserName,
   string Email,
   string Country,
   string PhoneNumber,
   string Password,
   string ConfirmPassword) : IRequest<Result<bool>>;

    public class RegisterOrchestratorHandler : BaseRequestHandler<RegisterOrchestrator, Result<bool>>
    {
        public RegisterOrchestratorHandler(RequestParameters requestParameters) : base(requestParameters) { }

        public async override Task<Result<bool>> Handle(RegisterOrchestrator request, CancellationToken cancellationToken)
        {
            var command = request.Map<RegisterCommand>();

            var RegisterResult = await _mediator.Send(command);

            if (!RegisterResult.IsSuccess)
            {
                return Result.Failure<bool>(UserErrors.UserDoesntCreated);
            }

            await _mediator.Send(new SendVerificationOTP(request.Email));

            return Result.Success(true);
        }
    }
}
