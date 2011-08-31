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
    /// The Controller class for the Event
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    public class EventController 
    {

    #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventController"/> class.
        /// </summary>
        public EventController()
        {
        }

    #endregion

    #region Public Methods

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Adds a new Event object to the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="objEvent">The EventInfo object</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void AddEvent(EventInfo objEvent)
        {
            DataProvider.Instance().AddEvent(objEvent.ModuleId, objEvent.UserId, objEvent.CategoryId, objEvent.Label, objEvent.EventDate, objEvent.EventDate2, objEvent.Measurement, objEvent.Label2, objEvent.Measurement2, objEvent.ExternalId, objEvent.Comments);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// deletes an object from the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="ModuleId">The Id of the module</param>
        /// <param name="UserId">The Id of the user</param>
        /// <param name="EventId">The Id of the event</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void DeleteEvent(int ModuleId, int UserId, int EventId) 
        {
            DataProvider.Instance().DeleteEvent(ModuleId,UserId,EventId);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// gets an object from the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="ModuleId">The Id of the module</param>
        /// <param name="UserId">The Id of the user</param>
        /// <param name="EventId">The Id of the item</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public EventInfo GetEvent(int ModuleId, int UserId, int EventId)
        {
            return CBO.FillObject < EventInfo >(DataProvider.Instance().GetEvent(ModuleId,UserId,EventId));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// gets an object from the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="ModuleId">The Id of the module</param>
        /// <param name="UserId">The Id of the user</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public List<EventInfo> GetEvents(int ModuleId, int UserId)
        {
            return CBO.FillCollection< EventInfo >(DataProvider.Instance().GetEvents(ModuleId, UserId));
        }

        /// <summary>
        /// gets an object from the database
        /// </summary>
        /// <param name="ModuleId">The Id of the module</param>
        /// <param name="UserId">The Id of the user</param>
        /// <param name="CategoryId">The Id of the category</param>
        /// <returns></returns>
        /// -----------------------------------------------------------------------------
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public List<EventInfo> GetEvents(int? ModuleId, int UserId, int? CategoryId)
        {
            return CBO.FillCollection<EventInfo>(DataProvider.Instance().GetEvents(ModuleId, UserId, CategoryId));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// saves an object to the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="objEvent">The EventInfo object</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void UpdateEvent(EventInfo objEvent)
        {
            DataProvider.Instance().UpdateEvent(objEvent.EventId, objEvent.ModuleId, objEvent.UserId, objEvent.CategoryId, objEvent.Label, objEvent.EventDate, objEvent.EventDate2, objEvent.Measurement, objEvent.Label2, objEvent.Measurement2, objEvent.ExternalId, objEvent.Comments);
        }
        /// <summary>
        /// This method will obtain a listing of currently waiting moderated posts
        /// </summary>
        /// <param name="moduleId">The id of the module</param>
        /// <returns></returns>
        public List<EventInfo> GetModeratedEvents(int moduleId)
        {
            return CBO.FillCollection<EventInfo>(DataProvider.Instance().GetModeratedEvents(moduleId));
        }

    #endregion



    }
}

