using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskr.Dtos
{
    public class ProjectResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}