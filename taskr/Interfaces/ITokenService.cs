using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskr.Models;

namespace taskr.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}