﻿#pragma checksum "..\..\options.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "70F12056FE9CF243922D7E6CEE6EF0DE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Checkers;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Checkers {
    
    
    /// <summary>
    /// options
    /// </summary>
    public partial class options : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\options.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bntcancel;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\options.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnok;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\options.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbnoPlayers;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\options.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtp1;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\options.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtp2;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\options.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chkMusic;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Checkers;component/options.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\options.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.bntcancel = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\options.xaml"
            this.bntcancel.Click += new System.Windows.RoutedEventHandler(this.bntcancel_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnok = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\options.xaml"
            this.btnok.Click += new System.Windows.RoutedEventHandler(this.btnok_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.cmbnoPlayers = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.txtp1 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtp2 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.chkMusic = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

