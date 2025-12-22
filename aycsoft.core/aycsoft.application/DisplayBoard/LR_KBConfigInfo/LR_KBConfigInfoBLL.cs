using System.Collections.Generic;
using System.Data;
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
    /// 日 期：2022.11.14
    /// 描 述：看板配置信息
    /// </summary>
    public class LR_KBConfigInfoBLL : BLLBase, LR_KBConfigInfoIBLL, BLL
    {
        private readonly LR_KBConfigInfoService lR_KBConfigInfoService = new LR_KBConfigInfoService();
        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<LR_KBConfigInfoEntity>> GetList(string queryJson)
        {
            return lR_KBConfigInfoService.GetList(queryJson);
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<LR_KBConfigInfoEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            return lR_KBConfigInfoService.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<LR_KBConfigInfoEntity> GetEntity(string keyValue)
        {
            return lR_KBConfigInfoService.GetEntity(keyValue);
        }
        /// <summary>
        /// 根据看板id获取所有配置
        /// </summary>
        /// <param name="keyValue">看板id</param>
        /// <returns></returns>
        public Task<IEnumerable<LR_KBConfigInfoEntity>> GetListByKBId(string keyValue)
        {
            return lR_KBConfigInfoService.GetListByKBId(keyValue);
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
            await lR_KBConfigInfoService.DeleteEntity(keyValue);
        }
        /// <summary>
        /// 根据看板id删除其所有配置信息
        /// </summary>
        /// <param name="keyValue">看板id</param>
        /// <returns></returns>
        public async Task DeleteByKBId(string keyValue)
        {
            await lR_KBConfigInfoService.DeleteByKBId(keyValue);
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, LR_KBConfigInfoEntity entity)
        {
            await lR_KBConfigInfoService.SaveEntity(keyValue, entity);
        }

        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取配置数据
        /// </summary>
        /// <param name="configInfoList">配置信息列表</param>
        /// <returns></returns>
        public async Task<IEnumerable<ConfigInfoDataModel>> GetConfigData(List<ConfigInfoModel> configInfoList)
        {
            List<ConfigInfoDataModel> list = new List<ConfigInfoDataModel>();
            foreach (var item in configInfoList)
            {
                ConfigInfoDataModel configInfoDataModel = new ConfigInfoDataModel();
                configInfoDataModel.id = item.id;
                configInfoDataModel.modelType = item.modelType;
                configInfoDataModel.type = item.type;
                configInfoDataModel.data = null;
                configInfoDataModel.dataType = item.dataType;
                if (item.type == "1")
                {
                    DataTable dt = await lR_KBConfigInfoService.BaseRepository(item.dbId).FindTable(item.sql);
                    if (dt.Rows.Count > 0)
                    {
                        configInfoDataModel.data = dt;
                    }
                }
                else
                {
                    var result = await HttpMethods.Get(item.url);
                    ResParameter resParameter = result.ToObject<ResParameter>();
                    if (resParameter != null)
                    {
                        if (resParameter.code.ToString() == "success")
                        {
                            configInfoDataModel.data = resParameter.data;
                        }
                    }
                }
                list.Add(configInfoDataModel);
            }
            return list;
        }
        #endregion

        #region 获取接口数据
        /// <summary>
        /// 根据接口路径获取接口数据（仅限get方法）
        /// </summary>
        /// <param name="path">接口路径</param>
        /// <returns></returns>
        public async Task<object> GetApiData(string path)
        {
            var result = await HttpMethods.Get(path);
            ResParameter resParameter = result.ToObject<ResParameter>();
            object data;
            if (resParameter != null)
            {
                if (resParameter.code.ToString() == "success")
                {
                    data = resParameter.data;
                }
                else
                {
                    data = "";
                }
            }
            else
            {
                data = "";
            }
            return data;
        }
        #endregion

    }
}
