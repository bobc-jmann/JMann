﻿#pragma checksum "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "888F223D37E23C40FC1406157852DADFC5FABBB8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



public partial class _Default : System.Web.SessionState.IRequiresSessionState {
    
    
    #line 13 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
    protected global::System.Web.UI.WebControls.Label LblTitle;
    
    #line default
    #line hidden
    
    
    #line 20 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
    protected global::System.Web.UI.WebControls.DropDownList ddlLocation;
    
    #line default
    #line hidden
    
    
    #line 27 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
    protected global::System.Web.UI.WebControls.TextBox TxtDate;
    
    #line default
    #line hidden
    
    
    #line 28 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
    protected global::System.Web.UI.WebControls.CompareValidator CompvDate;
    
    #line default
    #line hidden
    
    
    #line 34 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
    protected global::System.Web.UI.HtmlControls.HtmlGenericControl div1;
    
    #line default
    #line hidden
    
    protected System.Web.Profile.DefaultProfile Profile {
        get {
            return ((System.Web.Profile.DefaultProfile)(this.Context.Profile));
        }
    }
    
    protected System.Web.HttpApplication ApplicationInstance {
        get {
            return ((System.Web.HttpApplication)(this.Context.ApplicationInstance));
        }
    }
}
namespace ASP {
    
    #line 3 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
    using System.Web.UI.WebControls.Expressions;
    
    #line default
    #line hidden
    
    #line 387 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Collections;
    
    #line default
    #line hidden
    
    #line 393 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Text;
    
    #line default
    #line hidden
    
    #line 400 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web.Profile;
    
    #line default
    #line hidden
    
    #line 3 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
    using System.Web.UI;
    
    #line default
    #line hidden
    
    #line 388 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Collections.Generic;
    
    #line default
    #line hidden
    
    #line 392 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Linq;
    
    #line default
    #line hidden
    
    #line 1 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
    using ASP;
    
    #line default
    #line hidden
    
    #line 398 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web.SessionState;
    
    #line default
    #line hidden
    
    #line 391 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Configuration;
    
    #line default
    #line hidden
    
    #line 395 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web;
    
    #line default
    #line hidden
    
    #line 3 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
    using System.Web.DynamicData;
    
    #line default
    #line hidden
    
    #line 396 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web.Caching;
    
    #line default
    #line hidden
    
    #line 3 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
    using System.Web.UI.WebControls;
    
    #line default
    #line hidden
    
    #line 390 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.ComponentModel.DataAnnotations;
    
    #line default
    #line hidden
    
    #line 399 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web.Security;
    
    #line default
    #line hidden
    
    #line 386 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System;
    
    #line default
    #line hidden
    
    #line 3 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
    using System.Web.UI.WebControls.WebParts;
    
    #line default
    #line hidden
    
    #line 394 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Text.RegularExpressions;
    
    #line default
    #line hidden
    
    #line 389 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Collections.Specialized;
    
    #line default
    #line hidden
    
    #line 405 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Xml.Linq;
    
    #line default
    #line hidden
    
    #line 404 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web.UI.HtmlControls;
    
    #line default
    #line hidden
    
    
    [System.Runtime.CompilerServices.CompilerGlobalScopeAttribute()]
    public class pivotregisterrecord_aspx : global::_Default, System.Web.IHttpHandler {
        
        private static bool @__initialized;
        
        private static object @__stringResource;
        
        private static object @__fileDependencies;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public pivotregisterrecord_aspx() {
            string[] dependencies;
            
            #line 912304 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx.cs"
            ((global::System.Web.UI.Page)(this)).AppRelativeVirtualPath = "~/PivotRegisterRecord.aspx";
            
