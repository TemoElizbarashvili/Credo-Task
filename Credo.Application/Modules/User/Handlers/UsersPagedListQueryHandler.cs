using Credo.Application.Modules.User.Queries;
using Credo.Common.Models;
using Credo.Domain.RepositoriesContracts;
using MediatR;

namespace Credo.Application.Modules.User.Handlers;

public class UsersPagedListQueryHandler : UserRequestsHandlerBase, IRequestHandler<UsersPagedListQuery, PagedList<Domain.Entities.User>>
{
    public UsersPagedListQueryHandler(IUserRepository userRepository) : base(userRepository) { }

    public async Task<PagedList<Domain.Entities.User>> Handle(UsersPagedListQuery request, CancellationToken cancellationToken)
    {
        var result = 
            await _userRepository.UsersPagedListAsync(request.PageNumber, request.PageSize, request.UserName, request.FirstName, request.LastName, request.PersonNumber, request.ShowDrafts);

        return result;
    }
}
