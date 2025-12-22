using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_NewWorkFlow.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.03.13
    /// 描 述：签章管理
    /// </summary>
    [Area("LR_NewWorkFlow")]
    public class StampInfoController : MvcControllerBase
    {
        private readonly StampIBLL _stampIBLL;
        private readonly ImgIBLL _imgIBLL;

        public StampInfoController(StampIBLL stampIBLL, ImgIBLL imgIBLL) {
            _stampIBLL = stampIBLL;
            _imgIBLL = imgIBLL;
        }

        #region 视图功能 
        /// <summary>
        /// 管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StampDetailIndex()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EqualForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = await _stampIBLL.GetPageList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                paginationobj.total,
                paginationobj.page,
                paginationobj.records
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取所有的印章信息
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList(string keyword)
        {
            var data =await _stampIBLL.GetList(keyword);
            return Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存印章
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, StampEntity entity)
        {
            await _stampIBLL.SaveEntity(keyValue, entity);
            return SuccessInfo("保存成功。");

        }
        /// <summary>
        /// 删除印章
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            var stampEntity = await _stampIBLL.GetEntity(keyValue);
            if (stampEntity != null && !string.IsNullOrEmpty(stampEntity.F_ImgFile)) {
                await _imgIBLL.DeleteEntity(stampEntity.F_ImgFile);
            }
            await _stampIBLL.DeleteEntity(keyValue);
            return SuccessInfo("删除成功！");
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取图片文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetImg(string keyValue)
        {
            var stampEntity = await _stampIBLL.GetEntity(keyValue);
            if (stampEntity != null && !string.IsNullOrEmpty(stampEntity.F_ImgFile))
            {
                ImgEntity imgEntity = await _imgIBLL.GetEntity(stampEntity.F_ImgFile);
                if (imgEntity != null && !string.IsNullOrEmpty(imgEntity.F_Content))
                {
                    string imgContent = imgEntity.F_Content.Replace("data:image/" + imgEntity.F_ExName.Replace(".", "") + ";base64,", "");
                    byte[] arr = Convert.FromBase64String(imgContent);
                    return File(arr, "application/octet-stream");
                }
                else
                {
                    byte[] arr2 = FileHelper.ReadRoot("/img/add.jpg");
                    return File(arr2, "application/octet-stream");
                }
            }
            else {
                byte[] arr3 = FileHelper.ReadRoot("/img/add.jpg");
                return File(arr3, "application/octet-stream");
            }
            
        }
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">印章实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadFile(string keyValue, StampEntity entity)
        {
            var files = Request.Form.Files;
            //没有文件上传，直接返回
            if (files.Count == 0 || files[0].Length == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                await _stampIBLL.SaveEntity(keyValue, entity);
            }
            else
            {
                string FileEextension = Path.GetExtension(files[0].FileName);
                ImgEntity imgEntity = null;
                if (string.IsNullOrEmpty(entity.F_ImgFile))
                {
                    imgEntity = new ImgEntity();
                }
                else
                {
                    imgEntity = await _imgIBLL.GetEntity(entity.F_ImgFile);
                    if (imgEntity == null)
                    {
                        imgEntity = new ImgEntity();
                    }
                }

                imgEntity.F_Name = files[0].FileName;
                imgEntity.F_ExName = FileEextension;
                byte[] bytes = new byte[files[0].Length];
                MemoryStream ms = new MemoryStream();
                files[0].CopyTo(ms);
                ms.Flush();
                ms.Position = 0;
                ms.Read(bytes, 0, bytes.Length);
                ms.Close();
                ms.Dispose();
                imgEntity.F_Content = Convert.ToBase64String(bytes);
                await _imgIBLL.SaveEntity(entity.F_ImgFile, imgEntity);

                entity.F_ImgFile = imgEntity.F_Id;
                await _stampIBLL.SaveEntity(keyValue, entity);
            }
            return SuccessInfo("保存成功。");
        }

        /// <summary>
        /// 启用/停用
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态 1启用 0禁用</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> UpDateSate(string keyValue, int state)
        {
            await _stampIBLL.UpdateState(keyValue, state);
            return SuccessInfo((state == 1 ? "启用" : "禁用") + "成功！");
        }
        /// <summary>
        /// 密码验证
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> EqualPW(string keyValue, string password)
        {
            var result =await _stampIBLL.EqualPassword(keyValue, password);
            if (result)
            {
                return SuccessInfo("密码验证成功！");
            }
            else
            {
                return Fail("密码不正确！");
            }

        }
        #endregion
    }
}