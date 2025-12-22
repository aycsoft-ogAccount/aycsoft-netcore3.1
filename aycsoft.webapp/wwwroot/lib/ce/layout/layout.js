/**
 * 布局插件，采用 左 右 上 下 中 5块方式布局；中是必须有的
 * 默认 布局区域为自适应父级高和宽
 * resizable 是否可以改变尺寸 默认 false
 * height    设置上下区域块高度 默认 80
 * width     设置左右区域块宽度 默认 180
 */
(function($){
    "use strict"

    var _layout = {
        top:function($this,$mid,$div,op){
            op.height &&  $div.css({'height':op.height})
            var h = $div.outerHeight()
            if(op.resizable){
                $this.addClass('ce-layout-resizable-top')
                h += 4
                $div.append('<div class="ce-layout-move ce-layout-move-top" ce-type="top"  ></div>')
            }
            $this.css({'padding-top':h}).addClass('ce-layout-hasTop')

            // 添加标题
            op.title && $div.append('<div class="ce-layout-title" >'+ op.title +'</div>')
        },
        bottom:function($this,$mid,$div,op){
            op.height &&  $div.css({'height':op.height})
            var h = $div.outerHeight()
            if(op.resizable){
                $this.addClass('ce-layout-resizable-bottom')
                h += 4
                $div.append('<div class="ce-layout-move ce-layout-move-bottom" ce-type="bottom" ></div>')
            }

            $this.css({'padding-bottom':h}).addClass('ce-layout-hasBottom')

            // 添加标题
            op.title && $div.append('<div class="ce-layout-title" >'+ op.title +'</div>')
        },
        left:function($this,$mid,$div,op){
            op.width &&  $div.css({'width':op.width})
            var w = $div.outerWidth()
            if(op.resizable){
                $this.addClass('ce-layout-resizable-left')
                w += 4
                $div.append('<div class="ce-layout-move ce-layout-move-left" ce-type="left" ></div>')
            }
            $this.addClass('ce-layout-hasLeft')
            $mid.css({'padding-left':w})
            $mid.append($div)

            // 添加标题
            op.title && $div.append('<div class="ce-layout-title" >'+ op.title +'</div>')
        },
        right:function($this,$mid,$div,op){
            op.width &&  $div.css({'width':op.width})
            var w = $div.outerWidth()
            if(op.resizable){
                $this.addClass('ce-layout-resizable-right')
                w += 4
                $div.append('<div class="ce-layout-move ce-layout-move-right" ce-type="right" ></div>')
            }
            $this.addClass('ce-layout-hasRight')
            $mid.css({'padding-right':w})
            $mid.append($div)

            // 添加标题
            op.title && $div.append('<div class="ce-layout-title" >'+ op.title +'</div>')
        },
        center:function($this,$mid,$div,op){
            $mid.append($div)

            // 添加标题
            op.title && $div.append('<div class="ce-layout-title" >'+ op.title +'</div>')
        }
    }
    var _getHW = function(max,hw){
        if(hw<10){
            hw = 10
        }
        if(hw>max){
            hw = max
        }

        return hw
    }

    var _layoutMoving = {
        top:function($this,op,e){
            var h = op._size + (e.pageY - op._pageY)
            h = _getHW(op._maxsize,h)
            $this.children('.ce-layout-top').css('height',h)
            $this.css('padding-top', h + 4)
        },
        bottom:function($this,op,e){
            var h = op._size - (e.pageY - op._pageY)
            h = _getHW(op._maxsize,h)
            $this.children('.ce-layout-bottom').css('height', h)
            $this.css('padding-bottom',h+ 4)
        },
        left:function($this,op,e){
            var w = op._size + (e.pageX - op._pageX)
            w = _getHW(op._maxsize,w)
            $this.children('.ce-layout-mid').children('.ce-layout-left').css('width',w )
            $this.children('.ce-layout-mid').css('padding-left',w+ 4)
        },
        right:function($this,op,e){
            var w = op._size - (e.pageX - op._pageX)
            w = _getHW(op._maxsize,w)
            $this.children('.ce-layout-mid').children('.ce-layout-right').css('width', w)
            $this.children('.ce-layout-mid').css('padding-right', w+ 4)
        }
    }

    $.fn.celayout = function(op){
        var $this = $(this)
        op = op || {}
        $this.addClass('ce-layout-box').addClass('ce-layout')
        var $mid = $('<div class="ce-layout-box ce-layout-mid" ></div>')
        var hasCenter = false
        $this.children().each(function(){
            var $div = $(this)
            var type = $div.attr('data-type')
            $div.addClass('ce-layout-box').addClass('ce-layout-' + type)
            if(type == 'center'){
                hasCenter = true
            }
            if(_layout[type]){
                op[type] =  $.extend({
                    resizable:false // 支持 上下左右 4个模块
                }, op[type] || {})
                _layout[type]($this,$mid,$div,op[type])
            }
            else{
                console.warn('区域类型【'+ type+'】不支持!')
            }
        })
        $this.append($mid)
        if(!hasCenter){
            console.warn('没有中间区域模块，请添加!')
        }

        $this[0]._op = op

        // 绑定事件
        $this.on('mousedown', function (e) {
            e = e || window.event
            var et = e.target || e.srcElement
            var $et = $(et)
            var $layout = $(this)
            var op = $layout[0]._op
            if ($et.hasClass('ce-layout-move')) {
                var type = $et.attr('ce-type')
                op._moveType = type
                op._ismove = true
                op._pageX = e.pageX
                op._pageY = e.pageY
                if(type == 'top' || type == 'bottom'){
                    op._size = $et.parent().outerHeight()
                    op._maxsize =op._size + $this.children('.ce-layout-mid').children('.ce-layout-center').outerHeight()
                }
                else{
                    op._size = $et.parent().outerWidth()
                    op._maxsize =op._size + $this.children('.ce-layout-mid').children('.ce-layout-center').outerWidth()
                }
            }
        })

        $this.mousemove(function (e) {
            e = e || window.event
            var $this = $(this);
            var op = $this[0]._op
            if (op._ismove) {
                _layoutMoving[op._moveType] && _layoutMoving[op._moveType]($this,op,e)
            }
        })

        $this.on('click', function (e) {
            e = e || window.event
            var $this = $(this)
            var op = $this[0]._op
            if (op._ismove) {
                op._ismove = false
            }
        })

    }

    $.fn.celayoutSet = function(){
    }
})(jQuery);
