using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace taskr.Dtos
{
    public class ProjectRequest
    {
        [Required]
        [MinLength(3, ErrorMessage = "Nome do projeto precisa ter ao mínimo 3 caracteres")]
        [MaxLength(80, ErrorMessage = "Nome do projeto não pode ter mais que 80 caracteres")]
        public string Name { get; set; } = string.Empty;
    }
}