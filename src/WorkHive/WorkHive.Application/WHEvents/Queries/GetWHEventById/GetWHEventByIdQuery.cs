using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Common.Interfaces;

namespace WorkHive.Application.WHEvents.Queries.GetWHEventById;
public record GetWHEventByIdQuery : IRequest<WHEventModel>
{
    public Guid Guid { get; init; }

    public GetWHEventByIdQuery(Guid guid)
    {
        Guid = guid;
    }
}


public class GetWHEventByIdQueryHandler : IRequestHandler<GetWHEventByIdQuery, WHEventModel>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetWHEventByIdQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<WHEventModel> Handle(GetWHEventByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _applicationDbContext.WHEvents.SingleAsync(e => e.Id == request.Guid);
        return new WHEventModel(entity);

    }
}
