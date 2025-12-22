using aycsoft.iapplication;
using aycsoft.website.Models;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace aycsoft.website.Controllers
{
    public class HomeController : MvcControllerBase
    {
        private readonly HomeConfigIBLL _homeConfigIBLL;
        private readonly ArticleIBLL _articleIBLL;
        private readonly PageIBLL _pageIBLL;
        private readonly ImgIBLL _imgIBLL;
        public HomeController(HomeConfigIBLL homeConfigIBLL, ArticleIBLL articleIBLL, PageIBLL pageIBLL, ImgIBLL imgIBLL)
        {
            _homeConfigIBLL = homeConfigIBLL;
            _articleIBLL = articleIBLL;
            _pageIBLL = pageIBLL;
            _imgIBLL = imgIBLL;
        }


        #region 视图功能
        /// <summary>
        /// 首页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 子页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChildIndex()
        {
            return View();
        }

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ListIndex()
        {
            return View();
        }
        /// <summary>
        /// 图表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ImgListIndex()
        {
            return View();
        }

        /// <summary>
        /// 详情页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DetailIndex()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取全部数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllList()
        {
            var data =await _homeConfigIBLL.GetALLList();
            return Success(data);
        }
        /// <summary>
        /// 获取数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList(string type)
        {
            var data =await _homeConfigIBLL.GetList(type);
            return Success(data);
        }
        /// <summary>
        /// 获取表单数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPageData(string keyValue)
        {
            var data = await _pageIBLL.GetEntity(keyValue);
            return Success(data);
        }
        #endregion

        #region 扩展功能
        /// <summary>
        /// 获取图片文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetImg(string type)
        {
            var entity = await _homeConfigIBLL.GetEntityByType(type);
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


        /// <summary>
        /// 获取设置图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetArticleImg(string keyValue)
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
        /// <summary>
        /// 获取表单数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetArticle(string keyValue)
        {
            var data =await _articleIBLL.GetEntity(keyValue);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetArticlePageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data =await _articleIBLL.GetPageList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取设置图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPageImg(string keyValue)
        {
            var entity = await _pageIBLL.GetEntity(keyValue);
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
                    byte[] arr2 = FileHelper.ReadRoot("/img/plhome/添加图片.jpg");
                    return File(arr2, "application/octet-stream");
                }
            }
            else
            {
                byte[] arr3 = FileHelper.ReadRoot("/img/plhome/添加图片.jpg");
                return File(arr3, "application/octet-stream");
            }

        }
        /// <summary>
        /// 获取设置图片
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUeditorImg(string id)
        {
            string path = ConfigHelper.GetConfig().UeditorImg + "/ueditor/upload/image" + id;
            path = System.Text.RegularExpressions.Regex.Replace(path, @"\s", "");
            byte[] arr3 = System.IO.File.ReadAllBytes(path);
            return File(arr3, "application/octet-stream");
        }
        #endregion








        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
