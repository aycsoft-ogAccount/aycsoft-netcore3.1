/*
 * 版 本 Aycsoft-ADMS-Core Aycsoft敏捷开发框架(http://www.aycsoft.cn)
 * Copyright (c) 2013-2018 上海轻 创信息技术有限公司
 * 创建人：轻 创-前端开发组
 * 日 期：2017.03.22
 * 描 述：时间轴方法（降序）
 */
$.fn.lrtimeline = function (nodelist, isFinished) {

    // title   标题
    // people  审核人
    // content 内容
    // time    时间

    var $self = $(this);
    if ($self.length == 0) {
        return $self;
    }
    $self.addClass('lr-timeline');
    var $wrap = $('<div class="lr-timeline-allwrap"></div>');
    var $ul = $('<ul></ul>');

    if (nodelist.length > 0) {
        // 开始节点
        var $begin = $('<li class="lr-timeline-header"><div>当前</div></li>');

        if (isFinished == 1) {
            $ul.append('<li class="lr-timeline-header-finish"><div>流程结束</div></li>');
        }
        else {
            $ul.append($begin);
        }

        

        $.each(nodelist, function (_index, _item) {
            // 中间节点
            var $li = $('<li class="lr-timeline-item" ><div class="lr-timeline-wrap" ></div></li>');
            if (_index == 0) {
                $li.find('div').addClass('lr-timeline-current');
            }
            var $itemwrap = $li.find('.lr-timeline-wrap');
            var $itemcontent = $('<div class="lr-timeline-content"><span class="arrow"></span></div>');
            $itemcontent.append('<div class="lr-timeline-title">' + _item.title + '</div>');

            var peopleHtml = '';
            if (_item.people) {
                var peoples = _item.people.split(',');
                $.each(peoples, function (_index, _userId) {
                    if (_index > 0) {
                        peopleHtml += ',';
                    }
                    peopleHtml += '<span uId ="' + _userId +'" ></span>';
                })
            }
            else {
                peopleHtml = _item.peopleName || '系统处理';
            }

            $itemcontent.append('<div class="lr-timeline-body"><span>' + peopleHtml + '：</span>' + _item.content + '</div>');


            $itemwrap.append('<span class="lr-timeline-date">' + _item.time + '</span>');
            $itemwrap.append($itemcontent);

            var $event = $itemcontent.find('.lr-event');
            if ($event.length > 0) {
                $event[0].lrdata = _item;
                $itemcontent.find('.lr-event').on('click', function () {
                    var data = $(this)[0].lrdata;
                    data.callback && data.callback(data);
                });
            }
          

            $ul.append($li);



        });

        // 结束节点
        $ul.append('<li class="lr-timeline-ender"><div>开始</div></li>');
    }
    
    $wrap.html($ul);
    $self.html($wrap);
    $self.find('[uId]').each(function () {
        var $this = $(this);
        var userId = $this.attr('uId');
        top.aycsoft.clientdata.getAsync('user', {
            key: userId,
            callback: function (user) {
                if (user.F_DepartmentId) {
                    top.aycsoft.clientdata.getAsync('department', {
                        key: user.F_DepartmentId,
                        callback: function (department) {
                            $this.text('【' + department.F_FullName + '】' + user.F_RealName);
                        }
                    });
                }
                else {
                    $this.text(user.F_RealName);
                }
            }
        });
    });
    /**/

};