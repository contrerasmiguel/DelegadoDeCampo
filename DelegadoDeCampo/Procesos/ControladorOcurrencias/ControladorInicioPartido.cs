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
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // En caso de que el delegado haya reportado el inicio del partido previamente, ir a la siguiente actividad.
            if (ReporteOcurrencia.Instancia.PartidoIniciado())
            {
                var intent = new Intent(this, typeof(ControladorEstadoPartido));
                StartActivity(intent);
                Finish();
            }
            
            // En caso de que el delegado no haya reportado el inicio del partido, mostrar las vistas de esta actividad.
            else
            {
                SetContentView(Resource.Layout.IuInicioPartido);

                FindViewById<Button>(Resource.Id.btnIniciarPartido).Click += ControladorInicioPartido_Click;
                FindViewById<Button>(Resource.Id.btnCerrarSesionIP).Click += ControladorCerrarSesion_Click;

                FindViewById<TextView>(Resource.Id.txtSesionIniciada).Text = "Bienvenido, " + SesionUsuario.Instancia.ObtenerNombreDelegadoConectado();
            }
        }

        private void ControladorInicioPartido_Click(object sender, EventArgs e)
        {
            bool ban = ReporteOcurrencia.Instancia.ReportarInicioPartido();
            if (ban == false)
                Toast.MakeText(this, "Hay problemas.", ToastLength.Short)/*.Show()*/;
            else
            {
                Toast.MakeText(this, "El delegado inició el partido.", ToastLength.Short)/*.Show()*/;
                var i = new Intent(this, typeof(ControladorEstadoPartido));
                StartActivity(i);
                Finish();
            }
        }

        private void ControladorCerrarSesion_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, SesionUsuario.Instancia.CerrarSesion(), ToastLength.Short)/*.Show()*/;
            var i = new Intent(this, typeof(ControladorInicioSesion));
            StartActivity(i);
            Finish();
        }
    }
}