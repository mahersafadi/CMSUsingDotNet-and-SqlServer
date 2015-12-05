var debug = true;
function alertMsg(msg, type) {
    if (debug == true) {
        if (type == null || type == '' || type == undefined)
            type = 'info';
        alert(type + ':' + msg);
    }
}



$(document).ready(function () {
//    $().UItoTop({ easingType: 'easeOutQuart' });

//    var opts = {
//        lines: 13, // The number of lines to draw
//        length: 20, // The length of each line
//        width: 10, // The line thickness
//        radius: 30, // The radius of the inner circle
//        corners: 1, // Corner roundness (0..1)
//        rotate: 0, // The rotation offset
//        direction: 1, // 1: clockwise, -1: counterclockwise
//        color: '#000', // #rgb or #rrggbb or array of colors
//        speed: 1, // Rounds per second
//        trail: 60, // Afterglow percentage
//        shadow: false, // Whether to render a shadow
//        hwaccel: false, // Whether to use hardware acceleration
//        className: 'spinner', // The CSS class to assign to the spinner
//        zIndex: 2e9, // The z-index (defaults to 2000000000)
//        top: 'auto', // Top position relative to parent in px
//        left: 'auto' // Left position relative to parent in px
//    };
//    var target = document.getElementById('foo');
//    var spinner = new Spinner(opts).spin(target);





});



function executeFunctionByName(functionName, context, argsArray) {
    try {
        var args = Array.prototype.slice.call(arguments).splice(2);
        var namespaces = functionName.split(".");
        var func = namespaces.pop();
        for (var i = 0; i < namespaces.length; i++) {
            context = context[namespaces[i]];
        }
        //Slide_Show.generate();
        return context[func].apply(this, args);
    }
    catch (e) {
        alertMsg(e.message, 'executeFunctionByName');
    }
}

var Generator = {
    init: function () {
        try {
            //Order by priorities

            var regions = document.getElementById('regions');
            regions = regions.value;
            var regionsPriorities = document.getElementById('regionsPriorities');
            regionsPriorities = regionsPriorities.value;
            regionsPriorities = regionsPriorities.split(",");
            regions = regions.split(",");
            var arr = [];
            for (var i = 0; i < regions.length; i++) {
                var index = i;
                for (var j = i; j < regions.length; j++) {
                    if (regionsPriorities[j] < regionsPriorities[index])
                        index = j;
                }
                var temp = regionsPriorities[index];
                regionsPriorities[index] = regionsPriorities[i];
                regionsPriorities[i] = temp;

                var temp = regions[index];
                regions[index] = regions[i];
                regions[i] = temp;
            }
            //==============================================
            //Strart sending
            for (var i = 0; i < regions.length; i++) {

                var url = 'GeneratorHandler.ashx?rid=' + regions[i];
                var callBack = 'Generator.continureInit';
                var args = new Array();
                args["id"] = regions[i];
                Ajax.send(url, 'get', null, callBack, args);
            }
            //==============================================

        }
        catch (e) {
            alertMsg(e.message, 'generator, init');
        }
    },
    continureInit: function (args/*response, itemId*/) {
        var response = args["response"];
        var rr = response.split("@@");
        response = rr[0];
        var regionId = args["id"];
        if (response != null) {
            var content = document.getElementById(regionId);
            content.innerHTML = response;
            if(content.id=="region_headermiddle")
                setTimeout("Slide_Show.generate()",400);
            if(content.id== "r1_right_1")
                $('#latest_news').vTicker();
            
            if(content.id=="r1_left_2")
              {     
//                $('#da-slider').cslider({
//                    autoplay: true,
//                    bgincrement: 450
//                });

$("#owl-example").owlCarousel({
    items : 1,
    //Pagination
    pagination : true,
    paginationNumbers: false,
	navigation: false,
	scrollPerPage: true,
	mouseDrag: true,
    autoPlay: true
	});

             }


        }
    }
};


var Ajax = {
    get: function () {
        try {
            return new XMLHttpRequest();
        }
        catch (e) {
            try {
                return new ActiveXObject("Msxml2.XMLHTTP");
            }
            catch (ee) {
                try {
                    return new ActiveXObject("Msxml3.XMLHTTP");
                }
                catch (eee) {
                    return new ActiveXObject("Microsoft.XMLHTTP");
                }
            }
        }
        return null;
    },
    send: function (url, method, content, callback, argsArray) {
        try {
            if (argsArray == null || argsArray == undefined)
                argsArray = new Array();
            var req = Ajax.get();
            if (req != null) {
                if (method == null || method == '' || method == undefined)
                    method = 'post';

                var rootURL = window.location.href.toString().split(window.location.host)[1];
                
                rootURL = rootURL.substring(1);
                rootURL = rootURL.substring(0, rootURL.indexOf('/'));
                if (rootURL != null && rootURL.length > 0 && rootURL != '') {
                    url = '/' + rootURL + '/' + url;
                }
                
                req.open(method, url, true);
                req.setRequestHeader("Content-type", "application/x-www-form-urlencoded;charset=utf-8");
                req.onreadystatechange = function () {
                    if (req.readyState == 4) {
                        argsArray["response"] = req.responseText;
                        executeFunctionByName(callback, window, argsArray);
                        
                    }
                };

                if (method == 'get')
                    req.send();
                else
                    req.send(content);
            }
        }
        catch (e) {
            alertMsg(e.message, 'ajax,send');
        }
    }
};




// Generate the administration menu
//type = 5
//region id = ra1
var Menu = {
    generate: function () {
        $.get("GeneratorHandler.ashx", { type: "5", rid: "ra1" }, function (data) {
            ra1.innerHTML = data;
        });
    },

    get_content: function (event) {
        $.get("GeneratorHandler.ashx", { type: "5", m_name: event.id }, function (data) {
            ra2.innerHTML = data;
            if(event.id=="menu_security")
                kmrSimpleTabs.init();
        });
    },

    change_bg_color: function (event, color) {
        event.style.backgroundColor = color;
    },

    change_color: function (event, color) {
        event.style.color = color;
    },
    show_lis: function (id) {
        document.getElementById(id).style.display = "";
    },
    hide_lis: function (id) {
        document.getElementById(id).style.display = "none";
    }
};


