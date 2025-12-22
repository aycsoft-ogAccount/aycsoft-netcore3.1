using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.12
    /// 描 述：用户
    /// </summary>
    public class UserBLL:UserIBLL,BLL
    {
        private readonly UserService userService = new UserService();

        #region 获取数据
        /// <summary>
        /// 获取用户信息通过账号
        /// </summary>
        /// <param name="account">用户账号</param>
        /// <returns></returns>
        public Task<UserEntity> GetEntityByAccount(string account)
        {
            return userService.GetEntityByAccount(account) ;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<UserEntity> GetEntity(string keyValue)
        {
            return userService.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取登录者用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<UserEntity> GetEntity() {
            string account = ContextHelper.GetItem("account") as string;
            if (string.IsNullOrEmpty(account)) {
                return new UserEntity();
            }
            return await userService.GetEntityByAccount(account);
        }
        /// <summary>
        /// 用户列表(根据用户主键集合)
        /// </summary>
        /// <param name="keyValues">用户主键集合主键</param>
        /// <returns></returns>
        public Task<IEnumerable<UserEntity>> GetListByKeyValues(string keyValues)
        {
            return userService.GetListByKeyValues(keyValues);
        }
        /// <summary>
        /// 用户列表(根据公司主键,部门主键)
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="departmentId">部门主键</param>
        /// <param name="keyword">查询关键词</param>
        /// <returns></returns>
        public Task<IEnumerable<UserEntity>> GetList(string companyId, string departmentId, string keyword)
        {
            return userService.GetList(companyId, departmentId, keyword);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="departmentId">部门主键</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">查询关键词</param>
        /// <returns></returns>
        public Task<IEnumerable<UserEntity>> GetPageList(string companyId, string departmentId, Pagination pagination, string keyword)
        {
            return userService.GetPageList(companyId, departmentId, pagination, keyword);
        }
        /// <summary>
        /// 用户列表,全部
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public Task<IEnumerable<UserEntity>> GetAllList(string keyword)
        {
            return userService.GetAllList(keyword);
        }
        /// <summary>
        /// 用户列表（导出Excel）
        /// </summary>
        /// <returns></returns>
        public async Task<MemoryStream> GetExportList()
        {
            //取出数据源
            DataTable exportTable =await userService.GetExportList();
            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.Title = "测试用户导出";
            excelconfig.TitleFont = "微软雅黑";
            excelconfig.TitlePoint = 25;
            excelconfig.FileName = "用户导出.xls";
            excelconfig.IsAllSizeColumn = true;
            //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
            excelconfig.ColumnEntity = new List<ColumnModel>();
            excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_account", ExcelColumn = "账户" });
            excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_realname", ExcelColumn = "姓名" });
            excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_gender", ExcelColumn = "性别" });
            excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_birthday", ExcelColumn = "生日" });
            excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_mobile", ExcelColumn = "手机", Background = Color.Red });
            excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_telephone", ExcelColumn = "电话", Background = Color.Red });
            excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_wechat", ExcelColumn = "微信" });
            excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_organize", ExcelColumn = "公司" });
            excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_department", ExcelColumn = "部门" });
            excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_description", ExcelColumn = "说明" });
            excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_createdate", ExcelColumn = "创建日期" });
            excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_createusername", ExcelColumn = "创建人" });
            //
            return ExcelHelper.ExportMemoryStream(exportTable, excelconfig);
        }
        /// <summary>
        /// 获取超级管理员用户列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<UserEntity>> GetAdminList()
        {
            return userService.GetAdminList();
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            await userService.Delete(keyValue);
        }
        /// <summary>
        /// 保存用户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="userEntity">用户实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, UserEntity userEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                userEntity.F_Account = null;// 账号不允许改动
            }
            await userService.SaveEntity(keyValue, userEntity);
        }
        /// <summary>
        /// 修改用户登录密码
        /// </summary>
        /// <param name="newPassword">新密码（MD5 小写）</param>
        /// <param name="oldPassword">旧密码（MD5 小写）</param>
        public async Task<bool> RevisePassword(string newPassword, string oldPassword)
        {
            var userInfo = await GetEntity();
            string oldPasswordByEncrypt = Md5Helper.Encrypt(DESEncrypt.Encrypt(oldPassword, userInfo.F_Secretkey).ToLower(), 32).ToLower();
            if (oldPasswordByEncrypt == userInfo.F_Password)
            {
                await userService.RevisePassword(userInfo.F_UserId, newPassword, userInfo);
            }
            else
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 重置密码(000000)
        /// </summary>
        /// <param name="keyValue">账号主键</param>
        public async Task ResetPassword(string keyValue)
        {
            var userInfo = await GetEntity();
            string password = Md5Helper.Encrypt("000000", 32).ToLower();
            await userService.RevisePassword(keyValue, password, userInfo);
        }
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="state">状态：1-启动；0-禁用</param>
        public async Task UpdateState(string keyValue, int state)
        {
            var userInfo = await GetEntity();
            await userService.UpdateState(keyValue, state, userInfo);
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 判断密码是否正确
        /// </summary>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassWord">新密码</param>
        /// <param name="secretkey">密钥</param>
        /// <returns></returns>
        public bool IsPasswordOk(string oldPassword, string newPassWord,string secretkey) {

            string dbPassword = Md5Helper.Encrypt(DESEncrypt.Encrypt(newPassWord, secretkey).ToLower(), 32).ToLower();
            if (dbPassword == oldPassword) {
                return true;
            }

            return false;
        }

        #endregion 
    }
}
