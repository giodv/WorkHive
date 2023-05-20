using System.ComponentModel.DataAnnotations;

namespace WorkHive.Domain.Entities;

public class WHCompany: WHEntityBase
{
    public string Name { get; set; }

    public virtual ICollection<WHUser> Users { get; set; }
}