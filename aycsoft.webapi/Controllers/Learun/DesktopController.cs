using aycsoft.iapplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aycsoft.webapi.Controllers
{
    /// <summary>
    /// 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架
    /// Copyright (c) 2021-present 广州轻创软件信息科技有限公司
    /// 创建人：young
    /// 日 期：2020.04.22
    /// 描 述：桌面配置接口
    /// </summary>
    public class DesktopController : MvcControllerBase
    {
        private readonly DTTargetIBLL _dTTargetIBLL;
        private readonly DTListIBLL _dTListIBLL;
        private readonly DTChartIBLL _dTChartIBLL;
        private readonly DataSourceIBLL _dataSourceIBLL;
        private readonly DTImgIBLL _dTImgIBLL;
        private readonly ImgIBLL _imgIBLL;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dTTargetIBLL">统计数据接口</param>
        /// <param name="dTListIBLL">列表接口</param>
        /// <param name="dTChartIBLL">图标接口</param>
        /// <param name="dataSourceIBLL">数据库连接接口</param>
        /// <param name="dTImgIBLL">桌面图片接口</param>
        /// <param name="imgIBLL">图片接口</param>
        public DesktopController(DTTargetIBLL dTTargetIBLL, DTListIBLL dTListIBLL, DTChartIBLL dTChartIBLL, DataSourceIBLL dataSourceIBLL, DTImgIBLL dTImgIBLL, ImgIBLL imgIBLL)
        {
            _dTTargetIBLL = dTTargetIBLL;
            _dTListIBLL = dTListIBLL;
            _dTChartIBLL = dTChartIBLL;
            _dataSourceIBLL = dataSourceIBLL;
            _dTImgIBLL = dTImgIBLL;
            _imgIBLL = imgIBLL;
        }

        /// <summary>
        /// 获取桌面配置信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Setting()
        {
            var target = await _dTTargetIBLL.GetList();
            var list = await _dTListIBLL.GetList();
            var chart = await _dTChartIBLL.GetList();

            var data = new
            {
                target,
                list,
                chart
            };
            return Success(data);
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="type">类型 target,chart,list </param>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Data(string type, string id)
        {
            switch (type)
            {
                case "target":
                    var data = await _dTTargetIBLL.GetEntity(id);
                    if (data != null && !string.IsNullOrEmpty(data.F_Sql))
                    {
                        var dt = await _dataSourceIBLL.GetDataTableBySql(data.F_DataSourceId, data.F_Sql);
                        var jsonData2 = new
                        {
                            id,
                            value = dt.Rows[0][0]
                        };
                        return Success(dt);
                    }
                    else
                    {
                        var jsonData = new
                        {
                            id,
                            value = ""
                        };
                        return Success(jsonData);
                    }
                case "chart":
                    var chartData = await _dTChartIBLL.GetEntity(id);
                    if (chartData != null && !string.IsNullOrEmpty(chartData.F_Sql))
                    {
                        var dt = await _dataSourceIBLL.GetDataTableBySql(chartData.F_DataSourceId, chartData.F_Sql);
                        var jsonData2 = new
                        {
                            id,
                            value = dt
                        };
                        return Success(jsonData2);
                    }
                    else
                    {
                        var jsonData = new
                        {
                            id
                        };
                        return Success(jsonData);
                    }
                case "list":
                    var listdata = await _dTListIBLL.GetEntity(id);
                    if (listdata != null && !string.IsNullOrEmpty(listdata.F_Sql))
                    {
                        var dt = await _dataSourceIBLL.GetDataTableBySql(listdata.F_DataSourceId, listdata.F_Sql);
                        var jsonData2 = new
                        {
                            id,
                            value = dt
                        };
                        return Success(jsonData2);
                    }
                    else
                    {
                        var jsonData = new
                        {
                            id
                        };
                        return Success(jsonData);
                    }
            }

            return Success(new
            {
                Id = id
            });
        }

        /// <summary>
        /// 获取桌首页图片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Imgid()
        {

            var list = await _dTImgIBLL.GetList();
            List<string> res = new List<string>();
            foreach (var item in list)
            {
                res.Add(item.F_Id);
            }
            return Success(res);
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Img(string id)
        {
            var stampEntity = await _dTImgIBLL.GetEntity(id);
            if (stampEntity != null && !string.IsNullOrEmpty(stampEntity.F_FileName))
            {
                ImgEntity imgEntity = await _imgIBLL.GetEntity(stampEntity.F_FileName);
                if (imgEntity != null && !string.IsNullOrEmpty(imgEntity.F_Content))
                {
                    string imgContent = imgEntity.F_Content.Replace("data:image/" + imgEntity.F_ExName.Replace(".", "") + ";base64,", "");
                    byte[] arr = Convert.FromBase64String(imgContent);
                    return File(arr, "application/octet-stream");
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}