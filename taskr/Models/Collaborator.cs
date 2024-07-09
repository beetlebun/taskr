using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace taskr.Models
{
    [Table("Collaborators")]
    public class Collaborator
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
    }
}