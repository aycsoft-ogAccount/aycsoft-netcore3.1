using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ce.autofac.extension;
using aycsoft.util;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.22
    /// 描 述：语言映射
    /// </summary>
    public interface LGMapIBLL : IBLL
    {

        #region 获取数据
        /// <summary>
        /// 获取语言包映射关系数据集合
        /// </summary>
        /// <param name="code">语言包编码</param>
        /// <param name="isMain">是否是主语言</param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetMap(string code, bool isMain);
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="TypeCode">编码</param>
        /// <returns></returns>
        Task<IEnumerable<LGMapEntity>> GetListByTypeCode(string TypeCode);

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="typeList">语言类型列表</param>
        /// <returns></returns>
        Task<DataTable> GetPageList(Pagination pagination, string queryJson, string typeList);
        /// <summary>
        /// 获取对应内容的翻译数据
        /// </summary>
        /// <param name="dataList">需要翻译的列表</param>
        /// <param name="typeList">语言类型列表</param>
        /// <returns></returns>
        Task<DataTable> GetList(string dataList, string typeList);
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<LGMapEntity> GetEntity(string keyValue);
        /// <summary>
        /// 根据名称获取列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        Task<IEnumerable<LGMapEntity>> GetListByName(string name);

        /// <summary>
        /// 根据名称与类型获取列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="typeCode">语言类型</param>
        /// <returns></returns>
        Task<IEnumerable<LGMapEntity>> GetListByNameAndType(string name, string typeCode);
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="code">一组语言编码</param>
        /// <returns></returns>
        Task DeleteEntity(string code);

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="nameList">原列表</param>
        /// <param name="newNameList">新列表</param>
        /// <param name="code">一组语言编码</param>
        /// <returns></returns>
        Task SaveEntity(string nameList, string newNameList, string code);
        #endregion
    }
}
