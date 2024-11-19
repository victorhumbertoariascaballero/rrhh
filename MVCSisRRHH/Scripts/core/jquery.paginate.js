(function ($) {
    $.fn.paginate = function (options) {
        var opts = $.extend({}, $.fn.paginate.defaults, options);
        return this.each(function () {
            $this = $(this);
            var o = $.meta ? $.extend({}, opts, $this.data()) : opts;
            var selectedpage = o.start;
            $.fn.draw(o, $this, selectedpage);
        });
    };
    var anchocuadro = 31;
    var outsidewidth_tmp = 0; 
    var insidewidth = 0;
    var bName = navigator.appName;
    var bVer = navigator.appVersion;
    if (bVer.indexOf('MSIE 7.0') > 0)
        var ver = "ie7";
    $.fn.paginate.defaults = {
        count: 5,
        start: 12,
        display: 5,
        border: false,
        border_color: '#fff',
        text_color: '#8cc59d',
        background_color: 'black',
        border_hover_color: '#fff',
        text_hover_color: '#fff',
        background_hover_color: '#fff',
        rotate: true,
        images: false,
        mouse: 'slide',
        first_text: 'Primero',
        last_text: 'Ultimo',
        nombre_paginador: "paginador",
        onChange: function () { return false; }
    };
    $.fn.draw = function (o, obj, selectedpage) {
        if (o.display > o.count)
            o.display = o.count;
        $this.empty();
        if (o.images) {
            var spreviousclass = 'jPag-sprevious-img';
            var previousclass = 'jPag-previous-img';
            var snextclass = 'jPag-snext-img';
            var nextclass = 'jPag-next-img';
        }
        else {
            var spreviousclass = 'jPag-sprevious';
            var previousclass = 'jPag-previous';
            var snextclass = 'jPag-snext';
            var nextclass = 'jPag-next';
        }



        var _divwrapleft = $(document.createElement('div')).addClass('firstPagination');

        var _ul2 = $(document.createElement('ul')).addClass('pagination pagination-sm')
        var _first = $(document.createElement('li')).html('<a><span>' + o.first_text + '</span></a>');
        var _rotleft = $(document.createElement('li')).addClass(spreviousclass).html('<a><span><<</span></a>');;
        _ul2.append(_first).append(_rotleft);

        _divwrapleft.append(_ul2);

        var _ul = $(document.createElement('ul')).addClass('pagination pagination-sm')
        var _ulwrapdiv = $(document.createElement('div')).addClass("middlePagination");

        var c = (o.display - 1) / 2;
        var first = selectedpage - c;
        var selobj;

        for (var i = 0; i < o.count; i++) {
            var val = i + 1;
            if (val == selectedpage) {
                var _obj = $(document.createElement('li')).addClass('active').html('<a><span>' + val + '</span></a>');
                selobj = _obj;
                _ul.append(_obj);
            }
            else {
                var _obj = $(document.createElement('li')).html('<a><span>' + val + '</span></a>');
                _ul.append(_obj);
            }
        }
        _ulwrapdiv.append(_ul);

        //Lo de la derecha
        var _divwrapright = $(document.createElement('div')).addClass('lastPagination');
        var _ulDerecha = $(document.createElement('ul')).addClass('pagination pagination-sm')
        var _last = $(document.createElement('li')).html('<a><span>' + o.last_text + '</span></a>');
        var _rotright = $(document.createElement('li')).addClass(spreviousclass).html('<a><span>>></span></a>');;
        _ulDerecha.append(_rotright).append(_last);

        _divwrapright.append(_ulDerecha);

        //append all:

        $this.addClass('jPaginate').append(_divwrapleft).append(_ulwrapdiv).append(_divwrapright);//;

        if (!o.border) {
            if (o.background_color == 'none') var a_css = { 'color': o.text_color };
            else var a_css = { 'color': o.text_color, 'background-color': o.background_color };
            if (o.background_hover_color == 'none') var hover_css = { 'color': o.text_hover_color };
            else var hover_css = { 'color': o.text_hover_color, 'background-color': o.background_hover_color };
        }
        else {
            if (o.background_color == 'none') var a_css = { 'color': o.text_color, 'border': '1px solid ' + o.border_color };
            else var a_css = { 'color': o.text_color, 'background-color': o.background_color, 'border': '1px solid ' + o.border_color };
            if (o.background_hover_color == 'none') var hover_css = { 'color': o.text_hover_color, 'border': '1px solid ' + o.border_hover_color };
            else var hover_css = { 'color': o.text_hover_color, 'background-color': o.background_hover_color, 'border': '1px solid ' + o.border_hover_color };
        }

        $.fn.applystyle(o, $this, a_css, hover_css, _first, _ul, _ulwrapdiv, _divwrapright);
        //calculate width of the ones displayed:
        var outsidewidth = outsidewidth_tmp;
        //var outsidewidth = outsidewidth_tmp - _first.parent().width() - 3;


        if (o.rotate) {
            _rotright.hover(
				function () {
				    thumbs_scroll_interval = setInterval(
					function () {
					    var left = _ulwrapdiv.scrollLeft() + 1;
					    _ulwrapdiv.scrollLeft(left);
					},
					.1
				  );
				},
				function () {
				    clearInterval(thumbs_scroll_interval);
				}
			);
            _rotleft.hover(
				function () {
				    thumbs_scroll_interval = setInterval(
					function () {
					    var left = _ulwrapdiv.scrollLeft() - 1;
					    _ulwrapdiv.scrollLeft(left);
					},
					20
				  );
				},
				function () {
				    clearInterval(thumbs_scroll_interval);
				}
			);
            if (o.mouse == 'press') {
                _rotright.mousedown(
					function () {
					    thumbs_mouse_interval = setInterval(
						function () {
						    var left = _ulwrapdiv.scrollLeft() + 5;
						    _ulwrapdiv.scrollLeft(left);
						},
						20
					  );
					}
				).mouseup(
					function () {
					    clearInterval(thumbs_mouse_interval);
					}
				);
                _rotleft.mousedown(
					function () {
					    thumbs_mouse_interval = setInterval(
						function () {
						    var left = _ulwrapdiv.scrollLeft() - 5;
						    _ulwrapdiv.scrollLeft(left);
						},
						20
					  );
					}
				).mouseup(
					function () {
					    clearInterval(thumbs_mouse_interval);
					}
				);
            }
            else {
                _rotleft.click(function (e) {
                    var width = outsidewidth - 10;
                    var left = _ulwrapdiv.scrollLeft() - width;
                    _ulwrapdiv.animate({ scrollLeft: left + 'px' });
                });

                _rotright.click(function (e) {
                    var width = outsidewidth - 10;
                    var left = _ulwrapdiv.scrollLeft() + width;
                    _ulwrapdiv.animate({ scrollLeft: left + 'px' });
                });
            }
        }

        //first and last:
        _first.click(function (e) {
            _ulwrapdiv.animate({ scrollLeft: '0px' });
            _ulwrapdiv.find('li').eq(0).click();
        });
        _last.click(function (e) {
            _ulwrapdiv.animate({ scrollLeft: insidewidth + 'px' });
            _ulwrapdiv.find('li').eq(o.count - 1).click();
        });

        //click a page
        //var selector = "#" + o.nombre_paginador + " .middlePagination li";
        //$(selector).click(function (e) {
        _ulwrapdiv.find("li").click(function (e) {
            //alert(selector);
            $(selobj).removeClass("active");
            var currval = $(this).find('a span').html();
            $(this).addClass("active");
            selobj = $(this);
            $.fn.applystyle(o, $(this).parent().parent().parent(), a_css, hover_css, _first, _ul, _ulwrapdiv, _divwrapright);
            var left = (this.offsetLeft) / 2;
            var left2 = _ulwrapdiv.scrollLeft() + left;
            var tmp = left - (outsidewidth / 2);
            //var cero = 0;
            if (ver == 'ie7') {
                _ulwrapdiv.animate({ scrollLeft: left + tmp - _first.parent().width() + 52 + 'px' });
            }
            else {
                //var selector = "#" + o.nombre_paginador + " .middlePagination";
                //alert(selector);
                //$(selector).animate({ scrollLeft: left + tmp - _first.parent().width() + 'px' });
                _ulwrapdiv.animate({ scrollLeft: left + tmp - _first.parent().width() + 'px' });
            }
            
            o.onChange(currval, o.nombre_paginador);
        });

        var last = _ulwrapdiv.find('li').eq(o.start - 1);
        last.attr('id', 'tmp');
        var cero = 0;
        //Modificado Mario
        var left = 0;
        if (document.getElementById('tmp') != null && document.getElementById('tmp') != undefined) {
            left = document.getElementById('tmp').offsetLeft / 2;
        }
        last.removeAttr('id');
        var tmp = left - (outsidewidth / 2);

        if (ver == 'ie7') {
            _ulwrapdiv.animate({ scrollLeft: ((o.start - 1) * anchocuadro) + 52 + 'px' });
        }
        else {
            _ulwrapdiv.animate({ scrollLeft: ((o.start - 1) * anchocuadro) + 'px' });
        }
    }

    $.fn.applystyle = function (o, obj, a_css, hover_css, _first, _ul, _ulwrapdiv, _divwrapright) {

        insidewidth = 0;
        _ul.css('width', (anchocuadro * o.count) + 1 + 'px'); //hero
        _ulwrapdiv.css("overflow", "hidden");
        _ulwrapdiv.css("width", (anchocuadro * o.display) - 2 + "px");
    }

})(jQuery);