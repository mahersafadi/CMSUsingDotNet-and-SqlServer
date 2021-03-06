var $event = $.event, $special, resizeTimeout;
$special = $event.special.debouncedresize = {setup: function() {
        $(this).on("resize", $special.handler)
    }, teardown: function() {
        $(this).off("resize", $special.handler)
    }, handler: function(e, a) {
        var d = this, c = arguments, b = function() {
            e.type = "debouncedresize";
            $event.dispatch.apply(d, c)
        };
        if (resizeTimeout) {
            clearTimeout(resizeTimeout)
        }
        a ? b() : resizeTimeout = setTimeout(b, $special.threshold)
    }, threshold: 250};
var BLANK = "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///ywAAAAAAQABAAACAUwAOw==";
$.fn.imagesLoaded = function(h) {
    var e = this, j = $.isFunction($.Deferred) ? $.Deferred() : 0, i = $.isFunction(j.notify), b = e.find("img").add(e.filter("img")), c = [], g = [], d = [];
    if ($.isPlainObject(h)) {
        $.each(h, function(k, l) {
            if (k === "callback") {
                h = l
            } else {
                if (j) {
                    j[k](l)
                }
            }
        })
    }
    function f() {
        var k = $(g), l = $(d);
        if (j) {
            if (d.length) {
                j.reject(b, k, l)
            } else {
                j.resolve(b)
            }
        }
        if ($.isFunction(h)) {
            h.call(e, b, k, l)
        }
    }
    function a(k, l) {
        if (k.src === BLANK || $.inArray(k, c) !== -1) {
            return
        }
        c.push(k);
        if (l) {
            d.push(k)
        } else {
            g.push(k)
        }
        $.data(k, "imagesLoaded", {isBroken: l, src: k.src});
        if (i) {
            j.notifyWith($(k), [l, b, $(g), $(d)])
        }
        if (b.length === c.length) {
            setTimeout(f);
            b.unbind(".imagesLoaded")
        }
    }
    if (!b.length) {
        f()
    } else {
        b.bind("load.imagesLoaded error.imagesLoaded", function(k) {
            a(k.target, k.type === "error")
        }).each(function(k, m) {
            var n = m.src;
            var l = $.data(m, "imagesLoaded");
            if (l && l.src === n) {
                a(m, l.isBroken);
                return
            }
            if (m.complete && m.naturalWidth !== undefined) {
                a(m, m.naturalWidth === 0 || m.naturalHeight === 0);
                return
            }
            if (m.readyState || m.complete) {
                m.src = BLANK;
                m.src = n
            }
        })
    }
    return j ? j.promise(e) : e
};
$(function() {
    $.elastic_grid = {version: "1.0"};
    $.fn.elastic_grid = function(G) {
        G = $.extend({}, {items: null, filterEffect: "", hoverDirection: true, hoverDelay: 0, hoverInverse: false, expandingHeight: 500, expandingSpeed: 300, callback: function() {
            }}, G);
        var u = $(this);
        var H = G.items.length;
        if (H == 0) {
            return false
        }
       /* u.html('<div class="wagwep-container"><nav id="porfolio-nav" class="clearfix"><ul id="portfolio-filter" class="nav nav-tabs clearfix"></ul></nav></div>');*/
        var g = "";
        var o = $('<ul id="og-grid" class="og-grid"></ul>');
        for (itemIdx = 0; itemIdx < H; itemIdx++) {
            if (G.items[itemIdx] != undefined) {
                var E = G.items[itemIdx];
                liObject = $("<li></li>");
                tags = E.tags;
                strTag = "";
                for (var C = tags.length - 1; C >= 0; C--) {
                    strTag += "," + tags[C]
                }
                strTag = strTag.substring(1);
                liObject.attr("data-tags", strTag);
                aObject = $("<a></a>");
                aObject.attr("href", "javascript:;;");
                imgObject = $("<img/>");
                imgObject.attr("src", E.thumbnail[0]);
                imgObject.attr("data-largesrc", E.large[0]);
                spanObject = $("<span></span>");
                spanObject.html(E.title);
                figureObject = $("<figure></figure>");
                figureObject.append(spanObject);
                imgObject.appendTo(aObject);
                figureObject.appendTo(aObject);
                aObject.appendTo(liObject);
                liObject.appendTo(o)
            }
        }
        if (G.filterEffect == "") {
            G.filterEffect = "moveup"
        }
        o.addClass("effect-" + G.filterEffect);
        o.appendTo(u);
        if (G.hoverDirection == true) {
            o.find("li").each(function() {
                $(this).hoverdir({hoverDelay: G.hoverDelay, inverse: G.hoverInverse})
            })
        }
        var m = u.find("#portfolio-filter");
        var x = o.find("li"), c = {};
        numOfTag = 0;
        x.each(function(J) {
            var K = $(this), I = K.data("tags").split(",");
            K.attr("data-id", J);
            K.addClass("all");
            $.each(I, function(i, L) {
                L = $.trim(L);
                K.addClass(L.toLowerCase().replace(" ", "-"));
                if (!(L in c)) {
                    c[L] = [];
                    numOfTag++
                }
                c[L].push(K)
            })
        });
        if (numOfTag > 1) {
            f("All");
            $.each(c, function(I, i) {
                f(I)
            })
        } else {
            m.remove()
        }
        m.find("a").bind("click", function(I) {
            k.find("li.og-expanded").find("a").trigger("click");
            k.find(".og-close").trigger("click");
            $this = $(this);
            $this.css("outline", "none");
            m.find(".current").removeClass("current");
            $this.parent().addClass("current");
            var J = $this.text().toLowerCase().replace(" ", "-");
            var i = H;
            o.find("li").each(function(K, L) {
                classie.remove(L, "hidden");
                classie.remove(L, "animate");
                if (!--i) {
                    setTimeout(function() {
                        p(o.find("li"), J)
                    }, 1)
                }
            });
            return false
        });
        function p(i, I) {
            i.each(function(J, K) {
                if (classie.has(K, I)) {
                    classie.toggle(K, "animate");
                    classie.remove(K, "hidden")
                } else {
                    classie.add(K, "hidden");
                    classie.remove(K, "animate")
                }
            })
        }
        m.find("li:first").addClass("current");
        function f(K) {
            var J = K.toLowerCase().replace(" ", "-");
            if (K != "") {
                var i = $("<li>");
                var I = $("<a>", {html: K, "data-filter": "." + J, href: "#", "class": "filter"}).appendTo(i);
                i.appendTo(m)
            }
        }
        var k = o, h = k.children("li"), y = -1, t = -1, F = 0, q = 10, w = $(window), d, A = $("html, body"), B = {WebkitTransition: "webkitTransitionEnd", MozTransition: "transitionend", OTransition: "oTransitionEnd", msTransition: "MSTransitionEnd", transition: "transitionend"}, j = B[Modernizr.prefixed("transition")], s = Modernizr.csstransitions, D = {minHeight: G.expandingHeight, speed: G.expandingSpeed, easing: "ease"};
        function v(i) {
            h = h.add(i);
            i.each(function() {
                var I = $(this);
                I.data({offsetTop: I.offset().top, height: I.height()})
            });
            l(i)
        }
        function r(i) {
            h.each(function() {
                var I = $(this);
                I.data("offsetTop", I.offset().top);
                if (i) {
                    I.data("height", I.height())
                }
            })
        }
        function n() {
            l(h);
            w.on("debouncedresize", function() {
                F = 0;
                t = -1;
                r();
                b();
                var i = $.data(this, "preview");
                if (typeof i != "undefined") {
                    z()
                }
            })
        }
        function l(i) {
            i.on("click", "span.og-close", function() {
                z();
                return false
            }).children("a").on("click", function(J) {
                var I = $(this).parent();
                I.removeClass("animate");
                y === I.index() ? z($(this)) : a(I);
                return false
            })
        }
        function b() {
            d = {width: w.width(), height: w.height()}
        }
        function a(I) {
            z();
            var J = $.data(this, "preview"), i = I.data("offsetTop");
            F = 0;
            if (typeof J != "undefined") {
                if (t !== i) {
                    if (i > t) {
                        F = J.height
                    }
                    z()
                } else {
                    J.update(I);
                    return false
                }
            }
            t = i;
            J = $.data(this, "preview", new e(I));
            J.open()
        }
        function z() {
            h.find(".og-pointer").remove();
            y = -1;
            var i = $.data(this, "preview");
            if (typeof i == "undefined") {
            } else {
                i.close()
            }
            $.removeData(this, "preview")
        }
        function e(i) {
            this.$item = i;
            this.expandedIdx = this.$item.index();
            this.create();
            this.update()
        }
        e.prototype = {create: function() {
                this.$title = $("<h3></h3>");
                this.$description = $("<p></p>");
                this.$href = $('<a href="#">Visit website</a>');
                this.$detailButtonList = $('<span class="buttons-list"></span>');
                this.$details = $('<div class="og-details"></div>').append(this.$title, this.$description, this.$detailButtonList);
                this.$loading = $('<div class="og-loading"></div>');
                this.$fullimage = $('<div class="og-fullimg"></div>').append(this.$loading);
                this.$closePreview = $('<span class="og-close"></span>');
                this.$previewInner = $('<div class="og-expander-inner"></div>').append(this.$closePreview, this.$fullimage, this.$details);
                this.$previewEl = $('<div class="og-expander"></div>').append(this.$previewInner);
                this.$item.append($('<div class="og-pointer"></div>'));
                this.$item.append(this.getEl());
                if (s) {
                    this.setTransition()
                }
            }, update: function(J) {
                if (J) {
                    this.$item = J
                }
                if (y !== -1) {
                    var O = h.eq(y);
                    O.removeClass("og-expanded");
                    this.$item.addClass("og-expanded");
                    this.positionPreview()
                }
                y = this.$item.index();
                if (typeof G.items[y] === "undefined") {
                } else {
                    eldata = G.items[y];
                    this.$title.html(eldata.title);
                    this.$description.html(eldata.description);
                    this.$detailButtonList.html("");
                    urlList = eldata.button_list;
                    if (urlList.length > 0) {
                        for (C = 0; C < urlList.length; C++) {
                            var i = $("<a></a>");
                            i.addClass("link-button");
                            if (C == 0) {
                                i.addClass("first")
                            }
                            i.attr("href", urlList[C]["url"]);
                            i.html(urlList[C]["title"]);
                            this.$detailButtonList.append(i)
                        }
                    }
                    var I = this;
                    if (typeof I.$largeImg != "undefined") {
                        I.$largeImg.remove()
                    }
                    glarge = eldata.large;
                    gthumbs = eldata.thumbnail;
                    if (glarge.length == gthumbs.length && glarge.length > 0) {
                        var L = $("<ul></ul>");
                        for (C = 0; C < gthumbs.length; C++) {
                            var N = $("<li></li>");
                            var i = $('<a href="javascript:;;"></a>');
                            var K = $("<img/>");
                            K.addClass("related_photo");
                            if (C == 0) {
                                K.addClass("selected")
                            }
                            K.attr("src", gthumbs[C]);
                            K.attr("data-large", glarge[C]);
                            i.append(K);
                            N.append(i);
                            L.append(N)
                        }
                        L.addClass("elastislide-list");
                        L.elastislide();
                        var M = $('<div class="elastislide-wrapper elastislide-horizontal"></div>');
                        M.append(L).find(".related_photo").bind("click", function() {
                            M.find(".selected").removeClass("selected");
                            $(this).addClass("selected");
                            $largePhoto = $(this).data("large");
                            $("<img/>").load(function() {
                                I.$fullimage.find("img").fadeOut(500, function() {
                                    $(this).fadeIn(500).attr("src", $largePhoto)
                                })
                            }).attr("src", $largePhoto)
                        });
                        I.$details.append('<div class="infosep"></div>');
                        I.$details.append(M)
                    } else {
                        I.$details.find(".infosep, .og-grid-small").remove()
                    }
                    if (I.$fullimage.is(":visible")) {
                        this.$loading.show();
                        $("<img/>").load(function() {
                            var P = $(this);
                            if (P.attr("src") === I.$item.children("a").find("img").data("largesrc")) {
                                I.$loading.hide();
                                I.$fullimage.find("img").remove();
                                I.$largeImg = P.fadeIn(350);
                                I.$fullimage.append(I.$largeImg)
                            }
                        }).attr("src", eldata.large[0])
                    }
                }
            }, open: function() {
                setTimeout($.proxy(function() {
                    this.setHeights();
                    this.positionPreview()
                }, this), 25)
            }, close: function() {
                var i = this, I = function() {
                    if (s) {
                        $(this).off(j)
                    }
                    i.$item.removeClass("og-expanded");
                    i.$previewEl.remove()
                };
                setTimeout($.proxy(function() {
                    if (typeof this.$largeImg !== "undefined") {
                        this.$largeImg.fadeOut("fast")
                    }
                    this.$previewEl.css("height", 0);
                    var J = h.eq(this.expandedIdx);
                    J.css("height", J.data("height")).on(j, I);
                    if (!s) {
                        I.call()
                    }
                }, this), 25);
                return false
            }, calcHeight: function() {
                var I = d.height - this.$item.data("height") - q, i = d.height;
                if (I < D.minHeight) {
                    I = D.minHeight;
                    i = D.minHeight + this.$item.data("height") + q
                }
                this.height = I;
                this.itemHeight = i
            }, setHeights: function() {
                var i = this, I = function() {
                    if (s) {
                        i.$item.off(j)
                    }
                    i.$item.addClass("og-expanded")
                };
                this.calcHeight();
                this.$previewEl.css("height", this.height);
                this.$item.css("height", this.itemHeight).on(j, I);
                if (!s) {
                    I.call()
                }
            }, positionPreview: function() {
                var I = this.$item.data("offsetTop"), i = this.$previewEl.offset().top - F, J = this.height + this.$item.data("height") + q <= d.height ? I : this.height < d.height ? i - (d.height - this.height) : i;
                A.animate({scrollTop: J}, D.speed)
            }, setTransition: function() {
                this.$previewEl.css("transition", "height " + D.speed + "ms " + D.easing);
                this.$item.css("transition", "height " + D.speed + "ms " + D.easing)
            }, getEl: function() {
                return this.$previewEl
            }};
        k.imagesLoaded(function() {
            r(true);
            b();
            n()
        })
    }
});
(function(c, b, d) {
    c.HoverDir = function(e, f) {
        this.$el = c(f);
        this._init(e)
    };
    c.HoverDir.defaults = {speed: 300, easing: "ease", hoverDelay: 0, inverse: false};
    c.HoverDir.prototype = {_init: function(e) {
            this.options = c.extend(true, {}, c.HoverDir.defaults, e);
            this.transitionProp = "all " + this.options.speed + "ms " + this.options.easing;
            this.support = Modernizr.csstransitions;
            this._loadEvents()
        }, _loadEvents: function() {
            var e = this;
            this.$el.on("mouseenter.hoverdir, mouseleave.hoverdir", function(i) {
                var g = c(this), f = g.find("figure"), j = e._getDir(g, {x: i.pageX, y: i.pageY}), h = e._getStyle(j);
                if (i.type === "mouseenter") {
                    f.hide().css(h.from);
                    clearTimeout(e.tmhover);
                    e.tmhover = setTimeout(function() {
                        f.show(0, function() {
                            var k = c(this);
                            if (e.support) {
                                k.css("transition", e.transitionProp)
                            }
                            e._applyAnimation(k, h.to, e.options.speed)
                        })
                    }, e.options.hoverDelay)
                } else {
                    if (e.support) {
                        f.css("transition", e.transitionProp)
                    }
                    clearTimeout(e.tmhover);
                    e._applyAnimation(f, h.from, e.options.speed)
                }
            })
        }, _getDir: function(g, k) {
            var f = g.width(), i = g.height(), e = (k.x - g.offset().left - (f / 2)) * (f > i ? (i / f) : 1), l = (k.y - g.offset().top - (i / 2)) * (i > f ? (f / i) : 1), j = Math.round((((Math.atan2(l, e) * (180 / Math.PI)) + 180) / 90) + 3) % 4;
            return j
        }, _getStyle: function(k) {
            var g, l, i = {left: "0px", top: "-100%"}, e = {left: "0px", top: "100%"}, h = {left: "-100%", top: "0px"}, f = {left: "100%", top: "0px"}, m = {top: "0px"}, j = {left: "0px"};
            switch (k) {
                case 0:
                    g = !this.options.inverse ? i : e;
                    l = m;
                    break;
                case 1:
                    g = !this.options.inverse ? f : h;
                    l = j;
                    break;
                case 2:
                    g = !this.options.inverse ? e : i;
                    l = m;
                    break;
                case 3:
                    g = !this.options.inverse ? h : f;
                    l = j;
                    break
            }
            return{from: g, to: l}
        }, _applyAnimation: function(f, e, g) {
            c.fn.applyStyle = this.support ? c.fn.css : c.fn.animate;
            f.stop().applyStyle(e, c.extend(true, [], {duration: g + "ms"}))
        }};
    var a = function(e) {
        if (b.console) {
            b.console.error(e)
        }
    };
    c.fn.hoverdir = function(g) {
        var e = c.data(this, "hoverdir");
        if (typeof g === "string") {
            var f = Array.prototype.slice.call(arguments, 1);
            this.each(function() {
                if (!e) {
                    a("cannot call methods on hoverdir prior to initialization; attempted to call method '" + g + "'");
                    return
                }
                if (!c.isFunction(e[g]) || g.charAt(0) === "_") {
                    a("no such method '" + g + "' for hoverdir instance");
                    return
                }
                e[g].apply(e, f)
            })
        } else {
            this.each(function() {
                if (e) {
                    e._init()
                } else {
                    e = c.data(this, "hoverdir", new c.HoverDir(g, this))
                }
            })
        }
        return e
    }
})(jQuery, window);
(function(g, h, e) {
    var a = g.event, b, j;
    b = a.special.debouncedresize = {setup: function() {
            g(this).on("resize", b.handler)
        }, teardown: function() {
            g(this).off("resize", b.handler)
        }, handler: function(o, k) {
            var n = this, m = arguments, l = function() {
                o.type = "debouncedresize";
                a.dispatch.apply(n, m)
            };
            if (j) {
                clearTimeout(j)
            }
            k ? l() : j = setTimeout(l, b.threshold)
        }, threshold: 150};
    var c = "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///ywAAAAAAQABAAACAUwAOw==";
    g.fn.imagesLoaded = function(r) {
        var o = this, t = g.isFunction(g.Deferred) ? g.Deferred() : 0, s = g.isFunction(t.notify), l = o.find("img").add(o.filter("img")), m = [], q = [], n = [];
        if (g.isPlainObject(r)) {
            g.each(r, function(u, v) {
                if (u === "callback") {
                    r = v
                } else {
                    if (t) {
                        t[u](v)
                    }
                }
            })
        }
        function p() {
            var u = g(q), v = g(n);
            if (t) {
                if (n.length) {
                    t.reject(l, u, v)
                } else {
                    t.resolve(l)
                }
            }
            if (g.isFunction(r)) {
                r.call(o, l, u, v)
            }
        }
        function k(u, v) {
            if (u.src === c || g.inArray(u, m) !== -1) {
                return
            }
            m.push(u);
            if (v) {
                n.push(u)
            } else {
                q.push(u)
            }
            g.data(u, "imagesLoaded", {isBroken: v, src: u.src});
            if (s) {
                t.notifyWith(g(u), [v, l, g(q), g(n)])
            }
            if (l.length === m.length) {
                setTimeout(p);
                l.unbind(".imagesLoaded")
            }
        }
        if (!l.length) {
            p()
        } else {
            l.bind("load.imagesLoaded error.imagesLoaded", function(u) {
                k(u.target, u.type === "error")
            }).each(function(u, w) {
                var x = w.src;
                var v = g.data(w, "imagesLoaded");
                if (v && v.src === x) {
                    k(w, v.isBroken);
                    return
                }
                if (w.complete && w.naturalWidth !== e) {
                    k(w, w.naturalWidth === 0 || w.naturalHeight === 0);
                    return
                }
                if (w.readyState || w.complete) {
                    w.src = c;
                    w.src = x
                }
            })
        }
        return t ? t.promise(o) : o
    };
    var d = g(h), f = h.Modernizr;
    g.Elastislide = function(k, l) {
        this.$el = g(l);
        this._init(k)
    };
    g.Elastislide.defaults = {orientation: "horizontal", speed: 500, easing: "ease-in-out", minItems: 3, start: 0, onClick: function(m, k, l) {
            return false
        }, onReady: function() {
            return false
        }, onBeforeSlide: function() {
            return false
        }, onAfterSlide: function() {
            return false
        }};
    g.Elastislide.prototype = {_init: function(l) {
            this.options = g.extend(true, {}, g.Elastislide.defaults, l);
            var k = this, m = {WebkitTransition: "webkitTransitionEnd", MozTransition: "transitionend", OTransition: "oTransitionEnd", msTransition: "MSTransitionEnd", transition: "transitionend"};
            this.transEndEventName = m[f.prefixed("transition")];
            this.support = f.csstransitions && f.csstransforms;
            this.current = this.options.start;
            this.isSliding = false;
            this.$items = this.$el.children("li");
            this.itemsCount = this.$items.length;
            if (this.itemsCount === 0) {
                return false
            }
            this._validate();
            this.$items.detach();
            this.$el.empty();
            this.$el.append(this.$items);
            this.$el.wrap('<div class="elastislide-wrapper elastislide-loading elastislide-' + this.options.orientation + '"></div>');
            this.hasTransition = false;
            this.hasTransitionTimeout = setTimeout(function() {
                k._addTransition()
            }, 100);
            this.$el.imagesLoaded(function() {
                k.$el.show();
                k._layout();
                k._configure();
                if (k.hasTransition) {
                    k._removeTransition();
                    k._slideToItem(k.current);
                    k.$el.on(k.transEndEventName, function() {
                        k.$el.off(k.transEndEventName);
                        k._setWrapperSize();
                        k._addTransition();
                        k._initEvents()
                    })
                } else {
                    clearTimeout(k.hasTransitionTimeout);
                    k._setWrapperSize();
                    k._initEvents();
                    k._slideToItem(k.current);
                    setTimeout(function() {
                        k._addTransition()
                    }, 25)
                }
                k.options.onReady()
            })
        }, _validate: function() {
            if (this.options.speed < 0) {
                this.options.speed = 500
            }
            if (this.options.minItems < 1 || this.options.minItems > this.itemsCount) {
                this.options.minItems = 1
            }
            if (this.options.start < 0 || this.options.start > this.itemsCount - 1) {
                this.options.start = 0
            }
            if (this.options.orientation != "horizontal" && this.options.orientation != "vertical") {
                this.options.orientation = "horizontal"
            }
        }, _layout: function() {
            this.$el.wrap('<div class="elastislide-carousel"></div>');
            this.$carousel = this.$el.parent();
            this.$wrapper = this.$carousel.parent().removeClass("elastislide-loading");
            var k = this.$items.find("img:first");
            this.imgSize = {width: k.outerWidth(true), height: k.outerHeight(true)};
            this._setItemsSize();
            this.options.orientation === "horizontal" ? this.$el.css("max-height", this.imgSize.height) : this.$el.css("height", this.options.minItems * this.imgSize.height);
            this._addControls()
        }, _addTransition: function() {
            if (this.support) {
                this.$el.css("transition", "all " + this.options.speed + "ms " + this.options.easing)
            }
            this.hasTransition = true
        }, _removeTransition: function() {
            if (this.support) {
                this.$el.css("transition", "all 0s")
            }
            this.hasTransition = false
        }, _addControls: function() {
            var k = this;
            this.$navigation = g('<nav><span class="elastislide-prev">Previous</span><span class="elastislide-next">Next</span></nav>').appendTo(this.$wrapper);
            this.$navPrev = this.$navigation.find("span.elastislide-prev").on("mousedown.elastislide", function(l) {
                k._slide("prev");
                return false
            });
            this.$navNext = this.$navigation.find("span.elastislide-next").on("mousedown.elastislide", function(l) {
                k._slide("next");
                return false
            })
        }, _setItemsSize: function() {
            var k = this.options.orientation === "horizontal" ? (Math.floor(this.$carousel.width() / this.options.minItems) * 100) / this.$carousel.width() : 100;
            this.$items.css({width: k + "%", "max-width": this.imgSize.width, "max-height": this.imgSize.height});
            if (this.options.orientation === "vertical") {
                this.$wrapper.css("max-width", this.imgSize.width + parseInt(this.$wrapper.css("padding-left")) + parseInt(this.$wrapper.css("padding-right")))
            }
        }, _setWrapperSize: function() {
            if (this.options.orientation === "vertical") {
                this.$wrapper.css({height: this.options.minItems * this.imgSize.height + parseInt(this.$wrapper.css("padding-top")) + parseInt(this.$wrapper.css("padding-bottom"))})
            }
        }, _configure: function() {
            this.fitCount = this.options.orientation === "horizontal" ? this.$carousel.width() < this.options.minItems * this.imgSize.width ? this.options.minItems : Math.floor(this.$carousel.width() / this.imgSize.width) : this.$carousel.height() < this.options.minItems * this.imgSize.height ? this.options.minItems : Math.floor(this.$carousel.height() / this.imgSize.height)
        }, _initEvents: function() {
            var k = this;
            d.on("debouncedresize.elastislide", function() {
                k._setItemsSize();
                k._configure();
                k._slideToItem(k.current)
            });
            this.$el.on(this.transEndEventName, function() {
                k._onEndTransition()
            });
            if (this.options.orientation === "horizontal") {
                this.$el.on({swipeleft: function() {
                        k._slide("next")
                    }, swiperight: function() {
                        k._slide("prev")
                    }})
            } else {
                this.$el.on({swipeup: function() {
                        k._slide("next")
                    }, swipedown: function() {
                        k._slide("prev")
                    }})
            }
            this.$el.on("click.elastislide", "li", function(m) {
                var l = g(this);
                k.options.onClick(l, l.index(), m)
            })
        }, _destroy: function(k) {
            this.$el.off(this.transEndEventName).off("swipeleft swiperight swipeup swipedown .elastislide");
            d.off(".elastislide");
            this.$el.css({"max-height": "none", transition: "none"}).unwrap(this.$carousel).unwrap(this.$wrapper);
            this.$items.css({width: "auto", "max-width": "none", "max-height": "none"});
            this.$navigation.remove();
            this.$wrapper.remove();
            if (k) {
                k.call()
            }
        }, _toggleControls: function(k, l) {
            if (l) {
                (k === "next") ? this.$navNext.show() : this.$navPrev.show()
            } else {
                (k === "next") ? this.$navNext.hide() : this.$navPrev.hide()
            }
        }, _slide: function(l, n) {
            if (this.isSliding) {
                return false
            }
            this.options.onBeforeSlide();
            this.isSliding = true;
            var t = this, k = this.translation || 0, q = this.options.orientation === "horizontal" ? this.$items.outerWidth(true) : this.$items.outerHeight(true), o = this.itemsCount * q, m = this.options.orientation === "horizontal" ? this.$carousel.width() : this.$carousel.height();
            if (n === e) {
                var p = this.fitCount * q;
                if (p < 0) {
                    return false
                }
                if (l === "next" && o - (Math.abs(k) + p) < m) {
                    p = o - (Math.abs(k) + m);
                    this._toggleControls("next", false);
                    this._toggleControls("prev", true)
                } else {
                    if (l === "prev" && Math.abs(k) - p < 0) {
                        p = Math.abs(k);
                        this._toggleControls("next", true);
                        this._toggleControls("prev", false)
                    } else {
                        var s = l === "next" ? Math.abs(k) + Math.abs(p) : Math.abs(k) - Math.abs(p);
                        s > 0 ? this._toggleControls("prev", true) : this._toggleControls("prev", false);
                        s < o - m ? this._toggleControls("next", true) : this._toggleControls("next", false)
                    }
                }
                n = l === "next" ? k - p : k + p
            } else {
                var p = Math.abs(n);
                if (Math.max(o, m) - p < m) {
                    n = -(Math.max(o, m) - m)
                }
                p > 0 ? this._toggleControls("prev", true) : this._toggleControls("prev", false);
                Math.max(o, m) - m > p ? this._toggleControls("next", true) : this._toggleControls("next", false)
            }
            this.translation = n;
            if (k === n) {
                this._onEndTransition();
                return false
            }
            if (this.support) {
                this.options.orientation === "horizontal" ? this.$el.css("transform", "translateX(" + n + "px)") : this.$el.css("transform", "translateY(" + n + "px)")
            } else {
                g.fn.applyStyle = this.hasTransition ? g.fn.animate : g.fn.css;
                var r = this.options.orientation === "horizontal" ? {left: n} : {top: n};
                this.$el.stop().applyStyle(r, g.extend(true, [], {duration: this.options.speed, complete: function() {
                        t._onEndTransition()
                    }}))
            }
            if (!this.hasTransition) {
                this._onEndTransition()
            }
        }, _onEndTransition: function() {
            this.isSliding = false;
            this.options.onAfterSlide()
        }, _slideTo: function(o) {
            var o = o || this.current, n = Math.abs(this.translation) || 0, m = this.options.orientation === "horizontal" ? this.$items.outerWidth(true) : this.$items.outerHeight(true), l = n + this.$carousel.width(), k = Math.abs(o * m);
            if (k + m > l || k < n) {
                this._slideToItem(o)
            }
        }, _slideToItem: function(l) {
            var k = this.options.orientation === "horizontal" ? l * this.$items.outerWidth(true) : l * this.$items.outerHeight(true);
            this._slide("", -k)
        }, add: function(n) {
            var k = this, m = this.current, l = this.$items.eq(this.current);
            this.$items = this.$el.children("li");
            this.itemsCount = this.$items.length;
            this.current = l.index();
            this._setItemsSize();
            this._configure();
            this._removeTransition();
            m < this.current ? this._slideToItem(this.current) : this._slide("next", this.translation);
            setTimeout(function() {
                k._addTransition()
            }, 25);
            if (n) {
                n.call()
            }
        }, setCurrent: function(k, l) {
            this.current = k;
            this._slideTo();
            if (l) {
                l.call()
            }
        }, next: function() {
            self._slide("next")
        }, previous: function() {
            self._slide("prev")
        }, slideStart: function() {
            this._slideTo(0)
        }, slideEnd: function() {
            this._slideTo(this.itemsCount - 1)
        }, destroy: function(k) {
            this._destroy(k)
        }};
    var i = function(k) {
        if (h.console) {
            h.console.error(k)
        }
    };
    g.fn.elastislide = function(m) {
        var k = g.data(this, "elastislide");
        if (typeof m === "string") {
            var l = Array.prototype.slice.call(arguments, 1);
            this.each(function() {
                if (!k) {
                    i("cannot call methods on elastislide prior to initialization; attempted to call method '" + m + "'");
                    return
                }
                if (!g.isFunction(k[m]) || m.charAt(0) === "_") {
                    i("no such method '" + m + "' for elastislide self");
                    return
                }
                k[m].apply(k, l)
            })
        } else {
            this.each(function() {
                if (k) {
                    k._init()
                } else {
                    k = g.data(this, "elastislide", new g.Elastislide(m, this))
                }
            })
        }
        return k
    }
})(jQuery, window);