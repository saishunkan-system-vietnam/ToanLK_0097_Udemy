using System;
namespace BaseProject.API.Exceptions
{
    public class HasntVerifyEmail : Exception
    {
        public HasntVerifyEmail()
        {
        }

        public HasntVerifyEmail(string message)
            : base(String.Format("HasntVerifyEmail: {0}",message))
        {
        }
    }
}
