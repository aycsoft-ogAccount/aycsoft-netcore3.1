using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.25
    /// 描 述：Excel导出管理
    /// </summary>
    [Area("LR_SystemModule")]
    public class ExcelImportController : MvcControllerBase
    {
        private ExcelImportIBLL _excelImportIBLL;
        private AnnexesFileIBLL _annexesFileIBLL;

        public ExcelImportController(ExcelImportIBLL excelImportIBLL, AnnexesFileIBLL annexesFileIBLL)
        {
            _excelImportIBLL = excelImportIBLL;
            _annexesFileIBLL = annexesFileIBLL;
        }

        #region 视图功能
        /// <summary>
        /// 导入模板管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 导入模板管理表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 设置字段属性
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SetFieldForm()
        {
            return View();
        }

        /// <summary>
        /// 导入页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ImportForm()
        {
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = await _excelImportIBLL.GetPageList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                paginationobj.total,
                paginationobj.page,
                paginationobj.records,
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="moduleId">功能模块主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetList(string moduleId)
        {
            var data = await _excelImportIBLL.GetList(moduleId);
            return Success(data);
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
            var entity = await _excelImportIBLL.GetEntity(keyValue);
            var list = await _excelImportIBLL.GetFieldList(keyValue);
            var data = new
            {
                entity,
                list
            };
            return Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="strEntity">实体</param>
        /// <param name="strList">实体</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, string strEntity, string strList)
        {
            var entity = strEntity.ToObject<ExcelImportEntity>();
            var filedList = strList.ToObject<List<ExcelImportFieldEntity>>();
            await _excelImportIBLL.SaveEntity(keyValue, entity, filedList);
            return SuccessInfo("保存成功！");
        }
        /// <summary>
        /// 删除表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await _excelImportIBLL.DeleteEntity(keyValue);
            return SuccessInfo("删除成功！");
        }
        /// <summary>
        /// 更新表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> UpdateForm(string keyValue, ExcelImportEntity entity)
        {
            await _excelImportIBLL.UpdateEntity(keyValue, entity);
            return SuccessInfo("操作成功！");
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="keyValue">文件id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DownSchemeFile(string keyValue)
        {
            var templateInfo = await _excelImportIBLL.GetEntity(keyValue);
            var fileds = await _excelImportIBLL.GetFieldList(keyValue);

            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.FileName = templateInfo.F_Name + ".xls";
            excelconfig.IsAllSizeColumn = true;
            excelconfig.ColumnEntity = new List<ColumnModel>();
            //表头
            DataTable dt = new DataTable();
            foreach (var col in fileds)
            {
                if (col.F_RelationType != 1 && col.F_RelationType != 4 && col.F_RelationType != 5 && col.F_RelationType != 6 && col.F_RelationType != 7)
                {
                    excelconfig.ColumnEntity.Add(new ColumnModel()
                    {
                        Column = col.F_Name,
                        ExcelColumn = col.F_ColName,
                        Alignment = "center",
                    });
                    dt.Columns.Add(col.F_Name, typeof(string));
                }
            }

            return File(ExcelHelper.ExportMemoryStream(dt, excelconfig), "application/ms-excel", excelconfig.FileName);
        }

        /// <summary>
        /// excel文件导入（通用）
        /// </summary>
        /// <param name="templateId">模板Id</param>
        /// <param name="fileId">文件主键</param>
        /// <param name="chunks">分片数</param>
        /// <param name="ext">文件扩展名</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ExecuteImportExcel(string templateId, string fileId, int chunks, string ext)
        {
            string path = _annexesFileIBLL.SaveAnnexes(fileId, fileId + "." + ext, chunks);
            if (!string.IsNullOrEmpty(path))
            {
                DataTable dt = ExcelHelper.ExcelImport(path);
                var res = await _excelImportIBLL.ImportTable(templateId, fileId, dt);
                var data = new
                {
                    Success = res.snum,
                    Fail = res.fnum,
                    data = res.elist
                };
                System.IO.File.Delete(path);
                return Success(data);
            }
            else
            {
                return Fail("导入数据失败!");
            }
        }
        #endregion
    }
}