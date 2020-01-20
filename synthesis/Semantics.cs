using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Utils;


namespace Ex {
    public static class Semantics {
        public static string Substring(string v, int start, int end) => v.Substring(start, end - start);
        public static string Append(string prefix, string suffix) => prefix + suffix;
    }
}
