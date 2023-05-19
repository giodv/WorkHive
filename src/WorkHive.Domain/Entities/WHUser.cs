using System.ComponentModel.DataAnnotations;

namespace WorkHive.Domain.Entities;

public class WHUser
{
    [Key] public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public WHCompany WhCompany { get; set; }
}