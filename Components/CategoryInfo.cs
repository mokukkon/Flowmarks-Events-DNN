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
    public class CategoryInfo : DotNetNuke.Entities.Modules.IHydratable
    {

        #region Private Members

        private int _CategoryId;
        private int _UserId;
        private int? _ParentId;
        private string _Name;
        private string _Comments;
        private DateTime _DateCreated;
        private DateTime _DateModified;
        private bool _isDeleted;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryInfo"/> class.
        /// </summary>
        public CategoryInfo()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets and sets the category Id
        /// </summary>
        public int CategoryId
        {
            get
            {
                return _CategoryId;
            }
            set
            {
                _CategoryId = value;
            }
        }

        /// <summary>
        /// Gets and sets the userid of the person owning the category
        /// </summary>
        public int UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                _UserId = value;
            }
        }

        /// <summary>
        /// Gets and sets the parent category in a hierarchy
        /// </summary>
        public int? ParentId
        {
            get
            {
                return _ParentId;
            }
            set
            {
                _ParentId = value;
            }
        }

        /// <summary>
        /// Gets and sets the name of the category
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }


        /// <summary>
        /// Gets and sets the comments provided by the submitter
        /// </summary>
        public string Comments
        {
            get
            {
                return _Comments;
            }
            set
            {
                _Comments = value;
            }
        }

        /// <summary>
        /// Gets and sets the Date when Created
        /// </summary>
        public DateTime DateCreated
        {
            get
            {
                return _DateCreated;
            }
            set
            {
                _DateCreated = value;
            }
        }

        /// <summary>
        /// Gets and sets the Date when Updated
        /// </summary>
        public DateTime DateModified
        {
            get
            {
                return _DateModified;
            }
            set
            {
                _DateModified = value;
            }
        }

        /// <summary>
        /// This field is an indicator noting if a category is deleted
        /// </summary>
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }

        /// <summary>
        /// This field is an indicator noting if a category is hidden
        /// </summary>
        public bool IsHidden { get; set;}

        /// <summary>
        /// Gets or sets the category label.
        /// </summary>
        /// <value>
        /// The label_ category.
        /// </value>
        public string Label_Category { get; set; }

        /// <summary>
        /// Gets or sets the label label.
        /// </summary>
        /// <value>
        /// The label label.
        /// </value>
        public string Label_Label { get; set; }

        /// <summary>
        /// Gets or sets the EventDate label.
        /// </summary>
        /// <value>
        /// The EventDate label.
        /// </value>
        public string Label_EventDate { get; set; }

        /// <summary>
        /// Gets or sets the EventDate2 label.
        /// </summary>
        /// <value>
        /// The EventDate2 label.
        /// </value>
        public string Label_EventDate2 { get; set; }

        /// <summary>
        /// Gets or sets the Measurement label.
        /// </summary>
        /// <value>
        /// The Measurement label.
        /// </value>
        public string Label_Measurement { get; set; }

        /// <summary>
        /// Gets or sets the Measurement2 label.
        /// </summary>
        /// <value>
        /// The Measurement2 label.
        /// </value>
        public string Label_Measurement2 { get; set; }

        /// <summary>
        /// Gets or sets the label2 label.
        /// </summary>
        /// <value>
        /// The label2 label.
        /// </value>
        public string Label_Label2 { get; set; }

        /// <summary>
        /// Gets or sets the external id label.
        /// </summary>
        /// <value>
        /// The external id label.
        /// </value>
        public string Label_ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the comments label.
        /// </summary>
        /// <value>
        /// The comments label.
        /// </value>
        public string Label_Comments { get; set; }

        #endregion

        #region IHydratable Implementation
        /// <summary>
        /// Gets or sets the IHydratable key ID.
        /// </summary>
        /// <value>
        /// The key ID.
        /// </value>
        public int KeyID
        {
            get { return CategoryId; }
            set { CategoryId = value; }
        }

        /// <summary>
        /// Fills the specified CategoryInfo.
        /// </summary>
        /// <param name="oReader">The o reader.</param>
             public void Fill(IDataReader oReader)
        {
            CategoryId = (int)oReader["CategoryId"];

            if (oReader["UserId"] != DBNull.Value)
                 UserId = (int)oReader["UserId"];

            if (oReader["ParentId"] != DBNull.Value)
                ParentId = (int)(oReader["ParentId"]);

            if (oReader["Name"] != DBNull.Value)
                Name = Convert.ToString(oReader["Name"]);

            if (oReader["Comments"] != DBNull.Value)
                Comments = (string)(oReader["Comments"]);

            if (oReader["DateCreated"] != DBNull.Value)
                DateCreated = (DateTime)(oReader["DateCreated"]);

            if (oReader["DateModified"] != DBNull.Value)
                DateModified = (DateTime)oReader["DateModified"];

            IsDeleted = (bool)oReader["IsDeleted"];
            IsHidden = (bool)oReader["IsHidden"];

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
