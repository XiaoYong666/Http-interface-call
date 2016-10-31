using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string ss = "hello world!";
            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] bytesToPost = encoding.GetBytes(ss);
            string res = Post("http://localhost:23094/test.aspx", bytesToPost);
            Console.WriteLine(res);
            Console.ReadLine();
        }

        private static string Post(string url, byte[] bytesToPost)
        {
            if (String.IsNullOrEmpty(url))
                return "url参数为空值";
            if (bytesToPost == null)
                return "post数据为空值";
            string ResponseString = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            System.Net.ServicePointManager.DefaultConnectionLimit = 50;
            request.KeepAlive = false;
            request.Method = "POST";
            request.ContentType = "text/xml";//提交xml   
            request.ContentLength = bytesToPost.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(bytesToPost, 0, bytesToPost.Length);
            HttpWebResponse HttpWebRespon = (HttpWebResponse)request.GetResponse();
            StreamReader myStreamReader = new StreamReader(HttpWebRespon.GetResponseStream(), Encoding.UTF8);
            ResponseString = myStreamReader.ReadToEnd();
            myStreamReader.Close();

            writer.Flush();
            if (writer != null)
            {
                writer.Close();
            }
            if (request != null)
            {
                request.Abort();
            }
            return ResponseString;
        }
    }
}
