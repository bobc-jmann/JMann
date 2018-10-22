#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\ZTapeRecordDataLayerBase.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5D0311B70C17952D0235A4F9ACF567F68023987F"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\ZTapeRecordDataLayerBase.cs"
using System;
using System.Data;
using System.Data.SqlClient;
using jmann.BusinessObject;
 
namespace jmann.DataLayer.Base
{
     /// <summary>
     /// Base class for ZTapeRecordDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the ZTapeRecordDataLayer class 
     /// </summary>
     public class ZTapeRecordDataLayerBase
     {
         // constructor 
         public ZTapeRecordDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static ZTapeRecord SelectByPrimaryKey(int zTapeRecordID)
         {
              string storedProcName = "[dbo].[pr_ZTapeRecord_SelectByPrimaryKey]";
              SqlConnection connection = Dbase.GetConnection();
              SqlCommand command = Dbase.GetCommand(storedProcName, connection);

              // parameters
              command.Parameters.AddWithValue("@zTapeRecordID", zTapeRecordID);

              DataSet ds = Dbase.GetDbaseDataSet(command);
              ZTapeRecord objZTapeRecord = null;

              if (ds.Tables[0].Rows.Count > 0)
              {
                  objZTapeRecord = new ZTapeRecord();
                  objZTapeRecord.ZTapeRecordID = (int)ds.Tables[0].Rows[0]["ZTapeRecordID"];

                  if (ds.Tables[0].Rows[0]["Date"] != System.DBNull.Value)
                      objZTapeRecord.Date = (DateTime)ds.Tables[0].Rows[0]["Date"];
                  else
                      objZTapeRecord.Date = null;

                  if (ds.Tables[0].Rows[0]["LocationID"] != System.DBNull.Value)
                      objZTapeRecord.LocationID = (int)ds.Tables[0].Rows[0]["LocationID"];
                  else
                      objZTapeRecord.LocationID = null;


                  if (ds.Tables[0].Rows[0]["Register"] != System.DBNull.Value)
                      objZTapeRecord.Register = (int)ds.Tables[0].Rows[0]["Register"];
                  else
                      objZTapeRecord.Register = null;

                  if (ds.Tables[0].Rows[0]["AddAdjustment"] != System.DBNull.Value)
                      objZTapeRecord.AddAdjustment = (decimal)ds.Tables[0].Rows[0]["AddAdjustment"];
                  else
                      objZTapeRecord.AddAdjustment = null;

                  if (ds.Tables[0].Rows[0]["AddComment"] != System.DBNull.Value)
                      objZTapeRecord.AddComment = (string)ds.Tables[0].Rows[0]["AddComment"];
                  else
                      objZTapeRecord.AddComment = null;

                  if (ds.Tables[0].Rows[0]["CashAdjustment"] != System.DBNull.Value)
                      objZTapeRecord.CashAdjustment = (decimal)ds.Tables[0].Rows[0]["CashAdjustment"];
                  else
                      objZTapeRecord.CashAdjustment = null;

                  if (ds.Tables[0].Rows[0]["CashComment"] != System.DBNull.Value)
                      objZTapeRecord.CashComment = (string)ds.Tables[0].Rows[0]["CashComment"];
                  else
                      objZTapeRecord.CashComment = null;

                  if (ds.Tables[0].Rows[0]["Discount1StCount"] != System.DBNull.Value)
                      objZTapeRecord.Discount1StCount = (int)ds.Tables[0].Rows[0]["Discount1StCount"];
                  else
                      objZTapeRecord.Discount1StCount = null;

                  if (ds.Tables[0].Rows[0]["Discount1StTotal"] != System.DBNull.Value)
                      objZTapeRecord.Discount1StTotal = (decimal)ds.Tables[0].Rows[0]["Discount1StTotal"];
                  else
                      objZTapeRecord.Discount1StTotal = null;

                  if (ds.Tables[0].Rows[0]["Discount2StCount"] != System.DBNull.Value)
                      objZTapeRecord.Discount2StCount = (int)ds.Tables[0].Rows[0]["Discount2StCount"];
                  else
                      objZTapeRecord.Discount2StCount = null;

                  if (ds.Tables[0].Rows[0]["Discount2StTotal"] != System.DBNull.Value)
                      objZTapeRecord.Discount2StTotal = (decimal)ds.Tables[0].Rows[0]["Discount2StTotal"];
                  else
                      objZTapeRecord.Discount2StTotal = null;

                  if (ds.Tables[0].Rows[0]["ChargeCount"] != System.DBNull.Value)
                      objZTapeRecord.ChargeCount = (int)ds.Tables[0].Rows[0]["ChargeCount"];
                  else
                      objZTapeRecord.ChargeCount = null;

                  if (ds.Tables[0].Rows[0]["ChargeTotal"] != System.DBNull.Value)
                      objZTapeRecord.ChargeTotal = (decimal)ds.Tables[0].Rows[0]["ChargeTotal"];
                  else
                      objZTapeRecord.ChargeTotal = null;

                  if (ds.Tables[0].Rows[0]["CashCount"] != System.DBNull.Value)
                      objZTapeRecord.CashCount = (int)ds.Tables[0].Rows[0]["CashCount"];
                  else
                      objZTapeRecord.CashCount = null;

                  if (ds.Tables[0].Rows[0]["CashTotal"] != System.DBNull.Value)
                      objZTapeRecord.CashTotal = (decimal)ds.Tables[0].Rows[0]["CashTotal"];
                  else
                      objZTapeRecord.CashTotal = null;

                  if (ds.Tables[0].Rows[0]["Tax"] != System.DBNull.Value)
                      objZTapeRecord.Tax = (decimal)ds.Tables[0].Rows[0]["Tax"];
                  else
                      objZTapeRecord.Tax = null;

                  if (ds.Tables[0].Rows[0]["Discount1ItCount"] != System.DBNull.Value)
                      objZTapeRecord.Discount1ItCount = (int)ds.Tables[0].Rows[0]["Discount1ItCount"];
                  else
                      objZTapeRecord.Discount1ItCount = null;

                  if (ds.Tables[0].Rows[0]["Discount1ItTotal"] != System.DBNull.Value)
                      objZTapeRecord.Discount1ItTotal = (decimal)ds.Tables[0].Rows[0]["Discount1ItTotal"];
                  else
                      objZTapeRecord.Discount1ItTotal = null;

                  if (ds.Tables[0].Rows[0]["Discount2ItCount"] != System.DBNull.Value)
                      objZTapeRecord.Discount2ItCount = (int)ds.Tables[0].Rows[0]["Discount2ItCount"];
                  else
                      objZTapeRecord.Discount2ItCount = null;

                  if (ds.Tables[0].Rows[0]["Discount2ItTotal"] != System.DBNull.Value)
                      objZTapeRecord.Discount2ItTotal = (decimal)ds.Tables[0].Rows[0]["Discount2ItTotal"];
                  else
                      objZTapeRecord.Discount2ItTotal = null;

                  if (ds.Tables[0].Rows[0]["ReturnsCount"] != System.DBNull.Value)
                      objZTapeRecord.ReturnsCount = (int)ds.Tables[0].Rows[0]["ReturnsCount"];
                  else
                      objZTapeRecord.ReturnsCount = null;

                  if (ds.Tables[0].Rows[0]["ReturnsTotal"] != System.DBNull.Value)
                      objZTapeRecord.ReturnsTotal = (decimal)ds.Tables[0].Rows[0]["ReturnsTotal"];
                  else
                      objZTapeRecord.ReturnsTotal = null;

                  if (ds.Tables[0].Rows[0]["ReturnsTax"] != System.DBNull.Value)
                      objZTapeRecord.ReturnsTax = (decimal)ds.Tables[0].Rows[0]["ReturnsTax"];
                  else
                      objZTapeRecord.ReturnsTax = null;

                  if (ds.Tables[0].Rows[0]["FurnitureCount"] != System.DBNull.Value)
                      objZTapeRecord.FurnitureCount = (int)ds.Tables[0].Rows[0]["FurnitureCount"];
                  else
                      objZTapeRecord.FurnitureCount = null;

                  if (ds.Tables[0].Rows[0]["FurnitureTotal"] != System.DBNull.Value)
                      objZTapeRecord.FurnitureTotal = (decimal)ds.Tables[0].Rows[0]["FurnitureTotal"];
                  else
                      objZTapeRecord.FurnitureTotal = null;

                  if (ds.Tables[0].Rows[0]["JewelryCount"] != System.DBNull.Value)
                      objZTapeRecord.JewelryCount = (int)ds.Tables[0].Rows[0]["JewelryCount"];
                  else
                      objZTapeRecord.JewelryCount = null;

                  if (ds.Tables[0].Rows[0]["JewelryTotal"] != System.DBNull.Value)
                      objZTapeRecord.JewelryTotal = (decimal)ds.Tables[0].Rows[0]["JewelryTotal"];
                  else
                      objZTapeRecord.JewelryTotal = null;

                  if (ds.Tables[0].Rows[0]["ElectricalCount"] != System.DBNull.Value)
                      objZTapeRecord.ElectricalCount = (int)ds.Tables[0].Rows[0]["ElectricalCount"];
                  else
                      objZTapeRecord.ElectricalCount = null;

                  if (ds.Tables[0].Rows[0]["ElectricalTotal"] != System.DBNull.Value)
                      objZTapeRecord.ElectricalTotal = (decimal)ds.Tables[0].Rows[0]["ElectricalTotal"];
                  else
                      objZTapeRecord.ElectricalTotal = null;

                  if (ds.Tables[0].Rows[0]["WomensCount"] != System.DBNull.Value)
                      objZTapeRecord.WomensCount = (int)ds.Tables[0].Rows[0]["WomensCount"];
                  else
                      objZTapeRecord.WomensCount = null;

                  if (ds.Tables[0].Rows[0]["WomensTotal"] != System.DBNull.Value)
                      objZTapeRecord.WomensTotal = (decimal)ds.Tables[0].Rows[0]["WomensTotal"];
                  else
                      objZTapeRecord.WomensTotal = null;

                  if (ds.Tables[0].Rows[0]["BinsCount"] != System.DBNull.Value)
                      objZTapeRecord.BinsCount = (int)ds.Tables[0].Rows[0]["BinsCount"];
                  else
                      objZTapeRecord.BinsCount = null;

                  if (ds.Tables[0].Rows[0]["BinsTotal"] != System.DBNull.Value)
                      objZTapeRecord.BinsTotal = (decimal)ds.Tables[0].Rows[0]["BinsTotal"];
                  else
                      objZTapeRecord.BinsTotal = null;

                  if (ds.Tables[0].Rows[0]["MiscCount"] != System.DBNull.Value)
                      objZTapeRecord.MiscCount = (int)ds.Tables[0].Rows[0]["MiscCount"];
                  else
                      objZTapeRecord.MiscCount = null;

                  if (ds.Tables[0].Rows[0]["MiscTotal"] != System.DBNull.Value)
                      objZTapeRecord.MiscTotal = (decimal)ds.Tables[0].Rows[0]["MiscTotal"];
                  else
                      objZTapeRecord.MiscTotal = null;

                  if (ds.Tables[0].Rows[0]["ShoesCount"] != System.DBNull.Value)
                      objZTapeRecord.ShoesCount = (int)ds.Tables[0].Rows[0]["ShoesCount"];
                  else
                      objZTapeRecord.ShoesCount = null;

                  if (ds.Tables[0].Rows[0]["ShoesTotal"] != System.DBNull.Value)
                      objZTapeRecord.ShoesTotal = (decimal)ds.Tables[0].Rows[0]["ShoesTotal"];
                  else
                      objZTapeRecord.ShoesTotal = null;

                  if (ds.Tables[0].Rows[0]["BoutiqueCount"] != System.DBNull.Value)
                      objZTapeRecord.BoutiqueCount = (int)ds.Tables[0].Rows[0]["BoutiqueCount"];
                  else
                      objZTapeRecord.BoutiqueCount = null;

                  if (ds.Tables[0].Rows[0]["BoutiqueTotal"] != System.DBNull.Value)
                      objZTapeRecord.BoutiqueTotal = (decimal)ds.Tables[0].Rows[0]["BoutiqueTotal"];
                  else
                      objZTapeRecord.BoutiqueTotal = null;

                  if (ds.Tables[0].Rows[0]["ChildsCount"] != System.DBNull.Value)
                      objZTapeRecord.ChildsCount = (int)ds.Tables[0].Rows[0]["ChildsCount"];
                  else
                      objZTapeRecord.ChildsCount = null;

                  if (ds.Tables[0].Rows[0]["ChildsTotal"] != System.DBNull.Value)
                      objZTapeRecord.ChildsTotal = (decimal)ds.Tables[0].Rows[0]["ChildsTotal"];
                  else
                      objZTapeRecord.ChildsTotal = null;

                  if (ds.Tables[0].Rows[0]["MensCount"] != System.DBNull.Value)
                      objZTapeRecord.MensCount = (int)ds.Tables[0].Rows[0]["MensCount"];
                  else
                      objZTapeRecord.MensCount = null;

                  if (ds.Tables[0].Rows[0]["MensTotal"] != System.DBNull.Value)
                      objZTapeRecord.MensTotal = (decimal)ds.Tables[0].Rows[0]["MensTotal"];
                  else
                      objZTapeRecord.MensTotal = null;

                  if (ds.Tables[0].Rows[0]["BooksCount"] != System.DBNull.Value)
                      objZTapeRecord.BooksCount = (int)ds.Tables[0].Rows[0]["BooksCount"];
                  else
                      objZTapeRecord.BooksCount = null;

                  if (ds.Tables[0].Rows[0]["BooksTotal"] != System.DBNull.Value)
                      objZTapeRecord.BooksTotal = (decimal)ds.Tables[0].Rows[0]["BooksTotal"];
                  else
                      objZTapeRecord.BooksTotal = null;

                  if (ds.Tables[0].Rows[0]["NewMerchCount"] != System.DBNull.Value)
                      objZTapeRecord.NewMerchCount = (int)ds.Tables[0].Rows[0]["NewMerchCount"];
                  else
                      objZTapeRecord.NewMerchCount = null;

                  if (ds.Tables[0].Rows[0]["NewMerchTotal"] != System.DBNull.Value)
                      objZTapeRecord.NewMerchTotal = (decimal)ds.Tables[0].Rows[0]["NewMerchTotal"];
                  else
                      objZTapeRecord.NewMerchTotal = null;

                  if (ds.Tables[0].Rows[0]["FurnitureCountDisc"] != System.DBNull.Value)
                      objZTapeRecord.FurnitureCountDisc = (int)ds.Tables[0].Rows[0]["FurnitureCountDisc"];
                  else
                      objZTapeRecord.FurnitureCountDisc = null;

                  if (ds.Tables[0].Rows[0]["FurnitureTotalDisc"] != System.DBNull.Value)
                      objZTapeRecord.FurnitureTotalDisc = (decimal)ds.Tables[0].Rows[0]["FurnitureTotalDisc"];
                  else
                      objZTapeRecord.FurnitureTotalDisc = null;

                  if (ds.Tables[0].Rows[0]["JewelryCountDisc"] != System.DBNull.Value)
                      objZTapeRecord.JewelryCountDisc = (int)ds.Tables[0].Rows[0]["JewelryCountDisc"];
                  else
                      objZTapeRecord.JewelryCountDisc = null;

                  if (ds.Tables[0].Rows[0]["JewelryTotalDisc"] != System.DBNull.Value)
                      objZTapeRecord.JewelryTotalDisc = (decimal)ds.Tables[0].Rows[0]["JewelryTotalDisc"];
                  else
                      objZTapeRecord.JewelryTotalDisc = null;

                  if (ds.Tables[0].Rows[0]["ElectricalCountDisc"] != System.DBNull.Value)
                      objZTapeRecord.ElectricalCountDisc = (int)ds.Tables[0].Rows[0]["ElectricalCountDisc"];
                  else
                      objZTapeRecord.ElectricalCountDisc = null;

                  if (ds.Tables[0].Rows[0]["ElectricalTotalDisc"] != System.DBNull.Value)
                      objZTapeRecord.ElectricalTotalDisc = (decimal)ds.Tables[0].Rows[0]["ElectricalTotalDisc"];
                  else
                      objZTapeRecord.ElectricalTotalDisc = null;

                  if (ds.Tables[0].Rows[0]["WomensCountDisc"] != System.DBNull.Value)
                      objZTapeRecord.WomensCountDisc = (int)ds.Tables[0].Rows[0]["WomensCountDisc"];
                  else
                      objZTapeRecord.WomensCountDisc = null;

                  if (ds.Tables[0].Rows[0]["WomensTotalDisc"] != System.DBNull.Value)
                      objZTapeRecord.WomensTotalDisc = (decimal)ds.Tables[0].Rows[0]["WomensTotalDisc"];
                  else
                      objZTapeRecord.WomensTotalDisc = null;

                  if (ds.Tables[0].Rows[0]["BinsCountDisc"] != System.DBNull.Value)
                      objZTapeRecord.BinsCountDisc = (int)ds.Tables[0].Rows[0]["BinsCountDisc"];
                  else
                      objZTapeRecord.BinsCountDisc = null;

                  if (ds.Tables[0].Rows[0]["BinsTotalDisc"] != System.DBNull.Value)
                      objZTapeRecord.BinsTotalDisc = (decimal)ds.Tables[0].Rows[0]["BinsTotalDisc"];
                  else
                      objZTapeRecord.BinsTotalDisc = null;

                  if (ds.Tables[0].Rows[0]["MiscCountDisc"] != System.DBNull.Value)
                      objZTapeRecord.MiscCountDisc = (int)ds.Tables[0].Rows[0]["MiscCountDisc"];
                  else
                      objZTapeRecord.MiscCountDisc = null;

                  if (ds.Tables[0].Rows[0]["MiscTotalDisc"] != System.DBNull.Value)
                      objZTapeRecord.MiscTotalDisc = (decimal)ds.Tables[0].Rows[0]["MiscTotalDisc"];
                  else
                      objZTapeRecord.MiscTotalDisc = null;

                  if (ds.Tables[0].Rows[0]["ShoesCountDisc"] != System.DBNull.Value)
                      objZTapeRecord.ShoesCountDisc = (int)ds.Tables[0].Rows[0]["ShoesCountDisc"];
                  else
                      objZTapeRecord.ShoesCountDisc = null;

                  if (ds.Tables[0].Rows[0]["ShoesTotalDisc"] != System.DBNull.Value)
                      objZTapeRecord.ShoesTotalDisc = (decimal)ds.Tables[0].Rows[0]["ShoesTotalDisc"];
                  else
                      objZTapeRecord.ShoesTotalDisc = null;

                  if (ds.Tables[0].Rows[0]["BoutiqueCountDisc"] != System.DBNull.Value)
                      objZTapeRecord.BoutiqueCountDisc = (int)ds.Tables[0].Rows[0]["BoutiqueCountDisc"];
                  else
                      objZTapeRecord.BoutiqueCountDisc = null;

                  if (ds.Tables[0].Rows[0]["BoutiqueTotalDisc"] != System.DBNull.Value)
                      objZTapeRecord.BoutiqueTotalDisc = (decimal)ds.Tables[0].Rows[0]["BoutiqueTotalDisc"];
                  else
                      objZTapeRecord.BoutiqueTotalDisc = null;

                  if (ds.Tables[0].Rows[0]["ChildsCountDisc"] != System.DBNull.Value)
                      objZTapeRecord.ChildsCountDisc = (int)ds.Tables[0].Rows[0]["ChildsCountDisc"];
                  else
                      objZTapeRecord.ChildsCountDisc = null;

                  if (ds.Tables[0].Rows[0]["ChildsTotalDisc"] != System.DBNull.Value)
                      objZTapeRecord.ChildsTotalDisc = (decimal)ds.Tables[0].Rows[0]["ChildsTotalDisc"];
                  else
                      objZTapeRecord.ChildsTotalDisc = null;

                  if (ds.Tables[0].Rows[0]["MensCountDisc"] != System.DBNull.Value)
                      objZTapeRecord.MensCountDisc = (int)ds.Tables[0].Rows[0]["MensCountDisc"];
                  else
                      objZTapeRecord.MensCountDisc = null;

                  if (ds.Tables[0].Rows[0]["MensTotalDisc"] != System.DBNull.Value)
                      objZTapeRecord.MensTotalDisc = (decimal)ds.Tables[0].Rows[0]["MensTotalDisc"];
                  else
                      objZTapeRecord.MensTotalDisc = null;

                  if (ds.Tables[0].Rows[0]["BooksCountDisc"] != System.DBNull.Value)
                      objZTapeRecord.BooksCountDisc = (int)ds.Tables[0].Rows[0]["BooksCountDisc"];
                  else
                      objZTapeRecord.BooksCountDisc = null;

                  if (ds.Tables[0].Rows[0]["BooksTotalDisc"] != System.DBNull.Value)
                      objZTapeRecord.BooksTotalDisc = (decimal)ds.Tables[0].Rows[0]["BooksTotalDisc"];
                  else
                      objZTapeRecord.BooksTotalDisc = null;

                  if (ds.Tables[0].Rows[0]["CorrectionCount"] != System.DBNull.Value)
                      objZTapeRecord.CorrectionCount = (int)ds.Tables[0].Rows[0]["CorrectionCount"];
                  else
                      objZTapeRecord.CorrectionCount = null;

                  if (ds.Tables[0].Rows[0]["CorrectionTotal"] != System.DBNull.Value)
                      objZTapeRecord.CorrectionTotal = (decimal)ds.Tables[0].Rows[0]["CorrectionTotal"];
                  else
                      objZTapeRecord.CorrectionTotal = null;

                  if (ds.Tables[0].Rows[0]["VoidCount"] != System.DBNull.Value)
                      objZTapeRecord.VoidCount = (int)ds.Tables[0].Rows[0]["VoidCount"];
                  else
                      objZTapeRecord.VoidCount = null;

                  if (ds.Tables[0].Rows[0]["VoidTotal"] != System.DBNull.Value)
                      objZTapeRecord.VoidTotal = (decimal)ds.Tables[0].Rows[0]["VoidTotal"];
                  else
                      objZTapeRecord.VoidTotal = null;

                  if (ds.Tables[0].Rows[0]["AllVoidCount"] != System.DBNull.Value)
                      objZTapeRecord.AllVoidCount = (int)ds.Tables[0].Rows[0]["AllVoidCount"];
                  else
                      objZTapeRecord.AllVoidCount = null;

                  if (ds.Tables[0].Rows[0]["AllVoidTotal"] != System.DBNull.Value)
                      objZTapeRecord.AllVoidTotal = (decimal)ds.Tables[0].Rows[0]["AllVoidTotal"];
                  else
                      objZTapeRecord.AllVoidTotal = null;

              }
              command.Dispose();
              connection.Close();
              connection.Dispose();
              ds.Dispose();

              return objZTapeRecord;
         }
 
