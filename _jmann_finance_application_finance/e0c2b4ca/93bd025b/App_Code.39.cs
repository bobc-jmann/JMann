#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\MonthlyGoalsDataLayerBase.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5D1D726668545BC1BFCCF8AAA2E57001B86F291E"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\MonthlyGoalsDataLayerBase.cs"
using System;
using System.Data;
using System.Data.SqlClient;
using jmann.BusinessObject;
 
namespace jmann.DataLayer.Base
{
     /// <summary>
     /// Base class for MonthlyGoalsDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the MonthlyGoalsDataLayer class 
     /// </summary>
     public class MonthlyGoalsDataLayerBase
     {
         // constructor 
         public MonthlyGoalsDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static MonthlyGoals SelectByPrimaryKey(int monthlyGoalsID)
         {
              string storedProcName = "[dbo].[pr_MonthlyGoals_SelectByPrimaryKey]";
              SqlConnection connection = Dbase.GetConnection();
              SqlCommand command = Dbase.GetCommand(storedProcName, connection);

              // parameters
              command.Parameters.AddWithValue("@monthlyGoalsID", monthlyGoalsID);

              DataSet ds = Dbase.GetDbaseDataSet(command);
              MonthlyGoals objMonthlyGoals = null;

              if (ds.Tables[0].Rows.Count > 0)
              {
                  objMonthlyGoals = new MonthlyGoals();
                  objMonthlyGoals.MonthlyGoalsID = (int)ds.Tables[0].Rows[0]["MonthlyGoalsID"];

                  if (ds.Tables[0].Rows[0]["Year"] != System.DBNull.Value)
                      objMonthlyGoals.Year = (Int16)ds.Tables[0].Rows[0]["Year"];
                  else
                      objMonthlyGoals.Year = null;

                  if (ds.Tables[0].Rows[0]["Month"] != System.DBNull.Value)
                      objMonthlyGoals.Month = (Int16)ds.Tables[0].Rows[0]["Month"];
                  else
                      objMonthlyGoals.Month = null;

                  if (ds.Tables[0].Rows[0]["LocationID"] != System.DBNull.Value)
                      objMonthlyGoals.LocationID = (int)ds.Tables[0].Rows[0]["LocationID"];
                  else
                      objMonthlyGoals.LocationID = null;


                  if (ds.Tables[0].Rows[0]["FurnitureTotal"] != System.DBNull.Value)
                      objMonthlyGoals.FurnitureTotal = (double)ds.Tables[0].Rows[0]["FurnitureTotal"];
                  else
                      objMonthlyGoals.FurnitureTotal = null;

                  if (ds.Tables[0].Rows[0]["JewelryTotal"] != System.DBNull.Value)
                      objMonthlyGoals.JewelryTotal = (double)ds.Tables[0].Rows[0]["JewelryTotal"];
                  else
                      objMonthlyGoals.JewelryTotal = null;

                  if (ds.Tables[0].Rows[0]["ElectricalTotal"] != System.DBNull.Value)
                      objMonthlyGoals.ElectricalTotal = (double)ds.Tables[0].Rows[0]["ElectricalTotal"];
                  else
                      objMonthlyGoals.ElectricalTotal = null;

                  if (ds.Tables[0].Rows[0]["HangTotal"] != System.DBNull.Value)
                      objMonthlyGoals.HangTotal = (double)ds.Tables[0].Rows[0]["HangTotal"];
                  else
                      objMonthlyGoals.HangTotal = null;

                  if (ds.Tables[0].Rows[0]["BinsTotal"] != System.DBNull.Value)
                      objMonthlyGoals.BinsTotal = (double)ds.Tables[0].Rows[0]["BinsTotal"];
                  else
                      objMonthlyGoals.BinsTotal = null;

                  if (ds.Tables[0].Rows[0]["MiscTotal"] != System.DBNull.Value)
                      objMonthlyGoals.MiscTotal = (double)ds.Tables[0].Rows[0]["MiscTotal"];
                  else
                      objMonthlyGoals.MiscTotal = null;

                  if (ds.Tables[0].Rows[0]["ShoesTotal"] != System.DBNull.Value)
                      objMonthlyGoals.ShoesTotal = (double)ds.Tables[0].Rows[0]["ShoesTotal"];
                  else
                      objMonthlyGoals.ShoesTotal = null;

                  if (ds.Tables[0].Rows[0]["BoutiqueTotal"] != System.DBNull.Value)
                      objMonthlyGoals.BoutiqueTotal = (double)ds.Tables[0].Rows[0]["BoutiqueTotal"];
                  else
                      objMonthlyGoals.BoutiqueTotal = null;

                  if (ds.Tables[0].Rows[0]["BooksTotal"] != System.DBNull.Value)
                      objMonthlyGoals.BooksTotal = (double)ds.Tables[0].Rows[0]["BooksTotal"];
                  else
                      objMonthlyGoals.BooksTotal = null;

                  if (ds.Tables[0].Rows[0]["Total"] != System.DBNull.Value)
                      objMonthlyGoals.Total = (decimal)ds.Tables[0].Rows[0]["Total"];
                  else
                      objMonthlyGoals.Total = null;

              }
              command.Dispose();
              connection.Close();
              connection.Dispose();
              ds.Dispose();

              return objMonthlyGoals;
         }
 
