using Google.Protobuf.WellKnownTypes;
using WorkHive.Application.WHEvents;

namespace WorkHive.Server.Helper;

public static class WHEventReplyExtension
{
    public static WHEventReply CreateFromModel(WHEventModel model)
    {
        var reply = new WHEventReply
        {
            Id = model.Id.ToString(),
            Description = model.Description,
            StartDateTime = model.StartDate.Ticks,
            EndDateTime = model.EndDate.Ticks,
            EventType = (uint)model.EventType,
            Location = model.Location,
            MaxGuest = model.MaxGuest,
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
