/*
 * 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架(http://www.aycsoft.cn)
 * Copyright (c) 2021-present 广州轻创软件信息科技有限公司
 * 创建人：young
 * 日 期：2018.05.10
 * 描 述：客户端语言包加载（菜单，tab条）
 */
(function ($, aycsoft) {
    "use strict";

    var type = 'no';
    var lgTypeList = {};
    var mainType = null;
    var isRead = {};

    var lgData = {};

    aycsoft.language = {
        getMainCode: function () {
            return mainType;
        },
        get: function (text, callback) {
            // 判断当前客户端的语言版本
            if (mainType != null && type.toLowerCase() != mainType.toLowerCase()) {
                // 判断当前语言包是否加载完成
                if (isRead[type.toLowerCase()] && isRead[mainType.toLowerCase()]) {
                    var mdata = lgData[mainType];
                    var cdata = lgData[type];
                    callback(cdata[mdata[text]] || text);
                }
                else {
                    setTimeout(function () {
                        aycsoft.language.get(text, callback);
                    }, 200);
                }
            }
            else {
                callback(text);
            }
        },
        getSyn: function (text) {
            // 判断当前客户端的语言版本
            if (type != mainType) {
                var mdata = storage.get(mainType);
                var cdata = storage.get(type);
                return cdata.data[mdata.data[text]] || text;
            }
            else {
                return text;
            }
        }
    };
    $(function () {
        type = top.$.cookie('lr_adms_core_lang') || 'no';
        var $setting = $('#lr_lg_setting');
        if (type == 'no') {
            $setting.find('span').text('简体中文');
        }
        $setting.on('click', 'li>a', function () {
            var code = $(this).attr('data-value');
            top.$.cookie('lr_adms_core_lang', code, { path: "/" });
            location.reload();
        });
        // 获取当前语言类型
        aycsoft.httpAsyncGet(top.$.rootUrl + '/LR_LGManager/LGType/GetList', function (res) {
            if (res.code == 200) {
                var $ul = $setting.find('ul');

                $.each(res.data, function (_index, _item) {
                    lgTypeList[_item.F_Code] = _item.F_Name;
                    if (_item.F_IsMain == 1) {
                        mainType = _item.F_Code;
                        if (type == 'no') {
                            type = mainType;
                            top.$.cookie('lr_adms_core_lang', type, { path: "/" });
                        }
                    }
                    isRead[_item.F_Code] = false;

                    var html = '<li><a href="javascript:void(0);" data-value="' + _item.F_Code + '" >' + _item.F_Name + '</a></li>';
                    $ul.append(html);
                });
                $setting.find('span').text(lgTypeList[type]);

                // 开始加载语言包,如果当前设置的语言不是主语言的话
                if (type.toLowerCase() != mainType.toLowerCase()) {
                    aycsoft.httpAsyncGet(top.$.rootUrl + '/LR_LGManager/LGMap/GetLanguageByCode?typeCode=' + mainType + '&isMain=true', function (res) {
                        if (res.code == 200) {
                            lgData[mainType] = res.data;
                        }
                        isRead[mainType.toLowerCase()] = true;
                    });

                    aycsoft.httpAsyncGet(top.$.rootUrl + '/LR_LGManager/LGMap/GetLanguageByCode?typeCode=' + type + '&isMain=false', function (res) {
                        if (res.code == 200) {
                            lgData[type] = res.data;
                        }
                        isRead[type.toLowerCase()] = true;
                    });

                }
            }
            else {
                $setting.hide();
            }
        });
    });
})(window.jQuery, top.aycsoft);