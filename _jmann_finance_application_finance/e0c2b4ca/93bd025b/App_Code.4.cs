#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectBase\CartsBase.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A69F2C7036A5417D6F0C1A26E486BE7907C4906C"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectBase\CartsBase.cs"
using System; 
using System.Data; 
using jmann.DataLayer; 
using jmann.BusinessObject; 
using System.Web.Script.Serialization; 
 
namespace jmann.BusinessObject.Base 
{ 
     /// <summary>
     /// Base class for Carts.  Do not make changes to this class,
     /// instead, put additional code in the Carts class 
     /// </summary>
     public class CartsBase
     { 
         /// <summary> 
         /// Gets or Sets CartsID 
         /// </summary> 
         public int CartsID { get; set; } 
 
         /// <summary> 
         /// Gets or Sets Date 
         /// </summary> 
         public DateTime? Date { get; set; } 
 
         /// <summary> 
         /// Gets or Sets LocationID 
         /// </summary> 
         public int? LocationID { get; set; }

		 /// <summary> 
		 /// Gets or Sets CartsWorkedHard 
		 /// </summary> 
		 public double? CartsWorkedHard { get; set; }

		 /// <summary> 
		 /// Gets or Sets CartsWorkedSoft 
		 /// </summary> 
		 public double? CartsWorkedSoft { get; set; }

		 /// <summary> 
		 /// Gets or Sets CartsWorkedTotal
		 /// </summary> 
		 public double? CartsWorkedTotal { get; set; }

		 /// <summary> 
		 /// Gets or Sets OnHandHard 
		 /// </summary> 
		 public double? OnHandHard { get; set; }

		 /// <summary> 
		 /// Gets or Sets OnHandSoft 
		 /// </summary> 
		 public double? OnHandSoft { get; set; }

		 /// <summary> 
		 /// Gets or Sets OnHandTotal
		 /// </summary> 
		 public double? OnHandTotal { get; set; }

