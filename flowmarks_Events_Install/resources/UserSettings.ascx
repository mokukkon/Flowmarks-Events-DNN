<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserSettings.ascx.cs"
    Inherits="flowmarks.Modules.Events.UserSettings" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.3, Version=9.3.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.3, Version=9.3.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.3, Version=9.3.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.3, Version=9.3.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dxw" %>
<div id="settings">
    <div class="container">
        <table class="topnavi" cellpadding="0" cellspacing="0" width="100%" style="background-color: White; margin-bottom: 8px;">
            <col style="width: 200px;" />
            <col style="width: 100%; text-align: right;" />
            <tr>
                <td class="left" nowrap>
                    <asp:HyperLink ID="lnkEvents" runat="server" class="large" Text="Events" ToolTip="Edit events"></asp:HyperLink>
                    |
                    <asp:HyperLink ID="lnkSettings" runat="server" class="large selected" Text="Categories" ToolTip="Edit categories"></asp:HyperLink>
                    <asp:Literal ID="litReportSeparator" runat="server" Text=" | "></asp:Literal>
                    <asp:HyperLink ID="lnkReport" runat="server" class="large" Text="Reports" ToolTip="View reports"></asp:HyperLink>
                </td>
                <td class="center">
                    <div id="MessageBox" runat="server" class="MessageBox">
                        <asp:Label ID="lblMessage" runat="server" class="large"></asp:Label>
                    </div>
                    <asp:Label ID="litCurrentDateTime" runat="server" class="large" Visible="false"></asp:Label>
                </td>
                <td class="right">
                </td>
            </tr>
        </table>
        <div class="bd">
