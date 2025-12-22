using ce.autofac.extension;
using aycsoft.webapp.Models;
using aycsoft.iapplication;
using aycsoft.operat;
using aycsoft.util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace aycsoft.webapp.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.26
    /// 描 述：主页面模块控制器
    /// </summary>
    public class HomeController : MvcControllerBase
    {
        private readonly LogIBLL _logIBLL;
        private readonly IHttpContextAccessor _iHttpContextAccessor;

        public HomeController(LogIBLL logIBLL, IHttpContextAccessor iHttpContextAccessor)
        {
            _logIBLL = logIBLL;
            _iHttpContextAccessor = iHttpContextAccessor;
        }


        #region 视图功能
        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            string uitheme = GetCookies("lr_adms_core_ui");
            switch (uitheme)
            {
                case "1":
                    return View("AdminDefault");      // 经典版本
                case "2":
                    return View("AdminAccordion");    // 手风琴版本
                case "3":
                    return View("AdminWindos");       // Windos版本
                case "4":
                    return View("AdminTop");          // 顶部菜单版本
                default:
                    return View("AdminDefault");            // 经典版本
            }
        }

        /// <summary>
        /// 桌面
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AdminDesktop()
        {
            var userInfo = await this.CurrentUser();
            string uItheme = userInfo.F_HeadIcon;
            string[] uIthemeList = ConfigHelper.GetConfig().UItheme.Split(",");
            bool findFlag = false;
            foreach (var item in uIthemeList)
            {
                findFlag |= item == uItheme;
            }

            if (!findFlag)
            {
                uItheme = "default";
            }


            return uItheme switch
            {
                "default" => View("DesktopDefault"),      // 经典版本
                "accordion" => View("DesktopAccordion"),    // 手风琴版本
                "windows" => View("DesktopWindos"),       // Windos版本
                "top" => View("DesktopTop"),          // 顶部菜单版本
                _ => View("DesktopDefault"),      // 经典版本
            };
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        /// <summary>
        /// 访问功能
        /// </summary>
        /// <param name="moduleName">功能模块</param>
        /// <param name="moduleUrl">访问路径</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> VisitModule(string moduleName, string moduleUrl)
        {
            var userInfo = await this.CurrentUser();
            LogEntity logEntity = new LogEntity();
            logEntity.F_CategoryId = 2;
            logEntity.F_OperateTypeId = ((int)OperationType.Visit).ToString();
            logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Visit);
            logEntity.F_OperateAccount = userInfo.F_Account;
            logEntity.F_OperateUserId = userInfo.F_UserId;
            logEntity.F_Module = moduleName;
            logEntity.F_ExecuteResult = 1;
            logEntity.F_IPAddress = _iHttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            logEntity.F_ExecuteResultJson = "访问地址：" + moduleUrl;
            await _logIBLL.Write(logEntity);
            return Success("ok");
        }
    }
}