         /// <summary>
         /// Selects all ZTapeRecord 
         /// </summary> 
         public static ZTapeRecordCollection SelectAll() 
         { 
             return SelectShared("[dbo].[pr_ZTapeRecord_SelectAll]", String.Empty, null); 
         } 
 
         /// <summary>
         /// Selects all ZTapeRecord by Location, related to column LocationID 
         /// </summary> 
         public static ZTapeRecordCollection SelectZTapeRecordCollectionByLocation(int locationID) 
         { 
             return SelectShared("[dbo].[pr_ZTapeRecord_SelectAllByLocation]", "locationID", locationID); 
         } 
 
         /// <summary>
         /// Selects ZTapeRecordID and Date columns for use with a DropDownList web control 
         /// </summary> 
         public static ZTapeRecordCollection SelectZTapeRecordDropDownListData() 
         { 
              string storedProcName = "[dbo].[pr_ZTapeRecord_SelectAll]";
              SqlConnection connection = Dbase.GetConnection();
              SqlCommand command = Dbase.GetCommand(storedProcName, connection);
 
              DataSet ds = Dbase.GetDbaseDataSet(command); 
              ZTapeRecordCollection objZTapeRecordCol = new ZTapeRecordCollection(); 
              ZTapeRecord objZTapeRecord; 
 
              if (ds.Tables[0].Rows.Count > 0) 
              { 
                 foreach(DataRow dr in ds.Tables[0].Rows) 
                 { 
                     objZTapeRecord = new ZTapeRecord(); 
                     objZTapeRecord.ZTapeRecordID = (int)dr["ZTapeRecordID"]; 
 
                     if (dr["Date"] != System.DBNull.Value) 
                         objZTapeRecord.Date = (DateTime)(dr["Date"]); 
                     else 
                         objZTapeRecord.Date = null; 
 
                     objZTapeRecordCol.Add(objZTapeRecord);
                 } 
              } 
 
              command.Dispose(); 
              connection.Close(); 
              connection.Dispose(); 
              ds.Dispose(); 
 
              return objZTapeRecordCol; 
         } 
 
