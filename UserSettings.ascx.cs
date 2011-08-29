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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//using DotNetNuke;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

using DevExpress.Web.ASPxGridView;

//Using 
using flowmarks.Modules.Events.Components;
using System.Collections.Specialized;

namespace flowmarks.Modules.Events
{
    /// <summary>
    /// Display and edit Categories
    /// </summary>
    public partial class UserSettings : PortalModuleBase
    {
        /// <summary>
        /// DotNetNuke Event logger
        /// </summary>
        public DotNetNuke.Services.Log.EventLog.EventLogController EventLogger = new DotNetNuke.Services.Log.EventLog.EventLogController();
        private const int NoCategoryId = -1;
        private const int AnonymousUserId = -1;
        private const int EveryUserId = 0;

        #region "Properties"

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

        #endregion

        private void Page_Init(object sender, System.EventArgs e)
        {

            // DNN controls ASP.NET AJAX features
            if (DotNetNuke.Framework.AJAX.IsInstalled())
            {
                //DotNetNuke.Framework.AJAX.RegisterScriptManager();
                //DotNetNuke.Framework.AJAX.WrapUpdatePanelControl(lblMessage, true);
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Configure display
                ConfigureDisplay();

                CategoryController objCategories = new CategoryController();
                List<CategoryInfo> categories;

                //get the content from the Event table
                categories = objCategories.GetCategories(UserId, NoCategoryId, null);
                gvCategorySettings.DataSource = categories.OrderBy(a => a.ParentId).ThenBy(a => a.CategoryId);
                gvCategorySettings.DataBind();

                dsRootCategories.SelectParameters["UserID"].DefaultValue = Convert.ToString(UserId);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
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
            litCurrentDateTime.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
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
        /// Handles the RowUpdating event of the gvCategorySettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.Web.Data.ASPxDataUpdatingEventArgs"/> instance containing the event data.</param>
        protected void gvCategorySettings_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (UserId == AnonymousUserId && !AllowAnonymousEdits)
            {
                ShowError("Login required to save categories.");
                e.Cancel = true;
                return;
            }
            else
            {

                CategoryController objCategories = new CategoryController();
                CategoryInfo category = ReadCategory(e.Keys, e.NewValues);
                category.UserId = UserId;

                objCategories.UpdateCategory(category);

                List<CategoryInfo> categories;
                categories = objCategories.GetCategories(UserId, NoCategoryId, null);
                ASPxGridView gv = (ASPxGridView)sender;
                gv.DataSource = categories.OrderBy(a => a.ParentId).ThenBy(a => a.CategoryId);
                gv.DataBind();

                gv.CancelEdit();
                e.Cancel = true;
               
            }
        }

        /// <summary>
        /// Handles the RowDeleting event of the gvCategorySettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.Web.Data.ASPxDataDeletingEventArgs"/> instance containing the event data.</param>
        protected void gvCategorySettings_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                if (UserId == AnonymousUserId && !AllowAnonymousEdits)
                {
                    ShowError("Login required to delete categories.");
                    e.Cancel = true;
                    return;
                }
                else
                {
                    CategoryController objCategories = new CategoryController();
                    int categoryId = (int)e.Keys["CategoryId"];

                    if (categoryId == NoCategoryId)
                    {
                        e.Cancel = true;
                        ShowError("No Category can't be deleted.");
                        return;
                    }
                    else
                    {
                        objCategories.DeleteCategory(UserId, categoryId);
                        ShowInfo(string.Format("Deleted: {0}", e.Values["Name"]));
                    }

                    List<CategoryInfo> categories;
                    categories = objCategories.GetCategories(UserId, NoCategoryId, null);
                    ASPxGridView gv = (ASPxGridView)sender;

                    gv.DataSource = categories.OrderBy(a => a.ParentId).ThenBy(a => a.CategoryId);
                    gv.DataBind();
                    gv.CancelEdit();
                    e.Cancel = true;
                }
                
            }
            catch (Exception ex)
            {
                EventLogger.AddLog("flowmark_Events", string.Format("gvCategorySettings_RowDeleting: {0}", ex.ToString()), PortalSettings, UserId, DotNetNuke.Services.Log.EventLog.EventLogController.EventLogType.HOST_ALERT);
                ShowError("Couldn't delete category. Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the RowInserting event of the gvCategorySettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.Web.Data.ASPxDataInsertingEventArgs"/> instance containing the event data.</param>
        protected void gvCategorySettings_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (UserId == AnonymousUserId && !AllowAnonymousEdits)
            {
                ShowError("Login required to save categories.");
                e.Cancel = true;
                return;
            }
            else
            {

                CategoryController objCategories = new CategoryController();
                CategoryInfo category = ReadCategory(e.NewValues);
                category.UserId = UserId;

                objCategories.AddCategory(category);

                List<CategoryInfo> categories;
                categories = objCategories.GetCategories(UserId, NoCategoryId, null);
                ASPxGridView gv = (ASPxGridView)sender;

                gv.DataSource = categories.OrderBy(a => a.ParentId).ThenBy(a => a.CategoryId);
                gv.DataBind();
                gv.CancelEdit();
                e.Cancel = true;
            }
        }

        private CategoryInfo ReadCategory(OrderedDictionary keys, OrderedDictionary values)
        {
            CategoryInfo category = new CategoryInfo();
            category.DateModified = DateTime.Now;

            category.CategoryId = (int)keys["CategoryId"];
            category.Name = (string)values["Name"];
            category.ParentId = (int?)values["ParentId"];
            category.IsHidden = (bool)values["IsHidden"];
            category.Comments = (string)values["Comments"];

            category.Label_Category = (string)values["Label_Category"];
            category.Label_Label = (string)values["Label_Label"];
            category.Label_EventDate = (string)values["Label_EventDate"];
            category.Label_EventDate2 = (string)values["Label_EventDate2"];
            category.Label_Measurement = (string)values["Label_Measurement"];
            category.Label_Measurement2 = (string)values["Label_Measurement2"];
            category.Label_Label2 = (string)values["Label_Label2"];
            category.Label_ExternalId = (string)values["Label_ExternalId"];


            return category;
        }

        private CategoryInfo ReadCategory(OrderedDictionary values)
        {
            CategoryInfo category = new CategoryInfo();
            category.DateCreated = DateTime.Now;

            category.Name = (string)values["Name"];
            category.ParentId = (int?)values["ParentId"];

            if (values["IsHidden"] == null)
                category.IsHidden = false;
            else
                category.IsHidden = (bool)values["IsHidden"];

            category.Comments = (string)values["Comments"];

            category.Label_Category = (string)values["Label_Category"];
            category.Label_Label = (string)values["Label_Label"];
            category.Label_EventDate = (string)values["Label_EventDate"];
            category.Label_EventDate2 = (string)values["Label_EventDate2"];
            category.Label_Measurement = (string)values["Label_Measurement"];
            category.Label_Measurement2 = (string)values["Label_Measurement2"];
            category.Label_Label2 = (string)values["Label_Label2"];
            category.Label_ExternalId = (string)values["Label_ExternalId"];

            return category;
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


    }
}