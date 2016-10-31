using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VipJump
{
    public partial class test : System.Web.UI.Page
    {
        private string urlnode = ConfigurationSettings.AppSettings["urlnode"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if(string .IsNullOrEmpty (urlnode))
            {
                urlnode ="jump";
            }
            try
            {
                string res = GetPost();
                Response.Write(res);
            }
            catch (Exception ex)
            {
                Response.Write("异常："+ex.Message);
            }
        }

        public string GetStringURLPara()
        {
            if (Request.QueryString[urlnode] != null)
            {
                string method = Request.QueryString[urlnode].ToString();
                return method;
            }
            return "";
        }

        public string GetPost()
        {
            if ("POST" == Request.RequestType)
            {
                System.IO.Stream sm = Request.InputStream;//获取post正文
                int len = (int)sm.Length;//post数据长度
                byte[] inputByts = new byte[len];//字节数据,用于存储post数据
                sm.Read(inputByts, 0, len);//将post数据写入byte数组中
                sm.Close();//关闭IO流
                //**********下面是把字节数组类型转换成字符串**********
                string data = Encoding.GetEncoding("UTF-8").GetString(inputByts);//转为unicode编码
                //data = Server.UrlDecode(data);//url参数用到.
                return data;
            }
            else
                return null;
        }


     

    }
}