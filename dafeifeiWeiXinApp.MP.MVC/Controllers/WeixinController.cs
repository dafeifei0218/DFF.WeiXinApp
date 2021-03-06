﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities.Menu;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MvcExtension;
using DFF.WeiXinApp.MP.MVC.CommonService.CustomMessageHandler;

namespace DFF.WeiXinApp.MP.MVC.Controllers
{
    /// <summary>
    /// 微信控制器
    /// </summary>
    public class WeixinController : Controller
    {
        #region 验证登录

        //public static readonly string Token = "YourToken";
        //public static readonly string EncodingAESKey = "YourKey";
        //public static readonly string AppId = "YourAppId";

        public static readonly string Token = ConfigurationManager.AppSettings["Token"].ToString(); //与微信公众账号后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = ConfigurationManager.AppSettings["EncodingAESKey"].ToString(); //与微信公众账号后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string AppId = ConfigurationManager.AppSettings["AppId"].ToString(); //与微信公众账号后台的AppId设置保持一致，区分大小写。

        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：http://weixin.senparc.com/weixin
        /// </summary>
        /// <param name="postModel">微信公众服务器加密参数集合</param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Index")]
        //public ActionResult Index(string signature, string timestamp, string nonce, string echostr)
        public ActionResult Get(PostModel postModel, string echostr)
        {
            string token = ConfigurationManager.AppSettings["Token"].ToString();

            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, token))
            {
                return Content(echostr);
            }
            else
            {
                return Content("failed:" + postModel.Signature + "," + Senparc.Weixin.MP.CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, token) + "。" +
                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content("参数错误！");
            }

            postModel.Token = Token;
            postModel.EncodingAESKey = EncodingAESKey;
            postModel.AppId = AppId;

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new CustomMessageHandler(Request.InputStream, postModel); //接收消息

            messageHandler.Execute(); //执行微信处理过程

            return new FixWeixinBugWeixinResult(messageHandler); //返回结果
        }

        #endregion

        #region 已注释

        //public void Auth()
        //{
        //    string token = ConfigurationManager.AppSettings["Token"].ToString();
        //    string signature = Request.QueryString["signature"];
        //    string timestamp = Request.QueryString["timestamp"];
        //    string nonce = Request.QueryString["nonce"];
        //    string echostr = Request.QueryString["echostr"];

        //    if (Request.HttpMethod.ToUpper() == "GET")
        //    {
        //        //
        //        if (CheckSignature(signature, timestamp, nonce, token))
        //        {
        //            WriteContent();
        //        }
        //        else
        //        {
        //            WriteContent();
        //        }
        //    }
        //}

        //private void WriteContent()
        //{
        //    throw new NotImplementedException();
        //}

        //private bool CheckSignature(string signature, string timestamp, string nonce, string token)
        //{
        //    return signature == GetSignature(timestamp, nonce, token);
        //}

        //private string GetSignature(string timestamp, string nonce, string token)
        //{
        //    string[] arr = new[] {token, timestamp, nonce}.OrderBy(z => z).ToArray();
        //    string arrString = string.Join("", arr);
        //    System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
        //    byte[] sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
        //    StringBuilder enText = new StringBuilder();
        //    foreach (var b in sha1Arr)
        //    {
        //        enText.AppendFormat("{0:x2}", b);
        //    }
        //    return enText.ToString();
        //} 

        #endregion

        #region 二维码

        /// <summary>
        /// 二维码
        /// </summary>
        /// <returns></returns>
        public ActionResult ErWeiCode()
        {

            return View();
        } 

        #endregion
    }
}