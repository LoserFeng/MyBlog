using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Globalization;
using System.Threading;
using Whisper.net.Ggml;
using Whisper.net;
using Utility.MarkDown.Model;
using FFMpegCore;
using OpenCvSharp;
using Newtonsoft.Json;
using System.Drawing.Printing;
using Org.BouncyCastle.Math.EC.Multiplier;
using SqlSugar;
using Utility.Language;

namespace Utility.MarkDown
{
    public class Video2MD
    {



        private readonly String _videoPath;


        private readonly BlogNews _blog;
        private readonly String blogId;
        private readonly string _blogPath;
        private readonly string _tmpPath;
        private readonly string _assetsPath;




        public Video2MD(String videoPath,String tmpPath,BlogNews blogNews) {
        
            _videoPath = videoPath;
            _blog = blogNews;
            _tmpPath = tmpPath;
            _assetsPath = Path.Combine(tmpPath, "assets");


        }



        public async Task< bool >CoverVideo2MD()
        {


            Console.WriteLine("begin to Cover Video to MD");







            var fileName = blogId + ".md";



            Console.WriteLine("begin to extract Voice voice.wav"+" From the Video" + _videoPath);

            Console.WriteLine("Waiting.....");



            await extractWavFromMp4();

            Console.WriteLine("extract success!");




            Console.WriteLine("begin to Get Key Frame!");

            var frameList = getKeyFrame(_videoPath, _assetsPath, _tmpPath);



            Console.WriteLine("begin to covertWavToText");


            String audioPath = Path.Combine(_tmpPath, "voice.wav");


            String[] args = { _videoPath, audioPath, _assetsPath, _tmpPath };

           var SegmentListTask = covertWavToTextAsync(audioPath);


            



            

            var segmentList=await SegmentListTask;

            Console.WriteLine("covertWavToText success!");




   

            Console.WriteLine("get Key Frame success!");


            Console.WriteLine("begin to  execute MDGenerate");


            MDGenerate(_blog,_assetsPath, _tmpPath, segmentList, frameList);


            Console.WriteLine("END");







            return true;

        }



        public void MDGenerate(BlogNews blogNews,string assetsPath,string tmpPath,List<Segment>segmentList,List<Frame> frameList)
        {



            var blogPath=Path.Combine(tmpPath,blogNews.Title+".md");

            var segmentIdx = 0;
            var frameIdx = 0;




            string pre = "photo";


            using (StreamWriter sw = new StreamWriter(blogPath))
            {



                sw.WriteLine("# **" + _blog.Title + "**\n");
                sw.WriteLine("**=========================================================**\n");

                sw.WriteLine("**简介：**\n");


                sw.WriteLine("**" + _blog.Introduction + "**\n");



                sw.WriteLine("<font color= gray>画面时间：" + frameList[frameIdx].TimePoint.ToString() + "</font>\n");

                sw.WriteLine(tPhoto(frameIdx, frameList[frameIdx].FilePath));
                sw.WriteLine("  \n");




                while (segmentIdx < segmentList.Count && frameIdx < frameList.Count-1)
                {

                    double s_time = segmentList[segmentIdx].StartTime.TotalSeconds;

                    double e_time = segmentList[segmentIdx].EndTime.TotalSeconds;
                    double f_time = frameList[frameIdx].TimePoint.TotalSeconds;




                    if(Math.Abs(s_time - f_time) < Math.Abs(e_time - f_time))
                    {

                        if (pre == "text")
                        {
                            sw.WriteLine("。 \n");

                        }


                        


                        sw.WriteLine("<font color= gray>画面时间：" + frameList[frameIdx].TimePoint.ToString()+ "</font> \n");

                        sw.WriteLine("  \n");
                        sw.WriteLine(tPhoto(frameIdx+1, frameList[++frameIdx].FilePath));
                        sw.WriteLine("  \n");


                        pre = "photo";
                    }
                    else
                    {
                        if (pre == "text")
                        {
                            sw.Write("，");
                        }


                        sw.Write(" **" + segmentList[segmentIdx++].Text + "** ");


                        pre = "text";

                    }





                }




                while(frameIdx < frameList.Count-1)
                {
                    sw.WriteLine("<br/>");
                    sw.WriteLine("  \n");
                    sw.WriteLine("<font color= gray>画面时间：" + frameList[frameIdx].TimePoint.ToString() + "</font> \n");
                    sw.WriteLine("  \n");
                    sw.WriteLine("  \n");
                    sw.WriteLine(tPhoto(frameIdx, frameList[++frameIdx].FilePath));

                    sw.WriteLine("<br/>");
                }



                while (segmentIdx < segmentList.Count)
                {

                    if(pre=="text")
                    {
                        sw.Write(",");
                    }

                    sw.Write(" **" + segmentList[segmentIdx++].Text + "** ");
                }

                sw.WriteLine("。 \n");

            }




        }



        public string tPhoto(int photo_Id,String photo_path)
        {

            return $"![视频截图({photo_Id})]({photo_path})";


            
        }




