using System.Transactions;
using Fina.Api.Data;
using Fina.Core.Handlers;
using Fina.Core.Requests.Transections;
using Fina.Core.Responses;

namespace Fina.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Transaction?>> DeleteAsync(CreateTransactionRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResponse<List<Transaction>?>> GetAllAsync(CreateTransactionRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Transaction?>> GetByIdAsync(CreateTransactionRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<Transaction?>> UpdateAsync(CreateTransactionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}