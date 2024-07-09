using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskr.Dtos
{
    public class CollaboratorResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}