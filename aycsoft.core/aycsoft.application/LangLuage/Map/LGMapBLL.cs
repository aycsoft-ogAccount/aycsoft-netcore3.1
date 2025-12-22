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
    /// 日 期：2022.10.22
    /// 描 述：语言映射
    /// </summary>
    public class LGMapBLL : BLLBase, LGMapIBLL, BLL
    {
        private readonly LGMapService lGMapService = new LGMapService();

        #region 获取数据
        /// <summary>
        /// 获取语言包映射关系数据集合
        /// </summary>
        /// <param name="code">语言包编码</param>
        /// <param name="isMain">是否是主语言</param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> GetMap(string code, bool isMain)
        {
            var list = new Dictionary<string, string>();
            var _list = await lGMapService.GetListByTypeCode(code);

            if (isMain)
            {
                foreach (var item in _list)
                {
                    if (!list.ContainsKey(item.F_Name))
                    {
                        list.Add(item.F_Name, item.F_Code);
                    }
                }
            }
            else
            {
                foreach (var item in _list)
                {
                    if (!list.ContainsKey(item.F_Code))
                    {
                        list.Add(item.F_Code, item.F_Name);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="TypeCode">编码</param>
        /// <returns></returns>
        public Task<IEnumerable<LGMapEntity>> GetListByTypeCode(string TypeCode)
        {
            return lGMapService.GetListByTypeCode(TypeCode);
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="typeList">语言类型列表</param>
        /// <returns></returns>
        public Task<DataTable> GetPageList(Pagination pagination, string queryJson, string typeList)
        {
            return lGMapService.GetPageList(pagination, queryJson, typeList);
        }

        /// <summary>
        /// 获取对应内容的翻译数据
        /// </summary>
        /// <param name="dataList">需要翻译的列表</param>
        /// <param name="typeList">语言类型列表</param>
        /// <returns></returns>
        public Task<DataTable> GetList(string dataList, string typeList)
        {
            return lGMapService.GetList(dataList, typeList);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<LGMapEntity> GetEntity(string keyValue)
        {
            return lGMapService.GetEntity(keyValue);
        }
        /// <summary>
        /// 根据名称获取列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public Task<IEnumerable<LGMapEntity>> GetListByName(string name)
        {
            return lGMapService.GetListByName(name);
        }

        /// <summary>
        /// 根据名称与类型获取列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="typeCode">语言类型</param>
        /// <returns></returns>
        public Task<IEnumerable<LGMapEntity>> GetListByNameAndType(string name, string typeCode)
        {
            return lGMapService.GetListByNameAndType(name, typeCode);
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="code">一组语言编码</param>
        /// <returns></returns>
        public async Task DeleteEntity(string code)
        {
            await lGMapService.DeleteEntity(code);
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="nameList">原列表</param>
        /// <param name="newNameList">新列表</param>
        /// <param name="code">一组语言编码</param>
        /// <returns></returns>
        public async Task SaveEntity(string nameList, string newNameList, string code)
        {
            await lGMapService.SaveEntity(nameList, newNameList, code);
        }

        #endregion

    }
}
