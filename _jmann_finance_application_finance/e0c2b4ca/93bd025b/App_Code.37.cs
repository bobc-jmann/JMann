#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\DAL.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "343125AEB3D26650077BCF23E9167A2B4E286355"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\DataLayerBase\DAL.cs"
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

public class DAL
{
	private Globals g = new Globals();
	//private Utils.DB db = new Utils.DB(Globals.CN);
	private Utils.DB db;
	private string qstr = "";

	public DAL() {
		db = new Utils.DB(g.CN);
	} // constructor

	public DataSet EmployeesAll
	{
		get {return employees_get_all();}
	}
	public DataTableReader LocationsAll
	{
		get {return locations_get_all();}
	}


	private DataSet employees_get_all()
	{
		qstr = "usp_employees_all";
		return db.GetData(qstr);
	}
	private DataTableReader locations_get_all()
	{
		qstr = "usp_locations_all";
		return db.GetDataTableReader(qstr);
	}
}

#line default
#line hidden
