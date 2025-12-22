using aycsoft.iapplication;
using aycsoft.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.12
    /// 描 述：用户(数据库操作)
    /// </summary>
    public class UserService : ServiceBase
    {
        #region 属性 构造函数
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public UserService()
        {
            fieldSql = @" 
                        t.F_UserId,
                        t.F_EnCode,
                        t.F_Account,
                        t.F_Password,
                        t.F_Secretkey,
                        t.F_RealName,
                        t.F_NickName,
                        t.F_HeadIcon,
                        t.F_QuickQuery,
                        t.F_SimpleSpelling,
                        t.F_Gender,
                        t.F_Birthday,
                        t.F_Mobile,
                        t.F_Telephone,
                        t.F_Email,
                        t.F_OICQ,
                        t.F_WeChat,
                        t.F_MSN,
                        t.F_CompanyId,
                        t.F_DepartmentId,
                        t.F_SecurityLevel,
                        t.F_OpenId,
                        t.F_Question,
                        t.F_AnswerQuestion,
                        t.F_CheckOnLine,
                        t.F_AllowStartTime,
                        t.F_AllowEndTime,
                        t.F_LockStartDate,
                        t.F_LockEndDate,
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
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取用户信息通过账号
        /// </summary>
        /// <param name="account">用户账号</param>
        /// <returns></returns>
        public Task<UserEntity> GetEntityByAccount(string account)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Base_User t ");
            strSql.Append(" WHERE t.F_Account = @account AND t.F_DeleteMark = 0  ");
            return this.BaseRepository().FindEntity<UserEntity>(strSql.ToString(), new { account });
        }

        /// <summary>
        /// 用户列表(根据用户主键集合)
        /// </summary>
        /// <param name="keyValues">用户主键集合主键</param>
        /// <returns></returns>
        public Task<IEnumerable<UserEntity>> GetListByKeyValues(string keyValues)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql.Replace("t.F_Password,", "").Replace("t.F_Secretkey,", ""));
            strSql.Append(" FROM LR_Base_User t WHERE t.F_DeleteMark = 0 ");
            strSql.Append(" AND F_UserId in ('" + keyValues.Replace(",", "','") + "')");


            return this.BaseRepository().FindList<UserEntity>(strSql.ToString());
        }

        /// <summary>
        /// 用户列表(根据公司主键)
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="departmentId">部门主键</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public Task<IEnumerable<UserEntity>> GetList(string companyId, string departmentId, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql.Replace("t.F_Password,", "").Replace("t.F_Secretkey,", ""));
            strSql.Append(" FROM LR_Base_User t WHERE t.F_DeleteMark = 0 ");

            if (!string.IsNullOrEmpty(companyId))
            {
                strSql.Append(" AND t.F_CompanyId = @companyId ");
            }

            if (!string.IsNullOrEmpty(departmentId))
            {
                strSql.Append(" AND t.F_DepartmentId = @departmentId ");
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = "%" + keyword + "%";
                strSql.Append(" AND( t.F_Account like @keyword or t.F_RealName like @keyword  or t.F_Mobile like @keyword  ) ");
            }

            return this.BaseRepository().FindList<UserEntity>(strSql.ToString(), new { companyId, departmentId, keyword });
        }

        /// <summary>
        /// 用户列表(根据公司主键)(分页)
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="departmentId">部门主键</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public Task<IEnumerable<UserEntity>> GetPageList(string companyId, string departmentId, Pagination pagination, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql.Replace("t.F_Password,", "").Replace("t.F_Secretkey,", ""));
            strSql.Append(" FROM LR_Base_User t WHERE t.F_DeleteMark = 0 ");

            if (!string.IsNullOrEmpty(companyId))
            {
                strSql.Append(" AND t.F_CompanyId = @companyId ");
            }

            if (!string.IsNullOrEmpty(departmentId))
            {
                strSql.Append(" AND t.F_DepartmentId = @departmentId ");
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = "%" + keyword + "%";
                strSql.Append(" AND( t.F_Account like @keyword or t.F_RealName like @keyword  or t.F_Mobile like @keyword  ) ");
            }

            return this.BaseRepository().FindList<UserEntity>(strSql.ToString(), new { companyId, departmentId, keyword }, pagination);
        }

        /// <summary>
        /// 用户列表,全部
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public Task<IEnumerable<UserEntity>> GetAllList(string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql.Replace("t.F_Password,", "").Replace("t.F_Secretkey,", ""));
            strSql.Append(" FROM LR_Base_User t WHERE t.F_DeleteMark = 0 AND t.F_EnabledMark = 1 ");
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = "%" + keyword + "%";
                strSql.Append(" AND( t.F_Account like @keyword or t.F_RealName like @keyword  or t.F_Mobile like @keyword  ) ");
            }
            strSql.Append(" ORDER BY t.F_CompanyId,t.F_DepartmentId,t.F_RealName ");

            return this.BaseRepository().FindList<UserEntity>(strSql.ToString(),new { keyword });
        }
        /// <summary>
        /// 用户列表（导出Excel）
        /// </summary>
        /// <returns></returns>
        public Task<DataTable> GetExportList()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT u.F_Account
                                  ,u.F_RealName
                                  ,CASE WHEN u.F_Gender=1 THEN '男' ELSE '女' END AS F_Gender
                                  ,u.F_Birthday
                                  ,u.F_Mobile
                                  ,u.F_Telephone
                                  ,u.F_Email
                                  ,u.F_WeChat
                                  ,u.F_MSN
                                  ,o.F_FullName AS F_Company
                                  ,d.F_FullName AS F_Department
                                  ,u.F_Description
                                  ,u.F_CreateDate
                                  ,u.F_CreateUserName
                              FROM LR_Base_User u
                              INNER JOIN LR_Base_Department d ON u.F_DepartmentId=d.F_DepartmentId
                              INNER JOIN LR_Base_Company o ON u.F_CompanyId=o.F_CompanyId WHERE u.F_DeleteMark = 0 ");
            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 用户实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Task<UserEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<UserEntity>(keyValue);
        }
        /// <summary>
        /// 获取超级管理员用户列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<UserEntity>> GetAdminList()
        {
            return this.BaseRepository().FindList<UserEntity>(" select * from LR_Base_User where F_SecurityLevel = 1 ");
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            await this.BaseRepository().DeleteAny<UserEntity>(new { F_UserId = keyValue });
        }
        /// <summary>
        /// 保存用户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="userEntity">用户实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, UserEntity userEntity)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                userEntity.F_CreateUserId = this.GetUserId();
                userEntity.F_CreateUserName = this.GetUserName();

                userEntity.F_UserId = Guid.NewGuid().ToString();
                userEntity.F_CreateDate = DateTime.Now;
                userEntity.F_DeleteMark = 0;
                userEntity.F_EnabledMark = 1;
                userEntity.F_Secretkey = Md5Helper.Encrypt(CommonHelper.CreateNo(), 16).ToLower();
                userEntity.F_Password = Md5Helper.Encrypt(DESEncrypt.Encrypt(userEntity.F_Password, userEntity.F_Secretkey).ToLower(), 32).ToLower();
                await this.BaseRepository().Insert(userEntity);
            }
            else
            {
                userEntity.F_ModifyUserId = this.GetUserId();
                userEntity.F_ModifyUserName = this.GetUserName();

                userEntity.F_UserId = keyValue;
                userEntity.F_ModifyDate = DateTime.Now;
                userEntity.F_Secretkey = null;
                userEntity.F_Password = null;
                await this.BaseRepository().Update(userEntity);
            }
        }
        /// <summary>
        /// 修改用户登录密码
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="password">新密码（MD5 小写）</param>
        /// <param name="userInfo">当前用户信息</param>
        public async Task RevisePassword(string keyValue, string password, UserEntity userInfo)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.F_ModifyUserId = userInfo.F_UserId;
            userEntity.F_ModifyUserName = userInfo.F_RealName;

            userEntity.F_UserId = keyValue;
            userEntity.F_ModifyDate = DateTime.Now;
            userEntity.F_Secretkey = Md5Helper.Encrypt(CommonHelper.CreateNo(), 16).ToLower();
            userEntity.F_Password = Md5Helper.Encrypt(DESEncrypt.Encrypt(password, userEntity.F_Secretkey).ToLower(), 32).ToLower();
            await this.BaseRepository().Update(userEntity);
        }
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="state">状态：1-启动；0-禁用</param>
        /// <param name="userInfo">当前用户信息</param>
        public async Task UpdateState(string keyValue, int state, UserEntity userInfo)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.F_ModifyUserId = userInfo.F_UserId;
            userEntity.F_ModifyUserName = userInfo.F_RealName;

            userEntity.F_UserId = keyValue;
            userEntity.F_ModifyDate = DateTime.Now;
            userEntity.F_EnabledMark = state;
            await this.BaseRepository().Update(userEntity);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userEntity">实体对象</param>
        public async Task UpdateEntity(UserEntity userEntity)
        {
            await this.BaseRepository().Update(userEntity);
        }
        #endregion
    }
}
