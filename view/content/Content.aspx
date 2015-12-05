<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" ValidateRequest="false"
    CodeFile="Content.aspx.cs" Inherits="view_content_Content" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--<script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/general.js" type="text/javascript"></script>
    <link id="style_file" href="../css/style-ar.css" rel="stylesheet" type="text/css" />
    <script src="../ckeditor/ckeditor.js" type="text/javascript"></script>
    <link href="../ckeditor/contents.css" rel="stylesheet" type="text/css" />
    <script src="../ckeditor/config.js" type="text/javascript"></script>
    <link href="../ckeditor/skins/moono/editor.css" rel="stylesheet" type="text/css" />
    <script src="../ckeditor/lang/ar.js" type="text/javascript"></script>
    <script src="../ckeditor/styles.js" type="text/javascript"></script>
    <script src="../ckeditor/build-config.js" type="text/javascript"></script>
    <script src="tabs/domtab.js" type="text/javascript"></script>
    <link href="tabs/domtab.css" rel="stylesheet" type="text/css" />
    <style>
        border: solid;</style>--%>
    <link href="../css/style-<%= Global.getLangFromSession() %>.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/general.js" type="text/javascript"></script>
    <script src="../ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../ckeditor/build-config.js" type="text/javascript"></script>
    <link rel="stylesheet" href="tab/css/foundation.css" />
    <script src="tab/js/vendor/modernizr.js"></script>


   <%-- <script src="datepicker/js/foundation-datepicker.js" type="text/javascript"></script>
    <link href="datepicker/css/foundation-datepicker.css" rel="stylesheet" type="text/css" />
    
    <script>
        $(function () {

            $('#dp1').fdatepicker({
                format: 'mm-dd-yyyy'
            });

        });
	</script>--%>
