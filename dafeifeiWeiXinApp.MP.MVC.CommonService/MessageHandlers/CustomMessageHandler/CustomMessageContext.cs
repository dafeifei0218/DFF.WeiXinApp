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
        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomMessageContext()
        {
            base.MessageContextRemoved += CustomMessageContext_MessageContextRemoved;
        }

        /// <summary>
        /// 当MessageContext被删除时触发的事件。（当上下文过期，被移除时触发的事件） 
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
