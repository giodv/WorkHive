using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorkHive.Application.Common.Interfaces;
using WorkHive.Application.Common.Logging;

namespace WorkHive.Application.WHEvents.Queries.GetWHEventsList;
public record GetWHEventsListQuery : IRequest<IEnumerable<WHEventModel>>
{
    public DateTimeOffset? StartDateTime { get; }
    public DateTimeOffset? EndDateTime { get; }
    public string Location { get; }

    public GetWHEventsListQuery(DateTimeOffset? startDatetime, DateTimeOffset? endDateTime, string location)
    {
        StartDateTime = startDatetime;
        EndDateTime = endDateTime;
        Location = location;
    }
}


public class GetWHEventsListQueryHandler : IRequestHandler<GetWHEventsListQuery, IEnumerable<WHEventModel>>
{
    private readonly ILogger<GetWHEventsListQueryHandler> _logger;
    private readonly IApplicationDbContext _applicationDbContext;

    public GetWHEventsListQueryHandler(ILogger<GetWHEventsListQueryHandler> logger, IApplicationDbContext applicationDbContext)
    {
        _logger = logger;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<WHEventModel>> Handle(GetWHEventsListQuery request, CancellationToken cancellationToken)
    {

        try
        {
            var response = _applicationDbContext.WHEvents.AsQueryable();
            if (request.StartDateTime.HasValue)
            {
                response = response.Where(el => el.StartDate > request.StartDateTime);
            }
            if (request.EndDateTime.HasValue)
            {
                response = response.Where(el => el.EndDate < request.EndDateTime);
            }
            if (!string.IsNullOrWhiteSpace(request.Location))
            {
                response = response.Where(el => EF.Functions.Like(el.Location, $"%{request.Location}%"));
            }

            return await response.Select(entity => new WHEventModel(entity)).ToListAsync();

        }
        catch (Exception ex)
        {
            _logger.ErrorWhileExecutingQuery(ex);
            throw;
        }
    }
}