            #line default
            #line hidden
            if ((global::ASP.pivotregisterrecord_aspx.@__initialized == false)) {
                global::ASP.pivotregisterrecord_aspx.@__stringResource = this.ReadStringResource();
                dependencies = new string[6];
                dependencies[0] = "~/PivotRegisterRecord.aspx";
                dependencies[1] = "~/MasterPage.master";
                dependencies[2] = "~/MasterPage.master.cs";
                dependencies[3] = "~/UC/footer.ascx";
                dependencies[4] = "~/UC/footer.ascx.cs";
                dependencies[5] = "~/PivotRegisterRecord.aspx.cs";
                global::ASP.pivotregisterrecord_aspx.@__fileDependencies = this.GetWrappedFileDependencies(dependencies);
                global::ASP.pivotregisterrecord_aspx.@__initialized = true;
            }
            this.Server.ScriptTimeout = 30000000;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControlContent1(System.Web.UI.Control @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 3 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n    <script src=\"Scripts/addrecord-script.js\" type=\"text/javascript\"></script>\r" +
                        "\n    <script type=\"text/javascript\">\r\n        $(function () {\r\n            Initi" +
                        "alizeDatePicker(\"input[id$=TxtDate]\");\r\n        });\r\n     </script>\r\n"));
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.Label @__BuildControlLblTitle() {
            global::System.Web.UI.WebControls.Label @__ctrl;
            
            #line 13 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl = new global::System.Web.UI.WebControls.Label();
            
            #line default
            #line hidden
            this.LblTitle = @__ctrl;
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this);
            
            #line 13 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.ID = "LblTitle";
            
            #line default
            #line hidden
            
            #line 13 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.Text = "Daily Register Web Report";
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.DropDownList @__BuildControlddlLocation() {
            global::System.Web.UI.WebControls.DropDownList @__ctrl;
            
            #line 20 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl = new global::System.Web.UI.WebControls.DropDownList();
            
            #line default
            #line hidden
            this.ddlLocation = @__ctrl;
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this);
            
            #line 20 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.ID = "ddlLocation";
            
            #line default
            #line hidden
            
            #line 20 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.AutoPostBack = true;
            
            #line default
            #line hidden
            
            #line 20 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.SelectedIndexChanged += new System.EventHandler(this.ddlLocation_SelectedIndexChanged);
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.TextBox @__BuildControlTxtDate() {
            global::System.Web.UI.WebControls.TextBox @__ctrl;
            
            #line 27 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl = new global::System.Web.UI.WebControls.TextBox();
            
            #line default
            #line hidden
            this.TxtDate = @__ctrl;
            @__ctrl.TemplateControl = this;
            @__ctrl.SkinID = "TextBoxDate";
            @__ctrl.ApplyStyleSheetSkin(this);
            
            #line 27 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.ID = "TxtDate";
            
            #line default
            #line hidden
            
            #line 27 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("onBlur", "txtDate_TextChanged");
            
            #line default
            #line hidden
            
            #line 27 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.TextChanged += new System.EventHandler(this.txtDate_TextChanged);
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.CompareValidator @__BuildControlCompvDate() {
            global::System.Web.UI.WebControls.CompareValidator @__ctrl;
            
            #line 28 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl = new global::System.Web.UI.WebControls.CompareValidator();
            
            #line default
            #line hidden
            this.CompvDate = @__ctrl;
            @__ctrl.TemplateControl = this;
            @__ctrl.ApplyStyleSheetSkin(this);
            
            #line 28 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.ID = "CompvDate";
            
            #line default
            #line hidden
            
            #line 28 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.ControlToValidate = "TxtDate";
            
            #line default
            #line hidden
            
            #line 28 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.ErrorMessage = "Invalid date!";
            
            #line default
            #line hidden
            
            #line 28 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.Display = global::System.Web.UI.WebControls.ValidatorDisplay.Dynamic;
            
            #line default
            #line hidden
            
            #line 28 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.Operator = global::System.Web.UI.WebControls.ValidationCompareOperator.DataTypeCheck;
            
            #line default
            #line hidden
            
            #line 28 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.Type = global::System.Web.UI.WebControls.ValidationDataType.Date;
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlGenericControl @__BuildControldiv1() {
            global::System.Web.UI.HtmlControls.HtmlGenericControl @__ctrl;
            
            #line 34 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlGenericControl("div");
            
            #line default
            #line hidden
            this.div1 = @__ctrl;
            @__ctrl.TemplateControl = this;
            
            #line 34 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.ID = "div1";
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 34 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n    <p></p>\r\n"));
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControlContent2(System.Web.UI.Control @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl(" \r\n        <h3 id=\"h3AddEditRecord\" class=\"ui-widget-header\">"));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.Label @__ctrl1;
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl1 = this.@__BuildControlLblTitle();
            
            #line default
            #line hidden
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</h3>\r\n<div id=\"controls\">\r\n<table>\r\n   <tr>\r\n      <td class=\"style7\">Location:<" +
                        "/td>\r\n      <td></td>\r\n      <td class=\"style7\">\r\n         "));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.DropDownList @__ctrl2;
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl2 = this.@__BuildControlddlLocation();
            
            #line default
            #line hidden
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__parser.AddParsedSubObject(@__ctrl2);
            
            #line default
            #line hidden
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n      </td>\r\n   <tr>\r\n       <td class=\"style7\">Date:</td>\r\n       <td></td>\r\n\t" +
                        "   <td>"));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.TextBox @__ctrl3;
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl3 = this.@__BuildControlTxtDate();
            
