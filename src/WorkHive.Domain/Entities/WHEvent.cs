namespace WorkHive.Domain.Entities;
public class WHEvent : WHEntityBase
{
    public Guid OwnerId { get; set; }
    public WHUser Owner { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public string Location { get; set; }
    public WHEventType EventAttributes { get; set; }
    public string Description { get; set; }
    public int? MaxGuest { get; set; }

    public virtual ICollection<WHUser> Guests { get; set; }

    /// <summary>
    /// Tags the user can set like wifi/smoking area/...
    /// </summary>
    public List<string> LocationAttributes { get; set; }
}