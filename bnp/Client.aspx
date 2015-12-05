<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Client.aspx.cs" Inherits="bnp_Client" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>BNP Card-Client Profile</title>
        <script src="/js/jquery.js" type="text/javascript"></script>
        <script src="/js/general.js" type="text/javascript"></script>

        <script src="/js/jquery-ui-1.9.2.custom.min.js"></script>
        <script src="/view/js/jquery_theatre_min.js" type="text/javascript"></script>
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
        <link rel="stylesheet" href="/assets/css/jquery-ui.css">
        <link rel="stylesheet" href="/view/css/demo.css">
        <link rel="stylesheet" href="/view/css/theatre.css">
        <script type="text/javascript" src="/js/bnp.js" ></script>
        <script>
            $(function () {
                $("#tabs").tabs();
            });
        </script>
    </head>
    <body  runat="server" id="body1">
    <input type='hidden' runat="server" id='regions' value='region_header,client_region' />
    <input type='hidden' runat="server" id='regionsPriorities' value='1,2' />
    <form id="form1" runat="server">
        <div class="basic_menu" id="region_header">
        </div>
        <br clear="all" />
        <div>
            <br clear="all" />
              <div class="main_part"  >
            <table width="70%" style="float:left;">
                <tr>
                    <td width="5%" valign="top"></td>
                    <td width="100%" valign="top">
                        <div id="client_region" style="widows:100%;">
                        </div>
                    </td>
                   
                    <td width="5%"></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <div id="tabs">
                             <ul>
                                  <li><a href="#bnpVouchers">BNP Vouchers</a></li>
                                <li><a href="#myVouchers">My Vouchers</a></li>
                               
                            </ul>                        
                            <div id='myVouchers' runat="server">

                            </div>
                            <div id='bnpVouchers' runat="server">

                            </div>
                        </div>
                    </td>
                    <td></td>
                </tr>
            </table>
             <div style="width:20%;float:right;margin-right:5%;">
                
                
                                <div id="side_bar" style="width:100%;">
                                       <br clear="all"> 
                                        <style>
                                            #side_bar_banner1{
                                                width:100%;
                                                height:360px;
                                                background-color:#ccc;		
                                            }
                                            #side_bar_banner2{
                                                width:100%;
                                                height:160px;
                                                background-color:#ccc;		
                                            }
                                            #side_bar_banner3{
                                                width:100%;
                                                height:760px;
                                                background-color:#ccc;		
                                            }
                                        </style>
                                      <div  id="side_bar_banner1" style='width:100%'><%=control.cmsmodules.PannerModule.generatePanner("upper", "fade", "360px") %></div>
                                       <br clear="all"> 
                                       <div id="side_bar_banner2" style='width:100%'><%=control.cmsmodules.PannerModule.generatePanner("middle", "fade", "160px") %></div>
                                        <br clear="all"> 
                                         <div id="side_bar_banner3"  style='width:100%'><%=control.cmsmodules.PannerModule.generatePanner("bottom", "fade", "760px") %></div>
                                        <br clear="all"> 
                                      </div>
                                      <br clear="all">
                 
            </div>
           </div>
        </div>
    </form>
</body>
</html>
