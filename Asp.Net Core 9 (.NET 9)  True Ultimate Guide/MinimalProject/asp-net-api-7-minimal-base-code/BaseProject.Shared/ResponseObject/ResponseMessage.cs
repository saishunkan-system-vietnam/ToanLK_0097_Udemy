using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.ResponseObject
{
    public static class ResponseMessage
    {
        // general
        public const string SERVER_ERROR = "Internal server error occurred while processing your request. Please try again later.";
        public const string RESPONSE_SUCCESS = "Request processed successfully.";
        public const string OUT_OF_APP_WAY = "Your request was not accepted due to the following reason: [Provide a clear and concise explanation of the issue, e.g., 'Potential security violation detected.']";

        // login
        public const string WRONG_ACCOUNT = "Invalid username or password.";
        public const string LOGIN_SUCCESS = "Login successful.";
        public const string ACCOUNT_NOT_VERIFY = "Your account hasn't been verify, check out your email";

        // refresh token
        public const string INVALID_TOKEN = "Invalid token. Please log in again.";
        public const string EXPIRED_TOKEN = "The access token has expired. Please refresh your token.";
        public const string REQUIRED_LOGIN = "Your session has expired. Please log in again to obtain a new access token.";
        public const string REQUIRED_REFRESH_TOKEN = "A valid refresh token is required to access this resource. Please provide a valid refresh token.";


        // registered account
        public const string REGISTER_SUCCESS = "Your account has been successfully registered.";
        public const string INVALID_REFRESH_TOKEN = "Invalid token. Please log in again.";
        public const string EXPIRED_TOKEN_VERIFY = "The verification token has expired. Please obtain a new verification token and try again.";
        public const string ACCOUNT_VERIFY_SUCCESS = "Your account has been successfully verified.";
        public const string ALLREADY_REGISTED = "The provided email address or phone number is already registered. Please use a different email address or phone number.";

    }
}


