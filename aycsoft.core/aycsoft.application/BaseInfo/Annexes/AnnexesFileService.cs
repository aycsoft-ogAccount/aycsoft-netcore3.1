using aycsoft.iapplication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.25
    /// 描 述：附件管理
    /// </summary>
    public class AnnexesFileService : ServiceBase
    {
        #region 属性 构造函数
        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public AnnexesFileService()
        {
            fieldSql = @" 
                   t.F_Id,
                   t.F_FolderId,
                   t.F_FileName,
                   t.F_FilePath,
                   t.F_FileSize,
                   t.F_FileExtensions,
                   t.F_FileType,
                   t.F_DownloadCount,
                   t.F_CreateDate,
                   t.F_CreateUserId,
                   t.F_CreateUserName
                    ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="folderId">文件夹值</param>
        /// <returns></returns>
        public Task<IEnumerable<AnnexesFileEntity>> GetList(string folderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT " + fieldSql + " FROM LR_Base_AnnexesFile t WHERE t.F_FolderId = @folderId Order By t.F_CreateDate ");
            return this.BaseRepository().FindList<AnnexesFileEntity>(strSql.ToString(), new { folderId });
        }
        /// <summary>
        /// 获取附件名称集合
        /// </summary>
        /// <param name="folderId">主键值</param>
        /// <returns></returns>
        public async Task<string> GetFileNames(string folderId)
        {
            string res = "";
            IEnumerable<AnnexesFileEntity> list = await GetList(folderId);
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(res))
                {
                    res += ",";
                }
                res += item.F_FileName;
            }
            return res;
        }
        /// <summary>
        /// 获取附件实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<AnnexesFileEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<AnnexesFileEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存数据实体
        /// </summary>
        /// <param name="folderId">附件夹主键</param>
        /// <param name="annexesFileEntity">附件实体数据</param>
        public async Task SaveEntity(string folderId, AnnexesFileEntity annexesFileEntity)
        {
            annexesFileEntity.F_CreateDate = DateTime.Now;
            annexesFileEntity.F_FolderId = folderId;
            await this.BaseRepository().Insert(annexesFileEntity);
        }
        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="fileId">文件主键</param>
        public async Task DeleteEntity(string fileId)
        {
            await this.BaseRepository().DeleteAny<AnnexesFileEntity>(new { F_Id = fileId });
        }
        #endregion
    }
}
