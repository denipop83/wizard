using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Wizard.App.Models;
using Wizard.Data;

namespace Wizard.App.Features;

public static class GetCountries
{
    public record Query : IRequest<CountriesResponse>;
    
    public class Handler : IRequestHandler<Query, CountriesResponse>
    {
        private readonly AppDbContext _dbContext;

        public Handler(AppDbContext dbContext) => _dbContext = dbContext;

        public async Task<CountriesResponse> Handle(Query query, CancellationToken cancellationToken) =>
            new(await _dbContext.Countries
                .AsNoTracking()
                .ProjectToType<Country>()
                .ToArrayAsync(cancellationToken));
    }
}