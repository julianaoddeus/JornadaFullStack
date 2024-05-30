using Fina.Api.Data;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description
            };

            try
            {
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                
                return new Response<Category?>(category, 201);
            }
            catch(Exception ex)
            {
                return new Response<Category?>(null, 500, "erro:c001 [Não foi possível criar a categoria]");
            }
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
           try
           {
                var category = await context.Categories.FirstOrDefaultAsync(f => f.Id == request.Id && f.UserId == request.UserId);
                
                if (category is null)
                    return new Response<Category?>();

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return new Response<Category?>(category, message:"Categoria excluída com sucesso!");
           }
           catch (Exception ex)
           {
                return new Response<Category?>(null, 500, "erro:c002 [Não foi possível excluir a categoria]");
           }
        }

        public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoryRequest request)
        {
           try
           {
                var query = context.Categories
                .AsNoTracking()
                .Where(w => w.UserId == request.UserId)
                .OrderBy(o => o.Title);

                var categories = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Category>?>(
                    categories,
                    count,
                    request.PageNumber,
                    request.PageSize
                );
           }
           catch 
           {            
                return new PagedResponse<List<Category>?>(null, 500, "erro: c003 [Não foi possível consultar as categorias]");
           }
        }

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
           try
           {
                var category = await context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(f => f.Id == request.Id && f.UserId == request.UserId);
                
                return category is null
                    ? new Response<Category?>(null, 404, message:"[Categoria não encontrada]")
                    : new PagedResponse<Category?>(category);
                
                
           }
           catch (System.Exception)
           {
                return new Response<Category?>(null, 500, message:"erro: c004 [Não foi possível recuperar a categoria]");            
           }
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
               var category = await context.Categories
                    .FirstOrDefaultAsync(f => f.Id == request.Id && f.UserId == request.UserId);
               
               if(category is null)
                    return new Response<Category?>(null, 404 , message:"[Categoria não encontrada]");

                category.Title = request.Title;
                category.Description = request.Description;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                 return new Response<Category?>(category, message: "[Categoria atualizada com sucesso]");
               
            }
            catch 
            {
                 return new Response<Category?>(null, 500, message: "erro:c005 [Não foi possível alterar a categoria]");
            }
        }
    }
}