using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Common.Interfaces;
using WorkHive.Domain.Entities;

namespace WorkHive.Application.WHEvents.Commands.CreateWHEvent;
public record JoinWHEventCommand : IRequest
{

    public JoinWHEventCommand(Guid id, Guid guestId)
    {
        Id = id;
        GuestId = guestId;
    }
    public Guid Id { get; init; }

    public Guid GuestId { get; init; }
}

public class JoinWHEventCommandHandler : IRequestHandler<JoinWHEventCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public JoinWHEventCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task Handle(JoinWHEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _applicationDbContext.WHEvents.SingleAsync(e => e.Id == request.Id);

        if (entity != null)
        {
            if (entity.MaxGuest.HasValue && entity.MaxGuest.Value > entity.Guests.Count && !entity.Guests.Select(x => x.Id).Contains(request.GuestId))
            {
                entity.Guests.Add(new WHUser { Id = request.GuestId });
            }
        }

        _applicationDbContext.WHEvents.Update(entity);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}
