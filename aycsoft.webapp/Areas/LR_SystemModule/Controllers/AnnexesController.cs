using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.03.03
    /// 描 述：行政区域
    /// </summary>
    [Area("LR_SystemModule")]
    public class AnnexesController : MvcControllerBase
    {
        private readonly AnnexesFileIBLL _annexesFileIBLL;

        public AnnexesController(AnnexesFileIBLL annexesFileIBLL) {
            _annexesFileIBLL = annexesFileIBLL;
        }

        #region 视图功能
        /// <summary>
        /// 上传列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UploadForm()
        {
            return View();
        }
        /// <summary>
        /// 下载列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DownForm()
        {
            return View();
        }

        #region 文件预览
        /// <summary>
        /// 文件预览
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PreviewForm()
        {
            return View();
        }
        #endregion
        #endregion

        #region 提交数据
        /// <summary>
        /// 上传附件分片数据
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="chunk">分片序号</param>
        /// <param name="Filedata">文件数据</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadAnnexesFileChunk(string fileGuid, int chunk, int chunks)
        {
            var files = Request.Form.Files;
            //没有文件上传，直接返回
            if (files[0].Length == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                return Fail("没有文件信息");
            }
            byte[] bytes = new byte[files[0].Length];
            MemoryStream ms = new MemoryStream();
            files[0].CopyTo(ms);
            ms.Flush();
            ms.Position = 0;
            ms.Read(bytes, 0, bytes.Length);
            ms.Close();
            ms.Dispose();
            _annexesFileIBLL.SaveChunkAnnexes(fileGuid, chunk, bytes);
            return Success("保存成功");
        }
        /// <summary>
        /// 移除附件分片数据
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="chunks">总分片数</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RemoveAnnexesFileChunk(string fileGuid, int chunks)
        {
            _annexesFileIBLL.RemoveChunkAnnexes(fileGuid, chunks);
            return Success("移除成功");
        }
        /// <summary>
        /// 合并上传附件的分片数据(固定文件夹)
        /// </summary>
        /// <param name="folderId">附件夹主键</param>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="fileName">文件名</param>
        /// <param name="chunks">文件总分片数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> MergeAnnexesFile(string folderId, string fileGuid, string fileName, int chunks)
        {
            bool res =await _annexesFileIBLL.SaveAnnexes(folderId, fileGuid, fileName, chunks);
            if (res)
            {
                return Success("保存文件成功");

            }
            else
            {
                return Fail("保存文件失败");
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileId">文件主键</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteAnnexesFile(string fileId)
        {
            AnnexesFileEntity fileInfoEntity =await _annexesFileIBLL.GetEntity(fileId);
            await _annexesFileIBLL.DeleteEntity(fileId);
            //删除文件
            if (System.IO.File.Exists(fileInfoEntity.F_FilePath))
            {
                System.IO.File.Delete(fileInfoEntity.F_FilePath);
            }
            return Success("删除附件成功");
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns></returns>
        public async Task<IActionResult> DownAnnexesFile(string fileId)
        {
            var data =await _annexesFileIBLL.GetEntity(fileId);
            byte[] arr2 = FileHelper.Read(data.F_FilePath);
            return File(arr2, FileHelper.getContentType(data.F_FileType), data.F_FileName);
        }
        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <param name="folderId">附件夹主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAnnexesFileList(string folderId)
        {
            var data =await _annexesFileIBLL.GetList(folderId);
            return Success(data);
        }
        /// <summary>
        /// 获取附件夹信息
        /// </summary>
        /// <param name="folderId">附件夹主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFileNames(string folderId)
        {
            var data = await _annexesFileIBLL.GetFileNames(folderId);
            return Success(data);
        }
        #endregion

        #region 预览附件
        /// <summary>
        /// 文件预览
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PreviewFile(string fileId)
        {
            var data =await _annexesFileIBLL.GetEntity(fileId);
            byte[] content = new byte[0];
            string fileExt;
            switch (data.F_FileType)
            {
                case "xls":
                case "xlsx":
                    fileExt = "html";
                    if (DirFileHelper.IsExistFile(data.F_FilePath))
                    {
                        content = Encoding.Default.GetBytes(ce.office.extension.ExcelHelper.ToHtml(data.F_FilePath));
                    }
                    break;
                case "docx":
                    fileExt = "html";
                    if (DirFileHelper.IsExistFile(data.F_FilePath))
                    {
                        content = Encoding.Default.GetBytes(ce.office.extension.WordHelper.ToHtml(data.F_FilePath));
                    }
                    break;
                case "jpg":
                case "gif":
                case "png":
                case "bmp":
                case "jpeg":
                case "txt":
                case "csv":
                case "html":
                case "pdf":
                    fileExt = data.F_FileType;
                    content = FileHelper.Read(data.F_FilePath);
                    break;
                case "doc":
                case "ppt":
                case "pptx":
                    fileExt = "txt";
                    content = Encoding.Default.GetBytes("当前类型文件不支持预览！");
                    break;
                default:
                    fileExt = "txt";
                    content = Encoding.Default.GetBytes("当前类型文件不支持预览！");
                    break;
            }
            return File(content, FileHelper.getContentType(fileExt));
        }
        #endregion


    }
}