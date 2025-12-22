using ce.autofac.extension;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.25
    /// 描 述：附件管理
    /// </summary>
    public interface AnnexesFileIBLL:IBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyValue">附件夹主键</param>
        /// <returns></returns>
        Task<IEnumerable<AnnexesFileEntity>> GetList(string keyValue);
        /// <summary>
        /// 获取附件名称集合
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Task<string> GetFileNames(string keyValue);
        /// <summary>
        /// 获取附件实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Task<AnnexesFileEntity> GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存数据实体
        /// </summary>
        /// <param name="folderId">附件夹主键</param>
        /// <param name="annexesFileEntity">附件实体数据</param>
        Task SaveEntity(string folderId, AnnexesFileEntity annexesFileEntity);
        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="fileId">文件主键</param>
        Task DeleteEntity(string fileId);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 保存附件（支持大文件分片传输）
        /// </summary>
        /// <param name="folderId">附件夹主键</param>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="chunks">文件总共分多少片</param>
        Task<bool> SaveAnnexes(string folderId, string fileGuid, string fileName, int chunks);
        /// <summary>
        /// 保存附件（支持大文件分片传输）
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="chunks">文件总共分多少片</param>
        /// <returns></returns>
        string SaveAnnexes(string fileGuid, string fileName, int chunks);
        /// <summary>
        /// 保存附件到文件中
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="chunks">总共分片数</param>
        /// <returns></returns>
        long SaveAnnexesToFile(string fileGuid, string filePath, int chunks);
        /// <summary>
        /// 保存分片附件
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="chunk">分片文件序号</param>
        /// <param name="bytes">文件流</param>
        void SaveChunkAnnexes(string fileGuid, int chunk, byte[] bytes);
        /// <summary>
        /// 移除文件分片数据
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="chunks">文件分片数</param>
        void RemoveChunkAnnexes(string fileGuid, int chunks);
        #endregion
    }
}
