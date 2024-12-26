using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaseProject.Shared.StringHelper
{
    public static class StringExtensions
    {
        // string format for couple of key value
        public static string StringFormat(this string format, IDictionary<string, string> values)
        {
            foreach (var p in values)
                format = format.Replace("{{" + p.Key + "}}", p.Value);
            return format;
        }
    }
}
