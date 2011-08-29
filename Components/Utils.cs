using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace flowmarks.Modules.Events.Components
{
    /// <summary>
    /// Static utility methods
    /// </summary>
    public class Utils
    {

        /// <summary>
        /// Format a string as a fixed length UI string
        /// </summary>
        /// <param name="original">The original string.</param>
        /// <param name="maxlength">The maximum length.</param>
        /// <returns></returns>
        public static string UIFixedLength(string original, int maxlength)
        {
            string trailString = "...";
            string result = null;

            if (original.Length > maxlength && maxlength > 3)
            {
                result = original.Substring(0, (maxlength - trailString.Length)) + trailString;
            }
            else
            {
                result = original;
            }

            return result;

        }

        /// <summary>
        /// Styles and shows the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageBox">The message box.</param>
        /// <param name="lblMessage">Label control.</param>
        public static void ShowError(string message, HtmlGenericControl messageBox, Label lblMessage)
        {
            messageBox.Style.Add("background-color", "#cd5c5c");
            messageBox.Style.Add("color", "white");
            lblMessage.Text = Utils.UIFixedLength(message, 60);
            lblMessage.ToolTip = message;
            lblMessage.CssClass = "msgError";
            messageBox.Visible = true;
        }

        /// <summary>
        /// Styles and shows the info message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageBox">The message box.</param>
        /// <param name="lblMessage">Label control.</param>
        public static void ShowInfo(string message, HtmlGenericControl messageBox, Label lblMessage)
        {
            messageBox.Style.Add("background-color", "#fad163");
            messageBox.Style.Add("color", "black");
            //lblMessage.Text = Utils.UIFixedLength(message, 60);
            lblMessage.Text = message;
            lblMessage.CssClass = "msgInfo";
            messageBox.Visible = true;
        }

        /// <summary>
        /// Formats the report URL.
        /// </summary>
        /// <param name="url">The report URL.</param>
        /// <param name="Request">The request.</param>
        /// <param name="SkinSrc">The skin SRC.</param>
        /// <param name="ConSrc">The con SRC.</param>
        /// <returns>Report url with skin and container in querystring</returns>
        public static string FormatReportUrl(string url, HttpRequest Request, string SkinSrc, string ConSrc)
        {
            if (url.Contains(Request.Url.Scheme))
                ; // url = Server.UrlEncode(url);
            else
                url = Request.Url.Scheme + "://" + Request.Url.Host + url;

            if (!url.ToLower().Contains("?"))
                url += "?";

            if (!url.ToLower().Contains("skinsrc"))
                url += SkinSrc;

            if (!url.ToLower().Contains("containersrc"))
                url += ConSrc;

            return url;
        }
    }
}
