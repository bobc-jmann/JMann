#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectBase\LocationBase.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "34218EF124E45AF137042FEE19DC8654709C3CA6"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectBase\LocationBase.cs"
using System; 
using System.Data; 
using jmann.DataLayer; 
using jmann.BusinessObject; 
using System.Web.Script.Serialization; 
 
namespace jmann.BusinessObject.Base 
{ 
     /// <summary>
     /// Base class for Location.  Do not make changes to this class,
     /// instead, put additional code in the Location class 
     /// </summary>
     public class LocationBase
     { 
         /// <summary> 
         /// Gets or Sets LocationID 
         /// </summary> 
         public int LocationID { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Name 
         /// </summary> 
         public string Name { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Description 
         /// </summary> 
         public string Description { get; set; } 
 
         /// <summary> 
         /// Gets or Sets FeetOfRack 
         /// </summary> 
         public int? FeetOfRack { get; set; } 
 
         /// <summary> 
         /// Gets or sets the related Carts(s) by LocationID 
         /// </summary> 
         [ScriptIgnore] 
         public Lazy<CartsCollection> CartsCollection 
         { 
             get 
             { 
                 int value; 
                 bool hasValue = Int32.TryParse(LocationID.ToString(), out value); 
 
                 if (hasValue) 
                     return new Lazy<CartsCollection>(() => jmann.BusinessObject.Carts.SelectCartsCollectionByLocation(value)); 
                 else 
                     return null; 
             } 
             set { } 
         } 
 
         /// <summary> 
         /// Gets or sets the related CreditCardRecord(s) by LocationID 
         /// </summary> 
         [ScriptIgnore] 
         public Lazy<CreditCardRecordCollection> CreditCardRecordCollection 
         { 
             get 
             { 
                 int value; 
                 bool hasValue = Int32.TryParse(LocationID.ToString(), out value); 
 
                 if (hasValue) 
                     return new Lazy<CreditCardRecordCollection>(() => jmann.BusinessObject.CreditCardRecord.SelectCreditCardRecordCollectionByLocation(value)); 
                 else 
                     return null; 
             } 
             set { } 
         } 
 
         /// <summary> 
         /// Gets or sets the related MonthlyGoals(s) by LocationID 
         /// </summary> 
         [ScriptIgnore] 
         public Lazy<MonthlyGoalsCollection> MonthlyGoalsCollection 
         { 
             get 
             { 
                 int value; 
                 bool hasValue = Int32.TryParse(LocationID.ToString(), out value); 
 
                 if (hasValue) 
                     return new Lazy<MonthlyGoalsCollection>(() => jmann.BusinessObject.MonthlyGoals.SelectMonthlyGoalsCollectionByLocation(value)); 
                 else 
                     return null; 
             } 
             set { } 
         } 
 
         /// <summary> 
         /// Gets or sets the related RegisterRecord(s) by LocationID 
         /// </summary> 
         [ScriptIgnore] 
         public Lazy<RegisterRecordCollection> RegisterRecordCollection 
         { 
             get 
             { 
                 int value; 
                 bool hasValue = Int32.TryParse(LocationID.ToString(), out value); 
 
                 if (hasValue) 
                     return new Lazy<RegisterRecordCollection>(() => jmann.BusinessObject.RegisterRecord.SelectRegisterRecordCollectionByLocation(value)); 
                 else 
                     return null; 
             } 
             set { } 
         } 
 
         /// <summary> 
         /// Gets or sets the related ZTapeRecord(s) by LocationID 
         /// </summary> 
         [ScriptIgnore] 
         public Lazy<ZTapeRecordCollection> ZTapeRecordCollection 
         { 
             get 
             { 
                 int value; 
                 bool hasValue = Int32.TryParse(LocationID.ToString(), out value); 
 
                 if (hasValue) 
                     return new Lazy<ZTapeRecordCollection>(() => jmann.BusinessObject.ZTapeRecord.SelectZTapeRecordCollectionByLocation(value)); 
                 else 
                     return null; 
             } 
             set { } 
         } 
 
 
         /// <summary> 
         /// Constructor 
         /// </summary> 
         public LocationBase() 
         { 
         } 
 
         /// <summary>
         /// Selects a record by primary key(s) 
         /// </summary>
         public static Location SelectByPrimaryKey(int locationID) 
         { 
             return LocationDataLayer.SelectByPrimaryKey(locationID); 
         } 
 
         /// <summary> 
         /// Selects all records as a collection (List) of Location 
         /// </summary> 
         public static LocationCollection SelectAll() 
         { 
             return LocationDataLayer.SelectAll(); 
         } 
 
         /// <summary> 
         /// Selects all records as a collection (List) of Location sorted by the sort expression 
         /// </summary> 
         public static LocationCollection SelectAll(string sortExpression) 
         { 
             LocationCollection objLocationCol = LocationDataLayer.SelectAll();
             return SortByExpression(objLocationCol, sortExpression);
         } 
 
         /// <summary>
         /// Selects LocationID and Name columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc 
         /// </summary> 
         public static LocationCollection SelectLocationDropDownListData() 
         { 
             return LocationDataLayer.SelectLocationDropDownListData(); 
         } 
 
         /// <summary>
         /// Sorts the LocationCollection by sort expression 
         /// </summary> 
         public static LocationCollection SortByExpression(LocationCollection objLocationCol, string sortExpression) 
         { 
             bool isSortDescending = sortExpression.Contains(" DESC");

             if (isSortDescending)
                 sortExpression = sortExpression.Replace(" DESC", "");

             switch (sortExpression)
             {
                 case "LocationID":
                     objLocationCol.Sort(jmann.BusinessObject.Location.ByLocationID);
                     break;
                 case "Name":
                     objLocationCol.Sort(jmann.BusinessObject.Location.ByName);
                     break;
                 case "Description":
                     objLocationCol.Sort(jmann.BusinessObject.Location.ByDescription);
                     break;
                 case "FeetOfRack":
                     objLocationCol.Sort(jmann.BusinessObject.Location.ByFeetOfRack);
                     break;
                 default:
                     break;
             }

             if (isSortDescending) 
                 objLocationCol.Reverse();

             return objLocationCol;
         } 
 
         /// <summary>
         /// Inserts a record 
         /// </summary> 
         public int Insert() 
         { 
             Location objLocation = (Location)this; 
             return LocationDataLayer.Insert(objLocation); 
         } 
 
         /// <summary>
         /// Updates a record 
         /// </summary> 
         public void Update() 
         { 
             Location objLocation = (Location)this; 
             LocationDataLayer.Update(objLocation); 
         } 
 
         /// <summary>
         /// Deletes a record based on primary key(s) 
         /// </summary>
         public static void Delete(int locationID) 
         { 
             LocationDataLayer.Delete(locationID); 
         } 
 
         /// <summary> 
         /// Compares LocationID used for sorting 
         /// </summary> 
         public static Comparison<Location> ByLocationID = delegate(Location x, Location y) 
         { 
             return x.LocationID.CompareTo(y.LocationID); 
         }; 
 
         /// <summary> 
         /// Compares Name used for sorting 
         /// </summary> 
         public static Comparison<Location> ByName = delegate(Location x, Location y) 
         { 
             string value1 = x.Name ?? String.Empty; 
             string value2 = y.Name ?? String.Empty; 
             return value1.CompareTo(value2); 
         }; 
 
         /// <summary> 
         /// Compares Description used for sorting 
         /// </summary> 
         public static Comparison<Location> ByDescription = delegate(Location x, Location y) 
         { 
             string value1 = x.Description ?? String.Empty; 
             string value2 = y.Description ?? String.Empty; 
             return value1.CompareTo(value2); 
         }; 
 
         /// <summary> 
         /// Compares FeetOfRack used for sorting 
         /// </summary> 
         public static Comparison<Location> ByFeetOfRack = delegate(Location x, Location y) 
         { 
             return Nullable.Compare(x.FeetOfRack, y.FeetOfRack); 
         }; 
 
     } 
} 


#line default
#line hidden
