namespace WorkHive.Domain.Entities;
public class WHBabysitEvent : WHEvent
{
    public virtual WHBabySitter? BabySitter { get; set; }

    public Guid? BabySitterId { get; set; }

    public virtual WHUser? User { get; set; }

    public Guid? UserId { get; set; }

}