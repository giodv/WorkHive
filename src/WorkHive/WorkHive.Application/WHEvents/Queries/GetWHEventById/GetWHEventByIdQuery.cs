using MediatR;

namespace WorkHive.Application.WHEvents.Queries.GetWHEventById;
public record GetWHEventByIdQuery : IRequest<WHEventModel>
{
    public Guid Guid { get; init; }
}


public class GetWHEventByIdQueryHandler : IRequestHandler<GetWHEventByIdQuery, WHEventModel>
{
    public Task<WHEventModel> Handle(GetWHEventByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
