#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectCollection\EmployeeCollection.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "59ED4C582AA52805038F1F59AAAB45E5BC8C030B"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectCollection\EmployeeCollection.cs"
using System; 
using System.Collections.Generic; 
using jmann.BusinessObject; 
 
namespace jmann.BusinessObject 
{ 
     /// <summary>
     /// This class inherits from the Generic List of Employee. 
     /// It's used so that you can iterate through the list or collection of the 
     /// Employee class.  You don't need to add any code to this helper class. 
     /// </summary>
     public class EmployeeCollection : List<Employee> 
     { 
         // constructor 
         public EmployeeCollection() 
         { 
         } 
     } 
} 


#line default
#line hidden