var Lang = {
    generate: function () {
        $.get("GeneratorHandler.ashx", { __module_name: "Language", mode: "1" }, function (data) {
            ra2.innerHTML = data;
        });
    },


    save_lang: function () {
        $.get("GeneratorHandler.ashx", { __module_name: "Language", mode: "2", code: lang_code.value, name: lang_name.value, dir: lang_dir.value }, function (data) {
            alert("Data " + data);
            Lang.generate();
        });
    },

    delete_lang: function (event) {
        $.get("GeneratorHandler.ashx", { __module_name: "Language", mode: "4", code: event.id }, function (data) {
            alert("Data " + data);
            Lang.generate();
        });
    },



    change_lang: function (event) {
        $.get("GeneratorHandler.ashx", { __module_name: "Language", mode: "3", code: event.value }, function (data) {
            if (data == "rtl") {
                $('#style_file[rel=stylesheet]').attr('href', 'web/css/style.css');
                //$('#tiles-min-slide-show[rel=stylesheet]').attr('href', 'web/slider/css/jquery.tiles.min-ar.css');

            }
            else {
                $('#style_file[rel=stylesheet]').attr('href', 'web/css/style-en.css');
                //$('#tiles-min-slide-show[rel=stylesheet]').attr('href', 'web/slider/css/jquery.tiles.min.css');
            }

            //Lang.generate();
            Generator.init();
        });
    }
};


var LangDetail = {
    generate: function () {
        $.get("GeneratorHandler.ashx", { __module_name: "LangDetail", mode: "1" }, function (data) {
            ra2.innerHTML = data;
        });
    },

    save_lang_detail: function () {
        $.get("GeneratorHandler.ashx", { __module_name: "LangDetail", mode: "2", key: lang_key.value, val: lang_value.value, lang: lang_lan.value }, function (data) {
            alert("Data " + data);
            LangDetail.generate();
        });
    },

    delete_lang_detail: function (event) {
        $.get("GeneratorHandler.ashx", { __module_name: "LangDetail", mode: "3", ___id: event.id }, function (data) {
            alert("Data " + data);
            LangDetail.generate();
        });

    }
};


//=====================================================================================
var Category = {
    generate: function () {
        $.get("GeneratorHandler.ashx", { __module_name: "Category", mode: "1" }, function (data) {
            ra2.innerHTML = data;
        });
    },

    save_cat: function () {
        $.get("GeneratorHandler.ashx", { __module_name: "Category", mode: "2", name: cat_name.value, sub_cat: sub.value }, function (data) {
            alert(data);
            Category.generate();
        });
    },


    delete_cat: function (event) {
        $.get("GeneratorHandler.ashx", { __module_name: "Category", mode: "3", __id: event.id }, function (data) {
            alert("Data " + data);
            Category.generate();
        });
    },

    get_parent: function () {
        $.get("GeneratorHandler.ashx", { __module_name: "Category", mode: "4", name: cat_name.value, sub_cat: sub.value }, function (data) {
            alert(data);
            Category.generate();
        });
    },

    show_category: function (id) {
        //document.getElementById("fountainG").style.display = "";
        //$("html, body").animate({ scrollTop: 400 }, "slow");
        $.get("GeneratorHandler.ashx", { __module_name: "Category", mode: "5", __id: id }, function (data) {
            var arr = data.split('###');
            document.getElementById("main_content").style.display="block";
            document.getElementById("main_content").innerHTML = "<div id='toto'>" + arr[0] + "</div><div style='background:#eeeeee; height:30px; vertical-align:middle; text-align: center;' id='s_more'>" + arr[1] + "</div></br>";
            //Category.scrollTo(400);
            Content.xx();
            //document.getElementById("fountainG").style.display = "none";
        });
    },

    scrollTo: function(to){
        $('html, body').animate({scrollTop: to}, 1000);
    },

    show_more: function (id, from_id) {
        
        //document.getElementById("fountainG").style.display = "";
        $.get("GeneratorHandler.ashx", { __module_name: "Category", mode: "6", __id: id, from_id: from_id }, function (data) {


            if (data.toString() == "[object XMLDocument]") {
                //document.getElementById("fountainG").style.display = "none";
                document.getElementById('s_more').innerHTML = '';
            }

            var arr = data.split('###');

            if (from_id != "0")
                document.getElementById("toto").innerHTML += arr[0];
            else
                document.getElementById("toto").innerHTML = arr[0];


            if (arr[0] == '' || arr[0] == null)
                document.getElementById('s_more').innerHTML = '';
            else
                document.getElementById('s_more').innerHTML = arr[1];


            document.getElementById("fountainG").style.display = "none";
        });
    }

};



//===========================================================================



