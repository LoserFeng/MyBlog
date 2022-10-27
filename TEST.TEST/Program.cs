using MyBlog.Model;
using SqlSugar;
using System.Text.RegularExpressions;

namespace TEST.TEST
{

    public class Base
    {
        public string name;
        public Base(string name)
        {
            this.name = name;
        }


        public string a()
        {

            return "123";
        }


        public virtual string b()
        {
            return "234";
        }



    }


    public class high : Base
    {
        public high(string name) : base(name)
        {
            this.name = name;
        }


        public new string a()
        {
            b();
            return "2222";
        }


        public override string b()
        {
            return "33333";
        }



    }

    internal class Program
    {
        static void Main(string[] args)
        {

            Base b = new high("234");


            Console.WriteLine(b.b());

            
        }





        public static string getString(string str,string regular)
        {
            var res = Regex.Match(str, regular);
            
            return res.Groups[3].ToString();
        }
    }



}