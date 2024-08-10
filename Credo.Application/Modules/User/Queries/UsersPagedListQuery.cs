using Credo.Infrastructure.Models;
using MediatR;

namespace Credo.Application.Modules.User.Queries;

public class UsersPagedListQuery : IRequest<PagedList<Domain.Entities.User>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public bool ShowDrafts { get; set; } = false;
}