		 /// <summary> 
         /// Gets or Sets HangTotal 
         /// </summary> 
         public int? HangTotal { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ThrownCount 
         /// </summary> 
         public int? ThrownCount { get; set; } 
 
         /// <summary> 
         /// Gets or Sets ThrownLbs 
         /// </summary> 
         public int? ThrownLbs { get; set; } 
 
         /// <summary> 
         /// Gets or Sets RaggedLbs 
         /// </summary> 
         public int? RaggedLbs { get; set; } 
 
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
         public CartsBase() 
         { 
         } 
 
         /// <summary>
         /// Selects a record by primary key(s) 
         /// </summary>
         public static Carts SelectByPrimaryKey(int cartsID) 
         { 
             return CartsDataLayer.SelectByPrimaryKey(cartsID); 
         } 
 
         /// <summary> 
         /// Selects all records as a collection (List) of Carts 
         /// </summary> 
         public static CartsCollection SelectAll() 
         { 
             return CartsDataLayer.SelectAll(); 
         } 
 
         /// <summary> 
         /// Selects all records as a collection (List) of Carts sorted by the sort expression 
         /// </summary> 
         public static CartsCollection SelectAll(string sortExpression) 
         { 
             CartsCollection objCartsCol = CartsDataLayer.SelectAll();
             return SortByExpression(objCartsCol, sortExpression);
         } 
 
         /// <summary>
         /// Selects all Carts by Location, related to column LocationID 
         /// </summary> 
         public static CartsCollection SelectCartsCollectionByLocation(int locationID) 
         { 
             return CartsDataLayer.SelectCartsCollectionByLocation(locationID); 
         } 
 
         /// <summary>
         /// Selects all Carts by Location, related to column LocationID, sorted by the sort expression 
         /// </summary> 
         public static CartsCollection SelectCartsCollectionByLocation(int locationID, string sortExpression) 
         { 
             CartsCollection objCartsCol = CartsDataLayer.SelectCartsCollectionByLocation(locationID); 
             return SortByExpression(objCartsCol, sortExpression);
         } 
 
         /// <summary>
         /// Selects CartsID and Date columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc 
         /// </summary> 
         public static CartsCollection SelectCartsDropDownListData() 
         { 
             return CartsDataLayer.SelectCartsDropDownListData(); 
         } 
 
         /// <summary>
         /// Sorts the CartsCollection by sort expression 
         /// </summary> 
         public static CartsCollection SortByExpression(CartsCollection objCartsCol, string sortExpression) 
         { 
             bool isSortDescending = sortExpression.Contains(" DESC");

             if (isSortDescending)
                 sortExpression = sortExpression.Replace(" DESC", "");

             switch (sortExpression)
             {
                 case "CartsID":
                     objCartsCol.Sort(jmann.BusinessObject.Carts.ByCartsID);
                     break;
                 case "Date":
                     objCartsCol.Sort(jmann.BusinessObject.Carts.ByDate);
                     break;
                 case "LocationID":
                     objCartsCol.Sort(jmann.BusinessObject.Carts.ByLocationID);
                     break;
				 case "CartsWorkedHard":
					 objCartsCol.Sort(jmann.BusinessObject.Carts.ByCartsWorkedHard);
					 break;
				 case "CartsWorkedSoft":
					 objCartsCol.Sort(jmann.BusinessObject.Carts.ByCartsWorkedSoft);
					 break;
				 case "CartsWorkedTotal":
					 objCartsCol.Sort(jmann.BusinessObject.Carts.ByCartsWorkedTotal);
					 break;
				 case "OnHandHard":
					 objCartsCol.Sort(jmann.BusinessObject.Carts.ByOnHandHard);
					 break;
				 case "OnHandSoft":
					 objCartsCol.Sort(jmann.BusinessObject.Carts.ByOnHandSoft);
					 break;
				 case "OnHandTotal":
					 objCartsCol.Sort(jmann.BusinessObject.Carts.ByOnHandTotal);
					 break;
				 case "HangTotal":
                     objCartsCol.Sort(jmann.BusinessObject.Carts.ByHangTotal);
                     break;
                 case "ThrownCount":
                     objCartsCol.Sort(jmann.BusinessObject.Carts.ByThrownCount);
                     break;
                 case "ThrownLbs":
                     objCartsCol.Sort(jmann.BusinessObject.Carts.ByThrownLbs);
                     break;
                 case "RaggedLbs":
                     objCartsCol.Sort(jmann.BusinessObject.Carts.ByRaggedLbs);
                     break;
                 default:
                     break;
             }

             if (isSortDescending) 
                 objCartsCol.Reverse();

             return objCartsCol;
         } 
 
         /// <summary>
         /// Inserts a record 
         /// </summary> 
         public int Insert() 
         { 
             Carts objCarts = (Carts)this; 
             return CartsDataLayer.Insert(objCarts); 
         } 
 
         /// <summary>
         /// Updates a record 
         /// </summary> 
         public void Update() 
         { 
             Carts objCarts = (Carts)this; 
             CartsDataLayer.Update(objCarts); 
         } 
 
         /// <summary>
         /// Deletes a record based on primary key(s) 
         /// </summary>
         public static void Delete(int cartsID) 
         { 
             CartsDataLayer.Delete(cartsID); 
         } 
 
         /// <summary> 
         /// Compares CartsID used for sorting 
         /// </summary> 
         public static Comparison<Carts> ByCartsID = delegate(Carts x, Carts y) 
         { 
             return x.CartsID.CompareTo(y.CartsID); 
         }; 
 
         /// <summary> 
         /// Compares Date used for sorting 
         /// </summary> 
         public static Comparison<Carts> ByDate = delegate(Carts x, Carts y) 
         { 
             return Nullable.Compare(x.Date, y.Date); 
         }; 
 
         /// <summary> 
         /// Compares LocationID used for sorting 
         /// </summary> 
         public static Comparison<Carts> ByLocationID = delegate(Carts x, Carts y) 
         { 
             return Nullable.Compare(x.LocationID, y.LocationID); 
         };

		 /// <summary> 
		 /// Compares CartsWorkedHard used for sorting 
		 /// </summary> 
		 public static Comparison<Carts> ByCartsWorkedHard = delegate(Carts x, Carts y)
		 {
			 return Nullable.Compare(x.CartsWorkedHard, y.CartsWorkedHard);
		 };

		 /// <summary> 
		 /// Compares CartsWorkedSoft used for sorting 
		 /// </summary> 
		 public static Comparison<Carts> ByCartsWorkedSoft = delegate(Carts x, Carts y)
		 {
			 return Nullable.Compare(x.CartsWorkedSoft, y.CartsWorkedSoft);
		 };

		 /// <summary> 
		 /// Compares CartsWorkedTotal used for sorting 
		 /// </summary> 
		 public static Comparison<Carts> ByCartsWorkedTotal = delegate(Carts x, Carts y)
		 {
			 return Nullable.Compare(x.CartsWorkedTotal, y.CartsWorkedTotal);
		 };

		 /// <summary> 
		 /// Compares OnHandHard used for sorting 
		 /// </summary> 
		 public static Comparison<Carts> ByOnHandHard = delegate(Carts x, Carts y)
		 {
			 return Nullable.Compare(x.OnHandHard, y.OnHandHard);
		 };

		 /// <summary> 
		 /// Compares OnHandSoft used for sorting 
		 /// </summary> 
		 public static Comparison<Carts> ByOnHandSoft = delegate(Carts x, Carts y)
		 {
			 return Nullable.Compare(x.OnHandSoft, y.OnHandSoft);
		 };

		 /// <summary> 
		 /// Compares OnHandTotal used for sorting 
		 /// </summary> 
		 public static Comparison<Carts> ByOnHandTotal = delegate(Carts x, Carts y)
		 {
			 return Nullable.Compare(x.OnHandTotal, y.OnHandTotal);
		 };

		 /// <summary> 
         /// Compares HangTotal used for sorting 
         /// </summary> 
         public static Comparison<Carts> ByHangTotal = delegate(Carts x, Carts y) 
         { 
             return Nullable.Compare(x.HangTotal, y.HangTotal); 
         }; 
 
         /// <summary> 
         /// Compares ThrownCount used for sorting 
         /// </summary> 
         public static Comparison<Carts> ByThrownCount = delegate(Carts x, Carts y) 
         { 
             return Nullable.Compare(x.ThrownCount, y.ThrownCount); 
         }; 
 
         /// <summary> 
         /// Compares ThrownLbs used for sorting 
         /// </summary> 
         public static Comparison<Carts> ByThrownLbs = delegate(Carts x, Carts y) 
         { 
             return Nullable.Compare(x.ThrownLbs, y.ThrownLbs); 
         }; 
 
         /// <summary> 
         /// Compares RaggedLbs used for sorting 
         /// </summary> 
         public static Comparison<Carts> ByRaggedLbs = delegate(Carts x, Carts y) 
         { 
             return Nullable.Compare(x.RaggedLbs, y.RaggedLbs); 
         }; 
 
     } 
} 


#line default
#line hidden
