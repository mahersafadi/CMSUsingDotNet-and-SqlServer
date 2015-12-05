<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sponser.aspx.cs" Inherits="bnp_Sponser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Offers List</title>
        <meta charset="utf-8" />
        <meta charset="utf-8" />
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
        <link rel="Stylesheet" href="/view/web/css/bnp_card.css" />
        <link href="/view/bnp/carousel.css" rel="stylesheet">
        <link rel="stylesheet" href="/view/bnp/css/fancybox/jquery.fancybox-buttons.css">
        <link rel="stylesheet" href="/view/bnp/css/fancybox/jquery.fancybox-thumbs.css">
        <link rel="stylesheet" href="/view/bnp/css/fancybox/jquery.fancybox.css">
        <link rel="stylesheet" href="/view/bnp/css/demo.css">
    </head>
    <body  runat="server" id="body1">
        <input type='hidden' runat="server" id='regions' value='region_header' />
        <input type='hidden' runat="server" id='regionsPriorities' value='1' />
        <table width="100%">
            <tr>
                <td width="100%" align="center">
                    <div class="head_upper">

                    </div>
                    <div id="region_header">
                        
                    </div>

                    <div class="main_part">
                        <table width="100%">
                            <tr>
                                 <td width="10%" valign="top"></td>
                                <td width="60%" valign="top">
                                    <table width="100%">    
                                        <tr><td valign="top"> <div id="offersListDiv" runat="server"></div> </td></tr>
                                        <tr><td><div class="seperator"></div></td></tr>
                                         <tr><td id="r1_middle"></td></tr>
                                        <tr><td></td></tr>
                                    </table>
                                </td>
                                <td width="10px"></td>
                               <td width="253px" valign="top">
                                    <div id="side_bar" style="width:100%;">
                                       <br clear="all"> 
                                        <style>
                                           #side_bar_banner1{
                                                width:100%;
                                                height:358px;
                                                background-color:#ccc;		
                                            }
                                            #side_bar_banner2{
                                                width:100%;
                                                 height:275px;
                                                background-color:#ccc;		
                                            }
                                            #side_bar_banner3{
                                                width:100%;
                                                height:612px;
                                                background-color:#ccc;		
                                            }
                                        </style>
                                     <div  id="side_bar_banner1" style='width:100%'><%=control.cmsmodules.PannerModule.generatePanner("upper", "fade", "358px") %></div>
                                       <br clear="all"> 
                                       <div id="side_bar_banner2" style='width:100%'><%=control.cmsmodules.PannerModule.generatePanner("middle", "fade", "275px") %></div>
                                        <br clear="all"> 
                                         <div id="side_bar_banner3"  style='width:100%'><%=control.cmsmodules.PannerModule.generatePanner("bottom", "fade", "612px") %></div>
                                        <br clear="all"> 
                                      </div>
                                      <br clear="all">
                                </td>
                                <td width="100px"></td>
                            </tr>
                            <tr>
                                <td colspan="5" height="30px"></td>
                            </tr>
                            <tr>
                                <td colspan="5" height="266px" class="footer">
                                    <table width="100%">
                                        <tr>
                                            <td width="100px"></td>
                                            <td  valign="top">
                                                <table>
                                                    <tr><td height="30px"></td></tr>
                                                    <tr><td class="footer_h">About BNP</td></tr>
                                                    <tr><td class="footer_b">The Concept</td></tr>
                                                    <tr><td class="footer_b">Profile</td></tr>
                                                    <tr><td height="30px"></td></tr>
                                                    <tr><td class="footer_h">Partenenrs</td></tr>
                                                    <tr><td class="footer_b">Partener List</td></tr>
                                                    <tr><td class="footer_b">Become a partener</td></tr>
                                                    <tr><td height="30px"></td></tr>
                                                    <tr><td class="footer_h">Privacy Policy</td></tr>
                                                </table>
                                            </td>
                                            <td width="55px"></td>
                                            <td valign="top">
                                                <table>
                                                    <tr><td height="30px"></td></tr>
                                                    <tr><td class="footer_h">Terms Of Use</td></tr>
                                                    <tr><td class="footer_h">Benefit Providers</td></tr>
                                                    <tr><td class="footer_h">Offers</td></tr>
                                                    <tr><td class="footer_h">Media Gallery</td></tr>
                                                    <tr><td class="footer_h">News & Events</td></tr>
                                                </table>
                                            </td>
                                            <td width="80px"></td>
                                            <td valign="top">
                                                <table>
                                                    <tr><td class="footer_c" align="center">Site Map</td></tr>
                                                    <tr><td height="40px"></td></tr>
                                                    <tr>
                                                        <td align="center" id="subscribeModule">
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr><td height="24px"></td></tr>
                                                    <tr><td class="footer_h" align="center">Follow us on:</td></tr>
                                                    <tr><td height="15px"></td></tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td class="img"><img src="/view/imag/icons/fb.png"</td>
                                                                    <td class="img"><img src="/view/imag/icons/tweeter.png"</td>
                                                                    <td class="img"><img src="/view/imag/icons/gp.png"</td>
                                                                    <td class="img"><img src="/view/imag/icons/camera.png"</td>
                                                                    <td class="img"><img src="/view/imag/icons/yt.png"</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="80px"></td>
                                            <td valign="top">
                                                <table>
                                                    <tr><td height="30px"></td></tr>
                                                    <tr><td class="footer_h" nowrap="nowrap">Contact Us&nbsp;&nbsp;</td>
                                                        <td class="footer_b"> B International Group</td>
                                                    </tr>
                                                    <tr><td></td><td class="footer_b">P.O.Box 14-6716 Beirut, Lebanon</td></tr>
                                                    <tr><td></td><td class="footer_b">Koraytem, Beirut, Lebanon</td></tr>
                                                    <tr><td colspan="2" height="10px"></td></tr>
                                                    <tr><td></td><td class="footer_b">00961    1 863 860</td></tr>
                                                    <tr><td colspan="2" height="10px"></td></tr>
                                                    <tr><td></td><td class="footer_b">info@bnpcard.com</td></tr>
                                                    <tr><td height="20px"></td></tr>
                                                    <tr><td colspan="2" class="footer_h" align="center">Sign In Or Register</td></tr>
                                                </table>
                                            </td>
                                            <td width="100px"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <script src="/view/bnp/bootstrap/dist/js/bootstrap.min.js"></script> 
        <script src="/view/bnp/bootstrap/docs/assets/js/docs.min.js"></script>
    </body>
<//html>