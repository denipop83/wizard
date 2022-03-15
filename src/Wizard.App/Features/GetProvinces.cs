using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Wizard.App.Models;
using Wizard.Data;

namespace Wizard.App.Features;

public static class GetProvinces
{
    public record Query(int CountryId) : IRequest<Result>;

    public class Handler : IRequestHandler<Query, Result>
    {
        private readonly AppDbContext _dbContext;

        public Handler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(Query query, CancellationToken cancellationToken) =>
            new(await _dbContext.Provinces
                .AsNoTracking()
                .Where(x => x.Country.Id == query.CountryId)
                .ProjectToType<Province>()
                .ToArrayAsync(cancellationToken));
    }

    public record Result(IEnumerable<Province> Provinces);
}