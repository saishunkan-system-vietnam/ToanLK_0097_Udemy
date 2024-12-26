using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Core.Mail
{
    public interface IMailSender
    {
        bool SendEmail(Message message, bool isHtml);
    }
}
