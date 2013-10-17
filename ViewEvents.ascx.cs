/*
 * Copyright (c) 2011 Mika Kukkonen (http://www.flowmarks.com)
 * Copyright Contact: flowmarks@gmail.com
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to deal 
 * in the Software without restriction, including without limitation the rights to use, 
 * copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial 
 * portions of the Software. 
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT 
 * NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Linq;

//using DotNetNuke;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

//Using 
using flowmarks.Modules.Events.Components;
using DevExpress.Web.ASPxEditors;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace flowmarks.Modules.Events
{
    /// <summary>
    /// Display and edit Events
    /// </summary>
    partial class ViewEvents : PortalModuleBase, IActionable
    {
        private const int NoCategoryId = -1;
        private const int AnonymousUserId = -1;
        private const int EveryUserId = 0;

        /// <summary>
        /// DotNetNuke Event logger
        /// </summary>
        public DotNetNuke.Services.Log.EventLog.EventLogController EventLogger = new DotNetNuke.Services.Log.EventLog.EventLogController();

        #region Properties

        /// <summary>
        /// Gets or sets the selected category in the dropdownlist.
        /// </summary>
        /// <value>
        /// The selected category.
        /// </value>
        protected int? SelectedCategory
        {
            get
            {
                int result;
                Int32.TryParse(Request.Params["category"], out result);
                if (result == 0)
                    if (ddlFilterCategory.SelectedItem != null && !String.IsNullOrEmpty(ddlFilterCategory.SelectedItem.Value))
                    {
                        Int32.TryParse(ddlFilterCategory.SelectedItem.Value, out result);
                        return result;
                    }
                    else
                        return null;
                else
                    ddlFilterCategory.SelectedValue = result.ToString();
                ddlFilterCategory.DataBind();
                return result;
            }
            set
            {
                ddlFilterCategory.SelectedItem.Value = value.ToString();
                ddlFilterCategory.DataBind();
            }

        }

        /// <summary>
        /// Get the skin source from active tab and format as a querystring parameter
        /// </summary>
        public string SkinSrc
        {
            get
            {
                //get the skin source from active tab 
                string strSkinSrc = PortalSettings.ActiveTab.SkinSrc;
                //find the paths, and remove the .ascx extention 
                strSkinSrc = strSkinSrc.Replace(".ascx", "");
                //here we encode the path for the querystring 
                strSkinSrc = "&SkinSrc=" + System.Web.HttpUtility.UrlEncode(strSkinSrc);
                return strSkinSrc;
            }
        }

        /// <summary>
        /// Get the container source from active tab and format as a querystring parameter
        /// </summary>
        public string ConSrc
        {
            get
            {
                //get the container source from the current module (as you can set containers for each module on the tab)
                string strConSrc = this.ModuleConfiguration.ContainerSrc;
                //find the paths, and remove the .ascx extention
                strConSrc = strConSrc.Replace(".ascx", "");
                //here we encode the path for the querystring 
                strConSrc = "&ContainerSrc=" + System.Web.HttpUtility.UrlEncode(strConSrc);
                return strConSrc;
            }
        }

        /// <summary>
        /// Combines the formatted date and time.
        /// </summary>
        public string DateTimeFormat
        {
            get
            {
                return DateFormat + " " + TimeFormat;
            }
        }

        /// <summary>
        ///  Gets the date format from settings or falls back to default.
        /// </summary>
        public string DateFormat
        {
            get
            {
                object format = Settings["fm_Events_DateFormat"];
                if (format != null && !String.IsNullOrEmpty(format.ToString()))
                {
                    return format.ToString();
                }
                else
                {
                    return Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern;
                }
            }

        }

        /// <summary>
        /// Gets the time format from settings or falls back to default.
        /// </summary>
        public string TimeFormat
        {
            get
            {
                object format = Settings["fm_Events_TimeFormat"];
                if (format != null && !String.IsNullOrEmpty(format.ToString()))
                {
                    return format.ToString();
                }
                else
                {
                    return Thread.CurrentThread.CurrentUICulture.DateTimeFormat.LongTimePattern;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether editing is allowed for non-logged-in users.
        /// </summary>
        /// <value>
        ///   <c>true</c> if anonymous edits allowed; otherwise, <c>false</c>.
        /// </value>
        public bool AllowAnonymousEdits
        {
            get
            {
                object setting = Settings["fm_Events_AnonymousEdits"];
                if (setting != null && bool.Parse(setting.ToString()))
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the module is isolated from other installed modules in the portal.
        /// </summary>
        /// <value>
        ///   <c>true</c> if events created in this module are visible; otherwise, <c>false</c>.
        /// </value>
        public bool ModuleIsolation
        {
            get
            {
                object setting = Settings["fm_Events_ModuleIsolation"];
                if (setting != null && bool.Parse(setting.ToString()))
                    return true;
                else
                    return false;
            }
        }


        /// <summary>
        /// Gets the user time zone from DNN profile As .NET TimeZoneInfo.
        /// </summary>
        public TimeZoneInfo UserTimeZone
        {
            get
            {
                TimeZoneInfo result = TimeZoneInfo.Utc;
                if (UserInfo.Profile.TimeZone != 0)
                {
                    foreach (TimeZoneInfo tz in TimeZoneInfo.GetSystemTimeZones())
                    {
                        // DNN6 Preferred TimeZone
                        Type type = UserInfo.Profile.GetType();
                        bool exists = type.GetProperties().Where(p => p.Name.Equals("PreferredTimeZone")).Any();
                        if (exists)
                        {
                            result = (TimeZoneInfo)UserInfo.Profile.GetType().GetProperty("PreferredTimeZone").GetValue(UserInfo.Profile, null);

                        }
                       else //Legacy DNN5 TimeZone
                        {
                            if (tz.BaseUtcOffset.TotalMinutes == UserInfo.Profile.TimeZone && tz.SupportsDaylightSavingTime)
                                result = tz;
                        }
                    }
                }
                return result;
            }
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Template for event items
        /// </summary>
        protected string _ItemTemplate;

        /// <summary>
        /// Template for alternating event items
        /// </summary>
        protected string _AlternatingItemTemplate;

        #endregion

        #region Event Handlers

        /// <summary>
        /// Register jQuery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(System.Object sender, System.EventArgs e)
        {
            DotNetNuke.Framework.jQuery.RequestRegistration();
            DotNetNuke.Framework.jQuery.RequestUIRegistration();
        }

        /// <summary>
        /// Sets up the stage and binds event entries to the listview
        /// </summary>
        /// <param Label="sender"></param>
        /// <param Label="e"></param>
        protected void Page_Load(System.Object sender, System.EventArgs e)
        {
            try
            {
                dsActiveCategories.SelectParameters["UserID"].DefaultValue = Convert.ToString(UserId);
                dsRootCategories.SelectParameters["UserID"].DefaultValue = Convert.ToString(UserId);
                dsFilterCategories.SelectParameters["UserID"].DefaultValue = Convert.ToString(UserId);

                //Load settings
                LoadTemplates();

                //Configure display 
                ConfigureDisplay();

                //On first load setup values
                if (!IsPostBack)
                {
                    //Load the Event
                    LoadEventEntries();
                }

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }

        }

        /// <summary>
        /// Gets the localized templates for the module
        /// </summary>        
        private void LoadTemplates()
        {
            //Loan Templates
            _ItemTemplate = Localization.GetString("ItemTemplate", LocalResourceFile);
            _AlternatingItemTemplate = Localization.GetString("AlternatingItemTemplate", LocalResourceFile);
        }

        /// <summary>
        /// This method will configure the display based on the module settings
        /// </summary>
        private void ConfigureDisplay()
        {
            //Get the settings
            object reportsUrl = Settings["fm_Events_ReportsUrl"];
            object reportsToNewWindowSetting = Settings["fm_Events_ReportsToNewWindow"];

            lnkEvents.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(this.TabId, "", "&mid=" + this.ModuleId + SkinSrc + ConSrc);
            litCurrentDateTime.Text = ConvertFromUtcToUserTimeZone(DateTime.UtcNow).ToString(DateTimeFormat);
            lnkSettings.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(this.TabId, "UserSettings", "&mid=" + this.ModuleId + SkinSrc + ConSrc);

            if (reportsUrl != null && !String.IsNullOrEmpty(reportsUrl.ToString()))
            {
                lnkReport.Visible = true;
                litReportSeparator.Visible = true;
                string url = reportsUrl.ToString();
                lnkReport.NavigateUrl = FormatReportUrl(url);
            }
            else
            {
                lnkReport.Visible = false;
                litReportSeparator.Visible = false;
            }

            if (reportsToNewWindowSetting != null && bool.Parse(reportsToNewWindowSetting.ToString()))
            {
                lnkReport.Target = "_blank";
            }


        }

        string FormatReportUrl(string url)
        {
            return Utils.FormatReportUrl(url, Request, SkinSrc, ConSrc);
        }

        /// <summary>
        /// Loads the event entries and binds to the listview.
        /// </summary>
        private void LoadEventEntries()
        {
            //Get paging elements
            int pageSize = 15;
            int currentPage = 1;

            //Try to get page size
            if (Settings["fm_Events_PageSize"] != null)
                pageSize = int.Parse(Settings["fm_Events_PageSize"].ToString());

            EventController objEvents = new EventController();
            List<EventInfo> colEvents;

            //get the content from the Event table
            int? modId;
            if (ModuleIsolation) { modId = ModuleId; } else { modId = null; }
            colEvents = objEvents.GetEvents(modId, UserId, SelectedCategory);

            //Setup the paged datasource
            PagedDataSource oDataSource = new PagedDataSource();
            oDataSource.DataSource = colEvents;

            //do we need the pager
            if (colEvents.Count > pageSize)
            {
                //Check for a page selection
                if (Request.QueryString["currentpage"] != null)
                {
                    //get it but be careful of input
                    try
                    {
                        currentPage = int.Parse(Request.QueryString["currentpage"]);
                    }
                    catch (FormatException)
                    { }
                }

                //Setup datasource and pager
                oDataSource.PageSize = pageSize;
                oDataSource.AllowPaging = true;
                dnnPager.TotalRecords = colEvents.Count;
                dnnPager.PageSize = pageSize;
                dnnPager.TabID = this.TabId;

                if (SelectedCategory != null)
                    dnnPager.QuerystringParams = "category=" + SelectedCategory;

                //Ensure current page is correct, default if incorrect
                if (currentPage > oDataSource.PageCount)
                    currentPage = 1;

                //Setup current page info
                dnnPager.CurrentPage = currentPage;
                oDataSource.CurrentPageIndex = currentPage - 1;

                //Ensure visibility
                dnnPager.Visible = true;
            }
            else
            {
                //No paging needed, disable it on the source, and hide the page
                dnnPager.Visible = false;
                oDataSource.AllowPaging = false;
            }

            //bind the content to the repeater
            lstContent.DataSource = oDataSource;
            lstContent.DataBind();

            //Ensure that the no records message isn't visible
            pnlNoRecords.Visible = false;
        }

        /// <summary>
        /// Handles the Itemœnserting event of the lstContent control. A Stub for AutoEventWireup.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewInsertEventArgs"/> instance containing the event data.</param>
        protected void lstContent_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
        }

        /// <summary>
        /// Handles the ItemUpdating event of the lstContent control. A Stub for AutoEventWireup.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewUpdateEventArgs"/> instance containing the event data.</param>
        protected void lstContent_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
        }

        /// <summary>
        /// Handles the ItemDeleting event of the lstContent control. A Stub for AutoEventWireup.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewDeleteEventArgs"/> instance containing the event data.</param>
        protected void lstContent_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
        }

        /// <summary>
        /// Handles the ItemEditing event of the lstContent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewEditEventArgs"/> instance containing the event data.</param>
        protected void lstContent_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            CloseInsert();
            lstContent.EditIndex = e.NewEditIndex;
            LoadEventEntries();
        }

        /// <summary>
        /// Handles the ItemCreated event of the lstContent control. Insert template prepared here.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewItemEventArgs"/> instance containing the event data.</param>
        protected void lstContent_ItemCreated(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.InsertItem)
            {
                if (SelectedCategory != null)
                {
                    DropDownList ddlRootCategory;
                    DropDownList ddlCategory;
                    ddlRootCategory = (DropDownList)lstContent.InsertItem.FindControl("ddlRootCategory");
                    ddlCategory = (DropDownList)lstContent.InsertItem.FindControl("ddlCategory");
                    String SelectedValue = SelectedCategory.Value.ToString();

                    CategoryController objCategories = new CategoryController();
                    List<CategoryInfo> categories;
                    //get the content from the Event table
                    categories = objCategories.GetCategories(UserId, NoCategoryId, false);

                    ddlRootCategory.DataSourceID = null;
                    ddlCategory.DataSourceID = null;
                    ddlRootCategory.Items.Clear();
                    foreach (CategoryInfo category in categories.Where(c => c.ParentId == null))
                    {
                        ddlRootCategory.Items.Add(new ListItem(category.Name, category.CategoryId.ToString()));
                    }

                    if (ddlRootCategory.Items.FindByValue(SelectedValue) != null)
                    {
                        ddlRootCategory.SelectedValue = SelectedValue;
                        ddlRootCategory_SelectedIndexChanged(null, EventArgs.Empty);
                    }

                }
                else
                {
                    RefreshLabels(e.Item, null);
                }
                TextBox dateEventDate = (TextBox)e.Item.FindControl("dateEventDate");
                dateEventDate.Text = Convert.ToString(ConvertFromUtcToUserTimeZone(DateTime.UtcNow).ToString(DateTimeFormat));

                SetRegexValidators(e.Item);

            }
        }

        /// <summary>
        /// Handles the PreRender event of the lstContent control. Set regex datetime validators for the edit item.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lstContent_PreRender(object sender, EventArgs e)
        {
            if (this.lstContent.EditIndex != -1)
            {
                SetRegexValidators(this.lstContent.Items[this.lstContent.EditIndex]);
            }
        }

        private void SetRegexValidators(ListViewItem item)
        {
            RegularExpressionValidator valdateEventDateRegex = (RegularExpressionValidator)item.FindControl("valdateEventDateRegex");
            RegularExpressionValidator valdateEventDate2Regex = (RegularExpressionValidator)item.FindControl("valdateEventDate2Regex");
            object dateTimeValidationRegex = Settings["fm_Events_DateTimeValidationRegex"];
            if (dateTimeValidationRegex != null && !String.IsNullOrEmpty(dateTimeValidationRegex.ToString()))
            {
                valdateEventDateRegex.ValidationExpression = valdateEventDate2Regex.ValidationExpression = dateTimeValidationRegex.ToString();
                valdateEventDateRegex.ErrorMessage = valdateEventDate2Regex.ErrorMessage = DateTimeFormat;
                // dd.MM.yyyy HH:mm ValidationExpression="^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[012])\.(19|20)\d\d[ ](0[0-9]|[1][0-9]|2[0-3])[:]([0-5][0-9])$"  
            }
            else
            {
                valdateEventDateRegex.Enabled = valdateEventDate2Regex.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the cmdAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            CloseEdit();
            lstContent.InsertItemPosition = InsertItemPosition.FirstItem;
            LoadEventEntries();
        }

        /// <summary>
        /// Handles the Click event of the cmdQuickAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void cmdQuickAdd_Click(object sender, EventArgs e)
        {
            EventInfo oInfo = new EventInfo();
            oInfo.ModuleId = ModuleId;
            oInfo.UserId = UserId;
            oInfo.EventDate = DateTime.UtcNow;
            oInfo.CategoryId = int.Parse(ddlFilterCategory.SelectedValue);
            oInfo.Label = "";

            //Save it
            EventController oController = new EventController();
            oController.AddEvent(oInfo);

            //Reload Event display
            LoadEventEntries();
        }


        /// <summary>
        /// Handles the ItemCanceling event of the lstContent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewCancelEventArgs"/> instance containing the event data.</param>
        protected void lstContent_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            CloseInsert();
            CloseEdit();
            LoadEventEntries();
        }

        private void CloseInsert()
        {
            lstContent.InsertItemPosition = InsertItemPosition.None;
        }

        private void CloseEdit()
        {
            lstContent.EditIndex = -1;

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlFilterCategory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEventEntries();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlRootCategory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ddlRootCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            int index = lstContent.EditIndex;
            DropDownList ddlRootCategory;
            DropDownList ddlCategory;

            if (lstContent.EditIndex >= 0)
            {
                ddlRootCategory = (DropDownList)lstContent.EditItem.FindControl("ddlRootCategory");
                ddlCategory = (DropDownList)lstContent.EditItem.FindControl("ddlCategory");
            }
            else
            {
                ddlRootCategory = (DropDownList)lstContent.InsertItem.FindControl("ddlRootCategory");
                ddlCategory = (DropDownList)lstContent.InsertItem.FindControl("ddlCategory");
            }

            int ParentId = Convert.ToInt32(ddlRootCategory.SelectedValue);
            string SelectedValue = ddlCategory.SelectedValue.ToString();

            CategoryController objCategories = new CategoryController();
            List<CategoryInfo> categories;

            //get the content from the Event table
            categories = objCategories.GetCategories(UserId, ParentId, false);

            ddlCategory.Items.Clear();
            foreach (CategoryInfo category in categories)
            {
                ddlCategory.Items.Add(new ListItem(category.Name, category.CategoryId.ToString()));
            }

            if (ddlCategory.Items.FindByValue(SelectedValue) != null)
            {
                ddlCategory.SelectedValue = SelectedValue;
            }

            CategoryInfo SelectedCategory = objCategories.GetCategory(UserId, ParentId);
            if (lstContent.EditIndex >= 0)
            {
                RefreshLabels(lstContent.EditItem, SelectedCategory);
            }
            else
            {
                RefreshLabels(lstContent.InsertItem, SelectedCategory);
            }

        }

        string lastHeader = null;
        /// <summary>
        /// Adds the grouping header row for future and past events.
        /// </summary>
        /// <returns></returns>
        protected string AddGroupingRowForFutureAndPast()
        {
            DateTime eventDate = (DateTime)Eval("EventDate");
            String category = (String)Eval("Category");

            String rootCategory = "";
            if (ddlFilterCategory.SelectedItem != null)
                rootCategory = ddlFilterCategory.SelectedItem.Text;

            String currentHeader;
            if (eventDate > ConvertFromUtcToUserTimeZone(DateTime.UtcNow))
            {
                currentHeader = Localization.GetString("FutureEventsHeader", LocalResourceFile);
                if (String.IsNullOrEmpty(currentHeader)) currentHeader = "Future Events";
            }
            else
            {
                currentHeader = Localization.GetString("PastEventsHeader", LocalResourceFile);
                if (String.IsNullOrEmpty(currentHeader)) currentHeader = "Past Events";
            }

            if (lastHeader != currentHeader)
            {
                lastHeader = currentHeader;
                string result = String.Format("<tr><th>{0}</th></tr>", currentHeader);
                if (SelectedCategory != null)
                {
                    result = String.Format("<tr><th>{0} - {1}</th></tr>", currentHeader, rootCategory);
                }
                return result;
            }
            else
            {
                return String.Empty;
            }
        }
        #endregion

        #region Optional Interfaces

        /// <summary>
        /// Sets the module actions available for this module.
        /// </summary>
        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection Actions = new ModuleActionCollection();
                return Actions;
            }
        }

        #endregion

        #region Item Binding Events

        /// <summary>
        /// This is the driving method to generating the rows of the listview. We output the event
        /// entries according the the template provided by the user/admin
        /// </summary>
        /// <param Label="sender"></param>
        /// <param Label="e"></param>
        protected void lstContent_ItemDataBound(object sender, System.Web.UI.WebControls.ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ListViewDataItem lvDataItem = (ListViewDataItem)e.Item;
                if (lvDataItem.DisplayIndex == lstContent.EditIndex)
                {
                    //Check if selected category is active
                    DropDownList ddlCategory = (DropDownList)e.Item.FindControl("ddlCategory");
                    String CategoryID = Convert.ToString(DataBinder.Eval(lvDataItem.DataItem, "CategoryID"));
                    if (ddlCategory != null && ddlCategory.Items.FindByValue(CategoryID) != null)
                    {
                        ddlCategory.SelectedValue = CategoryID;
                    }
                }

                var dataItem = lvDataItem.DataItem;

                //Get the literal out of the list
                Literal oEvent = (Literal)e.Item.FindControl("eventblock");

                //Get the data values
                string oLabel = Convert.ToString(DataBinder.Eval(dataItem, "Label"));

                DateTime eventDate = Convert.ToDateTime(DataBinder.Eval(dataItem, "EventDate"));
                string oTimeSpan = formatRelativeTime(eventDate);

                string oEventDate = ConvertFromUtcToUserTimeZone(eventDate).ToString(DateTimeFormat);

                string oMeasurement = Convert.ToString(DataBinder.Eval(dataItem, "Measurement"));
                string oCategory = Convert.ToString(DataBinder.Eval(dataItem, "Category"));
                string oComments = Convert.ToString(DataBinder.Eval(dataItem, "comments"));
                DateTime oDateCreated = Convert.ToDateTime(DataBinder.Eval(dataItem, "DateCreated").ToString());

                //Get the template
                StringBuilder oTemplate = new StringBuilder();
                if (e.Item.ItemType == ListViewItemType.DataItem)
                    oTemplate.Append(_ItemTemplate);
                else
                    oTemplate.Append(_AlternatingItemTemplate);

                //Replace the template tokens for standard elements
                oTemplate.Replace("[LABEL]", Server.HtmlEncode(oLabel));
                oTemplate.Replace("[TIMESPAN]", oTimeSpan);
                oTemplate.Replace("[EVENTDATE]", oEventDate);
                oTemplate.Replace("[MEASUREMENT]", oMeasurement);
                oTemplate.Replace("[CATEGORY]", Server.HtmlEncode(oCategory));
                oTemplate.Replace("[COMMENTS]", Server.HtmlEncode(oComments));
                oTemplate.Replace("[DATECREATED]", oDateCreated.ToShortDateString());

                //Set the text
                oEvent.Text = oTemplate.ToString();

            }
        }

        /// <summary>
        /// Handles the ItemCommand event of the lstContent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ListViewCommandEventArgs"/> instance containing the event data.</param>
        protected void lstContent_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Insert":
                    {
                        InsertEvent(e.Item);
                        break;
                    }
                case "Update":
                    {
                        UpdateEvent(e.CommandArgument as string, e.Item);
                        break;
                    }
                case "Delete":
                    {
                        DeleteEvent(e.CommandArgument as string);
                        break;
                    }
            }
        }

        private EventInfo ReadEvent(ListViewItem item)
        {
            TextBox txtLabel = (TextBox)item.FindControl("txtLabel");
            TextBox txtComments = (TextBox)item.FindControl("txtComments");
            TextBox txtMeasurement = (TextBox)item.FindControl("txtMeasurement");
            TextBox dateEventDate = (TextBox)item.FindControl("dateEventDate");
            TextBox dateEventDate2 = (TextBox)item.FindControl("dateEventDate2");

            DropDownList ddlCategory = (DropDownList)item.FindControl("ddlCategory");

            //Declare our new info object
            EventInfo oInfo = new EventInfo();
            oInfo.ModuleId = ModuleId;
            oInfo.UserId = UserId;
            oInfo.Label = txtLabel.Text;
            oInfo.Comments = txtComments.Text;

            DateTime parsedDate;
            if (!DateTime.TryParseExact(dateEventDate.Text, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out parsedDate))
                if (!DateTime.TryParse(dateEventDate.Text, out parsedDate))
                {
                    throw new Exception(String.Format("Invalid Date. Required format {0}", Server.HtmlEncode(DateTimeFormat)));
                }

            oInfo.EventDate = ConvertFromUserTimeZoneToUtc(parsedDate);

            DateTime? parsedDate2;
            if (String.IsNullOrEmpty(dateEventDate2.Text))
            {
                parsedDate2 = null;
            }
            else
            {
                parsedDate2 = null;

                DateTime tempDate;
                if (!DateTime.TryParseExact(dateEventDate2.Text, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out tempDate))
                    if (!DateTime.TryParse(dateEventDate2.Text, out tempDate))
                        throw new Exception(String.Format("Invalid 2nd Date. Required format {0}", Server.HtmlEncode(DateTimeFormat)));
                parsedDate2 = ConvertFromUserTimeZoneToUtc(tempDate);
            }

            oInfo.EventDate2 = parsedDate2;

            TextBox txtLabel2 = (TextBox)item.FindControl("txtLabel2");
            oInfo.Label2 = txtLabel2.Text;

            if (String.IsNullOrEmpty(txtMeasurement.Text))
                oInfo.Measurement = null;
            else
            {

                double measurement;
                double.TryParse(txtMeasurement.Text.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, out measurement);
                oInfo.Measurement = measurement;
            }

            TextBox txtMeasurement2 = (TextBox)item.FindControl("txtMeasurement2");
            if (String.IsNullOrEmpty(txtMeasurement2.Text))
                oInfo.Measurement2 = null;
            else
            {
                double Measurement2;
                double.TryParse(txtMeasurement2.Text.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, out Measurement2);
                oInfo.Measurement2 = Measurement2;
            }

            TextBox txtExternalId = (TextBox)item.FindControl("txtExternalId");
            oInfo.ExternalId = txtExternalId.Text.Trim();

            oInfo.CategoryId = int.Parse(ddlCategory.SelectedValue.ToString());

            return oInfo;
        }

        private void ClearInputFields(ListViewItem item)
        {
            TextBox txtLabel = (TextBox)item.FindControl("txtLabel");
            TextBox txtComments = (TextBox)item.FindControl("txtComments");
            TextBox txtMeasurement = (TextBox)item.FindControl("txtMeasurement");
            TextBox dateEventDate = (TextBox)item.FindControl("dateEventDate");

            DropDownList ddlCategory = (DropDownList)item.FindControl("ddlCategory");
            txtLabel.Text = "";
            txtComments.Text = "";
            dateEventDate.Text = "";
            txtMeasurement.Text = "";
            ddlCategory.SelectedIndex = 0;
        }

        private void InsertEvent(ListViewItem insertItem)
        {
            //Ensure page is valid
            Page.Validate("InsertEvent");

            if (Page.IsValid)
            {
                if (UserId == AnonymousUserId && !AllowAnonymousEdits)
                {
                    ShowError("Login required to save events.");
                    return;
                }
                else
                {
                    try
                    {
                        EventInfo oInfo = ReadEvent(insertItem);

                        //Save it
                        EventController oController = new EventController();
                        oController.AddEvent(oInfo);

                        //Clear input fields
                        ClearInputFields(insertItem);

                        if (string.IsNullOrEmpty(oInfo.Label))
                            ShowInfo(String.Format("Event at {0} saved.", ConvertFromUtcToUserTimeZone(oInfo.EventDate).ToString(DateTimeFormat)));
                        else
                            ShowInfo(String.Format("'{0}' saved.", oInfo.Label));

                        CloseInsert();
                    }
                    catch (Exception ex)
                    {
                        EventLogger.AddLog("flowmarks_Events", string.Format("InsertEvent: {0}", ex.ToString()), PortalSettings, UserId, DotNetNuke.Services.Log.EventLog.EventLogController.EventLogType.HOST_ALERT);
                        ShowError("Error creating event: " + ex.Message);
                    }
                    finally
                    {
                        //Reload Event display
                        LoadEventEntries();
                    }
                }


            }

        }

        private void UpdateEvent(string sEventID, ListViewItem editItem)
        {
            try
            {
                //Ensure page is valid
                Page.Validate("ICGEvent");

                int eventID = 0;
                Int32.TryParse(sEventID, out eventID);

                if (Page.IsValid && eventID > 0)
                {
                    if (UserId == AnonymousUserId && !AllowAnonymousEdits)
                    {
                        ShowError("Login required to save events.");
                        return;
                    }
                    else
                    {
                        EventInfo oInfo = ReadEvent(editItem);
                        oInfo.EventId = eventID;

                        //Save it
                        EventController oController = new EventController();
                        oController.UpdateEvent(oInfo);
                        if (string.IsNullOrEmpty(oInfo.Label))
                            ShowInfo(String.Format("Event at {0} saved.", ConvertFromUtcToUserTimeZone(oInfo.EventDate).ToString(DateTimeFormat)));
                        else
                            ShowInfo(String.Format("'{0}' saved.", oInfo.Label));

                        CloseEdit();
                    }
                }
            }
            catch (Exception ex)
            {
                EventLogger.AddLog("flowmarks_Events", string.Format("UpdateEvent: {0}", ex.ToString()), PortalSettings, UserId, DotNetNuke.Services.Log.EventLog.EventLogController.EventLogType.HOST_ALERT);
                ShowError("Error: " + ex.Message);
            }
            finally
            {
                //Reload Event display
                LoadEventEntries();
            }
        }

        private void DeleteEvent(string sEventID)
        {
            try
            {
                int eventID = 0;
                Int32.TryParse(sEventID, out eventID);

                if (eventID > 0)
                {
                    if (UserId == AnonymousUserId && !AllowAnonymousEdits)
                    {
                        ShowError("Login required to delete events.");
                        return;
                    }
                    else
                    {

                        //DeleteEvent it
                        EventController oController = new EventController();
                        oController.DeleteEvent(ModuleId, UserId, eventID);
                        //ShowInfo(String.Format("Event deleted. <a id='{0}' class='undo' href='#undo'><b>Undelete</b></a>", eventID));
                        ShowInfo("Event deleted.");

                        CloseEdit();
                    }
                }
            }
            catch (Exception ex)
            {
                EventLogger.AddLog("flowmarks_Events", string.Format("DeleteEvent: {0}", ex.ToString()), PortalSettings, UserId, DotNetNuke.Services.Log.EventLog.EventLogController.EventLogType.HOST_ALERT);
                ShowError("Error: " + ex.Message);
            }
            finally
            {
                //Reload Event display
                LoadEventEntries();
            }

        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="controlLabel">The control label.</param>
        /// <returns></returns>
        public string GetLabel(Object label, String controlLabel)
        {
            string defaultLabel = Localization.GetString(controlLabel, LocalResourceFile);

            if (label == null || String.IsNullOrEmpty(label.ToString()))
                return defaultLabel ?? String.Empty;
            else
                return Server.HtmlEncode(label.ToString());
        }

        /// <summary>
        /// Gets the last date.
        /// </summary>
        /// <param name="created">The date created.</param>
        /// <param name="modified">The date modified.</param>
        /// <returns></returns>
        public DateTime GetLastDate(Object created, Object modified)
        {
            var dateCreated = (DateTime)created;
            var dateModified = (DateTime)modified;
            if (dateModified > dateCreated)
                return dateModified;
            else
                return dateCreated;
        }

        /// <summary>
        /// Format a tooltip string from EventInfo object
        /// </summary>
        /// <param name="dataitem">An EventInfo object</param>
        /// <returns></returns>
        public string ToolTip(object dataitem)
        {
            StringBuilder sb = new StringBuilder();

            if (dataitem != null)
            {
                try
                {
                    EventInfo oInfo = (EventInfo)dataitem;
                    int labelwidth = 0;

                    if (!string.IsNullOrEmpty(oInfo.Label))
                        sb.AppendFormat("{0}: {1}" + Environment.NewLine, GetLabel(oInfo.Label_Label, "lblLabel").PadRight(labelwidth), oInfo.Label);

                    sb.AppendFormat("{0}: {1}" + Environment.NewLine, GetLabel(oInfo.Label_EventDate, "lblEventDate").PadRight(labelwidth), ConvertFromUtcToUserTimeZone(oInfo.EventDate).ToString(DateTimeFormat));

                    sb.AppendFormat("{0}: {1}" + Environment.NewLine, GetLabel(oInfo.Label_Category, "lblCategory").PadRight(labelwidth), oInfo.Category);

                    if (!string.IsNullOrEmpty(oInfo.Label2))
                        sb.AppendFormat("{0}: {1}" + Environment.NewLine, GetLabel(oInfo.Label_Label2, "lblLabel2").PadRight(labelwidth), oInfo.Label2);

                    if (oInfo.EventDate2 != null)
                        sb.AppendFormat("{0}: {1}" + Environment.NewLine, GetLabel(oInfo.Label_EventDate2, "lblEventDate2").PadRight(labelwidth), ConvertFromUtcToUserTimeZone(oInfo.EventDate2.Value).ToString(DateTimeFormat));

                    if (oInfo.Measurement != null)
                        sb.AppendFormat("{0}: {1}" + Environment.NewLine, GetLabel(oInfo.Label_Measurement, "lblMeasurement").PadRight(labelwidth), oInfo.Measurement);

                    if (oInfo.Measurement2 != null)
                        sb.AppendFormat("{0}: {1}" + Environment.NewLine, GetLabel(oInfo.Label_Measurement2, "lblMeasurement2").PadRight(labelwidth), oInfo.Measurement2);

                    if (!string.IsNullOrEmpty(oInfo.Comments))
                        sb.AppendFormat("{0}: {1}" + Environment.NewLine, GetLabel(oInfo.Label_Comments, "lblComments").PadRight(labelwidth), oInfo.Comments);

                    if (!string.IsNullOrEmpty(oInfo.ExternalId))
                        sb.AppendFormat("{0}: {1}" + Environment.NewLine, GetLabel(oInfo.Label_ExternalId, "lblExternalId").PadRight(labelwidth), oInfo.ExternalId);

                    sb.AppendFormat("{0}: {1}" + Environment.NewLine, "Edited", ConvertFromUtcToUserTimeZone(GetLastDate(oInfo.DateCreated, oInfo.DateModified)).ToString(DateTimeFormat));
                }
                catch (Exception ex)
                {
                    EventLogger.AddLog("flowmarks_Events", string.Format("ToolTip: {0}", ex.ToString()), PortalSettings, UserId, DotNetNuke.Services.Log.EventLog.EventLogController.EventLogType.HOST_ALERT);
                    ShowError(ex.Message);
                }
            }

            return sb.ToString();
        }


        private void RefreshLabels(ListViewItem item, CategoryInfo category)
        {
            Label lblCategory = (Label)item.FindControl("lblCategory");
            Label lblLabel = (Label)item.FindControl("lblLabel");
            Label lblLabel2 = (Label)item.FindControl("lblLabel2");
            Label lblComments = (Label)item.FindControl("lblComments");
            Label lblMeasurement = (Label)item.FindControl("lblMeasurement");
            Label lblMeasurement2 = (Label)item.FindControl("lblMeasurement2");
            Label lblEventDate = (Label)item.FindControl("lblEventDate");
            Label lblEventDate2 = (Label)item.FindControl("lblEventDate2");
            Label lblEventId = (Label)item.FindControl("lblEventId");
            Label lblExternalId = (Label)item.FindControl("lblExternalId");

            if (category == null)
                category = new CategoryInfo();

            lblCategory.Text = GetLabel(category.Label_Category, "lblCategory");
            lblLabel.Text = GetLabel(category.Label_Label, "lblLabel");
            lblLabel2.Text = GetLabel(category.Label_Label2, "lblLabel2");
            lblComments.Text = GetLabel(category.Label_Comments, "lblComments");
            lblEventDate.Text = GetLabel(category.Label_EventDate, "lblEventDate");
            lblEventDate2.Text = GetLabel(category.Label_EventDate2, "lblEventDate2");
            lblMeasurement.Text = GetLabel(category.Label_Measurement, "lblMeasurement");
            lblMeasurement2.Text = GetLabel(category.Label_Measurement2, "lblMeasurement2");
            lblExternalId.Text = GetLabel(category.Label_ExternalId, "lblExternalId");
        }


        /// <summary>
        /// Converts from UTC to user time zone.
        /// </summary>
        /// <param name="eventDate">The event date.</param>
        /// <returns></returns>
        public DateTime ConvertFromUtcToUserTimeZone(Object eventDate)
        {
            var dt = (DateTime)eventDate;
            return Utils.ConvertFromUtcToTimeZone(dt, UserTimeZone);
        }

        /// <summary>
        /// Converts from user time zone to UTC.
        /// </summary>
        /// <param name="eventDate">The event date.</param>
        /// <returns></returns>
        public DateTime ConvertFromUserTimeZoneToUtc(Object eventDate)
        {
            var dt = (DateTime)eventDate;
            return Utils.ConvertFromTimeZoneToUtc(dt, UserTimeZone);
        }

        /// <summary>
        /// Formats the time span.
        /// </summary>
        /// <param name="eventDate">The event date.</param>
        /// <returns></returns>
        public string formatTimeSpan(DateTime eventDate)
        {
            TimeSpan ts;
            if (eventDate > DateTime.UtcNow)
                ts = eventDate - DateTime.UtcNow;
            else
                ts = DateTime.UtcNow - eventDate;

            string result = "";

            if (ts.Days > 365)
            {
                int years = ts.Days / 365;
                if (years > 1)
                    result += years + " years ";
                else
                    result += "1 year ";
            }
            if (ts.Days > 30)
            {
                int months = (ts.Days / 30) % 12;
                if (months > 1)
                    result += months + " months ";
                else
                    result += "1 month ";
            }
            if (ts.Days > 0)
            {
                int days = ts.Days % 365;
                if (days > 1)
                    result += days + " days ";
                else
                    result += "1 day ";
            }
            if (ts.Hours > 0)
            {
                int hours = ts.Hours;
                if (hours > 1)
                    result += hours + " hours ";
                else
                    result += "1 hour ";
            }
            if (ts.Minutes > 0 && ts.Days < 30)
            {
                int minutes = ts.Minutes;
                if (minutes > 1)
                    result += minutes + " minutes";
                else
                    result += "1 minute";
            }
            else
                result += "0 minutes";

            return result;
        }

        /// <summary>
        /// Formats the datetime relative to the current time.
        /// </summary>
        /// <param name="dt">The datetime.</param>
        /// <returns></returns>
        public string formatRelativeTime(DateTime dt)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(Math.Abs(DateTime.UtcNow.Ticks - dt.Ticks));
            double delta = ts.TotalSeconds;

            if (delta < 0)
            {
                return "impossible";
            }
            if (delta < 1 * MINUTE)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 2 * MINUTE)
            {
                return "a minute ago";
            }
            if (delta < 60 * MINUTE)
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 120 * MINUTE)
            {
                return ts.Hours + " hour " + ts.Minutes + " minutes";
            }
            if (delta < 24 * HOUR)
            {
                return ts.Hours + " hours " + ts.Minutes + " minutes";
            }
            if (delta < 48 * HOUR)
            {
                return ts.Days + " day " + ts.Hours + " hours";
            }
            if (delta < 30 * DAY)
            {
                return ts.Days + " days " + ts.Hours + " hours";
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                int days = Convert.ToInt32(Math.Floor((double)ts.Days - (months * 30)));
                return months <= 1 ? months + " month " + days + " days" : months + " months " + days + " days";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365.25));
                int months = Convert.ToInt32(Math.Floor(((double)ts.Days - (years * 365.25)) / 30));
                int days = Convert.ToInt32(Math.Floor((double)ts.Days - (years * 365.25) - (months * 30)));
                return years <= 1 ? years + " year " + months + " months " + days + " days" : years + " years " + months + " months " + days + " days";
            }
        }

        /// <summary>
        /// Show error message.
        /// </summary>
        /// <param name="message"></param>
        public void ShowError(string message)
        {
            Utils.ShowError(message, MessageBox, lblMessage);
        }

        /// <summary>
        /// Show info-level message.
        /// </summary>
        /// <param name="message"></param>
        public void ShowInfo(string message)
        {
            Utils.ShowInfo(message, MessageBox, lblMessage);
        }

        #endregion
    }
}

