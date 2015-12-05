<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Partner.aspx.cs" Inherits="bnp_Partner" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>BNP Card-Partener Profile</title>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <meta name="description" content="">
        <meta name="author" content="">
        <link rel="icon" href="favicon.ico">
        <title>BNP CARD</title>
        <script src="/js/jquery.js" type="text/javascript"></script>
        <script src="/js/general.js" type="text/javascript"></script>
        <link rel="icon" href="/view/bnp/favicon.ico">
        <link href="/view/bnp/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">
        <!--[if lt IE 9]><script src="/view/bnp/bootstrap/docs/assets/js/ie8-responsive-file-warning.js"></script><![endif]-->
        <script src="/view/bnp/bootstrap/docs/assets/js/ie-emulation-modes-warning.js"></script>
        <script src="/view/bnp/bootstrap/docs/assets/js/ie10-viewport-bug-workaround.js"></script>
        <!--[if lt IE 9]>
              <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
              <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
            <![endif]-->
        <link rel="Stylesheet" href="../view/web/css/bnp_card.css" />
        <link href="/view/bnp/carousel.css" rel="stylesheet">
        <link rel="stylesheet" href="/view/bnp/css/fancybox/jquery.fancybox-buttons.css">
        <link rel="stylesheet" href="/view/bnp/css/fancybox/jquery.fancybox-thumbs.css">
        <link rel="stylesheet" href="/view/bnp/css/fancybox/jquery.fancybox.css">
        <link rel="stylesheet" href="/view/bnp/css/demo.css">
        <link rel="stylesheet" href="/view/css/demo.css">
        <link rel="stylesheet" href="/view/css/theatre.css">
        <script src="/js/jquery-ui-1.9.2.custom.min.js"></script>
        <script src="/view/js/jquery_theatre_min.js" type="text/javascript"></script>

        <script type="text/javascript" src="/js/bnp.js" ></script>
    </head>
    <body  runat="server" id="body1">
    <input type='hidden' runat="server" id='regions' value='region_header,client_region' />
    <input type='hidden' runat="server" id='regionsPriorities' value='1,2' />
    <form id="form1" runat="server">
        <div  id="region_header">
                        
        </div>
        <script>
            ffff();
            ffff = function () {
                var x = document.getElementById('menu_container');
                x.style = 'margin-top:-71px';
            }
        </script>
        <div>
            <table>
                <tr>
                    <td width="95px"></td>
                    <td width="745px">
                        <table>
                            <tr>
                                <td>
                                    <div id="client_region" style="widows:100%;"></div>
                                </td>
                            </tr>
                            <tr><td height="30px"></td></tr>
                            <tr><td><h3><b>My Offers</b></h3></td></tr>
                            <tr>
                                <td id="myOffers" runat="server">

                                </td>
                            </tr>
                            <tr><td height="10px"></td></tr>
                            <tr><td><h3><b>My Vouchers</b></h3></td></tr>
                            <tr>
                                <td id="myVouchers" runat="server">
                                    My Vouchers list is set here as tags
                                </td>
                            </tr>
                            <tr><td id="voucherInfo"></td></tr>
                            <tr><td id="clientsRegion">

                                </td></tr>
                            <tr>
                                <td height="25px"></td>
                            </tr>
                        </table>
                    </td>
            </table>
        </div>
    </form>
</body>
</html>
<%--  --%>