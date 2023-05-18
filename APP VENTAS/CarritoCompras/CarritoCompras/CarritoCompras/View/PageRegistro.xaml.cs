using AppNotas.Modelo;
using CarritoCompras.Modelo;
using CarritoCompras.Service;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarritoCompras.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageRegistro : ContentPage
	{
		public PageRegistro ()
		{
			InitializeComponent ();
		}
        private async void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNombre.Text) || String.IsNullOrEmpty(txtApellido.Text) || String.IsNullOrEmpty(txtDocumento.Text)  || String.IsNullOrEmpty(txtEmail.Text) || String.IsNullOrEmpty(txtContrasena.Text))
            {
                await DisplayAlert("Oops", "Ingrese todos los datos", "Aceptar");
                return;
            }
            else
            {
                string Phone = txtDocumento.Text;

                string Email = txtEmail.Text;
                string patron_E = @"^[\w\.-]+@[\w\.-]+\.\w+$";
                bool esValido_E = System.Text.RegularExpressions.Regex.IsMatch(Email, patron_E);

                string password = txtContrasena.Text;
                string patron_P = "^[a-zA-Z0-9]+$";
                bool esValido_P = System.Text.RegularExpressions.Regex.IsMatch(password, patron_P);

                if (Phone.Length < 10 || esValido_E == false || password.Length < 6 || esValido_P == false)
                {
                    await DisplayAlert("Oops", "Verifique los datos incorrectos.", "Aceptar");

                    if (Phone.Length < 10)
                    {
                        lblError_Documento.IsVisible = true;
                        lblError_Documento.Text = "Faltan caracteres.";
                    }

                    if (esValido_E == false)
                    {
                        lblError_Email.IsVisible = true;
                        lblError_Email.Text = "Formato de correo incorrecto";
                    }

                    if (password.Length < 6)
                    {
                        lblError_Contraseña.IsVisible = true;
                        lblError_Contraseña.Text = "La contraseña no puede tener menos de 6 caracteres.";

                    }
                    if (esValido_P == false)
                    {
                        lblError_Contraseña.IsVisible = true;
                        lblError_Contraseña.Text = "Solo caracteres alfabeticos y numericos.";
                    }
                }
                else
                {
                    Usuario oUsuario = new Usuario()
                    {
                        Nombres = txtNombre.Text,
                        Apellidos = txtApellido.Text,
                        Documento = txtDocumento.Text,
                        Email = txtEmail.Text,
                        Clave = txtContrasena.Text
                    };

                    bool respuesta = await ApiServiceAuthentication.RegistrarUsuario(oUsuario);

                    if (respuesta)
                    {
                        await DisplayAlert("Correcto", "Usuario registrado", "Aceptar");
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        await DisplayAlert("Oops", "No se pudo registrar", "Aceptar");
                    }

                }
            }
        }
        private void TapBackArrow_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var isValid = Regex.IsMatch(e.NewTextValue, "^[a-zA-Z]+$");

            if (e.NewTextValue.Length > 0)
            {
                ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }
        private void TxtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Email = txtEmail.Text;
            string patron_E = @"^[\w\.-]+@[\w\.-]+\.\w+$";
            bool esValido_E = System.Text.RegularExpressions.Regex.IsMatch(Email, patron_E);

            if (esValido_E)
            {
                lblError_Email.Text = "";
                lblError_Email.IsVisible = false;
            }
        }
        private void TxtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Phone = txtDocumento.Text;

            if(Phone.Length == 10)
            {
                lblError_Documento.Text = "";
                lblError_Documento.IsVisible = false;
            }

            var isValid = Regex.IsMatch(e.NewTextValue, "^[0-9]+$");

            if (e.NewTextValue.Length > 0)
            {
                ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }
        private void TxtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            string password = txtContrasena.Text;
            string patron_P = "^[a-zA-Z0-9]+$";
            bool esValido_P = System.Text.RegularExpressions.Regex.IsMatch(password, patron_P);
            if (esValido_P == true && password.Length >= 6)
            {
                lblError_Contraseña.Text = "";
                lblError_Contraseña.IsVisible = false;
            }

        }
    }
}
