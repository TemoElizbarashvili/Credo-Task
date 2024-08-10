using Credo.Application.Modules.User.Queries;
using Credo.Domain.RepositoriesContracts;
using Credo.Infrastructure.Models;
using MediatR;

namespace Credo.Application.Modules.User.Handlers;

public class UsersPagedListQueryHandler : UserRequestsHandlerBase, IRequestHandler<UsersPagedListQuery, PagedList<Domain.Entities.User>>
{
    public UsersPagedListQueryHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task<PagedList<Domain.Entities.User>> Handle(UsersPagedListQuery request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.UsersPagedListAsync(request.PageNumber, request.PageSize, request.ShowDrafts);

        return result;
    }
}
