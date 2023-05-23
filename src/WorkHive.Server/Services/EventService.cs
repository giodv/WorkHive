using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using WorkHive.Application.Common.Exceptions;
using WorkHive.Application.WHEvents;
using WorkHive.Application.WHEvents.Commands.CreateWHEvent;
using WorkHive.Application.WHEvents.Commands.DeleteWHEvent;
using WorkHive.Application.WHEvents.Commands.JoinWHEvent;
using WorkHive.Application.WHEvents.Commands.UpdateWHEvent;
using WorkHive.Application.WHEvents.Queries.GetWHEventById;
using WorkHive.Application.WHEvents.Queries.GetWHEventsList;
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
        try
        {
            CreateWHEventCommand createRequest = new CreateWHEventCommand(
                Guid.Parse("B4B117AA-3AD8-4D13-802A-B8BAD0DC8E95"), 
                new DateTime(request.StartDateTime), 
                new DateTime(request.EndDateTime), 
                request.Location, 
                (WHEventType)request.EventType, 
                request.Description,
                request.HasMaxGuest ? request.MaxGuest : null);
            WHEventModel response = await _mediator.Send(createRequest);
            return WHEventReplyExtension.CreateFromModel(response);

        }
        catch (ValidationException ex)
        {
            var metadata = new Metadata
            {
                { "ErrorMessage", ex.Message }
            };
            throw new RpcException(new Status(StatusCode.Internal, "Validation Exception"), metadata);
        }
    }

    public override async Task<WHEventReply> GetEvent(GetEventRequest request, ServerCallContext context)
    {
        var response = await _mediator.Send(new GetWHEventByIdQuery(Guid.Parse(request.Id)));
        return WHEventReplyExtension.CreateFromModel(response);
    }

    public override async Task GetEventStream(GetEventFilterRequest request, IServerStreamWriter<WHEventReply> responseStream, ServerCallContext context)
    {
        GetWHEventsListQuery requestQuery = new GetWHEventsListQuery(
            request.HasStartDateTime ? new DateTime(request.StartDateTime, DateTimeKind.Utc) : DateTimeOffset.MinValue,
            request.HasEndDateTime ? new DateTime(request.EndDateTime, DateTimeKind.Utc) : DateTimeOffset.MaxValue,
            request.Location,
            request.HasOrganizerId ? Guid.Parse(request.OrganizerId) : null,
            (WHEventType)request.EventType);

        foreach (var el in await _mediator.Send(requestQuery))
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
        //TODO: Get The guest ID from the auth token
        await _mediator.Send(new JoinWHEventCommand(Guid.Parse(request.Id), Guid.Parse("B4B117AA-3AD8-4D13-802A-B8BAD0DC8E95")));

        return new Empty();

    }

    public override async Task<WHEventsReply> GetEvents(GetEventFilterRequest request, ServerCallContext context)
    {
        GetWHEventsListQuery requestQuery = new GetWHEventsListQuery(
            request.HasStartDateTime ? new DateTime(request.StartDateTime, DateTimeKind.Utc) : null,
            request.HasEndDateTime ? new DateTime(request.EndDateTime, DateTimeKind.Utc) : null,
            request.Location,
            request.HasOrganizerId ? Guid.Parse(request.OrganizerId) : null,
            (WHEventType)request.EventType);


        var response = new WHEventsReply();
        foreach (var el in await _mediator.Send<IEnumerable<WHEventModel>>(requestQuery))
        {
            response.Events.Add(WHEventReplyExtension.CreateFromModel(el));
        }

        return response;

    }

    public override async Task<WHEventReply> UpdateEvent(UpdateEventRequest request, ServerCallContext context)
    {
        UpdateWHEventCommand requestCommand = new UpdateWHEventCommand(Guid.Parse(request.Id))
        {
            Description = request.HasDescription ? request.Description : null,
            Location = request.HasLocation ? request.Location : null,
            StartDate = request.HasStartDateTime ? new DateTime(request.StartDateTime, DateTimeKind.Utc) : null,
            EndDate = request.HasEndDateTime ? new DateTime(request.EndDateTime, DateTimeKind.Utc) : null,
            EventType = request.EventType != 0 ? (WHEventType)request.EventType : null,
            MaxGuest = request.HasMaxGuest ? request.MaxGuest : null,
        };


        var response = await _mediator.Send(requestCommand);

        return WHEventReplyExtension.CreateFromModel(response);
    }

}