         public static ZTapeRecordCollection SelectShared(string storedProcName, string param, object paramValue) 
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
              ZTapeRecordCollection objZTapeRecordCol = new ZTapeRecordCollection(); 
              ZTapeRecord objZTapeRecord; 
 
              if (ds.Tables[0].Rows.Count > 0) 
              { 
                 foreach(DataRow dr in ds.Tables[0].Rows) 
                 { 
                     objZTapeRecord = new ZTapeRecord(); 
                     objZTapeRecord.ZTapeRecordID = (int)dr["ZTapeRecordID"]; 
 
                     if (dr["Date"] != System.DBNull.Value) 
                         objZTapeRecord.Date = (DateTime)dr["Date"]; 
                     else 
                         objZTapeRecord.Date = null; 
 
                     if (dr["LocationID"] != System.DBNull.Value) 
                         objZTapeRecord.LocationID = (int)dr["LocationID"];
                     else
                         objZTapeRecord.LocationID = null;
 
 
                     if (dr["Register"] != System.DBNull.Value) 
                         objZTapeRecord.Register = (int)dr["Register"]; 
                     else 
                         objZTapeRecord.Register = null; 
 
                     if (dr["AddAdjustment"] != System.DBNull.Value) 
                         objZTapeRecord.AddAdjustment = (decimal)dr["AddAdjustment"]; 
                     else 
                         objZTapeRecord.AddAdjustment = null; 
 
                     if (dr["AddComment"] != System.DBNull.Value) 
                         objZTapeRecord.AddComment = dr["AddComment"].ToString(); 
                     else 
                         objZTapeRecord.AddComment = null; 
 
                     if (dr["CashAdjustment"] != System.DBNull.Value) 
                         objZTapeRecord.CashAdjustment = (decimal)dr["CashAdjustment"]; 
                     else 
                         objZTapeRecord.CashAdjustment = null; 
 
                     if (dr["CashComment"] != System.DBNull.Value) 
                         objZTapeRecord.CashComment = dr["CashComment"].ToString(); 
                     else 
                         objZTapeRecord.CashComment = null; 
 
                     if (dr["Discount1StCount"] != System.DBNull.Value) 
                         objZTapeRecord.Discount1StCount = (int)dr["Discount1StCount"]; 
                     else 
                         objZTapeRecord.Discount1StCount = null; 
 
                     if (dr["Discount1StTotal"] != System.DBNull.Value) 
                         objZTapeRecord.Discount1StTotal = (decimal)dr["Discount1StTotal"]; 
                     else 
                         objZTapeRecord.Discount1StTotal = null; 
 
                     if (dr["Discount2StCount"] != System.DBNull.Value) 
                         objZTapeRecord.Discount2StCount = (int)dr["Discount2StCount"]; 
                     else 
                         objZTapeRecord.Discount2StCount = null; 
 
                     if (dr["Discount2StTotal"] != System.DBNull.Value) 
                         objZTapeRecord.Discount2StTotal = (decimal)dr["Discount2StTotal"]; 
                     else 
                         objZTapeRecord.Discount2StTotal = null; 
 
                     if (dr["ChargeCount"] != System.DBNull.Value) 
                         objZTapeRecord.ChargeCount = (int)dr["ChargeCount"]; 
                     else 
                         objZTapeRecord.ChargeCount = null; 
 
                     if (dr["ChargeTotal"] != System.DBNull.Value) 
                         objZTapeRecord.ChargeTotal = (decimal)dr["ChargeTotal"]; 
                     else 
                         objZTapeRecord.ChargeTotal = null; 
 
                     if (dr["CashCount"] != System.DBNull.Value) 
                         objZTapeRecord.CashCount = (int)dr["CashCount"]; 
                     else 
                         objZTapeRecord.CashCount = null; 
 
                     if (dr["CashTotal"] != System.DBNull.Value) 
                         objZTapeRecord.CashTotal = (decimal)dr["CashTotal"]; 
                     else 
                         objZTapeRecord.CashTotal = null; 
 
                     if (dr["Tax"] != System.DBNull.Value) 
                         objZTapeRecord.Tax = (decimal)dr["Tax"]; 
                     else 
                         objZTapeRecord.Tax = null; 
 
                     if (dr["Discount1ItCount"] != System.DBNull.Value) 
                         objZTapeRecord.Discount1ItCount = (int)dr["Discount1ItCount"]; 
                     else 
                         objZTapeRecord.Discount1ItCount = null; 
 
                     if (dr["Discount1ItTotal"] != System.DBNull.Value) 
                         objZTapeRecord.Discount1ItTotal = (decimal)dr["Discount1ItTotal"]; 
                     else 
                         objZTapeRecord.Discount1ItTotal = null; 
 
                     if (dr["Discount2ItCount"] != System.DBNull.Value) 
                         objZTapeRecord.Discount2ItCount = (int)dr["Discount2ItCount"]; 
                     else 
                         objZTapeRecord.Discount2ItCount = null; 
 
                     if (dr["Discount2ItTotal"] != System.DBNull.Value) 
                         objZTapeRecord.Discount2ItTotal = (decimal)dr["Discount2ItTotal"]; 
                     else 
                         objZTapeRecord.Discount2ItTotal = null; 
 
                     if (dr["ReturnsCount"] != System.DBNull.Value) 
                         objZTapeRecord.ReturnsCount = (int)dr["ReturnsCount"]; 
                     else 
                         objZTapeRecord.ReturnsCount = null; 
 
                     if (dr["ReturnsTotal"] != System.DBNull.Value) 
                         objZTapeRecord.ReturnsTotal = (decimal)dr["ReturnsTotal"]; 
                     else 
                         objZTapeRecord.ReturnsTotal = null; 
 
                     if (dr["ReturnsTax"] != System.DBNull.Value) 
                         objZTapeRecord.ReturnsTax = (decimal)dr["ReturnsTax"]; 
                     else 
                         objZTapeRecord.ReturnsTax = null; 
 
                     if (dr["FurnitureCount"] != System.DBNull.Value) 
                         objZTapeRecord.FurnitureCount = (int)dr["FurnitureCount"]; 
                     else 
                         objZTapeRecord.FurnitureCount = null; 
 
                     if (dr["FurnitureTotal"] != System.DBNull.Value) 
                         objZTapeRecord.FurnitureTotal = (decimal)dr["FurnitureTotal"]; 
                     else 
                         objZTapeRecord.FurnitureTotal = null; 
 
                     if (dr["JewelryCount"] != System.DBNull.Value) 
                         objZTapeRecord.JewelryCount = (int)dr["JewelryCount"]; 
                     else 
                         objZTapeRecord.JewelryCount = null; 
 
                     if (dr["JewelryTotal"] != System.DBNull.Value) 
                         objZTapeRecord.JewelryTotal = (decimal)dr["JewelryTotal"]; 
                     else 
                         objZTapeRecord.JewelryTotal = null; 
 
                     if (dr["ElectricalCount"] != System.DBNull.Value) 
                         objZTapeRecord.ElectricalCount = (int)dr["ElectricalCount"]; 
                     else 
                         objZTapeRecord.ElectricalCount = null; 
 
                     if (dr["ElectricalTotal"] != System.DBNull.Value) 
                         objZTapeRecord.ElectricalTotal = (decimal)dr["ElectricalTotal"]; 
                     else 
                         objZTapeRecord.ElectricalTotal = null; 
 
                     if (dr["WomensCount"] != System.DBNull.Value) 
                         objZTapeRecord.WomensCount = (int)dr["WomensCount"]; 
                     else 
                         objZTapeRecord.WomensCount = null; 
 
                     if (dr["WomensTotal"] != System.DBNull.Value) 
                         objZTapeRecord.WomensTotal = (decimal)dr["WomensTotal"]; 
                     else 
                         objZTapeRecord.WomensTotal = null; 
 
                     if (dr["BinsCount"] != System.DBNull.Value) 
                         objZTapeRecord.BinsCount = (int)dr["BinsCount"]; 
                     else 
                         objZTapeRecord.BinsCount = null; 
 
                     if (dr["BinsTotal"] != System.DBNull.Value) 
                         objZTapeRecord.BinsTotal = (decimal)dr["BinsTotal"]; 
                     else 
                         objZTapeRecord.BinsTotal = null; 
 
                     if (dr["MiscCount"] != System.DBNull.Value) 
                         objZTapeRecord.MiscCount = (int)dr["MiscCount"]; 
                     else 
                         objZTapeRecord.MiscCount = null; 
 
                     if (dr["MiscTotal"] != System.DBNull.Value) 
                         objZTapeRecord.MiscTotal = (decimal)dr["MiscTotal"]; 
                     else 
                         objZTapeRecord.MiscTotal = null; 
 
                     if (dr["ShoesCount"] != System.DBNull.Value) 
                         objZTapeRecord.ShoesCount = (int)dr["ShoesCount"]; 
                     else 
                         objZTapeRecord.ShoesCount = null; 
 
                     if (dr["ShoesTotal"] != System.DBNull.Value) 
                         objZTapeRecord.ShoesTotal = (decimal)dr["ShoesTotal"]; 
                     else 
                         objZTapeRecord.ShoesTotal = null; 
 
                     if (dr["BoutiqueCount"] != System.DBNull.Value) 
                         objZTapeRecord.BoutiqueCount = (int)dr["BoutiqueCount"]; 
                     else 
                         objZTapeRecord.BoutiqueCount = null; 
 
                     if (dr["BoutiqueTotal"] != System.DBNull.Value) 
                         objZTapeRecord.BoutiqueTotal = (decimal)dr["BoutiqueTotal"]; 
                     else 
                         objZTapeRecord.BoutiqueTotal = null; 
 
                     if (dr["ChildsCount"] != System.DBNull.Value) 
                         objZTapeRecord.ChildsCount = (int)dr["ChildsCount"]; 
                     else 
                         objZTapeRecord.ChildsCount = null; 
 
                     if (dr["ChildsTotal"] != System.DBNull.Value) 
                         objZTapeRecord.ChildsTotal = (decimal)dr["ChildsTotal"]; 
                     else 
                         objZTapeRecord.ChildsTotal = null; 
 
                     if (dr["MensCount"] != System.DBNull.Value) 
                         objZTapeRecord.MensCount = (int)dr["MensCount"]; 
                     else 
                         objZTapeRecord.MensCount = null; 
 
                     if (dr["MensTotal"] != System.DBNull.Value) 
                         objZTapeRecord.MensTotal = (decimal)dr["MensTotal"]; 
                     else 
                         objZTapeRecord.MensTotal = null; 
 
                     if (dr["BooksCount"] != System.DBNull.Value) 
                         objZTapeRecord.BooksCount = (int)dr["BooksCount"]; 
                     else 
                         objZTapeRecord.BooksCount = null; 
 
                     if (dr["BooksTotal"] != System.DBNull.Value) 
                         objZTapeRecord.BooksTotal = (decimal)dr["BooksTotal"]; 
                     else 
                         objZTapeRecord.BooksTotal = null; 
 
                     if (dr["NewMerchCount"] != System.DBNull.Value) 
                         objZTapeRecord.NewMerchCount = (int)dr["NewMerchCount"]; 
                     else 
                         objZTapeRecord.NewMerchCount = null; 
 
                     if (dr["NewMerchTotal"] != System.DBNull.Value) 
                         objZTapeRecord.NewMerchTotal = (decimal)dr["NewMerchTotal"]; 
                     else 
                         objZTapeRecord.BooksTotal = null; 

                     if (dr["FurnitureCountDisc"] != System.DBNull.Value) 
                         objZTapeRecord.FurnitureCountDisc = (int)dr["FurnitureCountDisc"]; 
                     else 
                         objZTapeRecord.FurnitureCountDisc = null; 
 
                     if (dr["FurnitureTotalDisc"] != System.DBNull.Value) 
                         objZTapeRecord.FurnitureTotalDisc = (decimal)dr["FurnitureTotalDisc"]; 
                     else 
                         objZTapeRecord.FurnitureTotalDisc = null; 
 
                     if (dr["JewelryCountDisc"] != System.DBNull.Value) 
                         objZTapeRecord.JewelryCountDisc = (int)dr["JewelryCountDisc"]; 
                     else 
                         objZTapeRecord.JewelryCountDisc = null; 
 
                     if (dr["JewelryTotalDisc"] != System.DBNull.Value) 
                         objZTapeRecord.JewelryTotalDisc = (decimal)dr["JewelryTotalDisc"]; 
                     else 
                         objZTapeRecord.JewelryTotalDisc = null; 
 
                     if (dr["ElectricalCountDisc"] != System.DBNull.Value) 
                         objZTapeRecord.ElectricalCountDisc = (int)dr["ElectricalCountDisc"]; 
                     else 
                         objZTapeRecord.ElectricalCountDisc = null; 
 
                     if (dr["ElectricalTotalDisc"] != System.DBNull.Value) 
                         objZTapeRecord.ElectricalTotalDisc = (decimal)dr["ElectricalTotalDisc"]; 
                     else 
                         objZTapeRecord.ElectricalTotalDisc = null; 
 
                     if (dr["WomensCountDisc"] != System.DBNull.Value) 
                         objZTapeRecord.WomensCountDisc = (int)dr["WomensCountDisc"]; 
                     else 
                         objZTapeRecord.WomensCountDisc = null; 
 
                     if (dr["WomensTotalDisc"] != System.DBNull.Value) 
                         objZTapeRecord.WomensTotalDisc = (decimal)dr["WomensTotalDisc"]; 
                     else 
                         objZTapeRecord.WomensTotalDisc = null; 
 
                     if (dr["BinsCountDisc"] != System.DBNull.Value) 
                         objZTapeRecord.BinsCountDisc = (int)dr["BinsCountDisc"]; 
                     else 
                         objZTapeRecord.BinsCountDisc = null; 
 
                     if (dr["BinsTotalDisc"] != System.DBNull.Value) 
                         objZTapeRecord.BinsTotalDisc = (decimal)dr["BinsTotalDisc"]; 
                     else 
                         objZTapeRecord.BinsTotalDisc = null; 
 
                     if (dr["MiscCountDisc"] != System.DBNull.Value) 
                         objZTapeRecord.MiscCountDisc = (int)dr["MiscCountDisc"]; 
                     else 
                         objZTapeRecord.MiscCountDisc = null; 
 
                     if (dr["MiscTotalDisc"] != System.DBNull.Value) 
                         objZTapeRecord.MiscTotalDisc = (decimal)dr["MiscTotalDisc"]; 
                     else 
                         objZTapeRecord.MiscTotalDisc = null; 
 
                     if (dr["ShoesCountDisc"] != System.DBNull.Value) 
                         objZTapeRecord.ShoesCountDisc = (int)dr["ShoesCountDisc"]; 
                     else 
                         objZTapeRecord.ShoesCountDisc = null; 
 
                     if (dr["ShoesTotalDisc"] != System.DBNull.Value) 
                         objZTapeRecord.ShoesTotalDisc = (decimal)dr["ShoesTotalDisc"]; 
                     else 
                         objZTapeRecord.ShoesTotalDisc = null; 
 
                     if (dr["BoutiqueCountDisc"] != System.DBNull.Value) 
                         objZTapeRecord.BoutiqueCountDisc = (int)dr["BoutiqueCountDisc"]; 
                     else 
                         objZTapeRecord.BoutiqueCountDisc = null; 
 
                     if (dr["BoutiqueTotalDisc"] != System.DBNull.Value) 
                         objZTapeRecord.BoutiqueTotalDisc = (decimal)dr["BoutiqueTotalDisc"]; 
                     else 
                         objZTapeRecord.BoutiqueTotalDisc = null; 
 
                     if (dr["ChildsCountDisc"] != System.DBNull.Value) 
                         objZTapeRecord.ChildsCountDisc = (int)dr["ChildsCountDisc"]; 
                     else 
                         objZTapeRecord.ChildsCountDisc = null; 
 
                     if (dr["ChildsTotalDisc"] != System.DBNull.Value) 
                         objZTapeRecord.ChildsTotalDisc = (decimal)dr["ChildsTotalDisc"]; 
                     else 
                         objZTapeRecord.ChildsTotalDisc = null; 
 
                     if (dr["MensCountDisc"] != System.DBNull.Value) 
                         objZTapeRecord.MensCountDisc = (int)dr["MensCountDisc"]; 
                     else 
                         objZTapeRecord.MensCountDisc = null; 
 
                     if (dr["MensTotalDisc"] != System.DBNull.Value) 
                         objZTapeRecord.MensTotalDisc = (decimal)dr["MensTotalDisc"]; 
                     else 
                         objZTapeRecord.MensTotalDisc = null; 
 
                     if (dr["BooksCountDisc"] != System.DBNull.Value) 
                         objZTapeRecord.BooksCountDisc = (int)dr["BooksCountDisc"]; 
                     else 
                         objZTapeRecord.BooksCountDisc = null; 
 
                     if (dr["BooksTotalDisc"] != System.DBNull.Value) 
                         objZTapeRecord.BooksTotalDisc = (decimal)dr["BooksTotalDisc"]; 
                     else 
                         objZTapeRecord.BooksTotalDisc = null; 
 
                     if (dr["CorrectionCount"] != System.DBNull.Value) 
                         objZTapeRecord.CorrectionCount = (int)dr["CorrectionCount"]; 
                     else 
                         objZTapeRecord.CorrectionCount = null; 
 
                     if (dr["CorrectionTotal"] != System.DBNull.Value) 
                         objZTapeRecord.CorrectionTotal = (decimal)dr["CorrectionTotal"]; 
                     else 
                         objZTapeRecord.CorrectionTotal = null; 
 
                     if (dr["VoidCount"] != System.DBNull.Value) 
                         objZTapeRecord.VoidCount = (int)dr["VoidCount"]; 
                     else 
                         objZTapeRecord.VoidCount = null; 
 
                     if (dr["VoidTotal"] != System.DBNull.Value) 
                         objZTapeRecord.VoidTotal = (decimal)dr["VoidTotal"]; 
                     else 
                         objZTapeRecord.VoidTotal = null; 
 
                     if (dr["AllVoidCount"] != System.DBNull.Value) 
                         objZTapeRecord.AllVoidCount = (int)dr["AllVoidCount"]; 
                     else 
                         objZTapeRecord.AllVoidCount = null; 
 
                     if (dr["AllVoidTotal"] != System.DBNull.Value) 
                         objZTapeRecord.AllVoidTotal = (decimal)dr["AllVoidTotal"]; 
                     else 
                         objZTapeRecord.AllVoidTotal = null; 
 
                     objZTapeRecordCol.Add(objZTapeRecord);
                 } 
              } 
 