            #line default
            #line hidden
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__parser.AddParsedSubObject(@__ctrl3);
            
            #line default
            #line hidden
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</td>\r\n\t   <td>"));
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.CompareValidator @__ctrl4;
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl4 = this.@__BuildControlCompvDate();
            
            #line default
            #line hidden
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__parser.AddParsedSubObject(@__ctrl4);
            
            #line default
            #line hidden
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("</td>\r\n   </tr>\r\n\r\n   </tr> \r\n</table>\r\n</div>  \r\n   "));
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlGenericControl @__ctrl5;
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl5 = this.@__BuildControldiv1();
            
            #line default
            #line hidden
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__parser.AddParsedSubObject(@__ctrl5);
            
            #line default
            #line hidden
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n    <br />\r\n"));
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControlTree(pivotregisterrecord_aspx @__ctrl) {
            
            #line 1 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.MasterPageFile = "~/MasterPage.master";
            
            #line default
            #line hidden
            
            #line 1 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__ctrl.Theme = "Theme1";
            
            #line default
            #line hidden
            
            #line 1 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            this.InitializeCulture();
            
            #line default
            #line hidden
            
            #line 3 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            this.AddContentTemplate("head", new System.Web.UI.CompiledTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControlContent1)));
            
            #line default
            #line hidden
            
            #line 12 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            this.AddContentTemplate("ContentPlaceHolder1", new System.Web.UI.CompiledTemplateBuilder(new System.Web.UI.BuildTemplateMethod(this.@__BuildControlContent2)));
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 1 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n\r\n"));
            
            #line default
            #line hidden
            
            #line 1 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n\r\n"));
            
            #line default
            #line hidden
        }
        
        
        #line 912304 "D:\JMann\Finance\Application\Finance\PivotRegisterRecord.aspx.cs"
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void FrameworkInitialize() {
            base.FrameworkInitialize();
            this.SetStringResourcePointer(global::ASP.pivotregisterrecord_aspx.@__stringResource, 0);
            this.@__BuildControlTree(this);
            this.AddWrappedFileDependencies(global::ASP.pivotregisterrecord_aspx.@__fileDependencies);
            this.Request.ValidateInput();
        }
        
        #line default
        #line hidden
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override int GetTypeHashCode() {
            return 2015361368;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override void ProcessRequest(System.Web.HttpContext context) {
            base.ProcessRequest(context);
        }
    }
}
