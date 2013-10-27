<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewEvents.ascx.cs"
    Inherits="flowmarks.Modules.Events.ViewEvents" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude runat="server" FilePath="~/DesktopModules/flowmarks_Events/jquery-ui-timepicker-addon.min.js"/>
<dnn:DnnCssInclude runat="server" FilePath="/DesktopModules/flowmarks_Events/jquery-ui-1.9.2.custom.min.css"></dnn:DnnCssInclude>

<%  if (false) { %>
<script type="text/javascript" src="jquery.js"></script>
<script type="text/javascript" src="jquery-vsdoc.js"></script>
<%  } %>

<script type="text/javascript" charset="utf-8">
    jQuery(function() {

        initialize();

    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();

    prm.add_endRequest(function() {
        // re-bind your jquery events here
        initialize();
    });

    function initialize() {

        $("#dialog").dialog({
            autoOpen: false,
            modal: true
        });

        checkLabel2();
        checkEventDate2();
        checkMeasurement2();
        checkExternalId();

        $(".ui-state-default a").hover(function() {
            $(this).parent().addClass("ui-state-hover");
        }, function() {
            $(this).parent().removeClass("ui-state-hover");
        });

        $(function() {
            $("button, input:submit, a", ".buttonwrapper").button();
        });

        $(".dateEventDate").datetimepicker({
            dateFormat: '<% =DateFormat.ToLower().Replace("yyyy", "yy") %>',
            timeFormat: '<% =TimeFormat %>',
            minuteGrid: 15,
            stepMinute: 15,
            firstDay: 1
        });

        $(".dateEventDate2").datetimepicker({
            dateFormat: '<% =DateFormat.ToLower().Replace("yyyy", "yy") %>',
            timeFormat: '<% =TimeFormat %>',
            minuteGrid: 15,
            stepMinute: 15,
            firstDay: 1
        });

        $(".edit").hide().fadeIn();

        $("#cmdLabel2").click(function(e) {
            e.preventDefault();
            $(".trLabel2").toggle();
            $("#cmdLabel2").hide();
        });

        $("#cmdHideLabel2").click(function(e) {
            e.preventDefault();
            $(".trLabel2").hide();
            $("#cmdLabel2").show();
        });

        $("#cmdEventDate2").click(function(e) {
            e.preventDefault();
            $(".trEventDate2").toggle();
            $("#cmdEventDate2").hide();
        });

        $("#cmdHideEventDate2").click(function(e) {
            e.preventDefault();
            $(".trEventDate2").hide();
            $("#cmdEventDate2").show();
        });

        $("#cmdMeasurement2").click(function(e) {
            e.preventDefault();
            $(".trMeasurement2").toggle();
            $("#cmdMeasurement2").hide();
        });

        $("#cmdHideMeasurement2").click(function(e) {
            e.preventDefault();
            $(".trMeasurement2").hide();
            $("#cmdMeasurement2").show();
        });

        $("#cmdExternalId").click(function(e) {
            e.preventDefault();
            $(".trExternalId").toggle();
            $("#cmdExternalId").hide();
        });

        $("#cmdHideExternalId").click(function(e) {
            e.preventDefault();
            $(".trExternalId").hide();
            $("#cmdExternalId").show();
        });

        $(".saveButton").click(function(e) {
            e.preventDefault();
            var targetUrl = $(this).attr("href");
            window.location.href = "#top";                        
            window.location.href = targetUrl;
        });       

        $(".deleteButton").click(function(e) {
            e.preventDefault();
            var targetUrl = $(this).attr("href");

            $("#dialog").dialog({
                buttons: {
                    "Yes": function() {
                       $(this).dialog("close");
                       window.location.href = "#top";
                        
                        window.location.href = targetUrl;
                    },
                    "No": function() {
                        $(this).dialog("close");
                    }
                }
            });

            $("#dialog").dialog("open");
        });

    }


    function checkLabel2() {

        if ($(".txtLabel2").length) {
            if ($(".txtLabel2").val().length > 0) {
                $(".trLabel2").show();
                $("#cmdLabel2").hide();
            }
            else {
                $(".trLabel2").hide();
            }
        }

    }

    function checkEventDate2() {
        if ($(".dateEventDate2").length) {
            if ($(".dateEventDate2").val().length > 0) {
                $(".trEventDate2").show();
                $("#cmdEventDate2").hide();
            }
            else {
                $(".trEventDate2").hide();
            }
        }
    }

    function checkMeasurement2() {
        if ($(".txtMeasurement2").length) {
            if ($(".txtMeasurement2").val().length > 0) {
                $(".trMeasurement2").show();
                $("#cmdMeasurement2").hide();
            }
            else {
                $(".trMeasurement2").hide();
            }
        }
    }

    function checkExternalId() {
        if ($(".txtExternalId").length) {
            if ($(".txtExternalId").val().length > 0) {
                $(".trExternalId").show();
                $("#cmdExternalId").hide();
            }
            else {
                $(".trExternalId").hide();
            }
        }
    }

</script>

<a name="top"></a>
<div id="dialog" title="Delete event">
    Are you sure you want to delete this event?
</div>
<div id="_emn">
    <table class="topnavi" cellpadding="0" cellspacing="0" width="100%" style="background-color: White">
        <col style="width: 200px;" />
        <col style="width: 100%; text-align: right;" />
        <tr>
            <td class="left" nowrap>
                <asp:HyperLink ID="lnkEvents" runat="server" class="large selected" Text="Events"></asp:HyperLink>
                |
                <asp:HyperLink ID="lnkSettings" runat="server" class="large" Text="Categories" ToolTip="Edit categories"></asp:HyperLink>
                <asp:Literal ID="litReportSeparator" runat="server" Text=" | "></asp:Literal>
                <asp:HyperLink ID="lnkReport" runat="server" class="large" Text="Reports" ToolTip="View reports"></asp:HyperLink>
            </td>
            <td class="center">
                <div id="MessageBox" runat="server" class="MessageBox" EnableViewState="false">
                    <asp:Label ID="lblMessage" runat="server" ></asp:Label>
                </div>
                <asp:Label ID="litCurrentDateTime" runat="server" class="small" Visible="false"></asp:Label>
            </td>
            <td class="right" nowrap>
                <asp:LinkButton ID="cmdAdd" runat="server" class="large" Text="New Event" OnClick="cmdAdd_Click"
                    ToolTip="New Event"></asp:LinkButton>
                
                <asp:DropDownList ID="ddlFilterCategory" runat="server" Width="150px" DataSourceID="dsFilterCategories"
                    DataTextField="Name" DataValueField="CategoryId" AutoPostBack="true" OnSelectedIndexChanged="ddlFilterCategory_SelectedIndexChanged" />
                <asp:LinkButton ID="cmdQuickAdd" runat="server" class="small" Text="Quick Add" OnClick="cmdQuickAdd_Click"
                    Visible="false"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <asp:LinkButton ID="add" class="addArea" runat="server" ToolTip="New Event" OnClick="cmdAdd_Click"></asp:LinkButton>
    <div class="event old">
        <asp:ListView ID="lstContent" datakeyfield="EventId" runat="server" Style="width: 100%;"
            OnItemDataBound="lstContent_ItemDataBound" 
            OnItemCommand="lstContent_ItemCommand"
            OnItemCanceling="lstContent_ItemCanceling"
            OnItemInserting="lstContent_ItemInserting"
            OnItemEditing="lstContent_ItemEditing" 
            OnItemUpdating="lstContent_ItemUpdating"
            OnItemDeleting="lstContent_ItemDeleting"
            OnItemCreated="lstContent_ItemCreated"
            OnPreRender="lstContent_PreRender">
            <LayoutTemplate>
                <div class="listcontainer">
                    <table id="eventList" cellspacing="0" cellpadding="0" width="100%">
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                        <tr id="trFooter" runat="server">
                        </tr>
                    </table>
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <%# AddGroupingRowForFutureAndPast() %>
                <tr>
                    <td>
                        <asp:LinkButton runat="server" ID="cmdEditRow" CommandName="Edit" CssClass="editArea" ToolTip='<% # ToolTip(Container.DataItem) %>'>
                            <asp:Literal ID="eventBlock" runat="server"></asp:Literal>
                            <span class="small"><span id="cmdEdit" href="#edit" title="Edit event" class="linkButton">
                                edit</span> </span>
                        </asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <InsertItemTemplate>
                <tr>
                    <th>
                        <%= Localization.GetString("NewItemHeader", LocalResourceFile) %>
                    </th>
                </tr>
                <tr class="edit">
                    <td style="padding-top: 10px;">
                        <table border="0" cellspacing="0" cellpadding="0" runat="server" id="tblInsertEvent"
                            visible="true">
                            <tr runat="server" id="trCategory">
                                <td class="tdLabel top">
                                    <asp:Label ID="lblCategory" runat="server" AssociatedControlID="ddlCategory" Text='Category'>
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <div class="fmCategories">
                                        <div class="fmColumn">
                                        </div>
                                        <div class="fmLabel">
                                            <asp:Label ID="lblRootCategory" runat="server" AssociatedControlID="ddlRootCategory"
                                                Text="Top">
                                            </asp:Label>
                                        </div>
                                        <div class="fmCell">
                                            <asp:DropDownList ID="ddlRootCategory" runat="server" CssClass="fmDropDownList" DataSourceID="dsRootCategories"
                                                DataTextField="Name" DataValueField="CategoryId" AutoPostBack="true" OnSelectedIndexChanged="ddlRootCategory_SelectedIndexChanged"
                                                Width="282px" />
                                        </div>
                                    </div>
                                    <div class="fmCategories fmSpacing">
                                        <div class="fmColumn">
                                        </div>
                                        <div class="fmLabel">
                                            <asp:Label ID="lblSubCategory" runat="server" AssociatedControlID="ddlCategory" Text="Sub">
                                            </asp:Label>
                                        </div>
                                        <div class="fmCell">
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="fmDropDownList" DataSourceID="dsActiveCategories"
                                                DataTextField="Name" DataValueField="CategoryId" Width="282px" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr runat="server" id="trEventDate" class="trEventDate">
                                <td class="tdLabel">
                                    <asp:Label ID="lblEventDate" runat="server" AssociatedControlID="dateEventDate">
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="dateEventDate" CssClass="dateEventDate fmTextBox" runat="server"/>
                                    <div id="cmdEventDate2" class="ui-state-default ui-corner-all ui-icon ui-icon-circle-plus fmImageButton">
                                        <a href="#2ndDate" title="Add date">+</a>
                                    </div>
                                    <asp:RequiredFieldValidator ID="valdateEventDateReq" runat="server" ControlToValidate="dateEventDate"
                                        ErrorMessage="Required" CssClass="red" Width="0" ValidationGroup="ICGEvent"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="valdateEventDateRegex" runat="server" ControlToValidate="dateEventDate"
                                        CssClass="red" ValidationGroup="ICGEvent"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr runat="server" id="trEventDate2" class="trEventDate2">
                                <td class="tdLabel">
                                    <asp:Label ID="lblEventDate2" runat="server" AssociatedControlID="dateEventDate2">
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="dateEventDate2" CssClass="dateEventDate2 fmTextBox" runat="server" />
                                    <div id="cmdHideEventDate2" class="ui-state-default ui-state-active ui-corner-all fmImageButton">
                                        <div class="ui-icon ui-icon-circle-minus">
                                            <a href="#hide" title="Hide">-</a>
                                        </div>
                                    </div>
                                    
                                    <asp:RegularExpressionValidator ID="valdateEventDate2Regex" runat="server" ControlToValidate="dateEventDate2"
                                        CssClass="red" ValidationGroup="ICGEvent">
                                    </asp:RegularExpressionValidator>                                
                                </td>
                            </tr>
                            <tr id="trName" runat="server">
                                <td class="tdLabel">
                                    <asp:Label ID="lblLabel" runat="server" AssociatedControlID="txtLabel">
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="txtLabel" runat="server" CssClass="fmTextBox" MaxLength="200" Width="300px" />
                                    <div id="cmdLabel2" class="ui-state-default ui-corner-all ui-icon ui-icon-circle-plus fmImageButton">
                                        <a href="#2ndLabel" title="Add label">+</a>
                                    </div>
                                </td>
                            </tr>
                            <tr runat="server" id="trLabel2" class="trLabel2">
                                <td class="tdLabel">
                                    <asp:Label ID="lblLabel2" runat="server" AssociatedControlID="txtLabel2">
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="txtLabel2" CssClass="txtLabel2 fmTextBox" runat="server"
                                        MaxLength="200" Width="300px"></asp:TextBox>
                                    <div id="cmdHideLabel2" class="ui-state-default ui-state-active ui-corner-all fmImageButton">
                                        <div class="ui-icon ui-icon-circle-minus">
                                            <a href="#hide" title="Hide">-</a>
                                        </div>
                                    </div>
                                </td>
                            </tr>                            
                            <tr runat="server" id="trMeasurement">
                                <td class="tdLabel">
                                    <asp:Label ID="lblMeasurement" runat="server" AssociatedControlID="txtMeasurement">
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="txtMeasurement" runat="server" CssClass="fmTextBox" MaxLength="255"
                                        Placeholder="Number, e.g. 47"></asp:TextBox>
                                    <div id="cmdMeasurement2" class="ui-state-default ui-corner-all ui-icon ui-icon-circle-plus fmImageButton">
                                        <a href="#2ndMeasurement" title="Add measurement">+</a>
                                    </div>
                                    <asp:RegularExpressionValidator ID="valMeasurementValid" runat="server" ControlToValidate="txtMeasurement"
                                        Text="Number required" CssClass="red" ValidationExpression="^(\d|-)?(\d|,)*\.?\d*$" ValidationGroup="ICGEvent"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr runat="server" id="trMeasurement2" class="trMeasurement2">
                                <td class="tdLabel" nowrap>
                                    <asp:Label ID="lblMeasurement2" runat="server" AssociatedControlID="txtMeasurement2"
                                        controlname="txtMeasurement2">
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="txtMeasurement2" CssClass="txtMeasurement2 fmTextBox"
                                        runat="server" MaxLength="255"></asp:TextBox>
                                    <div id="cmdHideMeasurement2" class="ui-state-default ui-state-active ui-corner-all fmImageButton">
                                        <div class="ui-icon ui-icon-circle-minus">
                                            <a href="#hide" title="Hide">-</a>
                                        </div>
                                    </div>
                                    <asp:RegularExpressionValidator ID="valnMeasurement2" runat="server" ControlToValidate="txtMeasurement2"
                                        Text="Number required" CssClass="red" ValidationExpression="^(\d|-)?(\d|,)*\.?\d*$" ValidationGroup="ICGEvent"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel top">
                                    <asp:Label ID="lblComments" runat="server" AssociatedControlID="txtComments">
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="txtComments" runat="server" CssClass="fmTextBox" Height="100px"
                                        Width="300px" TextMode="multiline"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trEventId" runat="server" class="trEventId">
                                <td class="tdLabel">
                                    <asp:Label ID="lblEventId" runat="server" AssociatedControlID="lblEventIdValue">
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:Label ID="lblEventIdValue" runat="server"></asp:Label>
                                    <div id="cmdExternalId" class="ui-state-default ui-corner-all ui-icon ui-icon-circle-plus fmImageButton">
                                        <a href="#2ndIdentifier" title="Add ID">+</a>
                                    </div>
                                    <div style="float: right; font-size: 11px; font-style: italic; margin-right: 50px;">
                                        <asp:Label ID="lblDateLastModified" runat="server">
                                        </asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr id="trExternalId" class="trExternalId">
                                <td class="tdLabel">
                                    <asp:Label ID="lblExternalId" runat="server" AssociatedControlID="txtExternalId">
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="txtExternalId" CssClass="txtExternalId fmTextBox" runat="server"
                                        MaxLength="50"></asp:TextBox>
                                    <div id="cmdHideExternalId" class="ui-state-default ui-state-active ui-corner-all fmImageButton">
                                        <div class="ui-icon ui-icon-circle-minus">
                                            <a href="#hide" title="Hide">-</a>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="tdPadding">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div class="buttonwrapper">
                                        <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Insert" CausesValidation="true" ValidationGroup="ICGEvent" CssClass="ovalbutton"><span>Save</span></asp:LinkButton>
                                        <span class="buttonseparator" />
                                        <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" CssClass="ovalbutton"><span>Cancel</span></asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </InsertItemTemplate>
            <EditItemTemplate>
                <%# AddGroupingRowForFutureAndPast() %>
                <tr class="edit">
                    <td class="tdPadding">
                        <asp:Literal ID="eventBlock" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr class="edit">
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" runat="server" id="tblEditEvent"
                            visible="true">
                            <tr runat="server" id="trCategory">
                                <td class="tdLabel top">
                                    <asp:Label ID="lblCategory" runat="server" AssociatedControlID="ddlCategory" Text='Category'>
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <div class="fmCategories">
                                        <div class="fmColumn">
                                        </div>
                                        <div class="fmLabel">
                                            <asp:Label ID="lblRootCategory" runat="server" AssociatedControlID="ddlRootCategory"
                                                Text="Top">
                                            </asp:Label>
                                        </div>
                                        <div class="fmCell">
                                            <asp:DropDownList ID="ddlRootCategory" runat="server" CssClass="fmDropDownList" DataSourceID="dsRootCategories"
                                                DataTextField="Name" DataValueField="CategoryId" AutoPostBack="true" OnSelectedIndexChanged="ddlRootCategory_SelectedIndexChanged"
                                                Width="282px" />
                                        </div>
                                    </div>
                                    <div class="fmCategories fmSpacing">
                                        <div class="fmColumn">
                                        </div>
                                        <div class="fmLabel">
                                            <asp:Label ID="lblSubCategory" runat="server" AssociatedControlID="ddlCategory" Text="Sub">
                                            </asp:Label>
                                        </div>
                                        <div class="fmCell">
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="fmDropDownList" DataSourceID="dsActiveCategories"
                                                DataTextField="Name" DataValueField="CategoryId" Width="282px" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr runat="server" id="trEventDate" class="trEventDate">
                                <td class="tdLabel">
                                    <asp:Label ID="lblEventDate" runat="server" AssociatedControlID="dateEventDate" Text='<% # GetLabel(Eval("Label_EventDate"),"lblEventDate") %>'>
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="dateEventDate" CssClass="dateEventDate fmTextBox" runat="server"
                                        Text='<% # ConvertFromUtcToUserTimeZone(Eval("EventDate")).ToString(DateTimeFormat)%>' />
                                    <div id="cmdEventDate2" class="ui-state-default ui-corner-all ui-icon ui-icon-circle-plus fmImageButton">
                                        <a href="#2ndDate" title="Another date...">+</a>
                                    </div>
                                    <asp:RequiredFieldValidator ID="valdateEventDateReq" runat="server" ControlToValidate="dateEventDate"
                                        Text="Required" CssClass="red" Width="0" ValidationGroup="ICGEvent"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="valdateEventDateRegex" runat="server" ControlToValidate="dateEventDate"
                                        CssClass="red" ValidationGroup="ICGEvent"></asp:RegularExpressionValidator>

                                </td>
                            </tr>
                            <tr runat="server" id="trEventDate2" class="trEventDate2">
                                <td class="tdLabel">
                                    <asp:Label ID="lblEventDate2" runat="server" AssociatedControlID="dateEventDate2"
                                        suffix=":" Text='<% # GetLabel(Eval("Label_EventDate2"),"lblEventDate2") %>'>
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="dateEventDate2" CssClass="dateEventDate2 fmTextBox" runat="server"
                                        Text='<% # Eval("EventDate2") != null ? ConvertFromUtcToUserTimeZone(Eval("EventDate2")).ToString(DateTimeFormat) : "" %>' />
                                    <div id="cmdHideEventDate2" class="ui-state-default ui-state-active ui-corner-all fmImageButton">
                                        <div class="ui-icon ui-icon-circle-minus">
                                            <a href="#hide" title="Hide">-</a>
                                        </div>
                                    </div>
                                    <asp:RegularExpressionValidator ID="valdateEventDate2Regex" runat="server" ControlToValidate="dateEventDate2"
                                        CssClass="red" ValidationGroup="ICGEvent"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr id="trName" runat="server">
                                <td class="tdLabel">
                                    <asp:Label ID="lblLabel" runat="server" AssociatedControlID="txtLabel" Text='<% # GetLabel(Eval("Label_Label"),"lblLabel") %>'>
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="txtLabel" runat="server" CssClass="fmTextBox" MaxLength="200" Width="300px"
                                        Text='<% #Bind("Label")%>' />
                                    <div id="cmdLabel2" class="ui-state-default ui-corner-all ui-icon ui-icon-circle-plus fmImageButton">
                                        <a href="#2ndLabel" title="Another label...">+</a>
                                    </div>
                                </td>
                            </tr>
                            <tr runat="server" id="trLabel2" class="trLabel2">
                                <td class="tdLabel">
                                    <asp:Label ID="lblLabel2" runat="server" AssociatedControlID="txtLabel2"
                                        Text='<% # GetLabel(Eval("Label_Label2"),"lblLabel2") %>'>
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="txtLabel2" CssClass="txtLabel2 fmTextBox" runat="server"
                                        MaxLength="200" Width="300px" Text='<% #Bind("Label2")%>'></asp:TextBox>
                                    <div id="cmdHideLabel2" class="ui-state-default ui-state-active ui-corner-all fmImageButton">
                                        <div class="ui-icon ui-icon-circle-minus">
                                            <a href="#hide" title="Hide">-</a>
                                        </div>
                                    </div>
                                </td>
                            </tr>                            
                            <tr runat="server" id="trMeasurement">
                                <td class="tdLabel">
                                    <asp:Label ID="lblMeasurement" runat="server" AssociatedControlID="txtMeasurement"
                                        Text='<% # GetLabel(Eval("Label_Measurement"),"lblMeasurement") %>'>
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="txtMeasurement" runat="server" CssClass="fmTextBox" MaxLength="255"
                                        Placeholder="Number, e.g. 47" Text='<% #Bind("Measurement")%>'></asp:TextBox>
                                    <div id="cmdMeasurement2" class="ui-state-default ui-corner-all ui-icon ui-icon-circle-plus fmImageButton">
                                        <a href="#2ndMeasurement" title="Another measurement...">+</a>
                                    </div>
                                    <asp:RegularExpressionValidator ID="valWebsiteValid" runat="server" ControlToValidate="txtMeasurement"
                                        Text="Number required" CssClass="red" ValidationExpression="^(\d|-)?(\d|,)*\.?\d*$"
                                        ValidationGroup="ICGEvent"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr runat="server" id="trMeasurement2" class="trMeasurement2">
                                <td class="tdLabel" nowrap>
                                    <asp:Label ID="lblMeasurement2" runat="server" AssociatedControlID="txtMeasurement2"
                                        controlname="txtMeasurement2" Text='<% # GetLabel(Eval("Label_Measurement2"),"lblMeasurement2") %>'
                                        suffix=":">
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="txtMeasurement2" CssClass="txtMeasurement2 fmTextBox"
                                        runat="server" MaxLength="255" Text='<% #Bind("Measurement2")%>'></asp:TextBox>
                                    <div id="cmdHideMeasurement2" class="ui-state-default ui-state-active ui-corner-all fmImageButton">
                                        <div class="ui-icon ui-icon-circle-minus">
                                            <a href="#hide" title="Hide">-</a>
                                        </div>
                                    </div>
                                    <asp:RegularExpressionValidator ID="valnMeasurement2" runat="server" ControlToValidate="txtMeasurement2"
                                        Text="Number required" CssClass="red" ValidationExpression="^(\d|-)?(\d|,)*\.?\d*$"
                                        ValidationGroup="ICGEvent"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLabel top">
                                    <asp:Label ID="lblComments" runat="server" AssociatedControlID="txtComments" Text='<% # GetLabel(Eval("Label_Comments"),"lblComments") %>'>
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="txtComments" runat="server" CssClass="fmTextBox" Height="100px"
                                        Width="300px" TextMode="multiline" Text='<% #Bind("Comments")%>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trEventId" runat="server" class="trEventId">
                                <td class="tdLabel">
                                    <asp:Label ID="lblEventId" runat="server" AssociatedControlID="lblEventIdValue" Text='ID'>
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:Label ID="lblEventIdValue" runat="server" Text='<% #Eval("EventId")%>'></asp:Label>
                                    <div id="cmdExternalId" class="ui-state-default ui-corner-all ui-icon ui-icon-circle-plus fmImageButton">
                                        <a href="#2ndIdentifier" title="Another ID...">+</a>
                                    </div>
                                    <div style="float: right; font-size: 11px; font-style: italic; margin-right: 50px;">
                                        <asp:Label ID="lblDateLastModified" runat="server" ToolTip='<% # ConvertFromUtcToUserTimeZone(GetLastDate(Eval("DateCreated"),Eval("DateModified"))).ToString(DateTimeFormat) %>'
                                            Text='<% # "edited " + formatTimeSpan(GetLastDate(Eval("DateCreated"),Eval("DateModified"))) + " ago"%>'>
                                        </asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr id="trExternalId" class="trExternalId">
                                <td class="tdLabel">
                                    <asp:Label ID="lblExternalId" runat="server" AssociatedControlID="txtExternalId"
                                        Text='<% # GetLabel(Eval("Label_ExternalId"),"lblExternalId") %>'>
                                    </asp:Label>
                                </td>
                                <td class="tdField">
                                    <asp:TextBox ID="txtExternalId" CssClass="txtExternalId fmTextBox" runat="server"
                                        MaxLength="50" Text='<% #Bind("ExternalId")%>'></asp:TextBox>
                                    <div id="cmdHideExternalId" class="ui-state-default ui-state-active ui-corner-all fmImageButton">
                                        <div class="ui-icon ui-icon-circle-minus">
                                            <a href="#hide" title="Hide">-</a>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="tdPadding">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div class="buttonwrapper">
                                        <asp:LinkButton ID="UpdateButton" runat="server" CommandArgument='<%# Bind("EventID")%>'
                                            CommandName="Update" CssClass="saveButton" CausesValidation="true" ValidationGroup="ICGEvent" ><span>Save</span></asp:LinkButton>
                                        <span class="buttonseparator" />
                                        <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" CssClass="cancelButton"><span>Cancel</span></asp:LinkButton>
                                        <asp:LinkButton ID="RemoveButton" runat="server" CommandArgument='<%# Bind("EventID")%>'
                                            CommandName="Delete" Text="Delete" class="deleteButton" Style="float: right; margin-right: 50px;"></asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <div class="listcontainer">
                    <table id="eventList" cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <th>
                                <%= Localization.GetString("EmptyItemHeader", LocalResourceFile)%>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <%= Localization.GetString("EmptyItemBlock", LocalResourceFile)%>
                                <span class="small">
                                    <asp:LinkButton ID="cmdAdd" runat="server" CommandName="Add" Text="New Event" OnClick="cmdAdd_Click"></asp:LinkButton>
                                </span>
                            </td>
                        </tr>
                        <tr id="trFooter" runat="server">
                        </tr>
                    </table>
                </div>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <dnn:PagingControl id="dnnPager" runat="server" CssClass="pager" Visible="false"
        CSSClassLinkInactive="inactive" />
    <asp:Panel ID="pnlNoRecords" Visible="false" runat="server" CssClass="listcontainer">
        <asp:Label ID="litNoRecordsMessage" runat="server" ></asp:Label>
    </asp:Panel>
    <asp:SqlDataSource ID="dsRootCategories" runat="server" DataSourceMode="DataReader"
        ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>" SelectCommand="SELECT -1 AS CategoryID, 'All Categories' AS Name UNION SELECT c.CategoryID, c.Name FROM flowmarks_Category c WHERE c.ParentID IS NULL AND (NULLIF(c.UserID, 0) IS NULL OR c.UserID = @UserID) AND EXISTS (SELECT 1 FROM flowmarks_Category c2 WHERE c2.ParentID = c.CategoryID) AND c.IsDeleted = 0 AND c.IsHidden = 0">
        <SelectParameters>
            <asp:Parameter Name="UserID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsCategories" runat="server" DataSourceMode="DataReader" ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>"
        SelectCommand="SELECT CategoryID, Name FROM flowmarks_Category WHERE (NULLIF(@ParentID, -1) IS NULL OR ParentID = @ParentID) AND (NULLIF(UserID, 0) IS NULL OR UserID = @UserID) ">
        <SelectParameters>
            <asp:Parameter Name="ParentID" Type="Int32" DefaultValue="-1" />
            <asp:Parameter Name="UserID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsActiveCategories" runat="server" DataSourceMode="DataReader"
        ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>" SelectCommand="SELECT CategoryID, Name FROM flowmarks_Category WHERE (NULLIF(@ParentID, -1) IS NULL OR ParentID = @ParentID) AND (NULLIF(UserID, 0) IS NULL OR UserID = @UserID) AND IsDeleted = 0 AND IsHidden = 0 ">
        <SelectParameters>
            <asp:Parameter Name="ParentID" Type="Int32" DefaultValue="-1" />
            <asp:Parameter Name="UserID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="dsFilterCategories" runat="server" DataSourceMode="DataReader"
        ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>" SelectCommand="SELECT NULL AS CategoryID, 'All Categories' As Name UNION SELECT CategoryID, Name FROM flowmarks_Category WHERE ParentID IS NULL AND (NULLIF(UserID, 0) IS NULL OR UserID = @UserID) AND IsDeleted = 0 AND IsHidden = 0 ">
        <SelectParameters>
            <asp:Parameter Name="ParentID" Type="Int32" DefaultValue="-1" />
            <asp:Parameter Name="UserID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</div>