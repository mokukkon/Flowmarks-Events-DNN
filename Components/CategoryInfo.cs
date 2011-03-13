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
    public class CategoryInfo
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

        // initialization
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
        #endregion
        public string Label_Category { get; set; }
        public string Label_Label { get; set; }
        public string Label_EventDate { get; set; }
        public string Label_EventDate2 { get; set; }
        public string Label_Measurement { get; set; }
        public string Label_Measurement2 { get; set; }
        public string Label_Label2 { get; set; }
        public string Label_ExternalId { get; set; }
        public string Label_Comments { get; set; }  
        
    }
}
