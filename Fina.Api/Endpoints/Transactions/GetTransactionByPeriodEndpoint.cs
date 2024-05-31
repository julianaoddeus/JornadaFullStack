using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fina.Api.Common.API;
using Fina.Core;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transections;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Transactions
{
    public class GetTransactionByPeriodEndpoint : IEndpoint
    {
         public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
            .WithName("Transactions: Get All")
            .WithSummary("Recupera todas as transações")
            .WithOrder(4)            
            .Produces<PagedResponse<List<Transaction>?>>();

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, 
            [FromQuery] DateTime? startDate = null, 
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {            
            var request =  new GetTransactionsByPeriodRequest
            {
                UserId =ApiConfiguration.UserId,   
                StarDate = startDate,
                EndDate = endDate,
                PageNumber = pageNumber,
                PageSize = pageSize          
            };
            
            var result = await handler.GetByPeriodAsync(request);
            return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(request);
        }
    }
}