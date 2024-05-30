using Fina.Api.Common.API;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transections;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
            .WithName("Transactions: Create")
            .WithSummary("Cria uma nova transação")
            .WithOrder(1)            
            .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(ITransactionHandler handler, CreateTransactionRequest request)
        {
            request.UserId = ApiConfiguration.UserId;
            var response = await handler.CreateAsync(request);
            return response.IsSuccess
            ? TypedResults.Created($"v1/transactions/{ response.Data?.Id }", response)
            : TypedResults.BadRequest(response);
        }
    }
}