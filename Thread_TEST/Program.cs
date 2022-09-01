

static void Go()
{
    for(int i= 0; i < 10000; i++)
    {
        Console.WriteLine(i);
    }
}






Thread t =new Thread(Go);

t.Start();
t.Join();
Console.WriteLine("Thread t has ended!");


