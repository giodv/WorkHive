using WorkHive.Domain.Entities;

namespace WorkHive.Application.WHEvents;
public record WHEventModel(Guid Id, Guid OrganizerId, DateTime StartDate, DateTime EndDate, string Location, int EventType, string Description, int? MaxGuest, IList<Guid> GuestIds)
{
    public WHEventModel(WHEvent entity) : this(entity.Id, entity.OwnerId, entity.StartDate, entity.EndDate, entity.Location, (int)entity.EventAttributes, entity.Description, entity.MaxGuest, entity.Guests.Select(x => x.Id).ToList())
    {

    }
}


