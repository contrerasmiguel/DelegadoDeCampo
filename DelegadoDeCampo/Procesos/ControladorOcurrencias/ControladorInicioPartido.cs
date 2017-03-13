using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using DelegadoDeCampo.Modelo.GestionOcurrencias;
using DelegadoDeCampo.Modelo.GestionEstadoPartido;
using DelegadoDeCampo.Procesos.ControladorSesionUsuario;
using DelegadoDeCampo.Modelo.GestionSesionUsuario;

namespace DelegadoDeCampo.Procesos.ControladorOcurrencias
{
    [Activity(Label = "Sesión iniciada", Theme = "@android:style/Theme.NoTitleBar")]
    class ControladorInicioPartido : Activity
    {
        EstadoPartido estadoPartido;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IuInicioPartido);
            estadoPartido = new EstadoPartido();
            
            FindViewById<Button>(Resource.Id.btnIniciarPartido).Click += ControladorInicioPartido_Click;
            FindViewById<Button>(Resource.Id.btnCerrarSesionIP).Click += ControladorCerrarSesion_Click;

            FindViewById<TextView>(Resource.Id.txtSesionIniciada).Text = "Bienvenido, " + new SesionUsuario().ObtenerNombreDelegadoConectado();
        }

        private void ControladorInicioPartido_Click(object sender, EventArgs e)
        {
            bool ban = new ReporteOcurrencia().ReportarInicioPartido();
            if (ban == false)
                Toast.MakeText(this, "Hay problemas.", ToastLength.Short).Show();
            else
            {
                Toast.MakeText(this, "El delegado inició el partido.", ToastLength.Short).Show();
                var i = new Intent(this, typeof(ControladorEstadoPartido));
                StartActivity(i);
                Finish();
            }
        }

        private void ControladorCerrarSesion_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, estadoPartido.CerrarSesion(), ToastLength.Short).Show();
            var i = new Intent(this, typeof(ControladorInicioSesion));
            StartActivity(i);
            Finish();
        }
    }
}