var Content = {
    generate: function () {
        $.get("GeneratorHandler.ashx", { __module_name: "Content", mode: "1" }, function (data) {
            ra2.innerHTML = data;
        });
    },

    show_search_content: function () {
        //div_content.style.display = 'block';
        div_content.innerHTML= "";
        $.get("GeneratorHandler.ashx", { __module_name: "Content", mode: "2" }, function (data) {
            div_content_search.innerHTML = data;
        });
    },

    do_search_content: function (event) {
        $.get("GeneratorHandler.ashx", { __module_name: "Content", mode: "3", cat_id: all_categories.value, content_title: content_title.value }, function (data) {
            div_content.innerHTML = data;
        });
    },
    new_content: function (event) {
        $.get("GeneratorHandler.ashx", { __module_name: "Content", mode: "3" }, function (data) {
            div_content.innerHTML = data;
        });
    },

    save_new_content: function (evt) {
        $.get("GeneratorHandler.ashx", { __module_name: "Content", mode: "4", cat_id: all_categories.value }, function (data) {
        });
    },

    cancel_content: function (evn) {
        document.getElementById("div_content").style.display = 'none';
    },

    open_new_content: function (event) {
        if (event.id == "")
        //window.open('content/Content.aspx', '_blank', 'width=1200,height=700,titlebar=no, toolbar=no,scrollbars=yes,location=no,resizable =no');
            //window.open('content/Content.aspx', '_blank');
            {
                div_content_search.innerHTML= "";
                $("#div_content").html('<object style="width:100%; height:600px" data="content/Content.aspx">');
            }
        else
        //window.open('content/Content.aspx?__id=' + event.id, '_blank', 'width=1200,height=700,titlebar=no, toolbar=no,scrollbars=yes,location=no,resizable =no');
            //window.open('content/Content.aspx?__id=' + event.id, '_blank');
            {
                div_content_search.innerHTML= "";
                $("#div_content").html('<object style="width:100%; height:600px" data="content/Content.aspx?__id='+event.id+'">');
            }
    },

    th_mouseover: function (event) {
        event.style.backgroundColor = '#cccccc';
        event.style.cursor = 'pointer';
    },

    th_mouseout: function (event) {
        event.style.backgroundColor = '#eeeeee';
    },

    show_content: function (id, from_id) {
        
        
    if($(this).attr("target") == "_blank"){
        alert("ddddd");
        e.preventDefault();
    }

        //document.getElementById("fountainG").style.display = "";
        $.get("GeneratorHandler.ashx", { __module_name: "Content", mode: "5", __cid: id, from_id: from_id }, function (data) {
            //document.getElementById("main_content").innerHTML ="<div id='div_back' onclick='Content.yy()' />";
            document.getElementById("main_content").style.display="block";
            //document.getElementById("main_content").innerHTML ="<a class='button1' style='width:100px; height:25px;' href='#' onclick='javascript:Content.yy(); return false;'></a>";
            document.getElementById("main_content").innerHTML ="<div class='home' onclick='javascript:Content.yy(); return false;'></div>";

//            var home = "<div  class='ih-item circle effect16 left_to_right'><a href='#' onclick='javascript:Content.yy(); return false;'>";
//                home +="<div class='img'><img src='images/icons/home.png' /></div>";
//                home +="<div class='info'>";
//                home +="<div class='info-back'>";
//                home +="<h3>Heading here</h3>";
//                home +="<p>Description goes here</p>";
//                home +="</div>";
//                home +="</div></a></div>";
//            document.getElementById("main_content").innerHTML =home;
            document.getElementById("main_content").innerHTML += data;
            
            t2.style.display="table-row";
            
            Content.xx();
            
        });
    },


    xx: function(){
        t1.style.display ="none";
        $('#t2').fadeOut(1400);
        setTimeout("Content.xxx()", 500);
    },

    xxx: function(){
        $('#t2').height(600);
        setTimeout("Content.xxxx()", 890);
        $('#t1').fadeIn(3000);
        Category.scrollTo(400);
    },

    xxxx: function(){
        $('#t1').fadeIn(3000);
            //Content.xx2();         
            //Category.scrollTo(400);
    },

    yy: function(){
        t2.style.display ="none";
        $('#t1').fadeOut(500);
        setTimeout("Content.yyy()", 400);
    },

    yyy: function(){
        //$('#t2').height(600);
        //setTimeout("Content.yyyy()", 890);
        $('#t2').fadeIn(1000);
        Category.scrollTo(400);
        
    },

    yyyy: function(){
        $('#t2').fadeIn(3000);
            //Content.xx2();         
            //Category.scrollTo(400);
    },

    xx2: function(){
        try{
            if($('#t2').height() >= 0){
                $('#t2').height($('#t2').height()-1);
                setTimeout('Content.xx2()', 10);          
            }
            else{
                alert($('#t1').height());
                if($('#t1').height() < 500){
                    $('#t1').height(500);
                }
            }
         }
         catch(e){
            alert(e.message);
         }
    },

    save_content_form_result: function () {
        var tbl_form = Module.get_table_data("tbl_content_form");
        $.get("GeneratorHandler.ashx", { __module_name: "Content", mode: "6", form_data: tbl_form, __cid: content_id.value }, function (data) {
            //            document.getElementById("region_content_middle_1").innerHTML = data;
            //            document.getElementById("fountainG").style.display = "none";
            
            if(data == "Done")
                Content.show_content(content_id.value, "0");
            else
            alert(data);
        });

        //        $.get("GeneratorHandler.ashx", { __module_name: "Module", mode: "4", m_name: module_name.value, m_desc: module_description.value, m_region: module_region.value, cat: list_categories.value, m_type: module_types.value, md_data: module_details_data }, function (data) {
        //            alert("Data: " + data);
        //            div_module.innerHTML = "";
        //            Module.do_search_module(null);
        //        });
    },

    delete_content_image: function (id) {
        $.get("../GeneratorHandler.ashx", { __module_name: "Content", mode: "7", image_id: id }, function (data) {
            if (data == "1")
                document.getElementById("tbl_" + id).style.display = "none";
            else
                alert(data);
        });
    },

    delete_content: function (id) {
        if (confirm("Are you sure?")) {
            $.get("GeneratorHandler.ashx", { __module_name: "Content", mode: "8", __cid: id }, function (data) {
                alert(data);
                document.getElementById("content_div_" + id).style.display = "none";
            });
        }
    },

    test: function(){
    div_content_search.innerHTML= "";
         $("#div_content").html('<object style="width:100%; height:600px" data="content/Content.aspx">');
    }
};



