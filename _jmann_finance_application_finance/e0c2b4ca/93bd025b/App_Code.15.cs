#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectCollection\MonthlyGoalsCollection.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "375B41C3DEA2FB255B7E77064166BA934E783383"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\BusinessObjectCollection\MonthlyGoalsCollection.cs"
using System; 
using System.Collections.Generic; 
using jmann.BusinessObject; 
 
namespace jmann.BusinessObject 
{ 
     /// <summary>
     /// This class inherits from the Generic List of MonthlyGoals. 
     /// It's used so that you can iterate through the list or collection of the 
     /// MonthlyGoals class.  You don't need to add any code to this helper class. 
     /// </summary>
     public class MonthlyGoalsCollection : List<MonthlyGoals> 
     { 
         // constructor 
         public MonthlyGoalsCollection() 
         { 
         } 
     } 
} 


#line default
#line hidden
