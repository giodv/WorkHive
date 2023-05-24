using MediatR;
using WorkHive.Application.Common.Interfaces;
using WorkHive.Domain;
using WorkHive.Domain.Entities;

namespace WorkHive.Application.WHEvents.Commands.CreateWHEvent;
public record CreateWHEventCommand : IRequest<WHEventModel>
{

    public CreateWHEventCommand(Guid organizerId, DateTime startDate, DateTime endDate, string location, WHEventType eventType, string description, int? maxGuest, List<string> locationAttributes)
    {
        OrganizerId = organizerId;
        StartDate = startDate;
        EndDate = endDate;
        Location = location;
        EventType = eventType;
        Description = description;
        MaxGuest = maxGuest;
        LocationAttributes = locationAttributes;
    }

    public Guid OrganizerId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string Location { get; init; }
    public WHEventType EventType { get; init; }
    public string Description { get; init; }
    public int? MaxGuest { get; init; }
    public List<string> LocationAttributes { get; init; }

    public WHEvent ToEntity()
    {
        return new WHEvent
        {
            OwnerId = OrganizerId,
            StartDate = StartDate,
            EndDate = EndDate,
            Description = Description,
            EventAttributes = EventType,
            MaxGuest = MaxGuest,
            Location = Location,
            LocationAttributes = LocationAttributes
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
        if (!_applicationDbContext.WhUsers.Any(e => e.Id == request.OrganizerId))
        {
            await _applicationDbContext.WhUsers.AddAsync(new WHUser() { Id = request.OrganizerId, WhCompanyId = Guid.Parse("B4B117AA-3AD8-4D13-802A-B8BAD0DC8E94") });
        }

        var entity = await _applicationDbContext.WHEvents.AddAsync(request.ToEntity(), cancellationToken);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return new WHEventModel(entity.Entity);
    }
}