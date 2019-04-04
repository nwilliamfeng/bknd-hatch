using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BkndHatch.Tests
{
    public static class RegexExtensions
    {

        public static void Dump(this Regex regex, string input)
        {
            regex.Matches(input).OfType<Match>().ToList().ForEach(match =>
            {
                Console.WriteLine(match);
            });
        }

        public static string ReadFile(this string fileName)
        {
           return System.IO.File.ReadAllText($"Resource\\Text\\{fileName}");
        }
    }
}
