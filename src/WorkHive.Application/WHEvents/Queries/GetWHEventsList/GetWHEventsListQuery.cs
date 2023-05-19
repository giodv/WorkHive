﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Common.Interfaces;

namespace WorkHive.Application.WHEvents.Queries.GetWHEventById;
public record GetWHEventsListQuery : IRequest<IEnumerable<WHEventModel>>
{
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
        return await _applicationDbContext.WHEvents.Where(el => el.StartDate > DateTime.Now).Select(entity => new WHEventModel(entity)).ToListAsync();
    }
}