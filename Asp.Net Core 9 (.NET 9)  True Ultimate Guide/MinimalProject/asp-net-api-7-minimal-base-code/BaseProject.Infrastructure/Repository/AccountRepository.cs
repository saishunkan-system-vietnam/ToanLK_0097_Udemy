using BaseProject.Shared;
using BaseProject.Shared.Entity;

namespace BaseProject.Infrastructure
{
    public class AccountRepository : GenericRepository<Account>,IAccountRepository
    {
        public AccountRepository(DBMasterContext dbmasterContext, DBSlaveContext dbSlaveContext) : base (dbmasterContext, dbSlaveContext)
        {

        }
        
        public Account? Login(string email,string password)
        {
            Account? findedAccount = dbSlaveContext.Account.Where(x => x.Email == email).FirstOrDefault();

            if(findedAccount != null && findedAccount.Password.VerifyPassword(password))
            {
                return findedAccount;
            }
            return null;
        }
    }
}
