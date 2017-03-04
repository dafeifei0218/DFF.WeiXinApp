using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Senparc.Weixin.MP;

namespace DFF.WeiXinApp.MP.WebForm
{
    /// <summary>
    /// Weixin 的摘要说明
    /// </summary>
    public class Weixin : IHttpHandler
    {
        private readonly string Token = "dafeifei";//与微信公众账号后台的Token设置保持一致，区分大小写。

        public void ProcessRequest(HttpContext context)
        {
            string signature = context.Request["signature"];
            string timestamp = context.Request["timestamp"];
            string nonce = context.Request["nonce"];
            string echostr = context.Request["echostr"];

            if (context.Request.HttpMethod == "GET")
            {
                //get method - 仅在微信后台填写URL验证时触发
                if (CheckSignature.Check(signature, timestamp, nonce, Token))
                {
                    //WriteContent(echostr); //返回随机字符串则表示验证通过
                    context.Response.Output.Write(echostr);
                }
                else
                {
                    //WriteContent("failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, Token));
                    context.Response.Write("failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, Token));
                }
            }
            else
            {
                //判断Post或其他方式请求
            }
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void WriteContent(string str)
        {
            //Response.Output.Write(str);
        }
    }
}