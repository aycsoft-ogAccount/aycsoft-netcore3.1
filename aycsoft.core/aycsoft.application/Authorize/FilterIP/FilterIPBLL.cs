using System.Collections.Generic;
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
    /// 日 期：2022.10.25
    /// 描 述：IP过滤
    /// </summary>
    public class FilterIPBLL : BLLBase, FilterIPIBLL, BLL
    {
        private readonly FilterIPService filterIPService = new FilterIPService();

        #region 获取数据
        /// <summary>
        /// 过滤IP列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="visitType">访问:0-拒绝，1-允许</param>
        /// <returns></returns>
        public Task<IEnumerable<FilterIPEntity>> GetList(string objectId, string visitType)
        {
            return filterIPService.GetList(objectId, visitType);
        }
        /// <summary>
        /// 过滤IP实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Task<FilterIPEntity> GetEntity(string keyValue)
        {
            return filterIPService.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除过滤IP
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteEntiy(string keyValue)
        {
            await filterIPService.DeleteEntiy(keyValue);
        }
        /// <summary>
        /// 保存过滤IP表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="filterIPEntity">过滤IP实体</param>
        /// <returns></returns>
        public async Task SaveForm(string keyValue, FilterIPEntity filterIPEntity)
        {
            await filterIPService.SaveForm(keyValue, filterIPEntity);
        }
        #endregion

        #region IP过滤处理
        /// <summary>
        /// IP地址过滤
        /// </summary>
        /// <returns></returns>
        public async Task<bool> FilterIP()
        {
            var userInfo = await this.CurrentUser();
            if (userInfo.F_SecurityLevel == 1)
            {
                return true;
            }

            #region 黑名单处理
            IEnumerable<FilterIPEntity> blackIPList = await GetList(userInfo.F_UserId, "0");
            bool isBlack = CheckArea(blackIPList);
            if (isBlack)
            {
                return false;
            }
            #endregion

            #region 白名单处理
            bool makeWhite = false;
            List<FilterIPEntity> whiteIPList = (List<FilterIPEntity>)await GetList(userInfo.F_UserId, "1");
            if (whiteIPList.Count > 0)
            {
                makeWhite = true;
            }
            bool isWhite = CheckArea(whiteIPList);
            if (isWhite)
            {
                return true;
            }
            if (makeWhite)
            {
                return false;
            }
            #endregion
            return true;
        }
        /// <summary>
        /// 判断当前登陆用户IP是否在IP段中
        /// </summary>
        /// <param name="ipList">Ip列表</param>
        /// <returns></returns>
        private bool CheckArea(IEnumerable<FilterIPEntity> ipList)
        {
            if (ipList == null)
            {
                return false;
            }
            foreach (var item in ipList)
            {
                string strIP = item.F_IPLimit;
                string[] ipArry = strIP.Split(',');
                //黑名单起始IP
                string[] startArry = ipArry[0].Split('.');
                string startHead = startArry[0] + "." + startArry[1] + "." + startArry[2];
                int start = int.Parse(startArry[3]);
                //黑名单结束IP
                string[] endArry = ipArry[1].Split('.');
                string endHead = endArry[0] + "." + endArry[1] + "." + endArry[2];
                int end = int.Parse(endArry[3]);
                //当前IP
                string strIpAddress = WebHelper.GetClinetIP();
                string[] ipAddressArry = strIpAddress.Split('.');
                string ipAddressHead = ipAddressArry[0] + "." + ipAddressArry[1] + "." + ipAddressArry[2];
                int ipAddress = int.Parse(ipAddressArry[3]);
                if (ipAddressHead == startHead)
                {
                    if (ipAddress >= start && ipAddress <= end)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
