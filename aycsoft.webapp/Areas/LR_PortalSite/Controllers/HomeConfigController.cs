using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_PortalSite.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.20
    /// 描 述：首页设置
    /// </summary>
    [Area("LR_PortalSite")]
    public class HomeConfigController : MvcControllerBase
    {
        private readonly HomeConfigIBLL _homeConfigIBLL;
        private readonly ImgIBLL _imgIBLL;

        public HomeConfigController(HomeConfigIBLL homeConfigIBLL, ImgIBLL imgIBLL) {
            _homeConfigIBLL = homeConfigIBLL;
            _imgIBLL = imgIBLL;
        }


        #region 视图功能
        /// <summary>
        /// 首页配置页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 设置文字
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SetTextForm()
        {
            return View();
        }
        /// <summary>
        /// 顶部菜单设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TopMenuIndex()
        {
            return View();
        }
        /// <summary>
        /// 顶部菜单设置（表单）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TopMenuForm()
        {
            return View();
        }
        /// <summary>
        /// 底部菜单设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult BottomMenuIndex()
        {
            return View();
        }
        /// <summary>
        /// 底部菜单设置(表单)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult BottomMenuForm()
        {
            return View();
        }
        /// <summary>
        /// 配置轮播图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PictureForm()
        {
            return View();
        }
        /// <summary>
        /// 添加模块（选择类型）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SelectModuleForm()
        {
            return View();
        }
        /// <summary>
        /// 模块1配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ModuleForm1() {
            return View();
        }
        /// <summary>
        /// 模块2配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ModuleForm2()
        {
            return View();
        }
        /// <summary>
        /// 模块3配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ModuleForm3()
        {
            return View();
        }

        /// <summary>
        /// 模块3添加tab标签
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddTabForm()
        {
            return View();
        }

        /// <summary>
        /// 模块4配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ModuleForm4()
        {
            return View();
        }
        /// <summary>
        /// 模块5配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ModuleForm5()
        {
            return View();
        }


        #endregion

        #region 获取数据
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetAllList()
        {
            var data =await _homeConfigIBLL.GetALLList();
            return Success(data);
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList(string type)
        {
            var data =await _homeConfigIBLL.GetList(type);
            return Success(data);
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTree()
        {
            var data = await _homeConfigIBLL.GetTree();
            return Success(data);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetEntity(string keyValue)
        {
            var data =await _homeConfigIBLL.GetEntity(keyValue);
            return Success(data);
        }

        /// <summary>
        /// 获取图片文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetImg(string type)
        {
            var entity =await _homeConfigIBLL.GetEntityByType(type);
            if (entity != null && !string.IsNullOrEmpty(entity.F_Img))
            {
                ImgEntity imgEntity = await _imgIBLL.GetEntity(entity.F_Img);
                if (imgEntity != null && !string.IsNullOrEmpty(imgEntity.F_Content))
                {
                    string imgContent = imgEntity.F_Content.Replace("data:image/" + imgEntity.F_ExName.Replace(".", "") + ";base64,", "");
                    byte[] arr = Convert.FromBase64String(imgContent);
                    return File(arr, "application/octet-stream");
                }
                else
                {
                    byte[] arr2 = FileHelper.ReadRoot("/img/plhome/addImg.png");
                    return File(arr2, "application/octet-stream");
                }
            }
            else
            {
                byte[] arr3 = FileHelper.ReadRoot("/img/plhome/addImg.png");
                return File(arr3, "application/octet-stream");
            }

        }
        /// <summary>
        /// 获取图片文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetImg2(string keyValue)
        {
            var entity = await _homeConfigIBLL.GetEntity(keyValue);
            if (entity != null && !string.IsNullOrEmpty(entity.F_Img))
            {
                ImgEntity imgEntity = await _imgIBLL.GetEntity(entity.F_Img);
                if (imgEntity != null && !string.IsNullOrEmpty(imgEntity.F_Content))
                {
                    string imgContent = imgEntity.F_Content.Replace("data:image/" + imgEntity.F_ExName.Replace(".", "") + ";base64,", "");
                    byte[] arr = Convert.FromBase64String(imgContent);
                    return File(arr, "application/octet-stream");
                }
                else
                {
                    byte[] arr2 = FileHelper.ReadRoot("/img/plhome/addImg.png");
                    return File(arr2, "application/octet-stream");
                }
            }
            else
            {
                byte[] arr3 = FileHelper.ReadRoot("/img/plhome/addImg.png");
                return File(arr3, "application/octet-stream");
            }

        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await _homeConfigIBLL.DeleteEntity(keyValue);
            return SuccessInfo("删除成功！");
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, HomeConfigEntity entity)
        {
            await _homeConfigIBLL.SaveEntity(keyValue, entity);
            return Success(entity.F_Id);
        }


        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue1">主键1</param>
        /// <param name="keyValue2">主键2</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> UpdateForm(string keyValue1,string keyValue2)
        {
            HomeConfigEntity entity1 = await _homeConfigIBLL.GetEntity(keyValue1);
            HomeConfigEntity entity2 = await _homeConfigIBLL.GetEntity(keyValue2);

            if (entity1 != null && entity2 != null) {
                int sort = (int)entity1.F_Sort;
                entity1.F_Sort = entity2.F_Sort;
                entity2.F_Sort = sort;

                await _homeConfigIBLL.SaveEntity(entity1.F_Id, entity1);
                await _homeConfigIBLL.SaveEntity(entity2.F_Id, entity2);
            } 

            return Success("更新成功！");
        }

        /// <summary>
        /// 保存文字
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="text">文字</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveText(string type, string text)
        {
            await _homeConfigIBLL.SaveText(text, type);
            return Success("保存成功！");
        }
        /// <summary>
        /// 保存图片和存储数据
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> UploadFile(string type)
        {
            var files = Request.Form.Files;
            //没有文件上传，直接返回
            if (files[0].Length == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                return Fail("没有文件信息");
            }
            else
            {
                string FileEextension = Path.GetExtension(files[0].FileName);
                byte[] bytes = new byte[files[0].Length];
                MemoryStream ms = new MemoryStream();

                files[0].CopyTo(ms);
                ms.Flush();
                ms.Position = 0;
                ms.Read(bytes, 0, bytes.Length);
                ms.Dispose();

                string strBase64 = Convert.ToBase64String(bytes);
                await  _homeConfigIBLL.SaveImg(strBase64, files[0].FileName, FileEextension, type);
            }

            return SuccessInfo("保存成功。");
        }
        /// <summary>
        /// 保存图片和存储数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="sort">排序码</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadFile2(string keyValue,int sort)
        {
            var files = Request.Form.Files;
            //没有文件上传，直接返回
            if (files[0].Length == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                return Fail("没有文件信息");
            }
            else
            {
                string FileEextension = Path.GetExtension(files[0].FileName);
                byte[] bytes = new byte[files[0].Length];
                MemoryStream ms = new MemoryStream();

                files[0].CopyTo(ms);
                ms.Flush();
                ms.Position = 0;
                ms.Read(bytes, 0, bytes.Length);
                ms.Dispose();

                string strBase64 = Convert.ToBase64String(bytes);
                await _homeConfigIBLL.SaveImg2(strBase64, files[0].FileName, FileEextension, keyValue, sort);
            }

            return SuccessInfo("保存成功。");
        }
        #endregion
    }
}