﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestAPI.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.5.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://api.businesscentral.dynamics.com/v2.0/d8cf23a2-17d9-41d8-a10d-9aa603abf54" +
            "d/Sandbox/WS/CRONUS%20USA,%20Inc./Codeunit/PurchasejournalpostAPI?tenant=msft1a6" +
            "720t23238948&aid=FIN")]
        public string TestAPI_com_dynamics_businesscentral_api_PurchasejournalpostAPI {
            get {
                return ((string)(this["TestAPI_com_dynamics_businesscentral_api_PurchasejournalpostAPI"]));
            }
        }
    }
}
