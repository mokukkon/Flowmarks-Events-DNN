<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="flowmarks.Modules.Events.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td colspan="2">
            <asp:Label ID="lblVersion" runat="server" CssClass="et"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="SubHead" width="200">
            <dnn:Label ID="lblModuleIsolation" runat="server" ControlName="chkModuleIsolation"
                Suffix=":" />
        </td>
        <td>
            <asp:CheckBox ID="chkModuleIsolation" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="SubHead" width="200">
            <dnn:Label ID="lblAllowAnonymousEdits" runat="server" ControlName="chkAllowAnonymousEdits"
                Suffix=":" />
        </td>
        <td>
            <asp:CheckBox ID="chkAllowAnonymousEdits" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="SubHead" width="200">
            <dnn:label id="lblPageSize" runat="server" controlname="ddlPageSize" Suffix=":" />
        </td>
        <td>
            <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="NormalTextbox">
                <asp:ListItem Text="15" Value="15" />
                <asp:ListItem Text="25" Value="25" />
                <asp:ListItem Text="50" Value="50" />
                <asp:ListItem Text="75" Value="75" />
                <asp:ListItem Text="100" Value="100" />
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="SubHead" width="200">
            <dnn:Label ID="lblDateFormat" runat="server" ControlName="txtReportsUrl" Suffix=":" />
        </td>
        <td>
            <asp:TextBox ID="txtDateFormat" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="SubHead" width="200">
            <dnn:Label ID="lblTimeFormat" runat="server" ControlName="txtReportsUrl" Suffix=":" />
        </td>
        <td>
            <asp:TextBox ID="txtTimeFormat" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="SubHead" width="200">
            <dnn:Label ID="lblDateTimeValidationRegex" runat="server" ControlName="txtDateTimeValidationRegex" Suffix=":" />
        </td>
        <td>
            <asp:TextBox ID="txtDateTimeValidationRegex" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="SubHead" width="200">
            <dnn:Label ID="lblReportsUrl" runat="server" ControlName="txtReportsUrl" Suffix=":" />
        </td>
        <td>
            <asp:TextBox ID="txtReportsUrl" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="SubHead" width="200">
            <dnn:Label ID="lblReportsToNewWindow" runat="server" ControlName="chkReportsToNewWindow"
                Suffix=":" />
        </td>
        <td>
            <asp:CheckBox ID="chkReportsToNewWindow" runat="server" />
        </td>
    </tr>
</table>
