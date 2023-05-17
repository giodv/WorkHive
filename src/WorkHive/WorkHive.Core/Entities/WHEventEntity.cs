using System.ComponentModel.DataAnnotations.Schema;

namespace WorkHive.Core.Entities;
public class WHEventEntity
{
    public WHEventEntity()
    {
        GuestIds = new List<WHEventGuestEntity>();
    }
    public Guid Id { get; set; }
    public Guid OrganizerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; }
    public WHEventType EventType { get; set; }
    public string Description { get; set; }
    public int? MaxGuest { get; set; }

    public ICollection<WHEventGuestEntity> GuestIds { get; set; }

}

public class WHEventGuestEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
}