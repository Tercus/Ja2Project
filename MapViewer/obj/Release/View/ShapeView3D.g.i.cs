﻿#pragma checksum "..\..\..\View\ShapeView3D.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8D739D6F4BA71945EF550BB1AE6F4B37"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace MapViewer {
    
    
    /// <summary>
    /// ShapeView3D
    /// </summary>
    public partial class ShapeView3D : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 52 "..\..\..\View\ShapeView3D.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Viewport3D vp;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\View\ShapeView3D.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.Model3DGroup mgRoot;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\View\ShapeView3D.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.Model3DGroup mgShape;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\View\ShapeView3D.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.AxisAngleRotation3D anrX;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\View\ShapeView3D.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.AxisAngleRotation3D anrY;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\View\ShapeView3D.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.AxisAngleRotation3D anrZ;
        
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
            System.Uri resourceLocater = new System.Uri("/MapViewer;component/view/shapeview3d.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\ShapeView3D.xaml"
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
            
            #line 8 "..\..\..\View\ShapeView3D.xaml"
            ((MapViewer.ShapeView3D)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Viewport_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.vp = ((System.Windows.Controls.Viewport3D)(target));
            
            #line 53 "..\..\..\View\ShapeView3D.xaml"
            this.vp.MouseMove += new System.Windows.Input.MouseEventHandler(this.Viewport_MouseMove);
            
            #line default
            #line hidden
            
            #line 54 "..\..\..\View\ShapeView3D.xaml"
            this.vp.MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.Viewport_MouseWheel);
            
            #line default
            #line hidden
            
            #line 55 "..\..\..\View\ShapeView3D.xaml"
            this.vp.KeyDown += new System.Windows.Input.KeyEventHandler(this.Viewport_KeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.mgRoot = ((System.Windows.Media.Media3D.Model3DGroup)(target));
            return;
            case 4:
            this.mgShape = ((System.Windows.Media.Media3D.Model3DGroup)(target));
            return;
            case 5:
            this.anrX = ((System.Windows.Media.Media3D.AxisAngleRotation3D)(target));
            return;
            case 6:
            this.anrY = ((System.Windows.Media.Media3D.AxisAngleRotation3D)(target));
            return;
            case 7:
            this.anrZ = ((System.Windows.Media.Media3D.AxisAngleRotation3D)(target));
            return;
            case 8:
            
            #line 92 "..\..\..\View\ShapeView3D.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.TransparencyCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 93 "..\..\..\View\ShapeView3D.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.TransparencyCheckBox_Checked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

