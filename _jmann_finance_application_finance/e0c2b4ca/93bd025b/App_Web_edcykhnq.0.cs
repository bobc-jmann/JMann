﻿#pragma checksum "D:\JMann\Finance\Application\Finance\MasterPage.master" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9D214A877653433C7AE693A630D42F6BD87C709D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace jmann {
    
    
    public partial class MasterPage {
        
        
        #line 11 "D:\JMann\Finance\Application\Finance\MasterPage.master"
        protected global::System.Web.UI.WebControls.ContentPlaceHolder head;
        
        #line default
        #line hidden
        
        
        #line 113 "D:\JMann\Finance\Application\Finance\MasterPage.master"
        protected global::System.Web.UI.WebControls.ContentPlaceHolder ContentPlaceHolder1;
        
        #line default
        #line hidden
        
        
        #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
        protected global::System.Web.UI.HtmlControls.HtmlForm MasterPageForm1;
        
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
}
namespace ASP {
    
    #line 11 "D:\JMann\Finance\Application\Finance\MasterPage.master"
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
    
    #line 11 "D:\JMann\Finance\Application\Finance\MasterPage.master"
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
    
    #line 3 "D:\JMann\Finance\Application\Finance\MasterPage.master"
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
    
    #line 11 "D:\JMann\Finance\Application\Finance\MasterPage.master"
    using System.Web.DynamicData;
    
    #line default
    #line hidden
    
    #line 396 "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Config\web.config"
    using System.Web.Caching;
    
    #line default
    #line hidden
    
    #line 11 "D:\JMann\Finance\Application\Finance\MasterPage.master"
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
    
    #line 11 "D:\JMann\Finance\Application\Finance\MasterPage.master"
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
    public class masterpage_master : global::jmann.MasterPage {
        
        private System.Web.UI.ITemplate @__Template_head;
        
        private System.Web.UI.ITemplate @__Template_ContentPlaceHolder1;
        
        private static bool @__initialized;
        
        private static object @__stringResource;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public masterpage_master() {
            
            #line 912304 "D:\JMann\Finance\Application\Finance\MasterPage.master.cs"
            ((global::System.Web.UI.MasterPage)(this)).AppRelativeVirtualPath = "~/MasterPage.master";
            
            #line default
            #line hidden
            if ((global::ASP.masterpage_master.@__initialized == false)) {
                global::ASP.masterpage_master.@__stringResource = this.ReadStringResource();
                global::ASP.masterpage_master.@__initialized = true;
            }
            this.ContentPlaceHolders.Add("head");
            this.ContentPlaceHolders.Add("contentplaceholder1");
        }
        
        [TemplateContainer(typeof(System.Web.UI.MasterPage))]
        [TemplateInstanceAttribute(System.Web.UI.TemplateInstance.Single)]
        public virtual System.Web.UI.ITemplate Template_head {
            get {
                return this.@__Template_head;
            }
            set {
                this.@__Template_head = value;
            }
        }
        