         /// <summary>
         /// Selects all MonthlyGoals 
         /// </summary> 
         public static MonthlyGoalsCollection SelectAll() 
         { 
             return SelectShared("[dbo].[pr_MonthlyGoals_SelectAll]", String.Empty, null); 
         } 
 
         /// <summary>
         /// Selects all MonthlyGoals by Location, related to column LocationID 
         /// </summary> 
         public static MonthlyGoalsCollection SelectMonthlyGoalsCollectionByLocation(int locationID) 
         { 
             return SelectShared("[dbo].[pr_MonthlyGoals_SelectAllByLocation]", "locationID", locationID); 
         } 
 
         /// <summary>
         /// Selects MonthlyGoalsID and Year columns for use with a DropDownList web control 
         /// </summary> 
         public static MonthlyGoalsCollection SelectMonthlyGoalsDropDownListData() 
         { 
              string storedProcName = "[dbo].[pr_MonthlyGoals_SelectAll]";
              SqlConnection connection = Dbase.GetConnection();
              SqlCommand command = Dbase.GetCommand(storedProcName, connection);
 
              DataSet ds = Dbase.GetDbaseDataSet(command); 
              MonthlyGoalsCollection objMonthlyGoalsCol = new MonthlyGoalsCollection(); 
              MonthlyGoals objMonthlyGoals; 
 
              if (ds.Tables[0].Rows.Count > 0) 
              { 
                 foreach(DataRow dr in ds.Tables[0].Rows) 
                 { 
                     objMonthlyGoals = new MonthlyGoals(); 
                     objMonthlyGoals.MonthlyGoalsID = (int)dr["MonthlyGoalsID"]; 
 
                     if (dr["Year"] != System.DBNull.Value) 
                         objMonthlyGoals.Year = (Int16)(dr["Year"]); 
                     else 
                         objMonthlyGoals.Year = null; 
 
                     objMonthlyGoalsCol.Add(objMonthlyGoals);
                 } 
              } 
 
              command.Dispose(); 
              connection.Close(); 
              connection.Dispose(); 
              ds.Dispose(); 
 
              return objMonthlyGoalsCol; 
         } 
 
         public static MonthlyGoalsCollection SelectShared(string storedProcName, string param, object paramValue) 
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
              MonthlyGoalsCollection objMonthlyGoalsCol = new MonthlyGoalsCollection(); 
              MonthlyGoals objMonthlyGoals; 
 
