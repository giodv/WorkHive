using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Common.Interfaces;

namespace WorkHive.Application.WHEvents.Queries.GetWHEventsList;
public record GetWHEventsListQuery : IRequest<IEnumerable<WHEventModel>>
{
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }

    public GetWHEventsListQuery(DateTime startDatetime, DateTime endDateTime)
    {
        StartDateTime = startDatetime;
        EndDateTime = endDateTime;
    }
}


public class GetWHEventsListQueryHandler : IRequestHandler<GetWHEventsListQuery, IEnumerable<WHEventModel>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetWHEventsListQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<WHEventModel>> Handle(GetWHEventsListQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.WHEvents
            .Where(el => el.StartDate > request.StartDateTime)
            .Where(el => el.EndDate < request.EndDateTime)
            .Select(entity => new WHEventModel(entity))
            .ToListAsync();
    }
}
