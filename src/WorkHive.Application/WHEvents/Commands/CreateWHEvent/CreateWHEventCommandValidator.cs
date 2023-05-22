using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Common.Interfaces;

namespace WorkHive.Application.WHEvents.Commands.CreateWHEvent;
public class CreateWHEventCommandValidator : AbstractValidator<CreateWHEventCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateWHEventCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");

        RuleFor(v => v.StartDate)
            .NotNull().WithMessage("StartDate is required.");

        RuleFor(v => v.EndDate)
            .NotNull().WithMessage("EndDate is required.")
            .GreaterThan(v => v.StartDate).WithMessage("EndDate should be greater than StartDate");

        RuleFor(v => v.OrganizerId)
            .NotNull().NotEmpty().WithMessage("OrganizerId is required.")
            .MustAsync(UserMustExists).WithMessage("OrganizerId must be an existing user");
    }

    public async Task<bool> UserMustExists(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.WhUsers
            .AnyAsync(u => u.Id == userId, cancellationToken);
    }

}