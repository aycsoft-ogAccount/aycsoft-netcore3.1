using System.Collections.Generic;
using System.Threading.Tasks;
using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.05
    /// 描 述：即时通讯用户注册
    /// </summary>
    public class IMSysUserBLL : BLLBase, IMSysUserIBLL, BLL
    {

        private readonly IMSysUserService iMSysUserService = new IMSysUserService();
        private readonly IMMsgService iMMsgService = new IMMsgService();

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        public Task<IEnumerable<IMSysUserEntity>> GetList(string keyWord)
        {
            return iMSysUserService.GetList(keyWord);
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        public Task<IEnumerable<IMSysUserEntity>> GetPageList(Pagination pagination, string keyWord)
        {
            return iMSysUserService.GetPageList(pagination, keyWord);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<IMSysUserEntity> GetEntity(string keyValue)
        {
            return iMSysUserService.GetEntity(keyValue);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除实体数据(虚拟)
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public async Task DeleteEntity(string keyValue)
        {
            await iMSysUserService.DeleteEntity(keyValue);
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, IMSysUserEntity entity)
        {
            await iMSysUserService.SaveEntity(keyValue, entity);
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="userIdList">用户列表</param>
        /// <param name="content">消息内容</param>
        public async Task SendMsg(string code, IEnumerable<string> userIdList, string content)
        {
            if (!string.IsNullOrEmpty(content) && userIdList != null)
            {
                var entity = await iMSysUserService.GetEntityByCode(code);
                if (entity != null)
                {
                    foreach (var userId in userIdList)
                    {

                        IMMsgEntity iMMsgEntity = new IMMsgEntity();
                        iMMsgEntity.F_SendUserId = code;
                        iMMsgEntity.F_RecvUserId = userId;
                        iMMsgEntity.F_Content = content;
                        await iMMsgService.SaveEntity(iMMsgEntity);

                        await SendHubs.callMethod("sendMsg2", code, userId, content, 1);
                    }
                }
            }
        }
        #endregion
    }
}
