#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectBase\ZTapeRecordBase.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CCFFE52229EE3FEA9B40C8B3E3E8B32431CB60BB"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectBase\ZTapeRecordBase.cs"
using System; 
using System.Data; 
using jmann.DataLayer; 
using jmann.BusinessObject; 
using System.Web.Script.Serialization; 
 
namespace jmann.BusinessObject.Base 
{ 
     /// <summary>
     /// Base class for ZTapeRecord.  Do not make changes to this class,
     /// instead, put additional code in the ZTapeRecord class 
     /// </summary>
     public class ZTapeRecordBase
     { 
         /// <summary> 
         /// Gets or Sets ZTapeRecordID 
         /// </summary> 
         public int ZTapeRecordID { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Date 
         /// </summary> 
         public DateTime? Date { get; set; } 
 
         /// <summary> 
         /// Gets or Sets LocationID 
         /// </summary> 
         public int? LocationID { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Register 
         /// </summary> 
         public int? Register { get; set; } 
 
         /// <summary> 
         /// Gets or Sets AddAdjustment 
         /// </summary> 
         public decimal? AddAdjustment { get; set; } 
 
         /// <summary> 
         /// Gets or Sets AddComment 
         /// </summary> 
         public string AddComment { get; set; } 
 
         /// <summary> 
         /// Gets or Sets CashAdjustment 
         /// </summary> 
         public decimal? CashAdjustment { get; set; } 
 
         /// <summary> 
         /// Gets or Sets CashComment 
         /// </summary> 
         public string CashComment { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Discount1StCount 
         /// </summary> 
         public int? Discount1StCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Discount1StTotal 
         /// </summary> 
         public decimal? Discount1StTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Discount2StCount 
         /// </summary> 
         public int? Discount2StCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Discount2StTotal 
         /// </summary> 
         public decimal? Discount2StTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ChargeCount 
         /// </summary> 
         public int? ChargeCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ChargeTotal 
         /// </summary> 
         public decimal? ChargeTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets CashCount 
         /// </summary> 
         public int? CashCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets CashTotal 
         /// </summary> 
         public decimal? CashTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Tax 
         /// </summary> 
         public decimal? Tax { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Discount1ItCount 
         /// </summary> 
         public int? Discount1ItCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Discount1ItTotal 
         /// </summary> 
         public decimal? Discount1ItTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Discount2ItCount 
         /// </summary> 
         public int? Discount2ItCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Discount2ItTotal 
         /// </summary> 
         public decimal? Discount2ItTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ReturnsCount 
         /// </summary> 
         public int? ReturnsCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ReturnsTotal 
         /// </summary> 
         public decimal? ReturnsTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ReturnsTax 
         /// </summary> 
         public decimal? ReturnsTax { get; set; } 
 
         /// <summary> 
         /// Gets or Sets FurnitureCount 
         /// </summary> 
         public int? FurnitureCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets FurnitureTotal 
         /// </summary> 
         public decimal? FurnitureTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets JewelryCount 
         /// </summary> 
         public int? JewelryCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets JewelryTotal 
         /// </summary> 
         public decimal? JewelryTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ElectricalCount 
         /// </summary> 
         public int? ElectricalCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ElectricalTotal 
         /// </summary> 
         public decimal? ElectricalTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets WomensCount 
         /// </summary> 
         public int? WomensCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets WomensTotal 
         /// </summary> 
         public decimal? WomensTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets BinsCount 
         /// </summary> 
         public int? BinsCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets BinsTotal 
         /// </summary> 
         public decimal? BinsTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets MiscCount 
         /// </summary> 
         public int? MiscCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets MiscTotal 
         /// </summary> 
         public decimal? MiscTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ShoesCount 
         /// </summary> 
         public int? ShoesCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ShoesTotal 
         /// </summary> 
         public decimal? ShoesTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets BoutiqueCount 
         /// </summary> 
         public int? BoutiqueCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets BoutiqueTotal 
         /// </summary> 
         public decimal? BoutiqueTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ChildsCount 
         /// </summary> 
         public int? ChildsCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ChildsTotal 
         /// </summary> 
         public decimal? ChildsTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets MensCount 
         /// </summary> 
         public int? MensCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets MensTotal 
         /// </summary> 
         public decimal? MensTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets BooksCount 
         /// </summary> 
         public int? BooksCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets BooksTotal 
         /// </summary> 
         public decimal? BooksTotal { get; set; } 

          /// <summary> 
         /// Gets or Sets NewMerchCount 
         /// </summary> 
         public int? NewMerchCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets NewMerchTotal 
         /// </summary> 
         public decimal? NewMerchTotal { get; set; } 

         /// <summary> 
         /// Gets or Sets FurnitureCountDisc 
         /// </summary> 
         public int? FurnitureCountDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets FurnitureTotalDisc 
         /// </summary> 
         public decimal? FurnitureTotalDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets JewelryCountDisc 
         /// </summary> 
         public int? JewelryCountDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets JewelryTotalDisc 
         /// </summary> 
         public decimal? JewelryTotalDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ElectricalCountDisc 
         /// </summary> 
         public int? ElectricalCountDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ElectricalTotalDisc 
         /// </summary> 
         public decimal? ElectricalTotalDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets WomensCountDisc 
         /// </summary> 
         public int? WomensCountDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets WomensTotalDisc 
         /// </summary> 
         public decimal? WomensTotalDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets BinsCountDisc 
         /// </summary> 
         public int? BinsCountDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets BinsTotalDisc 
         /// </summary> 
         public decimal? BinsTotalDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets MiscCountDisc 
         /// </summary> 
         public int? MiscCountDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets MiscTotalDisc 
         /// </summary> 
         public decimal? MiscTotalDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ShoesCountDisc 
         /// </summary> 
         public int? ShoesCountDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ShoesTotalDisc 
         /// </summary> 
         public decimal? ShoesTotalDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets BoutiqueCountDisc 
         /// </summary> 
         public int? BoutiqueCountDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets BoutiqueTotalDisc 
         /// </summary> 
         public decimal? BoutiqueTotalDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ChildsCountDisc 
         /// </summary> 
         public int? ChildsCountDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ChildsTotalDisc 
         /// </summary> 
         public decimal? ChildsTotalDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets MensCountDisc 
         /// </summary> 
         public int? MensCountDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets MensTotalDisc 
         /// </summary> 
         public decimal? MensTotalDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets BooksCountDisc 
         /// </summary> 
         public int? BooksCountDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets BooksTotalDisc 
         /// </summary> 
         public decimal? BooksTotalDisc { get; set; } 
 
         /// <summary> 
         /// Gets or Sets CorrectionCount 
         /// </summary> 
         public int? CorrectionCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets CorrectionTotal 
         /// </summary> 
         public decimal? CorrectionTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets VoidCount 
         /// </summary> 
         public int? VoidCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets VoidTotal 
         /// </summary> 
         public decimal? VoidTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets AllVoidCount 
         /// </summary> 
         public int? AllVoidCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets AllVoidTotal 
         /// </summary> 
         public decimal? AllVoidTotal { get; set; } 
 
         /// <summary> 
         /// Gets or sets the Related Location.  Related to column LocationID 
         /// </summary> 
         [ScriptIgnore] 
         public Lazy<Location> Location 
         { 
             get 
             { 
                 int value; 
                 bool hasValue = Int32.TryParse(LocationID.ToString(), out value); 
 
                 if (hasValue) 
                     return new Lazy<Location>(() => jmann.BusinessObject.Location.SelectByPrimaryKey(value)); 
                 else 
                     return null; 
             } 
             set{ } 
         }  
 
 
         /// <summary> 
         /// Constructor 
         /// </summary> 
         public ZTapeRecordBase() 
         { 
         } 
 
         /// <summary>
         /// Selects a record by primary key(s) 
         /// </summary>
         public static ZTapeRecord SelectByPrimaryKey(int zTapeRecordID) 
         { 
             return ZTapeRecordDataLayer.SelectByPrimaryKey(zTapeRecordID); 
         } 
 
         /// <summary> 
         /// Selects all records as a collection (List) of ZTapeRecord 
         /// </summary> 
         public static ZTapeRecordCollection SelectAll() 
         { 
             return ZTapeRecordDataLayer.SelectAll(); 
         } 
 
         /// <summary> 
         /// Selects all records as a collection (List) of ZTapeRecord sorted by the sort expression 
         /// </summary> 
         public static ZTapeRecordCollection SelectAll(string sortExpression) 
         { 
             ZTapeRecordCollection objZTapeRecordCol = ZTapeRecordDataLayer.SelectAll();
             return SortByExpression(objZTapeRecordCol, sortExpression);
         } 
 
         /// <summary>
         /// Selects all ZTapeRecord by Location, related to column LocationID 
         /// </summary> 
         public static ZTapeRecordCollection SelectZTapeRecordCollectionByLocation(int locationID) 
         { 
             return ZTapeRecordDataLayer.SelectZTapeRecordCollectionByLocation(locationID); 
         } 
 
         /// <summary>
         /// Selects all ZTapeRecord by Location, related to column LocationID, sorted by the sort expression 
         /// </summary> 
         public static ZTapeRecordCollection SelectZTapeRecordCollectionByLocation(int locationID, string sortExpression) 
         { 
             ZTapeRecordCollection objZTapeRecordCol = ZTapeRecordDataLayer.SelectZTapeRecordCollectionByLocation(locationID); 
             return SortByExpression(objZTapeRecordCol, sortExpression);
         } 
 
         /// <summary>
         /// Selects ZTapeRecordID and Date columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc 
         /// </summary> 
         public static ZTapeRecordCollection SelectZTapeRecordDropDownListData() 
         { 
             return ZTapeRecordDataLayer.SelectZTapeRecordDropDownListData(); 
         } 
 
         /// <summary>
         /// Sorts the ZTapeRecordCollection by sort expression 
         /// </summary> 
         public static ZTapeRecordCollection SortByExpression(ZTapeRecordCollection objZTapeRecordCol, string sortExpression) 
         { 
             bool isSortDescending = sortExpression.Contains(" DESC");

             if (isSortDescending)
                 sortExpression = sortExpression.Replace(" DESC", "");

             switch (sortExpression)
             {
                 case "ZTapeRecordID":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByZTapeRecordID);
                     break;
                 case "Date":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByDate);
                     break;
                 case "LocationID":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByLocationID);
                     break;
                 case "Register":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByRegister);
                     break;
                 case "AddAdjustment":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByAddAdjustment);
                     break;
                 case "AddComment":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByAddComment);
                     break;
                 case "CashAdjustment":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByCashAdjustment);
                     break;
                 case "CashComment":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByCashComment);
                     break;
                 case "Discount1StCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByDiscount1StCount);
                     break;
                 case "Discount1StTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByDiscount1StTotal);
                     break;
                 case "Discount2StCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByDiscount2StCount);
                     break;
                 case "Discount2StTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByDiscount2StTotal);
                     break;
                 case "ChargeCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByChargeCount);
                     break;
                 case "ChargeTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByChargeTotal);
                     break;
                 case "CashCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByCashCount);
                     break;
                 case "CashTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByCashTotal);
                     break;
                 case "Tax":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByTax);
                     break;
                 case "Discount1ItCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByDiscount1ItCount);
                     break;
                 case "Discount1ItTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByDiscount1ItTotal);
                     break;
                 case "Discount2ItCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByDiscount2ItCount);
                     break;
                 case "Discount2ItTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByDiscount2ItTotal);
                     break;
                 case "ReturnsCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByReturnsCount);
                     break;
                 case "ReturnsTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByReturnsTotal);
                     break;
                 case "ReturnsTax":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByReturnsTax);
                     break;
                 case "FurnitureCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByFurnitureCount);
                     break;
                 case "FurnitureTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByFurnitureTotal);
                     break;
                 case "JewelryCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByJewelryCount);
                     break;
                 case "JewelryTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByJewelryTotal);
                     break;
                 case "ElectricalCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByElectricalCount);
                     break;
                 case "ElectricalTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByElectricalTotal);
                     break;
                 case "WomensCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByWomensCount);
                     break;
                 case "WomensTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByWomensTotal);
                     break;
                 case "BinsCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByBinsCount);
                     break;
                 case "BinsTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByBinsTotal);
                     break;
                 case "MiscCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByMiscCount);
                     break;
                 case "MiscTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByMiscTotal);
                     break;
                 case "ShoesCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByShoesCount);
                     break;
                 case "ShoesTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByShoesTotal);
                     break;
                 case "BoutiqueCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByBoutiqueCount);
                     break;
                 case "BoutiqueTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByBoutiqueTotal);
                     break;
                 case "ChildsCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByChildsCount);
                     break;
                 case "ChildsTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByChildsTotal);
                     break;
                 case "MensCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByMensCount);
                     break;
                 case "MensTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByMensTotal);
                     break;
                 case "BooksCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByBooksCount);
                     break;
                 case "BooksTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByBooksTotal);
                     break;
                 case "NewMerchCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByNewMerchCount);
                     break;
                 case "NewMerchTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByNewMerchTotal);
                     break;
                 case "FurnitureCountDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByFurnitureCountDisc);
                     break;
                 case "FurnitureTotalDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByFurnitureTotalDisc);
                     break;
                 case "JewelryCountDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByJewelryCountDisc);
                     break;
                 case "JewelryTotalDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByJewelryTotalDisc);
                     break;
                 case "ElectricalCountDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByElectricalCountDisc);
                     break;
                 case "ElectricalTotalDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByElectricalTotalDisc);
                     break;
                 case "WomensCountDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByWomensCountDisc);
                     break;
                 case "WomensTotalDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByWomensTotalDisc);
                     break;
                 case "BinsCountDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByBinsCountDisc);
                     break;
                 case "BinsTotalDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByBinsTotalDisc);
                     break;
                 case "MiscCountDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByMiscCountDisc);
                     break;
                 case "MiscTotalDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByMiscTotalDisc);
                     break;
                 case "ShoesCountDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByShoesCountDisc);
                     break;
                 case "ShoesTotalDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByShoesTotalDisc);
                     break;
                 case "BoutiqueCountDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByBoutiqueCountDisc);
                     break;
                 case "BoutiqueTotalDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByBoutiqueTotalDisc);
                     break;
                 case "ChildsCountDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByChildsCountDisc);
                     break;
                 case "ChildsTotalDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByChildsTotalDisc);
                     break;
                 case "MensCountDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByMensCountDisc);
                     break;
                 case "MensTotalDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByMensTotalDisc);
                     break;
                 case "BooksCountDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByBooksCountDisc);
                     break;
                 case "BooksTotalDisc":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByBooksTotalDisc);
                     break;
                 case "CorrectionCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByCorrectionCount);
                     break;
                 case "CorrectionTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByCorrectionTotal);
                     break;
                 case "VoidCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByVoidCount);
                     break;
                 case "VoidTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByVoidTotal);
                     break;
                 case "AllVoidCount":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByAllVoidCount);
                     break;
                 case "AllVoidTotal":
                     objZTapeRecordCol.Sort(jmann.BusinessObject.ZTapeRecord.ByAllVoidTotal);
                     break;
                 default:
                     break;
             }

             if (isSortDescending) 
                 objZTapeRecordCol.Reverse();

             return objZTapeRecordCol;
         } 
 
         /// <summary>
         /// Inserts a record 
         /// </summary> 
         public int Insert() 
         { 
             ZTapeRecord objZTapeRecord = (ZTapeRecord)this; 
             return ZTapeRecordDataLayer.Insert(objZTapeRecord); 
         } 
 
         /// <summary>
         /// Updates a record 
         /// </summary> 
         public void Update() 
         { 
             ZTapeRecord objZTapeRecord = (ZTapeRecord)this; 
             ZTapeRecordDataLayer.Update(objZTapeRecord); 
         } 
 
         /// <summary>
         /// Deletes a record based on primary key(s) 
         /// </summary>
         public static void Delete(int zTapeRecordID) 
         { 
             ZTapeRecordDataLayer.Delete(zTapeRecordID); 
         } 
 
         /// <summary> 
         /// Compares ZTapeRecordID used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByZTapeRecordID = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return x.ZTapeRecordID.CompareTo(y.ZTapeRecordID); 
         }; 
 
         /// <summary> 
         /// Compares Date used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByDate = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.Date, y.Date); 
         }; 
 
         /// <summary> 
         /// Compares LocationID used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByLocationID = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.LocationID, y.LocationID); 
         }; 
 
         /// <summary> 
         /// Compares Register used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByRegister = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.Register, y.Register); 
         }; 
 
         /// <summary> 
         /// Compares AddAdjustment used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByAddAdjustment = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.AddAdjustment, y.AddAdjustment); 
         }; 
 
         /// <summary> 
         /// Compares AddComment used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByAddComment = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             string value1 = x.AddComment ?? String.Empty; 
             string value2 = y.AddComment ?? String.Empty; 
             return value1.CompareTo(value2); 
         }; 
 
         /// <summary> 
         /// Compares CashAdjustment used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByCashAdjustment = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.CashAdjustment, y.CashAdjustment); 
         }; 
 
         /// <summary> 
         /// Compares CashComment used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByCashComment = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             string value1 = x.CashComment ?? String.Empty; 
             string value2 = y.CashComment ?? String.Empty; 
             return value1.CompareTo(value2); 
         }; 
 
         /// <summary> 
         /// Compares Discount1StCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByDiscount1StCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.Discount1StCount, y.Discount1StCount); 
         }; 
 
         /// <summary> 
         /// Compares Discount1StTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByDiscount1StTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.Discount1StTotal, y.Discount1StTotal); 
         }; 
 
         /// <summary> 
         /// Compares Discount2StCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByDiscount2StCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.Discount2StCount, y.Discount2StCount); 
         }; 
 
         /// <summary> 
         /// Compares Discount2StTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByDiscount2StTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.Discount2StTotal, y.Discount2StTotal); 
         }; 
 
         /// <summary> 
         /// Compares ChargeCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByChargeCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ChargeCount, y.ChargeCount); 
         }; 
 
         /// <summary> 
         /// Compares ChargeTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByChargeTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ChargeTotal, y.ChargeTotal); 
         }; 
 
         /// <summary> 
         /// Compares CashCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByCashCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.CashCount, y.CashCount); 
         }; 
 
         /// <summary> 
         /// Compares CashTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByCashTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.CashTotal, y.CashTotal); 
         }; 
 
         /// <summary> 
         /// Compares Tax used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByTax = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.Tax, y.Tax); 
         }; 
 
         /// <summary> 
         /// Compares Discount1ItCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByDiscount1ItCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.Discount1ItCount, y.Discount1ItCount); 
         }; 
 
         /// <summary> 
         /// Compares Discount1ItTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByDiscount1ItTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.Discount1ItTotal, y.Discount1ItTotal); 
         }; 
 
         /// <summary> 
         /// Compares Discount2ItCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByDiscount2ItCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.Discount2ItCount, y.Discount2ItCount); 
         }; 
 
         /// <summary> 
         /// Compares Discount2ItTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByDiscount2ItTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.Discount2ItTotal, y.Discount2ItTotal); 
         }; 
 
         /// <summary> 
         /// Compares ReturnsCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByReturnsCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ReturnsCount, y.ReturnsCount); 
         }; 
 
         /// <summary> 
         /// Compares ReturnsTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByReturnsTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ReturnsTotal, y.ReturnsTotal); 
         }; 
 
         /// <summary> 
         /// Compares ReturnsTax used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByReturnsTax = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ReturnsTax, y.ReturnsTax); 
         }; 
 
         /// <summary> 
         /// Compares FurnitureCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByFurnitureCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.FurnitureCount, y.FurnitureCount); 
         }; 
 
         /// <summary> 
         /// Compares FurnitureTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByFurnitureTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.FurnitureTotal, y.FurnitureTotal); 
         }; 
 
         /// <summary> 
         /// Compares JewelryCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByJewelryCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.JewelryCount, y.JewelryCount); 
         }; 
 
         /// <summary> 
         /// Compares JewelryTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByJewelryTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.JewelryTotal, y.JewelryTotal); 
         }; 
 
         /// <summary> 
         /// Compares ElectricalCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByElectricalCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ElectricalCount, y.ElectricalCount); 
         }; 
 
         /// <summary> 
         /// Compares ElectricalTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByElectricalTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ElectricalTotal, y.ElectricalTotal); 
         }; 
 
         /// <summary> 
         /// Compares WomensCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByWomensCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.WomensCount, y.WomensCount); 
         }; 
 
         /// <summary> 
         /// Compares WomensTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByWomensTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.WomensTotal, y.WomensTotal); 
         }; 
 
         /// <summary> 
         /// Compares BinsCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByBinsCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.BinsCount, y.BinsCount); 
         }; 
 
         /// <summary> 
         /// Compares BinsTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByBinsTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.BinsTotal, y.BinsTotal); 
         }; 
 
         /// <summary> 
         /// Compares MiscCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByMiscCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.MiscCount, y.MiscCount); 
         }; 
 
         /// <summary> 
         /// Compares MiscTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByMiscTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.MiscTotal, y.MiscTotal); 
         }; 
 
         /// <summary> 
         /// Compares ShoesCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByShoesCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ShoesCount, y.ShoesCount); 
         }; 
 
         /// <summary> 
         /// Compares ShoesTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByShoesTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ShoesTotal, y.ShoesTotal); 
         }; 
 
         /// <summary> 
         /// Compares BoutiqueCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByBoutiqueCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.BoutiqueCount, y.BoutiqueCount); 
         }; 
 
         /// <summary> 
         /// Compares BoutiqueTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByBoutiqueTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.BoutiqueTotal, y.BoutiqueTotal); 
         }; 
 
         /// <summary> 
         /// Compares ChildsCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByChildsCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ChildsCount, y.ChildsCount); 
         }; 
 
         /// <summary> 
         /// Compares ChildsTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByChildsTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ChildsTotal, y.ChildsTotal); 
         }; 
 
         /// <summary> 
         /// Compares MensCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByMensCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.MensCount, y.MensCount); 
         }; 
 
         /// <summary> 
         /// Compares MensTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByMensTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.MensTotal, y.MensTotal); 
         }; 
 
         /// <summary> 
         /// Compares BooksCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByBooksCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.BooksCount, y.BooksCount); 
         }; 
 
         /// <summary> 
         /// Compares BooksTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByBooksTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.BooksTotal, y.BooksTotal); 
         }; 
  
         /// <summary> 
         /// Compares NewMerchCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByNewMerchCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.NewMerchCount, y.NewMerchCount); 
         }; 
 
         /// <summary> 
         /// Compares NewMerchTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByNewMerchTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.NewMerchTotal, y.NewMerchTotal); 
         }; 
 
         /// <summary> 
         /// Compares FurnitureCountDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByFurnitureCountDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.FurnitureCountDisc, y.FurnitureCountDisc); 
         }; 
 
         /// <summary> 
         /// Compares FurnitureTotalDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByFurnitureTotalDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.FurnitureTotalDisc, y.FurnitureTotalDisc); 
         }; 
 
         /// <summary> 
         /// Compares JewelryCountDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByJewelryCountDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.JewelryCountDisc, y.JewelryCountDisc); 
         }; 
 
         /// <summary> 
         /// Compares JewelryTotalDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByJewelryTotalDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.JewelryTotalDisc, y.JewelryTotalDisc); 
         }; 
 
         /// <summary> 
         /// Compares ElectricalCountDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByElectricalCountDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ElectricalCountDisc, y.ElectricalCountDisc); 
         }; 
 
         /// <summary> 
         /// Compares ElectricalTotalDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByElectricalTotalDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ElectricalTotalDisc, y.ElectricalTotalDisc); 
         }; 
 
         /// <summary> 
         /// Compares WomensCountDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByWomensCountDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.WomensCountDisc, y.WomensCountDisc); 
         }; 
 
         /// <summary> 
         /// Compares WomensTotalDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByWomensTotalDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.WomensTotalDisc, y.WomensTotalDisc); 
         }; 
 
         /// <summary> 
         /// Compares BinsCountDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByBinsCountDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.BinsCountDisc, y.BinsCountDisc); 
         }; 
 
         /// <summary> 
         /// Compares BinsTotalDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByBinsTotalDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.BinsTotalDisc, y.BinsTotalDisc); 
         }; 
 
         /// <summary> 
         /// Compares MiscCountDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByMiscCountDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.MiscCountDisc, y.MiscCountDisc); 
         }; 
 
         /// <summary> 
         /// Compares MiscTotalDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByMiscTotalDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.MiscTotalDisc, y.MiscTotalDisc); 
         }; 
 
         /// <summary> 
         /// Compares ShoesCountDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByShoesCountDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ShoesCountDisc, y.ShoesCountDisc); 
         }; 
 
         /// <summary> 
         /// Compares ShoesTotalDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByShoesTotalDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ShoesTotalDisc, y.ShoesTotalDisc); 
         }; 
 
         /// <summary> 
         /// Compares BoutiqueCountDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByBoutiqueCountDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.BoutiqueCountDisc, y.BoutiqueCountDisc); 
         }; 
 
         /// <summary> 
         /// Compares BoutiqueTotalDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByBoutiqueTotalDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.BoutiqueTotalDisc, y.BoutiqueTotalDisc); 
         }; 
 
         /// <summary> 
         /// Compares ChildsCountDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByChildsCountDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ChildsCountDisc, y.ChildsCountDisc); 
         }; 
 
         /// <summary> 
         /// Compares ChildsTotalDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByChildsTotalDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.ChildsTotalDisc, y.ChildsTotalDisc); 
         }; 
 
         /// <summary> 
         /// Compares MensCountDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByMensCountDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.MensCountDisc, y.MensCountDisc); 
         }; 
 
         /// <summary> 
         /// Compares MensTotalDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByMensTotalDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.MensTotalDisc, y.MensTotalDisc); 
         }; 
 
         /// <summary> 
         /// Compares BooksCountDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByBooksCountDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.BooksCountDisc, y.BooksCountDisc); 
         }; 
 
         /// <summary> 
         /// Compares BooksTotalDisc used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByBooksTotalDisc = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.BooksTotalDisc, y.BooksTotalDisc); 
         }; 
 
         /// <summary> 
         /// Compares CorrectionCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByCorrectionCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.CorrectionCount, y.CorrectionCount); 
         }; 
 
         /// <summary> 
         /// Compares CorrectionTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByCorrectionTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.CorrectionTotal, y.CorrectionTotal); 
         }; 
 
         /// <summary> 
         /// Compares VoidCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByVoidCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.VoidCount, y.VoidCount); 
         }; 
 
         /// <summary> 
         /// Compares VoidTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByVoidTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.VoidTotal, y.VoidTotal); 
         }; 
 
         /// <summary> 
         /// Compares AllVoidCount used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByAllVoidCount = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.AllVoidCount, y.AllVoidCount); 
         }; 
 
         /// <summary> 
         /// Compares AllVoidTotal used for sorting 
         /// </summary> 
         public static Comparison<ZTapeRecord> ByAllVoidTotal = delegate(ZTapeRecord x, ZTapeRecord y) 
         { 
             return Nullable.Compare(x.AllVoidTotal, y.AllVoidTotal); 
         }; 
 
     } 
} 


#line default
#line hidden
