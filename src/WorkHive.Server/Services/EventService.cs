using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using WorkHive.Application.WHEvents;
using WorkHive.Application.WHEvents.Commands.CreateWHEvent;
using WorkHive.Application.WHEvents.Queries.GetWHEventById;
using WorkHive.Domain;
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

    public override async Task<WHEventReply> GetEvent(GetEventRequest request, ServerCallContext context)
    {
        WHEventModel response = await _mediator.Send(new GetWHEventByIdQuery(Guid.Parse(request.Id)));
        return WHEventReplyExtension.CreateFromModel(response);
    }

    public override async Task GetEventStream(GetEventFilterRequest request, IServerStreamWriter<WHEventReply> responseStream, ServerCallContext context)
    {
        foreach (var el in await _mediator.Send(new GetWHEventsListQuery()))
        {
            await responseStream.WriteAsync(WHEventReplyExtension.CreateFromModel(el));
        }
    }

    public override async Task<Empty> DeleteEvent(DeleteEventRequest request, ServerCallContext context)
    {
        await _mediator.Send(new DeleteWHEventCommand(Guid.Parse(request.Id)));

        return new Empty();
    }

    public override async Task<Empty> JoinEvent(JoinEventRequest request, ServerCallContext context)
    {
        // TODO: Get The guest ID from the auth token
        await _mediator.Send(new JoinWHEventCommand(Guid.Parse(request.Id), Guid.NewGuid()));

        return new Empty();
    }
}
