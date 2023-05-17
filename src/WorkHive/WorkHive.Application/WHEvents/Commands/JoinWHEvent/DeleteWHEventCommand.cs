using MediatR;
using WorkHive.Core;

namespace WorkHive.Application.WHEvents.Commands.CreateWHEvent;
public record DeleteWHEventCommand : IRequest
{
    public Guid OrganizerId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string Location { get; init; }
    public WHEventType[] eventType { get; init; }
    public string Description { get; init; }
    public int? MaxGuest { get; init; }
}

public class DeleteWHEventCommandHandler : IRequestHandler<DeleteWHEventCommand>
{
    public Task Handle(DeleteWHEventCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}