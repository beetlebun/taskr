using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace taskr.Dtos
{
    public class CollaboratorRequest
    {
        [Required]
        [MinLength(3, ErrorMessage = "Nome de colaborador precisa ter ao mínimo 3 caracteres")]
        [MaxLength(100, ErrorMessage = "Nome de colaborador não pode ter mais que 100 caracteres")]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}