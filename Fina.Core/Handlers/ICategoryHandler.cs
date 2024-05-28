using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Core.Handlers
{
    public interface ICategoryHandler
    {
        Task<Response<Category?>> CreateAsync(CreateCategoryRequest request);
        Task<Response<Category?>> UpdateAsync(CreateCategoryRequest request);
        Task<Response<Category?>> DeleteAsync(CreateCategoryRequest request);
        Task<Response<Category?>> GetByIdAsync(CreateCategoryRequest request);
        Task<PagedResponse<List<Category>?>> GetAllAsync(CreateCategoryRequest request);
    }
}