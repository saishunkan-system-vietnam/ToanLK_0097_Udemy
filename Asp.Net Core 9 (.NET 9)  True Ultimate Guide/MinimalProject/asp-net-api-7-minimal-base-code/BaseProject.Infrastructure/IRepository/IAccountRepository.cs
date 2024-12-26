
using BaseProject.Shared.Entity;

namespace BaseProject.Infrastructure
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        public Account? Login(string email, string password);
    }
}
