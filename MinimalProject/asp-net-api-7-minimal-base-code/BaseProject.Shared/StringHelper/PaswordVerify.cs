using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared
{
    public static class PaswordVerify
    {
        public static string HashPassword(this string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool VerifyPassword(this string hassPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, hassPassword);
        }
    }
}
