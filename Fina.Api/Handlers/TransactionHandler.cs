
using Fina.Api.Data;
using Fina.Core.Common;
using Fina.Core.Enums;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transections;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            if(request is { Type: TransactionTypeEnum.Withdraw, Amount: >=0 })
                request.Amount *= -1;

            var transaction = new Transaction
            {
                Title = request.Title,
                CreatedAt = DateTime.Now,
                PaidOrReceivedAt = request.PaidOrReceivedAt,
                Type = request.Type,
                Amount = request.Amount,
                UserId = request.UserId,
                CategoryId = request.CategoryId
            };

             try
            {
                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();
                
                return new Response<Transaction?>(transaction, 201);
            }
            catch(Exception ex)
            {
                return new Response<Transaction?>(null, 500, "erro: t001 [Não foi possível criar uma transação]");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {

            try
            {
                var transaction = await context.Transactions                
                    .FirstOrDefaultAsync(f => f.Id == request.Id && f.UserId == request.UserId);

                if(transaction is null)
                    return new Response<Transaction?>(null, 404, message:"Transação não encontrada.");
                
                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();
                return new Response<Transaction?>(transaction, message:"Transação excluída com sucesso.");
            }
            catch (System.Exception)
            {
                 return new Response<Transaction?>(null, 500, message:"erro: t002 [Não foi possível excluir a transação]");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(f => f.Id == request.Id && f.UserId != request.UserId);

                return transaction is null
                    ? new Response<Transaction?>(null, 404, message:"Transação não localizada.")
                    : new Response<Transaction?>(transaction);
            }
            catch 
            {
                return new Response<Transaction?>(null, 500, message:"erro: t003 [Não foi possível localizar sua transação]");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StarDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch 
            {
                return new PagedResponse<List<Transaction>?>(null, 500, message:"erro: t004 [Não foi possível determinar a data de início ou término]");
            }

            try
            {
                var query = context.Transactions
                    .AsNoTracking()
                    .Where(w => w.PaidOrReceivedAt >= request.StarDate &&
                        w.PaidOrReceivedAt <= request.EndDate &&
                        w.UserId == request.UserId)
                    .OrderBy(o => o.PaidOrReceivedAt);
                
                var transaction = await query
                    .Skip((request.PageNumber -1)* request.PageSize) 
                    .Take(request.PageSize)
                    .ToListAsync();
                
                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(
                    transaction, 
                    count,
                    request.PageNumber,
                    request.PageSize
                );

            }
            catch (System.Exception)
            {
               return new PagedResponse<List<Transaction>?>(null, 500, "erro: t004 [Não foi possível obter as transações]");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {           
            if (request is { Type: TransactionTypeEnum.Withdraw, Amount: >= 0 }) 
                request.Amount *= -1;
           try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(f => f.Id == request.Id && f.UserId != request.UserId);

                if(transaction is null)
                    return new Response<Transaction?>(null, 404, message: "Transação não localizada.");
                
                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.Title = request.Title;
                transaction.Type = request.Type;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();
                
                return new Response<Transaction?>(transaction);
            }
            catch (System.Exception)
            {
                return new Response<Transaction?>(null, 500, message:"erro: t005 [Não foi possível atualizar sua transação]");
            }
        }
    }
}