using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace aycsoft.webapi.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.17
    /// 描 述：附件API
    /// </summary>
    public class AnnexesController : MvcControllerBase
    {
        private readonly AnnexesFileIBLL _annexesFileIBLL;
        /// <summary>
        /// 初始方法
        /// </summary>
        /// <param name="annexesFileIBLL">附件方法</param>
        public AnnexesController(AnnexesFileIBLL annexesFileIBLL)
        {
            _annexesFileIBLL = annexesFileIBLL;
        }

        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <param name="folderId">文件夹Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List(string folderId)
        {
            var list = await _annexesFileIBLL.GetList(folderId);
            return Success(list);
        }

        /// <summary>
        /// 上传附件图片文件
        /// </summary>
        /// <param name="folderId">文件夹Id</param>
        /// <param name="file">文件</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm]string folderId, [FromForm]IFormFile file)
        {
            string filePath = ConfigHelper.GetConfig().AnnexesFile;
            string uploadDate = DateTime.Now.ToString("yyyyMMdd");
            string fileGuid = Guid.NewGuid().ToString();
            string FileEextension = Path.GetExtension(file.FileName);
            //文件存放路径格式：/{account}/yyyymmdd/{guid}.{后缀名}
            string virtualPath = string.Format("{0}/{1}/{2}/{3}{4}", filePath, this.GetUserAccount(), uploadDate, fileGuid, FileEextension);
            //创建文件夹
            string path = Path.GetDirectoryName(virtualPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            AnnexesFileEntity fileAnnexesEntity = new AnnexesFileEntity();
            if (!System.IO.File.Exists(virtualPath))
            {
                FileInfo fileInfo = new FileInfo(virtualPath);
                FileStream fs = fileInfo.Create();
                file.CopyTo(fs);
                fs.Close();

                //文件信息写入数据库
                fileAnnexesEntity.F_Id = fileGuid;
                fileAnnexesEntity.F_FileName = file.FileName;
                fileAnnexesEntity.F_FilePath = virtualPath;
                fileAnnexesEntity.F_FileSize = file.Length.ToString();
                fileAnnexesEntity.F_FileExtensions = FileEextension;
                fileAnnexesEntity.F_FileType = FileEextension.Replace(".", "");
                fileAnnexesEntity.F_CreateUserId = this.GetUserId();
                fileAnnexesEntity.F_CreateUserName = this.GetUserName();

                await _annexesFileIBLL.SaveEntity(folderId, fileAnnexesEntity);
            }

            return Success(fileGuid);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileId">文件主键</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]string fileId)
        {
            var fileInfoEntity = await _annexesFileIBLL.GetEntity(fileId);
            await _annexesFileIBLL.DeleteEntity(fileId);
            //删除文件
            if (System.IO.File.Exists(fileInfoEntity.F_FilePath))
            {
                System.IO.File.Delete(fileInfoEntity.F_FilePath);
            }

            return SuccessInfo("删除成功");
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileId">文件主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Down(string fileId)
        {
            var data = await _annexesFileIBLL.GetEntity(fileId);
            if (System.IO.File.Exists(data.F_FilePath))
            {
                return NotFound();
            }

            byte[] arr2 = FileHelper.Read(data.F_FilePath);
            return File(arr2, FileHelper.getContentType(data.F_FileType), data.F_FileName);
        }
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="fileId">文件主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> FileInfo(string fileId)
        {
            var fileEntity = await _annexesFileIBLL.GetEntity(fileId);
            return Success(fileEntity);
        }


        /// <summary>
        /// 获取图片文件的 Base64 编码 (钉钉端获取手写签名)
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ToBase64(IFormFile file)
        {
            var stream = file.OpenReadStream();
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            var result = Convert.ToBase64String(bytes);
            stream.Close();
            stream.Dispose();
            return Success(result);
        }

    }
}