using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;
using aycsoft.wechat;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.05
    /// 描 述：消息策略
    /// </summary>
    public class MStrategyInfoBLL : BLLBase, MStrategyInfoIBLL, BLL
    {
        private readonly MStrategyInfoService mStrategyInfoService = new MStrategyInfoService();

        private readonly UserRelationIBLL _userRelationIBLL;
        private readonly UserIBLL _userIBLL;
        private readonly IMSysUserIBLL _iMSysUserIBLL;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIBLL"></param>
        /// <param name="userRelationIBLL"></param>
        /// <param name="iMSysUserIBLL"></param>
        public MStrategyInfoBLL(UserIBLL userIBLL, UserRelationIBLL userRelationIBLL, IMSysUserIBLL iMSysUserIBLL)
        {
            _userRelationIBLL = userRelationIBLL;
            _userIBLL = userIBLL;
            _iMSysUserIBLL = iMSysUserIBLL;
        }

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<MStrategyInfoEntity>> GetList()
        {
            return mStrategyInfoService.GetList();
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<MStrategyInfoEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            return mStrategyInfoService.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<MStrategyInfoEntity> GetEntity(string keyValue)
        {
            return mStrategyInfoService.GetEntity(keyValue);
        }
        /// <summary>
        /// 根据策略编码获取策略
        /// </summary>
        /// <param name="code">策略编码</param>
        /// <returns></returns>
        public Task<MStrategyInfoEntity> GetEntityByCode(string code)
        {
            return mStrategyInfoService.GetEntityByCode(code);
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
            await mStrategyInfoService.DeleteEntity(keyValue);
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, MStrategyInfoEntity entity)
        {
            await mStrategyInfoService.SaveEntity(keyValue, entity);
        }
        #endregion

        #region 扩展方法（发送消息）
        /// <summary>
        /// 消息处理，在此处处理好数据，然后调用消息发送方法
        /// </summary>
        /// <param name="code">消息策略编码</param>
        /// <param name="content">消息内容</param>
        /// <param name="userlist">用户列表信息</param>
        /// <returns></returns>
        public async Task<ResParameter> SendMessage(string code, string content, string userlist)
        {
            ResParameter resParameter = new ResParameter();
            if (string.IsNullOrEmpty(code))//判断code编码是否输入
            {
                resParameter.code = ResponseCode.fail;
                resParameter.info = "code编码为空";
            }
            else if (string.IsNullOrEmpty(content))//判断是否输入信息内容
            {
                resParameter.code = ResponseCode.fail;
                resParameter.info = "content内容为空";
            }
            else
            {
                var strategyInfoEntity = await GetEntityByCode(code);//根据编码获取消息策略
                if (strategyInfoEntity == null)//如果获取不到消息策略则code编码无效
                {
                    resParameter.code = ResponseCode.fail;
                    resParameter.info = "code编码无效";
                }
                else
                {
                    #region 用户信息处理
                    List<UserEntity> list = new List<UserEntity>();//消息发送对象
                    if (string.IsNullOrEmpty(userlist))
                    {
                        if (string.IsNullOrEmpty(strategyInfoEntity.F_SendRole))
                        {
                            resParameter.code = ResponseCode.fail;
                            resParameter.info = "消息策略无发送角色，需要输入人员userlist信息";
                        }
                        else
                        {
                            String[] rolecontent = strategyInfoEntity.F_SendRole.Split(',');//根据角色id获取用户信息
                            foreach (var item in rolecontent)
                            {
                                var data = await _userRelationIBLL.GetUserIdList(item);
                                string userIds = "";
                                foreach (var items in data)
                                {
                                    if (userIds != "")
                                    {
                                        userIds += ",";
                                    }
                                    userIds += items.F_UserId;
                                }
                                var userList = await _userIBLL.GetListByKeyValues(userIds);
                                foreach (var user in userList)
                                {
                                    list.Add(user);
                                }
                            }
                        }
                    }
                    else
                    {
                        list = userlist.ToList<UserEntity>();
                    }
                    #endregion
                    if (list.Count <= 0)//判断用户列表有一个或一个以上的用户用于发送消息
                    {
                        resParameter.code = ResponseCode.fail;
                        resParameter.info = "找不到发送人员";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(strategyInfoEntity.F_MessageType))
                        {
                            resParameter.code = ResponseCode.fail;
                            resParameter.info = "消息类型为空,无法发送消息";
                        }
                        else
                        {
                            string[] typeList = strategyInfoEntity.F_MessageType.Split(',');

                            foreach (var type in typeList)
                            {
                                switch (type)
                                {
                                    case "1"://邮箱，调用邮箱发送方法
                                        await EmailSend(content, list);
                                        break;
                                    case "2"://微信，调用微信发送方法
                                        await WeChatSend(content, list);
                                        break;
                                    case "3": //短信，调用短信发送方法
                                        SMSSend(content, list);
                                        break;
                                    case "4": //系统IM，效用系统IM发送方法
                                        await IMSend(content, list);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            resParameter.code = ResponseCode.success;
            resParameter.info = "发送成功";

            return resParameter;
        }
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="list">用户列表信息</param>
        /// <returns></returns>
        public async Task EmailSend(string content, IEnumerable<UserEntity> list)
        {
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(item.F_Email))
                {
                    await MailHelper.Send(item.F_Email, "系统消息", content.Replace("-", ""));
                }
            }
        }
        /// <summary>
        /// 微信发送（企业号）
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="list">用户列表</param>
        /// <returns></returns>
        public async Task WeChatSend(string content, List<UserEntity> list)
        {
            string SalesManager = "";
            foreach (var item in list)
            {
                if (SalesManager != "")
                {
                    SalesManager += "|";
                }
                SalesManager += item.F_Account;
            }
            var text = new SendText()
            {
                agentid = ConfigHelper.GetConfig().CorpAppId,//应用ID
                touser = SalesManager,//@all:所有人,多个用户名用 “|”隔开
                text = new SendText.SendItem()
                {
                    content = content
                }
            };
            MessageSendResult result = await text.Send();//发送消息，并返回结果
        }
        /// <summary>
        ///  短息发送
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="list">用户列表</param>
        /// <returns></returns>
        public void SMSSend(string content, List<UserEntity> list)
        {
            // 自行对接响应的短息平台
        }
        /// <summary>
        /// 系统IM发送
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="list">用户列表</param>
        /// <returns></returns>
        public async Task IMSend(string content, List<UserEntity> list)
        {
            List<string> userList = new List<string>();
            foreach (var user in list)
            {
                userList.Add(user.F_UserId);
            }
            await _iMSysUserIBLL.SendMsg("IMSystem", userList, content);
        }
        #endregion
    }
}