        [TemplateContainer(typeof(System.Web.UI.MasterPage))]
        [TemplateInstanceAttribute(System.Web.UI.TemplateInstance.Single)]
        public virtual System.Web.UI.ITemplate Template_ContentPlaceHolder1 {
            get {
                return this.@__Template_ContentPlaceHolder1;
            }
            set {
                this.@__Template_ContentPlaceHolder1 = value;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlTitle @__BuildControl__control3() {
            global::System.Web.UI.HtmlControls.HtmlTitle @__ctrl;
            
            #line 9 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlTitle();
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlLink @__BuildControl__control4() {
            global::System.Web.UI.HtmlControls.HtmlLink @__ctrl;
            
            #line 10 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlLink();
            
            #line default
            #line hidden
            
            #line 10 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("rel", "stylesheet");
            
            #line default
            #line hidden
            
            #line 10 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("type", "text/css");
            
            #line default
            #line hidden
            
            #line 10 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl.Href = "Styles/global.css";
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.ContentPlaceHolder @__BuildControlhead() {
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl;
            
            #line 11 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl = new global::System.Web.UI.WebControls.ContentPlaceHolder();
            
            #line default
            #line hidden
            this.head = @__ctrl;
            
            #line 11 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl.ID = "head";
            
            #line default
            #line hidden
            if ((this.ContentTemplates != null)) {
                this.@__Template_head = ((System.Web.UI.ITemplate)(this.ContentTemplates["head"]));
            }
            if ((this.@__Template_head != null)) {
                this.InstantiateInContentPlaceHolder(@__ctrl, this.@__Template_head);
            }
            else {
                System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
                
                #line 11 "D:\JMann\Finance\Application\Finance\MasterPage.master"
                @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n    "));
                
                #line default
                #line hidden
            }
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlHead @__BuildControl__control2() {
            global::System.Web.UI.HtmlControls.HtmlHead @__ctrl;
            
            #line 8 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlHead("head");
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlTitle @__ctrl1;
            
            #line 8 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl1 = this.@__BuildControl__control3();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 8 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlLink @__ctrl2;
            
            #line 8 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl2 = this.@__BuildControl__control4();
            
            #line default
            #line hidden
            
            #line 8 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl2);
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl3;
            
            #line 8 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl3 = this.@__BuildControlhead();
            
            #line default
            #line hidden
            
            #line 8 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl3);
            
            #line default
            #line hidden
            
            #line 8 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n    <style type=\"text/css\">\r\n        .style1\r\n        {\r\n            width: 768" +
                        "px;\r\n        }\r\n    </style>\r\n"));
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControl__control5() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            
            #line 87 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            
            #line default
            #line hidden
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            
            #line 87 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl.OnClientClick = "aspnetForm.target =\'_blank\';";
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 87 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("Daily Register by Location Report"));
            
            #line default
            #line hidden
            
            #line 87 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl.Click += new System.EventHandler(this.RunDailyRegisterByLocation_Click);
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControl__control6() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            
            #line 88 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            
            #line default
            #line hidden
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            
            #line 88 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl.OnClientClick = "aspnetForm.target =\'_blank\';";
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 88 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("Cashier Report"));
            
            #line default
            #line hidden
            
            #line 88 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl.Click += new System.EventHandler(this.RunCashierReport_Click);
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControl__control7() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            
            #line 89 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            
            #line default
            #line hidden
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            
            #line 89 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl.OnClientClick = "aspnetForm.target =\'_blank\';";
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 89 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("Finance Summary"));
            
            #line default
            #line hidden
            
            #line 89 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl.Click += new System.EventHandler(this.RunFinanceSummary_Click);
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControl__control8() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            
            #line 90 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            
            #line default
            #line hidden
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            
            #line 90 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl.OnClientClick = "aspnetForm.target =\'_blank\';";
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 90 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("Weekly Sales"));
            
            #line default
            #line hidden
            
            #line 90 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl.Click += new System.EventHandler(this.RunWeeklySalesComparison_Click);
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.LinkButton @__BuildControl__control9() {
            global::System.Web.UI.WebControls.LinkButton @__ctrl;
            
            #line 91 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl = new global::System.Web.UI.WebControls.LinkButton();
            
            #line default
            #line hidden
            @__ctrl.ApplyStyleSheetSkin(this.Page);
            
            #line 91 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl.OnClientClick = "aspnetForm.target =\'_blank\';";
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 91 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("Cart Report"));
            
            #line default
            #line hidden
            
            #line 91 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl.Click += new System.EventHandler(this.RunCartReport_Click);
            
