using MediatR;
using WorkHive.Core;

namespace WorkHive.Application.WHEvents.Commands.CreateWHEvent;
public record JoinWHEventCommand : IRequest
{
    public Guid OrganizerId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string Location { get; init; }
    public WHEventType[] eventType { get; init; }
    public string Description { get; init; }
    public int? MaxGuest { get; init; }
}

public class JoinWHEventCommandHandler : IRequestHandler<JoinWHEventCommand>
{
    public Task Handle(JoinWHEventCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}