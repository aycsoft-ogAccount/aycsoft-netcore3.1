using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class AnnexesFileBLL : BLLBase, AnnexesFileIBLL, BLL
    {
        private readonly AnnexesFileService annexesFileService = new AnnexesFileService();

        #region 获取数据
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="folderId">附件夹主键</param>
        /// <returns></returns>
        public Task<IEnumerable<AnnexesFileEntity>> GetList(string folderId)
        {
            return annexesFileService.GetList(folderId);
        }
        /// <summary>
        /// 获取附件名称集合
        /// </summary>
        /// <param name="folderId">附件夹主键</param>
        /// <returns></returns>
        public Task<string> GetFileNames(string folderId)
        {
            return annexesFileService.GetFileNames(folderId);
        }
        /// <summary>
        /// 获取附件实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<AnnexesFileEntity> GetEntity(string keyValue)
        {
            return annexesFileService.GetEntity(keyValue);
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
            await annexesFileService.SaveEntity(folderId, annexesFileEntity);
        }
        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="fileId">文件主键</param>
        public async Task DeleteEntity(string fileId)
        {
            await annexesFileService.DeleteEntity(fileId);
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 保存附件（支持大文件分片传输）
        /// </summary>
        /// <param name="folderId">附件夹主键</param>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="chunks">文件总共分多少片</param>
        /// <returns></returns>
        public async Task<bool> SaveAnnexes(string folderId, string fileGuid, string fileName, int chunks)
        {
            //获取文件完整文件名(包含绝对路径)
            //文件存放路径格式：/{account}/yyyymmdd/{guid}.{后缀名}
            string filePath = ConfigHelper.GetConfig().AnnexesFile; ;
            string FileEextension = Path.GetExtension(fileName);
            string uploadDate = DateTime.Now.ToString("yyyyMMdd");
            string virtualPath = string.Format("{0}/{1}/{2}/{3}{4}", filePath, this.GetUserAccount(), uploadDate, fileGuid, FileEextension);
            //创建文件夹
            string path = Path.GetDirectoryName(virtualPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            AnnexesFileEntity fileAnnexesEntity = new AnnexesFileEntity();
            if (!File.Exists(virtualPath))
            {
                long filesize = SaveAnnexesToFile(fileGuid, virtualPath, chunks);
                if (filesize == -1)// 表示保存失败
                {
                    RemoveChunkAnnexes(fileGuid, chunks);
                    return false;
                }
                //文件信息写入数据库
                fileAnnexesEntity.F_Id = fileGuid;
                fileAnnexesEntity.F_FileName = fileName;
                fileAnnexesEntity.F_FilePath = virtualPath;
                fileAnnexesEntity.F_FileSize = filesize.ToString();
                fileAnnexesEntity.F_FileExtensions = FileEextension;
                fileAnnexesEntity.F_FileType = FileEextension.Replace(".", "");
                fileAnnexesEntity.F_CreateUserId = this.GetUserId();
                fileAnnexesEntity.F_CreateUserName = this.GetUserName();

                await SaveEntity(folderId, fileAnnexesEntity);
            }
            return true;
        }
        /// <summary>
        /// 保存附件（支持大文件分片传输）
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="chunks">文件总共分多少片</param>
        /// <returns></returns>
        public string SaveAnnexes(string fileGuid, string fileName, int chunks)
        {
            //获取文件完整文件名(包含绝对路径)
            //文件存放路径格式：/Resource/Temp/{date}/{guid}.{后缀名}
            string filePath = ConfigHelper.GetValue<string>("baseDir") + "/Resource/Temp/";
            string FileEextension = Path.GetExtension(fileName);
            string virtualPath = string.Format("{0}/{1}{2}", filePath, fileGuid, FileEextension);
            //创建文件夹
            string path = Path.GetDirectoryName(virtualPath);
            Directory.CreateDirectory(path);
            if (!File.Exists(virtualPath))
            {
                long filesize = SaveAnnexesToFile(fileGuid, virtualPath, chunks);
                if (filesize == -1)// 表示保存失败
                {
                    RemoveChunkAnnexes(fileGuid, chunks);
                    return "";
                }
            }
            return virtualPath;
        }
        /// <summary>
        /// 保存附件到文件中
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="chunks">总共分片数</param>
        /// <returns>-1:表示保存失败</returns>
        public long SaveAnnexesToFile(string fileGuid, string filePath, int chunks)
        {
            long filesize = 0;
            //创建一个FileInfo对象
            FileInfo file = new FileInfo(filePath);
            //创建文件
            FileStream fs = file.Create();
            for (int i = 0; i < chunks; i++)
            {
                byte[] bufferByRedis = FileHelper.ReadCache(i + "_" + fileGuid);
                if (bufferByRedis == null)
                {
                    return -1;
                }
                //写入二进制流
                fs.Write(bufferByRedis, 0, bufferByRedis.Length);
                filesize += bufferByRedis.Length;
                FileHelper.RemoveCache(i + "_" + fileGuid);
            }
            //关闭文件流
            fs.Close();

            return filesize;
        }
        /// <summary>
        /// 保存分片附件
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="chunk">分片文件序号</param>
        /// <param name="bytes">文件流</param>
        public void SaveChunkAnnexes(string fileGuid, int chunk, byte[] bytes)
        {
            FileHelper.WriteCache(chunk + "_" + fileGuid, bytes);
        }
        /// <summary>
        /// 移除文件分片数据
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="chunks">文件分片数</param>
        public void RemoveChunkAnnexes(string fileGuid, int chunks)
        {
            for (int i = 0; i < chunks; i++)
            {
                FileHelper.RemoveCache(i + "_" + fileGuid);
            }
        }
        #endregion
    }
}
