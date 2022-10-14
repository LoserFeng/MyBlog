using MyBlog.Model;
using System.Text.RegularExpressions;

namespace TEST.TEST
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //"^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$"。
            String s = "http://123.234.435.345/asd/asd/a=1&b=2";
            /* String regular= @"^(http://)([\w-]+\.)+[\w-]+(/[\w-./%?=&]*)?$";*/
            String regular = @"^(http://)([\w-]+\.)+[\w-]+(/[\w-./%?=&]*)?$";

            var res =getString(s, regular);
            Console.WriteLine(res);
        }


        public static string getString(string str,string regular)
        {
            var res = Regex.Match(str, regular);
            
            return res.Groups[3].ToString();
        }
    }



}