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
using DotNetNuke;
using System.Data;
using DotNetNuke.Framework;

namespace flowmarks.Modules.Events.Components
{
    /// <summary>
    /// Abstract DAL Class
    /// </summary>
    public abstract class DataProvider
    {

    #region Shared/Static Methods

        /// <summary>
        /// singleton reference to the instantiated object 
        /// </summary>
        static DataProvider  objProvider = null;

        // constructor
        static DataProvider()
        {
            CreateProvider();
        }

        // dynamically create provider
        private static void CreateProvider()
        {
            objProvider = (DataProvider)Reflection.CreateObject("data", "flowmarks.Modules.Events.Components", "");
        }

        /// <summary>
        /// Return the DataProvider instance.
        /// </summary>
        /// <returns></returns>
        public static  DataProvider Instance() 
        {
            return objProvider;
        }

    #endregion

    #region Abstract methods

        /// <summary>
        /// Adds the event.
        /// </summary>
        /// <param name="ModuleId">The module id.</param>
        /// <param name="UserId">The user id.</param>
        /// <param name="CategoryId">The category id.</param>
        /// <param name="Name">The name.</param>
        /// <param name="EventDate">The event date.</param>
        /// <param name="EventDate2">The event date2.</param>
        /// <param name="Measurement">The measurement.</param>
        /// <param name="Label2">The label2.</param>
        /// <param name="Measurement2">The measurement2.</param>
        /// <param name="ExternalId">The external id.</param>
        /// <param name="Comments">The comments.</param>
        public abstract void AddEvent(int ModuleId, int UserId, int CategoryId, string Name, DateTime EventDate, DateTime? EventDate2, double? Measurement, string Label2, double? Measurement2, string ExternalId, string Comments);

        /// <summary>
        /// Gets the event.
        /// </summary>
        /// <param name="ModuleId">The module id.</param>
        /// <param name="UserId">The user id.</param>
        /// <param name="EventId">The event id.</param>
        /// <returns></returns>
        public abstract IDataReader GetEvent(int ModuleId, int UserId, int EventId);

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="ModuleId">The module id.</param>
        /// <param name="UserId">The user id.</param>
        /// <returns></returns>
        public abstract IDataReader GetEvents(int ModuleId, int UserId);

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <param name="ModuleId">The module id.</param>
        /// <param name="UserId">The user id.</param>
        /// <param name="CategoryId">The category id.</param>
        /// <returns></returns>
        public abstract IDataReader GetEvents(int ModuleId, int UserId, int? CategoryId);

        /// <summary>
        /// Updates the event.
        /// </summary>
        /// <param name="EventId">The event id.</param>
        /// <param name="ModuleId">The module id.</param>
        /// <param name="UserId">The user id.</param>
        /// <param name="CategoryId">The category id.</param>
        /// <param name="Name">The name.</param>
        /// <param name="EventDate">The event date.</param>
        /// <param name="EventDate2">The event date2.</param>
        /// <param name="Measurement">The measurement.</param>
        /// <param name="Label2">The label2.</param>
        /// <param name="Measurement2">The measurement2.</param>
        /// <param name="ExternalId">The external id.</param>
        /// <param name="Comments">The comments.</param>
        public abstract void UpdateEvent(int EventId, int ModuleId, int UserId, int CategoryId, string Name, DateTime EventDate, DateTime? EventDate2, double? Measurement, string Label2, double? Measurement2, string ExternalId, string Comments);

        /// <summary>
        /// Deletes the event.
        /// </summary>
        /// <param name="ModuleId">The module id.</param>
        /// <param name="UserId">The user id.</param>
        /// <param name="EventId">The event id.</param>
        public abstract void DeleteEvent(int ModuleId, int UserId, int EventId);

        /// <summary>
        /// Gets the moderated events.
        /// </summary>
        /// <param name="moduleId">The module id.</param>
        /// <returns></returns>
        public abstract IDataReader GetModeratedEvents(int moduleId);

        /// <summary>
        /// Adds the category.
        /// </summary>
        /// <param name="UserId">The user id.</param>
        /// <param name="ParentId">The parent id.</param>
        /// <param name="Name">The name.</param>
        /// <param name="Comments">The comments.</param>
        /// <param name="Label_Category">The label_ category.</param>
        /// <param name="Label_Label">The label_ label.</param>
        /// <param name="Label_EventDate">The label_ event date.</param>
        /// <param name="Label_EventDate2">The label_ event date2.</param>
        /// <param name="Label_Measurement">The label_ measurement.</param>
        /// <param name="Label_Label2">The label_ label2.</param>
        /// <param name="Label_Measurement2">The label_ measurement2.</param>
        /// <param name="Label_ExternalId">The label_ external id.</param>
        /// <param name="Label_Comments">The label_ comments.</param>
        /// <param name="IsHidden">if set to <c>true</c> [is hidden].</param>
        public abstract void AddCategory(int UserId, int? ParentId, string Name, string Comments,
            string Label_Category,
            string Label_Label,
            string Label_EventDate,
            string Label_EventDate2,
            string Label_Measurement,
            string Label_Label2,
            string Label_Measurement2,
            string Label_ExternalId,
            string Label_Comments,
            bool IsHidden);

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="UserId">The user id.</param>
        /// <param name="CategoryId">The category id.</param>
        /// <returns></returns>
        public abstract IDataReader GetCategory(int UserId, int CategoryId);

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <param name="UserId">The user id.</param>
        /// <returns></returns>
        public abstract IDataReader GetCategories(int UserId);

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <param name="UserId">The user id.</param>
        /// <param name="ParentId">The parent id.</param>
        /// <param name="IsHidden">The is hidden.</param>
        /// <returns></returns>
        public abstract IDataReader GetCategories(int UserId, int? ParentId, bool? IsHidden);

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="UserId">The user id.</param>
        /// <param name="CategoryId">The category id.</param>
        /// <param name="ParentId">The parent id.</param>
        /// <param name="Name">The name.</param>
        /// <param name="Comments">The comments.</param>
        /// <param name="Label_Category">The label_ category.</param>
        /// <param name="Label_Label">The label_ label.</param>
        /// <param name="Label_EventDate">The label_ event date.</param>
        /// <param name="Label_EventDate2">The label_ event date2.</param>
        /// <param name="Label_Measurement">The label_ measurement.</param>
        /// <param name="Label_Label2">The label_ label2.</param>
        /// <param name="Label_Measurement2">The label_ measurement2.</param>
        /// <param name="Label_ExternalId">The label_ external id.</param>
        /// <param name="Label_Comments">The label_ comments.</param>
        /// <param name="IsHidden">if set to <c>true</c> [is hidden].</param>
        public abstract void UpdateCategory(int UserId, int CategoryId, int? ParentId, string Name, string Comments,
            string Label_Category,
            string Label_Label,
            string Label_EventDate,
            string Label_EventDate2,
            string Label_Measurement,
            string Label_Label2,
            string Label_Measurement2,
            string Label_ExternalId,
            string Label_Comments,
            bool IsHidden);

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="UserId">The user id.</param>
        /// <param name="CategoryId">The category id.</param>
        public abstract void DeleteCategory(int UserId, int CategoryId);

    #endregion
    
    }
}
