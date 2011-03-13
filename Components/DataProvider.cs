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

        // singleton reference to the instantiated object 
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

        // return the provider
        public static  DataProvider Instance() 
        {
            return objProvider;
        }

    #endregion

    #region Abstract methods

        public abstract void AddEvent(int ModuleId, int UserId, int CategoryId, string Name, DateTime EventDate, DateTime? EventDate2, double? Measurement, string Label2, double? Measurement2, string ExternalId, string Comments);
        public abstract IDataReader GetEvent(int ModuleId, int UserId, int EventId);
        public abstract IDataReader GetEvents(int ModuleId, int UserId);
        public abstract IDataReader GetEvents(int ModuleId, int UserId, int? CategoryId);
        public abstract void UpdateEvent(int EventId, int ModuleId, int UserId, int CategoryId, string Name, DateTime EventDate, DateTime? EventDate2, double? Measurement, string Label2, double? Measurement2, string ExternalId, string Comments);
        public abstract void DeleteEvent(int ModuleId, int UserId, int EventId);
        public abstract IDataReader GetModeratedEvents(int moduleId);

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
        public abstract IDataReader GetCategory(int UserId, int CategoryId);
        public abstract IDataReader GetCategories(int UserId);
        public abstract IDataReader GetCategories(int UserId, int? ParentId, bool? IsHidden);
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
        public abstract void DeleteCategory(int UserId, int CategoryId);

    #endregion
    
    }
}
