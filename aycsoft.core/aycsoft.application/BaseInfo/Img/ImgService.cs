using aycsoft.iapplication;
using System;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.17
    /// 描 述：图片保存
    /// </summary>
    public class ImgService : ServiceBase
    {
        #region 获取数据 
        /// <summary> 
        /// 获取LR_Base_Img表实体数据
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        public Task<ImgEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<ImgEntity>(keyValue);
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
            await this.BaseRepository().DeleteAny<ImgEntity>(new { F_Id = keyValue });
        }

        /// <summary> 
        /// 保存实体数据（新增、修改）
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <param name="entity">实体</param> 
        /// <returns></returns> 
        public async Task SaveEntity(string keyValue, ImgEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.F_Id = keyValue;
                var entityTmp = await this.BaseRepository().FindEntityByKey<ImgEntity>(keyValue);
                if (entityTmp == null)
                {
                    await this.BaseRepository().Insert(entity);
                }
                else
                {
                    await this.BaseRepository().Update(entity);
                }
            }
            else
            {
                entity.F_Id = Guid.NewGuid().ToString();
                await this.BaseRepository().Insert(entity);
            }
        }

        #endregion
    }
}
