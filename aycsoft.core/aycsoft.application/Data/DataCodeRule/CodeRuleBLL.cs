using ce.autofac.extension;
using aycsoft.iapplication;
using aycsoft.util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.09.24
    /// 描 述：编号规则
    /// </summary>
    public class CodeRuleBLL :BLLBase, CodeRuleIBLL, BLL
    {
        #region 属性
        private readonly CodeRuleService codeRuleService = new CodeRuleService();

        // 组织单位
        private readonly CompanyIBLL _companyIBLL;
        private readonly DepartmentIBLL  _departmentIBLL;
        private readonly UserIBLL  _userIBLL;
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyIBLL"></param>
        /// <param name="departmentIBLL"></param>
        /// <param name="userIBLL"></param>
        public CodeRuleBLL(CompanyIBLL companyIBLL, DepartmentIBLL departmentIBLL, UserIBLL userIBLL)
        {
            _companyIBLL = companyIBLL;
            _departmentIBLL = departmentIBLL;
            _userIBLL = userIBLL;
        }


        #region 获取数据
        /// <summary>
        /// 规则列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">查询参数</param>
        /// <returns></returns>
        public Task<IEnumerable<CodeRuleEntity>> GetPageList(Pagination pagination, string keyword)
        {
            return codeRuleService.GetPageList(pagination, keyword);
        }
        /// <summary>
        /// 规则列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<CodeRuleEntity>> GetList()
        {
            return codeRuleService.GetList();
        } 
        /// <summary>
        /// 规则实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Task<CodeRuleEntity> GetEntity(string keyValue)
        {
            return codeRuleService.GetEntity(keyValue);
        }
        /// <summary>
        /// 规则实体
        /// </summary>
        /// <param name="enCode">规则编码</param>
        /// <returns></returns>
        public Task<CodeRuleEntity> GetEntityByCode(string enCode)
        {
            return codeRuleService.GetEntityByCode(enCode);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            await codeRuleService.Delete(keyValue);
        }
        /// <summary>
        /// 保存规则表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="codeRuleEntity">规则实体</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, CodeRuleEntity codeRuleEntity)
        {
            await codeRuleService.SaveEntity(keyValue, codeRuleEntity);
        }
        #endregion

        #region 单据编码处理
        /// <summary>
        /// 获得指定模块或者编号的单据号
        /// </summary>
        /// <param name="enCode">编码</param>
        /// <param name="account">用户账号</param>
        /// <returns>单据号</returns>
        public async Task<string> GetBillCode(string enCode, string account = "")
        {
            string billCode = "";    //单据号
            string nextBillCode = "";//单据号
            bool isOutTime = false;  //是否已过期


            CodeRuleEntity coderuleentity = await GetEntityByCode(enCode);
            if (coderuleentity != null)
            {
                UserEntity userInfo = null;
                if (string.IsNullOrEmpty(account))
                {
                    userInfo = await this.CurrentUser();
                }
                else
                {
                    userInfo = await _userIBLL.GetEntityByAccount(account);
                }

                int nowSerious = 0;
                List<CodeRuleFormatModel> codeRuleFormatList = coderuleentity.F_RuleFormatJson.ToList<CodeRuleFormatModel>();
                string dateFormatStr = "";
                foreach (CodeRuleFormatModel codeRuleFormatEntity in codeRuleFormatList)
                {
                    switch (codeRuleFormatEntity.itemType.ToString())
                    {
                        //自定义项
                        case "0":
                            billCode = billCode + codeRuleFormatEntity.formatStr;
                            nextBillCode = nextBillCode + codeRuleFormatEntity.formatStr;
                            break;
                        //日期
                        case "1":
                            //日期字符串类型
                            dateFormatStr = codeRuleFormatEntity.formatStr;
                            billCode = billCode + DateTime.Now.ToString(codeRuleFormatEntity.formatStr.Replace("m", "M"));
                            nextBillCode = nextBillCode + DateTime.Now.ToString(codeRuleFormatEntity.formatStr.Replace("m", "M"));
                            break;
                        //流水号
                        case "2":
                            CodeRuleSeedEntity maxSeed = null;
                            CodeRuleSeedEntity codeRuleSeedEntity = null;
                            List<CodeRuleSeedEntity> seedList = (List<CodeRuleSeedEntity>)await codeRuleService.GetSeedList(coderuleentity.F_RuleId, userInfo);
                            maxSeed = seedList.Find(t => t.F_UserId.IsEmpty());
                            int seedStep = codeRuleFormatEntity.stepValue == null ? 1 : int.Parse(codeRuleFormatEntity.stepValue.ToString());//如果步长为空默认1
                            int initValue = codeRuleFormatEntity.initValue == null ? 1 : int.Parse(codeRuleFormatEntity.initValue.ToString());//如果初始值为空默认1
                            #region 处理流水号归0
                            // 首先确定最大种子是否未归0的
                            if (dateFormatStr.Contains("dd"))
                            {
                                if ((maxSeed.F_ModifyDate).ToDateString() != DateTime.Now.ToString("yyyy-MM-dd"))
                                {
                                    isOutTime = true;
                                    nowSerious = initValue;
                                    maxSeed.F_SeedValue = initValue + seedStep;
                                    maxSeed.F_ModifyDate = DateTime.Now;
                                }
                            }
                            else if (dateFormatStr.Contains("mm"))
                            {
                                if (((DateTime)maxSeed.F_ModifyDate).ToString("yyyy-MM") != DateTime.Now.ToString("yyyy-MM"))
                                {
                                    isOutTime = true;
                                    nowSerious = initValue;
                                    maxSeed.F_SeedValue = initValue + seedStep;
                                    maxSeed.F_ModifyDate = DateTime.Now;
                                }
                            }
                            else if (dateFormatStr.Contains("yy"))
                            {
                                if (((DateTime)maxSeed.F_ModifyDate).ToString("yyyy") != DateTime.Now.ToString("yyyy"))
                                {
                                    isOutTime = true;
                                    nowSerious = initValue;
                                    maxSeed.F_SeedValue = initValue + seedStep;
                                    maxSeed.F_ModifyDate = DateTime.Now;
                                }
                            }
                            #endregion
                            // 查找当前用户是否已有之前未用掉的种子做更新
                            codeRuleSeedEntity = seedList.Find(t => t.F_UserId == userInfo.F_UserId && t.F_RuleId == coderuleentity.F_RuleId && (t.F_CreateDate).ToDateString() == DateTime.Now.ToString("yyyy-MM-dd"));
                            string keyvalue = codeRuleSeedEntity == null ? "" : codeRuleSeedEntity.F_RuleSeedId;
                            if (isOutTime)
                            {
                                await codeRuleService.SaveSeed(maxSeed.F_RuleSeedId, maxSeed, userInfo);
                            }
                            else if (codeRuleSeedEntity == null)
                            {
                                nowSerious = (int)maxSeed.F_SeedValue;
                                maxSeed.F_SeedValue += seedStep;//种子加步长
                                await codeRuleService.SaveSeed(maxSeed.F_RuleSeedId, maxSeed, userInfo);
                            }
                            else
                            {
                                nowSerious = (int)codeRuleSeedEntity.F_SeedValue;
                            }
                            codeRuleSeedEntity = new CodeRuleSeedEntity();
                            codeRuleSeedEntity.F_SeedValue = nowSerious;
                            codeRuleSeedEntity.F_UserId = userInfo.F_UserId;
                            codeRuleSeedEntity.F_RuleId = coderuleentity.F_RuleId;
                            await codeRuleService.SaveSeed(keyvalue, codeRuleSeedEntity, userInfo);
                            // 最大种子已经过期
                            string seriousStr = new string('0', (int)(codeRuleFormatEntity.formatStr.Length - nowSerious.ToString().Length)) + nowSerious.ToString();
                            string NextSeriousStr = new string('0', (int)(codeRuleFormatEntity.formatStr.Length - nowSerious.ToString().Length)) + maxSeed.F_SeedValue.ToString();
                            billCode = billCode + seriousStr;
                            nextBillCode = nextBillCode + NextSeriousStr;
                            break;
                        //部门
                        case "3":
                            DepartmentEntity departmentEntity = await _departmentIBLL.GetEntity(userInfo.F_DepartmentId);
                            if (codeRuleFormatEntity.formatStr == "code")
                            {
                                billCode = billCode + departmentEntity.F_EnCode;
                                nextBillCode = nextBillCode + departmentEntity.F_EnCode;
                            }
                            else
                            {
                                billCode = billCode + departmentEntity.F_FullName;
                                nextBillCode = nextBillCode + departmentEntity.F_FullName;

                            }
                            break;
                        //公司
                        case "4":
                            CompanyEntity companyEntity = await _companyIBLL.GetEntity(userInfo.F_CompanyId);
                            if (codeRuleFormatEntity.formatStr == "code")
                            {
                                billCode = billCode + companyEntity.F_EnCode;
                                nextBillCode = nextBillCode + companyEntity.F_EnCode;
                            }
                            else
                            {
                                billCode = billCode + companyEntity.F_FullName;
                                nextBillCode = nextBillCode + companyEntity.F_FullName;
                            }
                            break;
                        //用户
                        case "5":
                            if (codeRuleFormatEntity.formatStr == "code")
                            {
                                billCode = billCode + userInfo.F_EnCode;
                                nextBillCode = nextBillCode + userInfo.F_EnCode;
                            }
                            else
                            {
                                billCode = billCode + userInfo.F_Account;
                                nextBillCode = nextBillCode + userInfo.F_Account;
                            }
                            break;
                        default:
                            break;
                    }
                }
                coderuleentity.F_CurrentNumber = nextBillCode;
                await codeRuleService.SaveEntity(coderuleentity.F_RuleId, coderuleentity);
            }
            return billCode;

        }
        /// <summary>
        /// 占用单据号
        /// </summary>
        /// <param name="enCode">单据编码</param>
        /// <param name="account">用户账号</param>
        /// <returns>true/false</returns>
        public async Task UseRuleSeed(string enCode,string account = "")
        {
            CodeRuleEntity codeRuleSeedEntity = await GetEntityByCode(enCode);
            if (codeRuleSeedEntity != null)
            {
                if (string.IsNullOrEmpty(account))
                {
                    var userInfo = await this.CurrentUser();
                    //删除用户已经用掉的种子
                    await codeRuleService.DeleteSeed(userInfo.F_UserId, codeRuleSeedEntity.F_RuleId);
                }
                else
                {
                    var userInfo2 = await _userIBLL.GetEntityByAccount(account);
                    await codeRuleService.DeleteSeed(userInfo2.F_UserId, codeRuleSeedEntity.F_RuleId);
                }
            }
        }
        #endregion
    }
}