            #line default
            #line hidden
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.WebControls.ContentPlaceHolder @__BuildControlContentPlaceHolder1() {
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl;
            
            #line 113 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl = new global::System.Web.UI.WebControls.ContentPlaceHolder();
            
            #line default
            #line hidden
            this.ContentPlaceHolder1 = @__ctrl;
            
            #line 113 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl.ID = "ContentPlaceHolder1";
            
            #line default
            #line hidden
            if ((this.ContentTemplates != null)) {
                this.@__Template_ContentPlaceHolder1 = ((System.Web.UI.ITemplate)(this.ContentTemplates["ContentPlaceHolder1"]));
            }
            if ((this.@__Template_ContentPlaceHolder1 != null)) {
                this.InstantiateInContentPlaceHolder(@__ctrl, this.@__Template_ContentPlaceHolder1);
            }
            else {
                System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
                
                #line 113 "D:\JMann\Finance\Application\Finance\MasterPage.master"
                @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n\r\n        "));
                
                #line default
                #line hidden
            }
            return @__ctrl;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::System.Web.UI.HtmlControls.HtmlForm @__BuildControlMasterPageForm1() {
            global::System.Web.UI.HtmlControls.HtmlForm @__ctrl;
            
            #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl = new global::System.Web.UI.HtmlControls.HtmlForm();
            
            #line default
            #line hidden
            this.MasterPageForm1 = @__ctrl;
            
            #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl.ID = "MasterPageForm1";
            
            #line default
            #line hidden
            
            #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            ((System.Web.UI.IAttributeAccessor)(@__ctrl)).SetAttribute("class", "cmxform");
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.LinkButton @__ctrl1;
            
            #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl1 = this.@__BuildControl__control5();
            
            #line default
            #line hidden
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.LinkButton @__ctrl2;
            
            #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl2 = this.@__BuildControl__control6();
            
            #line default
            #line hidden
            
            #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl2);
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.LinkButton @__ctrl3;
            
            #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl3 = this.@__BuildControl__control7();
            
            #line default
            #line hidden
            
            #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl3);
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.LinkButton @__ctrl4;
            
            #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl4 = this.@__BuildControl__control8();
            
            #line default
            #line hidden
            
            #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl4);
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.LinkButton @__ctrl5;
            
            #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl5 = this.@__BuildControl__control9();
            
            #line default
            #line hidden
            
            #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl5);
            
            #line default
            #line hidden
            global::System.Web.UI.WebControls.ContentPlaceHolder @__ctrl6;
            
            #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl6 = this.@__BuildControlContentPlaceHolder1();
            
            #line default
            #line hidden
            
            #line 22 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl6);
            
            #line default
            #line hidden
            @__ctrl.SetRenderMethodDelegate(new System.Web.UI.RenderMethod(this.@__RenderMasterPageForm1));
            return @__ctrl;
        }
        
        private void @__RenderMasterPageForm1(System.Web.UI.HtmlTextWriter @__w, System.Web.UI.Control parameterContainer) {
            this.WriteUTF8ResourceString(@__w, 0, 572, true);
            
            #line 42 "D:\JMann\Finance\Application\Finance\MasterPage.master"
                 
                    if (connectionString.IndexOf("_Training") > 0) {
                        HttpContext.Current.Response.Write("<tr><th>TRAINING</th></tr><tr /><tr />");
                    }
               
            
            #line default
            #line hidden
            this.WriteUTF8ResourceString(@__w, 572, 1557, true);
            parameterContainer.Controls[0].RenderControl(@__w);
            @__w.Write("</li>\r\n                   <li>");
            parameterContainer.Controls[1].RenderControl(@__w);
            @__w.Write("</li>\r\n                   <li>");
            parameterContainer.Controls[2].RenderControl(@__w);
            @__w.Write("</li>\r\n                   <li>");
            parameterContainer.Controls[3].RenderControl(@__w);
            @__w.Write("</li>\r\n                   <li>");
            parameterContainer.Controls[4].RenderControl(@__w);
            this.WriteUTF8ResourceString(@__w, 2129, 870, true);
            parameterContainer.Controls[5].RenderControl(@__w);
            @__w.Write("\r\n       </div>\r\n      </td>\r\n\r\n     </tr>\r\n    </table>\r\n  <footer>\r\n   <div cla" +
                    "ss=\"content-wrapper\">\r\n    <div class=\"float-left\">\r\n     <p>&copy; ");
            
            #line 124 "D:\JMann\Finance\Application\Finance\MasterPage.master"
       @__w.Write( DateTime.Now.ToString("d") );

            
            #line default
            #line hidden
            @__w.Write(" - JMann Data Entry Application</p>\r\n    </div>\r\n   </div>\r\n  </footer>\r\n    ");
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void @__BuildControlTree(masterpage_master @__ctrl) {
            System.Web.UI.IParserAccessor @__parser = ((System.Web.UI.IParserAccessor)(@__ctrl));
            
            #line 1 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n\r\n<!DOCTYPE html>\r\n\r\n<html>\r\n"));
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlHead @__ctrl1;
            
            #line 1 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl1 = this.@__BuildControl__control2();
            
            #line default
            #line hidden
            
            #line 1 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl1);
            
            #line default
            #line hidden
            
            #line 1 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n\r\n<body>\r\n    "));
            
            #line default
            #line hidden
            global::System.Web.UI.HtmlControls.HtmlForm @__ctrl2;
            
            #line 1 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__ctrl2 = this.@__BuildControlMasterPageForm1();
            
            #line default
            #line hidden
            
            #line 1 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(@__ctrl2);
            
            #line default
            #line hidden
            
            #line 1 "D:\JMann\Finance\Application\Finance\MasterPage.master"
            @__parser.AddParsedSubObject(new System.Web.UI.LiteralControl("\r\n</body>\r\n</html>\r\n"));
            
            #line default
            #line hidden
        }
        
        
        #line 912304 "D:\JMann\Finance\Application\Finance\MasterPage.master.cs"
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void FrameworkInitialize() {
            base.FrameworkInitialize();
            this.SetStringResourcePointer(global::ASP.masterpage_master.@__stringResource, 0);
            this.@__BuildControlTree(this);
        }
        
        #line default
        #line hidden
    }
}
