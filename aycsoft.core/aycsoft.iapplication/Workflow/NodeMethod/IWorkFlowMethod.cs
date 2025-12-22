
namespace aycsoft.iapplication
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：流程绑定方法需要继承的接口
    /// </summary>
    public interface IWorkFlowMethod
    {
        /// <summary>
        /// 流程绑定方法需要继承的接口
        /// </summary>
        /// <param name="parameter"></param>
        void Execute(WfMethodParameter parameter);
    }
}
