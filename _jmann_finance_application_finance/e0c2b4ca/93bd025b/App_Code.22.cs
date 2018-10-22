#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\Example\ZTapeRecordExample.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3975501864FAA16E66EE8B7FB1D283198D47A3C5"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\Example\ZTapeRecordExample.cs"
using System; 
using jmann.BusinessObject; 
using System.Web.UI.WebControls; 
// using System.Windows.Forms;    // Note: remove comment when using with windows forms 
 
/// <summary> 
/// These are data-centric code examples for the ZTapeRecord table. 
/// You can cut and paste the respective codes into your application 
/// by changing the sample values assigned from these examples. 
/// NOTE: This class contains private methods because they're 
/// not meant to be called by an outside client.  Each method contains 
/// code for the respective example being shown 
/// </summary> 
public sealed class ZTapeRecordExample 
{ 
    private ZTapeRecordExample() 
    { 
    } 
 
    private void SelectAll() 
    { 
        // select all records 
        ZTapeRecordCollection objZTapeRecordCol = ZTapeRecord.SelectAll(); 
 
        // Example 1:  you can optionally sort the collection in ascending order by your chosen field  
        objZTapeRecordCol.Sort(ZTapeRecord.ByDate); 
 
        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1  
        objZTapeRecordCol.Reverse(); 
 
        // Example 3:  directly bind to a GridView 
        GridView grid = new GridView(); 
        grid.DataSource = objZTapeRecordCol; 
        grid.DataBind(); 
 
        // Example 4:  loop through all the ZTapeRecord(s) 
        foreach (ZTapeRecord objZTapeRecord in objZTapeRecordCol) 
        { 
            int zTapeRecordID = objZTapeRecord.ZTapeRecordID; 
            DateTime? date = objZTapeRecord.Date; 
            int? locationID = objZTapeRecord.LocationID; 
            int? register = objZTapeRecord.Register; 
            decimal? addAdjustment = objZTapeRecord.AddAdjustment; 
            string addComment = objZTapeRecord.AddComment; 
            decimal? cashAdjustment = objZTapeRecord.CashAdjustment; 
            string cashComment = objZTapeRecord.CashComment; 
            int? discount1StCount = objZTapeRecord.Discount1StCount; 
            decimal? discount1StTotal = objZTapeRecord.Discount1StTotal; 
            int? discount2StCount = objZTapeRecord.Discount2StCount; 
            decimal? discount2StTotal = objZTapeRecord.Discount2StTotal; 
            int? chargeCount = objZTapeRecord.ChargeCount; 
            decimal? chargeTotal = objZTapeRecord.ChargeTotal; 
            int? cashCount = objZTapeRecord.CashCount; 
            decimal? cashTotal = objZTapeRecord.CashTotal; 
            decimal? tax = objZTapeRecord.Tax; 
            int? discount1ItCount = objZTapeRecord.Discount1ItCount; 
            decimal? discount1ItTotal = objZTapeRecord.Discount1ItTotal; 
            int? discount2ItCount = objZTapeRecord.Discount2ItCount; 
            decimal? discount2ItTotal = objZTapeRecord.Discount2ItTotal; 
            int? returnsCount = objZTapeRecord.ReturnsCount; 
            decimal? returnsTotal = objZTapeRecord.ReturnsTotal; 
            decimal? returnsTax = objZTapeRecord.ReturnsTax; 
            int? furnitureCount = objZTapeRecord.FurnitureCount; 
            decimal? furnitureTotal = objZTapeRecord.FurnitureTotal; 
            int? jewelryCount = objZTapeRecord.JewelryCount; 
            decimal? jewelryTotal = objZTapeRecord.JewelryTotal; 
            int? electricalCount = objZTapeRecord.ElectricalCount; 
            decimal? electricalTotal = objZTapeRecord.ElectricalTotal; 
            int? womensCount = objZTapeRecord.WomensCount; 
            decimal? womensTotal = objZTapeRecord.WomensTotal; 
            int? binsCount = objZTapeRecord.BinsCount; 
            decimal? binsTotal = objZTapeRecord.BinsTotal; 
            int? miscCount = objZTapeRecord.MiscCount; 
            decimal? miscTotal = objZTapeRecord.MiscTotal; 
            int? shoesCount = objZTapeRecord.ShoesCount; 
            decimal? shoesTotal = objZTapeRecord.ShoesTotal; 
            int? boutiqueCount = objZTapeRecord.BoutiqueCount; 
            decimal? boutiqueTotal = objZTapeRecord.BoutiqueTotal; 
            int? childsCount = objZTapeRecord.ChildsCount; 
            decimal? childsTotal = objZTapeRecord.ChildsTotal; 
            int? mensCount = objZTapeRecord.MensCount; 
            decimal? mensTotal = objZTapeRecord.MensTotal; 
            int? booksCount = objZTapeRecord.BooksCount; 
            decimal? booksTotal = objZTapeRecord.BooksTotal; 
            int? furnitureCountDisc = objZTapeRecord.FurnitureCountDisc; 
            decimal? furnitureTotalDisc = objZTapeRecord.FurnitureTotalDisc; 
            int? jewelryCountDisc = objZTapeRecord.JewelryCountDisc; 
            decimal? jewelryTotalDisc = objZTapeRecord.JewelryTotalDisc; 
            int? electricalCountDisc = objZTapeRecord.ElectricalCountDisc; 
            decimal? electricalTotalDisc = objZTapeRecord.ElectricalTotalDisc; 
            int? womensCountDisc = objZTapeRecord.WomensCountDisc; 
            decimal? womensTotalDisc = objZTapeRecord.WomensTotalDisc; 
            int? binsCountDisc = objZTapeRecord.BinsCountDisc; 
            decimal? binsTotalDisc = objZTapeRecord.BinsTotalDisc; 
            int? miscCountDisc = objZTapeRecord.MiscCountDisc; 
            decimal? miscTotalDisc = objZTapeRecord.MiscTotalDisc; 
            int? shoesCountDisc = objZTapeRecord.ShoesCountDisc; 
            decimal? shoesTotalDisc = objZTapeRecord.ShoesTotalDisc; 
            int? boutiqueCountDisc = objZTapeRecord.BoutiqueCountDisc; 
            decimal? boutiqueTotalDisc = objZTapeRecord.BoutiqueTotalDisc; 
            int? childsCountDisc = objZTapeRecord.ChildsCountDisc; 
            decimal? childsTotalDisc = objZTapeRecord.ChildsTotalDisc; 
            int? mensCountDisc = objZTapeRecord.MensCountDisc; 
            decimal? mensTotalDisc = objZTapeRecord.MensTotalDisc; 
            int? booksCountDisc = objZTapeRecord.BooksCountDisc; 
            decimal? booksTotalDisc = objZTapeRecord.BooksTotalDisc; 
            int? correctionCount = objZTapeRecord.CorrectionCount; 
            decimal? correctionTotal = objZTapeRecord.CorrectionTotal; 
            int? voidCount = objZTapeRecord.VoidCount; 
            decimal? voidTotal = objZTapeRecord.VoidTotal; 
            int? allVoidCount = objZTapeRecord.AllVoidCount; 
            decimal? allVoidTotal = objZTapeRecord.AllVoidTotal; 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objZTapeRecord.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objZTapeRecord.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objZTapeRecord.Location.Value; 
            } 
        } 
    } 
 
    private void SelectByPrimaryKey() 
    { 
        // select a record by primary key(s) 
        ZTapeRecord objZTapeRecord = ZTapeRecord.SelectByPrimaryKey(13); 
 
        if (objZTapeRecord != null) 
        { 
            // if record is found, a record is returned 
            int zTapeRecordID = objZTapeRecord.ZTapeRecordID; 
            DateTime? date = objZTapeRecord.Date; 
            int? locationID = objZTapeRecord.LocationID; 
            int? register = objZTapeRecord.Register; 
            decimal? addAdjustment = objZTapeRecord.AddAdjustment; 
            string addComment = objZTapeRecord.AddComment; 
            decimal? cashAdjustment = objZTapeRecord.CashAdjustment; 
            string cashComment = objZTapeRecord.CashComment; 
            int? discount1StCount = objZTapeRecord.Discount1StCount; 
            decimal? discount1StTotal = objZTapeRecord.Discount1StTotal; 
            int? discount2StCount = objZTapeRecord.Discount2StCount; 
            decimal? discount2StTotal = objZTapeRecord.Discount2StTotal; 
            int? chargeCount = objZTapeRecord.ChargeCount; 
            decimal? chargeTotal = objZTapeRecord.ChargeTotal; 
            int? cashCount = objZTapeRecord.CashCount; 
            decimal? cashTotal = objZTapeRecord.CashTotal; 
            decimal? tax = objZTapeRecord.Tax; 
            int? discount1ItCount = objZTapeRecord.Discount1ItCount; 
            decimal? discount1ItTotal = objZTapeRecord.Discount1ItTotal; 
            int? discount2ItCount = objZTapeRecord.Discount2ItCount; 
            decimal? discount2ItTotal = objZTapeRecord.Discount2ItTotal; 
            int? returnsCount = objZTapeRecord.ReturnsCount; 
            decimal? returnsTotal = objZTapeRecord.ReturnsTotal; 
            decimal? returnsTax = objZTapeRecord.ReturnsTax; 
            int? furnitureCount = objZTapeRecord.FurnitureCount; 
            decimal? furnitureTotal = objZTapeRecord.FurnitureTotal; 
            int? jewelryCount = objZTapeRecord.JewelryCount; 
            decimal? jewelryTotal = objZTapeRecord.JewelryTotal; 
            int? electricalCount = objZTapeRecord.ElectricalCount; 
            decimal? electricalTotal = objZTapeRecord.ElectricalTotal; 
            int? womensCount = objZTapeRecord.WomensCount; 
            decimal? womensTotal = objZTapeRecord.WomensTotal; 
            int? binsCount = objZTapeRecord.BinsCount; 
            decimal? binsTotal = objZTapeRecord.BinsTotal; 
            int? miscCount = objZTapeRecord.MiscCount; 
            decimal? miscTotal = objZTapeRecord.MiscTotal; 
            int? shoesCount = objZTapeRecord.ShoesCount; 
            decimal? shoesTotal = objZTapeRecord.ShoesTotal; 
            int? boutiqueCount = objZTapeRecord.BoutiqueCount; 
            decimal? boutiqueTotal = objZTapeRecord.BoutiqueTotal; 
            int? childsCount = objZTapeRecord.ChildsCount; 
            decimal? childsTotal = objZTapeRecord.ChildsTotal; 
            int? mensCount = objZTapeRecord.MensCount; 
            decimal? mensTotal = objZTapeRecord.MensTotal; 
            int? booksCount = objZTapeRecord.BooksCount; 
            decimal? booksTotal = objZTapeRecord.BooksTotal; 
            int? furnitureCountDisc = objZTapeRecord.FurnitureCountDisc; 
            decimal? furnitureTotalDisc = objZTapeRecord.FurnitureTotalDisc; 
            int? jewelryCountDisc = objZTapeRecord.JewelryCountDisc; 
            decimal? jewelryTotalDisc = objZTapeRecord.JewelryTotalDisc; 
            int? electricalCountDisc = objZTapeRecord.ElectricalCountDisc; 
            decimal? electricalTotalDisc = objZTapeRecord.ElectricalTotalDisc; 
            int? womensCountDisc = objZTapeRecord.WomensCountDisc; 
            decimal? womensTotalDisc = objZTapeRecord.WomensTotalDisc; 
            int? binsCountDisc = objZTapeRecord.BinsCountDisc; 
            decimal? binsTotalDisc = objZTapeRecord.BinsTotalDisc; 
            int? miscCountDisc = objZTapeRecord.MiscCountDisc; 
            decimal? miscTotalDisc = objZTapeRecord.MiscTotalDisc; 
            int? shoesCountDisc = objZTapeRecord.ShoesCountDisc; 
            decimal? shoesTotalDisc = objZTapeRecord.ShoesTotalDisc; 
            int? boutiqueCountDisc = objZTapeRecord.BoutiqueCountDisc; 
            decimal? boutiqueTotalDisc = objZTapeRecord.BoutiqueTotalDisc; 
            int? childsCountDisc = objZTapeRecord.ChildsCountDisc; 
            decimal? childsTotalDisc = objZTapeRecord.ChildsTotalDisc; 
            int? mensCountDisc = objZTapeRecord.MensCountDisc; 
            decimal? mensTotalDisc = objZTapeRecord.MensTotalDisc; 
            int? booksCountDisc = objZTapeRecord.BooksCountDisc; 
            decimal? booksTotalDisc = objZTapeRecord.BooksTotalDisc; 
            int? correctionCount = objZTapeRecord.CorrectionCount; 
            decimal? correctionTotal = objZTapeRecord.CorrectionTotal; 
            int? voidCount = objZTapeRecord.VoidCount; 
            decimal? voidTotal = objZTapeRecord.VoidTotal; 
            int? allVoidCount = objZTapeRecord.AllVoidCount; 
            decimal? allVoidTotal = objZTapeRecord.AllVoidTotal; 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objZTapeRecord.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objZTapeRecord.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objZTapeRecord.Location.Value; 
            } 
        } 
    } 
 
    /// <summary> 
    /// Select all records by Location, related to column LocationID 
    /// </summary>  
    private void SelectZTapeRecordCollectionByLocation() 
    { 
        ZTapeRecordCollection objZTapeRecordCol = ZTapeRecord.SelectZTapeRecordCollectionByLocation(3); 
 
        // Example 1:  you can optionally sort the collection in ascending order by your chosen field  
        objZTapeRecordCol.Sort(ZTapeRecord.ByDate); 
 
        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1  
        objZTapeRecordCol.Reverse(); 
 
        // Example 3:  directly bind to a GridView 
        GridView grid = new GridView(); 
        grid.DataSource = objZTapeRecordCol; 
        grid.DataBind(); 
 
        // Example 4:  loop through all the ZTapeRecord(s) 
        foreach (ZTapeRecord objZTapeRecord in objZTapeRecordCol) 
        { 
            int zTapeRecordID = objZTapeRecord.ZTapeRecordID; 
            DateTime? date = objZTapeRecord.Date; 
            int? locationID = objZTapeRecord.LocationID; 
            int? register = objZTapeRecord.Register; 
            decimal? addAdjustment = objZTapeRecord.AddAdjustment; 
            string addComment = objZTapeRecord.AddComment; 
            decimal? cashAdjustment = objZTapeRecord.CashAdjustment; 
            string cashComment = objZTapeRecord.CashComment; 
            int? discount1StCount = objZTapeRecord.Discount1StCount; 
            decimal? discount1StTotal = objZTapeRecord.Discount1StTotal; 
            int? discount2StCount = objZTapeRecord.Discount2StCount; 
            decimal? discount2StTotal = objZTapeRecord.Discount2StTotal; 
            int? chargeCount = objZTapeRecord.ChargeCount; 
            decimal? chargeTotal = objZTapeRecord.ChargeTotal; 
            int? cashCount = objZTapeRecord.CashCount; 
            decimal? cashTotal = objZTapeRecord.CashTotal; 
            decimal? tax = objZTapeRecord.Tax; 
            int? discount1ItCount = objZTapeRecord.Discount1ItCount; 
            decimal? discount1ItTotal = objZTapeRecord.Discount1ItTotal; 
            int? discount2ItCount = objZTapeRecord.Discount2ItCount; 
            decimal? discount2ItTotal = objZTapeRecord.Discount2ItTotal; 
            int? returnsCount = objZTapeRecord.ReturnsCount; 
            decimal? returnsTotal = objZTapeRecord.ReturnsTotal; 
            decimal? returnsTax = objZTapeRecord.ReturnsTax; 
            int? furnitureCount = objZTapeRecord.FurnitureCount; 
            decimal? furnitureTotal = objZTapeRecord.FurnitureTotal; 
            int? jewelryCount = objZTapeRecord.JewelryCount; 
            decimal? jewelryTotal = objZTapeRecord.JewelryTotal; 
            int? electricalCount = objZTapeRecord.ElectricalCount; 
            decimal? electricalTotal = objZTapeRecord.ElectricalTotal; 
            int? womensCount = objZTapeRecord.WomensCount; 
            decimal? womensTotal = objZTapeRecord.WomensTotal; 
            int? binsCount = objZTapeRecord.BinsCount; 
            decimal? binsTotal = objZTapeRecord.BinsTotal; 
            int? miscCount = objZTapeRecord.MiscCount; 
            decimal? miscTotal = objZTapeRecord.MiscTotal; 
            int? shoesCount = objZTapeRecord.ShoesCount; 
            decimal? shoesTotal = objZTapeRecord.ShoesTotal; 
            int? boutiqueCount = objZTapeRecord.BoutiqueCount; 
            decimal? boutiqueTotal = objZTapeRecord.BoutiqueTotal; 
            int? childsCount = objZTapeRecord.ChildsCount; 
            decimal? childsTotal = objZTapeRecord.ChildsTotal; 
            int? mensCount = objZTapeRecord.MensCount; 
            decimal? mensTotal = objZTapeRecord.MensTotal; 
            int? booksCount = objZTapeRecord.BooksCount; 
            decimal? booksTotal = objZTapeRecord.BooksTotal; 
            int? furnitureCountDisc = objZTapeRecord.FurnitureCountDisc; 
            decimal? furnitureTotalDisc = objZTapeRecord.FurnitureTotalDisc; 
            int? jewelryCountDisc = objZTapeRecord.JewelryCountDisc; 
            decimal? jewelryTotalDisc = objZTapeRecord.JewelryTotalDisc; 
            int? electricalCountDisc = objZTapeRecord.ElectricalCountDisc; 
            decimal? electricalTotalDisc = objZTapeRecord.ElectricalTotalDisc; 
            int? womensCountDisc = objZTapeRecord.WomensCountDisc; 
            decimal? womensTotalDisc = objZTapeRecord.WomensTotalDisc; 
            int? binsCountDisc = objZTapeRecord.BinsCountDisc; 
            decimal? binsTotalDisc = objZTapeRecord.BinsTotalDisc; 
            int? miscCountDisc = objZTapeRecord.MiscCountDisc; 
            decimal? miscTotalDisc = objZTapeRecord.MiscTotalDisc; 
            int? shoesCountDisc = objZTapeRecord.ShoesCountDisc; 
            decimal? shoesTotalDisc = objZTapeRecord.ShoesTotalDisc; 
            int? boutiqueCountDisc = objZTapeRecord.BoutiqueCountDisc; 
            decimal? boutiqueTotalDisc = objZTapeRecord.BoutiqueTotalDisc; 
            int? childsCountDisc = objZTapeRecord.ChildsCountDisc; 
            decimal? childsTotalDisc = objZTapeRecord.ChildsTotalDisc; 
            int? mensCountDisc = objZTapeRecord.MensCountDisc; 
            decimal? mensTotalDisc = objZTapeRecord.MensTotalDisc; 
            int? booksCountDisc = objZTapeRecord.BooksCountDisc; 
            decimal? booksTotalDisc = objZTapeRecord.BooksTotalDisc; 
            int? correctionCount = objZTapeRecord.CorrectionCount; 
            decimal? correctionTotal = objZTapeRecord.CorrectionTotal; 
            int? voidCount = objZTapeRecord.VoidCount; 
            decimal? voidTotal = objZTapeRecord.VoidTotal; 
            int? allVoidCount = objZTapeRecord.AllVoidCount; 
            decimal? allVoidTotal = objZTapeRecord.AllVoidTotal; 
 
            // optionally get the Location related to LocationID. 
            // Note this is lazily loaded which means there's no value until you ask for it 
            if (objZTapeRecord.LocationID != null) 
            { 
                Location objLocationRelatedToLocationID; 
 
                if (objZTapeRecord.Location.IsValueCreated) 
                    objLocationRelatedToLocationID = objZTapeRecord.Location.Value; 
            } 
        } 
    } 
 
    /// <summary> 
    /// Selects ZTapeRecordID and Date columns for use with a with a Drop Down List, Combo Box, Checked Box List, List View, List Box, etc 
    /// </summary> 
    private void SelectZTapeRecordDropDownListData() 
    { 
        ZTapeRecordCollection objZTapeRecordCol = ZTapeRecord.SelectZTapeRecordDropDownListData(); 
 
        // Example 1:  directly bind to a drop down list 
        DropDownList ddl1 = new DropDownList(); 
        ddl1.DataValueField = "ZTapeRecordID"; 
        ddl1.DataTextField = "Date"; 
        ddl1.DataSource = objZTapeRecordCol; 
        ddl1.DataBind(); 
 
        // Example 2:  add each item through a loop 
        DropDownList ddl2 = new DropDownList(); 
 
        foreach (ZTapeRecord objZTapeRecord in objZTapeRecordCol) 
        { 
            ddl2.Items.Add(new ListItem(objZTapeRecord.Date.ToString(), objZTapeRecord.ZTapeRecordID.ToString())); 
        } 
 
        // Example 3:  bind to a combo box.  Note: remove comment when using with windows forms 
        // ComboBox cbx1 = new ComboBox(); 
 
        // foreach (ZTapeRecord objZTapeRecord in objZTapeRecordCol) 
        // { 
        //     cbx1.Items.Add(new ListItem(objZTapeRecord.Date.ToString(), objZTapeRecord.ZTapeRecordID.ToString())); 
        // } 
    } 
 
    private void Insert() 
    { 
        // first instantiate a new ZTapeRecord 
        ZTapeRecord objZTapeRecord = new ZTapeRecord(); 
 
        // assign values you want inserted 
        objZTapeRecord.Date = DateTime.Now; 
        objZTapeRecord.LocationID = 3; 
        objZTapeRecord.Register = 1; 
        objZTapeRecord.AddAdjustment = Convert.ToDecimal(-18.8800); 
        objZTapeRecord.AddComment = "unrecognized $20.00 in compusafe"; 
        objZTapeRecord.CashAdjustment = Convert.ToDecimal(-0.0100); 
        objZTapeRecord.CashComment = "abc"; 
        objZTapeRecord.Discount1StCount = 0; 
        objZTapeRecord.Discount1StTotal = Convert.ToDecimal(0.0000); 
        objZTapeRecord.Discount2StCount = 12; 
        objZTapeRecord.Discount2StTotal = 52.4m; 
        objZTapeRecord.ChargeCount = 31; 
        objZTapeRecord.ChargeTotal = Convert.ToDecimal(939.1800); 
        objZTapeRecord.CashCount = 139; 
        objZTapeRecord.CashTotal = Convert.ToDecimal(1861.4200); 
        objZTapeRecord.Tax = Convert.ToDecimal(192.4200); 
        objZTapeRecord.Discount1ItCount = 0; 
        objZTapeRecord.Discount1ItTotal = Convert.ToDecimal(0.0000); 
        objZTapeRecord.Discount2ItCount = 12; 
        objZTapeRecord.Discount2ItTotal = 52.4m; 
        objZTapeRecord.ReturnsCount = 3; 
        objZTapeRecord.ReturnsTotal = Convert.ToDecimal(9.9400); 
        objZTapeRecord.ReturnsTax = Convert.ToDecimal(0.0000); 
        objZTapeRecord.FurnitureCount = 1; 
        objZTapeRecord.FurnitureTotal = Convert.ToDecimal(9.9800); 
        objZTapeRecord.JewelryCount = 5; 
        objZTapeRecord.JewelryTotal = Convert.ToDecimal(11.9000); 
        objZTapeRecord.ElectricalCount = 22; 
        objZTapeRecord.ElectricalTotal = Convert.ToDecimal(101.5600); 
        objZTapeRecord.WomensCount = 332; 
        objZTapeRecord.WomensTotal = Convert.ToDecimal(904.3200); 
        objZTapeRecord.BinsCount = 169; 
        objZTapeRecord.BinsTotal = Convert.ToDecimal(429.3800); 
        objZTapeRecord.MiscCount = 160; 
        objZTapeRecord.MiscTotal = Convert.ToDecimal(261.1500); 
        objZTapeRecord.ShoesCount = 45; 
        objZTapeRecord.ShoesTotal = Convert.ToDecimal(128.7500); 
        objZTapeRecord.BoutiqueCount = 0; 
        objZTapeRecord.BoutiqueTotal = Convert.ToDecimal(0.0000); 
        objZTapeRecord.ChildsCount = 67; 
        objZTapeRecord.ChildsTotal = Convert.ToDecimal(145.2200); 
        objZTapeRecord.MensCount = 151; 
        objZTapeRecord.MensTotal = Convert.ToDecimal(511.1500); 
        objZTapeRecord.BooksCount = 78; 
        objZTapeRecord.BooksTotal = Convert.ToDecimal(104.7700); 
        objZTapeRecord.FurnitureCountDisc = 0; 
        objZTapeRecord.FurnitureTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.JewelryCountDisc = 0; 
        objZTapeRecord.JewelryTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.ElectricalCountDisc = 0; 
        objZTapeRecord.ElectricalTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.WomensCountDisc = 0; 
        objZTapeRecord.WomensTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.BinsCountDisc = 0; 
        objZTapeRecord.BinsTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.MiscCountDisc = 0; 
        objZTapeRecord.MiscTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.ShoesCountDisc = 0; 
        objZTapeRecord.ShoesTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.BoutiqueCountDisc = 0; 
        objZTapeRecord.BoutiqueTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.ChildsCountDisc = 0; 
        objZTapeRecord.ChildsTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.MensCountDisc = 0; 
        objZTapeRecord.MensTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.BooksCountDisc = 0; 
        objZTapeRecord.BooksTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.CorrectionCount = 0; 
        objZTapeRecord.CorrectionTotal = Convert.ToDecimal(0.0000); 
        objZTapeRecord.VoidCount = 0; 
        objZTapeRecord.VoidTotal = Convert.ToDecimal(0.0000); 
        objZTapeRecord.AllVoidCount = 0; 
        objZTapeRecord.AllVoidTotal = Convert.ToDecimal(0.0000); 
 
        // finally, insert a new record 
        // the insert method returns the newly created primary key 
        int newlyCreatedPrimaryKey = objZTapeRecord.Insert(); 
    } 
 
    private void Update() 
    { 
        // first instantiate a new ZTapeRecord 
        ZTapeRecord objZTapeRecord = new ZTapeRecord(); 
 
        // assign the existing primary key(s) 
        // of the record you want updated 
        objZTapeRecord.ZTapeRecordID = 13; 
 
        // assign values you want updated 
        objZTapeRecord.Date = DateTime.Now; 
        objZTapeRecord.LocationID = 3; 
        objZTapeRecord.Register = 1; 
        objZTapeRecord.AddAdjustment = Convert.ToDecimal(-18.8800); 
        objZTapeRecord.AddComment = "unrecognized $20.00 in compusafe"; 
        objZTapeRecord.CashAdjustment = Convert.ToDecimal(-0.0100); 
        objZTapeRecord.CashComment = "abc"; 
        objZTapeRecord.Discount1StCount = 0; 
        objZTapeRecord.Discount1StTotal = Convert.ToDecimal(0.0000); 
        objZTapeRecord.Discount2StCount = 12; 
        objZTapeRecord.Discount2StTotal = 52.4m; 
        objZTapeRecord.ChargeCount = 31; 
        objZTapeRecord.ChargeTotal = Convert.ToDecimal(939.1800); 
        objZTapeRecord.CashCount = 139; 
        objZTapeRecord.CashTotal = Convert.ToDecimal(1861.4200); 
        objZTapeRecord.Tax = Convert.ToDecimal(192.4200); 
        objZTapeRecord.Discount1ItCount = 0; 
        objZTapeRecord.Discount1ItTotal = Convert.ToDecimal(0.0000); 
        objZTapeRecord.Discount2ItCount = 12; 
        objZTapeRecord.Discount2ItTotal = 52.4m; 
        objZTapeRecord.ReturnsCount = 3; 
        objZTapeRecord.ReturnsTotal = Convert.ToDecimal(9.9400); 
        objZTapeRecord.ReturnsTax = Convert.ToDecimal(0.0000); 
        objZTapeRecord.FurnitureCount = 1; 
        objZTapeRecord.FurnitureTotal = Convert.ToDecimal(9.9800); 
        objZTapeRecord.JewelryCount = 5; 
        objZTapeRecord.JewelryTotal = Convert.ToDecimal(11.9000); 
        objZTapeRecord.ElectricalCount = 22; 
        objZTapeRecord.ElectricalTotal = Convert.ToDecimal(101.5600); 
        objZTapeRecord.WomensCount = 332; 
        objZTapeRecord.WomensTotal = Convert.ToDecimal(904.3200); 
        objZTapeRecord.BinsCount = 169; 
        objZTapeRecord.BinsTotal = Convert.ToDecimal(429.3800); 
        objZTapeRecord.MiscCount = 160; 
        objZTapeRecord.MiscTotal = Convert.ToDecimal(261.1500); 
        objZTapeRecord.ShoesCount = 45; 
        objZTapeRecord.ShoesTotal = Convert.ToDecimal(128.7500); 
        objZTapeRecord.BoutiqueCount = 0; 
        objZTapeRecord.BoutiqueTotal = Convert.ToDecimal(0.0000); 
        objZTapeRecord.ChildsCount = 67; 
        objZTapeRecord.ChildsTotal = Convert.ToDecimal(145.2200); 
        objZTapeRecord.MensCount = 151; 
        objZTapeRecord.MensTotal = Convert.ToDecimal(511.1500); 
        objZTapeRecord.BooksCount = 78; 
        objZTapeRecord.BooksTotal = Convert.ToDecimal(104.7700); 
        objZTapeRecord.FurnitureCountDisc = 0; 
        objZTapeRecord.FurnitureTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.JewelryCountDisc = 0; 
        objZTapeRecord.JewelryTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.ElectricalCountDisc = 0; 
        objZTapeRecord.ElectricalTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.WomensCountDisc = 0; 
        objZTapeRecord.WomensTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.BinsCountDisc = 0; 
        objZTapeRecord.BinsTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.MiscCountDisc = 0; 
        objZTapeRecord.MiscTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.ShoesCountDisc = 0; 
        objZTapeRecord.ShoesTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.BoutiqueCountDisc = 0; 
        objZTapeRecord.BoutiqueTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.ChildsCountDisc = 0; 
        objZTapeRecord.ChildsTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.MensCountDisc = 0; 
        objZTapeRecord.MensTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.BooksCountDisc = 0; 
        objZTapeRecord.BooksTotalDisc = Convert.ToDecimal(0.0000); 
        objZTapeRecord.CorrectionCount = 0; 
        objZTapeRecord.CorrectionTotal = Convert.ToDecimal(0.0000); 
        objZTapeRecord.VoidCount = 0; 
        objZTapeRecord.VoidTotal = Convert.ToDecimal(0.0000); 
        objZTapeRecord.AllVoidCount = 0; 
        objZTapeRecord.AllVoidTotal = Convert.ToDecimal(0.0000); 
 
        // finally, update an existing record 
        objZTapeRecord.Update(); 
    } 
 
    private void Delete() 
    { 
        // delete a record by primary key 
        ZTapeRecord.Delete(44463); 
    } 
} 


#line default
#line hidden
