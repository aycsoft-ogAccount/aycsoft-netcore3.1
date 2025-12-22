/*
 * 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架(http://www.aycsoft.cn)
 * Copyright (c) 2021-present 广州轻创软件信息科技有限公司
 * 创建人：young
 * 日 期：2022.11.08
 * 描 述：获取客户端数据
 */
/*
`*******************登录后数据***********************
 *userinfo----------------------用户登录信息

 *modules-----------------------功能模块
 *modulesTree-------------------按照父节点的功能模块
 *modulesMap--------------------主键对应实例数据

 *******************使用时异步获取*******************
 *
    用户
    aycsoft.clientdata.getAsync('user', {
        key: cellvalue,
        callback: function (item) {
            callback(item.F_RealName);
        }
    });
 */

(function ($, aycsoft) {
    "use strict";

    var loadSate = {
        no: -1,  // 还未加载
        yes: 1,  // 已经加载成功
        ing: 0,  // 正在加载中
        fail: 2  // 加载失败
    };

    var clientDataFn = {};
    var clientAsyncData = {};
    var clientData = {};

    function initLoad(callback) {
        var res = loadSate.yes;
        var hasFail = false;
        for (var id in clientDataFn) {
            var _fn = clientDataFn[id];
            if (_fn.state == loadSate.fail) {
                hasFail = true;
            }
            else if (_fn.state == loadSate.no) {
                res = loadSate.ing;
                _fn.init();
            }
            else if (_fn.state == loadSate.ing) {
                res = loadSate.ing;
            }
        }
        if (res == loadSate.yes) {
            callback(hasFail);
        }
        else {
            setTimeout(function () {
                initLoad(callback);
            }, 100);
        }
    }
    function get(key, data) {
        var res = "";
        var len = data.length;
        if (len == undefined) {
            res = data[key];
        }
        else {
            for (var i = 0; i < len; i++) {
                if (key(data[i])) {
                    res = data[i];
                    break;
                }
            }
        }
        return res;
    }

    aycsoft.clientdata = {
        init: function (callback) {
            initLoad(function (res) {
                callback(res);
            });
        },
        get: function (nameArray) {//[key,function (v) { return v.key == value }]
            var res = "";
            if (!nameArray) {
                return res;
            }
            var len = nameArray.length;
            var data = clientData;
            for (var i = 0; i < len; i++) {
                res = get(nameArray[i], data);
                if (res != "" && res != undefined) {
                    data = res;
                }
                else {
                    break;
                }
            }
            res = res || "";
            return res;
        },
        getAsync: function (name, op) {//
            return clientAsyncData[name].get(op);
        },
        getAllAsync: function (name, op) {//
            return clientAsyncData[name].getAll(op);
        },
        getsAsync: function (name, op) {//
            return clientAsyncData[name].gets(op);
        },
        update: function (name) {
            clientAsyncData[name].update && clientAsyncData[name].update();
        }
    };


    /*******************登录后数据***********************/
    // 注册数据的加载方法
    // 功能模块数据

    clientDataFn.modules = {
        state: loadSate.no,
        init: function () {
            //初始化加载数据

            clientDataFn.modules.state = loadSate.ing;
            aycsoft.httpAsyncGet($.rootUrl + '/LR_SystemModule/Module/GetModuleList', function (res) {
                if (res.code == aycsoft.httpCode.success) {
                    clientData.modules = res.data;
                    clientDataFn.modules.toMap();
                    clientDataFn.modules.state = loadSate.yes;
                }
                else {
                    clientData.modules = [];
                    clientDataFn.modules.toMap();
                    clientDataFn.modules.state = loadSate.fail;
                }
            });
        },
        toMap: function () {
            //转化成树结构 和 转化成字典结构

            var modulesTree = {};
            var modulesMap = {};
            var modulesCodeMap = {};
            var _len = clientData.modules.length;
            for (var i = 0; i < _len; i++) {
                var _item = clientData.modules[i];
                if (_item.F_EnabledMark == 1) {
                    modulesTree[_item.F_ParentId] = modulesTree[_item.F_ParentId] || [];
                    modulesTree[_item.F_ParentId].push(_item);
                    modulesMap[_item.F_ModuleId] = _item;
                    modulesCodeMap[_item.F_EnCode] = _item;
                }
            }
            clientData.modulesTree = modulesTree;
            clientData.modulesMap = modulesMap;
            clientData.modulesCodeMap = modulesCodeMap;
        }
    };
    // 登录用户信息

    clientDataFn.userinfo = {
        state: loadSate.no,
        init: function () {
            //初始化加载数据

            clientDataFn.userinfo.state = loadSate.ing;
            aycsoft.httpAsyncGet($.rootUrl + '/Login/GetUserInfo', function (res) {
                if (res.code == aycsoft.httpCode.success) {
                    clientData.userinfo = res.data;
                    clientDataFn.userinfo.state = loadSate.yes;
                }
                else {
                    clientDataFn.userinfo.state = loadSate.fail;
                }
            });
        }
    };

    /*******************使用时异步获取*******************/
    var clientInfo = {};


    // 公司信息
    clientAsyncData.company = {
        states: {},
        init: function (op) {
            if (clientAsyncData.company.states[op.key] == loadSate.no || clientAsyncData.company.states[op.key] == undefined) {
                clientAsyncData.company.states[op.key] = loadSate.ing;
                clientInfo.company = clientInfo.company || {};
                aycsoft.httpAsync('GET', top.$.rootUrl + '/LR_OrganizationModule/Company/GetEntity', { keyValue: op.key }, function (data) {
                    if (!data) {
                        clientAsyncData.company.states[op.key] = loadSate.fail;
                    } else {
                        clientInfo.company[op.key] = data;
                        clientAsyncData.company.states[op.key] = loadSate.yes;
                    }
                });
            }
        },
        get: function (op) {
            clientAsyncData.company.init(op);
            if (clientAsyncData.company.states[op.key] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.company.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = clientInfo.company[op.key] || {};
                op.callback(data, op);
            }
        }
    };
    // 部门信息
    clientAsyncData.department = {
        states: {},
        init: function (op) {
            if (clientAsyncData.department.states[op.key] == loadSate.no || clientAsyncData.department.states[op.key] == undefined) {
                clientAsyncData.department.states[op.key] = loadSate.ing;
                clientInfo.department = clientInfo.department || {};
                aycsoft.httpAsync('GET', top.$.rootUrl + '/LR_OrganizationModule/Department/GetEntity', { departmentId: op.key }, function (data) {
                    if (!data) {
                        clientAsyncData.department.states[op.key] = loadSate.fail;
                    } else {
                        clientInfo.department[op.key] = data;
                        clientAsyncData.department.states[op.key] = loadSate.yes;
                    }
                });
            }
        },
        get: function (op) {
            clientAsyncData.department.init(op);
            if (clientAsyncData.department.states[op.key] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.department.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = clientInfo.department[op.key] || {};
                op.callback(data, op);
            }
        }
    };
    // 人员信息
    clientAsyncData.user = {
        states: {},
        init: function (key) {
            if (clientAsyncData.user.states[key] == loadSate.no || clientAsyncData.user.states[key] == undefined) {
                clientAsyncData.user.states[key] = loadSate.ing;
                clientInfo.user = clientInfo.user || {};
                aycsoft.httpAsync('GET', top.$.rootUrl + '/LR_OrganizationModule/User/GetUserEntity', { userId: key }, function (data) {
                    if (!data) {
                        clientAsyncData.user.states[key] = loadSate.fail;
                    } else {
                        clientInfo.user[key] = data;
                        clientAsyncData.user.states[key] = loadSate.yes;
                    }
                });
            }
        },
        get: function (op) {
            clientAsyncData.user.init(op.key);
            if (clientAsyncData.user.states[op.key] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.user.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = clientInfo.user[op.key] || {};
                op.callback(data, op);
            }
        }
    };
    // 角色信息
    clientAsyncData.role = {
        states: {},
        init: function (key) {
            if (clientAsyncData.role.states[key] == loadSate.no || clientAsyncData.role.states[key] == undefined) {
                clientAsyncData.role.states[key] = loadSate.ing;
                clientInfo.role = clientInfo.role || {};
                aycsoft.httpAsync('GET', top.$.rootUrl + '/LR_OrganizationModule/Role/GetEntity', { keyValue: key }, function (data) {
                    if (!data) {
                        clientAsyncData.role.states[key] = loadSate.fail;
                    } else {
                        clientInfo.role[key] = data;
                        clientAsyncData.role.states[key] = loadSate.yes;
                    }
                });
            }
        },
        get: function (op) {
            clientAsyncData.role.init(op.key);
            if (clientAsyncData.role.states[op.key] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.role.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = clientInfo.role[op.key] || {};
                op.callback(data, op);
            }
        }
    };
    // 数据字典
    clientAsyncData.dataItem = {
        states: {},
        init: function (code) {
            if (clientAsyncData.dataItem.states[code] == loadSate.no || clientAsyncData.dataItem.states[code] == loadSate.fail || clientAsyncData.dataItem.states[code] == undefined) {
                clientInfo.dataItem = clientInfo.dataItem || {};
                clientAsyncData.dataItem.states[code] = loadSate.ing;
                aycsoft.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailList', { itemCode: code }, function (data) {
                    if (!data) {
                        clientAsyncData.dataItem.states = loadSate.fail;
                    } else {
                        clientInfo.dataItem[code] = data;
                        clientAsyncData.dataItem.states[code] = loadSate.yes;
                    }
                });
            }
        },
        get: function (op) {
            clientAsyncData.dataItem.init(op.code);
            if (clientAsyncData.dataItem.states[op.code] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.dataItem.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = clientInfo.dataItem[op.code] || [];

                // 数据字典翻译
                var _item = clientAsyncData.dataItem.find(op.key, data);
                if (_item) {
                    top.aycsoft.language.get(_item.F_ItemName, function (text) {
                        _item.F_ItemName = text;
                        op.callback(_item, op);
                    });
                }
                else {
                    op.callback({}, op);
                }
            }
        },
        getAll: function (op) {
            clientAsyncData.dataItem.init(op.code);
            if (clientAsyncData.dataItem.states[op.code] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.dataItem.getAll(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = clientInfo.dataItem[op.code] || [];
                var res = [];
                $.each(data, function (_index, _item) {
                    _item.F_ItemName = top.aycsoft.language.getSyn(_item.F_ItemName);
                    res[_index] = _item;
                });
                op.callback(res, op);
            }
        },
        gets: function (op) {
            clientAsyncData.dataItem.init(op.code);
            if (clientAsyncData.dataItem.states[op.code] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.dataItem.gets(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = clientInfo.dataItem[op.code] || [];
                if (op.key == undefined || op.key == null) {
                    op.callback('', op);
                }
                else {
                    op.key = op.key + '';
                    var keyList = op.key.split(',');
                    var _text = []
                    $.each(keyList, function (_index, _item) {
                        var _item = clientAsyncData.dataItem.find(_item, data);
                        top.aycsoft.language.get(_item.F_ItemName, function (text) {
                            _text.push(text);
                        });
                    });
                    op.callback(String(_text), op);
                }
            }
        },
        find: function (key, data) {
            var res = {};
            for (var i = 0, len = data.length; i < len; i++) {
                if (data[i].F_ItemValue == key) {
                    res = data[i];
                    break;
                }
            }
            return res;
        },
        update: function (code) {
            clientAsyncData.dataItem.states[code] = loadSate.no;
        }
    };
    // 数据源数据
    clientAsyncData.sourceData = {
        states: {},
        get: function (op) {
            if (clientAsyncData.sourceData.states[op.code] == undefined || clientAsyncData.sourceData.states[op.code] == loadSate.no) {
                clientAsyncData.sourceData.states[op.code] = loadSate.ing;
                clientAsyncData.sourceData.load(op.code);
            }

            if (clientAsyncData.sourceData.states[op.code] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.sourceData.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = clientInfo.sourceData[op.code] || [];
                if (data) {
                    op.callback(clientAsyncData.sourceData.find(op.key, op.keyId, data) || {}, op);
                } else {
                    op.callback({}, op);
                }
            }
        },
        getAll: function (op) {
            if (clientAsyncData.sourceData.states[op.code] == undefined || clientAsyncData.sourceData.states[op.code] == loadSate.no) {
                clientAsyncData.sourceData.states[op.code] = loadSate.ing;
                clientAsyncData.sourceData.load(op.code);
            }

            if (clientAsyncData.sourceData.states[op.code] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.sourceData.getAll(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else if (clientAsyncData.sourceData.states[op.code] == loadSate.yes) {
                var data = clientInfo.sourceData[op.code] || [];

                if (!!data) {
                    op.callback(data, op);
                } else {
                    op.callback({}, op);
                }
            }
        },
        load: function (code) {
            clientInfo.sourceData = clientInfo.sourceData || {};
            aycsoft.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DataSource/GetDataTable', { code: code }, function (data) {
                console.log(data);
                if (!data) {
                    clientAsyncData.sourceData.states[code] = loadSate.fail;
                } else {
                    clientInfo.sourceData[code] = data;
                    clientAsyncData.sourceData.states[code] = loadSate.yes;
                }
            });
        },
        find: function (key, keyId, data) {
            var res = {};
            for (var i = 0, l = data.length; i < l; i++) {
                if (data[i][keyId] == key) {
                    res = data[i];
                    break;
                }
            }
            return res;
        },
        update: function (op) {
            clientAsyncData.sourceData.load(op.code);
        }
    };
    // 获取自定义数据 url key valueId
    clientAsyncData.custmerData = {
        states: {},
        get: function (op) {
            if (clientAsyncData.custmerData.states[op.url] == undefined || clientAsyncData.custmerData.states[op.url] == loadSate.no) {
                clientAsyncData.custmerData.states[op.url] = loadSate.ing;
                clientAsyncData.custmerData.load(op.url);
            }
            if (clientAsyncData.custmerData.states[op.url] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.custmerData.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = clientData[op.url] || [];
                if (!!data) {
                    op.callback(clientAsyncData.custmerData.find(op.key, op.keyId, data) || {}, op);
                } else {
                    op.callback({}, op);
                }
            }
        },
        gets: function (op) {
            if (clientAsyncData.custmerData.states[op.url] == undefined || clientAsyncData.custmerData.states[op.url] == loadSate.no) {
                clientAsyncData.custmerData.states[op.url] = loadSate.ing;
                clientAsyncData.custmerData.load(op.url);
            }
            if (clientAsyncData.custmerData.states[op.url] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.custmerData.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = clientData[op.url] || [];
                if (!!data) {
                    if (op.key == undefined || op.key == null) {
                        op.callback('', op);
                    }
                    else {
                        op.key = op.key + '';
                        var keyList = op.key.split(',');
                        var _text = []
                        $.each(keyList, function (_index, _item) {
                            var _item = clientAsyncData.custmerData.find(op.key, op.keyId, data) || {};
                            if (_item[op.textId]) {
                                _text.push(_item[op.textId]);
                            }

                        });
                        op.callback(String(_text), op);
                    }


                } else {
                    op.callback('', op);
                }
            }
        },
        getAll: function (op) {
            if (clientAsyncData.custmerData.states[op.url] == undefined || clientAsyncData.custmerData.states[op.url] == loadSate.no) {
                clientAsyncData.custmerData.states[op.url] = loadSate.ing;
                clientAsyncData.custmerData.load(op.url);
            }
            if (clientAsyncData.custmerData.states[op.url] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.custmerData.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = clientData[op.url] || [];
                if (!!data) {
                    op.callback(data, op);
                } else {
                    op.callback([], op);
                }
            }
        },
        load: function (url) {
            aycsoft.httpAsync('GET', top.$.rootUrl + url, {}, function (data) {
                if (!!data) {
                    clientData[url] = data;
                }
                clientAsyncData.custmerData.states[url] = loadSate.yes;
            });
        },
        find: function (key, keyId, data) {
            var res = {};
            for (var i = 0, l = data.length; i < l; i++) {
                if (data[i][keyId] == key) {
                    res = data[i];
                    break;
                }
            }
            return res;
        }
    };
})(window.jQuery, top.aycsoft);