              command.Dispose(); 
              connection.Close(); 
              connection.Dispose(); 
              ds.Dispose(); 
 
              return objZTapeRecordCol; 
         } 
 
         /// <summary>
         /// Inserts a record 
         /// </summary> 
         public static int Insert(ZTapeRecord objZTapeRecord) 
         { 
             string storedProcName = "[dbo].[pr_ZTapeRecord_Insert]"; 
             return InsertUpdate(objZTapeRecord, false, storedProcName); 
         } 
 
         /// <summary>
         /// Updates a record 
         /// </summary>
         public static void Update(ZTapeRecord objZTapeRecord) 
         { 
             string storedProcName = "[dbo].[pr_ZTapeRecord_Update]"; 
             InsertUpdate(objZTapeRecord, true, storedProcName); 
         } 
 
         private static int InsertUpdate(ZTapeRecord objZTapeRecord, bool isUpdate, string storedProcName) 
         { 
             SqlConnection connection = Dbase.GetConnection(); 
             SqlCommand command = Dbase.GetCommand(storedProcName, connection); 
 
             object date = objZTapeRecord.Date; 
             object locationID = objZTapeRecord.LocationID; 
             object register = objZTapeRecord.Register; 
             object addAdjustment = objZTapeRecord.AddAdjustment; 
             object addComment = objZTapeRecord.AddComment; 
             object cashAdjustment = objZTapeRecord.CashAdjustment; 
             object cashComment = objZTapeRecord.CashComment; 
             object discount1StCount = objZTapeRecord.Discount1StCount; 
             object discount1StTotal = objZTapeRecord.Discount1StTotal; 
             object discount2StCount = objZTapeRecord.Discount2StCount; 
             object discount2StTotal = objZTapeRecord.Discount2StTotal; 
             object chargeCount = objZTapeRecord.ChargeCount; 
             object chargeTotal = objZTapeRecord.ChargeTotal; 
             object cashCount = objZTapeRecord.CashCount; 
             object cashTotal = objZTapeRecord.CashTotal; 
             object tax = objZTapeRecord.Tax; 
             object discount1ItCount = objZTapeRecord.Discount1ItCount; 
             object discount1ItTotal = objZTapeRecord.Discount1ItTotal; 
             object discount2ItCount = objZTapeRecord.Discount2ItCount; 
             object discount2ItTotal = objZTapeRecord.Discount2ItTotal; 
             object returnsCount = objZTapeRecord.ReturnsCount; 
             object returnsTotal = objZTapeRecord.ReturnsTotal; 
             object returnsTax = objZTapeRecord.ReturnsTax; 
             object furnitureCount = objZTapeRecord.FurnitureCount; 
             object furnitureTotal = objZTapeRecord.FurnitureTotal; 
             object jewelryCount = objZTapeRecord.JewelryCount; 
             object jewelryTotal = objZTapeRecord.JewelryTotal; 
             object electricalCount = objZTapeRecord.ElectricalCount; 
             object electricalTotal = objZTapeRecord.ElectricalTotal; 
             object womensCount = objZTapeRecord.WomensCount; 
             object womensTotal = objZTapeRecord.WomensTotal; 
             object binsCount = objZTapeRecord.BinsCount; 
             object binsTotal = objZTapeRecord.BinsTotal; 
             object miscCount = objZTapeRecord.MiscCount; 
             object miscTotal = objZTapeRecord.MiscTotal; 
             object shoesCount = objZTapeRecord.ShoesCount; 
             object shoesTotal = objZTapeRecord.ShoesTotal; 
             object boutiqueCount = objZTapeRecord.BoutiqueCount; 
             object boutiqueTotal = objZTapeRecord.BoutiqueTotal; 
             object childsCount = objZTapeRecord.ChildsCount; 
             object childsTotal = objZTapeRecord.ChildsTotal; 
             object mensCount = objZTapeRecord.MensCount; 
             object mensTotal = objZTapeRecord.MensTotal; 
             object booksCount = objZTapeRecord.BooksCount; 
             object booksTotal = objZTapeRecord.BooksTotal; 
             object newMerchCount = objZTapeRecord.NewMerchCount; 
             object newMerchTotal = objZTapeRecord.NewMerchTotal; 
             object furnitureCountDisc = objZTapeRecord.FurnitureCountDisc; 
             object furnitureTotalDisc = objZTapeRecord.FurnitureTotalDisc; 
             object jewelryCountDisc = objZTapeRecord.JewelryCountDisc; 
             object jewelryTotalDisc = objZTapeRecord.JewelryTotalDisc; 
             object electricalCountDisc = objZTapeRecord.ElectricalCountDisc; 
             object electricalTotalDisc = objZTapeRecord.ElectricalTotalDisc; 
             object womensCountDisc = objZTapeRecord.WomensCountDisc; 
             object womensTotalDisc = objZTapeRecord.WomensTotalDisc; 
             object binsCountDisc = objZTapeRecord.BinsCountDisc; 
             object binsTotalDisc = objZTapeRecord.BinsTotalDisc; 
             object miscCountDisc = objZTapeRecord.MiscCountDisc; 
             object miscTotalDisc = objZTapeRecord.MiscTotalDisc; 
             object shoesCountDisc = objZTapeRecord.ShoesCountDisc; 
             object shoesTotalDisc = objZTapeRecord.ShoesTotalDisc; 
             object boutiqueCountDisc = objZTapeRecord.BoutiqueCountDisc; 
             object boutiqueTotalDisc = objZTapeRecord.BoutiqueTotalDisc; 
             object childsCountDisc = objZTapeRecord.ChildsCountDisc; 
             object childsTotalDisc = objZTapeRecord.ChildsTotalDisc; 
             object mensCountDisc = objZTapeRecord.MensCountDisc; 
             object mensTotalDisc = objZTapeRecord.MensTotalDisc; 
             object booksCountDisc = objZTapeRecord.BooksCountDisc; 
             object booksTotalDisc = objZTapeRecord.BooksTotalDisc; 
             object correctionCount = objZTapeRecord.CorrectionCount; 
             object correctionTotal = objZTapeRecord.CorrectionTotal; 
             object voidCount = objZTapeRecord.VoidCount; 
             object voidTotal = objZTapeRecord.VoidTotal; 
             object allVoidCount = objZTapeRecord.AllVoidCount; 
             object allVoidTotal = objZTapeRecord.AllVoidTotal; 
 
             if (objZTapeRecord.Date == null) 
                 date = System.DBNull.Value; 
 
             if (objZTapeRecord.LocationID == null) 
                 locationID = System.DBNull.Value; 
 
             if (objZTapeRecord.Register == null) 
                 register = System.DBNull.Value; 
 
             if (objZTapeRecord.AddAdjustment == null) 
                 addAdjustment = System.DBNull.Value; 
 
             if (String.IsNullOrEmpty(objZTapeRecord.AddComment)) 
                 addComment = System.DBNull.Value; 
 
             if (objZTapeRecord.CashAdjustment == null) 
                 cashAdjustment = System.DBNull.Value; 
 
             if (String.IsNullOrEmpty(objZTapeRecord.CashComment)) 
                 cashComment = System.DBNull.Value; 
 
             if (objZTapeRecord.Discount1StCount == null) 
                 discount1StCount = System.DBNull.Value; 
 
             if (objZTapeRecord.Discount1StTotal == null) 
                 discount1StTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.Discount2StCount == null) 
                 discount2StCount = System.DBNull.Value; 
 
