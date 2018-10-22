#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectBase\MonthlyGoalsBase.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BC8B0F168379174C542BE56A5B8DF6ABCD9F0B4E"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectBase\MonthlyGoalsBase.cs"
using System; 
using System.Data; 
using jmann.DataLayer; 
using jmann.BusinessObject; 
using System.Web.Script.Serialization; 
 
namespace jmann.BusinessObject.Base 
{ 
     /// <summary>
     /// Base class for MonthlyGoals.  Do not make changes to this class,
     /// instead, put additional code in the MonthlyGoals class 
     /// </summary>
     public class MonthlyGoalsBase
     { 
         /// <summary> 
         /// Gets or Sets MonthlyGoalsID 
         /// </summary> 
         public int MonthlyGoalsID { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Year 
         /// </summary> 
         public Int16? Year { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Month 
         /// </summary> 
         public Int16? Month { get; set; } 
 
         /// <summary> 
         /// Gets or Sets LocationID 
         /// </summary> 
         public int? LocationID { get; set; } 
 
         /// <summary> 
         /// Gets or Sets FurnitureTotal 
         /// </summary> 
         public double? FurnitureTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets JewelryTotal 
         /// </summary> 
         public double? JewelryTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ElectricalTotal 
         /// </summary> 
         public double? ElectricalTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets HangTotal 
         /// </summary> 
         public double? HangTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets BinsTotal 
         /// </summary> 
         public double? BinsTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets MiscTotal 
         /// </summary> 
         public double? MiscTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ShoesTotal 
         /// </summary> 
         public double? ShoesTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets BoutiqueTotal 
         /// </summary> 
         public double? BoutiqueTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets BooksTotal 
         /// </summary> 
         public double? BooksTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Total 
         /// </summary> 
         public decimal? Total { get; set; } 
 
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
         public MonthlyGoalsBase() 
         { 
         } 
 
         /// <summary>
         /// Selects a record by primary key(s) 
         /// </summary>
         public static MonthlyGoals SelectByPrimaryKey(int monthlyGoalsID) 
         { 
             return MonthlyGoalsDataLayer.SelectByPrimaryKey(monthlyGoalsID); 
         } 
 
         /// <summary> 
         /// Selects all records as a collection (List) of MonthlyGoals 
         /// </summary> 
         public static MonthlyGoalsCollection SelectAll() 
         { 
             return MonthlyGoalsDataLayer.SelectAll(); 
         } 
 
         /// <summary> 
         /// Selects all records as a collection (List) of MonthlyGoals sorted by the sort expression 
         /// </summary> 
         public static MonthlyGoalsCollection SelectAll(string sortExpression) 
         { 
             MonthlyGoalsCollection objMonthlyGoalsCol = MonthlyGoalsDataLayer.SelectAll();
             return SortByExpression(objMonthlyGoalsCol, sortExpression);
         } 
 
         /// <summary>
         /// Selects all MonthlyGoals by Location, related to column LocationID 
         /// </summary> 
         public static MonthlyGoalsCollection SelectMonthlyGoalsCollectionByLocation(int locationID) 
         { 
             return MonthlyGoalsDataLayer.SelectMonthlyGoalsCollectionByLocation(locationID); 
         } 
 
         /// <summary>
         /// Selects all MonthlyGoals by Location, related to column LocationID, sorted by the sort expression 
         /// </summary> 
         public static MonthlyGoalsCollection SelectMonthlyGoalsCollectionByLocation(int locationID, string sortExpression) 
         { 
             MonthlyGoalsCollection objMonthlyGoalsCol = MonthlyGoalsDataLayer.SelectMonthlyGoalsCollectionByLocation(locationID); 
             return SortByExpression(objMonthlyGoalsCol, sortExpression);
         } 
 
         /// <summary>
         /// Selects MonthlyGoalsID and Year columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc 
         /// </summary> 
         public static MonthlyGoalsCollection SelectMonthlyGoalsDropDownListData() 
         { 
             return MonthlyGoalsDataLayer.SelectMonthlyGoalsDropDownListData(); 
         } 
 
         /// <summary>
         /// Sorts the MonthlyGoalsCollection by sort expression 
         /// </summary> 
         public static MonthlyGoalsCollection SortByExpression(MonthlyGoalsCollection objMonthlyGoalsCol, string sortExpression) 
         { 
             bool isSortDescending = sortExpression.Contains(" DESC");

             if (isSortDescending)
                 sortExpression = sortExpression.Replace(" DESC", "");

             switch (sortExpression)
             {
                 case "MonthlyGoalsID":
                     objMonthlyGoalsCol.Sort(jmann.BusinessObject.MonthlyGoals.ByMonthlyGoalsID);
                     break;
                 case "Year":
                     objMonthlyGoalsCol.Sort(jmann.BusinessObject.MonthlyGoals.ByYear);
                     break;
                 case "Month":
                     objMonthlyGoalsCol.Sort(jmann.BusinessObject.MonthlyGoals.ByMonth);
                     break;
                 case "LocationID":
                     objMonthlyGoalsCol.Sort(jmann.BusinessObject.MonthlyGoals.ByLocationID);
                     break;
                 case "FurnitureTotal":
                     objMonthlyGoalsCol.Sort(jmann.BusinessObject.MonthlyGoals.ByFurnitureTotal);
                     break;
                 case "JewelryTotal":
                     objMonthlyGoalsCol.Sort(jmann.BusinessObject.MonthlyGoals.ByJewelryTotal);
                     break;
                 case "ElectricalTotal":
                     objMonthlyGoalsCol.Sort(jmann.BusinessObject.MonthlyGoals.ByElectricalTotal);
                     break;
                 case "HangTotal":
                     objMonthlyGoalsCol.Sort(jmann.BusinessObject.MonthlyGoals.ByHangTotal);
                     break;
                 case "BinsTotal":
                     objMonthlyGoalsCol.Sort(jmann.BusinessObject.MonthlyGoals.ByBinsTotal);
                     break;
                 case "MiscTotal":
                     objMonthlyGoalsCol.Sort(jmann.BusinessObject.MonthlyGoals.ByMiscTotal);
                     break;
                 case "ShoesTotal":
                     objMonthlyGoalsCol.Sort(jmann.BusinessObject.MonthlyGoals.ByShoesTotal);
                     break;
                 case "BoutiqueTotal":
                     objMonthlyGoalsCol.Sort(jmann.BusinessObject.MonthlyGoals.ByBoutiqueTotal);
                     break;
                 case "BooksTotal":
                     objMonthlyGoalsCol.Sort(jmann.BusinessObject.MonthlyGoals.ByBooksTotal);
                     break;
                 case "Total":
                     objMonthlyGoalsCol.Sort(jmann.BusinessObject.MonthlyGoals.ByTotal);
                     break;
                 default:
                     break;
             }

             if (isSortDescending) 
                 objMonthlyGoalsCol.Reverse();

             return objMonthlyGoalsCol;
         } 
 
         /// <summary>
         /// Inserts a record 
         /// </summary> 
         public int Insert() 
         { 
             MonthlyGoals objMonthlyGoals = (MonthlyGoals)this; 
             return MonthlyGoalsDataLayer.Insert(objMonthlyGoals); 
         } 
 
         /// <summary>
         /// Updates a record 
         /// </summary> 
         public void Update() 
         { 
             MonthlyGoals objMonthlyGoals = (MonthlyGoals)this; 
             MonthlyGoalsDataLayer.Update(objMonthlyGoals); 
         } 
 
         /// <summary>
         /// Deletes a record based on primary key(s) 
         /// </summary>
         public static void Delete(int monthlyGoalsID) 
         { 
             MonthlyGoalsDataLayer.Delete(monthlyGoalsID); 
         } 
 
         /// <summary> 
         /// Compares MonthlyGoalsID used for sorting 
         /// </summary> 
         public static Comparison<MonthlyGoals> ByMonthlyGoalsID = delegate(MonthlyGoals x, MonthlyGoals y) 
         { 
             return x.MonthlyGoalsID.CompareTo(y.MonthlyGoalsID); 
         }; 
 
         /// <summary> 
         /// Compares Year used for sorting 
         /// </summary> 
         public static Comparison<MonthlyGoals> ByYear = delegate(MonthlyGoals x, MonthlyGoals y) 
         { 
             return Nullable.Compare(x.Year, y.Year); 
         }; 
 
         /// <summary> 
         /// Compares Month used for sorting 
         /// </summary> 
         public static Comparison<MonthlyGoals> ByMonth = delegate(MonthlyGoals x, MonthlyGoals y) 
         { 
             return Nullable.Compare(x.Month, y.Month); 
         }; 
 
         /// <summary> 
         /// Compares LocationID used for sorting 
         /// </summary> 
         public static Comparison<MonthlyGoals> ByLocationID = delegate(MonthlyGoals x, MonthlyGoals y) 
         { 
             return Nullable.Compare(x.LocationID, y.LocationID); 
         }; 
 
         /// <summary> 
         /// Compares FurnitureTotal used for sorting 
         /// </summary> 
         public static Comparison<MonthlyGoals> ByFurnitureTotal = delegate(MonthlyGoals x, MonthlyGoals y) 
         { 
             return Nullable.Compare(x.FurnitureTotal, y.FurnitureTotal); 
         }; 
 
         /// <summary> 
         /// Compares JewelryTotal used for sorting 
         /// </summary> 
         public static Comparison<MonthlyGoals> ByJewelryTotal = delegate(MonthlyGoals x, MonthlyGoals y) 
         { 
             return Nullable.Compare(x.JewelryTotal, y.JewelryTotal); 
         }; 
 
         /// <summary> 
         /// Compares ElectricalTotal used for sorting 
         /// </summary> 
         public static Comparison<MonthlyGoals> ByElectricalTotal = delegate(MonthlyGoals x, MonthlyGoals y) 
         { 
             return Nullable.Compare(x.ElectricalTotal, y.ElectricalTotal); 
         }; 
 
         /// <summary> 
         /// Compares HangTotal used for sorting 
         /// </summary> 
         public static Comparison<MonthlyGoals> ByHangTotal = delegate(MonthlyGoals x, MonthlyGoals y) 
         { 
             return Nullable.Compare(x.HangTotal, y.HangTotal); 
         }; 
 
         /// <summary> 
         /// Compares BinsTotal used for sorting 
         /// </summary> 
         public static Comparison<MonthlyGoals> ByBinsTotal = delegate(MonthlyGoals x, MonthlyGoals y) 
         { 
             return Nullable.Compare(x.BinsTotal, y.BinsTotal); 
         }; 
 
         /// <summary> 
         /// Compares MiscTotal used for sorting 
         /// </summary> 
         public static Comparison<MonthlyGoals> ByMiscTotal = delegate(MonthlyGoals x, MonthlyGoals y) 
         { 
             return Nullable.Compare(x.MiscTotal, y.MiscTotal); 
         }; 
 
         /// <summary> 
         /// Compares ShoesTotal used for sorting 
         /// </summary> 
         public static Comparison<MonthlyGoals> ByShoesTotal = delegate(MonthlyGoals x, MonthlyGoals y) 
         { 
             return Nullable.Compare(x.ShoesTotal, y.ShoesTotal); 
         }; 
 
         /// <summary> 
         /// Compares BoutiqueTotal used for sorting 
         /// </summary> 
         public static Comparison<MonthlyGoals> ByBoutiqueTotal = delegate(MonthlyGoals x, MonthlyGoals y) 
         { 
             return Nullable.Compare(x.BoutiqueTotal, y.BoutiqueTotal); 
         }; 
 
         /// <summary> 
         /// Compares BooksTotal used for sorting 
         /// </summary> 
         public static Comparison<MonthlyGoals> ByBooksTotal = delegate(MonthlyGoals x, MonthlyGoals y) 
         { 
             return Nullable.Compare(x.BooksTotal, y.BooksTotal); 
         }; 
 
         /// <summary> 
         /// Compares Total used for sorting 
         /// </summary> 
         public static Comparison<MonthlyGoals> ByTotal = delegate(MonthlyGoals x, MonthlyGoals y) 
         { 
             return Nullable.Compare(x.Total, y.Total); 
         }; 
 
     } 
} 


#line default
#line hidden
