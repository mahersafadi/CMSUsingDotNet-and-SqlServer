function displayProfile() {
    try {
        alert('display profile');
    }
    catch (e) {
        alert(e.message);
    }
}


function editProfile(usrId)
{
    try {
        //client_profile
        //client_profileEdit
        $('#client_profile').fadeOut();
        $('#client_profileEdit').fadeIn();;
    }
    catch (e) {
        alert(e.message);
    }
}

function backToProfile() {
    $('#client_profile').fadeIn();
    $('#client_profileEdit').fadeOut();;
}
function addNewClient() {
    try{
        
        $('#new_client').fadeIn("slow");
        
    }
    catch (e) {
        alert(e.message);
    }
}

function cancelNewClient() {
    try {

        $('#new_client').fadeOut("slow");

    }
    catch (e) {
        alert(e.message);
    }
}

function doSaveNewClient(id) {
    try {
        var prefix = "";
        if (id != null && id != "")
            prefix = "gen";
        var msg = document.getElementById('newClientMsg');
        msg.innerHTML = '';
        //Collect
        var items = ['first_name', 'last_name', 'email', 'user_name', 'pwd', 'address', 'social_number', 'mobile', 'social_network1', 'csocialnetwork2', 'birthdate', 'gender'];
        var mandatory = ['first_name', 'email', 'user_name', 'pwd', 'address', 'social_number ', 'mobile'];
        var fields = ['First Name', 'Last Name', 'Email', 'User Name', 'Password', 'Address', 'Social Number', 'Mobile', 'gender'];
        //Check Password, and reconfirm
        if (id == null || id == "") {
            var pwd = $('#pwd').val();
            var cpwd = $('#cpwd').val();
            if (pwd.length < 5) {
                msg.innerHTML = 'Too Short Password';
                return;
            }
            if (pwd != cpwd) {
                msg.innerHTML = 'Password Confirm does not match Password';
                return;
            }
        }
        var index = 0;
        if(id == null && id == ''){
            for (key in mandatory) {
                var val = mandatory[key];
                val = $('#' + (prefix+val)).val();
                if (val == null || val == "") {
                    msg.innerHTML += fields[index] + " is mandatory <br/>";
                }
                index++;
            }
            if (msg.innerHTML != null && msg.innerHTML != "")
                return;
        }
        var fd = new FormData();
        for (key in items) {
            var k = items[key];
            var v = $('#'+(prefix+k)).val();
            fd.append("" + k, v);
        }
        fd.append("iddddddd", $('#iddddddd').html());
        var file = document.getElementById('img');
        fd.append('img', file.files[0]);

        if (id != "" && id != null) {
            fd.append("id", id);
        }

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/bnp/BnpHandler.ashx', true);
        xhr.onload = function () {
            if (this.status == 200) {
                Generator.init();
            }
        }
        xhr.send(fd);
    }
    catch (e) {
        alert(e.message);
    }
}

function progressHandlingFunction(e){
    if(e.lengthComputable){
        //$('progress').attr({value:e.loaded,max:e.total});
    }
}

