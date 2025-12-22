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
    /// 日 期：2022.11.20
    /// 描 述：门户网站首页配置
    /// </summary>
    public class HomeConfigBLL:BLLBase, HomeConfigIBLL,BLL
    {
        private readonly HomeConfigService homeConfigService = new HomeConfigService();
        private readonly ImgIBLL _imgIBLL;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgIBLL"></param>
        public HomeConfigBLL(ImgIBLL imgIBLL) {
            _imgIBLL = imgIBLL;
        }

        #region 获取数据 
        /// <summary> 
        /// 获取LR_PS_HomeConfig表实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param>
        /// <returns></returns> 
        public Task<HomeConfigEntity> GetEntity(string keyValue)
        {
            return homeConfigService.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取实体根据类型
        /// </summary>
        /// <param name="type">1.顶部文字2.底部文字3.底部地址4.logo图片5.微信图片6.顶部菜单7.底部菜单8.轮播图片9.模块 10底部logo</param>
        /// <returns></returns>
        public Task<HomeConfigEntity> GetEntityByType(string type) {
            return homeConfigService.GetEntityByType(type);
        }

        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public Task<IEnumerable<HomeConfigEntity>> GetList(string type)
        {
            return homeConfigService.GetList(type);
        }
        /// <summary>
        /// 获取所有的配置列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<HomeConfigEntity>> GetALLList()
        {
            return homeConfigService.GetALLList();
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
            var entity = await homeConfigService.GetEntity(keyValue);
            if (entity != null)
            {
                await homeConfigService.DeleteEntity(keyValue);
                if (!string.IsNullOrEmpty(entity.F_Img))
                {
                    await _imgIBLL.DeleteEntity(entity.F_Img);
                }
            }
        }

        /// <summary>
        /// 保存文字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        public async Task SaveText(string text, string type) {
            HomeConfigEntity entity =await GetEntityByType(type);
            string keyValue = null;
            if (entity == null)
            {
                entity = new HomeConfigEntity();
            }
            else {
                keyValue = entity.F_Id;
            }
            entity.F_Type = type;
            entity.F_Name = text;

            await homeConfigService.SaveEntity(keyValue, entity);
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="strBase64">图片字串</param>
        /// <param name="fileNmae">文件名</param>
        /// <param name="fileExName">文件扩展名</param>
        /// <param name="type">类型</param>
        public async Task SaveImg(string strBase64,string fileNmae,string fileExName, string type)
        {
            HomeConfigEntity entity =await GetEntityByType(type);
            string keyValue = null;
            if (entity == null)
            {
                entity = new HomeConfigEntity();
                entity.F_Type = type;
            }
            else
            {
                keyValue = entity.F_Id;
            }

            string imgKey = null;
            ImgEntity imgEntity = null;
            if (!string.IsNullOrEmpty(entity.F_Img))
            {
                imgEntity =await _imgIBLL.GetEntity(entity.F_Img);
                if (imgEntity != null)
                {
                    imgKey = entity.F_Img;
                }
                else
                {
                    imgEntity = new ImgEntity();
                }
            }
            else {
                imgEntity = new ImgEntity();
            }

            imgEntity.F_Content = strBase64;
            imgEntity.F_Name = fileNmae;
            imgEntity.F_ExName = fileExName;

            await _imgIBLL.SaveEntity(imgKey, imgEntity);

            entity.F_Img = imgEntity.F_Id;
            await homeConfigService.SaveEntity(keyValue, entity);
        }
        /// <summary>
        /// 保存轮播图片
        /// </summary>
        /// <param name="strBase64">图片字串</param>
        /// <param name="fileNmae">文件名</param>
        /// <param name="fileExName">文件扩展名</param>
        /// <param name="keyValue">主键</param>
        /// <param name="sort">排序码</param>
        public async Task SaveImg2(string strBase64, string fileNmae, string fileExName, string keyValue,int sort) {
            HomeConfigEntity entity = null;
            if (string.IsNullOrEmpty(keyValue))
            {
                entity = new HomeConfigEntity();
                entity.F_Type = "8";
            }
            else
            {
                entity =await GetEntity(keyValue);

                if (entity == null) {
                    entity = new HomeConfigEntity();
                    entity.F_Id = keyValue;
                    entity.F_Type = "8";

                    keyValue = "";
                }

            }
            entity.F_Sort = sort;

            string imgKey = null;
            ImgEntity imgEntity = null;
            if (!string.IsNullOrEmpty(entity.F_Img))
            {
                imgEntity = await _imgIBLL.GetEntity(entity.F_Img);
                if (imgEntity != null)
                {
                    imgKey = entity.F_Img;
                }
                else
                {
                    imgEntity = new ImgEntity();
                }
            }
            else
            {
                imgEntity = new ImgEntity();
            }

            imgEntity.F_Content = strBase64;
            imgEntity.F_Name = fileNmae;
            imgEntity.F_ExName = fileExName;

            await _imgIBLL.SaveEntity(imgKey, imgEntity);

            entity.F_Img = imgEntity.F_Id;
            await homeConfigService.SaveEntity(keyValue, entity);
        }


        /// <summary> 
        /// 保存实体数据（新增、修改） 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <param name="entity">实体</param> 
        /// <returns></returns> 
        public async Task SaveEntity(string keyValue, HomeConfigEntity entity)
        {
            await homeConfigService.SaveEntity(keyValue, entity);
        }

        #endregion

        #region 扩展方法

        /// <summary>
        /// 获取顶部菜单树形数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TreeModel>> GetTree()
        {
            List<HomeConfigEntity> alllist= (List<HomeConfigEntity>)await  GetList("6");
            List<HomeConfigEntity> list = alllist.FindAll(t=>t.F_Img == "0" || t.F_Img == "1");

            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in list)
            {
                TreeModel node = new TreeModel();
                node.id = item.F_Id;
                node.text = item.F_Name;
                node.value = item.F_Id;
                node.showcheck = false;
                node.checkstate = 0;
                node.isexpand = true;
                node.parentId = item.F_ParentId;
                treeList.Add(node);
            }
            return treeList.ToTree();
        }
        #endregion
    }
}
