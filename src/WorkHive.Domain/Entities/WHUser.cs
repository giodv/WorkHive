namespace WorkHive.Domain.Entities;

public class WHUser : WHEntityBase
{
    public Guid WhCompanyId { get; set; }
    public WHCompany WhCompany { get; set; }

    public virtual ICollection<WHEvent> GuestEvents { get; set; }

    public virtual ICollection<WHEvent> OwnerEvents { get; set; }

}