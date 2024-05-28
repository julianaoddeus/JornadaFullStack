using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Fina.Core.Requests.Transections;
using Fina.Core.Responses;

namespace Fina.Core.Handlers
{
    public interface ITransactionHandler
    {
        Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
        Task<Response<Transaction?>> UpdateAsync(CreateTransactionRequest request);
        Task<Response<Transaction?>> DeleteAsync(CreateTransactionRequest request);
        Task<Response<Transaction?>> GetByIdAsync(CreateTransactionRequest request);
        Task<PagedResponse<List<Transaction>?>> GetAllAsync(CreateTransactionRequest request);
      
    }
}