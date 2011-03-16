/*
 * Copyright (c) 2008-2009 flowmarks Inc (http://www.flowmarks.com)
 * Copyright Contact: webmaster@flowmarks.com
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
using System.Configuration;
using System.Data;
using DotNetNuke.Entities.Modules;

namespace flowmarks.Modules.Events.Components
{
    /// -----------------------------------------------------------------------------
    ///<summary>
    /// This class holds the information for the Event Module created by the flowmarks
    ///  each of these data elements are used to store the input information received from the visitors
    ///  of the current site.  This information is stored on a module level basis!
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// Version 1.0     September 5, 2010    Inital Creation of properties
    /// </history>
    /// -----------------------------------------------------------------------------
    public class EventInfo : DotNetNuke.Entities.Modules.IHydratable
    {

        #region Constructors

        // initialization
        public EventInfo()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets and sets the Module Id
        /// </summary>
        public int ModuleId { get; set; }

        /// <summary>
        /// Gets and sets the Item Id
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Gets and sets the userid of the person submitting the entry
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets and sets the categoryid of the event
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets and sets the category of the event
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets and sets the date and time of the event
        /// </summary>
        public DateTime EventDate { get; set; }

        /// <summary>
        /// Gets and sets the second date and time of the event
        /// </summary>
        public DateTime? EventDate2 { get; set; }

        /// <summary>
        /// Gets and sets the Label of the event
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets and sets the optional text field related to the event
        /// </summary>
        public string Label2 { get; set; }

        /// <summary>
        /// Gets and sets the measurement related to the event
        /// </summary>
        public double? Measurement { get; set; }

        /// <summary>
        /// Gets and sets the optional measurement field related to the event
        /// </summary>
        public double? Measurement2 { get; set; }

        /// <summary>
        /// Gets and sets the external identifier related to the event
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets and sets the comments provided by the submitter
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets and sets the Date when Created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets and sets the Date when Updated
        /// </summary>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// This field is an indicator noting if an event is deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        public string Label_Category { get; set; }
        public string Label_Label { get; set; }
        public string Label_Label2 { get; set; }
        public string Label_EventDate { get; set; }
        public string Label_EventDate2 { get; set; }
        public string Label_Measurement { get; set; }
        public string Label_Measurement2 { get; set; }
        public string Label_ExternalId { get; set; }
        public string Label_Comments { get; set; }

        #endregion
        #region IHydratable Implementation
        public int KeyID
        {
            get { return EventId; }
            set { EventId = value; }
        }

        public void Fill(IDataReader oReader)
        {
            EventId = (int)oReader["EventId"];
            ModuleId = (int)oReader["ModuleId"];
            UserId = (int)oReader["UserId"];
            CategoryId = (int)oReader["CategoryId"];

            if (oReader["Category"] != DBNull.Value)
                Category = Convert.ToString(oReader["Category"]);

            EventDate = (DateTime)oReader["EventDate"];

            if (oReader["EventDate2"] != DBNull.Value)
                EventDate2 = (DateTime)oReader["EventDate2"];

            Label = Convert.ToString(oReader["Label"]);

            if (oReader["Label2"] != DBNull.Value)
                Label2 = (string)(oReader["Label2"]);

            if (oReader["Measurement"] != DBNull.Value)
                Measurement = (double)oReader["Measurement"];

            if (oReader["Measurement2"] != DBNull.Value)
                Measurement2 = (double)oReader["Measurement2"];

            if (oReader["ExternalId"] != DBNull.Value)
                ExternalId = (string)(oReader["ExternalId"]);

            if (oReader["Comments"] != DBNull.Value)
                Comments = (string)(oReader["Comments"]);

            DateCreated = (DateTime)(oReader["DateCreated"]);

            if (oReader["DateModified"] != DBNull.Value)
                DateModified = (DateTime)oReader["DateModified"];

            IsDeleted = (bool)oReader["IsDeleted"];

            if (oReader["Label_Category"] != DBNull.Value)
                Label_Category = Convert.ToString(oReader["Label_Category"]);

            if (oReader["Label_Label"] != DBNull.Value)
                Label_Label = Convert.ToString(oReader["Label_Label"]);

            if (oReader["Label_Label2"] != DBNull.Value)
                Label_Label2 = Convert.ToString(oReader["Label_Label2"]);

            if (oReader["Label_EventDate"] != DBNull.Value)
                Label_EventDate = Convert.ToString(oReader["Label_EventDate"]);

            if (oReader["Label_EventDate2"] != DBNull.Value)
                Label_EventDate2 = Convert.ToString(oReader["Label_EventDate2"]);

            if (oReader["Label_Measurement"] != DBNull.Value)
                Label_Measurement = Convert.ToString(oReader["Label_Measurement"]);

            if (oReader["Label_Measurement2"] != DBNull.Value)
                Label_Measurement2 = Convert.ToString(oReader["Label_Measurement2"]);

            if (oReader["Label_ExternalId"] != DBNull.Value)
                Label_ExternalId = Convert.ToString(oReader["Label_ExternalId"]);

            if (oReader["Label_Comments"] != DBNull.Value)
                Label_Comments = Convert.ToString(oReader["Label_Comments"]);
        }
        #endregion
    }
}
