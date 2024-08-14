using System.Text.Json;
using Credo.Application.Modules.LoanApplication.Commands;
using Credo.Common.Models;
using Credo.Domain.Entities;
using Credo.Domain.RepositoriesContracts;
using Credo.Domain.Services;
using Credo.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Credo.Application.Modules.LoanApplication.Handlers;

public class CreateLoanApplicationCommandHandler : LoanApplicationBaseRequestHandler,
    IRequestHandler<CreateLoanApplicationCommand>
{
    private readonly IOutboxRepository _outboxRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLoanApplicationCommandHandler(
        LoanApplicationsService loanApplicationsService,
        IOutboxRepository outboxRepository,
        IUnitOfWork unitOfWork,
        ILogger<LoanApplicationBaseRequestHandler> logger) : base(loanApplicationsService, logger)
    {
        _outboxRepository = outboxRepository;
        _unitOfWork = unitOfWork;
    }

    public Task Handle(CreateLoanApplicationCommand request, CancellationToken cancellationToken)
    {
        try
        {
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
                }),
            };
            _outboxRepository.Add(outboxMessage);
        }
        catch (Exception ex)
        {
            Logger.LogError("Error while adding outbox message in database, in CreateLoanApplicationCommand, {@Exception}", ex);
            throw;
        }

        return Task.CompletedTask;
    }
}
