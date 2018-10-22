#pragma checksum "D:\JMann\Finance\Application\Finance\App_Code\Helper\Globals.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B5220A424C38539D6055A4CC9E36428607688169"

#line 1 "D:\JMann\Finance\Application\Finance\App_Code\Helper\Globals.cs"
using System;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;

public class Globals
{
	//public const string CN = @"Server=JMANN-SQL;Database=Finance;User ID=Finance_User;Password=!Finance5678$";
	public string CN = "";
	
  public Globals()
  {
	System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~/");
	CN = rootWebConfig.ConnectionStrings.ConnectionStrings["App-MainConnectionString"].ConnectionString;
  }

  public static bool isInt(string txt)
  {
      bool isnumeric = false;
      try
      {
        if (string.IsNullOrEmpty(txt))
        {
          isnumeric = false;
        }
        else
        {
          decimal a = Convert.ToInt32(txt);
          isnumeric = true;
        }
      }
      catch
      {
          isnumeric = false;
      }

      return isnumeric;
  }
  public static bool isNumeric(string anyString)
  {
      if (anyString == null)
      {
          anyString = "";
      }
      if (anyString.Length > 0)
      {
          double dummyOut = new double();
          System.Globalization.CultureInfo cultureInfo =
              new System.Globalization.CultureInfo("en-US", true);

          return Double.TryParse(anyString, System.Globalization.NumberStyles.Any,
              cultureInfo.NumberFormat, out dummyOut);
      }
      else
      {
          return false;
      }
  }
  public static string isNull(Object obj, string defaultvalue)
  {
      return obj == null ? defaultvalue.Trim() : obj.ToString().Trim();
  }
  public static bool IsDate(string sdate)
  {
      DateTime dt;
      bool isdate = true;
      try
      {
          dt = DateTime.Parse(sdate);
      }
      catch
      {
          isdate = false;
      }
      return isdate;
  }
  public static string nbsp(string str)
  {
      return (str == "" || str == null) ? "&nbsp;" : str;
  }
  public static string JSAlert(string msg)
  {
      StringBuilder sb = new StringBuilder("<script> alert('");
      sb.Append(msg);
      sb.Append("');</script>");
      return sb.ToString();
  }
  public static string JS(string script)
  {
      StringBuilder sb = new StringBuilder();
      sb.Append("<script>");
      sb.Append(script);
      sb.Append("</script>");
      return sb.ToString();
  }
  public static string GetTickCount()
  {
      return DateTime.Now.Ticks.ToString();
  }
  public static int Count(string str, char find)
  {
      int ret=0;
      foreach(char s in str)
      {
          if (s==find)
          {
              ++ret;
          }
      }
      return ret;
  }
  public static string EnSQL(string str)
  {
      return str.Replace("'", "''");
  }
  public static int Asc(string str)
  {
      return str.Substring(0, 1).ToCharArray()[0];
  }
  public static string Chr(int num)//Overloaded
  {
      return Convert.ToChar(num).ToString();
  }
  public static string Chr(string num)//Overloaded
  {
      return Convert.ToChar(Convert.ToInt16(num)).ToString();
  }
  public static string Now()
  {
      return DateTime.Now.ToString("MM/dd/yyyy");
  }
  public static string MonthName(int month)
  {
      return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month).Substring(0,3).ToUpper();
  }
	public static string GetSHA1(string txt)
	{
		byte[] HashValue, MessageBytes = Encoding.ASCII.GetBytes(txt);
		SHA1Managed SHhash = new SHA1Managed();
		HashValue = SHhash.ComputeHash(MessageBytes);
		return BitConverter.ToString(HashValue).Replace("-", "");
	}
}


#line default
#line hidden
