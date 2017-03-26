using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;

namespace DFF.WeiXinApp.MP.MVC.CommonService.CustomMessageHandler
{
    /// <summary>
    /// 自定义消息内容
    /// </summary>
    public class CustomMessageContext : MessageContext<IRequestMessageBase, IResponseMessageBase>
    {
        public CustomMessageContext()
        {
            base.MessageContextRemoved += CustomMessageContext_MessageContextRemoved;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomMessageContext_MessageContextRemoved(object sender, WeixinContextRemovedEventArgs<IRequestMessageBase, IResponseMessageBase> e)
        {
            var messageContext = e.MessageContext as CustomMessageContext;
            if (messageContext == null)
            {
                return; //如果是正常的调用，messageContext不会为null
            }
        }


    }
}
