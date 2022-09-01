using System.Linq.Expressions;


namespace MyBlog.TEST.Expression
{
    public class Expr
    {
        public static void Exp_test()
        {

            Func<string> func = () => { return "委托"; };
            Expression<Func<String>> func_exp=()=>"NB";
            Console.WriteLine(func_exp.Compile());



            Console.Read();

        }

    }
}