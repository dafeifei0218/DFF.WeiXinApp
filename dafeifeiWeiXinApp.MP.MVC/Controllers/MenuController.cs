using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities.Menu;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.CommonAPIs;

namespace DFF.WeiXinApp.MP.MVC.Controllers
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    public class MenuController : Controller
    {
        public static readonly string appId = ConfigurationManager.AppSettings["AppId"].ToString();
        public static readonly string appSecret = ConfigurationManager.AppSettings["AppSecret"].ToString();

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenu()
        {
            var accessToken = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
            //查询菜单
            var result02 = CommonApi.GetMenu(accessToken);

            return View();
        }

        #region 创建默认菜单

        /// <summary>
        /// 创建默认菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateMenuDefault()
        {

            var accessToken = AccessTokenContainer.TryGetAccessToken(appId, appSecret);

            ButtonGroup buttonGroup = new ButtonGroup();

            //单击测试
            buttonGroup.button.Add(new SingleClickButton()
            {
                key = "单击测试",
                name = "OneClick",
                type = ButtonType.click.ToString()
            });

            #region 二级菜单

            var subButton = new SubButton()
            {
                name = "二级菜单"
            };

            #region 子菜单

            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_Text",
                name = "返回文本"
            });
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_News",
                name = "返回图文"
            });
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "SubClickRoot_Music",
                name = "返回音乐"
            });
            subButton.sub_button.Add(new SingleClickButton()
            {
                key = "http://www.baidu.com",
                name = "Url跳转"
            });

            #endregion

            buttonGroup.button.Add(subButton);

            #endregion

            //提交到微信服务器
            var result01 = CommonApi.CreateMenu(accessToken, buttonGroup);
            ViewBag.WxJsonResult = result01;

            ViewBag.accessToken = accessToken;

            //查询菜单
            var result02 = CommonApi.GetMenu(accessToken);
            ViewData["ConditionalMenu"] = result02.conditionalmenu;
            ViewData["Menu"] = result02.menu;

            //删除菜单
            //var result03 = CommonApi.DeleteMenu(accessToken);

            return View();
        }

        #endregion
    }
}