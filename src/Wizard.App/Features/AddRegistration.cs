using Mapster;
using MediatR;
using Wizard.Data;
using Wizard.Domain.Entities;

namespace Wizard.App.Features;

public static class AddRegistration
{
    public record Command(string Email, string Password, int CountryId, int ProvinceId) : IRequest<Result>;

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMediator _mediator;

        public Handler(AppDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
        {
            var existedEmail = await _mediator.Send(new GetEmailInfo.Query(command.Email), cancellationToken);
            if (existedEmail != null)
            {
                return new Result(null, "Email already registered");
            }

            var registrationInfo = command.Adapt<RegistrationInfo>(); 

            try
            {
                await _dbContext.RegistrationInfos.AddAsync(registrationInfo, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                return new Result(null, e.Message);
            }

            return new Result(registrationInfo.Email, null);
        }
    }

    public record Result(string? Email, string? Error);
}