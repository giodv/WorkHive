using Google.Protobuf.WellKnownTypes;
using WorkHive.Application.WHEvents;

namespace WorkHive.Server.Helper;

public static class WHEventReplyExtension
{
    public static WHEventReply CreateFromModel(WHEventModel model)
    {
        var startDateTimeOffset = new DateTimeOffset(model.StartDate, TimeSpan.Zero);
        var endDateTimeOffset = new DateTimeOffset(model.EndDate, TimeSpan.Zero);
        
        var reply = new WHEventReply
        {
            Id = model.Id.ToString(),
            Description = model.Description,
            StartDateTime = startDateTimeOffset.ToUnixTimeSeconds(),
            EndDateTime = endDateTimeOffset.ToUnixTimeSeconds(),
            EventType = (uint)model.EventType,
            Location = model.Location,
            MaxGuest = model.MaxGuest.HasValue ? model.MaxGuest.Value : 0,
            OrganizerId = model.OrganizerId.ToString(),
        };
        foreach (var gid in model.GuestIds)
        {
            reply.GuestIds.Add(gid.ToString());
        }

        foreach (var attibute in model.LocationAttributes)
        {
            reply.Attributes.Add(attibute);
        }

        return reply;
    }
}
