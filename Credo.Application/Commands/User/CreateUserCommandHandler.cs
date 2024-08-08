using Credo.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;

namespace Credo.Application.Commands.User;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.Users.AddAsync(request.User);
            await _unitOfWork.CompleteAsync();
        }
        catch(Exception ex)
        {
            Log.Error($"Exception Happend while creating User. Message: {ex.Message}");
        }

        return request.User.Id;
    }
}
