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
using System.Web.UI;

using DotNetNuke;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using System.IO;

namespace flowmarks.Modules.Events
{

    /// <summary>
    /// This page is used to set and configure the display options for the module!
    /// </summary>
    public partial class Settings : ModuleSettingsBase
    {

        #region Base Method Implementations
        /// <summary>
        /// Load the settings if possible, if not, default the values
        /// </summary>
        public override void LoadSettings()
        {
            try
            {
                lblVersion.Text = GetModuleDllDescription(System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).InternalName) 
                 + " , " + System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;

                //Get all settings to objects
                object allowAnonymousEdits = ModuleSettings["fm_Events_AnonymousEdits"];
                object pageSizeSetting = ModuleSettings["fm_Events_PageSize"];
                object dateFormat = ModuleSettings["fm_Events_DateFormat"];
                object timeFormat = ModuleSettings["fm_Events_TimeFormat"];
                object reportsUrl = ModuleSettings["fm_Events_ReportsUrl"];
                object reportsToNewWindow = ModuleSettings["fm_Events_ReportsToNewWindow"];

                //Conditionally set or default them

                if (allowAnonymousEdits != null)
                    chkAllowAnonymousEdits.Checked = bool.Parse(allowAnonymousEdits.ToString());

                if (pageSizeSetting != null)
                {
                    System.Web.UI.WebControls.ListItem oItem = ddlPageSize.Items.FindByValue(pageSizeSetting.ToString());

                    if (oItem != null)
                        oItem.Selected = true;
                }

                if (dateFormat != null)
                    txtDateFormat.Text = (string)dateFormat;

                if (timeFormat != null)
                    txtTimeFormat.Text = (string)timeFormat;

                if (reportsUrl != null)
                    txtReportsUrl.Text = (string)reportsUrl;

                if (reportsToNewWindow != null)
                    chkReportsToNewWindow.Checked = bool.Parse(reportsToNewWindow.ToString());

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Saves the settings for the module, in version 2.0 this is used to update/remove old settings that are no longer used.
        /// </summary>
        public override void UpdateSettings()
        {
            try
            {
                //Save settings
                ModuleController oController = new ModuleController();
               
                oController.UpdateModuleSetting(this.ModuleId, "fm_Events_AnonymousEdits", chkAllowAnonymousEdits.Checked.ToString());
                oController.UpdateModuleSetting(this.ModuleId, "fm_Events_PageSize", ddlPageSize.SelectedValue);
                oController.UpdateModuleSetting(this.ModuleId, "fm_Events_DateFormat", txtDateFormat.Text);
                oController.UpdateModuleSetting(this.ModuleId, "fm_Events_TimeFormat", txtTimeFormat.Text);
                oController.UpdateModuleSetting(this.ModuleId, "fm_Events_ReportsUrl", txtReportsUrl.Text);
                oController.UpdateModuleSetting(this.ModuleId, "fm_Events_ReportsToNewWindow", chkReportsToNewWindow.Checked.ToString());

                //refresh cache
                ModuleController.SynchronizeModule(ModuleId);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion

        /// <summary>
        /// Gets the description of module's dll.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public string GetModuleDllDescription(string filename)
        {
            string s = null;
            string path = null;
            path = AppDomain.CurrentDomain.BaseDirectory + "Bin\\";

            string pathfilename = null;
            pathfilename = path + filename;

            FileInfo fFile = new FileInfo(pathfilename);

            if (!fFile.Exists)
            {
                s = "File not found (" + pathfilename + ")";
            }
            else
            {
                s = pathfilename + " , " + FormatSize(fFile.Length) + " , " + fFile.LastWriteTime.ToString("dd.MM.yyyy HH:mm.ss");
            }

            return s;
        }

        /// <summary>
        /// Get formatted the file size .
        /// </summary>
        /// <param name="size">The size in bytes.</param>
        /// <returns></returns>
        public static string FormatSize(long size)
        {
            double s = size;
            string[] format = new string[] { "{0} bytes", "{0} KB", "{0} MB", "{0} GB", "{0} TB", "{0} PB", "{0} EB" };

            int i = 0;
            while (i < format.Length && s >= 1024)
            {
                s = (int)(100 * s / 1024) / 100.0;
                i++;
            }

            return string.Format(format[i], s);
        }
    }
}
