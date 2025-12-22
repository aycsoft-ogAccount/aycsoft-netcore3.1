using ce.autofac.extension;
using Dapper;
using aycsoft.database;
using aycsoft.iapplication;
using aycsoft.util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace aycsoft.application
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2022.11.22
    /// 描 述：表单模板
    /// </summary>
    public class FormSchemeBLL :BLLBase, FormSchemeIBLL,BLL
    {
        private readonly FormSchemeService formSchemeService = new FormSchemeService();

        #region 获取数据
        /// <summary>
        /// 获取自定义表单列表
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<FormSchemeInfoEntity>> GetCustmerSchemeInfoList()
        {
            return formSchemeService.GetCustmerSchemeInfoList();
        }
        /// <summary>
        /// 获取表单分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="category">分类</param>
        /// <returns></returns>
        public Task<IEnumerable<FormSchemeInfoEntity>> GetSchemeInfoPageList(Pagination pagination, string keyword, string category)
        {
            return formSchemeService.GetSchemeInfoPageList(pagination, keyword, category);
        }
        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        public Task<IEnumerable<FormSchemeEntity>> GetSchemePageList(Pagination pagination, string schemeInfoId)
        {
            return formSchemeService.GetSchemePageList(pagination, schemeInfoId);
        }
        /// <summary>
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<FormSchemeInfoEntity> GetSchemeInfoEntity(string keyValue)
        {
            return formSchemeService.GetSchemeInfoEntity(keyValue);
        }
        /// <summary>
        /// 获取模板的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<FormSchemeEntity> GetSchemeEntity(string keyValue)
        {
            return formSchemeService.GetSchemeEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task Delete(string keyValue)
        {
            await formSchemeService.Delete(keyValue);
        }
        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfoEntity">模板基础信息</param>
        /// <param name="schemeEntity">模板信息</param>
        public async Task SaveEntity(string keyValue, FormSchemeInfoEntity schemeInfoEntity, FormSchemeEntity schemeEntity)
        {
            await formSchemeService.SaveEntity(keyValue, schemeInfoEntity, schemeEntity);
        }
        /// <summary>
        /// 保存模板基础信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfoEntity">模板基础信息</param>
        public async Task SaveSchemeInfoEntity(string keyValue, FormSchemeInfoEntity schemeInfoEntity)
        {
            await formSchemeService.SaveSchemeInfoEntity(keyValue, schemeInfoEntity);
        }
        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="schemeId">模板主键</param>
        public async Task UpdateScheme(string schemeInfoId, string schemeId)
        {
            await formSchemeService.UpdateScheme(schemeInfoId, schemeId);
        }
        /// <summary>
        /// 更新自定义表单模板状态
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        public async Task UpdateState(string schemeInfoId, int state)
        {
            await formSchemeService.UpdateState(schemeInfoId, state);
        }
        #endregion

        #region 扩展方法

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="sql">数据权限sql</param>
        /// <returns></returns>
        public async Task<DataTable> GetFormPageList(string schemeInfoId, Pagination pagination, string queryJson, string sql)
        {
            FormSchemeInfoEntity formSchemeInfoEntity = await GetSchemeInfoEntity(schemeInfoId);
            FormSchemeEntity formSchemeEntity = await GetSchemeEntity(formSchemeInfoEntity.F_SchemeId);
            FormSchemeModel formSchemeModel = formSchemeEntity.F_Scheme.ToObject<FormSchemeModel>();
            var queryParam = queryJson.ToJObject();
            string querySql = GetQuerySql(formSchemeModel, queryParam);
            var parameters = new DynamicParameters();
            foreach (var item in queryParam.Properties())
            {
                parameters.Add("@" + item.Name, item.Value);
            }
            if (!string.IsNullOrEmpty(sql))
            {
                querySql += "AND" + sql;
            }
            return await formSchemeService.BaseRepository(formSchemeModel.dbId).FindTable(querySql, parameters, pagination);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="sql">数据权限sql</param>
        /// <returns></returns>
        public async Task<DataTable> GetFormList(string schemeInfoId, string queryJson, string sql)
        {
            FormSchemeInfoEntity formSchemeInfoEntity = await GetSchemeInfoEntity(schemeInfoId);
            FormSchemeEntity formSchemeEntity = await GetSchemeEntity(formSchemeInfoEntity.F_SchemeId);
            FormSchemeModel formSchemeModel = formSchemeEntity.F_Scheme.ToObject<FormSchemeModel>();

            var queryParam = queryJson.ToJObject();
            
            string querySql = GetQuerySql(formSchemeModel, queryParam);
            var parameters = new DynamicParameters();
            foreach (var item in queryParam.Properties())
            {
                parameters.Add("@" + item.Name, item.Value);
            }

            if (!string.IsNullOrEmpty(sql)) {
                querySql += "AND" + sql;
            }
            
            return await formSchemeService.BaseRepository(formSchemeModel.dbId).FindTable(querySql, parameters);
        }
        /// <summary>
        /// 获取自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public async Task<Dictionary<string, DataTable>> GetInstanceForm(string schemeInfoId, string keyValue)
        {
            Dictionary<string, DataTable> res = new Dictionary<string, DataTable>();

            FormSchemeInfoEntity formSchemeInfoEntity = await GetSchemeInfoEntity(schemeInfoId);
            FormSchemeEntity formSchemeEntity = await GetSchemeEntity(formSchemeInfoEntity.F_SchemeId);
            FormSchemeModel formSchemeModel = formSchemeEntity.F_Scheme.ToObject<FormSchemeModel>();

            // 确定主从表之间的关系
            List<TreeModelEx<FormTableModel>> TableTree = new List<TreeModelEx<FormTableModel>>();// 从表
            foreach (var table in formSchemeModel.dbTable)
            {
                TreeModelEx<FormTableModel> treeone = new TreeModelEx<FormTableModel>();
                treeone.data = table;
                treeone.id = table.name;
                treeone.parentId = table.relationName;
                if (string.IsNullOrEmpty(table.relationName))
                {
                    treeone.parentId = "0";
                }
                TableTree.Add(treeone);
            }
            TableTree = TableTree.ToTree();

            // 确定表与组件之间的关系
            Dictionary<string, List<FormCompontModel>> tableComponts = new Dictionary<string, List<FormCompontModel>>();
            foreach (var tab in formSchemeModel.data)
            {
                foreach (var compont in tab.componts)
                {
                    if (!string.IsNullOrEmpty(compont.table))
                    {
                        if (!tableComponts.ContainsKey(compont.table))
                        {
                            tableComponts[compont.table] = new List<FormCompontModel>();
                        }
                        if (compont.type == "girdtable")
                        {
                            foreach (var item in compont.fieldsData)
                            {
                                if (!string.IsNullOrEmpty(item.field))
                                {
                                    FormCompontModel _compont = new FormCompontModel();
                                    _compont.field = item.field;
                                    _compont.id = item.field;
                                    if (item.type != "guid")
                                    {
                                        _compont.type = "girdfiled";
                                        tableComponts[compont.table].Add(_compont);
                                    }
                                }
                            }
                        }
                        else
                        {
                            tableComponts[compont.table].Add(compont);
                        }
                    }
                }
            }
            await GetInstanceTableData(TableTree, tableComponts, formSchemeModel.dbId, keyValue, null, res);
            return res;
        }
        /// <summary>
        /// 获取自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="processIdName">流程实例关联字段名</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public async Task<Dictionary<string, DataTable>> GetInstanceForm(string schemeInfoId, string processIdName, string keyValue)
        {
            Dictionary<string, DataTable> res = new Dictionary<string, DataTable>();

            FormSchemeInfoEntity formSchemeInfoEntity = await GetSchemeInfoEntity(schemeInfoId);
            FormSchemeEntity formSchemeEntity = await GetSchemeEntity(formSchemeInfoEntity.F_SchemeId);
            FormSchemeModel formSchemeModel = formSchemeEntity.F_Scheme.ToObject<FormSchemeModel>();

            // 确定主从表之间的关系
            List<TreeModelEx<FormTableModel>> TableTree = new List<TreeModelEx<FormTableModel>>();// 从表
            foreach (var table in formSchemeModel.dbTable)
            {
                TreeModelEx<FormTableModel> treeone = new TreeModelEx<FormTableModel>();
                treeone.data = table;
                treeone.id = table.name;
                treeone.parentId = table.relationName;
                if (string.IsNullOrEmpty(table.relationName))
                {
                    treeone.parentId = "0";
                }
                TableTree.Add(treeone);
            }
            TableTree = TableTree.ToTree();

            // 确定表与组件之间的关系
            Dictionary<string, List<FormCompontModel>> tableComponts = new Dictionary<string, List<FormCompontModel>>();
            foreach (var tab in formSchemeModel.data)
            {
                foreach (var compont in tab.componts)
                {
                    if (!string.IsNullOrEmpty(compont.table))
                    {
                        if (!tableComponts.ContainsKey(compont.table))
                        {
                            tableComponts[compont.table] = new List<FormCompontModel>();
                        }
                        if (compont.type == "girdtable")
                        {
                            foreach (var item in compont.fieldsData)
                            {
                                if (!string.IsNullOrEmpty(item.field))
                                {
                                    FormCompontModel _compont = new FormCompontModel();
                                    _compont.field = item.field;
                                    _compont.id = item.field;
                                    if (item.type != "guid")
                                    {
                                        _compont.type = "girdfiled";
                                        tableComponts[compont.table].Add(_compont);
                                    }
                                }
                            }
                        }
                        else
                        {
                            tableComponts[compont.table].Add(compont);
                        }
                    }
                }
            }
            await GetInstanceTableData(TableTree, tableComponts, formSchemeModel.dbId, keyValue, processIdName, null, res);
            return res;
        }
        
        /// <summary>
        /// 保存自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">表单模板主键</param>
        /// <param name="processIdName">流程关联字段名</param>
        /// <param name="keyValue">数据主键值</param>
        /// <param name="formData">自定义表单数据</param>
        public async Task SaveInstanceForm(string schemeInfoId, string processIdName, string keyValue, string formData)
        {
            FormSchemeInfoEntity schemeInfoEntity = await GetSchemeInfoEntity(schemeInfoId);
            FormSchemeEntity schemeEntity = await GetSchemeEntity(schemeInfoEntity.F_SchemeId);
            FormSchemeModel formSchemeModel = schemeEntity.F_Scheme.ToObject<FormSchemeModel>();

            #region 主从表分类
            List<FormTableModel> cTableList = new List<FormTableModel>();// 从表
            foreach (var table in formSchemeModel.dbTable)
            {
                if (string.IsNullOrEmpty(table.relationName))
                {
                    formSchemeModel.mainTableName = table.name;
                    formSchemeModel.mainTablePkey = table.field;
                }
                else
                {
                    cTableList.Add(table);
                }
            }
            #endregion

            #region 表单组件按表进行分类
            List<string> ruleCodes = new List<string>();

            string processIdFiled = "";
            Dictionary<string, List<FormCompontModel>> tableMap = new Dictionary<string, List<FormCompontModel>>();
            Dictionary<string, FormCompontModel> girdTableMap = new Dictionary<string, FormCompontModel>();// 从表
            foreach (var tab in formSchemeModel.data)
            {
                foreach (var compont in tab.componts)
                {
                    if (compont.id == processIdName)
                    {
                        processIdFiled = compont.field;
                    }
                    if (!string.IsNullOrEmpty(compont.table))
                    {
                        if (!tableMap.ContainsKey(compont.table))
                        {
                            tableMap[compont.table] = new List<FormCompontModel>();
                        }
                        if (compont.type == "girdtable")
                        {
                            girdTableMap.Add(compont.table, compont);
                            foreach (var item in compont.fieldsData)
                            {
                                FormCompontModel _compont = new FormCompontModel();
                                _compont.field = item.field;
                                _compont.id = item.field;
                                if (item.type == "guid")
                                {
                                    _compont.type = "girdguid";
                                }
                                tableMap[compont.table].Add(_compont);
                            }
                        }
                        else
                        {
                            if (compont.type == "encode")
                            {
                                ruleCodes.Add(compont.rulecode);
                            }
                            tableMap[compont.table].Add(compont);
                        }
                    }
                }
            }
            #endregion

            #region 数据保存或更新
            var db = formSchemeService.BaseRepository(formSchemeModel.dbId).BeginTrans();
            try
            {
                var formDataJson = formData.ToJObject();
                if (string.IsNullOrEmpty(keyValue))
                {
                    await InsertSql(db, formSchemeModel.mainTableName, tableMap, formDataJson);
                    foreach (FormTableModel table in cTableList)
                    {
                        string _value = "";
                        string _filed = "";
                        foreach (var compont in tableMap[table.relationName])
                        {
                            if (compont.field == table.relationField)
                            {
                                FormCompontModel newcompont = new FormCompontModel();
                                if (girdTableMap.ContainsKey(table.name))
                                {
                                    newcompont.id = table.field;
                                }
                                else
                                {
                                    newcompont.id = compont.id;
                                }
                                newcompont.field = table.field;
                                _filed = table.field;
                                _value = formDataJson[compont.id].ToString();
                                tableMap[table.name].Add(newcompont);
                                break;
                            }
                        }
                        if (girdTableMap.ContainsKey(table.name))
                        {
                            // 编辑表格
                            List<JObject> girdDataJson = formDataJson[girdTableMap[table.name].id].ToString().ToObject<List<JObject>>();
                            foreach (var girdData in girdDataJson)
                            {
                                girdData.Add(_filed, _value);
                                await InsertSql(db, table.name, tableMap, girdData);
                            }
                        }
                        else
                        {
                            await InsertSql(db, table.name, tableMap, formDataJson);
                        }
                    }
                }
                else
                {
                    // 更新主表数据
                    string mainTablePkey = formSchemeModel.mainTablePkey;
                    if (!string.IsNullOrEmpty(processIdFiled))
                    {
                        mainTablePkey = processIdFiled;
                    }
                    await UpdateSql(db, formSchemeModel.mainTableName, mainTablePkey, keyValue, tableMap, formDataJson);
                    foreach (FormTableModel table in cTableList)
                    {
                        if (girdTableMap.ContainsKey(table.name))
                        {
                            string _value = "";
                            string _filed = "";
                            foreach (var compont in tableMap[table.relationName])
                            {
                                if (compont.field == table.relationField)
                                {
                                    FormCompontModel newcompont = new FormCompontModel();
                                    newcompont.id = table.field;
                                    newcompont.field = table.field;
                                    _filed = table.field;
                                    _value = formDataJson[compont.id].ToString();
                                    tableMap[table.name].Add(newcompont);

                                    string strSql = " DELETE FROM " + table.name + " WHERE " + table.field + " = '" + formDataJson[compont.id].ToString() + "' ";
                                    await db.ExecuteSql(strSql);
                                    break;
                                }
                            }


                            // 编辑表格
                            List<JObject> girdDataJson = formDataJson[girdTableMap[table.name].id].ToString().ToObject<List<JObject>>();
                            foreach (var girdData in girdDataJson)
                            {
                                girdData.Add(_filed, _value);
                                await InsertSql(db, table.name, tableMap, girdData);
                            }
                        }
                        else
                        {
                            foreach (var compont in tableMap[table.relationName])
                            {
                                if (compont.field == table.relationField)
                                {
                                   await UpdateSql(db, table.name, table.field, formDataJson[compont.id].ToString(), tableMap, formDataJson);
                                }
                            }
                        }
                    }
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
            #endregion

            #region 占用单据编号
            foreach (string ruleCode in ruleCodes)
            {
               await this.UseRuleSeed(ruleCode);
            }
            #endregion
        }
        /// <summary>
        /// 删除自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">表单模板主键</param>
        /// <param name="keyValue">数据主键值</param>
        public async Task DeleteInstanceForm(string schemeInfoId, string keyValue)
        {
            FormSchemeInfoEntity formSchemeInfoEntity = await GetSchemeInfoEntity(schemeInfoId);
            FormSchemeEntity formSchemeEntity = await GetSchemeEntity(formSchemeInfoEntity.F_SchemeId);
            FormSchemeModel formSchemeModel = formSchemeEntity.F_Scheme.ToObject<FormSchemeModel>();

            // 确定主从表之间的关系
            List<TreeModelEx<FormTableModel>> TableTree = new List<TreeModelEx<FormTableModel>>();// 从表
            foreach (var table in formSchemeModel.dbTable)
            {
                TreeModelEx<FormTableModel> treeone = new TreeModelEx<FormTableModel>();
                treeone.data = table;
                treeone.id = table.name;
                treeone.parentId = table.relationName;
                if (string.IsNullOrEmpty(table.relationName))
                {
                    treeone.parentId = "0";
                }
                TableTree.Add(treeone);
            }
            TableTree = TableTree.ToTree();

            // 确定表与组件之间的关系
            Dictionary<string, List<FormCompontModel>> tableComponts = new Dictionary<string, List<FormCompontModel>>();
            foreach (var tab in formSchemeModel.data)
            {
                foreach (var compont in tab.componts)
                {
                    if (!string.IsNullOrEmpty(compont.table))
                    {
                        if (!tableComponts.ContainsKey(compont.table))
                        {
                            tableComponts[compont.table] = new List<FormCompontModel>();
                        }
                        tableComponts[compont.table].Add(compont);
                    }
                }
            }
            var db = formSchemeService.BaseRepository(formSchemeModel.dbId).BeginTrans();
            try
            {
                await DeleteInstanceTable(TableTree, tableComponts, formSchemeModel.dbId, keyValue, null, db);
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }


        /// <summary>
        /// 获取查询sql语句
        /// </summary>
        /// <param name="formSchemeModel">表单模板设置信息</param>
        /// <param name="queryParam">查询参数</param>
        /// <returns></returns>
        private string GetQuerySql(FormSchemeModel formSchemeModel, JObject queryParam)
        {
            string querySql = "";
            string fieldSql = "";
            Dictionary<string, string> girdTable = new Dictionary<string, string>();  // 表格绑定的表
            Dictionary<string, FormCompontModel> compontMap = new Dictionary<string, FormCompontModel>();
            Dictionary<string, string> tableMap = new Dictionary<string, string>();

            int _index = 0;
            foreach (var tab in formSchemeModel.data)
            {
                foreach (var compont in tab.componts)
                {
                    if (!string.IsNullOrEmpty(compont.table) && !tableMap.ContainsKey(compont.table))
                    {
                        tableMap.Add(compont.table, _index.ToString());
                        _index++;
                    }

                    if (compont.type == "girdtable")
                    {
                        if (!girdTable.ContainsKey(compont.table))
                        {
                            girdTable.Add(compont.table, "1");
                        }
                    }
                    else if (!string.IsNullOrEmpty(compont.field))
                    {
                        compontMap.Add(compont.id, compont);
                        fieldSql += compont.table + "tt." + compont.field + " as " + compont.field + tableMap[compont.table] + ",";
                    }
                }
            }
            // 确定主表
            List<FormTableModel> cTableList = new List<FormTableModel>();// 从表
            foreach (var table in formSchemeModel.dbTable)
            {
                if (string.IsNullOrEmpty(table.relationName))
                {
                    formSchemeModel.mainTableName = table.name;
                    formSchemeModel.mainTablePkey = table.field;
                }
                else if (!girdTable.ContainsKey(table.name))
                {
                    cTableList.Add(table);
                }
            }
            fieldSql = fieldSql.Remove(fieldSql.Length - 1, 1);
            //if (fieldSql.IndexOf(formSchemeModel.mainTableName + "tt." + formSchemeModel.mainTablePkey + " ") == -1)
            //{
            //    fieldSql += "," + formSchemeModel.mainTableName + "tt." + formSchemeModel.mainTablePkey;
            //}

            querySql += " SELECT " + fieldSql + " FROM  " + formSchemeModel.mainTableName + " " + formSchemeModel.mainTableName + "tt ";

            foreach (var ctable in cTableList)
            {
                querySql += " LEFT JOIN  " + ctable.name + " " + ctable.name + "tt " + "  ON " + ctable.name + "tt." + ctable.field + " = " + ctable.relationName + "tt." + ctable.relationField + " ";
            }
            querySql += "  where 1=1 ";

            JObject queryParamTemp = new JObject();
            if (queryParam != null && !queryParam["lrdateField"].IsEmpty())
            {
                queryParamTemp.Add("lrbegin", queryParam["lrbegin"].ToDate());
                queryParamTemp.Add("lrend", queryParam["lrend"].ToDate());
                querySql += " AND (" + formSchemeModel.mainTableName + "tt." + queryParam["lrdateField"].ToString() + " >=@lrbegin AND " + formSchemeModel.mainTableName + "tt." + queryParam["lrdateField"].ToString() + " <=@lrend ) ";
            }
            else if (queryParam != null) // 复合条件查询
            {

                foreach (var item in queryParam)
                {
                    if (!string.IsNullOrEmpty(item.Value.ToString()))
                    {

                        if (compontMap[item.Key].type == "radio" || compontMap[item.Key].type == "select" || compontMap[item.Key].type == "datetimerange" || compontMap[item.Key].type == "organize")
                        {
                            queryParamTemp.Add(GetFieldAlias(compontMap, tableMap, item.Key), item.Value);
                            querySql += " AND " + compontMap[item.Key].table + "tt." + compontMap[item.Key].field + " = @" + GetFieldAlias(compontMap, tableMap, item.Key);
                        }
                        else if (compontMap[item.Key].type == "checkbox")
                        {
                            querySql += " AND " + compontMap[item.Key].table + "tt." + compontMap[item.Key].field + " in ('" + item.Value.ToString().Replace(",", "','") + "')";
                        }
                        else
                        {
                            queryParamTemp.Add(GetFieldAlias(compontMap, tableMap, item.Key), "%" + item.Value + "%");
                            querySql += " AND " + compontMap[item.Key].table + "tt." + compontMap[item.Key].field + " like @" + GetFieldAlias(compontMap, tableMap, item.Key);
                        }
                    }
                }
            }
            queryParam.RemoveAll();
            foreach (var item in queryParamTemp)
            {
                queryParam.Add(item.Key, item.Value);
            }


            return querySql;
        }
        /// <summary>
        /// 获取字段别名
        /// </summary>
        /// <param name="compontMap">组件映射表</param>
        /// <param name="tableMap">表的名别映射表</param>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        private string GetFieldAlias(Dictionary<string, FormCompontModel> compontMap, Dictionary<string, string> tableMap, string id) {
            FormCompontModel compont = compontMap[id];
            string res = compont.field + tableMap[compont.table];
            return res;
        }

        /// <summary>
        /// 获取自定义表单实例表的实体数据
        /// </summary>
        /// <param name="tableTree">主从表关系树</param>
        /// <param name="tableComponts">表与组件之间的关系</param>
        /// <param name="dbId">数据库主键</param>
        /// <param name="keyValue">主键值</param>
        /// <param name="pData">父表的数据值</param>
        /// <param name="res">结果数据</param>
        private async Task GetInstanceTableData(List<TreeModelEx<FormTableModel>> tableTree, Dictionary<string, List<FormCompontModel>> tableComponts, string dbId, string keyValue,Dictionary<string,string> pData, Dictionary<string, DataTable> res)
        {
            foreach (var tableItem in tableTree)
            {
                string querySql = " SELECT ";
                if (tableComponts.ContainsKey(tableItem.data.name) && !res.ContainsKey(tableItem.data.name))
                {
                    foreach (var compont in tableComponts[tableItem.data.name])
                    {
                        querySql += compont.field + " , ";
                    }

                    if (string.IsNullOrEmpty(keyValue))
                    {
                        keyValue = pData[tableItem.data.relationField];
                    }

                    querySql = querySql.Remove(querySql.Length - 2, 2);
                    querySql += " FROM " + tableItem.data.name + " WHERE " + tableItem.data.field + " = @keyValue";

                    DataTable dt = await formSchemeService.BaseRepository(dbId).FindTable(querySql, new { keyValue });
                    res.Add(tableItem.data.name, dt);

                    // 获取它的从表数据
                    if (tableItem.ChildNodes.Count > 0 && dt.Rows.Count > 0)
                    {
                        Dictionary<string, string> pDatatmp = new Dictionary<string, string>();
                        foreach (var compont in tableComponts[tableItem.data.name])
                        {
                            if (!pDatatmp.ContainsKey(compont.field))
                            {
                                pDatatmp.Add(compont.field, dt.Rows[0][compont.field].ToString());
                            }
                        }
                        await GetInstanceTableData(tableItem.ChildNodes, tableComponts, dbId, "", pDatatmp, res);
                    }

                }
            }
        }

        /// <summary>
        /// 获取自定义表单实例表的实体数据
        /// </summary>
        /// <param name="tableTree">主从表关系树</param>
        /// <param name="tableComponts">表与组件之间的关系</param>
        /// <param name="dbId">数据库主键</param>
        /// <param name="keyName">主键字段名字</param>
        /// <param name="keyValue">主键值</param>
        /// <param name="pData">父表的数据值</param>
        /// <param name="res">结果数据</param>
        private async Task GetInstanceTableData(List<TreeModelEx<FormTableModel>> tableTree, Dictionary<string, List<FormCompontModel>> tableComponts, string dbId, string keyValue,string keyName, Dictionary<string, string> pData, Dictionary<string, DataTable> res)
        {
            foreach (var tableItem in tableTree)
            {
                string querySql = " SELECT ";
                if (tableComponts.ContainsKey(tableItem.data.name) && !res.ContainsKey(tableItem.data.name))
                {
                    string tableName = "";
                    foreach (var compont in tableComponts[tableItem.data.name])
                    {
                        if (keyName == compont.id)
                        {
                            tableName = compont.field;
                        }
                        querySql += compont.field + " , ";
                    }

                    if (string.IsNullOrEmpty(keyValue))
                    {
                        keyValue = pData[tableItem.data.relationField];
                    }

                    querySql = querySql.Remove(querySql.Length - 2, 2);
                    querySql += " FROM " + tableItem.data.name + " WHERE " + tableName + " = @keyValue";

                    DataTable dt = await formSchemeService.BaseRepository(dbId).FindTable(querySql, new { keyValue });
                    res.Add(tableItem.data.name, dt);

                    // 获取它的从表数据
                    if (tableItem.ChildNodes.Count > 0 && dt.Rows.Count > 0)
                    {
                        Dictionary<string, string> pDatatmp = new Dictionary<string, string>();
                        foreach (var compont in tableComponts[tableItem.data.name])
                        {
                            if (!pDatatmp.ContainsKey(compont.field))
                            {
                                pDatatmp.Add(compont.field, dt.Rows[0][compont.field].ToString());
                            }
                        }
                        await GetInstanceTableData(tableItem.ChildNodes, tableComponts, dbId, "", pDatatmp, res);
                    }

                }
            }
        }
        
        /// <summary>
        /// 删除自定义表单实例表的实体数据
        /// </summary>
        /// <param name="tableTree">主从表关系树</param>
        /// <param name="tableComponts">表与组件之间的关系</param>
        /// <param name="dbId">数据库主键</param>
        /// <param name="keyValue">主键值</param>
        /// <param name="pData">父表的数据值</param>
        /// <param name="db">数据库连接</param>
        private async Task DeleteInstanceTable(List<TreeModelEx<FormTableModel>> tableTree, Dictionary<string, List<FormCompontModel>> tableComponts, string dbId, string keyValue, DataTable pData,IRepository db)
        {
            foreach (var tableItem in tableTree)
            {
                string querySql = "";
                if (tableComponts.ContainsKey(tableItem.data.name))
                {
                    if (string.IsNullOrEmpty(keyValue))
                    {
                        keyValue = pData.Rows[0][tableItem.data.relationField].ToString();
                    }

                    // 如果有子表需要先获取数据
                    if (tableItem.ChildNodes.Count > 0)
                    {
                        querySql = " SELECT * FROM " + tableItem.data.name + " WHERE " + tableItem.data.field + " = @keyValue";
                        DataTable dt = await formSchemeService.BaseRepository(dbId).FindTable(querySql, new { keyValue });
                        await DeleteInstanceTable(tableItem.ChildNodes, tableComponts, dbId, "", dt, db);
                    }

                    // 删除数据
                    querySql = " DELETE FROM " + tableItem.data.name + " WHERE " + tableItem.data.field + " = @keyValue";
                    await db.ExecuteSql(querySql, new { keyValue });
                }
            }
        }

        /// <summary>
        /// 新增数据sql语句
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="tableName">表名</param>
        /// <param name="tableMap">表名->组件映射</param>
        /// <param name="formDataJson">表单数据</param>
        /// <returns></returns>
        private async Task InsertSql(IRepository db, string tableName, Dictionary<string, List<FormCompontModel>> tableMap, JObject formDataJson)
        {
            if (tableMap.ContainsKey(tableName) && tableMap[tableName].Count > 0)
            {

                var list = (await db.GetDataBaseTableFields(tableName)).ToJson().ToList<DatabaseTableFieldModel>();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (var item in list)
                {
                    if (!dic.ContainsKey(item.f_column.ToUpper()))
                    {
                        dic.Add(item.f_column.ToUpper(), item.f_datatype);
                    }
                }
                var dp = new DynamicParameters(new { });
                string strSql = "INSERT INTO " + tableName + "( ";
                string sqlValue = " ( ";
                foreach (var item in tableMap[tableName])
                {
                    if (!string.IsNullOrEmpty(item.field) && !formDataJson[item.id].IsEmpty())
                    {
                        strSql += item.field + ",";
                        sqlValue += " @" + item.field + ",";
                        if (dic.ContainsKey(item.field.ToUpper()))
                        {
                            switch (dic[item.field.ToUpper()].ToUpper())
                            {
                                case "DATE":
                                    dp.Add(item.field, formDataJson[item.id].ToDate(), DbType.Date);
                                    break;
                                case "DATETIME":
                                    dp.Add(item.field, formDataJson[item.id].ToDate(), DbType.DateTime);
                                    break;
                                case "DATETIME2":
                                    dp.Add(item.field, formDataJson[item.id].ToDate(), DbType.DateTime2);
                                    break;
                                case "NUMBER":
                                case "INT":
                                case "FLOAT":
                                    dp.Add(item.field, formDataJson[item.id].ToDecimal(), DbType.Decimal);
                                    break;
                                default:
                                    dp.Add(item.field, formDataJson[item.id].ToString(), DbType.String);
                                    break;

                            }
                        }
                    }
                    else if (item.type == "girdguid")
                    {
                        strSql += item.field + ",";
                        sqlValue += " @" + item.field + ",";
                        dp.Add(item.field, Guid.NewGuid().ToString(), DbType.String);
                    }
                }
                strSql = strSql.Remove(strSql.Length - 1, 1);
                sqlValue = sqlValue.Remove(sqlValue.Length - 1, 1);

                strSql += " ) VALUES " + sqlValue + ")";

                await db.ExecuteSql(strSql, dp);
            }
        }
        /// <summary>
        /// 更新数据sql语句
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="tableName">表名</param>
        /// <param name="pkey">主键字段</param>
        /// <param name="pkeyValue">主键数据</param>
        /// <param name="tableMap">表名->组件映射</param>
        /// <param name="formDataJson">上传的数据</param>
        /// <returns></returns>
        private async Task UpdateSql(IRepository db, string tableName, string pkey, string pkeyValue, Dictionary<string, List<FormCompontModel>> tableMap, JObject formDataJson)
        {
            if (tableMap.ContainsKey(tableName) && tableMap[tableName].Count > 0)
            {
                var list = (await db.GetDataBaseTableFields(tableName)).ToJson().ToList<DatabaseTableFieldModel>();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (var item in list)
                {
                    if (!dic.ContainsKey(item.f_column.ToUpper()))
                    {
                        dic.Add(item.f_column.ToUpper(), item.f_datatype);
                    }
                }

                var dp = new DynamicParameters(new { });
                //List<FieldValueParam> fieldValueParamlist = new List<FieldValueParam>();
                string strSql = " UPDATE   " + tableName + " SET  ";
                foreach (var item in tableMap[tableName])
                {


                    if (!string.IsNullOrEmpty(item.field) && item.field != pkey && !formDataJson[item.id].IsEmpty())
                    {
                        strSql += item.field + "=@" + item.field + ",";
                        //FieldValueParam fieldValueParam = new FieldValueParam();
                        //fieldValueParam.name = item.field;

                        if (dic.ContainsKey(item.field.ToUpper()))
                        {
                            switch (dic[item.field.ToUpper()].ToUpper())
                            {
                                case "DATE":
                                    dp.Add(item.field, formDataJson[item.id].ToDate(), DbType.Date);
                                    break;
                                case "DATETIME":
                                    dp.Add(item.field, formDataJson[item.id].ToDate(), DbType.DateTime);
                                    break;
                                case "DATETIME2":
                                    dp.Add(item.field, formDataJson[item.id].ToDate(), DbType.DateTime2);
                                    break;
                                case "NUMBER":
                                case "INT":
                                case "FLOAT":
                                    dp.Add(item.field, formDataJson[item.id].ToDecimal(), DbType.Decimal);
                                    break;
                                default:
                                    dp.Add(item.field, formDataJson[item.id].ToString(), DbType.String);
                                    break;
                            }
                        }
                    }
                    else if (item.field != pkey && formDataJson[item.id] != null)
                    {
                        strSql += item.field + "= null,";
                    }
                }
                strSql = strSql.Remove(strSql.Length - 1, 1);
                strSql += " WHERE " + pkey + "='" + pkeyValue + "'";

                await db.ExecuteSql(strSql, dp);
            }
        }
        #endregion
    }
}
