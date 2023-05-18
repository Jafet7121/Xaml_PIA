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
    public partial class PagePago : ContentPage
    {
        private Compra oGlobalCompra = new Compra();
        public PagePago(Compra oCompra)
        {
            InitializeComponent();
            oGlobalCompra = oCompra;

            obtenerEmail();
        }
        private async void BtnConfirmarPago_Clicked(object sender, EventArgs e)
        {
            float precioTotal = 0;

            if (string.IsNullOrWhiteSpace(txtNumeroTarjeta.Text) || string.IsNullOrWhiteSpace(txtFechaMessExpiracion.Text) || string.IsNullOrWhiteSpace(txtFechaAñoExpiracion.Text) || string.IsNullOrWhiteSpace(txtCodigoCVV.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                await DisplayAlert("Mensaje", "Complete todos los campos", "Ok");
                return;
            }
            else
            {
                string NumTarjeta = txtNumeroTarjeta.Text;
                string CVV = txtCodigoCVV.Text;

                if (NumTarjeta.Length < 16 || CVV.Length < 3)
                {
                    await DisplayAlert("Oops", "Verifique los datos incorrectos.", "Aceptar");
                    if (NumTarjeta.Length < 16)
                    {
                        lblError_NumTarjeta.IsVisible = true;
                        lblError_NumTarjeta.Text = "Faltan caracteres.";
                    }
                    if (CVV.Length < 3)
                    {
                        lblError_CVV.IsVisible = true;
                        lblError_CVV.Text = "Faltan caracteres.";
                    }
                }
                else
                {
                    DetallePago oDetallePago = new DetallePago()
                    {
                        numeroTarjeta = txtNumeroTarjeta.Text,
                        fechaExpiracion = string.Format("{0}/{1}", txtFechaMessExpiracion.Text, txtFechaAñoExpiracion.Text),
                        codigoCVV = txtCodigoCVV.Text,
                        email = txtEmail.Text
                    };

                    foreach (Bolsa item in oGlobalCompra.oListaBolsa)
                    {
                        precioTotal = precioTotal + item.montoTotal;
                    }


                    oGlobalCompra.oDetallePago = oDetallePago;
                    oGlobalCompra.precioTotal = precioTotal;

                    bool respuesta = await ApiServiceFirebase.RegistrarCompra(oGlobalCompra);

                    if (respuesta)
                    {
                        await DisplayAlert("Mensaje", "La compra fue realizada!", "Ok");
                        App.Current.MainPage = new PageInicio();
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "No se pudo completar la compra", "Ok");
                    }


                }
            }
        }
        private async void obtenerEmail()
        {
            Usuario oUsuario = await ApiServiceFirebase.ObtenerUsuario();
            txtEmail.Text = oUsuario.Email;
        }
        private void TxtNumTarjeta_TextChanged(object sender, TextChangedEventArgs e)
        {
            string nt = txtNumeroTarjeta.Text;

            if (nt.Length == 16)
            {
                lblError_NumTarjeta.Text = "";
                lblError_NumTarjeta.IsVisible = false;
            }
            var isValid = Regex.IsMatch(e.NewTextValue, "^[0-9]+$");

            if (e.NewTextValue.Length > 0)
            {
                ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }
        private void TxtCVV_TextChanged(object sender, TextChangedEventArgs e)
        {
            string cvv = txtCodigoCVV.Text;

            if (cvv.Length == 3)
            {
                lblError_CVV.Text = "";
                lblError_CVV.IsVisible = false;
            }
            var isValid = Regex.IsMatch(e.NewTextValue, "^[0-9]+$");

            if (e.NewTextValue.Length > 0)
            {
                ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }
        private void TxtFechaMessExpiracion_TextChanged(object sender, TextChangedEventArgs e)
        {
            string mes = txtFechaMessExpiracion.Text;
            string año = txtFechaAñoExpiracion.Text;
            ValidateFechaExpiracion(mes, año);
        }
        private void TxtFechaAñoExpiracion_TextChanged(object sender, TextChangedEventArgs e)
        {
            string mes = txtFechaMessExpiracion.Text;
            string año = txtFechaAñoExpiracion.Text;
            ValidateFechaExpiracion(mes, año);
        }
        private void ValidateFechaExpiracion(string mes, string año)
        {
            DateTime fechaActual = DateTime.Now;
            int mesExpiracion;
            int añoExpiracion;

            bool isMesValido = int.TryParse(mes, out mesExpiracion);
            bool isAñoValido = int.TryParse(año, out añoExpiracion);

            if (isMesValido && isAñoValido)
            {
                if (mesExpiracion < 1 || mesExpiracion > 12)
                {
                    lblError_FechaExpiracion.Text = "El mes de expiración no es válido.";
                    lblError_FechaExpiracion.IsVisible = true;
                    btnConfirmarPago.IsEnabled = false;
                    return;
                }

                int añoActual = fechaActual.Year % 100;

                if (añoExpiracion < añoActual)
                {
                    lblError_FechaExpiracion.Text = "El año de expiración no es válido.";
                    lblError_FechaExpiracion.IsVisible = true;
                    btnConfirmarPago.IsEnabled = false;
                    return;
                }


                DateTime fechaExpiracion = new DateTime(añoExpiracion + 2000, mesExpiracion, DateTime.DaysInMonth(añoExpiracion + 2000, mesExpiracion));
                if (fechaExpiracion < fechaActual)
                {
                    lblError_FechaExpiracion.Text = "La tarjeta ha expirado.";
                    lblError_FechaExpiracion.IsVisible = true;
                    btnConfirmarPago.IsEnabled = false;
                    return;
                }
            }

            btnConfirmarPago.IsEnabled = true;
            lblError_FechaExpiracion.IsVisible = false;
        }
    }
}