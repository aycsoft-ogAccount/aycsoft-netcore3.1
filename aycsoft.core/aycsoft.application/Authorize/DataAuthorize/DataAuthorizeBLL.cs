using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core 力软敏捷开发框架
    /// Copyright (c) 2021-present 力软信息技术（苏州）有限公司
    /// 创建人：young
    /// 日 期：2022.10.25
    /// 描 述：数据权限
    /// </summary>
    public class DataAuthorizeBLL : DataAuthorizeIBLL, BLL
    {
        private readonly DataAuthorizeService service = new DataAuthorizeService();

        private readonly UserIBLL _userIBLL;
        private readonly UserRelationIBLL _userRelationIBLL;
        private readonly CompanyIBLL _companyIBLL;
        private readonly DepartmentIBLL _departmentIBLL;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIBLL"></param>
        /// <param name="userRelationIBLL"></param>
        /// <param name="companyIBLL"></param>
        /// <param name="departmentIBLL"></param>
        public DataAuthorizeBLL(UserIBLL userIBLL, UserRelationIBLL userRelationIBLL, CompanyIBLL companyIBLL, DepartmentIBLL departmentIBLL)
        {
            _userIBLL = userIBLL;
            _userRelationIBLL = userRelationIBLL;
            _companyIBLL = companyIBLL;
            _departmentIBLL = departmentIBLL;
        }


        #region 获取数据
        /// <summary>
        /// 获取数据权限对应关系数据列表
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="objectId">用户或角色主键</param>
        /// <returns></returns>
        public Task<IEnumerable<DataAuthorizeEntity>> GetList(string code, string objectId)
        {
            return service.GetList(code, objectId);
        }
        /// <summary>
        /// 获取数据权限列表（分页）
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">查询关键词</param>
        /// <param name="objectId">用户或角色主键</param>
        /// <param name="type">1.普通权限 2.自定义表单权限</param>
        /// <returns></returns>
        public Task<IEnumerable<DataAuthorizeEntity>> GetPageList(Pagination pagination, string keyword, string objectId, int type)
        {
            return service.GetPageList(pagination, keyword, objectId, type);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<DataAuthorizeEntity> GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public async Task DeleteEntity(string keyValue)
        {
            await service.DeleteEntity(keyValue);
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, DataAuthorizeEntity entity)
        {
            await service.SaveEntity(keyValue, entity);
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public async Task<string> GetWhereSql(string code)
        {
            // 登录者用户ID，公司ID，部门ID,公司及下属公司，部门及下属部门，登录者账号，岗位，角色
            var userInfo = await _userIBLL.GetEntity();
            string companyIds = null;
            string departmentIds = null;
            string roleIds = "'" + await _userRelationIBLL.GetObjectIds(userInfo.F_UserId, 1) + "'";
            string postIds = null;

            if (userInfo.F_SecurityLevel == 1)
            {
                return "";
            }

            string objectId = userInfo.F_UserId;
            if (roleIds != "''")
            {
                objectId +="," + roleIds;
            }
            var list = await service.GetList(code, objectId);
            var strRes = new StringBuilder();

            int num = 0;
            foreach (var item in list)
            {
                if (num > 0)
                {
                    strRes.Append(" OR ( ");
                }
                else
                {
                    strRes.Append(" ( ");
                }
                string strFormula = item.F_Formula.Replace("@userId", "'" + userInfo.F_UserId + "'");
                strFormula = item.F_Formula.Replace("@account", "'" + userInfo.F_Account + "'");
                strFormula = item.F_Formula.Replace("@companyId", "'" + userInfo.F_CompanyId + "'");
                strFormula = item.F_Formula.Replace("@departmentId", "'" + userInfo.F_DepartmentId + "'");
                if (item.F_Formula.IndexOf("@companyIds", StringComparison.Ordinal) > -1)//公司及下属公司
                {
                    if (companyIds == null)
                    {
                        companyIds = "'" + string.Join("','", await _companyIBLL.GetSubNodes(userInfo.F_CompanyId)) + "'";
                    }
                    strFormula = item.F_Formula.Replace("@companyIds", companyIds);
                }
                if (item.F_Formula.IndexOf("@departmentIds", StringComparison.Ordinal) > -1)//部门及下属部门
                {
                    if (departmentIds == null)
                    {
                        departmentIds = "'" + string.Join("','", await _departmentIBLL.GetSubNodes(userInfo.F_CompanyId, userInfo.F_DepartmentId)) + "'";
                    }
                    strFormula = item.F_Formula.Replace("@departmentIds", departmentIds);
                }

                if (item.F_Formula.IndexOf("@postIds", StringComparison.Ordinal) > -1)//岗位
                {
                    if (postIds == null)
                    {
                        postIds = "'" + await _userRelationIBLL.GetObjectIds(userInfo.F_UserId, 2) + "'";
                    }
                    strFormula = item.F_Formula.Replace("@postIds", postIds);
                }
                strFormula = item.F_Formula.Replace("@roleIds", roleIds); //角色

                strRes.Append(strFormula);


                strRes.Append(" ) ");
                num++;
            }


            return strRes.ToString();

        }
        /// <summary>
        /// 获取自定义表单数据权限查询条件
        /// </summary>
        /// <param name="code">自定义表单功能主键</param>
        /// <returns></returns>
        public async Task<string> GetCustomerWhereSql(string code)
        {
            var userInfo = await _userIBLL.GetEntity();
            if (userInfo.F_SecurityLevel == 1)
            {
                return "";
            }
            string roleIds =await GetValue(7,"");
            string objectId = userInfo.F_UserId;
            if (roleIds != "''")
            {
                objectId += "," + roleIds;
            }
            var list = await service.GetList(code, objectId);
            var strRes = new StringBuilder();
            int sqlNum = 0;
            foreach (var item in list) {

                if (sqlNum > 0)
                {
                    strRes.Append(" OR ( ");
                }
                else
                {
                    strRes.Append(" ( ");
                }

                string strSql = "";
                var itemFormula = item.F_Formula.ToObject<Formula>();
                if (!string.IsNullOrEmpty(itemFormula.formula))
                {
                    strSql = item.F_Formula;
                    for (int i = 1; i < itemFormula.conditions.Count + 1; i++)
                    {
                        strSql = strSql.Replace("" + i, "{@aycsoft" + i + "aycsoft@}");
                    }
                }
                else
                {
                    for (int i = 1; i < itemFormula.conditions.Count + 1; i++)
                    {
                        if (strSql != "")
                        {
                            strSql += " AND ";
                        }
                        strSql += " {@aycsoft" + i + "aycsoft@} ";
                    }
                }
                int num = 1;
                foreach (var conditionItem in itemFormula.conditions)
                {
                    string strone = " " + conditionItem.F_FieldId;
                    strone += "0";
                    string value =await GetValue(conditionItem.F_FiledValueType, conditionItem.F_FiledValue);

                    switch (conditionItem.F_Symbol)
                    {
                        case 1:// 等于
                            strone += " = " + value;
                            break;
                        case 2:// 大于
                            strone += " > " + value;
                            break;
                        case 3:// 大于等于
                            strone += " >= " + value;
                            break;
                        case 4:// 小于
                            strone += " < " + value;
                            break;
                        case 5:// 小于等于
                            strone += " <= " + value;
                            break;
                        case 6:// 包含
                            strone += " like " + "%" + value + "%";
                            break;
                        case 7:// 包含于
                            strone += " in ("+ value + ")";
                            break;
                        case 8:// 不等于
                            strone += " != " + value;
                            break;
                        case 9:// 不包含
                            strone += " not like %" + value + "%";
                            break;
                        case 10:// 不包含于
                            strone += " not in (" + value + ")";
                            break;
                        default:
                            break;
                    }
                    strone += " ";
                    strSql = strSql.Replace("{@aycsoft" + num + "aycsoft@}", strone);
                    num++;
                }
                strRes.Append(strSql);
                strRes.Append(" ) ");
            }

            return strRes.ToString();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <param name="value">数据值</param>
        /// <returns></returns>
        private async Task<string> GetValue(int? type, string value)
        {
            var userInfo = await _userIBLL.GetEntity();
            //1.文本2.登录者ID3.登录者账号4.登录者公司5.登录者部门6.登录者岗位7.登录者角色
            string text = "";
            switch (type)
            {
                case 1:// 文本
                    text = value;
                    break;
                case 2:// 登录者ID
                    text = userInfo.F_UserId;
                    break;
                case 3:// 登录者账号
                    text = userInfo.F_Account;
                    break;
                case 4:// 登录者公司
                    text = userInfo.F_CompanyId;
                    break;
                case 41:// 登录者公司及下属公司
                    text = "'" + string.Join("','", await _companyIBLL.GetSubNodes(userInfo.F_CompanyId)) + "'";
                    break;
                case 5:// 登录者部门
                    text = userInfo.F_DepartmentId;
                    break;
                case 51:// 登录者部门及下属部门
                    text = "'" + string.Join("','", await _departmentIBLL.GetSubNodes(userInfo.F_CompanyId, userInfo.F_DepartmentId)) + "'";
                    break;
                case 6:// 登录者岗位
                    text = "'" + string.Join("','", await _userRelationIBLL.GetObjectIds(userInfo.F_UserId, 2)) + "'";
                    break;
                case 7:// 登录者角色
                    text = "'" + string.Join("','", await _userRelationIBLL.GetObjectIds(userInfo.F_UserId, 1)) + "'";
                    break;
                default:
                    text = value;
                    break;
            }
            return text;
        }

        /// <summary>
        /// 公式
        /// </summary>
        private class Formula { 
            /// <summary>
            /// 设置公式
            /// </summary>
            public string formula { get; set; }
            /// <summary>
            /// 设置条件
            /// </summary>
            public List<Condition> conditions { get; set; }
        }
        /// <summary>
        /// 设置条件
        /// </summary>
        private class Condition
        {
            /// <summary>
            /// 主键
            /// </summary>
            /// <returns></returns>
            public string F_Id { get; set; }
            /// <summary>
            /// 数据权限对应关系主键
            /// </summary>
            /// <returns></returns>
            public string F_DataAuthorizeRelationId { get; set; }
            /// <summary>
            /// 字段ID
            /// </summary>
            /// <returns></returns>
            public string F_FieldId { get; set; }
            /// <summary>
            /// 字段名称
            /// </summary>
            /// <returns></returns>
            public string F_FieldName { get; set; }
            /// <summary>
            /// 字段类型
            /// </summary>
            public int F_FieldType { get; set; }
            /// <summary>
            /// 比较符1.等于2.包含3.包含于4.不等于5.不包含6.不包含于
            /// </summary>
            /// <returns></returns>
            public int? F_Symbol { get; set; }
            /// <summary>
            /// 比较符名称
            /// </summary>
            /// <returns></returns>
            public string F_SymbolName { get; set; }
            /// <summary>
            /// 字段值类型1.文本2.登录者ID3.登录者账号4.登录者公司5.登录者部门6.登录者岗位7.登录者角色
            /// </summary>
            public int? F_FiledValueType { get; set; }
            /// <summary>
            /// 字段值
            /// </summary>
            /// <returns></returns>
            public string F_FiledValue { get; set; }
        }
        #endregion
    }
}
