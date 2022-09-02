using System.Timers;
using System.Threading;

namespace MyBlog.Thread_TEST
{

    public class program
    {
        public static TimeSpan waitTime = new TimeSpan(0, 0, 1);

        public static void Main()
        {
            Thread thread = new Thread(Work);
            thread.Start();


            if (thread.Join(waitTime + waitTime))
            {
                Console.WriteLine("New thread terminated");

            }
            else
            {
                Console.WriteLine("Join Timed out");

            }
            Thread.Sleep(1000);
        }

        private static void Work(object? obj)
        {
            Console.WriteLine("12");
            Thread.Sleep(0);
            Console.WriteLine("23");
            Thread.Sleep(waitTime*3);

            Console.WriteLine("are you ok?");

        }

    }
   
}