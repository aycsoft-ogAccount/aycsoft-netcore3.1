using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_PortalSite.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.20
    /// 描 述：详细信息维护
    /// </summary>
    [Area("LR_PortalSite")]
    public class ArticleController : MvcControllerBase
    {
        private readonly ArticleIBLL _articleIBLL;
        private readonly ImgIBLL _imgIBLL;

        public ArticleController(ArticleIBLL articleIBLL, ImgIBLL imgIBLL) {
            _articleIBLL = articleIBLL;
            _imgIBLL = imgIBLL;
        }


        #region 视图功能

        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
             return View();
        }
        /// <summary>
        /// 表单页
        /// </summary>
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
        /// <param name="queryJson">条件参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList( string queryJson )
        {
            var data =await _articleIBLL.GetList(queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">条件参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = await _articleIBLL.GetPageList(paginationobj, queryJson);
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
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetFormData(string keyValue)
        {
            var data =await _articleIBLL.GetEntity(keyValue);
            return Success(data);
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
            string[] content = keyValue.Split(',');
            foreach (var item in content)
            {
                var entity = await _articleIBLL.GetEntity(item);
                await _articleIBLL.DeleteEntity(item);

                if (!string.IsNullOrEmpty(entity.F_Img)) {
                   await  _imgIBLL.DeleteEntity(entity.F_Img);
                }
            }
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
        public async Task<IActionResult> SaveForm(string keyValue,ArticleEntity entity)
        {
            await _articleIBLL.SaveEntity(keyValue, entity);
            return SuccessInfo("保存成功！");
        }
        /// <summary>
        /// 保存图片和存储数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadFile(string keyValue, ArticleEntity entity)
        {
            entity.F_Content = entity.F_Content.Replace("script", "");
            var files = Request.Form.Files;
            //没有文件上传，直接返回
            if (files.Count == 0 || files[0].Length == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                await _articleIBLL.SaveEntity(keyValue, entity);
            }
            else
            {
                string FileEextension = Path.GetExtension(files[0].FileName);
                ImgEntity imgEntity = null;
                if (string.IsNullOrEmpty(entity.F_Img))
                {
                    imgEntity = new ImgEntity();
                }
                else
                {
                    imgEntity =await _imgIBLL.GetEntity(entity.F_Img);
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
                await _imgIBLL.SaveEntity(entity.F_Img, imgEntity);



                entity.F_ImgName = imgEntity.F_Name;
                entity.F_Img = imgEntity.F_Id;
                await _articleIBLL.SaveEntity(keyValue, entity);
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
            var stampEntity = await _articleIBLL.GetEntity(keyValue);
            if (stampEntity != null && !string.IsNullOrEmpty(stampEntity.F_Img))
            {
                ImgEntity imgEntity = await _imgIBLL.GetEntity(stampEntity.F_Img);
                if (imgEntity != null && !string.IsNullOrEmpty(imgEntity.F_Content))
                {
                    string imgContent = imgEntity.F_Content.Replace("data:image/" + imgEntity.F_ExName.Replace(".", "") + ";base64,", "");
                    byte[] arr = Convert.FromBase64String(imgContent);
                    return File(arr, "application/octet-stream");
                }
                else
                {
                    byte[] arr2 = FileHelper.ReadRoot("/img/plhome/banner_df.jpg");
                    return File(arr2, "application/octet-stream");
                }
            }
            else
            {
                byte[] arr3 = FileHelper.ReadRoot("/img/plhome/banner_df.jpg");
                return File(arr3, "application/octet-stream");
            }

        }
        #endregion
    }
}