<%--            <p>
                <asp:HyperLink ID="hlnkNewReport" runat="server" CssClass="cmdLink" Text="New category"></asp:HyperLink>
            </p>--%>
            <dxwgv:ASPxGridView ID="gvCategorySettings" runat="server" AutoGenerateColumns="False" EnableTheming="false"
                Width="100%" KeyFieldName="CategoryId" SettingsPager-AllButton-Visible="true"
                SettingsPager-PageSize="100" 
                OnRowUpdating="gvCategorySettings_RowUpdating" 
                OnRowDeleting="gvCategorySettings_RowDeleting"
                OnRowInserting="gvCategorySettings_RowInserting" 
                EnableCallbackCompression="false" >
                <Styles >
                <Header CssClass="hd" HorizontalAlign="Left" >
 
                </Header>
                </Styles>
                <Templates>
                    <EditForm>
                        <div style="padding: 4px 4px 3px 4px">
                            <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="100%">
                                <TabPages>
                                    <dxtc:TabPage Text="Category" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl runat="server">
                                                <dxwgv:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors"
                                                    runat="server">
                                                </dxwgv:ASPxGridViewTemplateReplacement>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Labels" Visible='False'>
                                        <ContentCollection>
                                            <dxw:ContentControl runat="server">
                                                <dxe:ASPxLabel runat="server" ID="nameLabel" Text="Name">
                                                </dxe:ASPxLabel>
                                                <dxe:ASPxTextBox runat="server" ID="nameLabelEditor" Text='<%# Eval("Label_Label")%>'
                                                    NullText="Label for event name, e.g. 'Course Name'">
                                                </dxe:ASPxTextBox>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                </TabPages>
                            </dxtc:ASPxPageControl>
                        </div>
                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                            <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" 
                                runat="server">
                            </dxwgv:ASPxGridViewTemplateReplacement>
                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                runat="server">
                            </dxwgv:ASPxGridViewTemplateReplacement>
                        </div>
                    </EditForm>
                </Templates>
                <Columns>
                    <dxwgv:GridViewDataTextColumn EditFormSettings-VisibleIndex="1" EditFormSettings-CaptionLocation="Top"
                        Name="Name" FieldName="Name" Caption="Category Name" PropertiesTextEdit-NullText="e.g. 'Computer Science courses'" />
                    <dxwgv:GridViewDataTextColumn EditFormSettings-VisibleIndex="2" EditFormSettings-CaptionLocation="Top"
                        Name="Label_Label" FieldName="Label_Label" Caption="Name-label" PropertiesTextEdit-NullText="Label for event name, e.g. 'Course Name'"
                        Visible="false" EditFormSettings-Visible="False" />
                    <dxwgv:GridViewDataTextColumn EditFormSettings-VisibleIndex="3" EditFormSettings-CaptionLocation="Top"
                        Name="Label_EventDate" FieldName="Label_EventDate" Caption="EventDate-label"
                        PropertiesTextEdit-NullText="Label for event date, e.g. 'Date Completed'" Visible="false"
                        EditFormSettings-Visible='False' />
                    <dxwgv:GridViewDataTextColumn EditFormSettings-VisibleIndex="4" EditFormSettings-CaptionLocation="Top"
                        Name="Label_Measurement" FieldName="Label_Measurement" Caption="Measurement-label"
                        PropertiesTextEdit-NullText="Label for default numeric variable, eg. 'Credits'"
                        Visible="false" EditFormSettings-Visible="False" />
                    <dxwgv:GridViewDataTextColumn EditFormSettings-VisibleIndex="5" EditFormSettings-CaptionLocation="Top"
                        Name="Label_Measurement2" FieldName="Label_Measurement2" Caption="Optional measurement -label"
                        PropertiesTextEdit-NullText="Label for 2nd. numeric variable, eg. 'Grading'"
                        Visible="false" EditFormSettings-Visible="False" />
                    <dxwgv:GridViewDataTextColumn EditFormSettings-VisibleIndex="6" EditFormSettings-CaptionLocation="Top"
                        Name="Label_Label2" FieldName="Label_Label2" Caption="Optional text -label"
                        PropertiesTextEdit-NullText="Label for optional text, e.g. 'Instructor'" Visible="false"
                        EditFormSettings-Visible="False" />
                    <dxwgv:GridViewDataTextColumn EditFormSettings-VisibleIndex="7" EditFormSettings-CaptionLocation="Top"
                        Name="Label_ExternalId" FieldName="Label_ExternalId" Caption="External ID -label"
                        PropertiesTextEdit-NullText="Label for External ID, eg. 'Course ID'" Visible="false"
                        EditFormSettings-Visible="False" />
                    <dxwgv:GridViewDataDateColumn EditFormSettings-VisibleIndex="8" Name="DateCreated"
                        Caption="Date Created" FieldName="DateCreated" EditFormSettings-Visible="False"
                        PropertiesDateEdit-DisplayFormatString="dd.MM.yyyy HH:mm" />
                    <dxwgv:GridViewDataDateColumn EditFormSettings-VisibleIndex="9" Name="DateModified"
                        FieldName="DateModified" Caption="Date Modified" EditFormSettings-Visible="False"
                        PropertiesDateEdit-DisplayFormatString="dd.MM.yyyy HH:mm" />
                    <dxwgv:GridViewDataMemoColumn EditFormSettings-CaptionLocation="Top" Name="Comments"
                        FieldName="Comments" Caption="Comments" Visible="false">
                        <EditFormSettings RowSpan="4" Visible="True" VisibleIndex="100" />
                    </dxwgv:GridViewDataMemoColumn>
                    <dxwgv:GridViewDataCheckColumn EditFormSettings-VisibleIndex="1" EditCellStyle-VerticalAlign="Bottom"
                        EditFormCaptionStyle-VerticalAlign="Bottom" PropertiesCheckEdit-Style-VerticalAlign="Bottom"
                        EditFormCaptionStyle-Wrap="False" Name="IsHidden" Caption="Hidden" ToolTip="Hide category and events belonging to it from the event list" FieldName="IsHidden"
                        EditFormSettings-Visible="True" />                       
                    <dxwgv:GridViewDataComboBoxColumn EditFormSettings-VisibleIndex="100" EditFormSettings-CaptionLocation="Top"
                        Name="ParentId" FieldName="ParentId" Caption="Top Category">
                        <PropertiesComboBox TextField="Name" ValueField="CategoryID" EnableSynchronization="False"
                            DataSourceID="dsRootCategories">
                            <ClientSideEvents SelectedIndexChanged="function(s, e) { OnCountryChanged(s); }">
                            </ClientSideEvents>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewCommandColumn Caption="&nbsp;">
                        <EditButton Visible="True" />
                        <NewButton Visible="True" />
                        <DeleteButton Visible="true">
                        </DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </div>
</div>
<asp:SqlDataSource ID="dsRootCategories" runat="server" DataSourceMode="DataReader"
    ConnectionString="<%$ ConnectionStrings:SiteSqlServer %>" SelectCommand="SELECT NULLIF(CategoryID, -1) AS CategoryID, Name FROM flowmarks_Category WHERE ParentID IS NULL AND (NULLIF(UserID, -1) IS NULL OR UserID = @UserID)">
    <SelectParameters>
        <asp:Parameter Name="UserID" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
