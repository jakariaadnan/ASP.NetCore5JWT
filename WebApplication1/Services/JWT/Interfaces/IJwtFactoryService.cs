using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services.JWT.Interfaces
{
    public interface IJwtFactoryService
    {    
        Task<String> GenerateToken(string userName, IList<string> roles);
    }
}
