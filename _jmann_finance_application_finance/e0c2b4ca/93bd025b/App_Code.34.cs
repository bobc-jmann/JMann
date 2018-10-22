#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\CreditCardRecordDataLayerBase.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ACD6B004F23682113856A53FE55DF81DF821198F"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\CreditCardRecordDataLayerBase.cs"
using System;
using System.Data;
using System.Data.SqlClient;
using jmann.BusinessObject;
 
namespace jmann.DataLayer.Base
{
     /// <summary>
     /// Base class for CreditCardRecordDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the CreditCardRecordDataLayer class 
     /// </summary>
     public class CreditCardRecordDataLayerBase
     {
         // constructor 
         public CreditCardRecordDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static CreditCardRecord SelectByPrimaryKey(int creditCardRecordID)
         {
              string storedProcName = "[dbo].[pr_CreditCardRecord_SelectByPrimaryKey]";
              SqlConnection connection = Dbase.GetConnection();
              SqlCommand command = Dbase.GetCommand(storedProcName, connection);

              // parameters
              command.Parameters.AddWithValue("@creditCardRecordID", creditCardRecordID);

              DataSet ds = Dbase.GetDbaseDataSet(command);
              CreditCardRecord objCreditCardRecord = null;

              if (ds.Tables[0].Rows.Count > 0)
              {
                  objCreditCardRecord = new CreditCardRecord();
                  objCreditCardRecord.CreditCardRecordID = (int)ds.Tables[0].Rows[0]["CreditCardRecordID"];

                  if (ds.Tables[0].Rows[0]["Date"] != System.DBNull.Value)
                      objCreditCardRecord.Date = (DateTime)ds.Tables[0].Rows[0]["Date"];
                  else
                      objCreditCardRecord.Date = null;

                  if (ds.Tables[0].Rows[0]["LocationID"] != System.DBNull.Value)
                      objCreditCardRecord.LocationID = (int)ds.Tables[0].Rows[0]["LocationID"];
                  else
                      objCreditCardRecord.LocationID = null;


                  if (ds.Tables[0].Rows[0]["AtmCount"] != System.DBNull.Value)
                      objCreditCardRecord.AtmCount = (int)ds.Tables[0].Rows[0]["AtmCount"];
                  else
                      objCreditCardRecord.AtmCount = null;

                  if (ds.Tables[0].Rows[0]["AtmTotal"] != System.DBNull.Value)
                      objCreditCardRecord.AtmTotal = (decimal)ds.Tables[0].Rows[0]["AtmTotal"];
                  else
                      objCreditCardRecord.AtmTotal = null;

                  if (ds.Tables[0].Rows[0]["VisaCount"] != System.DBNull.Value)
                      objCreditCardRecord.VisaCount = (int)ds.Tables[0].Rows[0]["VisaCount"];
                  else
                      objCreditCardRecord.VisaCount = null;

                  if (ds.Tables[0].Rows[0]["VisaTotal"] != System.DBNull.Value)
                      objCreditCardRecord.VisaTotal = (decimal)ds.Tables[0].Rows[0]["VisaTotal"];
                  else
                      objCreditCardRecord.VisaTotal = null;

                  if (ds.Tables[0].Rows[0]["MastercardCount"] != System.DBNull.Value)
                      objCreditCardRecord.MastercardCount = (int)ds.Tables[0].Rows[0]["MastercardCount"];
                  else
                      objCreditCardRecord.MastercardCount = null;

                  if (ds.Tables[0].Rows[0]["MastercardTotal"] != System.DBNull.Value)
                      objCreditCardRecord.MastercardTotal = (decimal)ds.Tables[0].Rows[0]["MastercardTotal"];
                  else
                      objCreditCardRecord.MastercardTotal = null;

                  if (ds.Tables[0].Rows[0]["DiscoverCount"] != System.DBNull.Value)
                      objCreditCardRecord.DiscoverCount = (int)ds.Tables[0].Rows[0]["DiscoverCount"];
                  else
                      objCreditCardRecord.DiscoverCount = null;

                  if (ds.Tables[0].Rows[0]["DiscoverTotal"] != System.DBNull.Value)
                      objCreditCardRecord.DiscoverTotal = (decimal)ds.Tables[0].Rows[0]["DiscoverTotal"];
                  else
                      objCreditCardRecord.DiscoverTotal = null;

              }
              command.Dispose();
              connection.Close();
              connection.Dispose();
              ds.Dispose();

              return objCreditCardRecord;
         }
 
         /// <summary>
         /// Selects all CreditCardRecord 
         /// </summary> 
         public static CreditCardRecordCollection SelectAll() 
         { 
             return SelectShared("[dbo].[pr_CreditCardRecord_SelectAll]", String.Empty, null); 
         } 
 
