using BaseProject.Shared.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Core.Service
{
    public interface IAccountService : IGenericService<Account>
    {
        Account? Login(string email, string password);
        Account? SignUp(Account newAccount);
        Account? GetByEmail(string email);
        Account? GetMe(HttpContext context);
    }
}