        public async Task< bool> extractWavFromMp4()
        {
            string inputFile = _videoPath;


            string outputFile_tmp = Path.Combine(_tmpPath, "voice_tmp.wav");
            string outputFile = Path.Combine(_tmpPath, "voice.wav");





            var ConvertVideo =new NReco.VideoConverter.FFMpegConverter();
            ConvertVideo.ConvertMedia(inputFile, outputFile_tmp,"wav");


           await FFMpegArguments
               .FromFileInput(outputFile_tmp)
               .OutputToFile($"{outputFile}", true,
                  options => options
                     .ForceFormat("wav")
                     .WithAudioSamplingRate(16000))
               .ProcessAsynchronously();


            return true;


        }





        public List<Frame>getKeyFrame(String videoPath,String assetsPath,String tmpPath)
        {

            var ScriptPath = "GetKeyFrame\\test.py";
            string DataPath = Path.Combine(tmpPath, "DATA.json");

            String[] args= { videoPath, assetsPath ,DataPath};

            RunPythonScript(ScriptPath, "", args);
  


            




            

            List<Frame> res = new List<Frame>();

            Dictionary<float, string> dict=new Dictionary<float, string>();
            using (StreamReader sr = new StreamReader(DataPath))
            {
                string json = sr.ReadToEnd();

                dict= JsonConvert.DeserializeObject<Dictionary<float,string>>(json);
                // 获取某一个关键字的值

                
            }


            Console.WriteLine(dict.Count);

            foreach(KeyValuePair<float,string> item in dict)
            {
                var filePath = Path.Combine(assetsPath, item.Value);

                res.Add(new Frame { TimePoint=TimeSpan.FromSeconds(item.Key),FilePath=filePath });

                Console.WriteLine("Key:"+res[res.Count-1].TimePoint.ToString() +"text:"+item.Value);


            }

            Console.WriteLine(res);

            return res;

        }


        public async Task<List<Segment>> covertWavToTextAsync(String audioPath)
        {
            var ggmlType = GgmlType.Base;
            var modelFileName = "ggml-base.bin";
            var wavFileName = audioPath;

            if (!File.Exists(modelFileName))
            {
                await DownloadModel(modelFileName, ggmlType);
            }

            using var whisperFactory = WhisperFactory.FromPath("ggml-base.bin");

            List<Segment> res=new List<Segment>();

            using var processor = whisperFactory.CreateBuilder()
                .WithLanguage("auto")
                .Build();

            using var fileStream = File.OpenRead(wavFileName);

            await foreach (var result in processor.ProcessAsync(fileStream))
            {
                Console.WriteLine($"{result.Start}->{result.End}: {result.Text}");


                res.Add(new Segment { StartTime = result.Start, EndTime = result.End, Text = StringEncoding.ToSimplifiedChinese(result.Text) }) ;



            }

            if (res[res.Count - 1].Text.Length < 3)
            {
                res.Remove(res[res.Count - 1]);

            }

            return res;




        }

        private static async Task DownloadModel(string fileName, GgmlType ggmlType)
        {
            Console.WriteLine($"Downloading Model {fileName}");
            using var modelStream = await WhisperGgmlDownloader.GetGgmlModelAsync(ggmlType);
            using var fileWriter = File.OpenWrite(fileName);
            await modelStream.CopyToAsync(fileWriter);
        }



        public static bool RunPythonScript(string sArgName, string args = "", params string[] teps)
        {

            try
            {



                Process p = new Process();
                string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sArgName;// 获得python文件的绝对路径（将文件放在c#的debug文件夹中可以这样操作）
                path = @"E:/develop/VS_Workspace/MyBlog/PythonAPI/" + sArgName;//(因为我没放debug下，所以直接写的绝对路径,替换掉上面的路径了)
                p.StartInfo.FileName = @"E:/develop/Anaconda/envs/paddle/python.exe";//没有配环境变量的话，可以像我这样写python.exe的绝对路径。如果配了，直接写"python.exe"即可
                string sArguments = path;
                foreach (string sigstr in teps)
                {
                    sArguments += " " + sigstr;//传递参数
                }

                sArguments += " " + args;

                p.StartInfo.Arguments = sArguments;

                p.StartInfo.UseShellExecute = false;

                p.StartInfo.RedirectStandardOutput = true;

                p.StartInfo.RedirectStandardInput = true;

                p.StartInfo.RedirectStandardError = true;

                p.StartInfo.CreateNoWindow = true;



                p.Start();
                p.BeginOutputReadLine();
                p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                p.WaitForExit();
             

               // p.Close();


                // Console.ReadLine();
                //p.WaitForExit(1000000);

            }
            catch(Exception ex) {
                Console.WriteLine(ex.ToString());
                return false;
                 
            }

            return true;
        }
        //输出打印的信息
        static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                AppendText(e.Data + Environment.NewLine);
            }
        }
        public delegate void AppendTextCallback(string text);
        public static void AppendText(string text)
        {
            Console.WriteLine(text);     //此处在控制台输出.py文件print的结果

        }




    }





}
