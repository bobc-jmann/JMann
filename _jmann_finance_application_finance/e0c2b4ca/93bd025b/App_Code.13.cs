#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\LocationDataLayerBase.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6BC2AE860FCEB70A078054867E29B4C9BDB93684"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\LocationDataLayerBase.cs"
using System;
using System.Data;
using System.Data.SqlClient;
using jmann.BusinessObject;
 
namespace jmann.DataLayer.Base
{
     /// <summary>
     /// Base class for LocationDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the LocationDataLayer class 
     /// </summary>
     public class LocationDataLayerBase
     {
         // constructor 
         public LocationDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Location SelectByPrimaryKey(int locationID)
         {
              string storedProcName = "[dbo].[pr_Location_SelectByPrimaryKey]";
              SqlConnection connection = Dbase.GetConnection();
              SqlCommand command = Dbase.GetCommand(storedProcName, connection);

              // parameters
              command.Parameters.AddWithValue("@locationID", locationID);

              DataSet ds = Dbase.GetDbaseDataSet(command);
              Location objLocation = null;

              if (ds.Tables[0].Rows.Count > 0)
              {
                  objLocation = new Location();
                  objLocation.LocationID = (int)ds.Tables[0].Rows[0]["LocationID"];

                  if (ds.Tables[0].Rows[0]["Name"] != System.DBNull.Value)
                      objLocation.Name = (string)ds.Tables[0].Rows[0]["Name"];
                  else
                      objLocation.Name = null;

                  if (ds.Tables[0].Rows[0]["Description"] != System.DBNull.Value)
                      objLocation.Description = (string)ds.Tables[0].Rows[0]["Description"];
                  else
                      objLocation.Description = null;

                  if (ds.Tables[0].Rows[0]["FeetOfRack"] != System.DBNull.Value)
                      objLocation.FeetOfRack = (int)ds.Tables[0].Rows[0]["FeetOfRack"];
                  else
                      objLocation.FeetOfRack = null;

              }
              command.Dispose();
              connection.Close();
              connection.Dispose();
              ds.Dispose();

              return objLocation;
         }
 
         /// <summary>
         /// Selects all Location 
         /// </summary> 
         public static LocationCollection SelectAll() 
         { 
             return SelectShared("[dbo].[pr_Location_SelectAll]", String.Empty, null); 
         } 
 
         /// <summary>
         /// Selects LocationID and Name columns for use with a DropDownList web control 
         /// </summary> 
         public static LocationCollection SelectLocationDropDownListData() 
         { 
              string storedProcName = "[dbo].[pr_Location_SelectAll]";
              SqlConnection connection = Dbase.GetConnection();
              SqlCommand command = Dbase.GetCommand(storedProcName, connection);
 
              DataSet ds = Dbase.GetDbaseDataSet(command); 
              LocationCollection objLocationCol = new LocationCollection(); 
              Location objLocation; 
 
              if (ds.Tables[0].Rows.Count > 0) 
              { 
                 foreach(DataRow dr in ds.Tables[0].Rows) 
                 { 
                     objLocation = new Location(); 
                     objLocation.LocationID = (int)dr["LocationID"]; 
 
                     if (dr["Name"] != System.DBNull.Value) 
                         objLocation.Name = (string)(dr["Name"]); 
                     else 
                         objLocation.Name = null; 
 
                     objLocationCol.Add(objLocation);
                 } 
              } 
 
              command.Dispose(); 
              connection.Close(); 
              connection.Dispose(); 
              ds.Dispose(); 
 
              return objLocationCol; 
         } 
 
         public static LocationCollection SelectShared(string storedProcName, string param, object paramValue) 
         { 
              SqlConnection connection = Dbase.GetConnection(); 
              SqlCommand command = Dbase.GetCommand(storedProcName, connection); 
 
              DataSet ds = Dbase.GetDbaseDataSet(command); 
              LocationCollection objLocationCol = new LocationCollection(); 
              Location objLocation; 
 
              if (ds.Tables[0].Rows.Count > 0) 
              { 
                 foreach(DataRow dr in ds.Tables[0].Rows) 
                 { 
                     objLocation = new Location(); 
                     objLocation.LocationID = (int)dr["LocationID"]; 
 
                     if (dr["Name"] != System.DBNull.Value) 
                         objLocation.Name = dr["Name"].ToString(); 
                     else 
                         objLocation.Name = null; 
 
                     if (dr["Description"] != System.DBNull.Value) 
                         objLocation.Description = dr["Description"].ToString(); 
                     else 
                         objLocation.Description = null; 
 
                     if (dr["FeetOfRack"] != System.DBNull.Value) 
                         objLocation.FeetOfRack = (int)dr["FeetOfRack"]; 
                     else 
                         objLocation.FeetOfRack = null; 
 
                     objLocationCol.Add(objLocation);
                 } 
              } 
 
              command.Dispose(); 
              connection.Close(); 
              connection.Dispose(); 
              ds.Dispose(); 
 
              return objLocationCol; 
         } 
 
         /// <summary>
         /// Inserts a record 
         /// </summary> 
         public static int Insert(Location objLocation) 
         { 
             string storedProcName = "[dbo].[pr_Location_Insert]"; 
             return InsertUpdate(objLocation, false, storedProcName); 
         } 
 
         /// <summary>
         /// Updates a record 
         /// </summary>
         public static void Update(Location objLocation) 
         { 
             string storedProcName = "[dbo].[pr_Location_Update]"; 
             InsertUpdate(objLocation, true, storedProcName); 
         } 
 
         private static int InsertUpdate(Location objLocation, bool isUpdate, string storedProcName) 
         { 
             SqlConnection connection = Dbase.GetConnection(); 
             SqlCommand command = Dbase.GetCommand(storedProcName, connection); 
 
             object name = objLocation.Name; 
             object description = objLocation.Description; 
             object feetOfRack = objLocation.FeetOfRack; 
 
             if (String.IsNullOrEmpty(objLocation.Name)) 
                 name = System.DBNull.Value; 
 
             if (String.IsNullOrEmpty(objLocation.Description)) 
                 description = System.DBNull.Value; 
 
             if (objLocation.FeetOfRack == null) 
                 feetOfRack = System.DBNull.Value; 
 
             // for update only 
             if (isUpdate) 
             { 
                 command.Parameters.AddWithValue("@locationID", objLocation.LocationID); 
             } 
 
             command.Parameters.AddWithValue("@name", name); 
             command.Parameters.AddWithValue("@description", description); 
             command.Parameters.AddWithValue("@feetOfRack", feetOfRack); 
 
             // execute and return value 
             int newlyCreatedLocationID = objLocation.LocationID; 
 
             if (isUpdate) 
                 command.ExecuteNonQuery(); 
             else 
                 newlyCreatedLocationID = (int)command.ExecuteScalar(); 
 
             command.Dispose(); 
             connection.Close(); 
             connection.Dispose(); 
 
             return newlyCreatedLocationID; 
         } 
 
         /// <summary>
         /// Deletes a record based on primary key(s) 
         /// </summary>
         public static void Delete(int locationID) 
         { 
             SqlConnection connection = Dbase.GetConnection(); 
             SqlCommand command = Dbase.GetCommand("[dbo].[pr_Location_Delete]", connection); 
 
             command.Parameters.AddWithValue("@locationID", locationID); 
 
             // execute stored proc 
             command.ExecuteNonQuery(); 
             command.Dispose(); 
             connection.Close(); 
             connection.Dispose(); 
         } 
     }
}


#line default
#line hidden
