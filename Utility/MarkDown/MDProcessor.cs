using Markdig;
using Markdig.Renderers.Normalize;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Microsoft.Extensions.Logging.Abstractions;
using MyBlog.Model;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.MarkDown
{

   


    public class MDProcessor
    {
        private readonly BlogNews _blog;
        private readonly string _importPath;
        private readonly string _assetsPath;




        public MDProcessor(BlogNews blog,string importPath,string exportPath)
        {
            _blog = blog;
            _importPath = importPath;
            _assetsPath = exportPath;
        }



        /// <summary>
        /// 解析MDProcessor导入的_blog，并且输出相应转换后的Content内容
        /// 准备把网站客户端上传的照片先赋值到一个存放临时文件的目录下，然后再把内容全部存放到数据库以及相应的位置，并把临时目录下的文件全部删除
        /// 
        /// </summary>
        /// <returns>content</returns>

        public string MarkdownParse()
        {
            if (_blog.Content == null)
            {
                return String.Empty;
            }

            var document = Markdown.Parse(_blog.Content);

            foreach (var node in document.AsEnumerable())
            {
                if (node is not ParagraphBlock { Inline: { } } paragraphBlock) continue;
                foreach (var inline in paragraphBlock.Inline)
                {
                    if (inline is not LinkInline { IsImage: true } linkInline) continue;

                    if (linkInline.Url == null) continue;
                    if (linkInline.Url.StartsWith("http")) continue;
                    Console.WriteLine(linkInline.Url);   //鬼知道这是什么东西，先输出一下康康
                    // 路径处理

                    var imgFileName = Path.GetFileName(linkInline.Url);
                    Console.WriteLine("imgFileName is"+imgFileName);
                    var imgPath = Path.Combine(_importPath, "assets", imgFileName);

                    var destDir = _assetsPath;
                    if (!Directory.Exists(destDir)) Directory.CreateDirectory(destDir);
                    var destPath = Path.Combine(destDir, imgFileName);
                    if (File.Exists(destPath))
                    {
                        // 图片重名处理
                        var imgId = Guid.NewGuid().ToString();
                        imgFileName = $"{Path.GetFileNameWithoutExtension(imgFileName)}-{imgId}.{Path.GetExtension(imgFileName)}";
                        destPath = Path.Combine(destDir, imgFileName);
                    }

                    // 替换图片链接
                    linkInline.Url = imgFileName;    //这里是不是有问题？

                    // 复制图片
                    File.Copy(imgPath, destPath);

                    Console.WriteLine($"复制 {imgPath} 到 {destPath}");
                }
            }


            using var writer = new StringWriter();
            var render = new NormalizeRenderer(writer);
            render.Render(document);
            return writer.ToString();

        }

        public string GetSummary(int length)
        {

            if (_blog.Content == null)
            {
                return String.Empty;
            }
            String content = Markdown.ToPlainText(_blog.Content);
            if (content.Length <= length)
            {
                return content;
            }
            else
            {
                return content.Substring(0, length);
            }
        }

    }


}
