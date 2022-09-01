using System;
using System.Reflection;

namespace Program
{

    public class TEST
    {
        static void Main(string[] args)
        {
            try
            {
                #region Reflection1
/*

                Console.WriteLine("-------------------------------------Reflection----------------------------------");
                Assembly assembly = Assembly.Load("TEST.Interface");   //加载方式一:dll文件名


                foreach(var type in assembly.GetTypes())
                {
                    Console.WriteLine(type.FullName);
                    foreach(var method in type.GetMethods())
                    {
                        Console.WriteLine("这是"+method.Name+"方法");
                    }
                }

*/

                #endregion
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadKey();
        }
    }
}