using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskr.Dtos
{
    public class UserResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}