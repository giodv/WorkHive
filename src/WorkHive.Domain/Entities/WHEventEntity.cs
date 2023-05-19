﻿namespace WorkHive.Core.Entities;
public class WHEventEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid OrganizerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; }
    public WHEventType EventType { get; set; }
    public string Description { get; set; }
    public int? MaxGuest { get; set; }

    public List<Guid> GuestIds { get; set; } = new();

}