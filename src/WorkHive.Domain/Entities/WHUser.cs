using System.ComponentModel.DataAnnotations;

namespace WorkHive.Domain.Entities;

public class WHUser: WHEntityBase
{
    public Guid CompanyId { get; set; }
    public WHCompany WhCompany { get; set; }
}