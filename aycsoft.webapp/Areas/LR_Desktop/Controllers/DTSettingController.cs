using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aycsoft.webapp.Areas.LR_Desktop.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.31
    /// 描 述：桌面配置
    /// </summary>
    [Area("LR_Desktop")]
    public class DTSettingController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// 移动端桌面配置（和pc桌面采用一套数据）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AppIndex()
        {
            return View();
        }
        /// <summary>
        /// pc端桌面设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PcIndex()
        {
            var userInfo =await this.CurrentUser();
            string uItheme = userInfo.F_HeadIcon;
            string[] uIthemeList = ConfigHelper.GetConfig().UItheme.Split(",");
            bool findFlag = false;
            foreach (var item in uIthemeList) {
                findFlag |= item == uItheme;
            }

            if (!findFlag) {
                uItheme = "default";
            }


            return uItheme switch
            {
                "default" => View("DefaultIndex"),      // 经典版本
                "accordion" => View("AccordionIndex"),    // 手风琴版本
                "windows" => View("WindowsIndex"),       // Windos版本
                "top" => View("TopIndex"),          // 顶部菜单版本
                _ => View("DefaultIndex"),      // 经典版本
            };
        }
        #endregion
    }
}