         /// <summary>
         /// Selects all CreditCardRecord by Location, related to column LocationID 
         /// </summary> 
         public static CreditCardRecordCollection SelectCreditCardRecordCollectionByLocation(int locationID) 
         { 
             return SelectShared("[dbo].[pr_CreditCardRecord_SelectAllByLocation]", "locationID", locationID); 
         } 
 
         /// <summary>
         /// Selects CreditCardRecordID and Date columns for use with a DropDownList web control 
         /// </summary> 
         public static CreditCardRecordCollection SelectCreditCardRecordDropDownListData() 
         { 
              string storedProcName = "[dbo].[pr_CreditCardRecord_SelectAll]";
              SqlConnection connection = Dbase.GetConnection();
              SqlCommand command = Dbase.GetCommand(storedProcName, connection);
 
              DataSet ds = Dbase.GetDbaseDataSet(command); 
              CreditCardRecordCollection objCreditCardRecordCol = new CreditCardRecordCollection(); 
              CreditCardRecord objCreditCardRecord; 
 
              if (ds.Tables[0].Rows.Count > 0) 
              { 
                 foreach(DataRow dr in ds.Tables[0].Rows) 
                 { 
                     objCreditCardRecord = new CreditCardRecord(); 
                     objCreditCardRecord.CreditCardRecordID = (int)dr["CreditCardRecordID"]; 
 
                     if (dr["Date"] != System.DBNull.Value) 
                         objCreditCardRecord.Date = (DateTime)(dr["Date"]); 
                     else 
                         objCreditCardRecord.Date = null; 
 
                     objCreditCardRecordCol.Add(objCreditCardRecord);
                 } 
              } 
 
              command.Dispose(); 
              connection.Close(); 
              connection.Dispose(); 
              ds.Dispose(); 
 
              return objCreditCardRecordCol; 
         } 
 
         public static CreditCardRecordCollection SelectShared(string storedProcName, string param, object paramValue) 
         { 
              SqlConnection connection = Dbase.GetConnection(); 
              SqlCommand command = Dbase.GetCommand(storedProcName, connection); 
 
              // parameters 
              switch (param) 
              { 
                  case "locationID": 
                      command.Parameters.AddWithValue("@locationID", paramValue); 
                      break; 
                  default: 
                      break; 
              } 
 
              DataSet ds = Dbase.GetDbaseDataSet(command); 
              CreditCardRecordCollection objCreditCardRecordCol = new CreditCardRecordCollection(); 
              CreditCardRecord objCreditCardRecord; 
 
              if (ds.Tables[0].Rows.Count > 0) 
              { 
                 foreach(DataRow dr in ds.Tables[0].Rows) 
                 { 
                     objCreditCardRecord = new CreditCardRecord(); 
                     objCreditCardRecord.CreditCardRecordID = (int)dr["CreditCardRecordID"]; 
 
                     if (dr["Date"] != System.DBNull.Value) 
                         objCreditCardRecord.Date = (DateTime)dr["Date"]; 
                     else 
                         objCreditCardRecord.Date = null; 
 
                     if (dr["LocationID"] != System.DBNull.Value) 
                         objCreditCardRecord.LocationID = (int)dr["LocationID"];
                     else
                         objCreditCardRecord.LocationID = null;
 
 
                     if (dr["AtmCount"] != System.DBNull.Value) 
                         objCreditCardRecord.AtmCount = (int)dr["AtmCount"]; 
                     else 
                         objCreditCardRecord.AtmCount = null; 
 
                     if (dr["AtmTotal"] != System.DBNull.Value) 
                         objCreditCardRecord.AtmTotal = (decimal)dr["AtmTotal"]; 
                     else 
                         objCreditCardRecord.AtmTotal = null; 
 
                     if (dr["VisaCount"] != System.DBNull.Value) 
                         objCreditCardRecord.VisaCount = (int)dr["VisaCount"]; 
                     else 
                         objCreditCardRecord.VisaCount = null; 
 
                     if (dr["VisaTotal"] != System.DBNull.Value) 
                         objCreditCardRecord.VisaTotal = (decimal)dr["VisaTotal"]; 
                     else 
                         objCreditCardRecord.VisaTotal = null; 
 
                     if (dr["MastercardCount"] != System.DBNull.Value) 
                         objCreditCardRecord.MastercardCount = (int)dr["MastercardCount"]; 
                     else 
                         objCreditCardRecord.MastercardCount = null; 
 
                     if (dr["MastercardTotal"] != System.DBNull.Value) 
                         objCreditCardRecord.MastercardTotal = (decimal)dr["MastercardTotal"]; 
                     else 
                         objCreditCardRecord.MastercardTotal = null; 
 
                     if (dr["DiscoverCount"] != System.DBNull.Value) 
                         objCreditCardRecord.DiscoverCount = (int)dr["DiscoverCount"]; 
                     else 
                         objCreditCardRecord.DiscoverCount = null; 
 
                     if (dr["DiscoverTotal"] != System.DBNull.Value) 
                         objCreditCardRecord.DiscoverTotal = (decimal)dr["DiscoverTotal"]; 
                     else 
                         objCreditCardRecord.DiscoverTotal = null; 
 
                     objCreditCardRecordCol.Add(objCreditCardRecord);
                 } 
              } 
 
              command.Dispose(); 
              connection.Close(); 
              connection.Dispose(); 
              ds.Dispose(); 
 
              return objCreditCardRecordCol; 
         } 
 
