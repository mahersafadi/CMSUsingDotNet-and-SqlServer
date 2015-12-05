<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>BNP Card Site Admin</title>
    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/admin.js" type="text/javascript"></script>    
    <script src="js/jscolor/jscolor.js" type="text/javascript"></script>
    <!-- SimpleTabs -->
    <script type="text/javascript" src="js/simple_tab/simpletabs_1.3.js"></script>
    <script src="js/Crypto.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/layout.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="css/ie.css" type="text/css" media="screen" />
	
</head>
<body  runat="server" id="body1">
        <input type='hidden' runat="server" id='regions' value='' />
        <input type='hidden' runat="server" id='regionsPriorities' value='' />
     <table style='width: 100%; height: 30px; background-color: #427196;'>
        <tr>
            <td style="width: 45%">
                <a href='Default.aspx'>
                    <img src="images/icons/home.png" id='imgHome' alt="home" /></a>
            </td>
            <td>
                <h1 style="color: White;">
                    <%= model.module.Lang.getByKey("site_management")%>
                </h1>
            </td>
            <td>
                <asp:MultiView runat="server" ID="mvLogin" ActiveViewIndex="0">
                    <asp:View runat="server" ID="vwGuest">
                    </asp:View>
                    <asp:View runat="server" ID="vwUser">
                        <div style="color: White">
                            <%--Welcome,
                            <%= ((model.db.user)System.Web.HttpContext.Current.Session["user"]).last_name + " " + 
                            ((model.db.user)System.Web.HttpContext.Current.Session["user"]).last_name%>
                            <br />--%>
                            <asp:Label runat="server" ID="lblUser"></asp:Label>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
    <asp:MultiView runat="server" ID="mvAll" ActiveViewIndex="1">
        <asp:View runat="server" ID="vwAll">
            <div id="r1_left_1">
            </div>
            <div id='ra1'>
            </div>
            <div id='ra2'>
            </div>
            <div id='ra3'>
            </div>
        </asp:View>
        <asp:View runat="server" ID="vwLogin">
          <form id="Form1" runat="server">
            <asp:LinkButton runat="server" ID="lnkLogout" Text="Logout" OnClick="lnkLogout_Click"></asp:LinkButton>
            <center>
                <table width="500px">
                    <tr>
                        <th colspan="2">
                            <center>
                                <%= model.module.Lang.getByKey("log_in") %>
                            </center>
                        </th>
                    </tr>
                    <tr>
                        <td align="right">
                            <%= model.module.Lang.getByKey("user_name") %>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtUserName"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <%= model.module.Lang.getByKey("password") %>
                        </td>
                        <td>
                            <asp:TextBox runat="server" TextMode="Password" ID="txtPassword"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button  runat="server" ID="btnOk" OnClick="btnOk_Click" Text="Log In" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label runat="server" ID="lblInfo" Style='color: Red; font-size: 14px;'></asp:Label>
                        </td>
                    </tr>
                </table>
            </center>
          </form>
        </asp:View>
    </asp:MultiView>
</body>
</html>
