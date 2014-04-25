using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PodSharp
{
    public class PodHelper
    {
        public static bool CheckTextForHtmlEncode(string text)
        {
            Regex reg = new Regex(@"&#?[a-z A-Z 0-9]{2,6};");
            return reg.IsMatch(text);
        }

        public static bool CheckTextForMarkup(string text)
        {
            Regex reg = new Regex(@"</?[a-z A-Z]+(\s.+)?>");
            return reg.IsMatch(text);
        }
    }
}
