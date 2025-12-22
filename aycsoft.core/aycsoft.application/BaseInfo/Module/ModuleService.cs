using aycsoft.iapplication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.10
    /// 描 述：功能模块
    /// </summary>
    public class ModuleService : ServiceBase
    {
        #region 属性 构造函数
        private readonly string fieldSql;
        private readonly string btnfieldSql;
        private readonly string colfieldSql;
        private readonly string formfieldSql;
        /// <summary>
        /// 
        /// </summary>
        public ModuleService()
        {
            fieldSql = @" 
                    t.F_ModuleId,
                    t.F_ParentId,
                    t.F_EnCode,
                    t.F_FullName,
                    t.F_Icon,
                    t.F_UrlAddress,
                    t.F_Target,
                    t.F_IsMenu,
                    t.F_AllowExpand,
                    t.F_IsPublic,
                    t.F_AllowEdit,
                    t.F_AllowDelete,
                    t.F_SortCode,
                    t.F_DeleteMark,
                    t.F_EnabledMark,
                    t.F_Description,
                    t.F_CreateDate,
                    t.F_CreateUserId,
                    t.F_CreateUserName,
                    t.F_ModifyDate,
                    t.F_ModifyUserId,
                    t.F_ModifyUserName
                    ";
            btnfieldSql = @"
                    t.F_ModuleButtonId,
                    t.F_ModuleId,
                    t.F_ParentId,
                    t.F_Icon,
                    t.F_EnCode,
                    t.F_FullName,
                    t.F_ActionAddress,
                    t.F_SortCode
                    ";
            colfieldSql = @"
                    t.F_ModuleColumnId,
                    t.F_ModuleId,
                    t.F_ParentId,
                    t.F_EnCode,
                    t.F_FullName,
                    t.F_SortCode,
                    t.F_Description
                    ";
            formfieldSql = @"
                    t.F_ModuleFormId,
                    t.F_ModuleId,
                    t.F_EnCode,
                    t.F_FullName,
                    t.F_SortCode
                    ";
        }
        #endregion

        #region 功能模块
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<ModuleEntity>> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + fieldSql + " FROM LR_Base_Module t WHERE t.F_DeleteMark = 0 Order By t.F_ParentId,t.F_SortCode ");
            return this.BaseRepository().FindList<ModuleEntity>(strSql.ToString());
        }

        /// <summary>
        /// 功能列表(url)
        /// </summary>
        /// <returns></returns>
        public Task<ModuleEntity> GetEntityByUrl(string url)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + fieldSql + " FROM LR_Base_Module t WHERE t.F_DeleteMark = 0 AND t.F_UrlAddress = @url ");
            return this.BaseRepository().FindEntity<ModuleEntity>(strSql.ToString(), new { url });
        }

        /// <summary>
        ///  功能列表(code)
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public Task<ModuleEntity> GetEntityByCode(string code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + fieldSql + " FROM LR_Base_Module t WHERE t.F_DeleteMark = 0 AND t.F_EnCode = @code ");
            return this.BaseRepository().FindEntity<ModuleEntity>(strSql.ToString(), new { code });
        }
        /// <summary>
        /// 功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Task<ModuleEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<ModuleEntity>(keyValue);
        }
        #endregion

        #region 模块按钮
        /// <summary>
        /// 获取按钮列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        public Task<IEnumerable<ModuleButtonEntity>> GetButtonList(string moduleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + btnfieldSql + " FROM LR_Base_ModuleButton t WHERE t.F_ModuleId = @moduleId ORDER BY t.F_SortCode ");
            return this.BaseRepository().FindList<ModuleButtonEntity>(strSql.ToString(), new { moduleId = moduleId });
        }
        /// <summary>
        /// 获取全部按钮列表数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<ModuleButtonEntity>> GetButtonList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + btnfieldSql + " FROM LR_Base_ModuleButton t ORDER BY t.F_SortCode ");
            return this.BaseRepository().FindList<ModuleButtonEntity>(strSql.ToString());
        }
        #endregion

        #region 模块视图列表
        /// <summary>
        /// 获取视图列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        public Task<IEnumerable<ModuleColumnEntity>> GetColumnList(string moduleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + colfieldSql + " FROM LR_Base_ModuleColumn t WHERE t.F_ModuleId = @moduleId ORDER BY t.F_SortCode ");
            return this.BaseRepository().FindList<ModuleColumnEntity>(strSql.ToString(), new { moduleId = moduleId });
        }
        /// <summary>
        /// 获取全部视图列表数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<ModuleColumnEntity>> GetColumnList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + colfieldSql + " FROM LR_Base_ModuleColumn t ORDER BY t.F_SortCode ");
            return this.BaseRepository().FindList<ModuleColumnEntity>(strSql.ToString());
        }
        #endregion

        #region 模块表单字段
        /// <summary>
        /// 获取表单字段数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        public Task<IEnumerable<ModuleFormEntity>> GetFormList(string moduleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + formfieldSql + " FROM LR_Base_ModuleForm t WHERE t.F_ModuleId = @moduleId ORDER BY t.F_SortCode ");
            return this.BaseRepository().FindList<ModuleFormEntity>(strSql.ToString(), new { moduleId = moduleId });
        }
        /// <summary>
        /// 获取全部表单字段数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<ModuleFormEntity>> GetFormList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + formfieldSql + " FROM LR_Base_ModuleForm t ORDER BY t.F_SortCode ");
            return this.BaseRepository().FindList<ModuleFormEntity>(strSql.ToString());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模块功能
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                await db.DeleteAny<ModuleEntity>(new { F_ModuleId = keyValue });
                await db.DeleteAny<ModuleButtonEntity>(new { F_ModuleId = keyValue });
                await db.DeleteAny<ModuleColumnEntity>(new { F_ModuleId = keyValue });
                await db.DeleteAny<ModuleFormEntity>(new { F_ModuleId = keyValue });

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存模块功能实体（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moduleEntity">实体</param>
        /// <param name="moduleButtonEntitys">按钮列表</param>
        /// <param name="moduleColumnEntitys">视图列集合</param>
        /// <param name="moduleFormEntitys">表单字段</param>
        public async Task SaveEntity(string keyValue, ModuleEntity moduleEntity, List<ModuleButtonEntity> moduleButtonEntitys, List<ModuleColumnEntity> moduleColumnEntitys, List<ModuleFormEntity> moduleFormEntitys)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {

                if (string.IsNullOrEmpty(moduleEntity.F_ParentId) || moduleEntity.F_ParentId == "-1")
                {
                    moduleEntity.F_ParentId = "0";
                }

                if (string.IsNullOrEmpty(keyValue))
                {
                    // 新增
                    moduleEntity.F_ModuleId = Guid.NewGuid().ToString();
                    moduleEntity.F_CreateDate = DateTime.Now;
                    moduleEntity.F_CreateUserId = this.GetUserId();
                    moduleEntity.F_CreateUserName = this.GetUserName();
                    moduleEntity.F_DeleteMark = 0;
                    await db.Insert(moduleEntity);
                }
                else
                {
                    // 编辑
                    moduleEntity.F_ModuleId = keyValue;
                    moduleEntity.F_ModifyDate = DateTime.Now;
                    moduleEntity.F_ModifyUserId = this.GetUserId();
                    moduleEntity.F_ModifyUserName = this.GetUserName();
                    await db.Update(moduleEntity);
                    await db.DeleteAny<ModuleButtonEntity>(new { F_ModuleId = keyValue });
                    await db.DeleteAny<ModuleColumnEntity>(new { F_ModuleId = keyValue });
                    await db.DeleteAny<ModuleFormEntity>(new { F_ModuleId = keyValue });
                }
                int num = 0;
                if (moduleButtonEntitys != null)
                {
                    foreach (var item in moduleButtonEntitys)
                    {
                        item.F_ModuleButtonId = Guid.NewGuid().ToString();
                        item.F_SortCode = num;
                        item.F_ModuleId = moduleEntity.F_ModuleId;
                        if (moduleButtonEntitys.Find(t => t.F_ModuleButtonId == item.F_ParentId) == null)
                        {
                            item.F_ParentId = "0";
                        }
                        await db.Insert(item);
                        num++;
                    }
                }

                if (moduleColumnEntitys != null)
                {
                    num = 0;
                    foreach (var item in moduleColumnEntitys)
                    {
                        item.F_ModuleColumnId = Guid.NewGuid().ToString();
                        item.F_SortCode = num;
                        item.F_ModuleId = moduleEntity.F_ModuleId;
                        await db.Insert(item);
                        num++;
                    }
                }
                if (moduleFormEntitys != null)
                {
                    num = 0;
                    foreach (var item in moduleFormEntitys)
                    {
                        item.F_ModuleFormId = Guid.NewGuid().ToString();
                        item.F_SortCode = num;
                        item.F_ModuleId = moduleEntity.F_ModuleId;
                        await db.Insert(item);
                        num++;
                    }
                }


                db.Commit();
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
