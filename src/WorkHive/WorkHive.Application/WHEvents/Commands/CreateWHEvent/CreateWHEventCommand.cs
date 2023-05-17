using MediatR;
using WorkHive.Application.Common.Interfaces;
using WorkHive.Core;
using WorkHive.Core.Entities;

namespace WorkHive.Application.WHEvents.Commands.CreateWHEvent;
public record CreateWHEventCommand : IRequest<WHEventModel>
{

    public CreateWHEventCommand(Guid organizerId, DateTime startDate, DateTime endDate, string location, WHEventType eventType, string description, int? maxGuest)
    {
        OrganizerId = organizerId;
        StartDate = startDate;
        EndDate = endDate;
        Location = location;
        EventType = eventType;
        Description = description;
        MaxGuest = maxGuest;
    }

    public Guid OrganizerId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string Location { get; init; }
    public WHEventType EventType { get; init; }
    public string Description { get; init; }
    public int? MaxGuest { get; init; }

    public WHEventEntity ToEntity()
    {
        return new WHEventEntity
        {
            OrganizerId = OrganizerId,
            StartDate = StartDate,
            EndDate = EndDate,
            Description = Description,
            EventType = EventType,
            MaxGuest = MaxGuest,
            Location = Location
        };
    }
}

public class CreateWHEventCommandHandler : IRequestHandler<CreateWHEventCommand, WHEventModel>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public CreateWHEventCommandHandler(IApplicationDbContext applicationDbContext)
    {
        this._applicationDbContext = applicationDbContext;
    }
    public async Task<WHEventModel> Handle(CreateWHEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _applicationDbContext.WHEvents.AddAsync(request.ToEntity());

        return new WHEventModel(entity.Entity);
    }
}