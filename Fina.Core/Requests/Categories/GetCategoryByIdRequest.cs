using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Core.Requests.Categories
{
    public class GetCategoryByIdRequest : Request
    {
        [Required(ErrorMessage ="Título inválido")]
        [MaxLength(80, ErrorMessage ="o título deve conter até 80 caracteres")]
        public string Title { get; set; } = string.Empty;
        
        [Required(ErrorMessage ="Descrição inválido")]
        public string? Description { get; set; }         
    }
}