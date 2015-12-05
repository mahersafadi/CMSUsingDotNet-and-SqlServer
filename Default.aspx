<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en" dir="ltr" xmlns="http://www.w3.org/1999/xhtml">
 <head>
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta name="description" content="">
<meta name="author" content="">
<link rel="icon" href="favicon.ico">
<title>BNP CARD</title>
<script src="js/jquery.js" type="text/javascript"></script>
<script src="js/general.js" type="text/javascript"></script>
<script src="/view/js/jquery-1.8.2.js" type="text/javascript"></script>
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
<link rel="Stylesheet" href="/assets/css/style.css" />
<link rel="Stylesheet" href="/view/web/css/bnp_card.css" />
<link href="/view/bnp/carousel.css" rel="stylesheet">
<link rel="stylesheet" href="/view/bnp/css/fancybox/jquery.fancybox-buttons.css">
<link rel="stylesheet" href="/view/bnp/css/fancybox/jquery.fancybox-thumbs.css">
<link rel="stylesheet" href="/view/bnp/css/fancybox/jquery.fancybox.css">
<link rel="stylesheet" href="/view/bnp/css/demo.css">
<link rel="stylesheet" href="/view/css/demo.css">
<link rel="stylesheet" href="/view/css/theatre.css">
     <script type="text/javascript">
         $(window).load(function() { // Run when everything is loaded (especially images)
             $('#myExample').theatre({ //Initialize theatre
                 // For comlete list of possible settings visit
                 'effect': 'fade', // css3:stack effect and fall over to 3d effect for older browsers
                 'selector': 'img', // for galleries we want to animage only images without the surrounding links
                 'controls': true,
                 
                 'paging': '.Paging', // generate paging using element with class 'paging' as the template
                  paging: '#Paging' 
             });
            
         });
	</script>
          <style type="text/css">
	  .Paging {
	 text-align: end;
margin: 0;
position: relative;
z-index: 999;
top: -50px;
right: 10px;
	  }
	  .Paging span {
	  text-decoration: none;
	 
	  cursor: pointer;
	  }
	  .Paging span.active {
	  font-size: 4em;
	  color: silver;
	  }

	  .Paging2 span {
	  font-size: 4em;
	  }

	</style>
</head>
    <body  runat="server" id="body1">
        <input type='hidden' runat="server" id='regions' value='region_header,r1_middle,region_content_middle' />
        <input type='hidden' runat="server" id='regionsPriorities' value='1,2,3,4' />
        <table width="100%">
            <tr>
                <td width="100%" align="left">
                   <!---------------------------------------------------------> 
                    <div  id="region_header">
                        
                    </div>

                    <div class="slide_show_container" id="region_headertop" runat="server">
                     </div>
               
                    <div class="slide_show_container" id="region_headertop_1" runat="server">
                       
                     </div>

                      <!---------------------------------------------------------> 
                    <%--<script src="/view/bnp/js/jquery1.11.min_SS.js"></script>--%>
                   

                    <div class="container content" runat="server" id="mainContent">

                    </div>
                    <div class="main_part"  >
                        <table width="100%">
                            <tr>
                                <td width="10%" valign="top"></td>
                                <td width="70%" valign="top">
                                    <table width="100%">    
                                        <tr><td valign="top" id="region_content_middle" runat="server"></td></tr>
                                        <tr><td><div class="seperator"></div></td></tr>
                                        <tr><td id="r1_middle"></td></tr>
                                    </table>
                                </td>
                                <td width="10px"></td>
                                <td width="248px" valign="top">
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
                                </td>
                                <td width="100px"></td>
                            </tr>
                            <tr>
                                <td colspan="5" height="30px">
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>

        <!---------------------------footer---------------------------------->  
        <!---------------------------footer---------------------------------->  
        <!---------------------------footer---------------------------------->        
