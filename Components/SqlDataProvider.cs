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
using System.Data;
using System.Data.SqlClient;

using Microsoft.ApplicationBlocks.Data;

using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;

namespace flowmarks.Modules.Events.Components
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The SqlDataProvider class is a SQL Server implementation of the abstract DataProvider
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    public class SqlDataProvider : DataProvider
    {

    #region Private Members

        private const string ProviderType = "data";
        private const string ModuleQualifier = "flowmarks_";

        private ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
        private string _connectionString;
        private string _providerPath;
        private string _objectQualifier;
        private string _databaseOwner;

    #endregion

    #region Constructors

        /// <summary>
        /// Constructs new SqlDataProvider instance
        /// </summary>
        public SqlDataProvider()
        {
            //Read the configuration specific information for this provider
            Provider objProvider = (Provider)_providerConfiguration.Providers[_providerConfiguration.DefaultProvider];

            //Read the attributes for this provider
            if ((objProvider.Attributes["connectionStringName"] != "") && (System.Configuration.ConfigurationManager.AppSettings[objProvider.Attributes["connectionStringName"]] != ""))
            {
                _connectionString = System.Configuration.ConfigurationManager.AppSettings[objProvider.Attributes["connectionStringName"]];
            }
            else
            {
                _connectionString = objProvider.Attributes["connectionString"];
            }

            _providerPath = objProvider.Attributes["providerPath"];

            _objectQualifier = objProvider.Attributes["objectQualifier"];

            if ((_objectQualifier != "") && (_objectQualifier.EndsWith("_") == false))
            {
                _objectQualifier += "_";
            }

            _databaseOwner = objProvider.Attributes["databaseOwner"];
            if ((_databaseOwner != "") && (_databaseOwner.EndsWith(".") == false))
            {
                _databaseOwner += ".";
            }
        }
    
    #endregion

    #region Properties

        /// <summary>
        /// Gets and sets the connection string
        /// </summary>
        public string ConnectionString
        {
            get {   return _connectionString;   }
        }

        /// <summary>
        /// Gets and sets the Provider path
        /// </summary>
        public string ProviderPath
        {
            get {   return _providerPath;   }
        }

        /// <summary>
        /// Gets and sets the Object qualifier
        /// </summary>
        public string ObjectQualifier
        {
            get {   return _objectQualifier;   }
        }

        /// <summary>
        /// Gets and sets the database ownere
        /// </summary>
        public string DatabaseOwner
        {
            get {   return _databaseOwner;   }
        }

    #endregion

    #region Private Methods

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gets the fully qualified name of the stored procedure
        /// </summary>
        /// <param name="name">The name of the stored procedure</param>
        /// <returns>The fully qualified name</returns>
        /// -----------------------------------------------------------------------------
        private string GetFullyQualifiedName(string name)
        {
            return DatabaseOwner + ObjectQualifier + ModuleQualifier + name;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gets the value for the field or DbNull if field has "null" value
        /// </summary>
        /// <param name="Field">The field to evaluate</param>
        /// <returns></returns>
        /// -----------------------------------------------------------------------------
        private Object GetNull(Object Field)
        {
            return Null.GetNull(Field, DBNull.Value);
        }

    #endregion

    #region Public Methods

        public override void AddEvent(int ModuleId, int UserId, int CategoryId, string Name, DateTime EventDate, DateTime? EventDate2, double? Measurement, string Label2, double? Measurement2, string ExternalId, string Comments)
        {
          int result = SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AddEvent"), 
              ModuleId, 
              UserId, 
              CategoryId, 
              Name,
              Label2,
              EventDate, 
              EventDate2, 
              Measurement,
              Measurement2,
              ExternalId, 
              Comments);
          if (result < 1) 
              throw new Exception("Failed to add new event");
        }

        public override void DeleteEvent(int ModuleId, int UserId, int EventId)
        {
            int result = SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("DeleteEvent"), ModuleId, UserId, EventId);
            if (result < 1)
                throw new Exception("Failed to delete event");
        }

        public override IDataReader GetEvent(int ModuleId, int UserId, int EventId)
        {
             return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetEvent"), ModuleId, UserId, EventId);
        }

        public override IDataReader GetEvents(int ModuleId, int UserId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetEvents"), ModuleId, UserId);
        }

        public override IDataReader GetEvents(int ModuleId, int UserId, int? CategoryId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetEventsByCategory"), 
                ModuleId, 
                UserId, 
                CategoryId);
        }

        public override void UpdateEvent(int EventId, int ModuleId, int UserId, int CategoryId, string Name, DateTime EventDate, DateTime? EventDate2, double? Measurement, string Label2, double? Measurement2, string ExternalId, string Comments)
        {
            int result = SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UpdateEvent"), 
                EventId, 
                ModuleId, 
                UserId, 
                CategoryId, 
                Name,
                Label2,
                EventDate, 
                EventDate2, 
                Measurement,
                Measurement2,
                ExternalId, 
                Comments);
            if (result < 1)
                throw new Exception("Failed to update event");            
        }

        public override IDataReader GetModeratedEvents(int moduleId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetModeratedEvents"), moduleId);
        }


        public override void AddCategory(int UserId, int? ParentId, string Name, string Comments,
                                        string Label_Category,
                                        string Label_Label,
                                        string Label_EventDate,
                                        string Label_EventDate2,
                                        string Label_Measurement,
                                        string Label_Label2,
                                        string Label_Measurement2,
                                        string Label_ExternalId,
                                        string Label_Comments,
                                        bool IsHidden
)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("AddCategory"), UserId, ParentId, 
                Name, 
                Comments,
                Label_Category, 
                Label_Label,
                Label_Label2,
                Label_EventDate, 
                Label_EventDate2, 
                Label_Measurement, 
                Label_Measurement2, 
                Label_ExternalId, 
                Label_Comments, 
                IsHidden
                );
        }

        public override void DeleteCategory(int UserId, int CategoryId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("DeleteCategory"), UserId, CategoryId);
        }

        public override IDataReader GetCategory(int UserId, int CategoryId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetCategory"), UserId, CategoryId);
        }

        public override IDataReader GetCategories(int UserId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetCategories"), UserId);
        }

        public override IDataReader GetCategories(int UserId, int? ParentId, bool? IsHidden)
        {
            return (IDataReader)SqlHelper.ExecuteReader(ConnectionString, GetFullyQualifiedName("GetCategories"), UserId, ParentId, IsHidden);
        }


        public override void UpdateCategory(int UserId, int CategoryId, int? ParentId, string Name, string Comments,
            string Label_Category,
            string Label_Label,
            string Label_EventDate,
            string Label_EventDate2,
            string Label_Measurement,
            string Label_Label2,
            string Label_Measurement2,
            string Label_ExternalId,
            string Label_Comments,
            bool IsHidden
            )
        {
            int status = SqlHelper.ExecuteNonQuery(ConnectionString, GetFullyQualifiedName("UpdateCategory"), CategoryId, UserId, ParentId, 
                Name, 
                Comments,
                Label_Category, 
                Label_Label,
                Label_Label2,
                Label_EventDate, 
                Label_EventDate2, 
                Label_Measurement, 
                Label_Measurement2, 
                Label_ExternalId, 
                Label_Comments, 
                IsHidden);
        }

    #endregion

    }
}
