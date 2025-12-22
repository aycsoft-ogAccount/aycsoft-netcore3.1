using ce.autofac.extension;
using aycsoft.util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.20
    /// 描 述：门户网站首页配置
    /// </summary>
    public interface HomeConfigIBLL : IBLL
    {
        #region 获取数据 
        /// <summary> 
        /// 获取LR_PS_HomeConfig表实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param>
        /// <returns></returns> 
        Task<HomeConfigEntity> GetEntity(string keyValue);
        /// <summary>
        /// 获取实体根据类型
        /// </summary>
        /// <param name="type">1.顶部文字2.底部文字3.底部地址4.logo图片5.微信图片6.顶部菜单7.底部菜单8.轮播图片9.模块 10底部logo</param>
        /// <returns></returns>
        Task<HomeConfigEntity> GetEntityByType(string type);

        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        Task<IEnumerable<HomeConfigEntity>> GetList(string type);
        /// <summary>
        /// 获取所有的配置列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<HomeConfigEntity>> GetALLList();
        #endregion

        #region 提交数据 

        /// <summary> 
        /// 删除实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        Task DeleteEntity(string keyValue);
        /// <summary> 
        /// 保存实体数据（新增、修改） 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <param name="entity">实体</param> 
        /// <returns></returns> 
        Task SaveEntity(string keyValue, HomeConfigEntity entity);
        /// <summary>
        /// 保存文字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        Task SaveText(string text, string type);
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="strBase64">图片字串</param>
        /// <param name="fileNmae">文件名</param>
        /// <param name="fileExName">文件扩展名</param>
        /// <param name="type">类型</param>
        Task SaveImg(string strBase64, string fileNmae, string fileExName, string type);
        /// <summary>
        /// 保存轮播图片
        /// </summary>
        /// <param name="strBase64">图片字串</param>
        /// <param name="fileNmae">文件名</param>
        /// <param name="fileExName">文件扩展名</param>
        /// <param name="keyValue">主键</param>
        /// <param name="sort">排序字段</param>
        Task SaveImg2(string strBase64, string fileNmae, string fileExName, string keyValue, int sort);
        #endregion

        #region 扩展方法

        /// <summary>
        /// 获取顶部菜单树形数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TreeModel>> GetTree();
        #endregion
    }
}
