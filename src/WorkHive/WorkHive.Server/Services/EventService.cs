using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using WorkHive.Application.WHEvents;
using WorkHive.Application.WHEvents.Commands.CreateWHEvent;
using WorkHive.Core;
using WorkHive.Server.Helper;

namespace WorkHive.Server.Services;
public class EventService : WHEvent.WHEventBase
{
    private readonly ILogger<EventService> _logger;
    private readonly IMediator _mediator;

    public EventService(ILogger<EventService> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public override async Task<WHEventReply> CreateEvent(CreateEventRequest request, ServerCallContext context)
    {
        // TODO: Get The organizer ID from the auth token
        WHEventModel response = await _mediator.Send(new CreateWHEventCommand(Guid.NewGuid(), request.StartDateTime.ToDateTime(), request.EndDateTime.ToDateTime(), request.Location, (WHEventType)request.EventType, request.Description, request.MaxGuest));
        return WHEventReplyExtension.CreateFromModel(response);
    }

    public override Task<WHEventReply> GetEvent(GetEventRequest request, ServerCallContext context)
    {
        return Task.FromResult(new WHEventReply
        {
            Description = "Hello " + request.Id
        });
    }
}
