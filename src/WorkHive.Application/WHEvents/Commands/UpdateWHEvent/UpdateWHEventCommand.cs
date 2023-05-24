using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Common.Interfaces;
using WorkHive.Domain;

namespace WorkHive.Application.WHEvents.Commands.UpdateWHEvent;
public record UpdateWHEventCommand : IRequest<WHEventModel>
{
    public Guid Id { get; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string? Location { get; init; }
    public WHEventType? EventType { get; init; }
    public string? Description { get; init; }
    public int? MaxGuest { get; init; }
    public List<string> LocationAttributes { get; init; }


    public UpdateWHEventCommand(Guid id)
    {
        Id = id;
    }
}

public class UpdateWHEventCommandHandler : IRequestHandler<UpdateWHEventCommand, WHEventModel>
{
    private readonly IApplicationDbContext _context;

    public UpdateWHEventCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WHEventModel> Handle(UpdateWHEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.WHEvents.SingleAsync(e => e.Id == request.Id, cancellationToken: cancellationToken);

        if (request.StartDate.HasValue)
        {
            entity.StartDate = request.StartDate.Value;
        }
        if (request.EndDate.HasValue)
        {
            entity.EndDate = request.EndDate.Value;
        }
        if (request.MaxGuest.HasValue)
        {
            entity.MaxGuest = request.MaxGuest.Value;
        }
        if (!string.IsNullOrWhiteSpace(request.Description))
        {
            entity.Description = request.Description;
        }
        if (!string.IsNullOrWhiteSpace(request.Location))
        {
            entity.Location = request.Location;
        }
        if (request.EventType.HasValue)
        {
            entity.EventAttributes = request.EventType.Value;
        }
        if(request.LocationAttributes != null && request.LocationAttributes.Any())
        {
            entity.LocationAttributes.Clear();
            foreach (var locationAttribute in request.LocationAttributes)
            {
                entity.LocationAttributes.Add(locationAttribute);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new WHEventModel(entity);
    }
}