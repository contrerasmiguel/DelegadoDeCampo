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
using DelegadoDeCampo.Modelo.GestionSesionUsuario;
using DelegadoDeCampo.Procesos.ControladorSesionUsuario;

namespace DelegadoDeCampo.Procesos.ControladorOcurrencias
{
    [Activity(Label = "Selección Tiempo", Theme = "@android:style/Theme.NoTitleBar")]
    class ControladorSeleccionTiempo : Activity
    {
        private TiempoOcurrencia tiempo;
        NumberPicker npMinutoDecena;
        NumberPicker npMinutoUnidad;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IuSeleccionTiempo);

            tiempo = TiempoOcurrencia.Instancia;
            FindViewById<Button>(Resource.Id.btnCerrarSesionT).Click += ControladorCerrarSesion_Click;
            npMinutoDecena = FindViewById<NumberPicker>(Resource.Id.npMinutoDecena);
            npMinutoDecena.MinValue = TiempoOcurrencia.MINIMO;
            npMinutoDecena.MaxValue = TiempoOcurrencia.MAXIMO_DECENA;

            npMinutoUnidad = FindViewById<NumberPicker>(Resource.Id.npMinutoUnidad);
            npMinutoUnidad.MinValue = TiempoOcurrencia.MINIMO;
            npMinutoUnidad.MaxValue = TiempoOcurrencia.MAXIMO_UNIDAD;

            FindViewById<Button>(Resource.Id.btnSeleccionarTiempo).Click += ControladorSeleccionTiempo_Click;
        }

        private void ControladorCerrarSesion_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, SesionUsuario.Instancia.CerrarSesion(), ToastLength.Short)/*.Show()*/;
            var i = new Intent(this, typeof(ControladorInicioSesion));
            StartActivity(i);
            Finish();
        }

        private void ControladorSeleccionTiempo_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Decena = " + npMinutoDecena.Value + " | Unidad = " + npMinutoUnidad.Value, ToastLength.Short)/*.Show()*/;
            string t = npMinutoDecena.Value + npMinutoUnidad.Value + "";
            int ti = Int32.Parse(t);
            bool ban = tiempo.AlmacenarTiempoJugador(ti);
            if (ban)
            {
                Toast.MakeText(this, "Se almacenó el tiempo de la ocurrencia.", ToastLength.Short)/*.Show()*/;
                var i = new Intent(this, typeof(ControladorEstadoPartido));
                StartActivity(i);
                Finish();
            }
            else
                Toast.MakeText(this, "No se almacenó el tiempo de la ocurrencia.", ToastLength.Short)/*.Show()*/;
        }
    }
}