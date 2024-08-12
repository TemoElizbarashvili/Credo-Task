using System.Text.Json;
using Credo.Application.Modules.LoanApplication.Commands;
using Credo.Application.Modules.LoanApplication.Models;
using Credo.Common.Helpers;
using Credo.Domain.Entities;
using Credo.Domain.RepositoriesContracts;
using Credo.Domain.Services;
using Credo.Domain.ValueObjects;
using Credo.Infrastructure.UnitOfWork;
using MediatR;

namespace Credo.Application.Modules.LoanApplication.Handlers;

public class CreateLoanApplicationCommandHandler : LoanApplicationBaseRequestHandler,
    IRequestHandler<CreateLoanApplicationCommand>
{
    private readonly IOutboxRepository _outboxRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLoanApplicationCommandHandler(
        LoanApplicationsService loanApplicationsService,
        IOutboxRepository outboxRepository, IUnitOfWork unitOfWork) : base(loanApplicationsService)
    {
        _outboxRepository = outboxRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateLoanApplicationCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
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


            await _loanApplicationsService.CreateLoanApplicationAsync(loanApplication);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

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
                    Id = loanApplication.Id
                }),
            };
            _outboxRepository.Add(outboxMessage);

            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
