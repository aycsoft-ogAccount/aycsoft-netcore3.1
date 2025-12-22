using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;

namespace aycsoft.webapp.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.26
    /// 描 述：通用控制器,处理通用的接口
    /// </summary>
    public class UtilityController : MvcControllerBase
    {

        #region 百度编辑器的后端接口
        /// <summary>
        /// 百度编辑器的后端接口
        /// </summary>
        /// <param name="action">执行动作</param>
        /// <returns></returns>
        public IActionResult UEditor()
        {
            string action = Request.Query["action"];

            switch (action)
            {
                case "config":
                    return Content(UeditorConfig.Items.ToJson());
                case "uploadimage":
                    return UEditorUpload(new UeditorUploadConfig()
                    {
                        AllowExtensions = UeditorConfig.GetStringList("imageAllowFiles"),
                        PathFormat = UeditorConfig.GetString("imagePathFormat"),
                        SizeLimit = UeditorConfig.GetInt("imageMaxSize"),
                        UploadFieldName = UeditorConfig.GetString("imageFieldName")
                    });
                case "uploadscrawl":
                    return UEditorUpload(new UeditorUploadConfig()
                    {
                        AllowExtensions = new string[] { ".png" },
                        PathFormat = UeditorConfig.GetString("scrawlPathFormat"),
                        SizeLimit = UeditorConfig.GetInt("scrawlMaxSize"),
                        UploadFieldName = UeditorConfig.GetString("scrawlFieldName"),
                        Base64 = true,
                        Base64Filename = "scrawl.png"
                    });
                case "uploadvideo":
                    return UEditorUpload(new UeditorUploadConfig()
                    {
                        AllowExtensions = UeditorConfig.GetStringList("videoAllowFiles"),
                        PathFormat = UeditorConfig.GetString("videoPathFormat"),
                        SizeLimit = UeditorConfig.GetInt("videoMaxSize"),
                        UploadFieldName = UeditorConfig.GetString("videoFieldName")
                    });
                case "uploadfile":
                    return UEditorUpload(new UeditorUploadConfig()
                    {
                        AllowExtensions = UeditorConfig.GetStringList("fileAllowFiles"),
                        PathFormat = UeditorConfig.GetString("filePathFormat"),
                        SizeLimit = UeditorConfig.GetInt("fileMaxSize"),
                        UploadFieldName = UeditorConfig.GetString("fileFieldName")
                    });
                case "listimage":
                    return ListFileManager(UeditorConfig.GetString("imageManagerListPath"), UeditorConfig.GetStringList("imageManagerAllowFiles"));
                case "listfile":
                    return ListFileManager(UeditorConfig.GetString("fileManagerListPath"), UeditorConfig.GetStringList("fileManagerAllowFiles"));
                case "catchimage":
                    return CrawlerHandler();
                default:
                    return Content(new
                    {
                        state = "action 参数为空或者 action 不被支持。"
                    }.ToJson());
            }
        }
        /// <summary>
        /// 百度编辑器的文件上传
        /// </summary>
        /// <param name="uploadConfig">上传配置信息</param>
        /// <returns></returns>
        public IActionResult UEditorUpload(UeditorUploadConfig uploadConfig)
        {
            UeditorUploadResult result = new UeditorUploadResult() { State = UeditorUploadState.Unknown };

            byte[] uploadFileBytes = null;
            string uploadFileName = null;

            if (uploadConfig.Base64)
            {
                uploadFileName = uploadConfig.Base64Filename;
                uploadFileBytes = Convert.FromBase64String(Request.Form[uploadConfig.UploadFieldName]);
            }
            else
            {
                var file = Request.Form.Files[uploadConfig.UploadFieldName];
                uploadFileName = file.FileName;

                if (!CheckFileType(uploadConfig, uploadFileName))
                {
                    return Content(new
                    {
                        state = GetStateMessage(UeditorUploadState.TypeNotAllow)
                    }.ToJson());
                }
                if (!CheckFileSize(uploadConfig, (int)file.Length))
                {
                    return Content(new
                    {
                        state = GetStateMessage(UeditorUploadState.SizeLimitExceed)
                    }.ToJson());
                }

                uploadFileBytes = new byte[file.Length];
                try
                {
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    ms.Flush();
                    ms.Position = 0;
                    ms.Read(uploadFileBytes, 0, uploadFileBytes.Length);
                    ms.Dispose();


                    //file.Read(uploadFileBytes, 0, file.Length);
                }
                catch (Exception)
                {
                    return Content(new
                    {
                        state = GetStateMessage(UeditorUploadState.NetworkError)
                    }.ToJson());
                }
            }

            result.OriginFileName = uploadFileName;

            var savePath = UeditorPathFormatter.Format(uploadFileName, uploadConfig.PathFormat);
            var localPath = ConfigHelper.GetValue<string>("baseDir") + "\\wwwroot\\ueditor\\" + savePath; ///Server.MapPath(savePath).Replace("\\Utility\\", "\\ueditor\\");// +"/ueditor/net";
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(localPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                }
                System.IO.File.WriteAllBytes(localPath, uploadFileBytes);
                result.Url = savePath;
                result.State = UeditorUploadState.Success;
            }
            catch (Exception e)
            {
                result.State = UeditorUploadState.FileAccessError;
                result.ErrorMessage = e.Message;
            }

            return Content(new
            {
                state = GetStateMessage(result.State),
                url = result.Url,
                title = result.OriginFileName,
                original = result.OriginFileName,
                error = result.ErrorMessage
            }.ToJson());
        }
        /// <summary>
        /// 百度编辑器的文件列表管理
        /// </summary>
        /// <param name="pathToList">文件列表目录</param>
        /// <param name="searchExtensions">扩展名</param>
        /// <returns></returns>
        public ActionResult ListFileManager(string pathToList, string[] searchExtensions)
        {
            int Start;
            int Size;
            int Total;
            String[] FileList;
            String[] SearchExtensions;
            SearchExtensions = searchExtensions.Select(x => x.ToLower()).ToArray();
            try
            {
                Start = String.IsNullOrEmpty(Request.Query["start"]) ? 0 : Convert.ToInt32(Request.Query["start"]);
                Size = String.IsNullOrEmpty(Request.Query["size"]) ? UeditorConfig.GetInt("imageManagerListSize") : Convert.ToInt32(Request.Query["size"]);
            }
            catch (FormatException)
            {
                return Content(new
                {
                    state = "参数不正确",
                    start = 0,
                    size = 0,
                    total = 0
                }.ToJson());
            }
            var buildingList = new List<String>();
            try
            {
                var localPath = ConfigHelper.GetValue<string>("baseDir") + "\\wwwroot\\ueditor\\" + pathToList; //Server.MapPath(pathToList).Replace("\\Utility\\", "\\ueditor\\");
                buildingList.AddRange(Directory.GetFiles(localPath, "*", SearchOption.AllDirectories)
                    .Where(x => SearchExtensions.Contains(Path.GetExtension(x).ToLower()))
                    .Select(x => pathToList + x.Substring(localPath.Length).Replace("\\", "/")));
                Total = buildingList.Count;
                FileList = buildingList.OrderBy(x => x).Skip(Start).Take(Size).ToArray();
            }
            catch (UnauthorizedAccessException)
            {
                return Content(new
                {
                    state = "文件系统权限不足",
                    start = 0,
                    size = 0,
                    total = 0
                }.ToJson());
            }
            catch (DirectoryNotFoundException)
            {
                return Content(new
                {
                    state = "路径不存在",
                    start = 0,
                    size = 0,
                    total = 0
                }.ToJson());
            }
            catch (IOException)
            {
                return Content(new
                {
                    state = "文件系统读取错误",
                    start = 0,
                    size = 0,
                    total = 0
                }.ToJson());
            }

            return Content(new
            {
                state = "SUCCESS",
                list = FileList == null ? null : FileList.Select(x => new { url = x }),
                start = Start,
                size = Size,
                total = Total
            }.ToJson());
        }

        public ActionResult CrawlerHandler()
        {
            string s = "c";
            string[] sources = s.Split(",");
            if (sources == null || sources.Length == 0)
            {
                return Content(new
                {
                    state = "参数错误：没有指定抓取源"
                }.ToJson());
            }

            UeditorCrawler[] crawlers = sources.Select(x => new UeditorCrawler(x).Fetch()).ToArray();
            return Content(new
            {
                state = "SUCCESS",
                list = crawlers.Select(x => new
                {
                    state = x.State,
                    source = x.SourceUrl,
                    url = x.ServerUrl
                })
            }.ToJson());
        }
        private string GetStateMessage(UeditorUploadState state)
        {
            switch (state)
            {
                case UeditorUploadState.Success:
                    return "SUCCESS";
                case UeditorUploadState.FileAccessError:
                    return "文件访问出错，请检查写入权限";
                case UeditorUploadState.SizeLimitExceed:
                    return "文件大小超出服务器限制";
                case UeditorUploadState.TypeNotAllow:
                    return "不允许的文件格式";
                case UeditorUploadState.NetworkError:
                    return "网络错误";
            }
            return "未知错误";
        }
        /// <summary>
        /// 检测是否符合上传文件格式
        /// </summary>
        /// <param name="uploadConfig">配置信息</param>
        /// <param name="filename">文件名字</param>
        /// <returns></returns>
        private bool CheckFileType(UeditorUploadConfig uploadConfig, string filename)
        {
            var fileExtension = Path.GetExtension(filename).ToLower();
            var res = false;
            foreach (var item in uploadConfig.AllowExtensions)
            {
                if (item == fileExtension)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }
        /// <summary>
        /// 检测是否符合上传文件大小
        /// </summary>
        /// <param name="uploadConfig">配置信息</param>
        /// <param name="size">文件大小</param>
        /// <returns></returns>
        private bool CheckFileSize(UeditorUploadConfig uploadConfig, int size)
        {
            return size < uploadConfig.SizeLimit;
        }


        #endregion

        #region 选择图标
        /// <summary>
        /// 图标的选择
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Icon()
        {
            return View();
        }
        /// <summary>
        /// 移动图标的选择
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AppIcon()
        {
            return View();
        }
        #endregion

        #region 开发向导
        /// <summary>
        /// pc端开发向导
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PCDevGuideIndex()
        {
            return View();
        }

        /// <summary>
        /// 移动端开发向导
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AppDevGuideIndex()
        {
            return View();
        }
        #endregion

        #region 导出数据
        /// <summary>
        /// 请选择要导出的字段页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ExcelExportForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ExportExcel(string fileName, string columnJson, string dataJson, string exportField)
        {
            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.Title = HttpUtility.UrlDecode(fileName);
            excelconfig.TitleFont = "微软雅黑";
            excelconfig.TitlePoint = 15;
            excelconfig.FileName = HttpUtility.UrlDecode(fileName) + ".xls";
            excelconfig.IsAllSizeColumn = true;
            excelconfig.ColumnEntity = new List<ColumnModel>();
            //表头
            List<JfGridModel> columnList = columnJson.ToList<JfGridModel>();
            //行数据
            DataTable rowData = dataJson.ToTable();
            //写入Excel表头
            Dictionary<string, string> exportFieldMap = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(exportField))
            {
                string[] exportFields = exportField.Split(',');
                foreach (var field in exportFields)
                {
                    if (!exportFieldMap.ContainsKey(field))
                    {
                        exportFieldMap.Add(field, "1");
                    }
                }
            }

            foreach (var columnModel in columnList)
            {
                if (exportFieldMap.ContainsKey(columnModel.name) || string.IsNullOrEmpty(exportField))
                {
                    excelconfig.ColumnEntity.Add(new ColumnModel()
                    {
                        Column = columnModel.name,
                        ExcelColumn = columnModel.label,
                        Alignment = columnModel.align,
                        Width = columnModel.name.Length
                    });
                }
            }
            return File(ExcelHelper.ExportMemoryStream(rowData, excelconfig), "application/ms-excel", excelconfig.FileName);
        }
        #endregion

        #region 自定义表单
        /// <summary>
        /// 表单预览
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PreviewForm()
        {
            return View();
        }
        /// <summary>
        /// 编辑表格
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditGridForm()
        {
            return View();
        }

        #endregion

        #region jfgrid弹层选择
        /// <summary>
        /// 列表选择弹层
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult JfGirdLayerForm()
        {
            return View();
        }
        #endregion

        #region 列表选择弹层
        /// <summary>
        /// 列表选择弹层
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GirdSelectIndex()
        {
            return View();
        }
        #endregion

        #region 树形选择弹层
        /// <summary>
        /// 列表选择弹层
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TreeSelectIndex()
        {
            return View();
        }
        #endregion

        #region 桌面消息列表详情查看
        /// <summary>
        /// 桌面消息列表详情查看
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ListContentIndex()
        {
            return View();
        }
        #endregion
    }
}