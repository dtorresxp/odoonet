﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OdooNet.Apps.Services.SyncFinanca.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.5.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2020-06-01")]
        public global::System.DateTime LAST_SYNC_ORDER_DATE {
            get {
                return ((global::System.DateTime)(this["LAST_SYNC_ORDER_DATE"]));
            }
            set {
                this["LAST_SYNC_ORDER_DATE"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ODOO")]
        public string FIN5_FJ_PKODUSER {
            get {
                return ((string)(this["FIN5_FJ_PKODUSER"]));
            }
            set {
                this["FIN5_FJ_PKODUSER"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("KPOS")]
        public string FIN5_FJ_PKODFKL {
            get {
                return ((string)(this["FIN5_FJ_PKODFKL"]));
            }
            set {
                this["FIN5_FJ_PKODFKL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("M01")]
        public string FIN5_FJ_KMAG {
            get {
                return ((string)(this["FIN5_FJ_KMAG"]));
            }
            set {
                this["FIN5_FJ_KMAG"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("K")]
        public string FIN5_FJSCR_TIPKLL {
            get {
                return ((string)(this["FIN5_FJSCR_TIPKLL"]));
            }
            set {
                this["FIN5_FJSCR_TIPKLL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("5000")]
        public double SYNC_EXEC_TIME_DELAY {
            get {
                return ((double)(this["SYNC_EXEC_TIME_DELAY"]));
            }
            set {
                this["SYNC_EXEC_TIME_DELAY"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2020-06-01")]
        public global::System.DateTime LAST_SYNC_PRODUCT_DATE {
            get {
                return ((global::System.DateTime)(this["LAST_SYNC_PRODUCT_DATE"]));
            }
            set {
                this["LAST_SYNC_PRODUCT_DATE"] = value;
            }
        }
    }
}
