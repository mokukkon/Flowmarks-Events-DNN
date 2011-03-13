/*
 * Copyright (c) 2008-2009 Mika Kukkonen (http://www.flowmarks.com)
 * Copyright Contact: mika.kukkonen@flowmarks.com
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
using System.Configuration;
using System.Data;
using System.Xml;
using System.Web;
using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;

namespace flowmarks.Modules.Events.Components
{
    /// -----------------------------------------------------------------------------
    ///<summary>
    /// The Controller class for the Category
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    public class CategoryController
    {

        #region Constructors

        public CategoryController()
        {
        }
        #endregion

        #region Public Methods

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Adds a new Category object to the database!
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="objCategory">The CategoryInfo object</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void AddCategory(CategoryInfo objCategory)
        {
            DataProvider.Instance().AddCategory(objCategory.UserId, objCategory.ParentId, objCategory.Name, objCategory.Comments,
                objCategory.Label_Category,
                objCategory.Label_Label,
                objCategory.Label_EventDate,
                objCategory.Label_EventDate2,
                objCategory.Label_Measurement,
                objCategory.Label_Label2,
                objCategory.Label_Measurement2,
                objCategory.Label_ExternalId,
                objCategory.Label_Comments,
                objCategory.IsHidden);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// deletes an object from the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="ModuleId">The Id of the module</param>
        /// <param name="CategoryId">The Id of the item</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void DeleteCategory(int UserId, int CategoryId)
        {
            DataProvider.Instance().DeleteCategory(UserId, CategoryId);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// gets an object from the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="moduleId">The Id of the module</param>
        /// <param name="CategoryId">The Id of the item</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public CategoryInfo GetCategory(int UserId, int CategoryId)
        {
            return CBO.FillObject<CategoryInfo>(DataProvider.Instance().GetCategory(UserId, CategoryId));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// gets an object from the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="moduleId">The Id of the module</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public List<CategoryInfo> GetCategories(int UserId)
        {
            return CBO.FillCollection<CategoryInfo>(DataProvider.Instance().GetCategories(UserId));
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// gets an object from the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="moduleId">The Id of the module</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public List<CategoryInfo> GetCategories(int UserId, int ParentId, bool? IsHidden)
        {
            return CBO.FillCollection<CategoryInfo>(DataProvider.Instance().GetCategories(UserId, ParentId, IsHidden));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// saves an object to the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="objCategory">The CategoryInfo object</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void UpdateCategory(CategoryInfo objCategory)
        {
            DataProvider.Instance().UpdateCategory(objCategory.UserId, objCategory.CategoryId, objCategory.ParentId, objCategory.Name, objCategory.Comments,
                objCategory.Label_Category,
                objCategory.Label_Label,
                objCategory.Label_EventDate,
                objCategory.Label_EventDate2,
                objCategory.Label_Measurement,
                objCategory.Label_Label2,
                objCategory.Label_Measurement2,
                objCategory.Label_ExternalId,
                objCategory.Label_Comments,
                objCategory.IsHidden);
        }


        #endregion




    }
}
