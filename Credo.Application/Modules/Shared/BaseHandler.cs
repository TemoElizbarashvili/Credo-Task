using Credo.Infrastructure.UnitOfWork;

namespace Credo.Application.Modules.Shared;

public abstract class BaseHandler
{
    protected readonly IUnitOfWork _unitOfWork;

    protected BaseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}