             if (objZTapeRecord.Discount2StTotal == null) 
                 discount2StTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.ChargeCount == null) 
                 chargeCount = System.DBNull.Value; 
 
             if (objZTapeRecord.ChargeTotal == null) 
                 chargeTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.CashCount == null) 
                 cashCount = System.DBNull.Value; 
 
             if (objZTapeRecord.CashTotal == null) 
                 cashTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.Tax == null) 
                 tax = System.DBNull.Value; 
 
             if (objZTapeRecord.Discount1ItCount == null) 
                 discount1ItCount = System.DBNull.Value; 
 
             if (objZTapeRecord.Discount1ItTotal == null) 
                 discount1ItTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.Discount2ItCount == null) 
                 discount2ItCount = System.DBNull.Value; 
 
             if (objZTapeRecord.Discount2ItTotal == null) 
                 discount2ItTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.ReturnsCount == null) 
                 returnsCount = System.DBNull.Value; 
 
             if (objZTapeRecord.ReturnsTotal == null) 
                 returnsTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.ReturnsTax == null) 
                 returnsTax = System.DBNull.Value; 
 
             if (objZTapeRecord.FurnitureCount == null) 
                 furnitureCount = System.DBNull.Value; 
 
             if (objZTapeRecord.FurnitureTotal == null) 
                 furnitureTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.JewelryCount == null) 
                 jewelryCount = System.DBNull.Value; 
 
             if (objZTapeRecord.JewelryTotal == null) 
                 jewelryTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.ElectricalCount == null) 
                 electricalCount = System.DBNull.Value; 
 
             if (objZTapeRecord.ElectricalTotal == null) 
                 electricalTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.WomensCount == null) 
                 womensCount = System.DBNull.Value; 
 
             if (objZTapeRecord.WomensTotal == null) 
                 womensTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.BinsCount == null) 
                 binsCount = System.DBNull.Value; 
 
             if (objZTapeRecord.BinsTotal == null) 
                 binsTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.MiscCount == null) 
                 miscCount = System.DBNull.Value; 
 
             if (objZTapeRecord.MiscTotal == null) 
                 miscTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.ShoesCount == null) 
                 shoesCount = System.DBNull.Value; 
 
             if (objZTapeRecord.ShoesTotal == null) 
                 shoesTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.BoutiqueCount == null) 
                 boutiqueCount = System.DBNull.Value; 
 
             if (objZTapeRecord.BoutiqueTotal == null) 
                 boutiqueTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.ChildsCount == null) 
                 childsCount = System.DBNull.Value; 
 
             if (objZTapeRecord.ChildsTotal == null) 
                 childsTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.MensCount == null) 
                 mensCount = System.DBNull.Value; 
 
             if (objZTapeRecord.MensTotal == null) 
                 mensTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.BooksCount == null) 
                 booksCount = System.DBNull.Value; 
 
             if (objZTapeRecord.BooksTotal == null) 
                 booksTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.NewMerchCount == null) 
                 newMerchCount = System.DBNull.Value; 
 
             if (objZTapeRecord.NewMerchTotal == null) 
                 newMerchTotal = System.DBNull.Value; 

             if (objZTapeRecord.FurnitureCountDisc == null) 
                 furnitureCountDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.FurnitureTotalDisc == null) 
                 furnitureTotalDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.JewelryCountDisc == null) 
                 jewelryCountDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.JewelryTotalDisc == null) 
                 jewelryTotalDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.ElectricalCountDisc == null) 
                 electricalCountDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.ElectricalTotalDisc == null) 
                 electricalTotalDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.WomensCountDisc == null) 
                 womensCountDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.WomensTotalDisc == null) 
                 womensTotalDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.BinsCountDisc == null) 
                 binsCountDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.BinsTotalDisc == null) 
                 binsTotalDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.MiscCountDisc == null) 
                 miscCountDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.MiscTotalDisc == null) 
                 miscTotalDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.ShoesCountDisc == null) 
                 shoesCountDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.ShoesTotalDisc == null) 
                 shoesTotalDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.BoutiqueCountDisc == null) 
                 boutiqueCountDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.BoutiqueTotalDisc == null) 
                 boutiqueTotalDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.ChildsCountDisc == null) 
                 childsCountDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.ChildsTotalDisc == null) 
                 childsTotalDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.MensCountDisc == null) 
                 mensCountDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.MensTotalDisc == null) 
                 mensTotalDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.BooksCountDisc == null) 
                 booksCountDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.BooksTotalDisc == null) 
                 booksTotalDisc = System.DBNull.Value; 
 
             if (objZTapeRecord.CorrectionCount == null) 
                 correctionCount = System.DBNull.Value; 
 
             if (objZTapeRecord.CorrectionTotal == null) 
                 correctionTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.VoidCount == null) 
                 voidCount = System.DBNull.Value; 
 
             if (objZTapeRecord.VoidTotal == null) 
                 voidTotal = System.DBNull.Value; 
 
             if (objZTapeRecord.AllVoidCount == null) 
                 allVoidCount = System.DBNull.Value; 
 
             if (objZTapeRecord.AllVoidTotal == null) 
                 allVoidTotal = System.DBNull.Value; 
 
             // for update only 
             if (isUpdate) 
             { 
                 command.Parameters.AddWithValue("@zTapeRecordID", objZTapeRecord.ZTapeRecordID); 
             } 
 
             command.Parameters.AddWithValue("@date", date); 
             command.Parameters.AddWithValue("@locationID", locationID); 
             command.Parameters.AddWithValue("@register", register); 
             command.Parameters.AddWithValue("@addAdjustment", addAdjustment); 
             command.Parameters.AddWithValue("@addComment", addComment); 
             command.Parameters.AddWithValue("@cashAdjustment", cashAdjustment); 
             command.Parameters.AddWithValue("@cashComment", cashComment); 
             command.Parameters.AddWithValue("@discount1StCount", discount1StCount); 
             command.Parameters.AddWithValue("@discount1StTotal", discount1StTotal); 
             command.Parameters.AddWithValue("@discount2StCount", discount2StCount); 
             command.Parameters.AddWithValue("@discount2StTotal", discount2StTotal); 
             command.Parameters.AddWithValue("@chargeCount", chargeCount); 
             command.Parameters.AddWithValue("@chargeTotal", chargeTotal); 
             command.Parameters.AddWithValue("@cashCount", cashCount); 
             command.Parameters.AddWithValue("@cashTotal", cashTotal); 
             command.Parameters.AddWithValue("@tax", tax); 
             command.Parameters.AddWithValue("@discount1ItCount", discount1ItCount); 
             command.Parameters.AddWithValue("@discount1ItTotal", discount1ItTotal); 
             command.Parameters.AddWithValue("@discount2ItCount", discount2ItCount); 
             command.Parameters.AddWithValue("@discount2ItTotal", discount2ItTotal); 
             command.Parameters.AddWithValue("@returnsCount", returnsCount); 
             command.Parameters.AddWithValue("@returnsTotal", returnsTotal); 
             command.Parameters.AddWithValue("@returnsTax", returnsTax); 
             command.Parameters.AddWithValue("@furnitureCount", furnitureCount); 
             command.Parameters.AddWithValue("@furnitureTotal", furnitureTotal); 
             command.Parameters.AddWithValue("@jewelryCount", jewelryCount); 
             command.Parameters.AddWithValue("@jewelryTotal", jewelryTotal); 
             command.Parameters.AddWithValue("@electricalCount", electricalCount); 
             command.Parameters.AddWithValue("@electricalTotal", electricalTotal); 
             command.Parameters.AddWithValue("@womensCount", womensCount); 
             command.Parameters.AddWithValue("@womensTotal", womensTotal); 
             command.Parameters.AddWithValue("@binsCount", binsCount); 
             command.Parameters.AddWithValue("@binsTotal", binsTotal); 
             command.Parameters.AddWithValue("@miscCount", miscCount); 
             command.Parameters.AddWithValue("@miscTotal", miscTotal); 
             command.Parameters.AddWithValue("@shoesCount", shoesCount); 
             command.Parameters.AddWithValue("@shoesTotal", shoesTotal); 
             command.Parameters.AddWithValue("@boutiqueCount", boutiqueCount); 
             command.Parameters.AddWithValue("@boutiqueTotal", boutiqueTotal); 
             command.Parameters.AddWithValue("@childsCount", childsCount); 
             command.Parameters.AddWithValue("@childsTotal", childsTotal); 
             command.Parameters.AddWithValue("@mensCount", mensCount); 
             command.Parameters.AddWithValue("@mensTotal", mensTotal); 
             command.Parameters.AddWithValue("@booksCount", booksCount); 
             command.Parameters.AddWithValue("@booksTotal", booksTotal); 
             command.Parameters.AddWithValue("@newMerchCount", newMerchCount); 
             command.Parameters.AddWithValue("@newMerchTotal", newMerchTotal); 
             command.Parameters.AddWithValue("@furnitureCountDisc", furnitureCountDisc); 
             command.Parameters.AddWithValue("@furnitureTotalDisc", furnitureTotalDisc); 
             command.Parameters.AddWithValue("@jewelryCountDisc", jewelryCountDisc); 
             command.Parameters.AddWithValue("@jewelryTotalDisc", jewelryTotalDisc); 
             command.Parameters.AddWithValue("@electricalCountDisc", electricalCountDisc); 
             command.Parameters.AddWithValue("@electricalTotalDisc", electricalTotalDisc); 
             command.Parameters.AddWithValue("@womensCountDisc", womensCountDisc); 
             command.Parameters.AddWithValue("@womensTotalDisc", womensTotalDisc); 
             command.Parameters.AddWithValue("@binsCountDisc", binsCountDisc); 
             command.Parameters.AddWithValue("@binsTotalDisc", binsTotalDisc); 
             command.Parameters.AddWithValue("@miscCountDisc", miscCountDisc); 
             command.Parameters.AddWithValue("@miscTotalDisc", miscTotalDisc); 
             command.Parameters.AddWithValue("@shoesCountDisc", shoesCountDisc); 
             command.Parameters.AddWithValue("@shoesTotalDisc", shoesTotalDisc); 
             command.Parameters.AddWithValue("@boutiqueCountDisc", boutiqueCountDisc); 
             command.Parameters.AddWithValue("@boutiqueTotalDisc", boutiqueTotalDisc); 
             command.Parameters.AddWithValue("@childsCountDisc", childsCountDisc); 
             command.Parameters.AddWithValue("@childsTotalDisc", childsTotalDisc); 
             command.Parameters.AddWithValue("@mensCountDisc", mensCountDisc); 
             command.Parameters.AddWithValue("@mensTotalDisc", mensTotalDisc); 
             command.Parameters.AddWithValue("@booksCountDisc", booksCountDisc); 
             command.Parameters.AddWithValue("@booksTotalDisc", booksTotalDisc); 
             command.Parameters.AddWithValue("@correctionCount", correctionCount); 
             command.Parameters.AddWithValue("@correctionTotal", correctionTotal); 
             command.Parameters.AddWithValue("@voidCount", voidCount); 
             command.Parameters.AddWithValue("@voidTotal", voidTotal); 
             command.Parameters.AddWithValue("@allVoidCount", allVoidCount); 
             command.Parameters.AddWithValue("@allVoidTotal", allVoidTotal); 
 
             // execute and return value 
             int newlyCreatedZTapeRecordID = objZTapeRecord.ZTapeRecordID; 
 
             if (isUpdate) 
                 command.ExecuteNonQuery(); 
             else 
                 newlyCreatedZTapeRecordID = (int)command.ExecuteScalar(); 
 
             command.Dispose(); 
             connection.Close(); 
             connection.Dispose(); 
 
             return newlyCreatedZTapeRecordID; 
         } 
 
         /// <summary>
         /// Deletes a record based on primary key(s) 
         /// </summary>
         public static void Delete(int zTapeRecordID) 
         { 
             SqlConnection connection = Dbase.GetConnection(); 
             SqlCommand command = Dbase.GetCommand("[dbo].[pr_ZTapeRecord_Delete]", connection); 
 
             command.Parameters.AddWithValue("@zTapeRecordID", zTapeRecordID); 
 
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
