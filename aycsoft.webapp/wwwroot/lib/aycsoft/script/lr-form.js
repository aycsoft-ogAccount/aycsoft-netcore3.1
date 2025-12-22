/*
 * 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架(http://www.aycsoft.cn)
 * Copyright (c) 2021-present 广州轻创软件信息科技有限公司
 * 创建人：young
 * 日 期：2017.03.16
 * 描 述：表单处理方法
 */
(function ($, aycsoft) {
    "use strict";

    /*获取和设置表单数据*/
    $.fn.lrGetFormData = function (keyValue) {// 获取表单数据
        var resdata = {};
        $(this).find('input,select,textarea,.lr-select,.lr-formselect,.lrUploader-wrap,.lr-radio,.lr-checkbox,.edui-default,.lr-search-date,.lrUserInfo').each(function (r) {
            var $this = $(this);
            var id = $this.attr('id');

            if (id) {
                var type = $this.attr('type');
                switch (type) {
                    case "radio":
                        if ($this.is(":checked")) {
                            var _name = $this.attr('name');
                            resdata[_name] = $("#" + id).val();
                        }
                        break;
                    case "checkbox":
                        if ($this.is(":checked")) {
                            resdata[id] = 1;
                        } else {
                            resdata[id] = 0;
                        }
                        break;
                    case "lrselect":
                        resdata[id] = $this.lrselectGet();
                        break;
                    case "formselect":
                        resdata[id] = $this.lrformselectGet();
                        break;
                    case "lrGirdSelect":
                        resdata[id] = $this.lrGirdSelectGet();
                        break;
                    case "lr-Uploader":
                        resdata[id] = $this.lrUploaderGet();
                        break;
                    case "lr-radio":
                        resdata[id] = $this.find('input:checked').val();
                        break;
                    case "lr-checkbox":
                        var _idlist = [];
                        $this.find('input:checked').each(function () {
                            _idlist.push($(this).val());
                        });
                        resdata[id] = String(_idlist);
                        break;
                    case "lr-search-date-type":// 表单
                        resdata[id] = ''; 
                        resdata[id + '_start'] = $this[0]._lrdate.dfop._begindate;
                        resdata[id + '_end'] = $this[0]._lrdate.dfop._enddate;
                        break;
                    case 'lr-userInfo':
                        resdata[id] = $this.lrUserInfoGet()
                        break;
                    default:
                        if ($this.hasClass('currentInfo')) {
                            var value = $this[0].lrvalue;
                            if (value == undefined) {
                                value = $this.val();
                            }
                            resdata[id] = $.trim(value);
                        }
                        else if ($this.hasClass('edui-default')) {
                            if ($this[0].ue) {
                                resdata[id] = $this[0].ue.getContent(null, null, true).replace(/[<>&"]/g, function (c) { return { '<': '&lt;', '>': '&gt;', '&': '&amp;', '"': '&quot;' }[c]; });
                            }
                        }
                        else {
                            var value = $this.val();
                            if (value != undefined && value != 'undefined') {
                                resdata[id] = $.trim(value);
                            }
                        }

                        if ($this.hasClass('lr-input-wdatepicker') && resdata[id] == '') {
                            resdata[id] = '1000-01-01 00:00:00'
                        }

                        break;
                }
                if (resdata[id] != undefined) {
                    resdata[id] += '';
                    if (resdata[id] == '') {
                        resdata[id] = '&nbsp;';
                    }
                    if (resdata[id] == '&nbsp;' && !keyValue) {
                        resdata[id] = '';
                    }
                }

            }
        });
        return resdata;
    };
    $.fn.lrSetFormData = function (data, isDfvalue) {// 设置表单数据 isDfvalue 是否设置默认值
        var $this = $(this);
        for (var id in data) {
            var value = data[id];
            var $obj = $this.find('#' + id);
            if ($obj.length == 0 && value != null) {
                $obj = $this.find('[name="' + id + '"][value="' + value + '"]');
                if ($obj.length > 0) {
                    if (!$obj.is(":checked")) {
                        $obj.trigger('click');
                    }
                }
            }
            else {
                var type = $obj.attr('type');
                if ($obj.hasClass("lr-input-wdatepicker")) {
                    type = "datepicker";
                }
                switch (type) {
                    case "checkbox":
                        var isck = 0;
                        if ($obj.is(":checked")) {
                            isck = 1;
                        } else {
                            isck = 0;
                        }
                        if (isck != parseInt(value)) {
                            $obj.trigger('click');
                        }
                        break;
                    case "lrselect":
                        $obj.lrselectSet(value);
                        break;
                    case "formselect":
                        $obj.lrformselectSet(value);
                        break;
                    case "lrGirdSelect":
                        $obj.lrGirdSelectSet(value);
                        break;
                    case "datepicker":
                        var _dateFmt = $obj.attr('data-dateFmt') || 'yyyy-MM-dd hh:mm';
                        $obj.val(aycsoft.formatDate(value, _dateFmt));
                        break;
                    case "lr-Uploader":
                        $obj.lrUploaderSet(value);
                        break;
                    case "lr-radio":
                        if (isDfvalue) {
                            if ($obj[0].dfvalue == 'lr1' || $obj[0].dfvalue == 'lr0') {
                                $obj.find('input').eq(0).trigger('click');
                            }
                            else {
                                if (!$obj.find('input[value="' + $obj[0].dfvalue + '"]').is(":checked")) {
                                    $obj.find('input[value="' + value + '"]').trigger('click');
                                }
                            }
                        }
                        else {
                            if (!$obj.find('input[value="' + value + '"]').is(":checked")) {
                                $obj.find('input[value="' + value + '"]').trigger('click');
                            }
                        }
                        break;
                    case "lr-checkbox":
                        $obj.find('input').each(function () {
                            if ($(this).is(":checked")) {
                                $(this).trigger('click');
                            }
                        })
                        if (isDfvalue) {
                            if ($obj[0].dfvalue == 'lr1' || $obj[0].dfvalue == 'lr0') {
                                if ($obj[0].dfvalue == 'lr1') {
                                    $obj.find('input').eq(0).trigger('click');
                                }
                                
                            }
                            else {
                                var values = $obj[0].dfvalue.split(",");
                                $.each(values, function (index, val) {
                                    $obj.find('input[value="' + val + '"]').trigger('click');
                                });
                            }
                        }
                        else {
                            value = value + ''
                            var values = value.split(",");
                            $.each(values, function (index, val) {
                                $obj.find('input[value="' + val + '"]').trigger('click');
                            });
                        }
                        break;
                    case "lr-search-date-type":
                        if (isDfvalue) {
                            var dateOp = $obj[0]._lrdate.dfop;
                            if (dateOp.dfvalue == -1) {
                                $obj.find('[data-value="clearDate"]').trigger('click');
                            }
                            else {
                                $.lrdate.select($obj, dateOp.dfvalue);
                            }
                        }
                        break;
                    case 'lr-userInfo':
                        $obj.lrUserInfoSet(value)
                        break;
                    default:
                        if ($obj.hasClass('currentInfo')) {
                            $obj[0].lrvalue = value;
                            if ($obj.hasClass('lr-currentInfo-user')) {
                                $obj.val('');
                                aycsoft.clientdata.getAsync('user', {
                                    key: value,
                                    callback: function (item, op) {
                                        op.obj.val(item.F_RealName);
                                    },
                                    obj: $obj
                                });
                            }
                            else if ($obj.hasClass('lr-currentInfo-company')) {
                                $obj.val('');
                                aycsoft.clientdata.getAsync('company', {
                                    key: value,
                                    callback: function (_data, op) {
                                        op.obj.val(_data.F_FullName);
                                    },
                                    obj: $obj
                                });
                            }
                            else if ($obj.hasClass('lr-currentInfo-department')) {
                                $obj.val('');
                                aycsoft.clientdata.getAsync('department', {
                                    key: value,
                                    callback: function (_data, op) {
                                        op.obj.val(_data.F_FullName);
                                    },
                                    obj: $obj
                                });
                            }
                            else {
                                $obj.val(value);
                            }

                        }
                        else if ($obj[0] && $obj[0].ue) {
                            if (!!value) {
                                var ue = $obj[0].ue;
                                setUe(ue, value);
                            }
                        }
                        else {
                            $obj.val(value);
                        }


                        break;
                }
            }
        }
    };


    function setUe(ue, value) {
        ue.ready(function () {
            var arrEntities = { 'lt': '<', 'gt': '>', 'nbsp': ' ', 'amp': '&', 'quot': '"' };
            var str = value.replace(/&(lt|gt|nbsp|amp|quot);/ig, function (all, t) { return arrEntities[t]; });
            str = str.replace(/&(lt|gt|nbsp|amp|quot);/ig, function (all, t) { return arrEntities[t]; });
            ue.setContent(str);
        });
    }

    $.fn.showEditer = function (content) {
        var arrEntities = { 'lt': '<', 'gt': '>', 'nbsp': ' ', 'amp': '&', 'quot': '"' };
        var str = content.replace(/&(lt|gt|nbsp|amp|quot);/ig, function (all, t) { return arrEntities[t]; });
        str = str.replace(/&(lt|gt|nbsp|amp|quot);/ig, function (all, t) { return arrEntities[t]; });
        $(this).html(str);
    }

    /*表单数据操作*/
    $.lrSetForm = function (url, callback) {
        aycsoft.loading(true, '正在获取数据');
        aycsoft.httpAsyncGet(url, function (res) {
            aycsoft.loading(false);
            if (res.code == aycsoft.httpCode.success) {
                callback(res.data);
            }
            else {
                aycsoft.layerClose(window.name);
                aycsoft.alert.error('表单数据获取失败！');
            }
        });
    };
    $.lrSaveForm = function (url, param, callback, isNotClosed) {
        aycsoft.loading(true, '正在保存数据');
        aycsoft.httpAsyncPost(url, param, function (res) {
            aycsoft.loading(false);
            if (res.code == aycsoft.httpCode.success) {
                if (!!callback) {
                    callback(res);
                }
                aycsoft.alert.success(res.info);
                if (!isNotClosed) {
                    aycsoft.layerClose(window.name);
                }
            }
            else {
                aycsoft.alert.error(res.info);
                aycsoft.httpErrorLog(res.info);
            }
        });
    };
    $.lrPostForm = function (url, param) {
        aycsoft.loading(true, '正在提交数据');
        aycsoft.httpAsyncPost(url, param, function (res) {
            aycsoft.loading(false);
            if (res.code == aycsoft.httpCode.success) {
                aycsoft.alert.success(res.info);
            }
            else {
                aycsoft.alert.error(res.info);
                aycsoft.httpErrorLog(res.info);
            }
        });
    };

    /*tab页切换*/
    $.fn.lrFormTab = function () {
        var $this = $(this);
        $this.parent().css({ 'padding-top': '44px' });
        $this.lrscroll();

        $this.on('DOMNodeInserted', function (e) {
            var $this = $(this);
            var w = 0;
            $this.find('li').each(function () {
                w += $(this).outerWidth();
            });
            $this.find('.lr-scroll-box').css({ 'width': w });
        });

        var $this = $(this);
        var w = 0;
        $this.find('li').each(function () {
            w += $(this).outerWidth();
        });
        $this.find('.lr-scroll-box').css({ 'width': w });

        $this.delegate('li', 'click', { $ul: $this }, function (e) {
            var $li = $(this);
            if (!$li.hasClass('active')) {
                var $parent = $li.parent();
                var $content = e.data.$ul.next();

                var id = $li.find('a').attr('data-value');
                $parent.find('li.active').removeClass('active');
                $li.addClass('active');
                $content.children('.tab-pane.active').removeClass('active');
                $content.children('#' + id).addClass('active');
            }
        });
    }
    $.fn.lrFormTabEx = function (callback) {
        var $this = $(this);
        $this.delegate('li', 'click', { $ul: $this }, function (e) {
            var $li = $(this);
            if (!$li.hasClass('active')) {
                var $parent = $li.parent();
                var $content = e.data.$ul.next();

                var id = $li.find('a').attr('data-value');
                $parent.find('li.active').removeClass('active');
                $li.addClass('active');
                $content.find('.tab-pane.active').removeClass('active');
                $content.find('#' + id).addClass('active');

                if (!!callback) {
                    callback(id);
                }
            }
        });
    }

    /*检测字段是否重复*/
    $.lrExistField = function (keyValue, controlId, tableName, tableKey, param) {
        var $control = $("#" + controlId);
        if (!$control.val()) {
            return false;
        }
        var data = {};
        data[controlId] = $control.val();
        $.extend(data, param || {});

        var postData = {
            keyValue: keyValue,
            tableName: tableName,
            keyName: tableKey,
            filedsJson: JSON.stringify(data)
        };

        aycsoft.httpAsync('GET', top.$.rootUrl + "/LR_SystemModule/DatabaseTable/ExistFiled", postData, function (data) {
            if (data == false) {
                $.lrValidformMessage($control, '已存在,请重新输入');
            }
        });
    };

    /*固定下拉框的一些封装：数据字典，组织机构，省市区级联*/
    // 数据字典下拉框
    $.fn.lrDataItemSelect = function (op) {
        var dfop = {
            allowSearch: true,// 是否允许搜索
            parentId:'0'
        }
        $.extend(dfop, op || {});
        if (!dfop.code) {
            return $(this);
        }
        var list = [];
        var $select = $(this).lrselect(dfop);
        aycsoft.clientdata.getAllAsync('dataItem', {
            code: dfop.code,
            callback: function (dataes) {
                $.each(dataes, function (_index, _item) {
                    if (_item.F_ParentId == dfop.parentId) {
                        list.push({ id: _item.F_ItemValue, text: _item.F_ItemName, title: _item.F_ItemName, k: _item.F_ItemDetailId });
                    }
                });
                $select.lrselectRefresh({
                    data: list
                });
            }
        });
        return $select;
    };

    // 数据源下拉框
    $.fn.lrDataSourceSelect = function (op) {
        op = op || {};
        var dfop = {
            // 是否允许搜索
            allowSearch: true,
            height:200
        }
        $.extend(dfop, op || {});
        if (!dfop.code) {
            return $(this);
        }
        var $select = $(this).lrselect(dfop);
        aycsoft.clientdata.getAllAsync('sourceData', {
            code: dfop.code,
            callback: function (dataes) {
                $select.lrselectRefresh({
                    data: dataes
                });
            }
        })
        return $select;
    }

    // 公司信息下拉框
    $.fn.lrCompanySelect = function (op) {
        var dfop = {
            type: 'tree',
            // 是否允许搜索
            allowSearch: true,
            // 访问数据接口地址
            url: top.$.rootUrl + '/LR_OrganizationModule/Company/GetTree',
            // 访问数据接口参数
            param: {},
            parentId:'0'
        };
        $.extend(dfop, op || {});
        dfop.param.parentId = dfop.parentId;
        var $select = $(this).lrselect(dfop);
        return $select;
    };

    // 部门信息下拉框
    $.fn.lrDepartmentSelect = function (op) {
        var dfop = {
            type: 'tree',
            // 是否允许搜索
            allowSearch: true,
            // 访问数据接口地址
            url: top.$.rootUrl + '/LR_OrganizationModule/Department/GetTree',
            // 访问数据接口参数
            param: {},
            parentId:'0'
        }
        $.extend(dfop, op || {});
        dfop.param.companyId = dfop.companyId;
        dfop.param.parentId = dfop.parentId;
        return $(this).lrselect(dfop);;
    };
    // 人员下拉框
    $.fn.lrUserSelect = function (type, select) {//0单选1多选
        if (type == 0) {
            $(this).lrformselect({
                layerUrl: top.$.rootUrl + '/LR_OrganizationModule/User/SelectOnlyForm',
                layerUrlW: 400,
                layerUrlH: 300,
                dataUrl: top.$.rootUrl + '/LR_OrganizationModule/User/GetListByUserIds',
                select: select
            });
        }
        else {
            $(this).lrformselect({
                layerUrl: top.$.rootUrl + '/LR_OrganizationModule/User/SelectForm',
                layerUrlW: 800,
                layerUrlH: 520,
                dataUrl: top.$.rootUrl + '/LR_OrganizationModule/User/GetListByUserIds',
                select: select
            });
        }
    }

    // 省市区级联
    $.fn.lrAreaSelect = function (op) {
        // op:parentId 父级id,maxHeight 200,
        var dfop = {
            // 字段
            value: "F_AreaCode",
            text: "F_AreaName",
            title: "F_AreaName",
            // 是否允许搜索
            allowSearch: true,
            // 访问数据接口地址
            url: top.$.rootUrl + '/LR_SystemModule/Area/Getlist',
            // 访问数据接口参数
            param: { parentId: '' },
        }
        op = op || {};
        if (!!op.parentId) {
            dfop.param.parentId = op.parentId;
        }
        var _obj = [], i = 0;
        var $this = $(this);
        $(this).find('div').each(function () {
            var $div = $('<div></div>');
            var $obj = $(this);
            dfop.placeholder = $obj.attr('placeholder');
            $div.addClass($obj.attr('class'));
            $obj.removeAttr('class');
            $obj.removeAttr('placeholder');
            $div.append($obj);
            $this.append($div);
            if (i == 0) {
                $obj.lrselect(dfop);
            }
            else {
                dfop.url = "";
                dfop.parentId = "";
                $obj.lrselect(dfop);
                _obj[i - 1].on('change', function () {
                    var _value = $(this).lrselectGet();
                    if (_value == "") {
                        $obj.lrselectRefresh({
                            url: '',
                            param: { parentId: _value },
                            data: []
                        });
                    }
                    else {
                        $obj.lrselectRefresh({
                            url: top.$.rootUrl + '/LR_SystemModule/Area/Getlist',
                            param: { parentId: _value },
                        });
                    }

                });
            }
            i++;
            _obj.push($obj);
        });
    };
    // 数据库选择
    $.fn.lrDbSelect = function (op) {
        // op:maxHeight 200,
        var dfop = {
            type: 'tree',
            // 是否允许搜索
            allowSearch: true,
            value: 'value',
            // 访问数据接口地址
            url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList'
        }
        op = op || {};

        return $(this).lrselect(dfop);
    };

    // 动态获取和设置radio，checkbox
    $.fn.lrRadioCheckbox = function (op) {
        var dfop = {
            type: 'radio',        // checkbox
            dataType: 'dataItem', // 默认是数据字典 dataSource（数据源）
            code: '',
            text: 'F_ItemName',
            value: 'F_ItemValue',
            dfvalue:'lr1' // lr1 选中第一个 lr0 一个都不选中默认
        };
        $.extend(dfop, op || {});
        var $this = $(this);
        $this.addClass(dfop.type);
        $this.addClass('lr-' + dfop.type);
        $this.attr('type', 'lr-' + dfop.type);
        var thisId = $this.attr('id');
        $this[0].dfvalue = dfop.dfvalue
        if (dfop.dfvalue != 'lr1' && dfop.dfvalue != 'lr0') {
            dfop.dfvalue = ',' + dfop.dfvalue + ','
        }

        if (dfop.dataType == 'dataItem') {
            aycsoft.clientdata.getAllAsync('dataItem', {
                code: dfop.code,
                callback: function (dataes) {
                    $.each(dataes, function (id, item) {
                        var $point = $('<label><input name="' + thisId + '" value="' + item.F_ItemValue + '" ' + (dfop.dfvalue.indexOf(',' + item.F_ItemValue +',') != -1 ? "checked" : "") + ' type="' + dfop.type + '">' + item.F_ItemName + '</label>');
                        $this.append($point);
                    });
                    if (dfop.dfvalue == 'lr1') {
                        $this.find('input').eq(0).trigger('click');
                    }
                }
            });
        }
        else {
            aycsoft.clientdata.getAllAsync('sourceData', {
                code: dfop.code,
                callback: function (dataes) {
                    $.each(dataes, function (id, item) {
                        var $point = $('<label><input name="' + thisId + '" value="' + item[dfop.value] + '" ' + (dfop.dfvalue.indexOf(',' + item[dfop.value] + ',') != -1 ? 'checked' : '') + ' type="' + dfop.type + '">' + item[dfop.text] + '</label>');
                        $this.append($point);
                    });
                    if (dfop.dfvalue == 'lr1') {
                        $this.find('input').eq(0).trigger('click');
                    }
                }
            });
        }
    };
    // 多条件查询框
    $.fn.lrMultipleQuery = function (search, height, width) {
        var $this = $(this);
        var contentHtml = $this.html();
        $this.addClass('lr-query-wrap');


        var _html = '';
        _html += '<div class="lr-query-btn"><i class="fa fa-search"></i>&nbsp;多条件查询</div>';
        _html += '<div class="lr-query-content">';
        //_html += '<div class="lr-query-formcontent">';
        _html += contentHtml;
        //_html += '</div>';
        _html += '<div class="lr-query-arrow"><div class="lr-query-inside"></div></div>';
        _html += '<div class="lr-query-content-bottom">';
        _html += '<a id="lr_btn_queryReset" class="btn btn-default">&nbsp;重&nbsp;&nbsp;置</a>';
        _html += '<a id="lr_btn_querySearch" class="btn btn-primary">&nbsp;查&nbsp;&nbsp;询</a>';
        _html += '</div>';
        _html += '</div>';
        $this.html(_html);
        $this.find('.lr-query-formcontent').show();

        $this.find('.lr-query-content').css({ 'width': width || 400, 'height': height || 300 });

        $this.find('.lr-query-btn').on('click', function () {
            var $content = $this.find('.lr-query-content');
            if ($content.hasClass('active')) {
                $content.removeClass('active');
            }
            else {
                $content.addClass('active');
            }
        });

        $this.find('#lr_btn_querySearch').on('click', function () {
            var $content = $this.find('.lr-query-content');
            var query = $content.lrGetFormData();
            $content.removeClass('active');
            search(query);
        });

        $this.find('#lr_btn_queryReset').on('click', function () {
            var $content = $this.find('.lr-query-content');
            var query = $content.lrGetFormData();
            for (var id in query) {
                query[id] = "";
            }
            $content.lrSetFormData(query);
        });

        $(document).click(function (e) {
            var et = e.target || e.srcElement;
            var $et = $(et);
            if (!$et.hasClass('lr-query-wrap') && $et.parents('.lr-query-wrap').length <= 0) {

                $('.lr-query-content').removeClass('active');
            }
        });
    };

    // 获取表单显示数据
    $.fn.lrGetFormShow = function () {
        var resdata = [];
        $(this).find('.lr-form-item').each(function () {
            var $this = $(this);
            if ($this.is(':hidden')) {
                return;
            }

            var point = {};
            point.name = ($this.find('.lr-form-item-title').text() || '').replace('*', '');
            for (var i = 1; i < 13; i++) {
                if ($this.hasClass('col-xs-' + i)) {
                    point.col = i;
                }
            }

            if ($this.find('.lr-form-item-title').length == 0) {
                if ($this.find('.jfgrid-layout').length == 0) {
                    point.text = $this.html();
                    point.type = 'title';
                    resdata.push(point);
                }
                else {
                    point.type = 'gird';
                    point.gridHead = $this.find('.jfgrid-layout').jfGridGet('settingInfo').headData;
                    point.data = $this.find('.jfgrid-layout').jfGridGet('showData');
                    resdata.push(point);
                }
            }
            else {
                point.type = 'input';
                var list = $this.find('input,textarea,.lr-select,.edui-default');
                if (list.length > 0) {
                    resdata.push(point);
                    list.each(function () {
                        var type = $(this).attr('type');
                        switch (type) {
                            case "radio":
                                if ($(this).is(":checked")) {
                                    point.text = $(this).parent().text();
                                }
                                break;
                            case "checkbox":
                                if ($(this).is(":checked")) {
                                    point.textList = point.textList || [];
                                    point.textList.push($(this).parent().text());
                                }
                                break;
                            case "lrselect":
                                point.text = $(this).find('.lr-select-placeholder').text();
                                break;
                            default:
                                if ($(this).hasClass('edui-default')) {
                                    if ($(this)[0].ue) {
                                        point.text = $(this)[0].ue.getContent(null, null, true);
                                    }
                                }
                                else {
                                    point.text = $(this).val();
                                }
                                break;
                        }
                    });
                }
            }
        });
        return resdata;
    }

    // 编辑器初始化
    $.fn.lrUe = function (dfvalue) {
        var $this = $(this);
        var id = $this.attr('id')
        if (!id) {
            console.error('富文本编辑器初始化DIV缺少Id！')
            return $this
        }
        $this[0].ue = UE.getEditor(id);
        if (dfvalue) {
            $this[0].ue.ready(function () {
                $this[0].ue.setContent(dfvalue);
            });
        }
        return $this;
    }

    // 日期计算组件
    $.fn.lrDatetimeRange = function (beginId, endId) {
        var $this = $(this)
        $('#' + beginId).on('change', function () {
            var st = $(this).val();
            var et = $('#' + endId).val();
            if (st && et) {
                var diff = aycsoft.parseDate(st).DateDiff('d', et) + 1;
                $this.val(diff);
            }
        });
        $('#' + endId).on('change', function () {
            var st = $('#' + beginId).val();
            var et = $(this).val();
            if (!!st && !!et) {
                var diff = aycsoft.parseDate(st).DateDiff('d', et) + 1;
                $this.val(diff);
            }
        });
    }

    // 当前时间
    $.fn.lrCurrentDate = function (dateformat) {
        var $this = $(this)
        $this[0].lrvalue = aycsoft.formatDate(new Date(), dateformat || 'yyyy-MM-dd hh:mm:ss');
        $this.val($this[0].lrvalue);
        return $this
    }

    // 单据编码
    $.fn.lrEncode = function (rulecode) {
        var $this = $(this)
        aycsoft.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/CodeRule/GetEnCode', { code: rulecode }, function (data) {
            if (!$this.val()) {
                $this.val(data);
            }
        })
    }

    // 当前登录者信息
    $.fn.lrUserInfo = function (dataType) {

        var $this = $(this)
        $this[0].dataType = dataType

        $this.addClass('lrUserInfo')
        $this.attr('type','lr-userInfo')

        var loginInfo = aycsoft.clientdata.get(['userinfo'])
        switch (dataType) {
            case 'name':
                $this[0].lrvalue = loginInfo.F_RealName
                $this.val(loginInfo.F_RealName)
                break;
            case 'account':
                $this[0].lrvalue = loginInfo.F_Account
                $this.val(loginInfo.F_Account)
                break;
            case 'id':
                $this[0].lrvalue = loginInfo.F_UserId
                aycsoft.clientdata.getAsync('user', {
                    key: loginInfo.F_UserId,
                    callback: function (item) {
                        if (!$this.val()) {
                            $this.val(item.F_RealName)
                        }
                    }
                });
                break;
            case 'companyId':
                $this[0].lrvalue = loginInfo.F_CompanyId
                aycsoft.clientdata.getAsync('company', {
                    key: loginInfo.F_CompanyId,
                    callback: function (item) {
                        if (!$this.val()) {
                            $this.val(item.F_FullName)
                        }
                    }
                });
                break;
            case 'departmentId':
                $this[0].lrvalue = loginInfo.F_DepartmentId
                aycsoft.clientdata.getAsync('department', {
                    key: loginInfo.F_DepartmentId,
                    callback: function (item) {
                        if (!$this.val()) {
                            $this.val(item.F_FullName)
                        }
                    }
                });
                break;
            case 'companyName':
                aycsoft.clientdata.getAsync('company', {
                    key: loginInfo.F_CompanyId,
                    callback: function (item) {
                        if (!$this[0].lrvalue) {
                            $this[0].lrvalue = item.F_FullName
                        }
                        if (!$this.val()) {
                            $this.val(item.F_FullName)
                        }
                    }
                });
                break;
            case 'departmentName':
                aycsoft.clientdata.getAsync('department', {
                    key: loginInfo.F_DepartmentId,
                    callback: function (item) {
                        if (!$this[0].lrvalue) {
                            $this[0].lrvalue = item.F_FullName
                        }
                        if (!$this.val()) {
                            $this.val(item.F_FullName)
                        }
                    }
                });
                
                break;
        }
    }

    $.fn.lrUserInfoSet = function (value) {
        var $this = $(this)
        var dataType = $this[0].dataType
        $this[0].lrvalue = value
        switch (dataType) {
            case 'name':
            case 'account':
            case 'companyName':
            case 'departmentName':
                $this.val(value)
                $this.val(value)
            case 'id':
                aycsoft.clientdata.getAsync('user', {
                    key: value,
                    callback: function (item) {
                        $this.val(item.F_RealName)
                    }
                });
                break;
            case 'companyId':
                aycsoft.clientdata.getAsync('company', {
                    key: value,
                    callback: function (item) {
                        $this.val(item.F_FullName)
                    }
                });
                break;
            case 'departmentId':
                aycsoft.clientdata.getAsync('department', {
                    key: value,
                    callback: function (item) {
                        $this.val(item.F_FullName)
                    }
                });
                break;
        }
    }

    $.fn.lrUserInfoGet = function () {
        return $(this)[0].lrvalue
    }

})(jQuery, top.aycsoft);