//*************************************************************
var Module = {
    generate: function () {
        $.get("GeneratorHandler.ashx", { __module_name: "Module", mode: "1" }, function (data) {
            ra2.innerHTML = data;
        });
    },

    new_module: function (event) {
        $.get("GeneratorHandler.ashx", { __module_name: "Module", mode: "2" }, function (data) {

            div_module_search_result.innerHTML = "";
            div_module.innerHTML = data;
            //var myColor = new jscolor.color("module_description")
            jscolor.init();

        });
    },

    save_module: function (event) {

        var module_details_data = Module.get_table_data("tbl_module_details");


        //        $.ajax({
        //            type: "POST",
        //            url: "GeneratorHandler.ashx",
        //            data: str,
        //            contentType: "application/json; charset=utf-8",
        //            dataType: "json",
        //            success: function (msg) {
        //            }
        //        });
        //alert(module_details_data);
        $.get("GeneratorHandler.ashx", { __module_name: "Module", mode: "4", m_name: module_name.value, m_desc: module_description.value, m_region: module_region.value, cat: list_categories.value, m_type: module_types.value, md_data: module_details_data }, function (data) {
            //arr = data.split('###');
            //document.getElementById("module_id").value = arr[1];
            //alert(arr[1]);
            alert("Data: " + data);
            div_module.innerHTML = "";
            Module.do_search_module(null);
        });
    },

    get_table_data: function (tabl_id) {
        var data = new Array();
        var str = "";
        var table = document.getElementById(tabl_id);
        var input = table.getElementsByTagName('input');
        var select = table.getElementsByTagName('select');
        var textarea = table.getElementsByTagName('textarea');

        for (var z = 0; z < input.length; z++) {
            if (input[z].type === 'checkbox')
                if (input[z].checked)
                    data[input[z].id] = "true";
                else
                    data[input[z].id] = "false";
            else
                data[input[z].id] = input[z].value;
        }

        for (var z = 0; z < select.length; z++) {
            data[select[z].id] = select[z].value;
        }

        for (var z = 0; z < textarea.length; z++) {
            data[textarea[z].id] = textarea[z].value;
        }

        for (var key in data) {
            var val = data[key];
            str += key + ":" + val + ",";
        }
        str = str.substring(0, str.length - 1);
        return str;
    },

    cancel_module: function (event) {
        //div_module.innerHTML = "";
        div_module_search_result.innerHTML = "";
    },

    search_module: function (event) {
        $.get("GeneratorHandler.ashx", { __module_name: "Module", mode: "5" }, function (data) {
            div_module.innerHTML = data;
        });
    },

    do_search_module: function (event) {
        var m_name = "";
        var m_desc = "";
        var m_region = "0";
        var cat = "0";
        var m_type = "0";
        if (event != null) {
            m_name = module_name.value;
            m_desc = module_description.value;
            cat = list_categories.value;
            m_type = module_types.value;
            m_region = module_region.value;
        }
        else
            div_module_search_result.innerHTML = "";

        $.get("GeneratorHandler.ashx", { __module_name: "Module", mode: "6", m_name: m_name, m_desc: m_desc, m_region: m_region, cat: cat, m_type: m_type }, function (data) {
            div_module_search_result.innerHTML = data;
        });
    },

    edit_module: function (event) {
        var mod_id = "";
        if (event == null)
            mod_id = document.getElementById("module_id").value;
        else
            mod_id = event.id;

        $.get("GeneratorHandler.ashx", { __module_name: "Module", mode: "7", __module_id: mod_id }, function (data) {
            div_module.innerHTML = data;
            div_module_search_result.innerHTML = "";
            jscolor.init();
        });
    },

    delete_module: function (event) {
        $.get("GeneratorHandler.ashx", { __module_name: "Module", mode: "8", __module_id: event.id }, function (data) {
            alert("Data " + data);
            Module.do_search_module(null);
        });

    },

    update_module: function (event) {
        var module_details_data = Module.get_table_data("tbl_module_details");
        $.get("GeneratorHandler.ashx", { __module_name: "Module", mode: "9", __module_id: event.id, m_name: module_name.value, m_desc: module_description.value, m_region: module_region.value, cat: list_categories.value, md_data: module_details_data }, function (data) {
            alert("Data: " + data);
            div_module.innerHTML = "";
            Module.do_search_module(null);

        });

    },

    save_menu_module: function (event) {
        $.get("GeneratorHandler.ashx", { __module_name: "Module", mode: "10", element_menu_name: element_menu_name.value, parent: parent.value, all_contents: all_contents.value }, function (data) {
            alert("Data " + data);
            Module.do_search_module(null);
        });
    },


    get_module_details: function (event) {
        $.get("GeneratorHandler.ashx", { __module_name: "Module", mode: "3", module_type: event.value }, function (data) {
            tbl_module_details.innerHTML = data;
            //event.options[event.selectedIndex].text
            jscolor.init();
        });
    },

    cancel_edit_module: function (event) {
        $.get("GeneratorHandler.ashx", { __module_name: "Module", mode: "9" }, function (data) {
            div_module.innerHTML = "";
            div_module_search_result.innerHTML = "";
            //Module.do_search_module(null);
        });
    }
};


//*******************************Client_Menu***************************************