         /// <summary>
         /// Inserts a record 
         /// </summary> 
         public static int Insert(CreditCardRecord objCreditCardRecord) 
         { 
             string storedProcName = "[dbo].[pr_CreditCardRecord_Insert]"; 
             return InsertUpdate(objCreditCardRecord, false, storedProcName); 
         } 
 
         /// <summary>
         /// Updates a record 
         /// </summary>
         public static void Update(CreditCardRecord objCreditCardRecord) 
         { 
             string storedProcName = "[dbo].[pr_CreditCardRecord_Update]"; 
             InsertUpdate(objCreditCardRecord, true, storedProcName); 
         } 
 
         private static int InsertUpdate(CreditCardRecord objCreditCardRecord, bool isUpdate, string storedProcName) 
         { 
             SqlConnection connection = Dbase.GetConnection(); 
             SqlCommand command = Dbase.GetCommand(storedProcName, connection); 
 
             object date = objCreditCardRecord.Date; 
             object locationID = objCreditCardRecord.LocationID; 
             object atmCount = objCreditCardRecord.AtmCount; 
             object atmTotal = objCreditCardRecord.AtmTotal; 
             object visaCount = objCreditCardRecord.VisaCount; 
             object visaTotal = objCreditCardRecord.VisaTotal; 
             object mastercardCount = objCreditCardRecord.MastercardCount; 
             object mastercardTotal = objCreditCardRecord.MastercardTotal; 
             object discoverCount = objCreditCardRecord.DiscoverCount; 
             object discoverTotal = objCreditCardRecord.DiscoverTotal; 
 
             if (objCreditCardRecord.Date == null) 
                 date = System.DBNull.Value; 
 
             if (objCreditCardRecord.LocationID == null) 
                 locationID = System.DBNull.Value; 
 
             if (objCreditCardRecord.AtmCount == null) 
                 atmCount = System.DBNull.Value; 
 
             if (objCreditCardRecord.AtmTotal == null) 
                 atmTotal = System.DBNull.Value; 
 
             if (objCreditCardRecord.VisaCount == null) 
                 visaCount = System.DBNull.Value; 
 
             if (objCreditCardRecord.VisaTotal == null) 
                 visaTotal = System.DBNull.Value; 
 
             if (objCreditCardRecord.MastercardCount == null) 
                 mastercardCount = System.DBNull.Value; 
 
             if (objCreditCardRecord.MastercardTotal == null) 
                 mastercardTotal = System.DBNull.Value; 
 
             if (objCreditCardRecord.DiscoverCount == null) 
                 discoverCount = System.DBNull.Value; 
 
             if (objCreditCardRecord.DiscoverTotal == null) 
                 discoverTotal = System.DBNull.Value; 
 
             // for update only 
             if (isUpdate) 
             { 
                 command.Parameters.AddWithValue("@creditCardRecordID", objCreditCardRecord.CreditCardRecordID); 
             } 
 
             command.Parameters.AddWithValue("@date", date); 
             command.Parameters.AddWithValue("@locationID", locationID); 
             command.Parameters.AddWithValue("@atmCount", atmCount); 
             command.Parameters.AddWithValue("@atmTotal", atmTotal); 
             command.Parameters.AddWithValue("@visaCount", visaCount); 
             command.Parameters.AddWithValue("@visaTotal", visaTotal); 
             command.Parameters.AddWithValue("@mastercardCount", mastercardCount); 
             command.Parameters.AddWithValue("@mastercardTotal", mastercardTotal); 
             command.Parameters.AddWithValue("@discoverCount", discoverCount); 
             command.Parameters.AddWithValue("@discoverTotal", discoverTotal); 
 
             // execute and return value 
             int newlyCreatedCreditCardRecordID = objCreditCardRecord.CreditCardRecordID; 
 
             if (isUpdate) 
                 command.ExecuteNonQuery(); 
             else 
                 newlyCreatedCreditCardRecordID = (int)command.ExecuteScalar(); 
 
             command.Dispose(); 
             connection.Close(); 
             connection.Dispose(); 
 
             return newlyCreatedCreditCardRecordID; 
         } 
 
         /// <summary>
         /// Deletes a record based on primary key(s) 
         /// </summary>
         public static void Delete(int creditCardRecordID) 
         { 
             SqlConnection connection = Dbase.GetConnection(); 
             SqlCommand command = Dbase.GetCommand("[dbo].[pr_CreditCardRecord_Delete]", connection); 
 
             command.Parameters.AddWithValue("@creditCardRecordID", creditCardRecordID); 
 
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
