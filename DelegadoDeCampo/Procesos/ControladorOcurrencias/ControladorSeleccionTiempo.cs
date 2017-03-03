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
    [Activity(Label = "DelegadoSeleccionTiempo")]
    class ControladorSeleccionTiempo : Activity
    {
        private TiempoOcurrencia tiempo;
        NumberPicker npMinutoDecena;
        NumberPicker npMinutoUnidad;
        EstadoPartido persistencia;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IuSeleccionTiempo);

            persistencia = new EstadoPartido(Application);

            tiempo = new TiempoOcurrencia();

            npMinutoDecena = FindViewById<NumberPicker>(Resource.Id.npMinutoDecena);
            npMinutoDecena.MinValue = TiempoOcurrencia.MINIMO;
            npMinutoDecena.MaxValue = TiempoOcurrencia.MAXIMO_DECENA;
            npMinutoDecena.ValueChanged += NpMinutoDecena_ValueChanged;

            npMinutoUnidad = FindViewById<NumberPicker>(Resource.Id.npMinutoUnidad);
            npMinutoUnidad.MinValue = TiempoOcurrencia.MINIMO;
            npMinutoUnidad.MaxValue = TiempoOcurrencia.MAXIMO_UNIDAD;
            npMinutoUnidad.ValueChanged += NpMinutoUnidad_ValueChanged;

            FindViewById<Button>(Resource.Id.btnSeleccionarTiempo).Click += ControladorSeleccionTiempo_Click;
        }

        private void ControladorSeleccionTiempo_Click(object sender, EventArgs e)
        {
            var reporte = new ReporteOcurrencia();

            if (persistencia.FinalPartido == 1)
            {
                reporte.ReportarFinalizacionPartido(tiempo.Minuto);
                Toast.MakeText(ApplicationContext, "Reporte de finalización de partido enviado correctamente.", Android.Widget.ToastLength.Short).Show();
            }
            else if (persistencia.Gol == 1)
            {
                reporte.ReportarGol(persistencia.IdJugador, persistencia.IdEquipo, tiempo.Minuto);
                Toast.MakeText(ApplicationContext, "Reporte de gol enviado correctamente.", Android.Widget.ToastLength.Short).Show();

                if (persistencia.IdEquipo == 0)
                {
                    persistencia.GolesA += 1;
                }
                else
                {
                    persistencia.GolesB += 1;
                }
            }
            else if (persistencia.Amarilla == 1)
            {
                reporte.ReportarAmarilla(persistencia.IdJugador, persistencia.IdEquipo, tiempo.Minuto);
                Toast.MakeText(ApplicationContext, "Reporte de tarjeta amarilla enviado correctamente.", Android.Widget.ToastLength.Short).Show();

                if (persistencia.IdEquipo == 0)
                {
                    persistencia.AmarillasA += 1;
                }
                else
                {
                    persistencia.AmarillasB += 1;
                }
            }
            else if (persistencia.Roja == 1)
            {
                reporte.ReportarRoja(persistencia.IdJugador, persistencia.IdEquipo, tiempo.Minuto);
                Toast.MakeText(ApplicationContext, "Reporte de tarjeta roja enviado correctamente.", Android.Widget.ToastLength.Short).Show();

                if (persistencia.IdEquipo == 0)
                {
                    persistencia.RojasA += 1;
                }
                else
                {
                    persistencia.RojasB += 1;
                }
            }

            persistencia.Gol = 0;
            persistencia.Amarilla = 0;
            persistencia.Roja = 0;

            persistencia.IdEquipo = -1;
            persistencia.IdJugador = -1;

            var i = new Intent(this, typeof(ControladorEstadoPartido));
            StartActivity(i);
        }

        private void NpMinutoUnidad_ValueChanged(object sender, NumberPicker.ValueChangeEventArgs e)
        {
            tiempo.MinutoUnidad = e.NewVal;
        }

        private void NpMinutoDecena_ValueChanged(object sender, NumberPicker.ValueChangeEventArgs e)
        {
            tiempo.MinutoDecena = e.NewVal;
        }
    }
}