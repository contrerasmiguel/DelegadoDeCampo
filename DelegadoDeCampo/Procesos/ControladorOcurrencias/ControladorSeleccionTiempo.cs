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

            npMinutoDecena = FindViewById<NumberPicker>(Resource.Id.npMinutoDecena);
            npMinutoDecena.MinValue = 0;
            npMinutoDecena.MaxValue = 20;
            //npMinutoDecena.ValueChanged += NpMinutoDecena_ValueChanged;

            npMinutoUnidad = FindViewById<NumberPicker>(Resource.Id.npMinutoUnidad);
            npMinutoUnidad.MinValue = 0;
            npMinutoUnidad.MaxValue = 10;
            //npMinutoUnidad.ValueChanged += NpMinutoUnidad_ValueChanged;

            FindViewById<Button>(Resource.Id.btnSeleccionarTiempo).Click += ControladorSeleccionTiempo_Click;
        }

        private void ControladorSeleccionTiempo_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Decena = " + npMinutoDecena.Value + " | Unidad = " + npMinutoUnidad.Value, ToastLength.Short).Show();
            string t = npMinutoDecena.Value + npMinutoUnidad.Value + "";
            int ti = Int32.Parse(t);
            tiempo = new TiempoOcurrencia();
            bool ban = tiempo.AlmacenarTiempoJugador(ti);
            if (ban)
            {
                Toast.MakeText(this, "Se almacenó el tiempo de la ocurrencia.", ToastLength.Short).Show();
                Finish();
            }
            else
                Toast.MakeText(this, "No se almacenó el tiempo de la ocurrencia.", ToastLength.Short).Show();
        }
    }
}