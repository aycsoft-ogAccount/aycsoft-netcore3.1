using System;
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
    /// 描 述：时段过滤
    /// </summary>
    public class FilterTimeBLL : BLLBase, FilterTimeIBLL, BLL
    {
        private readonly FilterTimeService filterTimeService = new FilterTimeService();

        #region 获取数据
        /// <summary>
        /// 过滤时段实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Task<FilterTimeEntity> GetEntity(string keyValue)
        {
            return filterTimeService.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除过滤时段
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteEntiy(string keyValue)
        {
            await filterTimeService.DeleteEntiy(keyValue);
        }
        /// <summary>
        /// 保存过滤时段表单（新增、修改）
        /// </summary>
        /// <param name="filterTimeEntity">过滤时段实体</param>
        /// <returns></returns>
        public async Task SaveForm(FilterTimeEntity filterTimeEntity)
        {
            string keyValue = "";
            FilterTimeEntity entity = await filterTimeService.GetEntity(filterTimeEntity.F_FilterTimeId);
            if (entity != null)
            {
                keyValue = entity.F_FilterTimeId;
            }
            await filterTimeService.SaveForm(keyValue, filterTimeEntity);
        }
        #endregion

        #region 处理时间过滤
        /// <summary>
        /// 处理时间过滤
        /// </summary>
        /// <returns></returns>
        public async Task<bool> FilterTime()
        {
            var userInfo = await this.CurrentUser();
            if (userInfo.F_SecurityLevel == 1)
            {
                return true;
            }
            FilterTimeEntity entity = await GetEntity(userInfo.F_Account);
            bool res = FilterTime(entity);
            return res;
        }
        /// <summary>
        /// 处理时间过滤
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        private bool FilterTime(FilterTimeEntity entity)
        {
            bool res = true;
            if (entity == null)
            {
                return res;
            }
            int weekday = Time.GetNumberWeekDay(DateTime.Now);
            string time = DateTime.Now.ToString("HH") + ":00";
            string strFilterTime = "";
            switch (weekday)
            {
                case 1:
                    strFilterTime = entity.F_WeekDay1;
                    break;
                case 2:
                    strFilterTime = entity.F_WeekDay2;
                    break;
                case 3:
                    strFilterTime = entity.F_WeekDay3;
                    break;
                case 4:
                    strFilterTime = entity.F_WeekDay4;
                    break;
                case 5:
                    strFilterTime = entity.F_WeekDay5;
                    break;
                case 6:
                    strFilterTime = entity.F_WeekDay6;
                    break;
                case 7:
                    strFilterTime = entity.F_WeekDay7;
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(strFilterTime))
            {
                //当前时段包含在限制时段中
                if (strFilterTime.IndexOf(time, StringComparison.Ordinal) >= 0)
                {
                    res = false;
                }
            }
            return res;
        }
        #endregion
    }
}
