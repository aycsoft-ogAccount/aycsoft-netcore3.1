using aycsoft.iapplication;
using aycsoft.util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aycsoft.webapp.Areas.LR_NewWorkFlow.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.03.13
    /// 描 述：工作流模板(新)
    /// </summary>
    [Area("LR_NewWorkFlow")]
    public class NWFSchemeController : MvcControllerBase
    {
        private readonly NWFSchemeIBLL _nWFSchemeIBLL;
        private readonly AnnexesFileIBLL _annexesFileIBLL;
        private readonly UserRelationIBLL _userRelationIBLL;

        public NWFSchemeController(NWFSchemeIBLL nWFSchemeIBLL, AnnexesFileIBLL annexesFileIBLL, UserRelationIBLL userRelationIBLL) {
            _nWFSchemeIBLL = nWFSchemeIBLL;
            _annexesFileIBLL = annexesFileIBLL;
            _userRelationIBLL = userRelationIBLL;
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
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 流程模板设计历史记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult HistoryForm()
        {
            return View();
        }
        /// <summary>
        /// 预览流程模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PreviewForm()
        {
            return View();
        }
        /// <summary>
        /// 节点信息设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NodeForm()
        {
            return View();
        }

        #region 审核人员添加
        /// <summary>
        /// 添加岗位
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PostForm()
        {
            return View();
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RoleForm()
        {
            return View();
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UserForm()
        {
            return View();
        }
        /// <summary>
        /// 添加上下级
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LevelForm()
        {
            return View();
        }
        /// <summary>
        /// 添加某节点执行人
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AuditorNodeForm()
        {
            return View();
        }
        /// <summary>
        /// 添加表单字段
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AuditorFieldForm()
        {
            return View();
        }
        #endregion

        #region 表单添加
        /// <summary>
        /// 表单添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult WorkformForm()
        {
            return View();
        }
        #endregion

        #region 条件字段
        /// <summary>
        /// 条件字段添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ConditionFieldForm()
        {
            return View();
        }
        #endregion

        #region 按钮设置
        /// <summary>
        /// 表单添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ButtonForm()
        {
            return View();
        }
        #endregion

        /// <summary>
        /// 线段信息设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LineForm()
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
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetInfoPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = await _nWFSchemeIBLL.GetInfoPageList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records,
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取流程列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetALLInfoList()
        {
            var data =await _nWFSchemeIBLL.GetALLInfoList();
            return Success(data);
        }

        /// <summary>
        /// 获取自定义流程列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetInfoList(string userId)
        {
            var data =await _nWFSchemeIBLL.GetInfoList(userId);
            return Success(data);
        }
        /// <summary>
        /// 获取流程模板数据
        /// </summary>
        /// <param name="code">流程编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetFormData(string code)
        {
            var schemeInfoEntity =await _nWFSchemeIBLL.GetInfoEntityByCode(code);
            if (schemeInfoEntity == null)
            {
                return Success(new { });
            }

            var schemeEntity =await _nWFSchemeIBLL.GetSchemeEntity(schemeInfoEntity.F_SchemeId);
            var nWFSchemeAuthList =await _nWFSchemeIBLL.GetAuthList(schemeInfoEntity.F_Id);
            var jsonData = new
            {
                info = schemeInfoEntity,
                scheme = schemeEntity,
                authList = nWFSchemeAuthList
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取模板分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId">流程模板信息主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetSchemePageList(string pagination, string schemeInfoId)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data =await _nWFSchemeIBLL.GetSchemePageList(paginationobj, schemeInfoId);
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
        /// 获取流程模板数据
        /// </summary>
        /// <param name="schemeId">模板主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetScheme(string schemeId)
        {
            var data =await _nWFSchemeIBLL.GetSchemeEntity(schemeId);
            return Success(data);
        }

        /// <summary>
        /// 获取流程模板数据
        /// </summary>
        /// <param name="code">流程编码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ExportScheme(string code)
        {
            NWFSchemeInfoEntity schemeInfoEntity =await _nWFSchemeIBLL.GetInfoEntityByCode(code);
            if (schemeInfoEntity != null)
            {
                NWFSchemeEntity schemeEntity =await _nWFSchemeIBLL.GetSchemeEntity(schemeInfoEntity.F_SchemeId);
                var nWFSchemeAuthList =await _nWFSchemeIBLL.GetAuthList(schemeInfoEntity.F_Id);
                var jsonData = new
                {
                    info = schemeInfoEntity,
                    scheme = schemeEntity,
                    authList = nWFSchemeAuthList
                };

                string data = jsonData.ToJson();
                byte[] arr = Encoding.Default.GetBytes(data);
                return File(arr, "application/octet-stream", schemeInfoEntity.F_Name + ".lrscheme");
            }
            else {
                return File(Encoding.Default.GetBytes("{}"), "application/octet-stream", schemeInfoEntity.F_Name + ".lrscheme");
            }
        }

        /// <summary>
        /// 导入流程模板
        /// </summary>
        /// <param name="fileId">文件主键</param>
        /// <param name="chunks">分片数</param>
        /// <param name="ext">文件扩展名</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ExecuteImportScheme(string fileId, int chunks, string ext)
        {
            string path = _annexesFileIBLL.SaveAnnexes(fileId, fileId + "." + ext, chunks);
            if (!string.IsNullOrEmpty(path))
            {
                // 读取导入文件
                string data =System.IO.File.ReadAllText(path);
                // 删除临时文件
                System.IO.File.Delete(path);
                if (!string.IsNullOrEmpty(data))
                {
                    NWFSchemeModel nWFSchemeModel = data.ToObject<NWFSchemeModel>();
                    // 验证流程编码是否重复
                    NWFSchemeInfoEntity schemeInfoEntityTmp =await _nWFSchemeIBLL.GetInfoEntityByCode(nWFSchemeModel.info.F_Code);
                    if (schemeInfoEntityTmp != null)
                    {
                        nWFSchemeModel.info.F_Code = Guid.NewGuid().ToString();
                    }
                    await _nWFSchemeIBLL.SaveEntity("", nWFSchemeModel.info, nWFSchemeModel.scheme, nWFSchemeModel.authList);
                }
                return SuccessInfo("导入成功");
            }
            else
            {
                return Fail("导入模板失败!");
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存流程模板
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfo">表单设计模板信息</param>
        /// <param name="shcemeAuth">模板权限信息</param>
        /// <param name="scheme">模板内容</param>
        /// <param name="type">类型1.正式2.草稿</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> SaveForm(string keyValue, string schemeInfo, string shcemeAuth, string scheme, int type)
        {
            NWFSchemeInfoEntity schemeInfoEntity = schemeInfo.ToObject<NWFSchemeInfoEntity>();
            List<NWFSchemeAuthEntity> nWFSchemeAuthList = shcemeAuth.ToObject<List<NWFSchemeAuthEntity>>();
            NWFSchemeEntity schemeEntity = new NWFSchemeEntity();
            schemeEntity.F_Content = scheme;
            schemeEntity.F_Type = type;

            // 验证流程编码是否重复
            NWFSchemeInfoEntity schemeInfoEntityTmp =await _nWFSchemeIBLL.GetInfoEntityByCode(schemeInfoEntity.F_Code);
            if (schemeInfoEntityTmp != null && schemeInfoEntityTmp.F_Id != keyValue)
            {
                return Fail("流程编码重复");
            }
            await _nWFSchemeIBLL.SaveEntity(keyValue, schemeInfoEntity, schemeEntity, nWFSchemeAuthList);
            return SuccessInfo("保存成功！");
        }
        /// <summary>
        /// 删除模板数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            await _nWFSchemeIBLL.DeleteEntity(keyValue);
            return SuccessInfo("删除成功！");
        }

        /// <summary>
        /// 启用/停用表单
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态1启用0禁用</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> UpDateSate(string keyValue, int state)
        {
            await _nWFSchemeIBLL.UpdateState(keyValue, state);
            return SuccessInfo((state == 1 ? "启用" : "禁用") + "成功！");
        }
        /// <summary>
        /// 更新表单模板版本
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态1启用0禁用</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> UpdateScheme(string schemeInfoId, string schemeId)
        {
            await _nWFSchemeIBLL.UpdateScheme(schemeInfoId, schemeId);
            return SuccessInfo("更新成功！");
        }
        #endregion


    }
}