using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.18
    /// 描 述：图片控制器
    /// </summary>
    [Area("LR_SystemModule")]
    public class ImgController : MvcControllerBase
    {
        private readonly ImgIBLL _imgIBLL;

        public ImgController(ImgIBLL imgIBLL)
        {
            _imgIBLL = imgIBLL;
        }

        #region 获取数据
        /// <summary>
        /// 获取图片文件
        /// </summary>
        /// <param name="id">图片编码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Down(string id)
        {

            ImgEntity imgEntity = await _imgIBLL.GetEntity(id);
            if (imgEntity != null && !string.IsNullOrEmpty(imgEntity.F_Content))
            {
                string imgContent = imgEntity.F_Content.Replace("data:image/" + imgEntity.F_ExName.Replace(".", "") + ";base64,", "");
                byte[] arr = Convert.FromBase64String(imgContent);
                return File(arr, "application/octet-stream");
            }
            else
            {
                byte[] arr2 = FileHelper.ReadRoot("/img/logo_default.png");
                return File(arr2, "application/octet-stream");
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadFile(string code)
        {
            var files =Request.Form.Files;
            //没有文件上传，直接返回
            if (files[0].Length == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                return Fail("没有文件信息");
            }
            string FileEextension = Path.GetExtension(files[0].FileName);
            ImgEntity imgEntity = new ImgEntity();

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
            _imgIBLL.SaveEntity(code, imgEntity);
            return SuccessInfo("上传成功。");
        }
        #endregion
    }
}