using Dapper;
using aycsoft.iapplication;
using aycsoft.util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.10.22
    /// 描 述：语言映射
    /// </summary>
    public class LGMapService : ServiceBase
    {
        #region 构造函数和属性

        private readonly string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public LGMapService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_Name,
                t.F_Code,
                t.F_TypeCode
            ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="TypeCode">编码</param>
        /// <returns></returns>
        public Task<IEnumerable<LGMapEntity>> GetListByTypeCode(string TypeCode)
        {
            var sql = "select * from LR_Lg_Map where  F_TypeCode='" + TypeCode + "' AND  F_Code <>'' AND F_Code<>'undefined'";
            return this.BaseRepository().FindList<LGMapEntity>(sql);
        }

        /// <summary>
        /// 获取对应内容的翻译数据
        /// </summary>
        /// <param name="dataList">需要翻译的列表</param>
        /// <param name="typeList">语言类型列表</param>
        /// <returns></returns>
        public Task<DataTable> GetList(string dataList, string typeList)
        {

            string indataList = "'" + dataList.Replace(",", "','") + "'";
            string[] list = typeList.Split(',');
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append("m.F_Code, m.F_Name as " + list[0] + " ");
            for (var i = 1; i < list.Length; i++)
            {
                strSql.Append(", m" + i + ".F_Name as " + list[i] + " ");
            }
            strSql.Append(" FROM LR_Lg_Map m ");
            for (var j = 1; j < list.Length; j++)
            {
                strSql.Append("LEFT JOIN LR_Lg_Map m" + j + " ON m.F_Code=m" + j + ".F_Code AND m" + j + ".F_TypeCode='" + list[j] + "' ");
            }
            strSql.Append(" WHERE m.F_TypeCode = '" + list[0] + "'  AND  m.F_Code <>'' AND m.F_Code<>'undefined'");
            strSql.Append(" AND m.F_Name in (" + indataList + ")");
            return this.BaseRepository().FindTable(strSql.ToString());
        }

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="typeList">语言类型列表</param>
        /// <returns></returns>
        public Task<DataTable> GetPageList(Pagination pagination, string queryJson, string typeList)
        {
            string[] list = typeList.Split(',');
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append("m.F_Code, m.F_Name as " + list[0] + " ");
            for (var i = 1; i < list.Length; i++)
            {
                strSql.Append(", m" + i + ".F_Name as " + list[i] + " ");
            }
            strSql.Append(" FROM LR_Lg_Map m ");
            for (var j = 1; j < list.Length; j++)
            {
                strSql.Append("LEFT JOIN LR_Lg_Map m" + j + " ON m.F_Code=m" + j + ".F_Code AND m" + j + ".F_TypeCode='" + list[j] + "' ");
            }
            strSql.Append(" WHERE m.F_TypeCode = '" + list[0] + "'  AND  m.F_Code <>'' AND m.F_Code<>'undefined'");
            var queryParam = queryJson.ToJObject();
            // 虚拟参数
            var dp = new DynamicParameters(new { });
            if (queryParam.Property("keyword") != null && queryParam.Property("keyword").ToString() != "")
            {
                dp.Add("F_Name", "%" + queryParam["keyword"].ToString() + "%", DbType.String);
                strSql.Append(" AND m.F_Name like @F_Name ");
            }
            return this.BaseRepository().FindTable(strSql.ToString(), dp, pagination);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<LGMapEntity> GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntityByKey<LGMapEntity>(keyValue);
        }
        /// <summary>
        /// 根据名称获取列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public Task<IEnumerable<LGMapEntity>> GetListByName(string name)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Lg_Map t WHERE 1=1 AND  t.F_Code <>'' AND t.F_Code<>'undefined' AND t.F_Name='" + name + "'");
            return this.BaseRepository().FindList<LGMapEntity>(strSql.ToString());
        }
        /// <summary>
        /// 根据名称与类型获取列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="typeCode">typeCode</param>
        /// <returns></returns>
        public Task<IEnumerable<LGMapEntity>> GetListByNameAndType(string name, string typeCode)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(fieldSql);
            strSql.Append(" FROM LR_Lg_Map t WHERE 1=1 AND  t.F_Code <>'' AND t.F_Code<>'undefined' AND t.F_Name='" + name + "' AND t.F_TypeCode='" + typeCode + "'");
            return this.BaseRepository().FindList<LGMapEntity>(strSql.ToString());
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="code">一组语言编码</param>
        /// <returns></returns>
        public async Task DeleteEntity(string code)
        {
            await this.BaseRepository().DeleteAny<LGMapEntity>(new { F_Code = code });
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="nameList">原列表</param>
        /// <param name="newNameList">新列表</param>
        /// <param name="code">一组语言编码</param>
        /// <returns></returns>
        public async Task SaveEntity(string nameList, string newNameList, string code)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    //编辑
                    JObject jo = JObject.Parse(nameList);
                    IEnumerable<JProperty> properties = jo.Properties();
                    //将原列表存入字典
                    var dic = new Dictionary<string, string>();
                    foreach (JProperty item in properties)
                    {
                        dic.Add(item.Name, item.Value.ToString());
                    }

                    //新列表
                    JObject newjo = JObject.Parse(newNameList);
                    IEnumerable<JProperty> newproperties = newjo.Properties();
                    foreach (JProperty item in newproperties)
                    {
                        if (dic.ContainsKey(item.Name) && (item.Value.ToString() != dic[item.Name]))
                        {
                            var sql = "UPDATE LR_LG_MAP SET F_Name=@newName WHERE F_Name=@oldName AND F_Code=@code";
                            await db.ExecuteSql(sql, new { newName = item.Value.ToString(), oldName = dic[item.Name], code = code });
                        }
                        else if (!dic.ContainsKey(item.Name))
                        {
                            LGMapEntity entity = new LGMapEntity();
                            entity.F_Id = Guid.NewGuid().ToString();
                            entity.F_Name = item.Value.ToString();
                            entity.F_TypeCode = item.Name;
                            entity.F_Code = code;
                            await db.Insert<LGMapEntity>(entity);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {//新增
                    //转为可遍历对象
                    JObject jo = JObject.Parse(newNameList);
                    IEnumerable<JProperty> properties = jo.Properties();
                    var F_Code = Guid.NewGuid().ToString();
                    //新增每一项
                    foreach (JProperty item in properties)
                    {
                        LGMapEntity entity = new LGMapEntity();
                        entity.F_Id = Guid.NewGuid().ToString();
                        entity.F_Name = item.Value.ToString();
                        entity.F_TypeCode = item.Name;
                        entity.F_Code = F_Code;
                        await db.Insert<LGMapEntity>(entity);
                    }
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
