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
    /// 日 期：2020.04.07
    /// 描 述：我的常用移动应用 
    /// </summary> 
    public class MyFunctionService: ServiceBase
    {
        #region 构造函数和属性 

        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public MyFunctionService()
        {
            fieldSql = @" 
                t.F_Id, 
                t.F_UserId, 
                t.F_FunctionId,
                t.F_Sort
            ";
        }
        #endregion

        #region 获取数据 

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="userId">用户主键ID</param>
        /// <returns></returns>
        public Task<IEnumerable<MyFunctionEntity>> GetList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_App_MyFunction t where t.F_UserId = @userId Order by  t.F_Sort ");
            return this.BaseRepository().FindList<MyFunctionEntity>(strSql.ToString(), new { userId });
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="strFunctionId">功能id</param>
        /// <returns></returns>
        public async Task SaveEntity(string userId,string strFunctionId)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                string[] functionIds = strFunctionId.Split(',');
                await db.DeleteAny<MyFunctionEntity>(new { F_UserId = userId });
                int num = 0;
                foreach (var functionId in functionIds) {
                    MyFunctionEntity entity = new MyFunctionEntity
                    {
                        F_Id = Guid.NewGuid().ToString(),
                        F_UserId = userId,
                        F_FunctionId = functionId,
                        F_Sort = num
                    };
                    await db.Insert(entity);
                    num++;
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion
    }
}
