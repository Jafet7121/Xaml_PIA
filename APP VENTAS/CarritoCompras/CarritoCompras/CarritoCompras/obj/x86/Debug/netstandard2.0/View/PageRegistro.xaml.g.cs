//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("CarritoCompras.View.PageRegistro.xaml", "View/PageRegistro.xaml", typeof(global::CarritoCompras.View.PageRegistro))]

namespace CarritoCompras.View {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("View\\PageRegistro.xaml")]
    public partial class PageRegistro : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.TapGestureRecognizer TapBackArrow;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::CarritoCompras.MyEntry txtNombre;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::CarritoCompras.MyEntry txtApellido;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::CarritoCompras.MyEntry txtDocumento;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::CarritoCompras.MyEntry txtEmail;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label lblError_Email;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::CarritoCompras.MyEntry txtContrasena;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label lblError_Contraseña;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button btnRegistrar;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(PageRegistro));
            TapBackArrow = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.TapGestureRecognizer>(this, "TapBackArrow");
            txtNombre = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::CarritoCompras.MyEntry>(this, "txtNombre");
            txtApellido = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::CarritoCompras.MyEntry>(this, "txtApellido");
            txtDocumento = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::CarritoCompras.MyEntry>(this, "txtDocumento");
            txtEmail = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::CarritoCompras.MyEntry>(this, "txtEmail");
            lblError_Email = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "lblError_Email");
            txtContrasena = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::CarritoCompras.MyEntry>(this, "txtContrasena");
            lblError_Contraseña = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "lblError_Contraseña");
            btnRegistrar = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "btnRegistrar");
        }
    }
}
