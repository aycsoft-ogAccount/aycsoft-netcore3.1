using System.Collections.Generic;

namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：添加用户标签
    /// 若部分userid非法，则返回
    /// {
    ///     "errcode": 0,
    ///     "errmsg": "invalid userlist failed"
    ///     "invalidlist"："usr1|usr2|usr"
    /// }
    /// 
    /// 当包含userid全部非法时返回
    /// {
    ///     "errcode": 40070,
    ///     "errmsg": "all list invalid "
    /// }
    /// </summary>
    public class TagAddtagusers : OperationRequestBase<TagAddtagusersResult, HttpPostRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string Url()
        {
            return "https://qyapi.weixin.qq.com/cgi-bin/tag/addtagusers?access_token=ACCESS_TOKEN";
        }

        /// <summary>
        /// 标签ID
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string tagid { get; set; }

        /// <summary>
        /// 企业员工ID列表
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public List<string> userlist { get; set; }
    }
}
