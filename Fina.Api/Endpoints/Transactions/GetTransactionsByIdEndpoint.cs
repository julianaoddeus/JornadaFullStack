using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fina.Api.Common.API;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transections;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class GetTransactionsByIdEndpoint : IEndpoint
    {
       public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
            .WithName("Transactions: Get by Id")
            .WithSummary("Recupera uma transação por id")
            .WithOrder(5)            
            .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, long id)
        {
            var request =  new GetTransactionByIdRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id,
            };
            var result  = await handler.GetByIdAsync(request);
            
            return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
        } 
    }
}