</head>
<body dir="<%= model.module.Lang.getDirection() %>">
    <form id="form1" runat="server">
    <div>
        <asp:MultiView runat="server" ID="mvAll" ActiveViewIndex="0">
            <asp:View runat="server" ID="vwInsertContent">
                <table class='gridtable'>
                    <tr>
                        <th colspan='2'>
                            <%= model.module.Lang.getByKey("insert_new_content") %>
                        </th>
                    </tr>
                    <tr>
                        <th>
                            <%= model.module.Lang.getByKey("category_name")%>
                        </th>
                        <td >
                            <asp:DropDownList runat="server" ID="lstCategories">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <%= model.module.Lang.getByKey("thumbnail") %>
                        </th>
                        <td>
                        <center>
                            <asp:FileUpload runat="server" ID="fileUpload1" />
                            <br />
                            <b><%= model.module.Lang.getByKey("or") %></b>
                            <br /><br />
                            <asp:CheckBox runat="server" ID="chkDefaultThumbnail" /> <%= model.module.Lang.getByKey("select_default_thumbnail") %>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td colspan='2'>
                            <asp:Button CssClass="medium alert button" Visible="<%# control.cmsmodules.Security.hasPermission(control.cmsmodules.PermissionName.insert_content.ToString()) %>"
                                runat="server" ID="btnSave" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label runat="server" ID="lblImage" Style="color: Red;"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View runat="server" ID="vwInsertContentDetail">
                <dl class="tabs" data-tab>
                    <dd class="active">
                        <a href="#panel2-1">
                            <%= model.module.Lang.getByKey("content_detail")%></a></dd>
                    <dd>
                        <a href="#panel2-2">
                            <%= model.module.Lang.getByKey("key_word")%></a></dd>
                    <dd>
                        <a href="#panel2-3"><%= model.module.Lang.getByKey("content_images")%></a>
                    </dd>
                    <dd>
                        <a href="#panel2-3"><%= model.module.Lang.getByKey("content_images")%></a>
                    </dd>
                    <dd>
                        <a href="#panel2-3"><%= model.module.Lang.getByKey("extra_fields")%></a>
                    </dd>
                </dl>
                <div class="tabs-content">
                    <div class="content active" id="panel2-1">
                        <p>
                        </p>
                        <table class='gridtable'>
                            <tr>
                                <th>
                                    <%= model.module.Lang.getByKey("language") %>
                                </th>
                                <td>
                                    <asp:DropDownList runat="server" ID="lstLangiages">
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hfCdId" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <%= model.module.Lang.getByKey("title") %>
                                </th>
                                <td>
                                    <asp:TextBox runat="server" Width="100%" ID="txtTitle"></asp:TextBox>
                                </td>
                            </tr>
                            <tr runat="server" visible="<%# isVisible() %>">
                                <th>
                                    <%= model.module.Lang.getByKey("no") %>
                                </th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNo"></asp:TextBox>
                                </td>
                            </tr>
                            <tr runat="server" visible="<%# isVisible() %>">
                                <th>
                                    <%= model.module.Lang.getByKey("publish_date") %>
                                </th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPublishDate"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <%= model.module.Lang.getByKey("text") %>
                                </th>
                                <td>
                                    <textarea class="ckeditor" runat="server" cols='80' id="editor1" name="editor1" rows='10'
                                        style="visibility: hidden; display: none;">	</textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button CssClass="medium alert button" Visible="<%# control.cmsmodules.Security.hasPermission(control.cmsmodules.PermissionName.insert_content.ToString()) %>"
                                        runat="server" ID="btnSaveAll" OnClick="btnSaveAll_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="content" id="panel2-2">
                        <p>
                        </p>
                        <table class="gridtable">
                            <tr>
                                <td>
                                    <%= model.module.Lang.getByKey("key_word") %>
                                    1
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtKeyWord1" Width="300"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= model.module.Lang.getByKey("key_word") %>
                                    2
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtKeyWord2" Width="300"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= model.module.Lang.getByKey("key_word") %>
                                    3
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtKeyWord3" Width="300"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= model.module.Lang.getByKey("key_word") %>
                                    4
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtKeyWord4" Width="300"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= model.module.Lang.getByKey("key_word") %>
                                    5
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtKeyWord5" Width="300"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= model.module.Lang.getByKey("key_word") %>
                                    6
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtKeyWord6" Width="300"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= model.module.Lang.getByKey("key_word") %>
                                    7
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtKeyWord7" Width="300"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= model.module.Lang.getByKey("key_word") %>
                                    8
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtKeyWord8" Width="300"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= model.module.Lang.getByKey("key_word") %>
                                    9
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtKeyWord9" Width="300"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%= model.module.Lang.getByKey("key_word") %>
                                    10
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtKeyWord10" Width="300"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="content" id="panel2-3">
                        <p>
                        </p>
                        <table class='gridtable'>
                            <tr>
                                <th>
                                    <%= model.module.Lang.getByKey("file")%>
                                </th>
                                <td>
                                    <asp:FileUpload runat="server" ID="fileUpload2" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <%= model.module.Lang.getByKey("description")%>
                                </th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtImageDesc"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button CssClass="small round button" Visible="<%# control.cmsmodules.Security.hasPermission(control.cmsmodules.PermissionName.insert_content.ToString()) %>"
                                        runat="server" ID="saveImage" OnClick="saveImage_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div id="contentImages" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    
                    
                     
               </div>
            </asp:View>
            <asp:View runat="server" ID="vwAll">
                <asp:Button CssClass="small radius button" Visible="<%# control.cmsmodules.Security.hasPermission(control.cmsmodules.PermissionName.insert_content.ToString()) %>"
                    runat="server" ID="btnNewContentDetail" OnClick="btnNewContentDetail_Click" />
                <br />
                <br />
                <img width="200" height="100" runat="server" id="imgThumb" />
                <br />
                <asp:Label runat="server" ID="lblContentInfo"></asp:Label>
                <br />
                <br />
                <h3>
                    Content Form
                </h3>
                <table class="gridtable">
                    <tr>
                        <th>
                            <%= model.module.Lang.getByKey("display_first_name")%>
                        </th>
                        <td style="width: 100px;">
                            <asp:CheckBox runat="server" ID="chkDisplayFirstName" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <%= model.module.Lang.getByKey("display_last_name")%>
                        </th>
                        <td>
                            <asp:CheckBox runat="server" ID="chkDisplayLastName" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <%= model.module.Lang.getByKey("display_email")%>
                        </th>
                        <td>
                            <asp:CheckBox runat="server" ID="chkDisplayEamil" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <%= model.module.Lang.getByKey("display_alias_name")%>
                        </th>
                        <td>
                            <asp:CheckBox runat="server" ID="chkDisplayAliasName" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <%= model.module.Lang.getByKey("display_address")%>
                        </th>
                        <td>
                            <asp:CheckBox runat="server" ID="chkDisplayAddress" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <%= model.module.Lang.getByKey("login_needed")%>
                        </th>
                        <td>
                            <asp:CheckBox runat="server" ID="chkLongNeeded" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <%= model.module.Lang.getByKey("review_needed")%>
                        </th>
                        <td>
                            <asp:CheckBox runat="server" ID="chkReviewNeeded" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <%= model.module.Lang.getByKey("text")%>
                        </th>
                        <td>
                            <asp:CheckBox runat="server" ID="chkDisplayText" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button CssClass="medium success button" Visible="<%# control.cmsmodules.Security.hasPermission(control.cmsmodules.PermissionName.insert_content.ToString()) %>"
                                runat="server" ID="btnSaveForm" Text="Save" OnClick="btnSaveForm_Click" />
                        </td>
                    </tr>
                </table>
                <h3>
                    Content Details
                </h3>
                <asp:GridView runat="server" CssClass="gridtable" ID="grdAll" AutoGenerateColumns="false"
                    OnSelectedIndexChanged="grdAll_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton Visible="<%# control.cmsmodules.Security.hasPermission(control.cmsmodules.PermissionName.update_content.ToString()) %>"
                                    runat="server" ID="lnkEdit" CommandArgument='<%# Eval("_cdid") %>' Text="Edit"
                                    OnClick="lnkEdit_Click"></asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton runat="server" ID="lnkDelete" Visible="<%# control.cmsmodules.Security.hasPermission(control.cmsmodules.PermissionName.delete_content.ToString()) %>"
                                    CommandArgument='<%# Eval("_cdid") %>' Text="Delete" OnClick="lnkDelete_Click"
                                    CommandName="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="language" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <div runat="server" style="width: 100px;" id="div_language">
                                    <%# Eval("__rlang")%>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="title" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <div runat="server" style="width: 450px;" id="div_title">
                                    <%# Eval("title")%>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="text">
                                <ItemTemplate>
                                    <div runat="server" style="width: 50%; " id="div_text">
                                        <%# trimHTML(Eval("text")) %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </asp:View>
            <asp:View runat="server" ID="vwNoPermission">
                <asp:Label runat="server" ID="lblNoPer" Style='color: Red; font-size: 16px; font-weight: bold;'
                    Text="There is no permission to view this page. Call the administrator site to solve the problem"></asp:Label>
            </asp:View>
        </asp:MultiView>
    </div>
    </form>
    <script src="tab/js/vendor/jquery.js"></script>
    <script src="tab/js/foundation.min.js"></script>
    <script type="text/javascript">
        $(document).foundation();

    </script>


     <script src="datepicker/js/foundation-datepicker.js" type="text/javascript"></script>
    <link href="datepicker/css/foundation-datepicker.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        $(function () {
            $('#txtPublishDate').fdatepicker({
                format: 'yyyy-mm-dd'
            });
        });
	</script>
</body>
</html>
