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
            StartDateTime = Timestamp.FromDateTime(model.StartDate.ToUniversalTime()),
            EndDateTime = Timestamp.FromDateTime(model.EndDate.ToUniversalTime()),
            EventType = (uint)model.EventType,
            Location = model.Location,
            MaxGuest = model.MaxGuest,
            OrganizerId = model.OrganizerId.ToString()
        };
        foreach (var gid in model.GuestIds)
        {
            reply.GuestIds.Add(gid.ToString());
        }

        return reply;
    }
}
