using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Shared.ResponseObject
{
    public class Response
    {
        public int? Code { get; set; }
        public string? Message { get; set; }
        public DateTime? Timestamp { get; set; } = DateTime.UtcNow;

    }

    public class R<T> : Response
    {
        public T? Payload { get; set; }
    }

    public class E<T> : Response
    {
        public string? ErrorName { get; set; }
        public string? ErrorURL { get; set; }
        public T? ErrorObject { get; set; }

    }

}