var Client_Menu = {
    generate: function () {
        $.get("GeneratorHandler.ashx", { __module_name: "Client_Menu", mode: "1" }, function (data) {
            ra2.innerHTML = data;
        });
    },

    new_menu: function () {
        $.get("GeneratorHandler.ashx", { __module_name: "Client_Menu", mode: "2" }, function (data) {
            div_client_menu.innerHTML = data;
            Client_Menu.get_menu_elements(null);
        });
    },

    get_menu_elements: function (ev) {
        var m_id = '';
        if (ev == null)
            m_id = document.getElementById("menu_modules").value
        else
            m_id = ev.value;

        $.get("GeneratorHandler.ashx", { __module_name: "Client_Menu", mode: "3", module_id: m_id }, function (data) {
            td_menu_elements.innerHTML = data;
        });
    },

    save_new_menu: function (ev) {
        $.get("GeneratorHandler.ashx", { __module_name: "Client_Menu", mode: "4", element_menu_name: element_menu_name.value, module_id: menu_modules.value, parent: menu_parent.value, all_contents: all_contents.value, cat_id: list_categories.value }, function (data) {
            alert("Data " + data);
        });
    },

    cancel_new_menu: function (event) {
        //alert($("#menu_modules option:selected").text());
        Client_Menu.generate();
    },

    edit_menu: function (event) {
        var menu_id = "";
        if (document.getElementById("mdoule_menu_id").value != "" && event == null)
            menu_id = document.getElementById("mdoule_menu_id").value;
        else
            menu_id = event.id;

        $.get("GeneratorHandler.ashx", { __module_name: "Client_Menu", mode: "5", module_id: menu_id }, function (data) {
            div_client_menu.innerHTML = data;
        });
    },

    edit_menu_element: function (event) {
        $.get("GeneratorHandler.ashx", { __module_name: "Client_Menu", mode: "6", menu_element_id: event.id }, function (data) {
            div_client_menu.innerHTML = data;
        });
    },

    back_to_menu: function (event) {
        Client_Menu.generate();
    },

    back_to_menu_elements: function (event) {
        mdoule_menu_id.value = event.id;
        Client_Menu.edit_menu(null);
    },

    update_menu: function (ev) {
        $.get("GeneratorHandler.ashx", { __module_name: "Client_Menu", mode: "7", element_menu_name: element_menu_name.value, module_id: menu_modules.value, parent: menu_parent.value, all_contents: all_contents.value, menu_element_id: menu_element_id.value, cat_id: list_categories.value }, function (data) {
            alert("Data " + data);
            Client_Menu.edit_menu(null);
        });
    },

    delete_menu_element: function (event) {
        $.get("GeneratorHandler.ashx", { __module_name: "Client_Menu", mode: "8", menu_element_id: event.id }, function (data) {
            alert("Data " + data);
            Client_Menu.generate();
        });
    }
};



//*******************************Engineer_Sort***************************************

var Engineer_Sort = {
    generate_search_engine: function () {
        
        $.get("GeneratorHandler.ashx", { __module_name: "Engineer_Sort", mode: "1" }, function (data) {
            document.getElementById("main_content").style.display="block";
            main_content.innerHTML = data;
            Content.xx();
        });
    },

    search: function () {
        $.get("GeneratorHandler.ashx", { __module_name: "Engineer_Sort", mode: "2", s_name: search_name.value, s_specification: search_specification.value, s_city: search_city.value, s_ministry: search_ministry.value, from_id: 0 }, function (data) {
            var res = data.split("###");
            eng_search_result.innerHTML = res[0];
            eng_show_more.innerHTML = res[1];
        });
    },

    search_more: function (id) {
        $.get("GeneratorHandler.ashx", { __module_name: "Engineer_Sort", mode: "3", s_name: search_name.value, s_specification: search_specification.value, s_city: search_city.value, s_ministry: search_ministry.value, from_id: id }, function (data) {
            var res = data.split("###");
            //eng_search_result.innerHTML += res[0];
            $("#tbl_res tbody").append(res[0]);
            eng_show_more.innerHTML = res[1];
        });
    }
};


var Search = {
    do_search: function () {
    if(things_to_search.value != ""){
        $.get("GeneratorHandler.ashx", { __module_name: "Search_Engine", mode: "1", txt: things_to_search.value}, function (data) {
            document.getElementById("main_content").style.display="block";
            document.getElementById("main_content").innerHTML = data;
            Content.xx();
        });
    }
    }
};

var Slide_Show = {
    generate: function () {
        //        var $slider = $('.slider-wrap');
        //        var html = $slider.html();

        //        // Options

        //        var defaults = {
        //            thumbSize: 20,
        //            onSlideshowEnd: function () { $('.stop, .start').toggle() }
        //        };

        //        var effects = {
        //            'default': { x: 6, y: 4, random: true },
        //            simple: { x: 6, y: 4, effect: 'simple', random: true },
        //            left: { x: 1, y: 8, effect: 'left' },
        //            up: { x: 20, y: 1, effect: 'up', rewind: 60, backReverse: true },
        //            leftright: { x: 1, y: 8, effect: 'leftright' },
        //            updown: { x: 20, y: 1, effect: 'updown', cssSpeed: 500, backReverse: true },
        //            switchlr: { x: 20, y: 1, effect: 'switchlr', backReverse: true },
        //            switchud: { x: 1, y: 8, effect: 'switchud' },
        //            fliplr: { x: 20, y: 1, effect: 'fliplr', backReverse: true },
        //            flipud: { x: 20, y: 3, effect: 'flipud', reverse: true, rewind: 75 },
        //            reduce: { x: 6, y: 4, effect: 'reduce' },
        //            360: { x: 1, y: 1, effect: '360', fade: true, cssSpeed: 600 }
        //        };

        //        $('#effects-select').change(function () {
        //            var effect = $(this).val();
        //            $slider.fadeTo(0, 0).html(html);
        //            $('.stop').hide();
        //            $('.start').show();
        //            setTimeout(function () {
        //                $('.slider').tilesSlider($.extend({}, defaults, effects[effect]));
        //                $slider.fadeTo(0, 1);
        //                $('body').removeClass('tiles-preload');
        //            }, 100);
        //            $('.code').empty().html(function () {
        //                var e = effects[effect], c = [];
        //                for (var i in e) {
        //                    if (i !== 'effect') {
        //                        c.push('<code>' + i + ': ' + e[i] + '</code>');
        //                    }
        //                }
        //                return c.join('');
        //            });
        //        });

        //        $('.start').click(function () {
        //            $(this).hide();
        //            $('.stop').show();
        //            $('.slider').tilesSlider('start')
        //        });

        //        $('.stop').click(function () {
        //            $(this).hide();
        //            $('.start').show()
        //            $('.slider').tilesSlider('stop')
        //        });


        //        $('.slider').tilesSlider($.extend({}, defaults, effects['flipud']));

        //        $(this).hide();
        //        $('.stop').show();
        //        $('.slider').tilesSlider('start');


        //$('#slider').rhinoslider();


        //        $('#slider').rhinoslider({

        //            effectTime: 3000,
        //            autoPlay: true,
        //            showCaptions: 'always',
        //            captionsOpacity: 0.7,
        //            effect: 'fade'

        //        });


        //$('#fwslider').slidesjs({         });     });
        //        new fwslider().init({
        //            duration: "1000", /* Fade Speed (miliseconds) */
        //            pause: "6000"  /* Autoslide pause between slides (miliseconds)*/
        //        });

        
        //toggle the componenet with class menu_body
        


        $('#camera_wrap_1').camera({
                thumbnails: false,
                pagination: false,
                height: '27%',
                loader: 'bar',
            });



        $('.menu_body').hide();
        $(".menu_head").click(function () {
            
            $(this).next(".menu_body").slideToggle(600);
            var plusmin;
            plusmin = $(this).children(".plusminus").text();

            if (plusmin == '+')
                $(this).children(".plusminus").text('-');
            else
                $(this).children(".plusminus").text('+');
        });

        
    }
};


