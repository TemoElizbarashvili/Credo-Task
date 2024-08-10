using Credo.Common.Models;
using MediatR;

namespace Credo.Application.Modules.User.Queries;

public class UsersPagedListQuery : IRequest<PagedList<Domain.Entities.User>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public bool ShowDrafts { get; set; } = false;
    public string? UserName { get; set; } = default!;
    public string? FirstName { get; set; } = default!;
    public string? LastName { get; set; } = default!;
    public string? PersonNumber { get; set; } = default!;
}
