using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Core.Requests.Transections
{
    public class GetTransactionRequest : Request
    {
        public long Id { get; set; }
    }
}