//************************************Security*************************************

var Application = {
    generate: function () {
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "1", mode: "1" }, function (data) {
            app_contents.innerHTML = data;
        });
    },

    edit_application: function (id) {
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "1", mode: "2", id: id }, function (data) {
            app_contents.innerHTML = data;
        });
    },

    update_application: function (id) {
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "1", mode: "3", id: id, app_name: app_name.value, is_active: is_active.checked }, function (data) {
            Application.generate();
        });
    },

    insert_application: function () {
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "1", mode: "4" }, function (data) {
            app_contents.innerHTML = data;
        });
    },

    save_application: function () {
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "1", mode: "5", app_name: app_name.value, is_active: is_active.checked }, function (data) {
            Application.generate();
        });
    }
};



var Permission= {
    generate: function()
    {
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "2", mode: "1"}, function (data) {
            permission_contents.innerHTML= data;
        });
    },

    insert_permission: function(){
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "2", mode: "2" }, function (data) {
            permission_contents.innerHTML= data;
        });
    },

    do_insert_permission: function(){
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "2", mode: "3", menu: menu_id.value, desc: desc.value }, function (data) {
            if(data== "done")
                Permission.generate();
             else 
                alert("Error");

        });
    },

    edit_permission: function(id){
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "2", mode: "4", id: id }, function (data) {
            permission_contents.innerHTML= data;
        });
    },

    do_edit_permission: function(id){
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "2", mode: "5", id: id, menu: menu_id.value, desc: desc.value }, function (data) {
             if(data== "done")
                Permission.generate();
             else 
                alert("Error");
        });
    },

   

    delete_Permission: function(__id){
    $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "2", mode: "6", id: __id }, function (data) {
            if(data== "done")
                Permission.generate();
             else 
                alert("Error");
        });  
    }
};



var Role= {
    generate: function()
    {
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "3", mode: "1"}, function (data) {
            role_contents.innerHTML= data;
        });
    },

    insert_role: function(){
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "3", mode: "2" }, function (data) {
            role_contents.innerHTML= data;
        });
    },

    do_insert_role: function(){
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "3", mode: "3", r_name: r_name.value, desc: desc.value, from_date: from_date.value, to_date: to_date.value }, function (data) {
            if(data== "done")
                Role.generate();
             else 
                alert("Error");

        });
    },

    edit_role: function(id){
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "3", mode: "4", id: id }, function (data) {
            role_contents.innerHTML= data;
        });
    },

    do_edit_role: function(id){
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "3", mode: "5", id: id, r_name: r_name.value, desc: desc.value, from_date: from_date.value, to_date: to_date.value }, function (data) {
             if(data== "done")
                Role.generate();
             else 
                alert("Error");
        });
    },

    delete_role: function(__id){
    $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "3", mode: "6", id: __id }, function (data) {
            if(data== "done")
                Role.generate();
             else 
                alert("Error");
        });  
    }
};

var PermissionInRole={
    get_permissions_role: function(event){
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "4", mode: "1", role_id: event.value}, function (data) {
            all_permissions.innerHTML= data;
        });
    },

    add_permission: function(){
         var val= $("select#lst_permissions_not_in_role option:selected").val();
         var txt= $("select#lst_permissions_not_in_role option:selected").text();
         $("select#lst_permissions_not_in_role option[value='"+val+"']").remove();

         if(val != "undefined" && txt != "")
         {
             var o = new Option(txt, val);
            /// jquerify the DOM object 'o' so we can use the html method
            //$(o).html("option text");
            $("#lst_permissions_in_role").append(o);
         }
    },

    remove_permission: function(){
         var val= $("select#lst_permissions_in_role option:selected").val();
         var txt= $("select#lst_permissions_in_role option:selected").text();
         
         $("select#lst_permissions_in_role option[value='"+val+"']").remove();

         if(val != "undefined" && txt != "")
         {
             var o = new Option(txt, val);
            /// jquerify the DOM object 'o' so we can use the html method
            //$(o).html("option text");
            $("#lst_permissions_not_in_role").append(o);
         }
    },

    save_Permission_in_role: function(){
        var permission_vals= "";//$("select#lst_permissions_in_role option:selected").val();
        
         for (var i = 0, j = lst_permissions_in_role.options.length; i < j; i++)
         {
         permission_vals +=lst_permissions_in_role.options[i].value+",";
         }
        
        if(permission_vals != "")
            permission_vals= permission_vals.substring(0, permission_vals.length-1);

        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "4", mode: "2", permission_vals: permission_vals, role_id: lst_roles.value}, function (data) {
            alert(data);
        });
    }
};

