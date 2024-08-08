﻿using Credo.Domain.Entities;
using Credo.Domain.Repositories;
using Credo.Domain.ValueObjects;

namespace Credo.Domain.Aggregates;

public class LoanApplicationAggregate
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;

    public LoanApplicationAggregate(ILoanApplicationRepository loanApplicationRepository)
    {
        _loanApplicationRepository = loanApplicationRepository;
    }

    public async Task SubmitLoanApplicationAsync(LoanApplication loanApplication)
        => await _loanApplicationRepository.AddAsync(loanApplication);

    public async Task UpdateLoanApplicationStatusAsync(int loanApplicationId, LoanStatus newStatus)
    {
        var loanApplication = await _loanApplicationRepository.GetByIdAsync(loanApplicationId);
        if (loanApplication is not null)
        {
            loanApplication.UpdateStatus(newStatus);
            await _loanApplicationRepository.UpdateAsync(loanApplication);
        }
    }
}
