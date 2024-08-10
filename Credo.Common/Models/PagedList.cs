namespace Credo.Infrastructure.Models;

public class PagedList<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public bool IsLastPage { get; set; }

    public PagedList(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
        IsLastPage = !(totalCount - (pageSize * pageNumber) > 0);
    }
}
