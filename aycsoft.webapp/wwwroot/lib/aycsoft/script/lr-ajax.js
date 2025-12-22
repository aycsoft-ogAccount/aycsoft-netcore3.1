/*
 * 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架(http://www.aycsoft.cn)
 * Copyright (c) 2021-present 广州轻创软件信息科技有限公司
 * 创建人：young
 * 日 期：2017.03.16
 * 描 述：ajax操作方法
 */
(function ($, aycsoft) {
    "use strict";
    var httpCode = {
        success: 200,
        fail: 400,
        exception: 500,
        nologin: 410 // 没有登录者信息

    };
    var exres = { code: httpCode.exception, info: '通信异常，请联系管理员！' };

    function isLogin(res) {
        if (res.code == aycsoft.httpCode.nologin) {
            var _topUrl = top.$.rootUrl + '/Login/Index';
            switch (res.info) {
                case 'nologin':
                    break;
                case 'noip':
                    _topUrl += '?error=ip';
                    break;
                case 'notime':
                    _topUrl += '?error=time';
                    break;
                case 'other':
                    _topUrl += '?error=other';
                    break;
            }
            top.window.location.href = _topUrl;
            return false;
        }
        return true;
    }
    function httpHeaders() {
        var headers = {
            token: $.lcoreUser.token
        }
        return headers; 
    }

    $.extend(aycsoft, {
        // http 通信异常的时候调用此方法
        httpErrorLog: function (msg) {
            aycsoft.log(msg);
        },
        // http请求返回数据码
        httpCode: httpCode,
        // get请求方法（异步）:url地址,callback回调函数
        httpAsyncGet: function (url, callback) {
            $.ajax({
                url: url,
                headers: httpHeaders(),
                type: "GET",
                dataType: "json",
                async: true,
                cache: false,
                success: function (res) {
                    isLogin(res);
                    if (res.code == aycsoft.httpCode.exception) {
                        aycsoft.httpErrorLog(res.info);
                    }
                    callback(res);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    aycsoft.httpErrorLog(textStatus);
                    callback(exres);
                },
                beforeSend: function () {
                },
                complete: function () {
                }
            });
        },
        // get请求方法（同步）:url地址,param参数
        httpGet: function (url, param) {
            var _res = {};
            $.ajax({
                url: url,
                headers: httpHeaders(),
                data: param,
                type: "GET",
                dataType: "json",
                async: false,
                cache: false,
                success: function (res) {
                    isLogin(res);
                    if (res.code == aycsoft.httpCode.exception) {
                        aycsoft.httpErrorLog(res.info);
                    }
                    _res = res;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    aycsoft.httpErrorLog(textStatus);
                },
                beforeSend: function () {
                },
                complete: function () {
                }
            });
            return _res;
        },
        // post请求方法（异步）:url地址,param参数,callback回调函数
        httpAsyncPost: function (url, param, callback) {
            $.ajax({
                url: url,
                headers: httpHeaders(),
                data: param,
                type: "POST",
                dataType: "json",
                async: true,
                cache: false,
                success: function (res) {
                    isLogin(res);
                    if (res.code == aycsoft.httpCode.exception) {
                        aycsoft.httpErrorLog(res.info);
                    }
                    callback && callback(res);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    aycsoft.httpErrorLog(textStatus);
                    callback && callback(exres);
                },
                beforeSend: function () {
                },
                complete: function () {
                }
            });
        },
        // post请求方法（同步步）:url地址,param参数,callback回调函数
        httpPost: function (url, param, callback) {
            $.ajax({
                url: url,
                headers: httpHeaders(),
                data: param,
                type: "POST",
                dataType: "json",
                async: false,
                cache: false,
                success: function (res) {
                    isLogin(res);
                    if (res.code == aycsoft.httpCode.exception) {
                        aycsoft.httpErrorLog(res.info);
                    }
                    callback(res);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    aycsoft.httpErrorLog(textStatus);
                    callback(exres);
                },
                beforeSend: function () {
                },
                complete: function () {
                }
            });
        },
        // ajax 异步封装
        httpAsync: function (type, url, param, callback) {
            $.ajax({
                url: url,
                headers: httpHeaders(),
                data: param,
                type: type,
                dataType: "json",
                async: true,
                cache: false,
                success: function (res) {
                    
                    if (!isLogin(res)) {
                        callback(null);
                    }
                    else if (res.code == aycsoft.httpCode.success) {
                        callback(res.data);
                    }
                    else {
                        aycsoft.httpErrorLog(res.info);
                        callback(null);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    aycsoft.httpErrorLog(textStatus);
                    callback(null);
                },
                beforeSend: function () {
                },
                complete: function () {
                }
            });
        },
        // ajax 同步封装
        httpSync: function (type, url, param, callback) {
            $.ajax({
                url: url,
                headers: httpHeaders(),
                data: param,
                type: type,
                dataType: "json",
                async: false,
                cache: false,
                success: function (res) {
                    if (!isLogin(res)) {
                        callback(null);
                    }
                    else if (res.code == aycsoft.httpCode.success) {
                        callback(res.data);
                    }
                    else {
                        aycsoft.httpErrorLog(res.info);
                        callback(null);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    aycsoft.httpErrorLog(textStatus);
                    callback(null);
                },
                beforeSend: function () {
                },
                complete: function () {
                }
            });
        },

        deleteForm: function (url, param, callback) {
            aycsoft.loading(true, '正在删除数据');
            aycsoft.httpAsyncPost(url, param, function (res) {
                aycsoft.loading(false);
                if (res.code == aycsoft.httpCode.success) {
                    if (!!callback) {
                        callback(res);
                    }
                    aycsoft.alert.success(res.info);
                }
                else {
                    aycsoft.alert.error(res.info);
                    aycsoft.httpErrorLog(res.info);
                }
                layer.close(layer.index);
            });
        },
        postForm: function (url, param, callback,msg) {
            aycsoft.loading(true, msg || '正在提交数据');
            aycsoft.httpAsyncPost(url, param, function (res) {
                aycsoft.loading(false);
                if (res.code == aycsoft.httpCode.success) {
                    if (!!callback) {
                        callback(res);
                    }
                    aycsoft.alert.success(res.info);
                    layer.close(layer.index);
                }
                else {
                    aycsoft.alert.error(res.info);
                    aycsoft.httpErrorLog(res.info);
                }
                
            });
        }
    });

})(window.jQuery, top.aycsoft);