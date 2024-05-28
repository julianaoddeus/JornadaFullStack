using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Core.Requests.Transections
{
    public class GetTransactionByPeriodRequest : PagedRequest
    {
        public DateTime? StarDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}