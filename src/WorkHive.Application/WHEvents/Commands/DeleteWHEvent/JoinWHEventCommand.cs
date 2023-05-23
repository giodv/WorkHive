using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Common.Interfaces;
using WorkHive.Domain.Entities;

namespace WorkHive.Application.WHEvents.Commands.DeleteWHEvent;
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
        var guestUser = await _applicationDbContext.WhUsers.SingleAsync(e => e.Id == request.GuestId);

        if (entity != null && guestUser != null)
        {
            if ((!entity.MaxGuest.HasValue || entity.MaxGuest.Value > entity.Guests.Count) && !entity.Guests.Select(x => x.Id).Contains(request.GuestId))
            {
                entity.Guests.Add(guestUser);
            }
        }

        _applicationDbContext.WHEvents.Update(entity);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

    }
}
