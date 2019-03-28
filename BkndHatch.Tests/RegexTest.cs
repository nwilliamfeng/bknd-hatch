using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace BkndHatch.Tests
{

    public class CSVItem
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
    public class RegexTest
    {
        public void MatchAny()
        {
            var sample = new StringBuilder();
            sample.AppendLine("sh.xls");
            sample.AppendLine("ah.xls");
            sample.AppendLine("cxh3.xls");
            sample.AppendLine("ch2.xml");
            sample.AppendLine("dh23.xls");
            sample.AppendLine("rdh23.doc");
            sample.AppendLine("rhn.xls");
            sample.AppendLine("wx3.doc");
            new Regex(@".h.\.xls").Matches(sample.ToString()).OfType<Match>().ToList().ForEach(match=>
            {
                Console.WriteLine(match);
            });
            
        }

        public void MatchSpecify()
        {
            var sample = new StringBuilder();
            sample.AppendLine("sh.xls");
            sample.AppendLine("ah.xls");
            sample.AppendLine("cxh3.xls");
            sample.AppendLine("ddh2.xml");
            sample.AppendLine("dch2.xml");
            sample.AppendLine("dh23.xls");
            sample.AppendLine("rdh23.doc");
            sample.AppendLine("rn.xls");
            sample.AppendLine("wx3.doc");
            new Regex(@"d[a-z]h.\.xml").Matches(sample.ToString()).OfType<Match>().ToList().ForEach(match =>
            {
                Console.WriteLine(match);
            });
        }

        public void MatchPrecise()
        {
            var content = "the phrase regular expression is often abbreviated as RegEx or regex";
            new Regex("[Rr]eg[Ee].").Dump(content);
        }

        public void MatchDate()
        {
            var content = "the phrase regular expression is often abbreviated as $2018-12-23 12:38:49abcd";
            new Regex(@"[1-2][0-9][0-9][0-9]\-[0-1][0-9]\-[0-3][0-9] [0-9][0-9]\:[0-9][0-9]\:[0-9][0-9]").Dump(content);
        }

        public void MatchExclude()
        {
            var sample = new StringBuilder();
            sample.AppendLine("sh.xls");
            sample.AppendLine("ah.xls");
            sample.AppendLine("cxh3.xls");
            sample.AppendLine("ddh2.xml");
            sample.AppendLine("dch2.xml");
            sample.AppendLine("dh23.xls");
            sample.AppendLine("rdh23.doc");
            sample.AppendLine("rn.xls");
            sample.AppendLine("wx3.doc");
            new Regex(@"[^a][h]\.xls").Dump(sample.ToString());
        }

        public void ReplaceBackSlant()
        {
            var str = @"www.baidu.com\home\account\userInfo";
            Console.WriteLine(new Regex(@"\\").Replace(str, " "));
        }

        public void RemoveEnter()
        {
            var str = "abcde" + (char)10 + "1234";
            Console.WriteLine($"Ready to remove enter character: {str}");
           var result =new Regex(@"\n").Replace(str,"");
            Console.WriteLine($"After op: "+result);
        }

        public void ValidateCSV()
        {
            Func<string, string> set = x =>
             {
                 return '"' + x + '"';
             };
            var sample = new StringBuilder();
            sample.AppendLine($"{set("101")} {set("Ben")} {set("Forta")}");
            sample.AppendLine($"{set("102")} {set("Tom")} {set("Cruise")}");
            sample.AppendLine();
            sample.AppendLine($"{set("103")} {set("Alan")} {set("Bobson")}");

            Console.WriteLine("the csv file content:");
            Console.WriteLine(sample.ToString());
            Console.WriteLine("after validate:");
            var result = new Regex(@"\n\r").Replace(sample.ToString(),"");          
            Console.WriteLine(result);

        }

        public void ReplaceOneWhiteSpace()
        {
            var str = "a b c d  and  e f g";
            Console.WriteLine( new Regex(@"[\s^ ]").Replace(str,""));
        }

        public void MatchEmail()
        {
            var str = "send a email to abc@forta.com from address: .nwills@yahoo.cn,after sent please sent copy to me ,my email address is : benson@189.com.cn.,dfsceew";
              new Regex(@"\w+@[\w+\.]+\.\w+").Dump(str);
        }


    }
}
