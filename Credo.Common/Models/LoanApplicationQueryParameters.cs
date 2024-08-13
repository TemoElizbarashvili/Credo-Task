namespace Credo.Common.Models;

public class LoanApplicationQueryParameters
{
    public int PageNumber { get; set; }
    public int PageSize {get; set;}
    public int? UserId {get; set;} = null;
    public string? Currency { get; set; } = null;
    public string? LoanType {get; set;} = null;
    public string? LoanStatus {get; set;} = null;
    public decimal? MinAmount {get; set;} = null;
    public decimal? MaxAmount {get; set;} = null;
}
