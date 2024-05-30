using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fina.Core.Enums;

namespace Fina.Core.Models
{
    public class Transaction
    {
        public long Id { get; set; } 
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidOrReceivedAt  { get; set; }
        public TransactionTypeEnum Type { get; set; } = TransactionTypeEnum.Withdraw;
        public decimal  Amount { get; set; }
        public Category Category { get; set; }
        public string CategoryId { get; set; } = null!;
        public string UserId { get; set; }  = string.Empty;    
    }
}