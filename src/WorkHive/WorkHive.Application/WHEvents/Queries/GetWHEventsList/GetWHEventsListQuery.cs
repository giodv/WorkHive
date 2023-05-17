using MediatR;

namespace WorkHive.Application.WHEvents.Queries.GetWHEventById;
public record GetWHEventsListQuery : IRequest<IEnumerable<WHEventModel>>
{
    public Guid Guid { get; init; }
}


public class GetWHEventsListQueryHandler : IRequestHandler<GetWHEventsListQuery, IEnumerable<WHEventModel>>
{
    public Task<IEnumerable<WHEventModel>> Handle(GetWHEventsListQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
