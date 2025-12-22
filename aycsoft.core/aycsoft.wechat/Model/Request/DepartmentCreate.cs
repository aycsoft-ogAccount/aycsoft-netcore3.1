using System;
namespace aycsoft.wechat
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.06
    /// 描 述：创建部门
    /// </summary>
    public class DepartmentCreate : OperationRequestBase<DepartmentResult, HttpPostRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override string Url()
        {
            return "https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token=ACCESS_TOKEN";
        }

        /// <summary>
        /// 部门id
        /// </summary>
        /// <returns></returns>
        public string id { get; set; }

        /// <summary>
        /// 部门名称。长度限制为1~64个字符
        /// </summary>
        /// <returns></returns>
        [Length(1, 64)]
        [IsNotNull]
        public string name { get; set; }

        /// <summary>
        /// 父亲部门id。根部门id为1
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string parentid { get; set; }

        /// <summary>
        /// 在父部门中的次序。从1开始，数字越大排序越靠后
        /// </summary>
        /// <returns></returns>
        public string order { get; set; }
    }
}
