<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Adminisrtation.aspx.cs" Inherits="view_Adminisrtation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html dir='<%=model.module.Lang.getDirection()%>' xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >
    <title></title>
    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/general.js" type="text/javascript"></script>
    <script src="/js/bnp.js" type="text/javascript"></script>
    <script src="/js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <link id="style_file" href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="js/jscolor/jscolor.js" type="text/javascript"></script>
    
    <style type="text/css" media="screen">
        @import "css/simple_tab/style.css";
    </style>
    <!-- SimpleTabs -->
    <script type="text/javascript" src="js/simple_tab/simpletabs_1.3.js"></script>
    <script src="js/Crypto.js" type="text/javascript"></script>
    <style type="text/css" media="screen">
        @import "css/simple_tab/simpletabs.css";
    </style>
    <link href="content/tab/css/foundation.css" rel="stylesheet" type="text/css" />
</head>
<body onload="Menu.generate()">
    
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
    <div style="width:80%; margin:0 auto;">
    <asp:MultiView runat="server" ID="mvAll" ActiveViewIndex="1">
        <asp:View runat="server" ID="vwAll">
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
                <table class="gridtable" >
                    <tr>
                        <th colspan="2">
                            <center>
                                <%= model.module.Lang.getByKey("log_in") %>
                            </center>
                        </th>
                    </tr>
                    <tr>
                        <th>
                            <%= model.module.Lang.getByKey("user_name") %>
                        </th>
                        <td>
                            <asp:TextBox runat="server" ID="txtUserName"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <%= model.module.Lang.getByKey("password") %>
                        </th>
                        <td>
                            <asp:TextBox runat="server" TextMode="Password" ID="txtPassword"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th colspan="2">
                            <asp:Button CssClass="medium alert button" runat="server" ID="btnOk" OnClick="btnOk_Click" />
                        </th>
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
        </div>
    <input type="hidden" id="secretkey" runat="server" />
</body>
</html>
