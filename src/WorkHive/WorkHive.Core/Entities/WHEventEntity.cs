using System.ComponentModel.DataAnnotations.Schema;

namespace WorkHive.Core.Entities;
public class WHEventEntity
{
    public Guid Id { get; set; }
    public Guid OrganizerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; }
    public WHEventType EventType { get; set; }
    public string Description { get; set; }
    public int? MaxGuest { get; set; }

    [NotMapped]
    public List<Guid> GuestIds { get; set; } = new List<Guid>();

}
