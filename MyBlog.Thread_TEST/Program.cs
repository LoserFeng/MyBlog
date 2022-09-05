using System.Timers;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Data.SqlTypes;
using System.Threading.Tasks;

namespace MyBlog.Thread_TEST
{

    public class program
    {


        #region 案例1
        /* public static TimeSpan waitTime = new TimeSpan(0, 0, 1);

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

         }*/

        #endregion


        #region lambda的变量也会共享线程
        /*      static bool _done = false;
              public static void Main()
              {
                  ThreadStart thread = () =>
                  {
                      if (!_done)
                      {
                          _done = true;
                          Console.WriteLine("Done");
                      }

                  };

                  new Thread(thread).Start();
                  thread();



              }*/


        #endregion



        //static bool _done;
        #region lock
        /*        static readonly object _locker=new object();    


                static void Main()
                {

                    new Thread(thr).Start();


                    thr();

                }



                static void thr()
                {
                                lock (_locker)
                    {
                        if (!_done)
                        {
                            Console.WriteLine("Done");
                            _done = true;
                        }
                        else
                        {
                            Console.WriteLine("has been Done");
                        }
        }
                }






        */

        #endregion



        #region 线程安全

        /*        static void Main()
                {
                    for(int i = 0; i < 10; i++)
                    {
                        int temp = i;
                        new Thread(() => Console.Write(temp)).Start();
                    }
                }*/
        #endregion




        #region signal1
        /*
                static void Main()
                {
                    var signal = new ManualResetEvent(false);

                    new Thread(() =>
                    {
                        Console.WriteLine("in the thread");
                        signal.WaitOne();
                        signal.Dispose();
                        Console.WriteLine("the thread end");


                    }).Start();


                    Thread.Sleep(3000);
                    signal.Set();


                }*/



        #endregion




        #region task

        /*        static void Main(String[]args)
                {
                    Task.Run(() => Console.Write("task!!!!!"));
                    Thread.Sleep(500);

                }
        */



        #endregion




        #region task2


        /*        static void Main(String[] args)
                {
                    Task task = Task.Run(() =>
                    {
                        Thread.Sleep(3000);
                        Console.WriteLine("Foo");
                    });
                    Console.WriteLine(task.IsCompleted);


                    task.Wait();

                    Console.WriteLine(task.IsCompleted);

                }
        */

        #endregion



        #region task.IsComleted
        /*        static void Main(String[] args)
                {
                    Task task = Task.Run(() =>
                    {
                        Thread.Sleep(3000);
                        Console.WriteLine("Foo");
                    });
                    Console.WriteLine(task.IsCompleted);


                    task.Wait();

                    Console.WriteLine(task.IsCompleted);

                }*/

        #endregion




        #region task<int>
        /*
                static void Main(String[] args)
                {
                    Task<int> task = Task.Run(
                        () =>
                        {
                            String input = "";
                            input=Console.ReadLine();
                            Console.WriteLine(input);
                            return 0;
                        });
                    int result = task.Result;
                    Console.WriteLine(result);

                }
        */

        #endregion




        #region task<int> 2





        /*        static void Main(String[] args)
                {
                    Task<int> task = Task<int>.Run(
                        () =>
                            Enumerable.Range(2, 3000000).Count(n =>
                                Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i != 0))


                        );



                    int result = task.Result;
                    Console.WriteLine(result);

                }
        */
        #endregion




        #region task Exception
        /*
                static void Main(string[] args)
                {
                    Task task = Task.Run(() => { throw null; });


                    try
                    {

                        task.Wait();
                    }
                    catch(AggregateException aex)
                    {
                        if(aex.InnerException is NullReferenceException)
                        {
                            Console.WriteLine("Null");

                        }
                        else
                        {
                            throw;
                        }
                    }

                }
        */



        #endregion







        #region Task continuation



        /// <summary>
        /// ·在task上调用GetAwaiter会返回一个awaiter对象
        ///·它的OnCompleted方法会告诉之前的task:“当你结束/发生故障的时候要执行委托”。
        ///·可以将Continuation附加到已经结束的task上面，此时Continuation将会被安排
        ///立即执行。
        /// </summary>
        /// <param name="args"></param>
        static void Main(String[] args)
        {
            Task<int> primeNumberTask = Task<int>.Run(
                () =>
                    Enumerable.Range(2, 3000000).Count(n =>
                        Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i != 0))


                );



            var awaiter = primeNumberTask.GetAwaiter();




            awaiter.OnCompleted(() =>
            {
                int result = awaiter.GetResult();
                Console.WriteLine(result);
            });


            Console.ReadKey();



        }






        #endregion


    }








}