var User={
    generate: function(){
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "5", mode: "1"}, function (data) {
            user_contents.innerHTML= data;
        });
    },

    insert_user: function(){
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "5", mode: "2"}, function (data) {
            user_contents.innerHTML= data;
        });
    },

    do_insert_user: function(){
        var tbl_insert = Module.get_table_data("tbl_insert_user");
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "5", mode: "3", tbl_data: tbl_insert}, function (data) {
        if(data !="done")
                div_info.innerHTML= data;
        else{
                alert(data);
                User.generate();
            }
        });
    },

    edit_user: function(id){
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "5", mode: "4", id: id}, function (data) {
            user_contents.innerHTML= data;
        });
    },
    
    do_edit_user: function(id){
        var tbl_edit = Module.get_table_data("tbl_edit_user");
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "5", mode: "5", tbl_data: tbl_edit, id: id}, function (data) {
        if(data !="done")
                div_info.innerHTML= data;
        else{
                alert(data);
                User.generate();
            }
        });
    },
    

    delete_user: function(id){
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "5", mode: "6", id: id}, function (data) {
        if(data=="done")
            User.generate();
        });
    },   
};

var UserInRole={
    get_users_role: function(event){
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "6", mode: "1", role_id: event.value}, function (data) {
            all_users.innerHTML= data;
        });
    },

    add_user: function(){
         var val= $("select#lst_users_not_in_role option:selected").val();
         var txt= $("select#lst_users_not_in_role option:selected").text();
         $("select#lst_users_not_in_role option[value='"+val+"']").remove();

         if(val != "undefined" && txt != "")
         {
             var o = new Option(txt, val);
            /// jquerify the DOM object 'o' so we can use the html method
            //$(o).html("option text");
            $("#lst_users_in_role").append(o);
         }
    },

    remove_user: function(){
         var val= $("select#lst_users_in_role option:selected").val();
         var txt= $("select#lst_users_in_role option:selected").text();
         
         $("select#lst_users_in_role option[value='"+val+"']").remove();

         if(val != "undefined" && txt != "")
         {
             var o = new Option(txt, val);
            /// jquerify the DOM object 'o' so we can use the html method
            //$(o).html("option text");
            $("#lst_users_not_in_role").append(o);
         }
    },

    save_user_in_role: function(){
        var user_vals= "";//$("select#lst_permissions_in_role option:selected").val();
        
         for (var i = 0, j = lst_users_in_role.options.length; i < j; i++)
         {
         user_vals +=lst_users_in_role.options[i].value+",";
         }
        
        if(user_vals != "")
            user_vals= user_vals.substring(0, user_vals.length-1);
            
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "6", mode: "2", user_vals: user_vals, role_id: lst_roles1.value}, function (data) {
            alert(data);
        });
    }
};

var LogIn= {
    generate: function(){
        $.get("GeneratorHandler.ashx", { __module_name: "Security", type: "7", mode: "1"}, function (data) {
                region_log_in.innerHTML= data;
        });
    }
}

