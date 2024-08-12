using System.Text.Json;
using Credo.Application.Modules.LoanApplication.Commands;
using Credo.Application.Modules.LoanApplication.Models;
using Credo.Domain.Entities;
using Credo.Domain.RepositoriesContracts;
using Credo.Domain.Services;
using Credo.Domain.ValueObjects;
using MediatR;

namespace Credo.Application.Modules.LoanApplication.Handlers;

public class CreateLoanApplicationCommandHandler : LoanApplicationBaseRequestHandler,
    IRequestHandler<CreateLoanApplicationCommand>
{
    private readonly IOutboxRepository _outboxRepository;

    public CreateLoanApplicationCommandHandler(
        LoanApplicationsService loanApplicationsService,
        IOutboxRepository outboxRepository) : base(loanApplicationsService)
    {
        _outboxRepository = outboxRepository;
    }

    public async Task Handle(CreateLoanApplicationCommand request, CancellationToken cancellationToken)
    {
        var loanApplication = new Domain.Entities.LoanApplication
        {
            UserId = request.UserId,
            Currency = request.Currency,
            Amount = request.Amount,
            Status = LoanStatus.InProgress,
            LoanType = request.LoanType,
            Period = request.Period,
            User = null
        };

        //TODO: fix that have gaps!
        var loanApplicationId = _loanApplicationsService.GetNextId();
        await _loanApplicationsService.CreateLoanApplicationAsync(loanApplication);

        var outboxMessage = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            OccurredOn = DateTime.UtcNow,
            Type = typeof(LoanApplicationCreated).AssemblyQualifiedName!,
            Data = JsonSerializer.Serialize(new LoanApplicationCreated
            {
                Currency = request.Currency,
                UserId = request.UserId,
                Status = request.Status,
                LoanType = request.LoanType,
                Period = request.Period,
                Amount = request.Amount,
                Id = loanApplicationId
            }),
        };

        _outboxRepository.Add(outboxMessage);
    }
}
