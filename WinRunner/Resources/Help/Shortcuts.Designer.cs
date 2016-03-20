﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WinRunner.Resources.Help {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Shortcuts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Shortcuts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WinRunner.Resources.Help.Shortcuts", typeof(Shortcuts).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File.
        /// </summary>
        public static string Heading1_1 {
            get {
                return ResourceManager.GetString("Heading1_1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Folder.
        /// </summary>
        public static string Heading1_2 {
            get {
                return ResourceManager.GetString("Heading1_2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Batch.
        /// </summary>
        public static string Heading1_3 {
            get {
                return ResourceManager.GetString("Heading1_3", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Every shortcut has one thing in common, the run name. This name is what you have to type into the Run command in order to use the shortcut. The name must be unique across all shortcuts and have certain character restrictions..
        /// </summary>
        public static string Paragraph1 {
            get {
                return ResourceManager.GetString("Paragraph1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There are a few types of shortcuts that WinRunner supports. The Registry itself only supports file shortcuts but WinRunner works around that to support additional types. All files that are created by this program are located in the current user&apos;s Documents folder..
        /// </summary>
        public static string Paragraph2 {
            get {
                return ResourceManager.GetString("Paragraph2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This will simply open the file using whatever default program that file type is set to use. This means executables will be executed, text files will be opened with an editor, images will be opened in photo viewers, etc..
        /// </summary>
        public static string Paragraph3 {
            get {
                return ResourceManager.GetString("Paragraph3", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This will open the specified folder. This is done using a Batch file..
        /// </summary>
        public static string Paragraph4 {
            get {
                return ResourceManager.GetString("Paragraph4", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This will execute the Batch script that you enter into the box. This is done using a Batch file..
        /// </summary>
        public static string Paragraph5 {
            get {
                return ResourceManager.GetString("Paragraph5", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Shortcuts.
        /// </summary>
        public static string Title {
            get {
                return ResourceManager.GetString("Title", resourceCulture);
            }
        }
    }
}