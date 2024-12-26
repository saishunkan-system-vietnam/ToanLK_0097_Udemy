using AutoMapper;
using BaseProject.Core.Mail;
using BaseProject.Core.Token;
using BaseProject.Infrastructure;
using BaseProject.Shared;
using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Enum;
using BaseProject.Shared.StringHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Security.Claims;

namespace BaseProject.Core.Service
{
    public class AccountService : GenericService<Account>, IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IMailSender _mailSender;
        private readonly IMapper _mapper;
        public AccountService(IUnitOfWork repository, TokenService tokenService, IMailSender mailSender, IMapper mapper, IConfiguration configuration) : base(repository)
        {
            _unitOfWork = repository;
            _tokenService = tokenService;
            _mailSender = mailSender;
            _mapper =  mapper;
            _configuration = configuration;
        }

        public Account? Login(string email, string password)
        {
            return _unitOfWork.AccountRepository.Login(email, password);
        }

        public Account? SignUp(Account newAccount)
        {
            var existedAccount = _unitOfWork.AccountRepository.GetByWhere(x => x.Email == newAccount.Email && x.PhoneNumber == x.PhoneNumber).FirstOrDefault();
            if(existedAccount != null)
            {
                return null;
            }
            Account addedAccount = new Account();
            _mapper.Map(newAccount, addedAccount);
            addedAccount.Id = Guid.NewGuid();
            addedAccount.Password = newAccount.Password!.HashPassword();
            addedAccount.ModifiedDate = DateTime.Now;
            addedAccount.CreateDate = DateTime.Now;
            addedAccount.Role = AccountRole.Customer;
            addedAccount.EmailValidated = false;

            base.Add(addedAccount);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, addedAccount.Email),
                new Claim(ClaimTypes.Role, addedAccount.Role)
            };
            string validateToken = _tokenService.GenerateToken(claims,5);

            var directory = Directory.GetCurrentDirectory();
            string context = File.ReadAllText(directory + "\\..\\BaseProject.Core\\Mail\\MailTemplate\\ValidateEmail.html");

            var clientUrl = _configuration["Client:Origin"];

            context = context.StringFormat(new Dictionary<string, string>() {
                { "Name", addedAccount.Name },// just for test
                { "verifyLink", $"{clientUrl}verify/{validateToken}/"} // fe client
            });

            Message verifyMessage = new Message(new string[] {addedAccount.Email}, EMAIL.VERIFY_SUBJECT, context, EMAIL.VERIFY_SUBJECT);

            _mailSender.SendEmail(verifyMessage, true);

            return addedAccount;
        }

        public Account? GetByEmail(string email)
        {
            return _unitOfWork.AccountRepository.GetByWhere(x => x.Email == email).FirstOrDefault();
        }

        public Account? GetMe(HttpContext context)
        {
            var a = context.Request.Headers["Authorization"];
            a = a.ToString().Replace("Bearer ", "");
            if(a == "")
            {
                return null;
            }
            var principle = _tokenService.GetPrincipalFromExpiredToken(a);
            var userName = principle.Identity.Name;
            var currentUser = GetByEmail(userName);
            return currentUser;
        }

    }
}
