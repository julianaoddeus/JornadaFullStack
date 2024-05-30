using Fina.Api.Common.API;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint :IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/{id}", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria")
            .WithOrder(2)            
            .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, UpdateCategoryRequest request , long id)
        {            
               request.UserId =ApiConfiguration.UserId;   
               request.Id = id;
            
            var response = await handler.UpdateAsync(request);
            return response.IsSuccess
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
        }
    }
}