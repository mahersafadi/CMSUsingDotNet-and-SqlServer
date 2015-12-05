<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changePWD.aspx.cs" Inherits="changePWD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BnP Card Change Password</title>
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
<body runat="server" id="body1">
    <input type='hidden' runat="server" id='regions' value='region_header' />
    <input type='hidden' runat="server" id='regionsPriorities' value='1' />
    <form id="form1" runat="server">
       <div class="head_upper"></div>
       <div id="region_header"></div>
       <div>
        <table>
            <tr><td height="30px"><input id="x"  type="hidden" runat="server" value="" /></td></tr>
            <tr>
                <td class="'label">&nbsp;&nbsp;&nbsp;User Name:&nbsp;&nbsp;</td><td><input id="userName" runat="server" /></td>
            </tr>
            <tr>
                <td class="'label">&nbsp;&nbsp;&nbsp;Current Password:&nbsp;&nbsp;</td><td><input id="currPWD" runat="server" /></td>
            </tr>
            <tr>
                <td class="'label">&nbsp;&nbsp;&nbsp;New Password:&nbsp;&nbsp;</td><td><input id="newPWD" runat="server" /></td>
            </tr>
            <tr>
                <td class="'label">&nbsp;&nbsp;&nbsp;Confirm Password:&nbsp;&nbsp;</td><td><input id="confirmPWD" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2" runat="server" id="msg" name="msg">

                </td>
            </tr>
            <tr>
                <td><input type="button" onclick="f()" value="Continue" /></td>
            </tr>
        </table>
           <script>
               function f() {
                   document.getElementById('x').value = "changePWD";
                   var form = document.getElementById('form1');
                   form.submit();
               }
           </script>
    </div>
    </form>
</body>
</html>
