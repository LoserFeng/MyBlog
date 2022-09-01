namespace TEST.Interface
{
    public class InterfaceHelper
    {
        public InterfaceHelper()
        {
            Console.WriteLine("{0}被构造",this.GetType().Name);

        }



        public void Query()
        {
            Console.WriteLine("{0}.Query", this.GetType().Name);

        }

    }
}