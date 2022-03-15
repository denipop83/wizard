using MediatR;
using Microsoft.EntityFrameworkCore;
using Wizard.App.Models;
using Wizard.Data;

namespace Wizard.App.Features;

public static class GetEmailInfo
{
    public record Query(string Email) : IRequest<EmailInfo?>;

    public class Handler : IRequestHandler<Query, EmailInfo?>
    {
        private readonly AppDbContext _dbContext;

        public Handler(AppDbContext dbContext) => _dbContext = dbContext;

        public async Task<EmailInfo?> Handle(Query query, CancellationToken cancellationToken)
        {
            var registrationInfo = await _dbContext.RegistrationInfos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == query.Email, cancellationToken);

            return registrationInfo != null ? new EmailInfo(registrationInfo.Email) : null;
        }
    }
}