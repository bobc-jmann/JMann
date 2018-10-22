#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\Example\EmployeeExample.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "85CD7836841B728F7CE90290F3F9A5E9865EBF85"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\Example\EmployeeExample.cs"
using System; 
using jmann.BusinessObject; 
using System.Web.UI.WebControls; 
// using System.Windows.Forms;    // Note: remove comment when using with windows forms 
 
/// <summary> 
/// These are data-centric code examples for the Employee table. 
/// You can cut and paste the respective codes into your application 
/// by changing the sample values assigned from these examples. 
/// NOTE: This class contains private methods because they're 
/// not meant to be called by an outside client.  Each method contains 
/// code for the respective example being shown 
/// </summary> 
public sealed class EmployeeExample 
{ 
    private EmployeeExample() 
    { 
    } 
 
    private void SelectAll() 
    { 
        // select all records 
        EmployeeCollection objEmployeeCol = Employee.SelectAll(); 
 
        // Example 1:  you can optionally sort the collection in ascending order by your chosen field  
        objEmployeeCol.Sort(Employee.ByDepartment); 
 
        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1  
        objEmployeeCol.Reverse(); 
 
        // Example 3:  directly bind to a GridView 
        GridView grid = new GridView(); 
        grid.DataSource = objEmployeeCol; 
        grid.DataBind(); 
 
        // Example 4:  loop through all the Employee(s) 
        foreach (Employee objEmployee in objEmployeeCol) 
        { 
            int employeeID = objEmployee.EmployeeID; 
            int? department = objEmployee.Department; 
            string name = objEmployee.Name; 
            int? locationID = objEmployee.LocationID; 
            string lastName = objEmployee.LastName; 
            string firstName = objEmployee.FirstName; 
            bool? expired = objEmployee.Expired; 
        } 
    } 
 
    private void SelectByPrimaryKey() 
    { 
        // select a record by primary key(s) 
        Employee objEmployee = Employee.SelectByPrimaryKey(1); 
 
        if (objEmployee != null) 
        { 
            // if record is found, a record is returned 
            int employeeID = objEmployee.EmployeeID; 
            int? department = objEmployee.Department; 
            string name = objEmployee.Name; 
            int? locationID = objEmployee.LocationID; 
            string lastName = objEmployee.LastName; 
            string firstName = objEmployee.FirstName; 
            bool? expired = objEmployee.Expired; 
        } 
    } 
 
    /// <summary> 
    /// Selects EmployeeID and Department columns for use with a with a Drop Down List, Combo Box, Checked Box List, List View, List Box, etc 
    /// </summary> 
    private void SelectEmployeeDropDownListData(int locationID) 
    { 
        EmployeeCollection objEmployeeCol = Employee.SelectEmployeeDropDownListData(locationID); 
 
        // Example 1:  directly bind to a drop down list 
        DropDownList ddl1 = new DropDownList(); 
        ddl1.DataValueField = "EmployeeID"; 
        ddl1.DataTextField = "Name"; 
        ddl1.DataSource = objEmployeeCol; 
        ddl1.DataBind(); 
 
        // Example 2:  add each item through a loop 
        DropDownList ddl2 = new DropDownList(); 
 
        foreach (Employee objEmployee in objEmployeeCol) 
        { 
            ddl2.Items.Add(new ListItem(objEmployee.Name.ToString(), objEmployee.EmployeeID.ToString())); 
        } 
 
        // Example 3:  bind to a combo box.  Note: remove comment when using with windows forms 
        // ComboBox cbx1 = new ComboBox(); 
 
        // foreach (Employee objEmployee in objEmployeeCol) 
        // { 
        //     cbx1.Items.Add(new ListItem(objEmployee.Department.ToString(), objEmployee.EmployeeID.ToString())); 
        // } 
    } 
 
    private void Insert() 
    { 
        // first instantiate a new Employee 
        Employee objEmployee = new Employee(); 
 
        // assign values you want inserted 
        objEmployee.Department = 1; 
        objEmployee.Name = "Peter"; 
        objEmployee.LocationID = 7; 
        objEmployee.LastName = "deVroede"; 
        objEmployee.FirstName = "Peter"; 
        objEmployee.Expired = false; 
 
        // finally, insert a new record 
        // the insert method returns the newly created primary key 
        int newlyCreatedPrimaryKey = objEmployee.Insert(); 
    } 
 
    private void Update() 
    { 
        // first instantiate a new Employee 
        Employee objEmployee = new Employee(); 
 
        // assign the existing primary key(s) 
        // of the record you want updated 
        objEmployee.EmployeeID = 1; 
 
        // assign values you want updated 
        objEmployee.Department = 1; 
        objEmployee.Name = "Peter"; 
        objEmployee.LocationID = 7; 
        objEmployee.LastName = "deVroede"; 
        objEmployee.FirstName = "Peter"; 
        objEmployee.Expired = false; 
 
        // finally, update an existing record 
        objEmployee.Update(); 
    } 
 
    private void Delete() 
    { 
        // delete a record by primary key 
        Employee.Delete(2545); 
    } 
} 


#line default
#line hidden