<footer>
  <div id="footer_contant">
  <div id="footer_sitemap">
   
    <ul class="footer_sitemap_ul_p">
          <li class=""> <a href="#" >About BNP <span class="caret"></span></a>
              <ul class="footer_sitemap_ul_s">
                <li><a href="#">The Concept</a></li>
                <li class="divider"></li>
                <li><a href="#">Profile</a></li>
                
                
                <li class="dropdown-header"></li>
                
              </ul>
            </li>
           <li class="dropdown"> <a href="#" class="dropdown-toggle" data-toggle="dropdown">Partners <span class="caret"></span></a>
              <ul class="footer_sitemap_ul_s" >
                <li><a href="#">Partner List</a></li>
                <li class="divider"></li>
                <li><a href="#">Become a Partner</a></li>
                
                
                <li class="dropdown-header"></li>
                
              </ul>
            </li>
            <li class="dropdown"> <a href="#" class="dropdown-toggle" data-toggle="dropdown">Privacy Policy <span class="caret"></span></a>
              <ul class="footer_sitemap_ul_s">
                <li><a href="#">Privacy Policy</a></li>
                <li class="divider"></li>
                <li><a href="#">Terms of Use</a></li>
                
                
                <li class="dropdown-header"></li>
                
              </ul>
            </li>
           
            
             
            
               
               
          </ul>
          </div>
           <div id="footer_sitemap1">
    <ul class="footer_sitemap_ul_p">
           <li><a href="#">Offers</a></li>
             <li><a href="faq.html">Media Gallery</a></li>
             
            <li><a href="contact.html">Contact us</a></li>       
            <li><a href="signup.html">Sign In or Register</a></li>
          </ul>
  </div>
  
  <div id="footer_news_leter" >
   <h2>Site Map</h2>
    <p class="lead2" style="color:#fff">Subscribe to our Newsletter</p>
    <form class="footer_news_leter_form">
      <input type="text" class="footer_news_leter_txt" value="enter your email">
      <input type="button" class="footer_news_leter_btn" value="Subscribe">
    </form>
    <br clear="all">
    <br clear="all">
    <div id="footer_contact_links">
    <h2>Follow us on:</h2>  <br clear="all">
<ul  >
                <li><a href="#" class="social_icon"><img src="view/imag/icons/fb.png" > </a></li>
               <li> <a class="social_icon" href="#"><img src="view/imag/icons/tweeter.png" > </a></li>
               <li> <a class="social_icon" href="#"><img src="view/imag/icons/gp.png" > </a></li>
               <li> <a class="social_icon" href="#"><img src="view/imag/icons/camera.png" > </a></li>
             <li>   <a class="social_icon" href="#"><img src="view/imag/icons/yt.png" > </a></li>
                
                
                
               
                   <br clear="all">
              </ul>
 </div>
  </div>
  <div id="footer_contact">
  <h2>Contact Us</h2>
   <div id="footer_contact_phon1">
 <p class="lead3" style="color:#999"> B International Group</p>
 <p class="lead3" style="color:#999"> P.O.Box 14-6716 Beirut, Lebanon</p>
  
      <p class="lead3" style="color:#999">Koraytem, Beirut Lebanon</p>
      <p class="lead3" style="color:#999">00961   1 863 860</p>
      <p class="lead3" style="color:#999">info@bnpcard.com</p>
   
    </div>
     <h2>Sign In or Register</h2>
 <br clear="all" >
 </div>
 <br clear="all" >
 </div>
  </footer>

     <%--   <table width="100%">
            <tr>
                <td height="266px" class="footer">
                    <table width="100%">
                        <tr>
                            <td width="100px">
                            </td>
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
                                                    <td class="img"><img src="view/imag/icons/fb.png"</td>
                                                    <td class="img"><img src="view/imag/icons/tweeter.png"</td>
                                                    <td class="img"><img src="view/imag/icons/gp.png"</td>
                                                    <td class="img"><img src="view/imag/icons/camera.png"</td>
                                                    <td class="img"><img src="view/imag/icons/yt.png"</td>
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
                                    <tr><td class="footer_h" nowrap="nowrap" onclick="goTo('/Default.aspx?id=1410')">Contact Us&nbsp;&nbsp;</td>
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
        </table>--%>


        <script src="/view/bnp/bootstrap/dist/js/bootstrap.min.js"></script> 
        <script src="/view/bnp/bootstrap/docs/assets/js/docs.min.js"></script>
    </body>
</html>