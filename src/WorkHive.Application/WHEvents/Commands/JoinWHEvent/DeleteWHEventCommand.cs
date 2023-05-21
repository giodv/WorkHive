using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Common.Interfaces;

namespace WorkHive.Application.WHEvents.Commands.JoinWHEvent;
public record DeleteWHEventCommand : IRequest
{
    public Guid Id { get; init; }

    public DeleteWHEventCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteWHEventCommandHandler : IRequestHandler<DeleteWHEventCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public DeleteWHEventCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task Handle(DeleteWHEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _applicationDbContext.WHEvents.SingleAsync(e => e.Id == request.Id);

        if (entity != null)
        {
            _applicationDbContext.WHEvents.Remove(entity);
        }
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}