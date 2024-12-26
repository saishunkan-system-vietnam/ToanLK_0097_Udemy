using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.ResponseObject
{
    public static class StatusCode
    {
        public const int OK = 200;
        public const int UNAUTHOR = 401;
        public const int INTERVAL_ERROR = 500;
        public const int BAD_REQUEST = 400;
        public const int CONFLICT = 409;
    }
    public static class ErrorName
    {
        public const string TOKEN_EXPIRED = "TokenExpired";
        public const string SERVER_ERROR = "ServerError";
        public const string UNAUTHOR = "Unauthorized";
        public const string INVALID_TOKEN = "InvalidToken";
        public const string INVALID_REFRESH_TOKEN = "InvalidRefreshToken";
        public const string FORBIDDEN = "Forbidden";
        public const string CONFLICT = "Conflict";
    }

}
