using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Common.Interfaces;

namespace WorkHive.Application.WHEvents.Queries.GetWHEventsList;
public record GetWHEventsListQuery : IRequest<IEnumerable<WHEventModel>>
{
    public DateTime StartDatetime { get; }

    public GetWHEventsListQuery(DateTime startDatetime)
    {
        StartDatetime = startDatetime;
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
        return await _applicationDbContext.WHEvents.Where(el => el.StartDate > request.StartDatetime).Select(entity => new WHEventModel(entity)).ToListAsync();
    }
}
