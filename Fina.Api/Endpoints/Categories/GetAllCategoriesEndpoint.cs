using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fina.Api.Common.API;
using Fina.Core;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint: IEndpoint
    {
         public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
            .WithName("Categories: Get All")
            .WithSummary("Recupera todas as categoria")
            .WithOrder(4)            
            .Produces<PagedResponse<List<Category>?>>();

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, 
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber, 
            [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {            
            var request =  new GetAllCategoryRequest
            {
                UserId =ApiConfiguration.UserId,   
                PageNumber = pageNumber,
                PageSize = pageSize             
            };
            
            var result = await handler.GetAllAsync(request);
            return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(request);
        }
    }
}