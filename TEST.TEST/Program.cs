using MyBlog.Model;

namespace TEST.TEST
{
    internal class Program
    {
        static void Main(string[] args)
        {


            DateTime date = new DateTime();


            int Length = 3;
            int[] arr = new int[5] {1,2,3,4,5};

            Console.WriteLine("Hello, World!");
            Console.WriteLine(Guid.NewGuid());
            int TagId = 0;

            TagInfo tagInfo = new TagInfo();
            List<TagInfo> tags = new List<TagInfo>()
            {
                new TagInfo
                {
                    Name="Hello",
                    Id=1
                },
                new TagInfo {

                    Name="Where",
                    Id=2
                }

            };
            if (tags.Exists(tag => tag.Id == 1))
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine("false");

            }
        }
    }



}