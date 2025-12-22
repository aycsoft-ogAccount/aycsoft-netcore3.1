(function ($, aycsoft) {
    "use strict";
    var connection;
    var isConnection = -1;// -1 没开始连接，1连接成功，0 失败
    var loadingMsg2 = {};

    var msgList = {};
    var imChat;
   
    var loadingMsg = {};



    var imUserId = '';     // 当前聊天的人
    var sysUserMap = {};   // 系统注册发送消息人

    var _im = {
        init: function () {
            _im.registerServer();
            _im.connect();
        }
        // 连接服务端
        , connect: function () {
            var loginInfo = aycsoft.clientdata.get(['userinfo']);
            
            connection = new signalR.HubConnectionBuilder().withUrl(imconfig.url + "/ChatsHub").build();
            console.log(loginInfo, imconfig, connection)

            connection.on('revMsg', function (userId, msg, dateTime, isSystem) {
                if (!loadingMsg2[userId]) {
                    var point = { userId: userId, content: msg, time: dateTime, isSystem: isSystem || 0 };
                    addMsgList(userId, point);
                    aycsoft.im.revMsg && aycsoft.im.revMsg(userId, msg, dateTime, isSystem || 0);
                }
            });

            connection.onclose(function(e){
                _im.disconnected();
                console.log('Connection closed!', e);
            });
            
            Object.defineProperty(WebSocket, 'OPEN', { value: 1, });
            connection.start().then(function () {
                _im.afterSuccess();
                var loginInfo = aycsoft.clientdata.get(['userinfo']);
                connection.invoke("SendInfo", loginInfo.F_UserId);
            }).catch(function (err) {
                _im.disconnected();
                return console.error(err.toString());
            });

         
        }
        // 连接成功后执行方法
        , afterSuccess: function () {
            isConnection = 1;
            $('.lr-im-bell').show();
        }
        // 断开连接后执行
        , disconnected: function () {
            isConnection = 0;
        }
        // 注册服务端方法
        ,registerServer: function () {
            // 发送信息
            _im.sendMsg = function (userId, msg) {
                if (isConnection == 1) {
                    var loginInfo = aycsoft.clientdata.get(['userinfo']);
                    connection.invoke("SendMsg", loginInfo.F_UserId,userId, msg, 0)
                    //imChat.server.sendMsg(userId, msg, 0);
                }
                else if (isConnection == -1) {
                    setTimeout(function () {
                        _im.sendMsg(userId, msg);
                    }, 100);
                }
            };
        }
    };


    function addMsgList(userId, item) {
        msgList[userId] = msgList[userId] || [];
        if (loadingMsg[userId]) {
            setTimeout(function () {
                addMsgList(userId, item);
            }, 100);
        }
        else {
            msgList[userId].push(item);
        }
    }

    var getTime = function (time) {
        var d = new Date();
        var c = d.DateDiff('d', time);
        if (c <= 1) {
            return aycsoft.formatDate(time, 'hh:mm:ss');
        }
        else {
            return aycsoft.formatDate(time, 'yyyy/MM/dd');
        }
    }
    // 发送聊天信息
    var sendMsg = function (msg, time) {
        var loginInfo = aycsoft.clientdata.get(['userinfo']);
        aycsoft.clientdata.getAsync('user', {
            key: loginInfo.userId,
            callback: function (data, op) {
                data.id = op.key;
                var _html = '\
                <div class="me im-time">'+ (time || '') + '</div>\
                <div class="im-me">\
                    <div class="headimg"><img src="' + top.$.rootUrl + '/LR_OrganizationModule/User/HeadImg?account=userhead_' + data.F_Account+ '"></div>\
                    <div class="arrow"></div>\
                    <span class="content">'+ msg + '</span>\
                </div>';

                $('.lr-im-msgcontent .lr-scroll-box').append(_html);
                $('.lr-im-msgcontent').lrscrollSet('moveBottom');
            }
        });
    };
    // 接收聊天消息
    var revMsg = function (userId, msg, time) {
        aycsoft.clientdata.getAsync('user', {
            key: userId,
            callback: function (data, op) {
                data.id = op.key;
                var _html = '\
                <div class="im-time">'+ (time || '') + '</div>\
                <div class="im-other">\
                    <div class="headimg"><img src="' + top.$.rootUrl + '/LR_OrganizationModule/User/HeadImg?account=userhead_' + data.F_Account + '"></div>\
                    <div class="arrow"></div>\
                    <span class="content">'+ msg + '</span>\
                </div>';


                $('.lr-im-msgcontent .lr-scroll-box').append(_html);
                $('.lr-im-msgcontent').lrscrollSet('moveBottom');
            }
        });
    };
    // 获取联系人
    var loadUserList = function (pid, type, deep, $list, companyId) {
        if (type == 'company') {// 公司
            aycsoft.httpAsync('GET', top.$.rootUrl + '/LR_OrganizationModule/Company/GetListByPId', { pid: pid}, function (data) {
                if (data) {
                    
                    $.each(data || [], function (index,item) {
                        var _html = '\
                            <div class="lr-im-company-item">\
                                <div class="lr-im-item-name lr-im-company" data-value="'+ item.F_CompanyId + '"  data-deep="' + deep +'" >\
                                    <i class="fa fa-angle-right"></i>'+ item.F_FullName + '\
                                    <img class="lr-im-loading-img" src="'+ top.$.rootUrl +'/img/aycsofttree/loading.gif">\
                                </div>\
                            </div>';
                        $list.append(_html);
                        
                    })
                   
                    console.log(data)
                }
                $list.addClass('loadcompany')
            })
        }
        else if(type == 'department') {// 部门
            aycsoft.httpAsync('GET', top.$.rootUrl + '/LR_OrganizationModule/Department/GetListByPid', { companyId: companyId, pid: pid}, function (data) {
                if (data) {
                    $.each(data || [], function (index, item) {
                        var _html = '\
                            <div class="lr-im-company-item">\
                                <div class="lr-im-item-name lr-im-department" data-cid="'+ companyId +'" data-value="'+ item.F_DepartmentId + '"  data-deep="' + deep + '" >\
                                    <i class="fa fa-angle-right"></i>'+ item.F_FullName + '\
                                    <img class="lr-im-loading-img" src="'+ top.$.rootUrl +'/img/aycsofttree/loading.gif">\
                                </div>\
                            </div>';
                        $list.append(_html);
                    })
                }
                $list.addClass('loaddepartment')
            })
        }
        else {// 用户
            aycsoft.httpAsync('GET', top.$.rootUrl + '/LR_OrganizationModule/User/GetList', { companyId: companyId, departmentId: pid }, function (data) {
                if (data) {
                    $.each(data || [], function (index, item) {
                        var _html = '\
                            <div class="lr-im-company-item">\
                                <div class="lr-im-item-name lr-im-user" data-value="'+ item.F_UserId + '" >\
                                     <img src="' + top.$.rootUrl + '/LR_OrganizationModule/User/HeadImg?account=userhead_' + item.F_Account + '" >' + item.F_RealName + '\
                                </div>\
                            </div>';
                        $list.append(_html);
                    })
                }
                $list.addClass('loaduser')
            })
        }
    }

    function isLoadUserList(isOk, callback) {
        if (isOk()) {
            callback()
        }
        else {
            setTimeout(function () { isLoadUserList(isOk, callback) },100)
        }
    }


    aycsoft.im = {
        init: function () {
            if (!imconfig.isOpen) {
                return;
            }
            aycsoft.im.bind();
            aycsoft.im.load();
        },
        addContacts: function (userId) {// 添加联系人
            aycsoft.httpAsync('Post', top.$.rootUrl + '/LR_IM/IMMsg/AddContact', { otherUserId: userId }, function (data) {});
        },
        removeContacts: function (userId) {// 移除联系人
            aycsoft.httpAsync('Post', top.$.rootUrl + '/LR_IM/IMMsg/RemoveContact', { otherUserId: userId }, function (data) { });
        },
        getContacts: function (callback) {// 获取最近的联系人列表
            aycsoft.httpAsync('GET', top.$.rootUrl + '/LR_IM/IMMsg/GetContactsList', {}, function (res) {
                if (res) {
                    _im.init();
                    callback(res.data || [], res.sysUserList || []);
                }
            })
        },
        updateContacts: function (userId) {
            aycsoft.httpAsync('Post', top.$.rootUrl + '/LR_IM/IMMsg/UpdateContactState', { otherUserId: userId}, function (data) {
            });
        },
        sendMsg: function (userId, msg) {// 发送消息
            var time = "";
            var loginInfo = aycsoft.clientdata.get(['userinfo']);
            var point = { userId: loginInfo.userId, content: msg, time: aycsoft.getDate('yyyy-MM-dd hh:mm:ss'), isSystem: 0 };
            addMsgList(userId, point);
            aycsoft.httpAsync('Post', top.$.rootUrl + '/LR_IM/IMMsg/SendMsg', { userId: userId, content: msg }, function (data) {
                _im.sendMsg(userId, msg);// 发送给即时通讯服务
            });
            if (msgList[userId].length > 1) {
                if (aycsoft.parseDate(point.time).DateDiff('s', msgList[userId][msgList[userId].length - 2].time) > 60) {
                    time = point.time;
                }
            }
            else {
                time = point.time;
            }
            return time;
        },
        getMsgList: function (userId, callback,isGetMsgList) {
            msgList[userId] = msgList[userId] || [];
            loadingMsg[userId] = true;
            if (msgList[userId].length == 0 && isGetMsgList) {// 如果没有信息，获取最近10条的聊天记录
                loadingMsg2[userId] = true;
                aycsoft.httpAsync('GET', top.$.rootUrl + '/LR_IM/IMMsg/GetMsgList', { userId: userId }, function (data) {
                    msgList[userId] = msgList[userId] || [];
                    data = data || [];
                    var len = data;
                    if (len > 0) {
                        for (var i = len - 1; i >= 0; i--) {
                            var item = data[i];
                            var point = { userId: _item.F_SendUserId, content: _item.F_Content, time: _item.F_CreateDate, isSystem: _item.F_IsSystem || 0 };
                            msgList[userId].push(point);
                        }
                    }
                    callback(msgList[userId]);
                    loadingMsg[userId] = false;
                    loadingMsg2[userId] = false;
                });
            }
            else {
                callback(msgList[userId]);
                loadingMsg[userId] = false;
            }
        },
        registerRevMsg: function (callback) {// 获取消息记录
            aycsoft.im.revMsg = callback;
        },

        load: function () {
            // 获取最近联系人列表
            aycsoft.im.getContacts(function (data, sysUserList) {
                $.each(sysUserList, function (_index, _item) {
                    sysUserMap[_item.F_Code] = _item;
                });

                var $userList = $('#lr_immsg_userlist .lr-scroll-box');
                $.each(data, function (_index, _item) {
                    var html = '<div class="msg-item' + (_item.F_IsRead == '1' ? ' imHasMsg' : '') + '" data-value="' + _item.F_OtherUserId + '" >';
                    html += '<div class="photo">';

                    if (sysUserMap[_item.F_OtherUserId]) {
                        html += '<i class="' + sysUserMap[_item.F_OtherUserId].F_Icon + '" ></i>';
                    }
                    else {
                        html += '<img src="' + top.$.rootUrl +'/LR_OrganizationModule/User/HeadImg?account=1">';
                    }

                    html += '<div class="point"></div>';
                    html += '</div>';
                    html += '<div class="name"></div>';
                    html += '<div class="msg">' + (_item.F_Content || '') + '</div>';
                    html += '<div class="date">' + getTime(_item.F_Time) + '</div>';
                    html += '</div>';

                    $userList.append(html);
                    if (sysUserMap[_item.F_OtherUserId]) {
                        var _$item = $userList.find('[data-value="' + _item.F_OtherUserId + '"]');
                        _$item.find('.name').text(sysUserMap[_item.F_OtherUserId].F_Name);
                        _$item.addClass('sys')
                        _$item = null;
                    }
                    else {
                        aycsoft.clientdata.getAsync('user', {
                            key: _item.F_OtherUserId,
                            callback: function (data, op) {
                                console.log(data)

                                var $item = $userList.find('[data-value="' + op.key + '"]');
                                $item.find('.name').text(data.F_RealName);
                                $item.find('img').attr('src', top.$.rootUrl + '/LR_OrganizationModule/User/HeadImg?account=userhead_' + data.F_Account);
                                $item = null;
                            }
                        });
                    }
                });
            })
            var $list = $('#lr_im_content_userlist .lr-scroll-box');
            loadUserList('0', 'company', 0, $list)
        },
        bind: function () {
            $('#lr_immsg_userlist').lrscroll();
            $('#lr_im_content_userlist').lrscroll();
            $('#lr_im_content_userlist2').lrscroll();
            $('.lr-im-msgcontent').lrscroll();
          

            $('.lr-im-bell').on('click', function () {
                var $this = $(this);
                if ($this.hasClass('open')) {
                    $this.removeClass('open');
                    $('.lr-im-body').removeClass('open');
                    $('.lr-im-black-overlay').hide();
                    imUserId = '';
                }
                else {
                    $this.addClass('open');
                    $('.lr-im-bell .point').hide();
                    $('.lr-im-body').addClass('open');
                }
            });
            // 最近消息 与 联系人之间的切换
            $('.lr-im-title .title-item').on('click', function () {
                var $this = $(this);
                if (!$this.hasClass('active')) {
                    $('.lr-im-body>.active').removeClass('active');
                    $('.lr-im-title>.active').removeClass('active');
                    $this.addClass('active');
                    var v = $this.attr('data-value');
                    $('#' + v).addClass('active');
                }
            });

            // 联系人
            $('#lr_im_content_userlist .lr-scroll-box').on('click', function (e) {
                e = e || window.event;
                var et = e.target || e.srcElement;
                var $et = $(et);

                if (et.tagName == 'IMG' || et.tagName == 'I') {
                    $et = $et.parent();
                }

                if ($et.hasClass('lr-im-company')) {// 点击公司项
                    // 判断是否加载子项
                    if ($et.hasClass('lr-im-loading')) {
                        return false;
                    }
                    else if ($et.hasClass('lr-im-loaded')) {
                        if ($et.parent().hasClass('open')) {
                            $et.parent().removeClass('open');

                        } else {
                            $et.parent().addClass('open')
                        }
                    }
                    else {
                        $et.addClass('lr-im-loading')
                        var id = $et.attr('data-value');
                        var deep = parseInt($et.attr('data-deep'));
                        var $list = $('<div class="lr-im-user-list" ></div>');
                        $list.css({ 'padding-left': '10px' });
                        var flag = false;

                        loadUserList('0', 'department', deep + 1, $list, id)
                        loadUserList(id, 'company', deep + 1, $list)

                        isLoadUserList(function () {
                            if ($list.hasClass('loadcompany') && $list.hasClass('loaddepartment')) {
                                return true
                            }
                            else {
                                return false
                            }
                        }, function () {
                            $et.removeClass('lr-im-loading')
                            $et.addClass('lr-im-loaded')
                            $et.parent().append($list)
                            $et.parent().addClass('open')
                        })

                    }
                    return false;
                }
                else if ($et.hasClass('lr-im-department')) {
                    if ($et.hasClass('lr-im-loading')) {
                        return false;
                    }
                    else if ($et.hasClass('lr-im-loaded')) {
                        if ($et.parent().hasClass('open')) {
                            $et.parent().removeClass('open');

                        } else {
                            $et.parent().addClass('open')
                        }
                    }
                    else {
                        $et.addClass('lr-im-loading')
                        var id = $et.attr('data-value');
                        var cid = $et.attr('data-cid');
                        var deep = parseInt($et.attr('data-deep'));
                        var $list = $('<div class="lr-im-user-list" ></div>');
                        $list.css({ 'padding-left': '10px' });
                        var flag = false;

                        loadUserList(id, 'user', deep + 1, $list, cid)
                        loadUserList(id, 'department', deep + 1, $list, cid)
                       

                        isLoadUserList(function () {
                            if ($list.hasClass('loaddepartment') && $list.hasClass('loaduser')) {
                                return true
                            }
                            else {
                                return false
                            }
                        }, function () {
                            $et.removeClass('lr-im-loading')
                            $et.addClass('lr-im-loaded')
                            $et.parent().append($list)
                            $et.parent().addClass('open')
                        })

                    }
                    return false;
                }
                else if ($et.hasClass('lr-im-user')) {
                    // 如果是用户列表
                    // 1.打开聊天窗口
                    // 2.添加一条最近联系人数据（如果没有添加的话）
                    // 3.获取最近的20条聊天数据或者最近的聊天信息


                    var id = $et.attr('data-value');
                    var $userList = $('#lr_immsg_userlist .lr-scroll-box');
                    var $userItem = $userList.find('[data-value="' + id + '"]');

                    // 更新下最近的联系人列表数据
                    $('.lr-im-title .title-item').eq(0).trigger('click');

                    imUserId = id;
                    if ($userItem.length > 0) {
                        $userList.prepend($userItem);
                        $userItem.trigger('click');
                    }
                    else {
                        var imgurl = $et.find('img').attr('src');
                        var _html = '\
                            <div class="msg-item" data-value="' + id + '" >\
                                <div class="photo">\
                                    <img src="'+ imgurl + '">\
                                    <div class="point"></div>\
                                </div>\
                                <div class="name"></div>\
                                <div class="msg"></div>\
                                <div class="date"></div>\
                            </div>';
                        $userList.prepend(_html);
                        $userItem = $userList.find('[data-value="' + id + '"]');
                        // 获取人员数据
                        aycsoft.clientdata.getAsync('user', {
                            key: id,
                            callback: function (data, op) {
                                $userList.find('[data-value="' + op.key + '"] .name').text(data.F_RealName);
                                $userItem.trigger('click');
                            }
                        });
                        aycsoft.im.addContacts(id);
                    }

                }
            });
            // 最近联系人列表点击
            $('#lr_immsg_userlist .lr-scroll-box').on('click', function (e) {
                e = e || window.event;
                var et = e.target || e.srcElement;
                var $et = $(et);

                if (!$et.hasClass('msg-item')) {
                    $et = $et.parents('.msg-item');
                }
                if ($et.length > 0) {
                    if (!$et.hasClass('active')) {
                        


                        var name = $et.find('.name').text();
                        imUserId = $et.attr('data-value');

                       

                        if ($et.hasClass('sys')) {
                            aycsoft.layerForm({
                                id: 'LookMsgIndex',
                                title: '查看消息-' + name,
                                url: top.$.rootUrl + '/LR_IM/IMMsg/Index?userId=' + imUserId + '&name=' + name,
                                width: 800,
                                height: 500,
                                maxmin: true,
                                btn: null
                            });
                            return;
                        }

                        $('#lr_immsg_userlist .lr-scroll-box .active').removeClass('active');
                        $et.addClass('active');
                        $('.lr-im-black-overlay').show();
                        var $imdialog = $('.lr-im-dialog');
                        $imdialog.find('.im-title').text("与" + name + "对话中");

                        $('#lr_im_input').val('');
                        $('#lr_im_input').select();

                        $('.lr-im-msgcontent .lr-scroll-box').html('');
                        // 获取聊天信息
                        aycsoft.im.getMsgList(imUserId, function (data) {
                            var len = data.length;
                            if (len > 0) {
                                for (var i = len - 1; i >= 0; i--) {
                                    var _item = data[i];
                                    aycsoft.clientdata.getAsync('user', {
                                        key: _item.userId,
                                        msg: _item.content,
                                        time: _item.time,
                                        callback: function (data, op) {
                                            var loginInfo = aycsoft.clientdata.get(['userinfo']);
                                            var _html = '\
                                            <div class="im-time '+ (loginInfo.userId == op.key ? 'me' : '') + ' ">' + op.time + '</div>\
                                            <div class="'+ (loginInfo.userId == op.key ? 'im-me' : 'im-other') + '">\
                                                <div class="headimg"><img src="'+ top.$.rootUrl + '/LR_OrganizationModule/User/HeadImg?account=userhead_' + data.F_Account + '"></div>\
                                                <div class="arrow"></div>\
                                                <span class="content">'+ op.msg + '</span>\
                                            </div>';
                                            $('.lr-im-msgcontent .lr-scroll-box').prepend(_html);
                                        }
                                    });
                                }
                                $('.lr-im-msgcontent').lrscrollSet('moveBottom');
                            }
                        }, $et.hasClass('imHasMsg'));
                        $et.removeClass('imHasMsg');
                        aycsoft.im.updateContacts(imUserId);
                    }
                }
            });

            // 联系人搜索
            $('.lr-im-search input').on("keypress", function (e) {
                e = e || window.event;
                if (e.keyCode == "13") {
                    var $this = $(this);
                    var keyword = $this.val();
                    
                    if (keyword) {
                        $('#lr_im_content_userlist').hide()
                        $('#lr_im_content_userlist2').show()
                        var $list = $('#lr_im_content_userlist2 .lr-scroll-box');

                        //GetAllList
                        aycsoft.httpAsync('GET', top.$.rootUrl + '/LR_OrganizationModule/User/GetAllList', { keyword: keyword }, function (data) {
                            $list.html('')
                            if (data) {
                                $.each(data || [], function (index, item) {
                                    var _html = '\
                                    <div class="lr-im-company-item">\
                                        <div class="lr-im-item-name lr-im-user" data-value="'+ item.F_UserId + '" >\
                                             <img src="' + top.$.rootUrl + '/LR_OrganizationModule/User/HeadImg?account=userhead_' + item.F_Account + '" >' + item.F_RealName + '\
                                        </div>\
                                    </div>';
                                    $list.append(_html);
                                })
                            }
                        })
                    }
                    else {
                        $('#lr_im_content_userlist2').hide()
                        $('#lr_im_content_userlist').show()
                    }
                }
            });
            // 发送消息
            $('#lr_im_input').on("keypress", function (e) {
                e = e || window.event;
                if (e.keyCode == "13") {
                    var text = $(this).val();
                    $(this).val('');
                    if (text.replace(/(^\s*)|(\s*$)/g, "") != '') {
                        var time = aycsoft.im.sendMsg(imUserId, text);
                        sendMsg(text, time);
                        var $userItem = $('#lr_immsg_userlist .lr-scroll-box [data-value="' + imUserId + '"]');
                        $userItem.find('.msg').text(text);
                        $userItem.find('.date').text(getTime(aycsoft.getDate('yyyy-MM-dd hh:mm:ss')));
                        $userItem = null;
                    }
                    return false;
                }
            });
            // 注册消息接收
            aycsoft.im.registerRevMsg(function (userId, msg, dateTime) {
                var $userList = $('#lr_immsg_userlist .lr-scroll-box');
                var $userItem = $userList.find('[data-value="' + userId + '"]');
                // 判断当前账号是否打开聊天窗口
                if (userId == imUserId) {
                    revMsg(userId, msg, dateTime);
                    aycsoft.im.updateContacts(userId);
                    $userItem.find('.msg').text(msg);
                    $userItem.find('.date').text(getTime(dateTime));
                }
                else {
                    if ($userItem.length > 0) {
                        $userList.prepend($userItem);
                        if (!$userItem.hasClass('imHasMsg')) {
                            $userItem.addClass('imHasMsg');
                        }
                        $userItem.find('.msg').text(msg);
                        $userItem.find('.date').text(getTime(dateTime));
                    }
                    else {
                        var html = '<div class="msg-item" data-value="' + userId + '" >';
                        html += '<div class="photo">';

                        if (sysUserMap[userId]) {
                            html += '<i class="' + sysUserMap[userId].F_Icon + '" ></i>';
                        }
                        else {
                            html += '<img src="' + top.$.rootUrl + '/Content/images/head/on-boy.jpg" >';
                        }

                        html += '<div class="point"></div>';
                        html += '</div>';
                        html += '<div class="name"></div>';
                        html += '<div class="msg">' + msg + '</div>';
                        html += '<div class="date">' + getTime(dateTime) + '</div>';
                        html += '</div>';
                        $userList.prepend(html);

                        if (sysUserMap[userId]) {
                            var _$item = $userList.find('[data-value="' + userId + '"]');
                            _$item.find('.name').text(sysUserMap[userId].F_Name);
                            _$item = null;
                        }
                        else {
                            aycsoft.clientdata.getAsync('user', {
                                key: userId,
                                callback: function (data, op) {
                                    var $item = $userList.find('[data-value="' + op.key + '"]');
                                    $item.find('.name').text(data.name);
                                    data.id = op.key;
                                    $item.find('img').attr('src', getHeadImg(data));
                                    $item = null;
                                }
                            });
                        }
                    }
                }
                if (!$('.lr-im-bell').hasClass('open')) {
                    $('.lr-im-bell .point').show();
                }
            });
            // 查看聊天记录
            $('#lr_im_look_msg_btn').on('click', function () {
                aycsoft.layerForm({
                    id: 'LookMsgIndex',
                    title: '查看聊天记录-' + $('#lr_im_msglist .lr-im-right .lr-im-touser').text(),
                    url: top.$.rootUrl + '/LR_IM/IMMsg/Index?userId=' + imUserId,
                    width: 800,
                    height: 500,
                    maxmin: true,
                    btn: null
                });
            });

            $('.im-close').on('click', function () {
                $('#lr_immsg_userlist .lr-scroll-box [data-value="' + imUserId + '"]').removeClass('active');
                $('.lr-im-black-overlay').hide();
                imUserId = '';
            });
        }
    };

})(jQuery, top.aycsoft);