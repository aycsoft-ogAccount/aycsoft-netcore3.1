using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aycsoft.iapplication;
using aycsoft.operat;
using aycsoft.util;
using aycsoft.wechat;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aycsoft.webapp.Areas.LR_WebChatModule.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.12
    /// 描 述：企业号部门同步
    /// </summary>
    [Area("LR_WebChatModule")]
    public class OrganizeController : MvcControllerBase
    {
        private readonly UserIBLL _userIBLL;
        private readonly DepartmentIBLL _departmentIBLL;
        private readonly CompanyIBLL _companyIBLL;
        private readonly LogIBLL _logIBLL;


        public OrganizeController(UserIBLL userIBLL, DepartmentIBLL departmentIBLL, CompanyIBLL companyIBLL, LogIBLL logIBLL)
        {
            _userIBLL = userIBLL;
            _departmentIBLL = departmentIBLL;
            _companyIBLL = companyIBLL;
            _logIBLL = logIBLL;
        }

        #region 视图功能
        /// <summary>
        /// 同步部门主界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 同步员工
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult MemberForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> GetTreeList(string keyword)
        {
            //获取微信部门数据
            DepartmentList wxdepartmentList = new DepartmentList();
            var list = await wxdepartmentList.Send();
            if (list.errcode != 0)
            {
                return Fail("微信接口错误码" + list.errcode + ",错误信息" + list.errmsg);
            }
            else
            {
                //转换成dic数据
                Dictionary<string, string> dir = new Dictionary<string, string>();
                foreach (var item in list.department)
                {
                    dir.Add(item.id, item.name);
                }
                List<WxDepartmentItem> res = new List<WxDepartmentItem>();
                //获取内部系统公司部门列表
                var companyList = await _companyIBLL.GetList();
                foreach (var item in companyList)
                {
                    WxDepartmentItem _ditem = new WxDepartmentItem
                    {
                        id = item.F_CompanyId,
                        code = item.F_EnCode,
                        name = item.F_FullName,
                        parentid = item.F_ParentId,
                        isSyn = 0
                    };

                    if (dir.ContainsKey(_ditem.code) && dir[_ditem.code] == _ditem.name)
                    {
                        _ditem.isSyn = 1;
                    }
                    res.Add(_ditem);
                    var departmentList = await _departmentIBLL.GetList(item.F_CompanyId);

                    foreach (var mditem in departmentList)
                    {
                        WxDepartmentItem _dditem = new WxDepartmentItem
                        {
                            id = mditem.F_DepartmentId,
                            code = mditem.F_EnCode,
                            name = mditem.F_FullName,
                            parentid = mditem.F_ParentId,
                            isSyn = 0
                        };
                        if (mditem.F_ParentId == "0")
                        {
                            _dditem.parentid = mditem.F_CompanyId;
                        }
                        if (dir.ContainsKey(_dditem.code) && dir[_dditem.code] == _dditem.name)
                        {
                            _dditem.isSyn = 1;
                        }
                        res.Add(_dditem);
                    }

                }

                if (!string.IsNullOrEmpty(keyword))
                {
                    res = res.FindAll(t => t.name.IndexOf(keyword, StringComparison.Ordinal) != -1);
                }

                return Success(res);
            }
        }

        /// <summary>
        /// 获取微信人员同步右侧同步信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="companyId"></param>
        /// <param name="departmentId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetUserPageList(string pagination, string keyword, string companyId, string departmentId, string code)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            //获取内部系统人员列表
            var data = await _userIBLL.GetPageList(companyId, departmentId, paginationobj, keyword);
            //获取微信员工列表
            UserSimplelist wxuserSimplelist = new UserSimplelist
            {
                department_id = code,
                fetch_child = 1,
                status = UserSimplelist.UserStatus.All
            };
            var wxData = await wxuserSimplelist.Send();
            if (wxData.errcode != 0)
            {
                return Fail("微信接口错误码" + wxData.errcode + ",错误信息" + wxData.errmsg);
            }
            #region 判断数据是否相同
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var i in wxData.userlist)
            {
                dic.Add(i.userid, i.name);
            }
            foreach (var item in data)
            {
                if (dic.ContainsKey(item.F_Account))
                {
                    if (item.F_RealName != dic[item.F_Account])
                    {
                        item.F_AnswerQuestion = "未同步";
                    }
                    else
                    {
                        item.F_AnswerQuestion = "已同步";
                    }
                }
                else
                {
                    item.F_AnswerQuestion = "未同步";
                }
            }
            #endregion
            var jsonData = new
            {
                rows = data,
                paginationobj.total,
                paginationobj.page,
                paginationobj.records,
            };
            return Success(jsonData);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 同步部门
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SyncDepartment()
        {
            //获取微信部门数据
            DepartmentList wxdepartmentList = new DepartmentList();
            var list = await wxdepartmentList.Send();
            if (list.errcode != 0)
            {
                return Fail("微信接口错误码" + list.errcode + ",错误信息" + list.errmsg);
            }
            else
            {
                Dictionary<string, DepartmentListResult.DepartmentItem> dir = new Dictionary<string, DepartmentListResult.DepartmentItem>();
                foreach (var item in list.department)
                {
                    dir.Add(item.id, item);
                }

                var userInfo = await this.CurrentUser();

                //获取内部系统公司部门列表
                var companyList = (List<CompanyEntity>)await _companyIBLL.GetList();
                foreach (var item in companyList)
                {
                    string parentid = "1";
                    var parentEntity = companyList.Find(i => i.F_CompanyId == item.F_ParentId);
                    if (parentEntity != null)
                    {
                        parentid = parentEntity.F_EnCode;
                    }
                    if (dir.ContainsKey(item.F_EnCode))
                    {
                        var wxitem = dir[item.F_EnCode];
                        if (wxitem.name != item.F_FullName || wxitem.parentid != parentid)
                        {
                            DepartmentUpdate departmentUpdate = new DepartmentUpdate()
                            {
                                id = item.F_EnCode,
                                name = item.F_FullName,
                                parentid = parentid
                            };
                            var res = await departmentUpdate.Send();

                            if (res.errcode != 0)
                            {
                                LogEntity logEntity = new LogEntity();
                                logEntity.F_CategoryId = 4;
                                logEntity.F_OperateTypeId = ((int)OperationType.Exception).ToString();
                                logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Exception);
                                logEntity.F_OperateAccount = userInfo.F_Account;
                                logEntity.F_OperateUserId = userInfo.F_UserId;
                                logEntity.F_ExecuteResult = -1;
                                logEntity.F_ExecuteResultJson = "微信接口错误码" + res.errcode + ",错误信息" + res.errmsg;
                                await _logIBLL.Write(logEntity);
                            }
                        }
                    }
                    else
                    {
                        DepartmentCreate departmentCreate = new DepartmentCreate()
                        {
                            id = item.F_EnCode,
                            name = item.F_FullName,
                            parentid = parentid
                        };
                        var res = await departmentCreate.Send();

                        if (res.errcode != 0)
                        {
                            LogEntity logEntity = new LogEntity();
                            logEntity.F_CategoryId = 4;
                            logEntity.F_OperateTypeId = ((int)OperationType.Exception).ToString();
                            logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Exception);
                            logEntity.F_OperateAccount = userInfo.F_Account;
                            logEntity.F_OperateUserId = userInfo.F_UserId;
                            logEntity.F_ExecuteResult = -1;
                            logEntity.F_ExecuteResultJson = "微信接口错误码" + res.errcode + ",错误信息" + res.errmsg;
                            await _logIBLL.Write(logEntity);
                        }

                    }

                    var departmentList = (List<DepartmentEntity>)await _departmentIBLL.GetList(item.F_CompanyId);

                    foreach (var mditem in departmentList)
                    {
                        string dparentid = "1";
                        var dparentEntity = departmentList.Find(i => i.F_DepartmentId == item.F_ParentId);
                        if (dparentEntity != null)
                        {
                            dparentid = parentEntity.F_EnCode;
                        }

                        if (dir.ContainsKey(mditem.F_EnCode))
                        {
                            var wxitem = dir[mditem.F_EnCode];
                            if (wxitem.name != mditem.F_FullName || wxitem.parentid != parentid)
                            {
                                DepartmentUpdate departmentUpdate = new DepartmentUpdate()
                                {
                                    id = mditem.F_EnCode,
                                    name = mditem.F_FullName,
                                    parentid = parentid
                                };
                                var res = await departmentUpdate.Send();

                                if (res.errcode != 0)
                                {
                                    LogEntity logEntity = new LogEntity();
                                    logEntity.F_CategoryId = 4;
                                    logEntity.F_OperateTypeId = ((int)OperationType.Exception).ToString();
                                    logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Exception);
                                    logEntity.F_OperateAccount = userInfo.F_Account;
                                    logEntity.F_OperateUserId = userInfo.F_UserId;
                                    logEntity.F_ExecuteResult = -1;
                                    logEntity.F_ExecuteResultJson = "微信接口错误码" + res.errcode + ",错误信息" + res.errmsg;
                                    await _logIBLL.Write(logEntity);
                                }
                            }
                        }
                        else
                        {
                            DepartmentCreate departmentCreate = new DepartmentCreate()
                            {
                                id = mditem.F_EnCode,
                                name = mditem.F_FullName,
                                parentid = parentid
                            };
                            var res = await departmentCreate.Send();

                            if (res.errcode != 0)
                            {
                                LogEntity logEntity = new LogEntity();
                                logEntity.F_CategoryId = 4;
                                logEntity.F_OperateTypeId = ((int)OperationType.Exception).ToString();
                                logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Exception);
                                logEntity.F_OperateAccount = userInfo.F_Account;
                                logEntity.F_OperateUserId = userInfo.F_UserId;
                                logEntity.F_ExecuteResult = -1;
                                logEntity.F_ExecuteResultJson = "微信接口错误码" + res.errcode + ",错误信息" + res.errmsg;
                                await _logIBLL.Write(logEntity);
                            }

                        }
                    }

                }



                return SuccessInfo("同步成功");
            }
        }


        /// <summary>
        /// 同步员工
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> SyncMember(string userId)
        {
            var userEntity = await _userIBLL.GetEntity(userId);

            UserGet wxuerGet = new UserGet();
            wxuerGet.userid = userEntity.F_Account;
            var res = await wxuerGet.Send();
            var userInfo = await this.CurrentUser();
            if (res.errcode == 0)
            {
                UserUpdate wxUserUpdate = new UserUpdate
                {
                    name = userEntity.F_RealName,
                    mobile = userEntity.F_Mobile,
                    userid = userEntity.F_Account,
                    enable = 1
                };
                if (userEntity.F_DepartmentId != null)
                {
                    var departmentEntity = await _departmentIBLL.GetEntity(userEntity.F_DepartmentId);
                    wxUserUpdate.department = new List<string> { departmentEntity == null ? "0" : departmentEntity.F_EnCode };
                }
                else
                {
                    var companyEntity = await _companyIBLL.GetEntity(userEntity.F_CompanyId);
                    wxUserUpdate.department = new List<string> { companyEntity == null ? "0" : companyEntity.F_EnCode };
                }

                var upres = await wxUserUpdate.Send();

                if (upres.errcode != 0)
                {
                    LogEntity logEntity = new LogEntity();
                    logEntity.F_CategoryId = 4;
                    logEntity.F_OperateTypeId = ((int)OperationType.Exception).ToString();
                    logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Exception);
                    logEntity.F_OperateAccount = userInfo.F_Account;
                    logEntity.F_OperateUserId = userInfo.F_UserId;
                    logEntity.F_ExecuteResult = -1;
                    logEntity.F_ExecuteResultJson = "微信接口错误码" + upres.errcode + ",错误信息" + upres.errmsg;
                    await _logIBLL.Write(logEntity);
                    return Fail("同步失败,请在系统日志查看原因");
                }

            }
            else if (res.errcode == 60111)
            {
                UserCreate wxUserCreate = new UserCreate()
                {
                    name = userEntity.F_RealName,
                    mobile = userEntity.F_Mobile,
                    userid = userEntity.F_Account
                };

                if (userEntity.F_DepartmentId != null)
                {
                    var departmentEntity = await _departmentIBLL.GetEntity(userEntity.F_DepartmentId);
                    wxUserCreate.department = new List<string> { departmentEntity == null ? "0" : departmentEntity.F_EnCode };
                }
                else
                {
                    var companyEntity = await _companyIBLL.GetEntity(userEntity.F_CompanyId);
                    wxUserCreate.department = new List<string> { companyEntity == null ? "0" : companyEntity.F_EnCode };
                }

                var crres = await wxUserCreate.Send();

                if (crres.errcode != 0)
                {
                    LogEntity logEntity = new LogEntity();
                    logEntity.F_CategoryId = 4;
                    logEntity.F_OperateTypeId = ((int)OperationType.Exception).ToString();
                    logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Exception);
                    logEntity.F_OperateAccount = userInfo.F_Account;
                    logEntity.F_OperateUserId = userInfo.F_UserId;
                    logEntity.F_ExecuteResult = -1;
                    logEntity.F_ExecuteResultJson = "微信接口错误码" + crres.errcode + ",错误信息" + crres.errmsg;
                    await _logIBLL.Write(logEntity);
                    return Fail("同步失败,请在系统日志查看原因");
                }

            }
            else
            {
                LogEntity logEntity = new LogEntity();
                logEntity.F_CategoryId = 4;
                logEntity.F_OperateTypeId = ((int)OperationType.Exception).ToString();
                logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Exception);
                logEntity.F_OperateAccount = userInfo.F_Account;
                logEntity.F_OperateUserId = userInfo.F_UserId;
                logEntity.F_ExecuteResult = -1;
                logEntity.F_ExecuteResultJson = "微信接口错误码" + res.errcode + ",错误信息" + res.errmsg;
                await _logIBLL.Write(logEntity);

                return Fail("同步失败,请在系统日志查看原因");
            }
            return Success(res);

        }

        #endregion



        public class WxDepartmentItem
        {
            /// <summary>
            /// 部门id
            /// </summary>
            /// <returns></returns>
            public string id { get; set; }
            /// <summary>
            /// 部门编码
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 部门名称
            /// </summary>
            /// <returns></returns>
            public string name { get; set; }

            /// <summary>
            /// 父亲部门id。根部门为1
            /// </summary>
            /// <returns></returns>
            public string parentid { get; set; }
            /// <summary>
            /// 是否同步 1 是 0 不是
            /// </summary>

            public int isSyn { get; set; }
        }
    }
}
