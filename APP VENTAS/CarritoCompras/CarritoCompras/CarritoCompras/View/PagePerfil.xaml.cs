using CarritoCompras.Modelo;
using CarritoCompras.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarritoCompras.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePerfil : ContentPage
    {
        public PagePerfil()
        {
            InitializeComponent();
            obtenerUsuario();

        }
        Usuario oGlobalUsuario;

        private async void BtnGuardarCambios_Clicked(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() == "" || txtApellido.Text.Trim() == "" || txtDocumento.Text.Trim() == "")
            {
                await DisplayAlert("Mensaje", "Debe completar todos los campos", "OK");
                return;
            }
            else
            {
                string Phone = txtDocumento.Text;

                if (Phone.Length < 10)
                {
                    await DisplayAlert("Oops", "Verifique los datos incorrectos.", "Aceptar");
                    lblError_Documento.IsVisible = true;
                    lblError_Documento.Text = "Faltan caracteres.";
                }
                else
                {
                    Usuario oUsuario = new Usuario()
                    {
                        Nombres = txtNombre.Text,
                        Apellidos = txtApellido.Text,
                        Documento = txtDocumento.Text,
                        Clave = oGlobalUsuario.Clave,
                        Email = oGlobalUsuario.Email
                    };

                    bool respuesta = await ApiServiceFirebase.GuardarCambiosUsuario(oUsuario);

                    if (respuesta)
                    {
                        await DisplayAlert("Mensaje", "Se guardaron los cambios", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "Hubo un error al guardar", "OK");
                    }

                }
            }
        }

        private async void obtenerUsuario()
        {

            oGlobalUsuario = await ApiServiceFirebase.ObtenerUsuario();

            if (oGlobalUsuario != null)
            {
                txtNombre.Text = oGlobalUsuario.Nombres;
                txtApellido.Text = oGlobalUsuario.Apellidos;
                txtDocumento.Text = oGlobalUsuario.Documento;
                txtEmail.Text = oGlobalUsuario.Email;

            }
        }

        private void BtnCerrarSesion_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new PageLogin();
        }
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var isValid = Regex.IsMatch(e.NewTextValue, "^[a-zA-Z]+$");

            if (e.NewTextValue.Length > 0)
            {
                ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }
        private void TxtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Phone = txtDocumento.Text;

            if (Phone.Length == 10)
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

    }
}