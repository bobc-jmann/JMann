#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectBase\CreditCardRecordBase.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0AF65D242574CA104D1593AB0907E537C6B8F817"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectBase\CreditCardRecordBase.cs"
using System; 
using System.Data; 
using jmann.DataLayer; 
using jmann.BusinessObject; 
using System.Web.Script.Serialization; 
 
namespace jmann.BusinessObject.Base 
{ 
     /// <summary>
     /// Base class for CreditCardRecord.  Do not make changes to this class,
     /// instead, put additional code in the CreditCardRecord class 
     /// </summary>
     public class CreditCardRecordBase
     { 
         /// <summary> 
         /// Gets or Sets CreditCardRecordID 
         /// </summary> 
         public int CreditCardRecordID { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Date 
         /// </summary> 
         public DateTime? Date { get; set; } 
 
         /// <summary> 
         /// Gets or Sets LocationID 
         /// </summary> 
         public int? LocationID { get; set; } 
 
         /// <summary> 
         /// Gets or Sets AtmCount 
         /// </summary> 
         public int? AtmCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets AtmTotal 
         /// </summary> 
         public decimal? AtmTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets VisaCount 
         /// </summary> 
         public int? VisaCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets VisaTotal 
         /// </summary> 
         public decimal? VisaTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets MastercardCount 
         /// </summary> 
         public int? MastercardCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets MastercardTotal 
         /// </summary> 
         public decimal? MastercardTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets DiscoverCount 
         /// </summary> 
         public int? DiscoverCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets DiscoverTotal 
         /// </summary> 
         public decimal? DiscoverTotal { get; set; } 
 
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
         public CreditCardRecordBase() 
         { 
         } 
 
         /// <summary>
         /// Selects a record by primary key(s) 
         /// </summary>
         public static CreditCardRecord SelectByPrimaryKey(int creditCardRecordID) 
         { 
             return CreditCardRecordDataLayer.SelectByPrimaryKey(creditCardRecordID); 
         } 
 
         /// <summary> 
         /// Selects all records as a collection (List) of CreditCardRecord 
         /// </summary> 
         public static CreditCardRecordCollection SelectAll() 
         { 
             return CreditCardRecordDataLayer.SelectAll(); 
         } 
 
         /// <summary> 
         /// Selects all records as a collection (List) of CreditCardRecord sorted by the sort expression 
         /// </summary> 
         public static CreditCardRecordCollection SelectAll(string sortExpression) 
         { 
             CreditCardRecordCollection objCreditCardRecordCol = CreditCardRecordDataLayer.SelectAll();
             return SortByExpression(objCreditCardRecordCol, sortExpression);
         } 
 
         /// <summary>
         /// Selects all CreditCardRecord by Location, related to column LocationID 
         /// </summary> 
         public static CreditCardRecordCollection SelectCreditCardRecordCollectionByLocation(int locationID) 
         { 
             return CreditCardRecordDataLayer.SelectCreditCardRecordCollectionByLocation(locationID); 
         } 
 
         /// <summary>
         /// Selects all CreditCardRecord by Location, related to column LocationID, sorted by the sort expression 
         /// </summary> 
         public static CreditCardRecordCollection SelectCreditCardRecordCollectionByLocation(int locationID, string sortExpression) 
         { 
             CreditCardRecordCollection objCreditCardRecordCol = CreditCardRecordDataLayer.SelectCreditCardRecordCollectionByLocation(locationID); 
             return SortByExpression(objCreditCardRecordCol, sortExpression);
         } 
 
         /// <summary>
         /// Selects CreditCardRecordID and Date columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc 
         /// </summary> 
         public static CreditCardRecordCollection SelectCreditCardRecordDropDownListData() 
         { 
             return CreditCardRecordDataLayer.SelectCreditCardRecordDropDownListData(); 
         } 
 
         /// <summary>
         /// Sorts the CreditCardRecordCollection by sort expression 
         /// </summary> 
         public static CreditCardRecordCollection SortByExpression(CreditCardRecordCollection objCreditCardRecordCol, string sortExpression) 
         { 
             bool isSortDescending = sortExpression.Contains(" DESC");

             if (isSortDescending)
                 sortExpression = sortExpression.Replace(" DESC", "");

             switch (sortExpression)
             {
                 case "CreditCardRecordID":
                     objCreditCardRecordCol.Sort(jmann.BusinessObject.CreditCardRecord.ByCreditCardRecordID);
                     break;
                 case "Date":
                     objCreditCardRecordCol.Sort(jmann.BusinessObject.CreditCardRecord.ByDate);
                     break;
                 case "LocationID":
                     objCreditCardRecordCol.Sort(jmann.BusinessObject.CreditCardRecord.ByLocationID);
                     break;
                 case "AtmCount":
                     objCreditCardRecordCol.Sort(jmann.BusinessObject.CreditCardRecord.ByAtmCount);
                     break;
                 case "AtmTotal":
                     objCreditCardRecordCol.Sort(jmann.BusinessObject.CreditCardRecord.ByAtmTotal);
                     break;
                 case "VisaCount":
                     objCreditCardRecordCol.Sort(jmann.BusinessObject.CreditCardRecord.ByVisaCount);
                     break;
                 case "VisaTotal":
                     objCreditCardRecordCol.Sort(jmann.BusinessObject.CreditCardRecord.ByVisaTotal);
                     break;
                 case "MastercardCount":
                     objCreditCardRecordCol.Sort(jmann.BusinessObject.CreditCardRecord.ByMastercardCount);
                     break;
                 case "MastercardTotal":
                     objCreditCardRecordCol.Sort(jmann.BusinessObject.CreditCardRecord.ByMastercardTotal);
                     break;
                 case "DiscoverCount":
                     objCreditCardRecordCol.Sort(jmann.BusinessObject.CreditCardRecord.ByDiscoverCount);
                     break;
                 case "DiscoverTotal":
                     objCreditCardRecordCol.Sort(jmann.BusinessObject.CreditCardRecord.ByDiscoverTotal);
                     break;
                 default:
                     break;
             }

             if (isSortDescending) 
                 objCreditCardRecordCol.Reverse();

             return objCreditCardRecordCol;
         } 
 
         /// <summary>
         /// Inserts a record 
         /// </summary> 
         public int Insert() 
         { 
             CreditCardRecord objCreditCardRecord = (CreditCardRecord)this; 
             return CreditCardRecordDataLayer.Insert(objCreditCardRecord); 
         } 
 
         /// <summary>
         /// Updates a record 
         /// </summary> 
         public void Update() 
         { 
             CreditCardRecord objCreditCardRecord = (CreditCardRecord)this; 
             CreditCardRecordDataLayer.Update(objCreditCardRecord); 
         } 
 
         /// <summary>
         /// Deletes a record based on primary key(s) 
         /// </summary>
         public static void Delete(int creditCardRecordID) 
         { 
             CreditCardRecordDataLayer.Delete(creditCardRecordID); 
         } 
 
         /// <summary> 
         /// Compares CreditCardRecordID used for sorting 
         /// </summary> 
         public static Comparison<CreditCardRecord> ByCreditCardRecordID = delegate(CreditCardRecord x, CreditCardRecord y) 
         { 
             return x.CreditCardRecordID.CompareTo(y.CreditCardRecordID); 
         }; 
 
         /// <summary> 
         /// Compares Date used for sorting 
         /// </summary> 
         public static Comparison<CreditCardRecord> ByDate = delegate(CreditCardRecord x, CreditCardRecord y) 
         { 
             return Nullable.Compare(x.Date, y.Date); 
         }; 
 
         /// <summary> 
         /// Compares LocationID used for sorting 
         /// </summary> 
         public static Comparison<CreditCardRecord> ByLocationID = delegate(CreditCardRecord x, CreditCardRecord y) 
         { 
             return Nullable.Compare(x.LocationID, y.LocationID); 
         }; 
 
         /// <summary> 
         /// Compares AtmCount used for sorting 
         /// </summary> 
         public static Comparison<CreditCardRecord> ByAtmCount = delegate(CreditCardRecord x, CreditCardRecord y) 
         { 
             return Nullable.Compare(x.AtmCount, y.AtmCount); 
         }; 
 
         /// <summary> 
         /// Compares AtmTotal used for sorting 
         /// </summary> 
         public static Comparison<CreditCardRecord> ByAtmTotal = delegate(CreditCardRecord x, CreditCardRecord y) 
         { 
             return Nullable.Compare(x.AtmTotal, y.AtmTotal); 
         }; 
 
         /// <summary> 
         /// Compares VisaCount used for sorting 
         /// </summary> 
         public static Comparison<CreditCardRecord> ByVisaCount = delegate(CreditCardRecord x, CreditCardRecord y) 
         { 
             return Nullable.Compare(x.VisaCount, y.VisaCount); 
         }; 
 
         /// <summary> 
         /// Compares VisaTotal used for sorting 
         /// </summary> 
         public static Comparison<CreditCardRecord> ByVisaTotal = delegate(CreditCardRecord x, CreditCardRecord y) 
         { 
             return Nullable.Compare(x.VisaTotal, y.VisaTotal); 
         }; 
 
         /// <summary> 
         /// Compares MastercardCount used for sorting 
         /// </summary> 
         public static Comparison<CreditCardRecord> ByMastercardCount = delegate(CreditCardRecord x, CreditCardRecord y) 
         { 
             return Nullable.Compare(x.MastercardCount, y.MastercardCount); 
         }; 
 
         /// <summary> 
         /// Compares MastercardTotal used for sorting 
         /// </summary> 
         public static Comparison<CreditCardRecord> ByMastercardTotal = delegate(CreditCardRecord x, CreditCardRecord y) 
         { 
             return Nullable.Compare(x.MastercardTotal, y.MastercardTotal); 
         }; 
 
         /// <summary> 
         /// Compares DiscoverCount used for sorting 
         /// </summary> 
         public static Comparison<CreditCardRecord> ByDiscoverCount = delegate(CreditCardRecord x, CreditCardRecord y) 
         { 
             return Nullable.Compare(x.DiscoverCount, y.DiscoverCount); 
         }; 
 
         /// <summary> 
         /// Compares DiscoverTotal used for sorting 
         /// </summary> 
         public static Comparison<CreditCardRecord> ByDiscoverTotal = delegate(CreditCardRecord x, CreditCardRecord y) 
         { 
             return Nullable.Compare(x.DiscoverTotal, y.DiscoverTotal); 
         }; 
 
     } 
} 


#line default
#line hidden
