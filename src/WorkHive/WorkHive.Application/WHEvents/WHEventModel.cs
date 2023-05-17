﻿using WorkHive.Core;
using WorkHive.Core.Entities;

namespace WorkHive.Application.WHEvents;
public record WHEventModel(Guid Id, Guid OrganizerId, DateTime StartDate, DateTime EndDate, string Location, int EventType, string Description, int? MaxGuest, IList<Guid> GuestIds)
{
    public WHEventModel(WHEventEntity entity) : this(entity.Id, entity.OrganizerId, entity.StartDate, entity.EndDate, entity.Location, (int)entity.EventType, entity.Description, entity.MaxGuest, entity.GuestIds.Select(e => e.Id).ToList())
    {

    }
}


