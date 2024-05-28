using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fina.Core.Enums;
using Fina.Core.Models;

namespace Fina.Core.Requests.Transections
{
    public class CreateTransactionRequest
    {        
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidOrReceiveAt { get; set; }
        public TransactionTypeEnum Type { get; set; } = TransactionTypeEnum.Withdraw;
        public decimal  Amount { get; set; }
        public Category Category { get; set; }
        public string CategoryId { get; set; } = null!;
        public string UserId { get; set; }  = string.Empty;    
    }
}