var gallary = {
    edit: function (id1) {
        gname = 'gname' + id1;
        var gname = $('#' + gname).html();
        $('#gid').val(id1);
        $('#gName').val(gname);
        $('#gpanel').fadeIn();
    }
    ,
    delete: function (id1) {
        if (window.confirm('Confirm Delete ?')) {
            $.get("GeneratorHandler.ashx", { __module_name: "gallary", mode: "delete", id: id1 }, function (data) {
                var ra2 = document.getElementById('ra2');
                ra2.innerHTML = data;
            });
        }
    }
    ,
    deleteImg: function (id1) {
        if (window.confirm('Confirm Delete Img ?')) {
            $.get("GeneratorHandler.ashx", { __module_name: "gallary", mode: "deleteimg", id: id1 }, function (data) {
                var ra2 = document.getElementById('ra2');
                ra2.innerHTML = data;
            });
        }
    }
    ,
    toggleActive: function (id1) {
        $.get("GeneratorHandler.ashx", { __module_name: "gallary", mode: "toogleActive", id: id1}, function (data) {
            var ra2 = document.getElementById('ra2');
            ra2.innerHTML = data;
        });
    }
    ,
    add: function () {
        $('#gid').val('');
        $('#gName').val('');
        $('#gpanel').fadeIn();
    }
    ,
    addImage: function (id1) {
        try{
            var fd = new FormData();
            var file = document.getElementById('f'+id1);
            fd.append('img', file.files[0]);
            fd.append("id", id1);
            fd.append("__module_name", "gallary");
            fd.append("mode", "addimg");
            var xhr = new XMLHttpRequest();
            xhr.open('POST', '/view/GeneratorHandler.ashx', true);
            xhr.onload = function () {
                if (this.status == 200) {
                    $.get("GeneratorHandler.ashx", { __module_name: "gallary", mode: "generate", gid: id1}, function (data) {
                        var ra2 = document.getElementById('ra2');
                        ra2.innerHTML = data;
                    });
                }
            }
            xhr.send(fd);
        }
        catch (e) {
            alert(e.message);
        }
    }
    ,
    viewImges: function (id) {
        if ($('#gd' + id).attr('mode') == "c") {
            $('#gd' + id).fadeIn();
            $('#gd' + id).attr('mode', 'o');
        }
        else {
            $('#gd' + id).fadeOut();
            $('#gd' + id).attr('mode', 'c');
        }
    },
    ok: function () {
        try{
            var id1 = $('#gid').val();
            var m = 'new';
            if (id1 != null && id1 != '')
                m = 'edit';
            var gname = $('#gName').val();
            $.get("GeneratorHandler.ashx", { __module_name: "gallary", mode: ''+m, id:''+id1 , name: gname}, function (data) {
                var ra2 = document.getElementById('ra2');
                ra2.innerHTML = data;
                $('#gpanel').fadeOut();
            });
        }
        catch (e) {
            alert(e.message);
        }
    },
    cancel: function () {
        try{
            $('#gpanel').fadeOut();
        }
        catch (e) {

        }
    }
};
var users = {
    items: ['first_name', 'last_name', 'birthdate', 'start_date', 'end_date', 'type', 'company', 'serial_number', 'address', 'gender', 'location', 'work_category', 'social_number', 'phone', 'mobile', 'website', 'email', 'user_name', 'pwd', 'social_network1', 'social_network2', 'social_newwork3']
    ,
    ok : function(){
        try {
            //make your validations
            var mandatory = ['first_name', 'last_name', 'email', 'user_name', 'address', 'social_number ', 'mobile', 'company'];
            var fields = ['First Name', 'Last Name', 'Email', 'User Name', 'Address', 'Social Number', 'Mobile', 'company'];

            var pwd = $('#pwd').val();
            var cpwd = $('#confirm').val();
            if (pwd.length < 5) {
                msg.innerHTML = 'Too Short Password';
                return;
            }
            if (pwd != cpwd) {
                msg.innerHTML = 'Password Confirm does not match Password';
                return;
            }
            msg.innerHTML = "";
            var index = 0;
            for (key in mandatory) {
                var val = mandatory[key];
                val = $('#' + val).val();
                if (val == null || val == "") {
                    msg.innerHTML += fields[index] + " is mandatory <br/>";
                }
                index++;
            }
            if (msg.innerHTML != null && msg.innerHTML != "")
                return;

            //then save data
            var fd = new FormData();
            for (key in users.items) {
                var k = users.items[key];
                var v = $('#' + k).val();
                fd.append("" + k, v);
            }
            var file = document.getElementById('img');
            fd.append('img', file.files[0]);
            var id = $('#Id').val();
            var mode = 'new';
            if (id != "" && id != null) {
                fd.append("id", id);
                mode = 'edit';
            }
            fd.append('__module_name', 'user');
            fd.append('mode', mode);

            var xhr = new XMLHttpRequest();
            xhr.open('POST', '/view/GeneratorHandler.ashx', true);
            xhr.onload = function () {
                if (this.status == 200) {
                    //Generator.init();
                }
            }
            xhr.send(fd);
        }
        catch (e) {
            alert(e.message);
        }
    },
    cancel : function(){
        try {
            $('#upanel').fadeOut();
        }
        catch (e) {

        }
    },
    add: function () {
        try {
            $('#id').val('');
            for (var i = 0; i < users.items.length; i++) {
                $('#' + users.items[i]).val('');
            }
            $('#user_name').removeAttr('readonly');
            document.getElementById('userLabel').innerHTML = "User Name";
            document.getElementById('pwdLabel').innerHTML = "Password";
            document.getElementById('pwdConfirmLabel').innerHTML = "Confirm Password";
            document.getElementById('pwd').style.display = '';
            document.getElementById('confirm').style.display = '';
            $('#upanel').fadeIn();
            try {
                $("#start_date").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'yy-mm-dd' });
                $("#end_date").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'yy-mm-dd' });
                $("#birthdate").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'yy-mm-dd' });
            }
            catch (e) { alert("datepicker: " + e.message); }
        }
        catch (e) {
            alertMsg(e.message);
        }
    },
    edit : function(id1){
        //uname = 'uname' + id1;
        //var uname = $('#' + uname).html();
        //$('#uid').val(id1);
        //$('#uName').val(uname);
        $.get("/view/GeneratorHandler.ashx", { __module_name: "user", mode: "getclientinfo", id: '' + id1 }, function (data) {
            var obj = jQuery.parseJSON(data);
            for (var key in obj) {
                $('#' + key).val(obj[key]);
            }
            //$('#id').val(id1);
            $('#upanel').fadeIn();
            $('#user_name').attr('readonly', 'true');
            document.getElementById('userLabel').innerHTML = "User Name RO";
            document.getElementById('pwdLabel').innerHTML = "";
            document.getElementById('pwdConfirmLabel').innerHTML = "";
            document.getElementById('pwd').style.display = 'none';
            document.getElementById('confirm').style.display = 'none';
        });
    },
    delete : function(id1){
        if (window.confirm('Confirm Delete ?')) {
            $.get("GeneratorHandler.ashx", { __module_name: "user", mode: "delete", id: id1 }, function (data) {
                var ra2 = document.getElementById('ra2');
                ra2.innerHTML = data;
            });
        }
    },
    toggleActive : function(id1){
        $.get("GeneratorHandler.ashx", { __module_name: "user", mode: "toogleActive", id: id1 }, function (data) {
            var ra2 = document.getElementById('ra2');
            ra2.innerHTML = data;
        });
    },
    viewDetails : function(id){

    },
    toggleCheckAll: function () {
        try {
            var allIds = $('#ids').val().split(",");
            for (var i = 0; i < allIds.length; i++) {
                $('#uchk' + allIds[i]).attr('checked', (!($('#uchk' + allIds[i]).attr('checked'))));
            }
        }
        catch (e) {
            alertMsg(e.message);
        }
    },
    activeChecked: function () {
        try {
            var _ids = "";
            var allIds = $('#ids').val().split(",");
            for (var i = 0; i < allIds.length; i++) {
                var isChecked = $('#uchk' + allIds[i]).attr('checked');
                if (isChecked) {
                    _ids += allIds[i] + ",";
                }
            }
            $.get("GeneratorHandler.ashx", { __module_name: "user", mode: "activeChecked", ids: _ids }, function (data) {
                var ra2 = document.getElementById('ra2');
                ra2.innerHTML = data;
            });
        }
        catch (ee) {
            alertMsg(ee.message);
        }
    }
    ,
    deActiveChecked: function () {
        try {
            var _ids = "";
            var allIds = $('#ids').val().split(",");
            for (var i = 0; i < allIds.length; i++) {
                var isChecked = $('#uchk' + allIds[i]).attr('checked');
                if (isChecked) {
                    _ids += allIds[i] + ",";
                }
            }
            $.get("GeneratorHandler.ashx", { __module_name: "user", mode: "deactiveChecked", ids: _ids }, function (data) {
                var ra2 = document.getElementById('ra2');
                ra2.innerHTML = data;
            });
        }
        catch (ee) {
            alertMsg(ee.message);
        }
    },
    resetPWD: function (id1) {
        if (window.confirm('continue resetting password for thi client to "123456" ?')) {
            $.get("GeneratorHandler.ashx", { __module_name: "user", mode: "resetpwd", id: id1}, function (data) {
                alert(data);
            });
        }
    }
};
