using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System;
using System.IO;
using Microsoft.VisualBasic;


namespace Utility.Language
{


        /// <summary>
    /// 字符串编码转换类
    /// </summary>
    public class StringEncoding
    {
        private StringEncoding()
        {
        }

        /// <summary>
        /// 将字符串转换为简体中文
        /// </summary>
        public static string ToSimplifiedChinese(string s)
        {
            return Microsoft.VisualBasic.Strings.StrConv(s, VbStrConv.SimplifiedChinese, 0);
        }

        /// <summary>
        /// 将文件转换为简体中文
        /// </summary>
        /// <param name="filename">源文件名</param>
        /// <param name="encoding">源文件字符编码</param>
        /// <param name="outFilename">目标文件名</param>
        /// <param name="outEncoding">目标文件字符编码</param>
        /// <example>
        /// <code>
        /// ToSimplifiedChinese("big5.txt", Encoding.GetEncoding("big5"), "gb.txt", Encoding.GetEncoding("gb2312"));
        /// ToSimplifiedChinese("big5.txt", Encoding.GetEncoding("big5"), "gb.txt", Encoding.UTF8);
        /// </code>
        /// </example>
        public static void ToSimplifiedChinese(string filename, Encoding encoding, string outFilename, Encoding outEncoding)
        {
            StreamReader r = new StreamReader(filename, encoding);
            StreamWriter w = new StreamWriter(outFilename, false, outEncoding);
            try
            {
                w.Write(Strings.StrConv(r.ReadToEnd(), VbStrConv.SimplifiedChinese, 0));
                w.Flush();
            }
            finally
            {
                w.Close();
                r.Close();
            }
        }

        /// <summary>
        /// 将字符串转换为繁体中文
        /// </summary>
        public static string ToTraditionalChinese(string s)
        {
            return Microsoft.VisualBasic.Strings.StrConv(s, VbStrConv.TraditionalChinese, 0);
        }

        /// <summary>
        /// 将文件转换为繁体中文
        /// </summary>
        /// <param name="filename">源文件名</param>
        /// <param name="encoding">源文件字符编码</param>
        /// <param name="outFilename">目标文件名</param>
        /// <param name="outEncoding">目标文件字符编码</param>
        /// <example>
        /// <code>
        /// ToTraditionalChinese("gb.txt", Encoding.GetEncoding("gb2312"), "gb.txt", Encoding.GetEncoding("big5"));
        /// ToTraditionalChinese("gb.txt", Encoding.GetEncoding("gb2312"), "gb.txt", Encoding.UTF8);
        /// </code>
        /// </example>
        public static void ToTraditionalChinese(string filename, Encoding encoding, string outFilename, Encoding outEncoding)
        {
            StreamReader r = new StreamReader(filename, encoding);
            StreamWriter w = new StreamWriter(outFilename, false, outEncoding);
            try
            {
                w.Write(Strings.StrConv(r.ReadToEnd(), VbStrConv.TraditionalChinese, 0));
                w.Flush();
            }
            finally
            {
                w.Close();
                r.Close();
            }
        }
    }

}
