using MediatR;
using Credo.Domain.Entities;
using Credo.Application.Modules.LoanApplication.Models;

namespace Credo.Application.Modules.LoanApplication.Queries;

public class GetApplicationsQuery : IRequest<List<LoanApplicationCreated>> { }