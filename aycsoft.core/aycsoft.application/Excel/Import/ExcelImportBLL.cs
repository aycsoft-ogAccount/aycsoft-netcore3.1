using ce.autofac.extension;
using Dapper;
using aycsoft.iapplication;
using aycsoft.util;
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
    /// 日 期：2022.09.19
    /// 描 述：Excel数据导入设置
    /// </summary>
    public class ExcelImportBLL : BLLBase, ExcelImportIBLL,BLL
    {
        private readonly ExcelImportService excelImportService = new ExcelImportService();

        private readonly DatabaseTableIBLL _databaseTableIBLL;
        private readonly DataItemIBLL _dataItemIBLL;
        private readonly DataSourceIBLL _dataSourceIBLL;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseTableIBLL"></param>
        /// <param name="dataItemIBLL"></param>
        /// <param name="dataSourceIBLL"></param>
        public ExcelImportBLL(DatabaseTableIBLL databaseTableIBLL,DataItemIBLL dataItemIBLL, DataSourceIBLL dataSourceIBLL) {
            _databaseTableIBLL = databaseTableIBLL;
            _dataItemIBLL = dataItemIBLL;
            _dataSourceIBLL = dataSourceIBLL;
        }

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件参数</param>
        /// <returns></returns>
        public Task<IEnumerable<ExcelImportEntity>> GetPageList(Pagination pagination, string queryJson)
        {
            return excelImportService.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取导入配置列表根据模块ID
        /// </summary>
        /// <param name="moduleId">功能模块主键</param>
        /// <returns></returns>
        public Task<IEnumerable<ExcelImportEntity>> GetList(string moduleId)
        {
            return excelImportService.GetList(moduleId);
        }
        /// <summary>
        /// 获取配置信息实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Task<ExcelImportEntity> GetEntity(string keyValue)
        {
            return excelImportService.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取配置字段列表
        /// </summary>
        /// <param name="importId">配置信息主键</param>
        /// <returns></returns>
        public Task<IEnumerable<ExcelImportFieldEntity>> GetFieldList(string importId)
        {
            return excelImportService.GetFieldList(importId);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public async Task DeleteEntity(string keyValue)
        {
            await excelImportService.DeleteEntity(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体数据</param>
        /// <param name="filedList">字段列表</param>
        /// <returns></returns>
        public async Task SaveEntity(string keyValue, ExcelImportEntity entity, List<ExcelImportFieldEntity> filedList)
        {
            await excelImportService.SaveEntity(keyValue, entity, filedList);
        }
        /// <summary>
        /// 更新配置主表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        public async Task UpdateEntity(string keyValue, ExcelImportEntity entity)
        {
            var userInfo = await this.CurrentUser();
            await excelImportService.UpdateEntity(keyValue, entity, userInfo);
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// excel 数据导入（未导入数据写入缓存）
        /// </summary>
        /// <param name="templateId">导入模板主键</param>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <returns></returns>
        public async Task<(DataTable elist, int snum,int fnum)> ImportTable(string templateId, string fileId, DataTable dt)
        {
            int snum = 0;
            int fnum = 0;
            // 创建一个datatable容器用于保存导入失败的数据
            DataTable failDt = new DataTable();
            dt.Columns.Add("导入错误", typeof(string));
            foreach (DataColumn dc in dt.Columns)
            {
                failDt.Columns.Add(dc.ColumnName, dc.DataType);
            }
            if (dt.Rows.Count > 0)
            {
                ExcelImportEntity entity = await GetEntity(templateId);
                List<ExcelImportFieldEntity> list = (List<ExcelImportFieldEntity>)await GetFieldList(templateId);
                if (entity != null && list.Count > 0)
                {
                    var userInfo = await this.CurrentUser();
                    // 获取当前表的所有字段
                    var fieldList = await _databaseTableIBLL.GetTableFiledList(entity.F_DbId, entity.F_DbTable);
                    Dictionary<string, string> fieldMap = new Dictionary<string, string>();
                    foreach (var field in fieldList)// 遍历字段设置每个字段的数据类型
                    {
                        fieldMap.Add(field.f_column, field.f_datatype);
                    }

                    // 拼接导入sql语句
                    string sql = " INSERT INTO " + entity.F_DbTable + " (";
                    string sqlValue = "(";
                    bool isfirt = true;

                    foreach (var field in list)
                    {
                        if (!isfirt)
                        {
                            sql += ",";
                            sqlValue += ",";
                        }
                        sql += field.F_Name;
                        sqlValue += "@" + field.F_Name;
                        isfirt = false;
                    }
                    sql += " ) VALUES " + sqlValue + ")";
                    string sqlonly = " select * from " + entity.F_DbTable + " where ";

                    // 数据字典数据
                    Dictionary<string, List<DataItemDetailEntity>> dataItemMap = new Dictionary<string, List<DataItemDetailEntity>>();
                    // 循环遍历导入
                    foreach (DataRow dr in dt.Rows)
                    {

                        try
                        {
                            var dp = new DynamicParameters(new { });
                            foreach (var col in list)
                            {
                                string paramName = "@" + col.F_Name;
                                DbType dbType = _databaseTableIBLL.FindModelsType(fieldMap[col.F_Name]).ToDbType();

                                switch (col.F_RelationType)
                                {
                                    case 0://无关联
                                        dp.Add(col.F_Name, dr[col.F_ColName].ToString(), dbType);
                                        await IsOnlyOne(col, sqlonly, dr[col.F_ColName].ToString(), entity.F_DbId, dbType);
                                        break;
                                    case 1://GUID
                                        dp.Add(col.F_Name, Guid.NewGuid().ToString(), dbType);
                                        break;
                                    case 2://数据字典
                                        string dataItemName = "";
                                        if (!dataItemMap.ContainsKey(col.F_DataItemCode))
                                        {
                                            List<DataItemDetailEntity> dataItemList = (List<DataItemDetailEntity>)await _dataItemIBLL.GetDetailList(col.F_DataItemCode);
                                            dataItemMap.Add(col.F_DataItemCode, dataItemList);
                                        }
                                        dataItemName = FindDataItemValue(dataItemMap[col.F_DataItemCode], dr[col.F_ColName].ToString(), col.F_ColName);
                                        dp.Add(col.F_Name, dataItemName, dbType);
                                        await IsOnlyOne(col, sqlonly, dataItemName, entity.F_DbId, dbType);
                                        break;
                                    case 3://数据表
                                        string v = "";
                                        try
                                        {
                                            string[] dataSources = col.F_DataSourceId.Split(',');
                                            string queryJson = "{" + dataSources[1] + ":\"" + dr[col.F_ColName].ToString() + "\"}";
                                            DataTable sourceDt = await _dataSourceIBLL.GetDataTable(dataSources[0], queryJson);
                                            v = sourceDt.Rows[0][0].ToString();
                                            dp.Add(col.F_Name, v, dbType);
                                        }
                                        catch (Exception)
                                        {
                                            throw (new Exception("【" + col.F_ColName + "】 找不到对应的数据"));
                                        }
                                        await IsOnlyOne(col, sqlonly, v, entity.F_DbId, dbType);
                                        break;
                                    case 4://固定值
                                        dp.Add(col.F_Name, col.F_Value, dbType);
                                        break;
                                    case 5://操作人ID
                                        dp.Add(col.F_Name, userInfo.F_UserId, dbType);
                                        break;
                                    case 6://操作人名字
                                        dp.Add(col.F_Name, userInfo.F_RealName, dbType);
                                        break;
                                    case 7://操作时间
                                        dp.Add(col.F_Name, DateTime.Now, dbType);
                                        break;
                                }
                            }
                            await excelImportService.BaseRepository(entity.F_DbId).ExecuteSql(sql, dp);
                            snum++;
                        }
                        catch (Exception ex)
                        {
                            fnum++;
                            if (entity.F_ErrorType == 0)// 如果错误机制是终止
                            {
                                dr["导入错误"] = ex.Message + "【之后数据未被导入】";
                                failDt.Rows.Add(dr.ItemArray);
                                break;
                            }
                            else
                            {
                                dr["导入错误"] = ex.Message;
                                failDt.Rows.Add(dr.ItemArray);
                            }
                        }
                    }
                }
            }
            return (failDt, snum, fnum);
        }

        /// <summary>
        /// 数据字典查找Value
        /// </summary>
        /// <param name="dataItemList">数据字典数据</param>
        /// <param name="itemName">项目名</param>
        /// <param name="colName">列名</param>
        /// <returns></returns>
        private string FindDataItemValue(List<DataItemDetailEntity> dataItemList, string itemName, string colName)
        {
            DataItemDetailEntity dataItem = dataItemList.Find(t => t.F_ItemName == itemName);
            if (dataItem != null)
            {
                return dataItem.F_ItemValue;
            }
            else
            {
                throw (new Exception("【" + colName + "】数据字典找不到对应值"));
            }
        }

        /// <summary>
        /// 判断是否数据有重复
        /// </summary>
        /// <param name="col"></param>
        /// <param name="sqlonly"></param>
        /// <param name="value"></param>
        /// <param name="dbId"></param>
        /// <param name="dbType"></param>
        private async Task IsOnlyOne(ExcelImportFieldEntity col, string sqlonly, string value, string dbId, DbType dbType)
        {
            if (col.F_OnlyOne == 1)
            {
                var dp = new DynamicParameters(new { });
                sqlonly += col.F_Name + " = @" + col.F_Name;
                dp.Add(col.F_Name, value, dbType);
                var d = await excelImportService.BaseRepository(dbId).FindTable(sqlonly, dp);
                if (d.Rows.Count > 0)
                {
                    throw new Exception("【" + col.F_ColName + "】此项数据不能重复");
                }
            }
        }
        #endregion
    }
}