              if (ds.Tables[0].Rows.Count > 0) 
              { 
                 foreach(DataRow dr in ds.Tables[0].Rows) 
                 { 
                     objMonthlyGoals = new MonthlyGoals(); 
                     objMonthlyGoals.MonthlyGoalsID = (int)dr["MonthlyGoalsID"]; 
 
                     if (dr["Year"] != System.DBNull.Value) 
                         objMonthlyGoals.Year = (Int16)dr["Year"]; 
                     else 
                         objMonthlyGoals.Year = null; 
 
                     if (dr["Month"] != System.DBNull.Value) 
                         objMonthlyGoals.Month = (Int16)dr["Month"]; 
                     else 
                         objMonthlyGoals.Month = null; 
 
                     if (dr["LocationID"] != System.DBNull.Value) 
                         objMonthlyGoals.LocationID = (int)dr["LocationID"];
                     else
                         objMonthlyGoals.LocationID = null;
 
 
                     if (dr["FurnitureTotal"] != System.DBNull.Value) 
                         objMonthlyGoals.FurnitureTotal = (double)dr["FurnitureTotal"]; 
                     else 
                         objMonthlyGoals.FurnitureTotal = null; 
 
                     if (dr["JewelryTotal"] != System.DBNull.Value) 
                         objMonthlyGoals.JewelryTotal = (double)dr["JewelryTotal"]; 
                     else 
                         objMonthlyGoals.JewelryTotal = null; 
 
                     if (dr["ElectricalTotal"] != System.DBNull.Value) 
                         objMonthlyGoals.ElectricalTotal = (double)dr["ElectricalTotal"]; 
                     else 
                         objMonthlyGoals.ElectricalTotal = null; 
 
                     if (dr["HangTotal"] != System.DBNull.Value) 
                         objMonthlyGoals.HangTotal = (double)dr["HangTotal"]; 
                     else 
                         objMonthlyGoals.HangTotal = null; 
 
                     if (dr["BinsTotal"] != System.DBNull.Value) 
                         objMonthlyGoals.BinsTotal = (double)dr["BinsTotal"]; 
                     else 
                         objMonthlyGoals.BinsTotal = null; 
 
                     if (dr["MiscTotal"] != System.DBNull.Value) 
                         objMonthlyGoals.MiscTotal = (double)dr["MiscTotal"]; 
                     else 
                         objMonthlyGoals.MiscTotal = null; 
 
                     if (dr["ShoesTotal"] != System.DBNull.Value) 
                         objMonthlyGoals.ShoesTotal = (double)dr["ShoesTotal"]; 
                     else 
                         objMonthlyGoals.ShoesTotal = null; 
 
                     if (dr["BoutiqueTotal"] != System.DBNull.Value) 
                         objMonthlyGoals.BoutiqueTotal = (double)dr["BoutiqueTotal"]; 
                     else 
                         objMonthlyGoals.BoutiqueTotal = null; 
 
                     if (dr["BooksTotal"] != System.DBNull.Value) 
                         objMonthlyGoals.BooksTotal = (double)dr["BooksTotal"]; 
                     else 
                         objMonthlyGoals.BooksTotal = null; 
 
                     if (dr["Total"] != System.DBNull.Value) 
                         objMonthlyGoals.Total = (decimal)dr["Total"]; 
                     else 
                         objMonthlyGoals.Total = null; 
 
                     objMonthlyGoalsCol.Add(objMonthlyGoals);
                 } 
              } 
 
              command.Dispose(); 
              connection.Close(); 
              connection.Dispose(); 
              ds.Dispose(); 
 
              return objMonthlyGoalsCol; 
         } 
 
         /// <summary>
         /// Inserts a record 
         /// </summary> 
         public static int Insert(MonthlyGoals objMonthlyGoals) 
         { 
             string storedProcName = "[dbo].[pr_MonthlyGoals_Insert]"; 
             return InsertUpdate(objMonthlyGoals, false, storedProcName); 
         } 
 
         /// <summary>
         /// Updates a record 
         /// </summary>
         public static void Update(MonthlyGoals objMonthlyGoals) 
         { 
             string storedProcName = "[dbo].[pr_MonthlyGoals_Update]"; 
             InsertUpdate(objMonthlyGoals, true, storedProcName); 
         } 
 