var bnpmgmt = {
    ok : function(){
        try {
            if (document.getElementById('mainTitle').innerHTML != "SlideShow") {
                if ($('#title').val() == null || $('#title').val() == '') {
                    document.getElementById('pmsg').innerHTML = "Title is Mandatroy";
                    return;
                }
                if ($('#_contnt').val() == null || $('#_contnt').val() == '') {
                    document.getElementById('pmsg').innerHTML = "Content is Mandatroy";
                    return;
                }
            }
            else if (document.getElementById('contentImg').files.length == 0) {
                document.getElementById('pmsg').innerHTML = "Image is Mandatroy";
            }
            var fd = new FormData();
            var m = "new";
            if ($('#id').val() != null && $('#id').val() != '')
                m = "update";
            fd.append('id', $('#id').val());
            fd.append('detId', $('#detId').val());
            fd.append('title', $('#title').val());
            fd.append('cat', document.getElementById('mainTitle').innerHTML);
            fd.append('content', $('#_contnt').val());
            var file = document.getElementById('contentImg');
            fd.append('contentimg', file.files[0]);
            fd.append('__module_name', 'BnpVoucherOfferNewsSlideShowMgmt');
            fd.append('mode', m);
            fd.append('sk', $('#secretkey').val());
            var xhr = new XMLHttpRequest();
            xhr.open('POST', '/bnp/BnpHandler.ashx', true);
            xhr.onload = function (data) {
                if (this.status == 200) {
                    bnpmgmt.filter();
                }
            }
            xhr.send(fd);
        }
        catch(e){
            alert(e.message);
        }
    },
    cancel : function(){
        try {
            $('#panel').fadeOut();
        }
        catch (e) {

        }
    },
    filter: function () {
        var filterByType = $('#search_type').val();
        var filterByName = $('#search_name').val();
        var filterByPartner = $('#search_partner').val();
        $.get("/bnp/BnpHandler.ashx", {
            __module_name: "BnpVoucherOfferNewsSlideShowMgmt",sk: $('#secretkey').val(),
            mode: "filter", type: filterByType, name: filterByName, partner: filterByPartner
        },
            function (data) {
                var bnp_content = document.getElementById('bnp_content');
                try{
                    bnp_content.innerHTML = data;
                    bnpmgmt.initDates();
                }
                catch (e) {
                    alert(e.message);
                }
            }
        );
    },
    initDates: function () {
        try {
            var idsToDated = $('#idsToDated').val();
            idsToDated = idsToDated.split(",");
            for (var i = 0; i < idsToDated.length; i++) {
                $('#from_date_' + idsToDated[i]).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'yy-mm-dd' });
                $('#to_date_' + idsToDated[i]).datepicker({ changeMonth: true, changeYear: true, dateFormat: 'yy-mm-dd' });
            }
        }
        catch (e) {
            alert(e.message);
        }
    },
    getDataForEdit: function (title, id) {
        $.get("/bnp/BnpHandler.ashx", { __module_name: "BnpVoucherOfferNewsSlideShowMgmt", mode: "getDataForEdit", cid: id },
            function (data) {
                try{
                    data = data.split('@@@@');
                    document.getElementById('mainTitle').innerHTML = title;
                    $('#id').val(data[0]);
                    $('#detId').val(data[1]);
                    $('#title').val(data[2]);
                    $('#_contnt').val(data[3]);
                    $('#contentImg').val('');
                    $('#panel').fadeIn();
                }
                catch (e) {
                    alert(e.message);
                }
            }
        );
    },
    editOffer : function(id){
        bnpmgmt.getDataForEdit('Edit Offer', id);
    },
    editVoucher: function (id) {
        bnpmgmt.getDataForEdit('Edit voucher', id);
    },
    editNews: function (id) {
        bnpmgmt.getDataForEdit('Edit News', id);
    }
    , editSlideShow: function (id) {
        bnpmgmt.getDataForEdit('Edit Slide Show', id);
    },
    editVoucher: function (id) {
        bnpmgmt.getDataForEdit('Edit Voucher', id);
    },
    setOfferPercentage: function (id, cid) {
        var _partner = $('#partner_' + id).val();
        var newPercent = $('#percentage_' + id).val();
        var mobile_ = $('#mobile_' + cid).val();
        var phone_ = $('#phone_' + cid).val();
        var region_ = $('#region_' + cid).val();
        var category_ = $('#category_' + cid).val();
        var email_ = $('#email_' + cid).val();
        var address_ = $('#address_' + cid).val();
        var open_ = $('#open_' + cid).val();
        var close_ = $('#close_' + cid).val();
        var sat_open_ = $('#sat_open_' + cid).val();
        var sat_close_ = $('#sat_close_' + cid).val();
        var sun_open_ = $('#sun_open_' + cid).val();
        var sun_close_ = $('#sun_close_' + cid).val();
        var facebook_ = $('#facebook_' + cid).val();
        var tweeter_ = $('#tweeter_' + cid).val();
        var youtube_ = $('#youtube_' + cid).val();
        var instagram_ = $('#instagram_' + cid).val();
        
        $.get("/bnp/BnpHandler.ashx", {
            __module_name: "BnpVoucherOfferNewsSlideShowMgmt", sk: $('#secretkey').val(), mode: "setOfferPercent", partner: _partner, percent: newPercent,
            mobile:mobile_, phone:phone_, region:region_ , email:email_, address:address_, category:category_, open:open_, close:close_, sat_open:sat_open_, sat_close:sat_close_, sun_open:sun_open_, sun_close:sun_close_, facebook:facebook_, tweeter:tweeter_, youtube:youtube_, instagram:mobile_, did: id
        },
            function (data) {
                bnpmgmt.filter();
            }
        );
    },
    setPlaceAndTime: function(id){
        var newPlace = $('#place_' + id).val();
        var newFrom = $('#from_' + id).val();
        var newTo = $('#to_' + id).val();
        $.get("/bnp/BnpHandler.ashx", { __module_name: "BnpVoucherOfferNewsSlideShowMgmt", sk: $('#secretkey').val(), mode: "setPlaceAndTime", place: newPlace, from:newFrom, to:newTo, did: id },
            function (data) {
                bnpmgmt.filter();
            }
        );
    },
    setFromTo: function(id){
        //Set From and Till for news
    },
    setOwnerAndNumber: function (id) {
        //alert('setOwnerAndNumber');
        var _partner = $('#partner_' + id).val();
        var _defaultNumber = $('#default_' + id).val();
        var _offerProvided = $('#provided_offer_' + id).val();
        $.get("/bnp/BnpHandler.ashx", { __module_name: "BnpVoucherOfferNewsSlideShowMgmt", sk: $('#secretkey').val(), mode: "setOwnerAndNumber", partner: _partner, defaultNumber: _defaultNumber, did: id , op:_offerProvided},
            function (data) {
                bnpmgmt.filter();
            }
        );
    },
    newOffer: function () {
        document.getElementById('mainTitle').innerHTML = "Offer";
        $('#id').val('');
        $('#detId').val('');
        $('#title').val('');
        $('#_contnt').val('');
        $('#contentImg').val('');
        $('#panel').fadeIn();
    },
    newVoucher: function () {
        document.getElementById('mainTitle').innerHTML = "Voucher";
        $('#id').val('');
        $('#detId').val('');
        $('#title').val('');
        $('#_contnt').val('');
        $('#contentImg').val('');
        $('#panel').fadeIn();
    },
    newNews: function () {
        document.getElementById('mainTitle').innerHTML = "News";
        $('#id').val('');
        $('#detId').val('');
        $('#title').val('');
        $('#_contnt').val('');
        $('#contentImg').val('');
        $('#panel').fadeIn();
    },
    newSlideShow: function () {
        document.getElementById('mainTitle').innerHTML = "SlideShow";
        $('#id').val('');
        $('#detId').val('');
        $('#title').val('');
        $('#_contnt').val('');
        $('#contentImg').val('');
        $('#panel').fadeIn();
    },
    newPoster: function () {
        document.getElementById('mainTitle').innerHTML = "Poster";
        $('#id').val('');
        $('#detId').val('');
        $('#title').val('');
        $('#_contnt').val('');
        $('#contentImg').val('');
        $('#panel').fadeIn();
    },
    toggleSlideShowDisplay: function (id1) {
        $.get("/bnp/BnpHandler.ashx", { __module_name: "BnpVoucherOfferNewsSlideShowMgmt", mode: "toggleSlideShowDisplay", id: id1 },
            function (data) {
                bnpmgmt.filter();
            }
        );
    },
    displyVoucherInfo: function (did, id1) {
        try{
            $.get("/bnp/BnpHandler.ashx", { __module_name: "BnpVoucherOfferNewsSlideShowMgmt", mode: "displyVoucherInfo", did: did, id: id1 },
                function (data) {
                    bnpmgmt.ctntId = id1;
                    bnpmgmt.detId = did;
                    document.getElementById('voucherInfo').innerHTML = data;
                }
            );
        }
        catch(e){}
    }
    ,
    ctntId:null,
    detId:null,
    searchSerialNumber: function () {
        try {
            var serialNumber = $('#searchSerialNumber').val();
            $.get("/bnp/BnpHandler.ashx", { __module_name: "BnpVoucherOfferNewsSlideShowMgmt", mode: "searchSerialNumber", did: bnpmgmt.detId, id: bnpmgmt.ctntId, sn: serialNumber },
                function (data) {
                    document.getElementById('clientsRegion').innerHTML = data;
                }
            );
        }
        catch (e) { }
    }
    ,
    changeVoucherNumbers: function (vchrId) {
        try {
            var serialNumber = $('#searchSerialNumber').val();
            var numberOfVouchers = $('#numberOfVouchers').val();
            $.get("/bnp/BnpHandler.ashx", { __module_name: "BnpVoucherOfferNewsSlideShowMgmt", mode: "changeVoucherNumbers", did: bnpmgmt.detId, vid:vchrId,sn: serialNumber, nov:numberOfVouchers},
                function (data) {
                    document.getElementById('clientsRegion').innerHTML = data;
                }
            );
        }
        catch (e) {
            alert(e.message);
        }
    },
    editPoster: function (id1) {
        bnpmgmt.getDataForEdit('Edit Poster', id1);
    },
    changePosterInfo: function (id1) {
        var panner1 = $('#panner_'+id1).val();
        var status1 = $('#poststatus_' + id1).val();
        var fromDate1 = $('#from_date_'+id1).val();
        var toDate1 = $('#to_date_' + id1).val();
        var _url = $('#url_' + id1).val();
        $.get("/bnp/BnpHandler.ashx", { __module_name: "BnpVoucherOfferNewsSlideShowMgmt", mode: "changePosterInfo", id: id1, panner: panner1, status: status1, fromDate: fromDate1, toDate: toDate1, url: _url},
                function (data) {
                    bnpmgmt.filter();
                }
            );
    },
    deleteBnpItem: function (cId) {
        try {
            if (window.confirm('Continue with delete?')) {
                $.get("/bnp/BnpHandler.ashx", { __module_name: "BnpVoucherOfferNewsSlideShowMgmt", mode: "deleteBnpItem", id: cId },
                    function (data) {
                        bnpmgmt.filter();
                    }
                );
            }
        }
        catch (e) {
            alert(e.message);
        }
    }
};