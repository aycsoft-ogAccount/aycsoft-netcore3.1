/*
 * 版 本 Aycsoft-ADMS-Core Aycsoft敏 捷开发框架(http://www.aycsoft.cn)
 * Copyright (c) 2021-present 广州轻创软件信息科技有限公司
 * 创建人：Aycsoft-前端 开发组
 * 日 期：2017.03.16
 * 描 述：tab窗口操作方法
 */
(function ($, aycsoft) {
    "use strict";
    //初始化菜单和tab页的属性Id
    var iframeIdList = {};

    aycsoft.frameTab = {
        iframeId: '',
        init: function () {
            aycsoft.frameTab.bind();
        },
        bind: function () {
            $(".lr-frame-tabs-wrap").lrscroll();
        },
        setCurrentIframeId: function (iframeId) {
            aycsoft.iframeId = iframeId;
        },
        open: function (module, notAllowClosed) {
            var $tabsUl = $('#lr_frame_tabs_ul');
            var $frameMain = $('#lr_frame_main');

            if (iframeIdList[module.F_ModuleId] == undefined || iframeIdList[module.F_ModuleId] == null) {
                // 隐藏之前的tab和窗口
                if (aycsoft.frameTab.iframeId != '') {
                    $tabsUl.find('#lr_tab_' + aycsoft.frameTab.iframeId).removeClass('active');
                    $frameMain.find('#lr_iframe_' + aycsoft.frameTab.iframeId).removeClass('active');
                    iframeIdList[aycsoft.frameTab.iframeId] = 0;
                }
                var parentId = aycsoft.frameTab.iframeId;
                aycsoft.frameTab.iframeId = module.F_ModuleId;
                iframeIdList[aycsoft.frameTab.iframeId] = 1;

                // 打开一个功能模块tab_iframe页面
                var $tabItem = $('<li class="lr-frame-tabItem active" id="lr_tab_' + module.F_ModuleId + '" parent-id="' + parentId + '"  ><span>' + module.F_FullName + '</span></li>');
                // 翻译
                aycsoft.language.get(module.F_FullName, function (text) {
                    $tabItem.find('span').text(text);
                    if (!notAllowClosed) {
                        $tabItem.append('<span class="reomve" title="关闭窗口"></span>');
                    }
                });


                var _url = module.F_UrlAddress;
                if (_url.indexOf('http://') == -1 && _url.indexOf('https://') == -1) {
                    _url = $.rootUrl + module.F_UrlAddress;
                    if (_url.indexOf('?') != -1) {
                        _url += '&lraccount=' + $.lcoreUser.account;
                    }
                    else {
                        _url += '?lraccount=' + $.lcoreUser.account;
                    }
                    if (module.F_EnCode) {
                        _url += '&lrmcode=' + module.F_EnCode;
                    }
                }

                var $iframe = $('<iframe class="lr-frame-iframe active" id="lr_iframe_' + module.F_ModuleId + '" frameborder="0" src="' + _url + '"></iframe>');
                $tabsUl.append($tabItem);
                $frameMain.append($iframe);

                var w = 0;
                var width = $tabsUl.children().each(function () {
                    w += $(this).outerWidth();
                });
                $tabsUl.css({ 'width': w });
                $tabsUl.parent().css({ 'width': w });


                $(".lr-frame-tabs-wrap").lrscrollSet('moveRight');



                //绑定一个点击事件
                $tabItem.on('click', function () {
                    var id = $(this).attr('id').replace('lr_tab_', '');
                    aycsoft.frameTab.focus(id);
                });
                $tabItem.find('.reomve').on('click', function () {
                    var id = $(this).parent().attr('id').replace('lr_tab_', '');
                    aycsoft.frameTab.close(id);
                    return false;
                });

                if (!!aycsoft.frameTab.opencallback) {
                    aycsoft.frameTab.opencallback();
                }
                if (!notAllowClosed) {
                    aycsoft.httpAsyncPost(top.$.rootUrl + "/Home/VisitModule", { moduleName: module.F_FullName, moduleUrl: module.F_UrlAddress });
                }
            }
            else {
                aycsoft.frameTab.focus(module.F_ModuleId);
            }
        },
        focus: function (moduleId) {
            if (iframeIdList[moduleId] == 0) {
                // 定位焦点tab页
                $('#lr_tab_' + aycsoft.frameTab.iframeId).removeClass('active');
                $('#lr_iframe_' + aycsoft.frameTab.iframeId).removeClass('active');
                iframeIdList[aycsoft.frameTab.iframeId] = 0;

                $('#lr_tab_' + moduleId).addClass('active');
                $('#lr_iframe_' + moduleId).addClass('active');
                aycsoft.frameTab.iframeId = moduleId;
                iframeIdList[moduleId] = 1;

                if (!!aycsoft.frameTab.opencallback) {
                    aycsoft.frameTab.opencallback();
                }
            }
        },
        leaveFocus: function () {
            $('#lr_tab_' + aycsoft.frameTab.iframeId).removeClass('active');
            $('#lr_iframe_' + aycsoft.frameTab.iframeId).removeClass('active');
            iframeIdList[aycsoft.frameTab.iframeId] = 0;
            aycsoft.frameTab.iframeId = '';
        },
        close: function (moduleId) {
            var pTabId = 'lr_tab_' + top.$('#lr_tab_' + aycsoft.frameTab.iframeId).attr('parent-id');
            delete iframeIdList[moduleId];

            var $this = $('#lr_tab_' + moduleId);
            var $prev = $this.prev();// 获取它的上一个节点数据;
            if ($prev.length < 1) {
                $prev = $this.next();
            }
            $this.remove();
            $('#lr_iframe_' + moduleId).remove();
            if (moduleId == aycsoft.frameTab.iframeId && $prev.length > 0) {
                var prevId = $prev.attr('id').replace('lr_tab_', '');

                $prev.addClass('active');
                $('#lr_iframe_' + prevId).addClass('active');
                aycsoft.frameTab.iframeId = prevId;
                iframeIdList[prevId] = 1;
            }
            else {
                if ($prev.length < 1) {
                    aycsoft.frameTab.iframeId = "";
                }
            }

            var $tabsUl = $('#lr_frame_tabs_ul');
            var w = 0;
            var width = $tabsUl.children().each(function () {
                w += $(this).outerWidth();
            });
            $tabsUl.css({ 'width': w });
            $tabsUl.parent().css({ 'width': w });

            if (!!aycsoft.frameTab.closecallback) {
                aycsoft.frameTab.closecallback();
            }
            $('#' + pTabId).trigger('click')
        }
        // 获取当前窗口
        , currentIframe: function () {
            var ifameId = 'lr_iframe_' + aycsoft.frameTab.iframeId;
            if (top.frames[ifameId].contentWindow != undefined) {
                return top.frames[ifameId].contentWindow;
            }
            else {
                return top.frames[ifameId];
            }
        }
        , parentIframe: function () {
            var ifameId = 'lr_iframe_' + top.$('#lr_tab_' + aycsoft.frameTab.iframeId).attr('parent-id');
            if (top.frames[ifameId].contentWindow != undefined) {
                return top.frames[ifameId].contentWindow;
            }
            else {
                return top.frames[ifameId];
            }
        }
        , wfFormIframe: function () {
            var currentIframe = aycsoft.frameTab.currentIframe();
            var iframeId = currentIframe.$('#form_list_iframes .form-list-iframe.active').attr('id');
            return aycsoft.iframe(iframeId, currentIframe.frames);
        }
        , closeByParam: function (name, value) {
            $('#lr_frame_tabs_ul li').each(function () {
                var id = $(this).attr('id').replace('lr_tab_', '');

                var frameObj = top.frames['lr_iframe_' + id];
                if (frameObj.contentWindow != undefined) {
                    frameObj = frameObj.contentWindow;
                }
                if (frameObj[name] == value) {
                    aycsoft.frameTab.close(id);
                    return false;
                }
            });
        }
        , opencallback: false
        , closecallback: false
    };

    aycsoft.frameTab.init();
})(window.jQuery, top.aycsoft);