         private static int InsertUpdate(MonthlyGoals objMonthlyGoals, bool isUpdate, string storedProcName) 
         { 
             SqlConnection connection = Dbase.GetConnection(); 
             SqlCommand command = Dbase.GetCommand(storedProcName, connection); 
 
             object year = objMonthlyGoals.Year; 
             object month = objMonthlyGoals.Month; 
             object locationID = objMonthlyGoals.LocationID; 
             object furnitureTotal = objMonthlyGoals.FurnitureTotal; 
             object jewelryTotal = objMonthlyGoals.JewelryTotal; 
             object electricalTotal = objMonthlyGoals.ElectricalTotal; 
             object hangTotal = objMonthlyGoals.HangTotal; 
             object binsTotal = objMonthlyGoals.BinsTotal; 
             object miscTotal = objMonthlyGoals.MiscTotal; 
             object shoesTotal = objMonthlyGoals.ShoesTotal; 
             object boutiqueTotal = objMonthlyGoals.BoutiqueTotal; 
             object booksTotal = objMonthlyGoals.BooksTotal; 
             object total = objMonthlyGoals.Total; 
 
             if (objMonthlyGoals.Year == null) 
                 year = System.DBNull.Value; 
 
             if (objMonthlyGoals.Month == null) 
                 month = System.DBNull.Value; 
 
             if (objMonthlyGoals.LocationID == null) 
                 locationID = System.DBNull.Value; 
 
             if (objMonthlyGoals.FurnitureTotal == null) 
                 furnitureTotal = System.DBNull.Value; 
 
             if (objMonthlyGoals.JewelryTotal == null) 
                 jewelryTotal = System.DBNull.Value; 
 
             if (objMonthlyGoals.ElectricalTotal == null) 
                 electricalTotal = System.DBNull.Value; 
 
             if (objMonthlyGoals.HangTotal == null) 
                 hangTotal = System.DBNull.Value; 
 
             if (objMonthlyGoals.BinsTotal == null) 
                 binsTotal = System.DBNull.Value; 
 
             if (objMonthlyGoals.MiscTotal == null) 
                 miscTotal = System.DBNull.Value; 
 
             if (objMonthlyGoals.ShoesTotal == null) 
                 shoesTotal = System.DBNull.Value; 
 
             if (objMonthlyGoals.BoutiqueTotal == null) 
                 boutiqueTotal = System.DBNull.Value; 
 
             if (objMonthlyGoals.BooksTotal == null) 
                 booksTotal = System.DBNull.Value; 
 
             if (objMonthlyGoals.Total == null) 
                 total = System.DBNull.Value; 
 
             // for update only 
             if (isUpdate) 
             { 
                 command.Parameters.AddWithValue("@monthlyGoalsID", objMonthlyGoals.MonthlyGoalsID); 
             } 
 
             command.Parameters.AddWithValue("@year", year); 
             command.Parameters.AddWithValue("@month", month); 
             command.Parameters.AddWithValue("@locationID", locationID); 
             command.Parameters.AddWithValue("@furnitureTotal", furnitureTotal); 
             command.Parameters.AddWithValue("@jewelryTotal", jewelryTotal); 
             command.Parameters.AddWithValue("@electricalTotal", electricalTotal); 
             command.Parameters.AddWithValue("@hangTotal", hangTotal); 
             command.Parameters.AddWithValue("@binsTotal", binsTotal); 
             command.Parameters.AddWithValue("@miscTotal", miscTotal); 
             command.Parameters.AddWithValue("@shoesTotal", shoesTotal); 
             command.Parameters.AddWithValue("@boutiqueTotal", boutiqueTotal); 
             command.Parameters.AddWithValue("@booksTotal", booksTotal); 
             command.Parameters.AddWithValue("@total", total); 
 
             // execute and return value 
             int newlyCreatedMonthlyGoalsID = objMonthlyGoals.MonthlyGoalsID; 
 
             if (isUpdate) 
                 command.ExecuteNonQuery(); 
             else 
                 newlyCreatedMonthlyGoalsID = (int)command.ExecuteScalar(); 
 
             command.Dispose(); 
             connection.Close(); 
             connection.Dispose(); 
 
             return newlyCreatedMonthlyGoalsID; 
         } 
 
         /// <summary>
         /// Deletes a record based on primary key(s) 
         /// </summary>
         public static void Delete(int monthlyGoalsID) 
         { 
             SqlConnection connection = Dbase.GetConnection(); 
             SqlCommand command = Dbase.GetCommand("[dbo].[pr_MonthlyGoals_Delete]", connection); 
 
             command.Parameters.AddWithValue("@monthlyGoalsID", monthlyGoalsID); 
 
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
