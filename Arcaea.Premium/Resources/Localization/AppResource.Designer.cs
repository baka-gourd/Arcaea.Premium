﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Arcaea.Premium.Resources.Localization {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class AppResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AppResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Arcaea.Premium.Resources.Localization.AppResource", typeof(AppResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to About.
        /// </summary>
        internal static string AboutPageName {
            get {
                return ResourceManager.GetString("AboutPageName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select File.
        /// </summary>
        internal static string Import_Btn1 {
            get {
                return ResourceManager.GetString("Import.Btn1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loading....
        /// </summary>
        internal static string Import_Btn1_Loading {
            get {
                return ResourceManager.GetString("Import.Btn1.Loading", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Import from st3 file.
        /// </summary>
        internal static string Import_Desc1 {
            get {
                return ResourceManager.GetString("Import.Desc1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Log.
        /// </summary>
        internal static string Import_Log {
            get {
                return ResourceManager.GetString("Import.Log", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Import.
        /// </summary>
        internal static string ImportPageName {
            get {
                return ResourceManager.GetString("ImportPageName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Home.
        /// </summary>
        internal static string MainPageName {
            get {
                return ResourceManager.GetString("MainPageName", resourceCulture);
            }
        }
    }
}
