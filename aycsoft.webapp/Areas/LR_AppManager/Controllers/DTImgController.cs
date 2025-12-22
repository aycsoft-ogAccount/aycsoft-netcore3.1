using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_AppManager
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.08
    /// 描 述：App首页图片管理
    /// </summary>
    [Area("LR_AppManager")]
    public class DTImgController : MvcControllerBase
    {
        private readonly DTImgIBLL _dTImgIBLL;
        private readonly ImgIBLL _imgIBLL;
        public DTImgController(DTImgIBLL dTImgIBLL, ImgIBLL imgIBLL) {
            _dTImgIBLL = dTImgIBLL;
            _imgIBLL = imgIBLL;
        }

        #region 视图功能

        /// <summary> 
        /// 主页面 
        /// <summary> 
        /// <returns></returns> 
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary> 
        /// 表单页 
        /// <summary> 
        /// <returns></returns> 
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据 

        /// <summary> 
        /// 获取列表数据 
        /// </summary> 
        /// <returns></returns> 
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList()
        {
            var data =await _dTImgIBLL.GetList();
            return Success(data);
        }
        /// <summary> 
        /// 获取列表分页数据 
        /// </summary> 
        /// <param name="pagination">分页参数</param> 
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data =await _dTImgIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取表单数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetFormData(string keyValue)
        {
            var data =await _dTImgIBLL.GetEntity(keyValue);
            return Success(data);
        }
        #endregion

        #region 提交数据 

        /// <summary> 
        /// 删除实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            var dTImgEntity = await _dTImgIBLL.GetEntity(keyValue);
            if (dTImgEntity != null && !string.IsNullOrEmpty(dTImgEntity.F_FileName))
            {
                await _imgIBLL.DeleteEntity(dTImgEntity.F_FileName);
            }
            await _dTImgIBLL.DeleteEntity(keyValue);
            return SuccessInfo("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改） 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, DTImgEntity entity)
        {
            await _dTImgIBLL.SaveEntity(keyValue, entity);
            return SuccessInfo("保存成功！");
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">印章实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadFile(string keyValue, DTImgEntity entity)
        {
            var files = Request.Form.Files;
            //没有文件上传，直接返回
            if (files[0].Length == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                await _dTImgIBLL.SaveEntity(keyValue, entity);
            }
            else
            {
                string FileEextension = Path.GetExtension(files[0].FileName);
                ImgEntity imgEntity = null;
                if (string.IsNullOrEmpty(entity.F_FileName))
                {
                    imgEntity = new ImgEntity();
                }
                else
                {
                    imgEntity = await _imgIBLL.GetEntity(entity.F_FileName);
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
                await _imgIBLL.SaveEntity(entity.F_FileName, imgEntity);

                entity.F_FileName = imgEntity.F_Id;
                await _dTImgIBLL.SaveEntity(keyValue, entity);
            }
            return SuccessInfo("保存成功。");
        }

        /// <summary>
        /// 获取图片文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetImg(string keyValue)
        {
            var stampEntity = await _dTImgIBLL.GetEntity(keyValue);
            if (stampEntity != null && !string.IsNullOrEmpty(stampEntity.F_FileName))
            {
                ImgEntity imgEntity = await _imgIBLL.GetEntity(stampEntity.F_FileName);
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
            else
            {
                byte[] arr3 = FileHelper.ReadRoot("/img/add.jpg");
                return File(arr3, "application/octet-stream");
            }

        }

        /// <summary>
        /// 启用/停用
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态1启用0禁用</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> UpDateSate(string keyValue, int state)
        {
            await _dTImgIBLL.UpdateState(keyValue, state);
            return SuccessInfo((state == 1 ? "启用" : "禁用") + "成功！");
        }
        #endregion
    }
}