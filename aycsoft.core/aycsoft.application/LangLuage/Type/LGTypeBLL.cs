using System.Collections.Generic;
using System.Threading.Tasks;
using ce.autofac.extension;
using aycsoft.iapplication;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.22
    /// 描 述：语言类型
    /// </summary>
    public class LGTypeBLL : BLLBase, LGTypeIBLL, BLL
    {
        private LGTypeService lGTypeService = new LGTypeService();

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<LGTypeEntity>> GetAllData()
        {
            return lGTypeService.GetAllData();
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<LGTypeEntity>> GetList(string queryJson)
        {
            return lGTypeService.GetList(queryJson);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<LGTypeEntity> GetEntity(string keyValue)
        {
            return lGTypeService.GetEntity(keyValue);
        }


        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public Task<LGTypeEntity> GetEntityByCode(string code)
        {
            return lGTypeService.GetEntityByCode(code);
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
            await lGTypeService.DeleteEntity(keyValue);
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, LGTypeEntity entity)
        {
            await lGTypeService.SaveEntity(keyValue, entity);
        }

        /// <summary>
        /// 设为主语言
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public async Task SetMainLG(string keyValue)
        {
            await lGTypeService.SetMainLG(keyValue);
        }
        #endregion

    }
}
