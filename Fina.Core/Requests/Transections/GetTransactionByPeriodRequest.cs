namespace Fina.Core.Requests.Transections
{
    public class GetTransactionsByPeriodRequest : PagedRequest
    {
        public DateTime? StarDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}