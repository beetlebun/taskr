using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace taskr.Dtos
{
    public class TaskRequest
    {
        [Required]
        [MinLength(4, ErrorMessage = "Nome da tarefa precisa ter ao mínimo 4 caracteres")]
        [MaxLength(100, ErrorMessage = "Nome da tarefa não pode ter mais que 100 caracteres")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(400, ErrorMessage = "Descrição da tarefa não pode ter mais que 400 caracteres")]
        public string Description { get; set; } = string.Empty;
        [Required]
        public Guid ProjectId { get; set; }
    }
}