using Credo.Application.Modules.Shared;
using Credo.Application.Modules.User.Commands;
using Credo.Infrastructure.UnitOfWork;
using MediatR;
using Serilog;

namespace Credo.Application.Modules.User.Handlers;

public class CreateUserCommandHandler : BaseHandler, IRequestHandler<CreateUserCommand, int>
{
    public CreateUserCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.UsersService.RegisterUserAsync(request.User);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            Log.Error($"Exception Happened while creating User. Message: {ex.Message}");
        }

        return request.User.Id;
    }
}
