using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace aycsoft.webapp.Areas.LR_CodeDemo.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.27
    /// 描 述：信息可视化演示
    /// </summary>
    [Area("LR_CodeDemo")]
    public class ImgShowController : MvcControllerBase
    {        
        [HttpGet]
        public IActionResult Demo1()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Demo2()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Demo3()
        {
            return View();
        }
    }
}