#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\Example\MonthlyGoalsExample.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CBDDAFECE4A3EF967B47DEF956AAA139DD6994D9"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\Example\MonthlyGoalsExample.cs"
using System; 
using jmann.BusinessObject; 
using System.Web.UI.WebControls; 
// using System.Windows.Forms;    // Note: remove comment when using with windows forms 
 
/// <summary> 
/// These are data-centric code examples for the MonthlyGoals table. 
/// You can cut and paste the respective codes into your application 
/// by changing the sample values assigned from these examples. 
/// NOTE: This class contains private methods because they're 
/// not meant to be called by an outside client.  Each method contains 
/// code for the respective example being shown 
/// </summary> 
public sealed class MonthlyGoalsExample 
{ 
    private MonthlyGoalsExample() 
    { 
    } 
 
    private void SelectAll() 
    { 
        // select all records 
        MonthlyGoalsCollection objMonthlyGoalsCol = MonthlyGoals.SelectAll(); 
 
        // Example 1:  you can optionally sort the collection in ascending order by your chosen field  
        objMonthlyGoalsCol.Sort(MonthlyGoals.ByYear); 
 
        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1  
        objMonthlyGoalsCol.Reverse(); 
 
        // Example 3:  directly bind to a GridView 
        GridView grid = new GridView(); 
        grid.DataSource = objMonthlyGoalsCol; 
        grid.DataBind(); 
 
        // Example 4:  loop through all the MonthlyGoals(s) 
        foreach (MonthlyGoals objMonthlyGoals in objMonthlyGoalsCol) 
        { 
            int monthlyGoalsID = objMonthlyGoals.MonthlyGoalsID; 
            Int16? year = objMonthlyGoals.Year; 
            Int16? month = objMonthlyGoals.Month; 
            int? locationID = objMonthlyGoals.LocationID; 
            double? furnitureTotal = objMonthlyGoals.FurnitureTotal; 
            double? jewelryTotal = objMonthlyGoals.JewelryTotal; 
            double? electricalTotal = objMonthlyGoals.ElectricalTotal; 
            double? hangTotal = objMonthlyGoals.HangTotal; 
            double? binsTotal = objMonthlyGoals.BinsTotal; 
            double? miscTotal = objMonthlyGoals.MiscTotal; 
            double? shoesTotal = objMonthlyGoals.ShoesTotal; 
            double? boutiqueTotal = objMonthlyGoals.BoutiqueTotal; 
            double? booksTotal = objMonthlyGoals.BooksTotal; 
            decimal? total = objMonthlyGoals.Total; 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objMonthlyGoals.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objMonthlyGoals.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objMonthlyGoals.Location.Value; 
            } 
        } 
    } 
 
    private void SelectByPrimaryKey() 
    { 
        // select a record by primary key(s) 
        MonthlyGoals objMonthlyGoals = MonthlyGoals.SelectByPrimaryKey(1); 
 
        if (objMonthlyGoals != null) 
        { 
            // if record is found, a record is returned 
            int monthlyGoalsID = objMonthlyGoals.MonthlyGoalsID; 
            Int16? year = objMonthlyGoals.Year; 
            Int16? month = objMonthlyGoals.Month; 
            int? locationID = objMonthlyGoals.LocationID; 
            double? furnitureTotal = objMonthlyGoals.FurnitureTotal; 
            double? jewelryTotal = objMonthlyGoals.JewelryTotal; 
            double? electricalTotal = objMonthlyGoals.ElectricalTotal; 
            double? hangTotal = objMonthlyGoals.HangTotal; 
            double? binsTotal = objMonthlyGoals.BinsTotal; 
            double? miscTotal = objMonthlyGoals.MiscTotal; 
            double? shoesTotal = objMonthlyGoals.ShoesTotal; 
            double? boutiqueTotal = objMonthlyGoals.BoutiqueTotal; 
            double? booksTotal = objMonthlyGoals.BooksTotal; 
            decimal? total = objMonthlyGoals.Total; 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objMonthlyGoals.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objMonthlyGoals.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objMonthlyGoals.Location.Value; 
            } 
        } 
    } 
 
    /// <summary> 
    /// Select all records by Location, related to column LocationID 
    /// </summary>  
    private void SelectMonthlyGoalsCollectionByLocation() 
    { 
        MonthlyGoalsCollection objMonthlyGoalsCol = MonthlyGoals.SelectMonthlyGoalsCollectionByLocation(2); 
 
        // Example 1:  you can optionally sort the collection in ascending order by your chosen field  
        objMonthlyGoalsCol.Sort(MonthlyGoals.ByYear); 
 
        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1  
        objMonthlyGoalsCol.Reverse(); 
 
        // Example 3:  directly bind to a GridView 
        GridView grid = new GridView(); 
        grid.DataSource = objMonthlyGoalsCol; 
        grid.DataBind(); 
 
        // Example 4:  loop through all the MonthlyGoals(s) 
        foreach (MonthlyGoals objMonthlyGoals in objMonthlyGoalsCol) 
        { 
            int monthlyGoalsID = objMonthlyGoals.MonthlyGoalsID; 
            Int16? year = objMonthlyGoals.Year; 
            Int16? month = objMonthlyGoals.Month; 
            int? locationID = objMonthlyGoals.LocationID; 
            double? furnitureTotal = objMonthlyGoals.FurnitureTotal; 
            double? jewelryTotal = objMonthlyGoals.JewelryTotal; 
            double? electricalTotal = objMonthlyGoals.ElectricalTotal; 
            double? hangTotal = objMonthlyGoals.HangTotal; 
            double? binsTotal = objMonthlyGoals.BinsTotal; 
            double? miscTotal = objMonthlyGoals.MiscTotal; 
            double? shoesTotal = objMonthlyGoals.ShoesTotal; 
            double? boutiqueTotal = objMonthlyGoals.BoutiqueTotal; 
            double? booksTotal = objMonthlyGoals.BooksTotal; 
            decimal? total = objMonthlyGoals.Total; 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objMonthlyGoals.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objMonthlyGoals.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objMonthlyGoals.Location.Value; 
            } 
        } 
    } 
 
    /// <summary> 
    /// Selects MonthlyGoalsID and Year columns for use with a with a Drop Down List, Combo Box, Checked Box List, List View, List Box, etc 
    /// </summary> 
    private void SelectMonthlyGoalsDropDownListData() 
    { 
        MonthlyGoalsCollection objMonthlyGoalsCol = MonthlyGoals.SelectMonthlyGoalsDropDownListData(); 
 
        // Example 1:  directly bind to a drop down list 
        DropDownList ddl1 = new DropDownList(); 
        ddl1.DataValueField = "MonthlyGoalsID"; 
        ddl1.DataTextField = "Year"; 
        ddl1.DataSource = objMonthlyGoalsCol; 
        ddl1.DataBind(); 
 
        // Example 2:  add each item through a loop 
        DropDownList ddl2 = new DropDownList(); 
 
        foreach (MonthlyGoals objMonthlyGoals in objMonthlyGoalsCol) 
        { 
            ddl2.Items.Add(new ListItem(objMonthlyGoals.Year.ToString(), objMonthlyGoals.MonthlyGoalsID.ToString())); 
        } 
 
        // Example 3:  bind to a combo box.  Note: remove comment when using with windows forms 
        // ComboBox cbx1 = new ComboBox(); 
 
        // foreach (MonthlyGoals objMonthlyGoals in objMonthlyGoalsCol) 
        // { 
        //     cbx1.Items.Add(new ListItem(objMonthlyGoals.Year.ToString(), objMonthlyGoals.MonthlyGoalsID.ToString())); 
        // } 
    } 
 
    private void Insert() 
    { 
        // first instantiate a new MonthlyGoals 
        MonthlyGoals objMonthlyGoals = new MonthlyGoals(); 
 
        // assign values you want inserted 
        objMonthlyGoals.Year = 2003; 
        objMonthlyGoals.Month = 1; 
        objMonthlyGoals.LocationID = 2; 
        objMonthlyGoals.FurnitureTotal = 0.02; 
        objMonthlyGoals.JewelryTotal = 0; 
        objMonthlyGoals.ElectricalTotal = 0.09; 
        objMonthlyGoals.HangTotal = 0.49; 
        objMonthlyGoals.BinsTotal = 0.12; 
        objMonthlyGoals.MiscTotal = 0.13; 
        objMonthlyGoals.ShoesTotal = 0.06; 
        objMonthlyGoals.BoutiqueTotal = 0.07; 
        objMonthlyGoals.BooksTotal = 0.02; 
        objMonthlyGoals.Total = Convert.ToDecimal(247900.0000); 
 
        // finally, insert a new record 
        // the insert method returns the newly created primary key 
        int newlyCreatedPrimaryKey = objMonthlyGoals.Insert(); 
    } 
 
    private void Update() 
    { 
        // first instantiate a new MonthlyGoals 
        MonthlyGoals objMonthlyGoals = new MonthlyGoals(); 
 
        // assign the existing primary key(s) 
        // of the record you want updated 
        objMonthlyGoals.MonthlyGoalsID = 1; 
 
        // assign values you want updated 
        objMonthlyGoals.Year = 2003; 
        objMonthlyGoals.Month = 1; 
        objMonthlyGoals.LocationID = 2; 
        objMonthlyGoals.FurnitureTotal = 0.02; 
        objMonthlyGoals.JewelryTotal = 0; 
        objMonthlyGoals.ElectricalTotal = 0.09; 
        objMonthlyGoals.HangTotal = 0.49; 
        objMonthlyGoals.BinsTotal = 0.12; 
        objMonthlyGoals.MiscTotal = 0.13; 
        objMonthlyGoals.ShoesTotal = 0.06; 
        objMonthlyGoals.BoutiqueTotal = 0.07; 
        objMonthlyGoals.BooksTotal = 0.02; 
        objMonthlyGoals.Total = Convert.ToDecimal(247900.0000); 
 
        // finally, update an existing record 
        objMonthlyGoals.Update(); 
    } 
 
    private void Delete() 
    { 
        // delete a record by primary key 
        MonthlyGoals.Delete(4118); 
    } 
} 


#line default
#line hidden
