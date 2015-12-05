<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Signin.aspx.cs" Inherits="Signin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>BNP Card-Signing In</title>
    <meta charset="utf-8" />
    <script src="/js/jquery.js" type="text/javascript"></script>
    <script src="/js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
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
    <link rel="Stylesheet" href="/assets/css/jquery-ui.css" />
    <link rel="Stylesheet" href="/assets/css/style.css" />
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
    <form id="form1" runat="server">
         <div class="head_upper">

         </div>
        <div id="region_header">
        </div>
    
       <div class="main_part"  >
           <div class="table-responsive">
                        <table class="table-responsive" width="100%">
                            <tr>
                                <td width="10%" valign="top"></td>
                                <td width="60%" valign="top">
<div class="table-responsive">
                    <table class="table-responsive" width="100%">
                        <tr><td height="10px"></td></tr>
                        <tr>
                            <td class="label_main1">Sign In</td>
                        </tr>
                        <tr>
                            <td height="1px" width="100%" bgcolor="#393939"></td>
                        </tr>
                        <tr>
                            <td height="25px"></td>
                        </tr>
                        <tr>
                            <td><table class="signin_table2 table">
                                <tr><td class="label_main2">UserName&nbsp;&nbsp;&nbsp;</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtUserName" CssClass="inp"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr><td height="5px"></td></tr>
                                <tr><td class="label_main2">Password&nbsp;&nbsp;&nbsp;</td>
                                    <td>
                                        <asp:TextBox runat="server" TextMode="Password" ID="txtPassword" CssClass="inp"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr><td height="5px"></td></tr>
                                <tr><td></td>
                                    <td><table class="table" width="100%"><tr><td align="left"><a href="#" style="font-size:10px;border-bottom:1px solid #333;color:#333"> Forgot your password?</a></td>
                                                    <td align="right">
                                                        <asp:Button runat="server" ID="lnkSignIn" Text="&nbsp;&nbsp;Sign In&nbsp;&nbsp;" OnClick="lnkLogin_Click" class="button1" ></asp:Button>
                                                    </td>
                                               </tr></table>

                                    </td>
                                </tr>
                                <tr><td> <asp:Label runat="server" ID="lblInfo" Style='color: Red; font-size: 14px;'></asp:Label></td></tr>
                                </table></td>
                        </tr>








                        <tr>
                            <td height="45px"></td>
                        </tr>

                       

                        <tr>
                            <td class="label_main1">Register / Reorder your BNP Card</td>
                        </tr>
                        <tr>
                            <td height="1px" width="100%" bgcolor="#393939"></td>
                        </tr>
                        <tr>
                            <td height="25px"></td>
                        </tr>
                        <tr>
                            <td>
                                <table class="signin_table2 table">
                                    <tr>
                                        <td colspan="2">
                                            <input value="As Client" type="button" id="client" name="user_type" onclick="client_company_clicked(3)" checked="checked" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<input value="As Partner" type="button" id="partner" name="user_type"  onclick="client_company_clicked(2)"/>
                                            <input runat="server" id="txtKind" type="hidden"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label runat="server" ID="Label21" Style='color: Red; font-size: 14px;'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label runat="server" ID="Labe22" Style='color: green; font-size: 14px;'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label_main2">First Name&nbsp;&nbsp;&nbsp;</td>
                                        <td><asp:TextBox runat="server" ID="txtFirstName" CssClass="inp"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="label_main2">Last Name&nbsp;&nbsp;&nbsp;</td>
                                        <td><asp:TextBox runat="server" ID="txtLastName" CssClass="inp"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="label_main2">Email&nbsp;&nbsp;&nbsp;</td>
                                        <td><asp:TextBox runat="server" ID="txtMobile" CssClass="inp"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="label_main2">Phone&nbsp;&nbsp;&nbsp;</td>
                                        <td><asp:TextBox runat="server" ID="txtPhone" CssClass="inp"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="label_main2">Email&nbsp;&nbsp;&nbsp;</td>
                                        <td><asp:TextBox runat="server" ID="txtEmail" CssClass="inp"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="label_main2">Address 1&nbsp;&nbsp;&nbsp;</td>
                                        <td><asp:TextBox runat="server" ID="txtAddress1" CssClass="inp"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="label_main2">Address 2&nbsp;&nbsp;&nbsp;</td>
                                        <td><asp:TextBox runat="server" ID="txtAddress2" CssClass="inp"></asp:TextBox></td>
                                    </tr>
                                    </table>
                               <table class="signin_table2 table" >
                                    <tr>
                                        <td class="label_main2">Region&nbsp;&nbsp;&nbsp;</td>
                                        <td>
                                            <select id="selRegion" runat="server">
                                            </select>
                                        </td>
                                    </tr>
                                    <tr id="gender" style="display:none;">
                                        <td class="label_main2">Gender&nbsp;&nbsp;&nbsp;</td>
                                        <td>
                                            <asp:RadioButtonList ID="raGender" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem  Selected="True" Text="Male&nbsp;&nbsp;&nbsp;&nbsp;" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="FMale&nbsp;&nbsp;&nbsp;&nbsp;" Value="2"></asp:ListItem>
                                                <%--<asp:ListItem Text="Others" Value="3"></asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr id="birthday" style="display:none;">
                                        <td class="label_main2">Birthdate&nbsp;&nbsp;&nbsp;</td>
                                        <td>
                                            <ASP:TextBox runat="server" ID="txtDate"></ASP:TextBox>
                                        </td>
                                    </tr>
                                      
                                    <tr id="comapny" style="display:none;">
                                        <td class="label_main2">Company&nbsp;&nbsp;&nbsp;</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="Company" CssClass="inp"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="website" style="display:none;">
                                        <td class="label_main2">Website&nbsp;&nbsp;&nbsp;</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtWebsite" CssClass="inp"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="category" style="display:none;">
                                        <td class="label_main2">Category&nbsp;&nbsp;&nbsp;</td>
                                        <td>
                                            <select id="selCategory" runat="server">
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label runat="server" ID="Label31" Style='color: Red; font-size: 14px;'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label runat="server" ID="Labe32" Style='color: green; font-size: 14px;'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <asp:Button runat="server" ID="Button2" Text="&nbsp;&nbsp;Sign Up&nbsp;&nbsp;" OnClick="lnkSignUp_Click" class="button1" ></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
    </div>
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
        </table>        
    </div> </div>
    </form>
    <script type="text/javascript" language="javascript">
        var clientItems = ['gender', 'birthday'];
        var companyItems = ['comapny', 'website', 'category'];
        $(document).ready(function () {

            try {
                $("#txtDate").datepicker({ changeMonth: true, changeYear: true, dateFormat: 'yy-mm-dd' });;
            }
            catch (e) {alert("datepicker: "+e.message);}
            client_company_clicked(3);
        });
        function client_company_clicked(ch) {
            try {
                var co = '';
                var cl = '';
                if (ch == 3) {
                    //client
                    $('#partner').attr("style", "color:#fff; background-color:#3C3836 !important;");
                    $('#client').attr("style", "color:#fff; background-color:#E61071 !important;");

                    co = 'none';
                    cl = '';
                }
                else if(ch==2){
                    co = '';
                    cl = 'none';
                    $('#partner').attr("style", "color:#fff; background-color:#E61071 !important;");
                    $('#client').attr("style", "color:#fff; background-color:#3C3836 !important;");
                }
                {
                    for (var i = 0; i < companyItems.length; i++) {
                        var x = document.getElementById(companyItems[i]);
                        x.style.display = co;
                       
                    }
                    for (var i = 0; i < clientItems.length; i++) {
                        var x = document.getElementById(clientItems[i]);
                        x.style.display = cl;
                       
                    }
                }
                $('#txtKind').val(ch);
            }
            catch (e) {
                alert(e.message);
            }
        }
    </script>
    <script src="/view/bnp/bootstrap/dist/js/bootstrap.min.js"></script> 
    <script src="/view/bnp/bootstrap/docs/assets/js/docs.min.js"></script>
</body>
</html>
