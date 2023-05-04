using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2v.core.clinetsync.common.Classes.Results
{
    public static class ClientResultStatus
    {
        public const string Accepted = "Accepted";
        public const string Success = "Success";
        public const string Created = "Created";
        public const string Updated = "Updated";
        public const string Deleted = "Deleted";
        public const string NotFound = "NotFound";
        public const string ServiceUnavailable = "ServiceUnavailable";
        public const string ValidationError = "ValidationError";
        public const string UnexpectedError = "UnexpectedError";
        public const string Conflict = "Conflict";
        public const string Forbidden = "Forbidden";
        public const string Unauthorized = "Unauthorized";
    }
}
