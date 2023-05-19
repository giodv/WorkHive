using System.ComponentModel.DataAnnotations;

namespace WorkHive.Domain.Entities;

public class WHCompany
{
    [Key] 
    public Guid Id { get; set; }
    public string Name { get; set; }
}