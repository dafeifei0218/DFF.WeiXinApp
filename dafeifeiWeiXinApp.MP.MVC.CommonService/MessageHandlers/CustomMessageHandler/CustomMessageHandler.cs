using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.MP.AppStore;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;


namespace DFF.WeiXinApp.MP.MVC.CommonService.CustomMessageHandler
{
    public class CustomMessageHandler: MessageHandler<CustomMessageContext>
    {
        public CustomMessageHandler(Stream inputStream, PostModel postModel) 
            : base(inputStream, postModel)
        {
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>(); //ResponseMessageText也可以是News等其他类型
            responseMessage.Content = "这条消息来自DefaultResponseMessage。";
            return responseMessage;
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>(); //ResponseMessageText也可以是News等其他类型
            responseMessage.Content = "您的OpenID是：" + requestMessage.FromUserName  //这里的requestMessage.FromUserName也可以直接携程base.WeixinOpenId
                + "。\r\n您发送了文字信息：" + requestMessage.Content;                //\r\n用于执行，requestMessage.Content即用户发过来的文字内容
            return responseMessage;
        }
    }
}
