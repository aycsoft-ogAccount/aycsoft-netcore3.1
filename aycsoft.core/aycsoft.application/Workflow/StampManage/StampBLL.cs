using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.03.13
    /// 描 述：印章管理
    /// </summary>
    public class StampBLL :BLLBase,StampIBLL,BLL
    {
        private readonly StampService _stampService = new StampService();

        #region 获取数据

        /// <summary>
        /// 获取所有的印章信息/模糊查询（根据名称/状态（启用或者停用））
        /// </summary>
        /// <param name="keyWord">查询的关键字</param>
        /// <returns></returns>
        public async Task<IEnumerable<StampEntity>> GetList(string keyWord)
        {
            return await _stampService.GetList(keyWord);
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public Task<IEnumerable<StampEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            return _stampService.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<StampEntity> GetEntity(string keyValue)
        {
            return _stampService.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存印章信息（新增/编辑）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public async Task SaveEntity(string keyValue, StampEntity entity)
        {
            await _stampService.SaveEntity(keyValue, entity);
        }

        /// <summary>
        /// 删除印章信息
        /// </summary>
        /// <param name="keyVlaue">主键</param>
        public async Task DeleteEntity(string keyVlaue)
        {
            await _stampService.DeleteEntity(keyVlaue);
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 更新数据状态
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态 1启用 0禁用</param>
        public async Task UpdateState(string keyValue, int state)
        {
            StampEntity entity = new StampEntity();
            entity.F_EnabledMark = state;
            await SaveEntity(keyValue, entity);
        }
        /// <summary>
        /// 密码验证
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public async Task<bool> EqualPassword(string keyValue, string password)
        {
            StampEntity entity =await GetEntity(keyValue);
            if (entity.F_Password.Equals(password))//加密后进行对比
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
