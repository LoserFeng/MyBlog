using MyBlog.Model;
using SqlSugar;
using System.Text.RegularExpressions;
using Utility.MarkDown;

namespace TEST.TEST
{

   public class Test
    {

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Test");


            String videoPath = "E:\\develop\\VS_Workspace\\MyBlog\\TEST.TEST\\Resource\\tmp\\test.mp4";
            String tmpPath = "E:\\develop\\VS_Workspace\\MyBlog\\TEST.TEST\\Resource\\tmp";

            //String blogPath = "C:\\Users\\feng_\\Desktop\\课件\\多媒体\\大作业\\MyBlog\\TEST.TEST\\Resource\\blog";

            BlogNews blogNews = new BlogNews
            {
                Id = 1,

                Tags = null,
                Title="test"



            };



            Video2MD processor = new Video2MD(videoPath,tmpPath,blogNews);


            await processor.CoverVideo2MD();


            Console.WriteLine("cover End");











        }
    }


}