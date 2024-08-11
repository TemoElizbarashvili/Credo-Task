using MediatR;

namespace Credo.Application.Modules.LoanApplication.Queries;

public class GetApplicationByIdQuery : IRequest<Domain.Entities.LoanApplication?>
{
    public int Id { get; set; }
    public bool WithUser { get; set; }
}
