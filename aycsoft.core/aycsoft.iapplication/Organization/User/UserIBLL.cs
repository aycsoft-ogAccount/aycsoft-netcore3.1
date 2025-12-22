using ce.autofac.extension;
using aycsoft.util;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.12
    /// 描 述：用户
    /// </summary>
    public interface UserIBLL: IBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取用户信息通过账号
        /// </summary>
        /// <param name="account">用户账号</param>
        /// <returns></returns>
        Task<UserEntity> GetEntityByAccount(string account);
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<UserEntity> GetEntity(string keyValue);
        /// <summary>
        /// 获取登录者用户信息
        /// </summary>
        /// <returns></returns>
        Task<UserEntity> GetEntity();
        /// <summary>
        /// 用户列表(根据用户主键集合)
        /// </summary>
        /// <param name="keyValues">用户主键集合主键</param>
        /// <returns></returns>
        Task<IEnumerable<UserEntity>> GetListByKeyValues(string keyValues);
        /// <summary>
        /// 用户列表(根据公司主键,部门主键)
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="departmentId">部门主键</param>
        /// <param name="keyword">查询关键词</param>
        /// <returns></returns>
        Task<IEnumerable<UserEntity>> GetList(string companyId, string departmentId, string keyword);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="departmentId">部门主键</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">查询关键词</param>
        /// <returns></returns>
        Task<IEnumerable<UserEntity>> GetPageList(string companyId, string departmentId, Pagination pagination, string keyword);
        /// <summary>
        /// 用户列表,全部
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        Task<IEnumerable<UserEntity>> GetAllList(string keyword);
        /// <summary>
        /// 用户列表（导出Excel）
        /// </summary>
        /// <returns></returns>
        Task<MemoryStream> GetExportList();

        /// <summary>
        /// 获取超级管理员用户列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UserEntity>> GetAdminList();
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        Task Delete(string keyValue);
        /// <summary>
        /// 保存用户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="userEntity">用户实体</param>
        /// <returns></returns>
        Task SaveEntity(string keyValue, UserEntity userEntity);
        /// <summary>
        /// 修改用户登录密码
        /// </summary>
        /// <param name="newPassword">新密码（MD5 小写）</param>
        /// <param name="oldPassword">旧密码（MD5 小写）</param>
        Task<bool> RevisePassword(string newPassword, string oldPassword);
        /// <summary>
        /// 重置密码(000000)
        /// </summary>
        /// <param name="keyValue">账号主键</param>
        Task ResetPassword(string keyValue);
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="state">状态：1-启动；0-禁用</param>
        Task UpdateState(string keyValue, int state);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 判断密码是否正确
        /// </summary>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassWord">新密码</param>
        /// <param name="secretkey">密钥</param>
        /// <returns></returns>
        bool IsPasswordOk(string oldPassword, string newPassWord, string secretkey);
        #endregion
    }
}
