<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title>Contact Us</title>
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
    <style>
        .main_part {
            text-align:start;
        }
    </style>
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
                <%--        <table width="100%">
                            <tr>
                                <td width="100px"></td>
                                <td width="733px" >
                                    <table width="100%">    
                                        <tr><td >
                                           
                                            </td></tr>
                                        <tr><td><div class="seperator"></div></td></tr>
                                        <tr><td></td></tr>
                                    </table>
                                </td>
                                <td width="15px"></td>
                                <td width="248px"></td>
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
                        </table>--%>

                        
         <div id="contactUsDiv" runat="server">
                                                 



             
<div class="container_marketing">
 <br clear="all"> 
  <!-- Three columns of text below the side_bar -->
   <!-- Three columns of text below the side_bar -->
  
  <!-- Three columns of text below the side_bar -->
   <!-- Three columns of text below the side_bar -->
   <div class="row">
   <div class="about_title">
   <h2>Contact Us</h2>
  
   <br clear="all">
   </div><br clear="all">
   <!-- START THE row_conten 1111111111111111111111 -->
  
  <!-- /.row --> 
  
  <!-- START THE FEATURETTES -->
  
  
    
     <!-- START THE row_conten 22222222222222222 -->
  <br clear="all">
  <div class="row_conten">
 
<div id="contact_box">
<ul>
<li><div class="contact_box_1">
<h2>Address</h2>
<h3>B International Group<br>
P.O.Box 14-6716 Beirut, Lebanon<br>
Koraytem, Beirut Lebanon</h3>
</div></li>
<li><div class="contact_box_1">
<h2>Phone</h2>
<h3>00961   1 863 860</h3>
</div></li>
<li><div class="contact_box_1">
<h2>Email</h2>
<h3>info@bnpcard.com</h3>
</div></li>
<li><div class="contact_box_follow">

<h2>Follow Us</h2>
 <a href="#" class="social_icon"><img src="view/images/fb.png" > </a>
 <a class="social_icon" href="#"><img src="view/images/twitter.png" > </a>
 <a class="social_icon" href="#"><img src="view/images/gmail.png" > </a>
 <a class="social_icon" href="#"><img src="view/images/insta.png" > </a>
 <a class="social_icon" href="#"><img src="view/images/youtube.png" > </a>
                
                
              
             
</div></li>
</ul>
  <br clear="all">
  <h2 class="map_title">Map</h2>
<img src="images/map.fw.png" class="map_contact">
  <br clear="all">
    <br clear="all">
 <h2 class="map_title">Email Us</h2>
    <form id="form1" runat="server">
    <div>
        <table  class="contact_table1">
            <tr>  
                <td colspan="3" >
                    <div >Submit a request for more information</div>  
                </td>  
            </tr>  
            <tr>  
                 <td width="30%" height="38">Name:</td>
                 
               <td width="70%">
                    <asp:TextBox ID="txtName" runat="server" Width="300px"  size="25" maxlength="50" />
                </td>   
                <td >
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" 
                        ControlToValidate="txtName" Display="Dynamic" 
                        ErrorMessage="Name">*</asp:RequiredFieldValidator>
                </td>   
            </tr>  
            <tr>  
                <td >Organization (optional):</td>  
                <td><asp:TextBox ID="txtOrganization" runat="server" Width="300px" /></td>   
                <td >
                </td>   
            </tr>  
            <tr>  
                <td >Phone (optional):</td>  
                <td><asp:TextBox ID="txtPhone" runat="server" Width="300px" /></td>   
                <td >
                </td>   
            </tr>  
            <tr>
                <td colspan="3" >
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" >
                    <asp:Button ID="Button1" runat="server" Text="Submit" Width="60px" OnClick="Submit" />&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="Reset" Width="60px" CausesValidation="False" OnClick="Reset" />
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                        HeaderText="Please fill out the following required fileds:" 
                        ShowMessageBox="True" ShowSummary="False" />
                </td>
            </tr>
            </table>
         <table  class="contact_table2">
            <tr>
                 <td width="30%" height="38" align="left">Email:</td>  
               
                <td><asp:TextBox ID="txtEmail" runat="server" Width="300px" /></td>   
                <td >
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                        ControlToValidate="txtEmail" Display="Dynamic" 
                        ErrorMessage="Email">*</asp:RequiredFieldValidator>
                </td>   
            </tr>  
            <tr>  
                <td >Request or question:</td>  
                <td><asp:TextBox ID="txtRequest" runat="server" TextMode="MultiLine" Width="100%" Height="60px" /></td>   
                <td >
                    <asp:RequiredFieldValidator ID="rfvRequest" runat="server" 
                        ControlToValidate="txtRequest" Display="Dynamic" 
                        ErrorMessage="Request or question">*</asp:RequiredFieldValidator>
                </td>   
            </tr>  
        </table>
    
    </div>
    </form>
<%-- <form method="POST" action="contactusprocess.asp">

<table  class="contact_table1" >
    <tr>
      <td width="30%" height="38">Name:</td>
      <td width="70%"><input name="Name" size="25" maxlength="50"></td>


    </tr>
    
    <tr>
      <td width="30%" height="38">phone:</td>
      <td width="70%"><input name="Telephone No" size="25" maxlength="50"> </td>
    </tr>
    <tr>
      <td width="30%" height="38">Email:</td>
      <td width="70%"><input name="Email Address" size="25" maxlength="50"></td>
    </tr>
    <tr>
      <td width="30%" height="38">Region:</td>
      <td width="70%">
    <Select>
     <option value="Select your Region">Select your Region</option>
     <option value="Select your Region">Select your Region</option>
     <option value="Select your Region">Select your Region</option>
      </Select>
    </td>
    </tr>
  
  </table>
  <table class="contact_table2" >
    <tr>
      <td width="30%" height="38" align="left">Subject</td>
      <td colspan="2"><input name="Name" size="25" maxlength="50"></td>
    </tr>
    
    <tr>
      <td width="30%" align="left" valign="top">Message </td>
       
      <td colspan="2"><textarea cols="24" name="Message" rows="6"></textarea> </td>
    </tr>
    <tr>
      <td align="left">  </td>
      <td width="37%">&nbsp;</td>
      <td width="33%"><input type="submit" value="send"
      name="submit" style="background-color:#333; color: #FFF;"></td>
    </tr>
  </table>
  </form>--%>

</div>

    
    </div>
    
    
      <!-- START THE FEATURETTES -->
    </div>
 
 <div id="side_bar">
   <br clear="all"> 
  <div id="side_bar_banner1"></div>
   <br clear="all"> 
   <div id="side_bar_banner2"></div>
    <br clear="all"> 
     <div id="side_bar_banner3"></div>
    <br clear="all"> 
  </div>
  <br clear="all">
  <!-- /END THE FEATURETTES --> 
  
  <!-- FOOTER --> 
  
</div>
<!-- /.container -->
                                            </div>

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
         <!---------------------------footer---------------------------------->  
        <!---------------------------footer---------------------------------->  
        <!---------------------------footer---------------------------------->     
        <script src="/view/bnp/bootstrap/dist/js/bootstrap.min.js"></script> 
        <script src="/view/bnp/bootstrap/docs/assets/js/docs.min.js"></script>
    </body>
<//html>