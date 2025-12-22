using ce.autofac.extension;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.07
    /// 描 述：我的常用移动应用 
    /// </summary>
    public interface MyFunctionIBLL:IBLL
    {
        #region 获取数据 

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="userId">用户主键ID</param>
        /// <returns></returns>
        Task<IEnumerable<MyFunctionEntity>> GetList(string userId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="strFunctionId">功能id</param>
        /// <returns></returns>
        Task SaveEntity(string userId, string strFunctionId);
        #endregion
    }
}
