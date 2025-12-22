using Dapper;
using aycsoft.application;
using aycsoft.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using aycsoft.iappdev;

namespace aycsoft.appdev
{
    /// <summary>
    /// Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期： 2020-06-18 06:35:30
    /// 描 述： 测试代码生成器 f_parent
    /// </summary>
    public class TestService : ServiceBase
    {
        #region 获取数据
        /// <summary>
        /// 获取主表f_parent的所有列表数据
        /// </summary>
        /// <param name="queryJson">查询参数,json字串</param>
        /// <returns></returns>
        public Task<IEnumerable<F_parentEntity>> GetList(string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT 
                            t.F_Id,t.F_text,t.F_textarea,t.F_edit,t.F_radio,t.F_checkbox,t.F_select,t.F_date,t.F_datev,t.F_code,t.F_company,t.F_department,t.F_user,t.F_file,t.F_date2,t.F_ccompany,t.F_cdepartment,t.F_cuser,t.F_cdate
                            FROM f_parent t 
                            ");
            strSql.Append(" where 1 = 1");
            var dp = new DynamicParameters(new { });
            if(!string.IsNullOrEmpty(queryJson)){
                var queryParam = queryJson.ToJObject();
                // 文本
                if (!queryParam["F_text"].IsEmpty())
                {
                    dp.Add("F_text", queryParam["F_text"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_text = @F_text ");
                }
                // 文本区域
                if (!queryParam["F_textarea"].IsEmpty())
                {
                    dp.Add("F_textarea", queryParam["F_textarea"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_textarea = @F_textarea ");
                }
                // 编辑框
                if (!queryParam["F_edit"].IsEmpty())
                {
                    dp.Add("F_edit", queryParam["F_edit"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_edit = @F_edit ");
                }
                // 单选
                if (!queryParam["F_radio"].IsEmpty())
                {
                    dp.Add("F_radio", queryParam["F_radio"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_radio = @F_radio ");
                }
                // 多选
                if (!queryParam["F_checkbox"].IsEmpty())
                {
                    dp.Add("F_checkbox", queryParam["F_checkbox"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_checkbox = @F_checkbox ");
                }
                // 选择框
                if (!queryParam["F_select"].IsEmpty())
                {
                    dp.Add("F_select", queryParam["F_select"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_select = @F_select ");
                }
                // 日期
                if (!queryParam["F_date_start"].IsEmpty() && !queryParam["F_date_end"].IsEmpty())
                {
                    dp.Add("F_date_start", queryParam["F_date_start"].ToDate() , DbType.DateTime);
                    dp.Add("F_date_end", queryParam["F_date_end"].ToDate() , DbType.DateTime);
                    strSql.Append(" AND( t.F_date >= @F_date_start AND t.F_date <= @F_date_end )");
                }
                // 日期差
                if (!queryParam["F_datev"].IsEmpty())
                {
                    dp.Add("F_datev", queryParam["F_datev"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_datev = @F_datev ");
                }
                // 编码
                if (!queryParam["F_code"].IsEmpty())
                {
                    dp.Add("F_code", queryParam["F_code"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_code = @F_code ");
                }
                // 公司
                if (!queryParam["F_company"].IsEmpty())
                {
                    dp.Add("F_company", queryParam["F_company"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_company = @F_company ");
                }
                // 部门
                if (!queryParam["F_department"].IsEmpty())
                {
                    dp.Add("F_department", queryParam["F_department"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_department = @F_department ");
                }
                // 用户
                if (!queryParam["F_user"].IsEmpty())
                {
                    dp.Add("F_user", queryParam["F_user"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_user = @F_user ");
                }
                // 附件
                if (!queryParam["F_file"].IsEmpty())
                {
                    dp.Add("F_file", queryParam["F_file"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_file = @F_file ");
                }
                // 时间2
                if (!queryParam["F_date2_start"].IsEmpty() && !queryParam["F_date2_end"].IsEmpty())
                {
                    dp.Add("F_date2_start", queryParam["F_date2_start"].ToDate() , DbType.DateTime);
                    dp.Add("F_date2_end", queryParam["F_date2_end"].ToDate() , DbType.DateTime);
                    strSql.Append(" AND( t.F_date2 >= @F_date2_start AND t.F_date2 <= @F_date2_end )");
                }
                // 当前公司
                if (!queryParam["F_ccompany"].IsEmpty())
                {
                    dp.Add("F_ccompany", queryParam["F_ccompany"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_ccompany = @F_ccompany ");
                }
                // 当前部门
                if (!queryParam["F_cdepartment"].IsEmpty())
                {
                    dp.Add("F_cdepartment", queryParam["F_cdepartment"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_cdepartment = @F_cdepartment ");
                }
                // 当前用户
                if (!queryParam["F_cuser"].IsEmpty())
                {
                    dp.Add("F_cuser", queryParam["F_cuser"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_cuser = @F_cuser ");
                }
                // 当前时间
                if (!queryParam["F_cdate_start"].IsEmpty() && !queryParam["F_cdate_end"].IsEmpty())
                {
                    dp.Add("F_cdate_start", queryParam["F_cdate_start"].ToDate() , DbType.DateTime);
                    dp.Add("F_cdate_end", queryParam["F_cdate_end"].ToDate() , DbType.DateTime);
                    strSql.Append(" AND( t.F_cdate >= @F_cdate_start AND t.F_cdate <= @F_cdate_end )");
                }

            }
            
            return this.BaseRepository().FindList<F_parentEntity>(strSql.ToString(), dp);
        }

        /// <summary>
        /// 获取主表f_parent的分页列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数,json字串</param>
        /// <returns></returns>
        public Task<IEnumerable<F_parentEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT 
                            t.F_Id,t.F_text,t.F_textarea,t.F_edit,t.F_radio,t.F_checkbox,t.F_select,t.F_date,t.F_datev,t.F_code,t.F_company,t.F_department,t.F_user,t.F_file,t.F_date2,t.F_ccompany,t.F_cdepartment,t.F_cuser,t.F_cdate
                            FROM f_parent t 
                            ");
            strSql.Append(" where 1 = 1");
            var dp = new DynamicParameters(new { });
            if(!string.IsNullOrEmpty(queryJson)){
                var queryParam = queryJson.ToJObject();
                // 文本
                if (!queryParam["F_text"].IsEmpty())
                {
                    dp.Add("F_text", queryParam["F_text"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_text = @F_text ");
                }
                // 文本区域
                if (!queryParam["F_textarea"].IsEmpty())
                {
                    dp.Add("F_textarea", queryParam["F_textarea"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_textarea = @F_textarea ");
                }
                // 编辑框
                if (!queryParam["F_edit"].IsEmpty())
                {
                    dp.Add("F_edit", queryParam["F_edit"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_edit = @F_edit ");
                }
                // 单选
                if (!queryParam["F_radio"].IsEmpty())
                {
                    dp.Add("F_radio", queryParam["F_radio"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_radio = @F_radio ");
                }
                // 多选
                if (!queryParam["F_checkbox"].IsEmpty())
                {
                    dp.Add("F_checkbox", queryParam["F_checkbox"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_checkbox = @F_checkbox ");
                }
                // 选择框
                if (!queryParam["F_select"].IsEmpty())
                {
                    dp.Add("F_select", queryParam["F_select"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_select = @F_select ");
                }
                // 日期
                if (!queryParam["F_date_start"].IsEmpty() && !queryParam["F_date_end"].IsEmpty())
                {
                    dp.Add("F_date_start", queryParam["F_date_start"].ToDate() , DbType.DateTime);
                    dp.Add("F_date_end", queryParam["F_date_end"].ToDate() , DbType.DateTime);
                    strSql.Append(" AND( t.F_date >= @F_date_start AND t.F_date <= @F_date_end )");
                }
                // 日期差
                if (!queryParam["F_datev"].IsEmpty())
                {
                    dp.Add("F_datev", queryParam["F_datev"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_datev = @F_datev ");
                }
                // 编码
                if (!queryParam["F_code"].IsEmpty())
                {
                    dp.Add("F_code", queryParam["F_code"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_code = @F_code ");
                }
                // 公司
                if (!queryParam["F_company"].IsEmpty())
                {
                    dp.Add("F_company", queryParam["F_company"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_company = @F_company ");
                }
                // 部门
                if (!queryParam["F_department"].IsEmpty())
                {
                    dp.Add("F_department", queryParam["F_department"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_department = @F_department ");
                }
                // 用户
                if (!queryParam["F_user"].IsEmpty())
                {
                    dp.Add("F_user", queryParam["F_user"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_user = @F_user ");
                }
                // 附件
                if (!queryParam["F_file"].IsEmpty())
                {
                    dp.Add("F_file", queryParam["F_file"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_file = @F_file ");
                }
                // 时间2
                if (!queryParam["F_date2_start"].IsEmpty() && !queryParam["F_date2_end"].IsEmpty())
                {
                    dp.Add("F_date2_start", queryParam["F_date2_start"].ToDate() , DbType.DateTime);
                    dp.Add("F_date2_end", queryParam["F_date2_end"].ToDate() , DbType.DateTime);
                    strSql.Append(" AND( t.F_date2 >= @F_date2_start AND t.F_date2 <= @F_date2_end )");
                }
                // 当前公司
                if (!queryParam["F_ccompany"].IsEmpty())
                {
                    dp.Add("F_ccompany", queryParam["F_ccompany"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_ccompany = @F_ccompany ");
                }
                // 当前部门
                if (!queryParam["F_cdepartment"].IsEmpty())
                {
                    dp.Add("F_cdepartment", queryParam["F_cdepartment"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_cdepartment = @F_cdepartment ");
                }
                // 当前用户
                if (!queryParam["F_cuser"].IsEmpty())
                {
                    dp.Add("F_cuser", queryParam["F_cuser"].ToString() , DbType.String);
                    strSql.Append(" AND t.F_cuser = @F_cuser ");
                }
                // 当前时间
                if (!queryParam["F_cdate_start"].IsEmpty() && !queryParam["F_cdate_end"].IsEmpty())
                {
                    dp.Add("F_cdate_start", queryParam["F_cdate_start"].ToDate() , DbType.DateTime);
                    dp.Add("F_cdate_end", queryParam["F_cdate_end"].ToDate() , DbType.DateTime);
                    strSql.Append(" AND( t.F_cdate >= @F_cdate_start AND t.F_cdate <= @F_cdate_end )");
                }

            }
            
            return this.BaseRepository().FindList<F_parentEntity>(strSql.ToString(), dp, pagination);
        }
        
        /// <summary>
        /// 获取f_children(f_children)的列表实体数据
        /// </summary>
        /// <param name="f_parentId">与表f_parent的关联字段</param>
        /// <returns></returns>
        public Task<IEnumerable<F_childrenEntity>> GetF_childrenList(string f_parentId)
        {
            return this.BaseRepository().FindList<F_childrenEntity>(new { F_parentId=f_parentId });
        }


        /// <summary>
        /// 获取主表f_parent的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<F_parentEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<F_parentEntity>(keyValue);
        }


        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主表主键</param>
        public async Task Delete(string keyValue)
        {
            var f_parentEntity = await this.BaseRepository().FindEntityByKey<F_parentEntity>(keyValue);
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.DeleteAny<F_parentEntity>(new { F_Id = keyValue });
                await db.DeleteAny<F_childrenEntity>(new { F_parentId = f_parentEntity.F_Id });
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 新增,更新
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="f_parentEntity">f_parent实体数据</param>
        /// <param name="f_childrenList">f_children实体数据列表</param>
        /// <returns></returns>
        public async Task<string> SaveEntity(string keyValue ,F_parentEntity f_parentEntity,IEnumerable<F_childrenEntity> f_childrenList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    if (string.IsNullOrEmpty(f_parentEntity.F_Id)) {
                        f_parentEntity.F_Id = Guid.NewGuid().ToString();
                    }
                    await db.Insert(f_parentEntity);
                }
                else
                {
                    f_parentEntity.F_Id = keyValue;
                    await db.Update(f_parentEntity);
                    var f_parentEntityOld = await db.FindEntityByKey<F_parentEntity>(keyValue);
                    await db.DeleteAny<F_childrenEntity>(new { F_parentId = f_parentEntityOld.F_Id });
                }
                foreach (var item in f_childrenList)
                {
                    item.F_Id = Guid.NewGuid().ToString();
                    item.F_parentId = f_parentEntity.F_Id;
                    await db.Insert(item);
                }
                db.Commit();
                return f_parentEntity.F_Id;
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion
    }
}
