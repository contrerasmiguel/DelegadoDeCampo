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
using DelegadoDeCampo.Modelo.ComunicacionRemota;

using DelegadoDeCampo.Procesos.ControladorSesionUsuario;

namespace DelegadoDeCampo.Procesos.ControladorOcurrencias
{
    [Activity(Label = "DelegadoEstadoPartido")]
    class ControladorEstadoPartido : Activity
    {
        private EstadoPartido estadoPartido;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IuMenuPrincipal);

            estadoPartido = new EstadoPartido(Application);

            if (estadoPartido.FinalPartido == 1)
            {
                FindViewById<Button>(Resource.Id.btnGolA).Enabled = false;
                FindViewById<Button>(Resource.Id.btnGolB).Enabled = false;

                FindViewById<Button>(Resource.Id.btnAmarillaA).Enabled = false;
                FindViewById<Button>(Resource.Id.btnAmarillaB).Enabled = false;

                FindViewById<Button>(Resource.Id.btnRojaA).Enabled = false;
                FindViewById<Button>(Resource.Id.btnRojaB).Enabled = false;

                FindViewById<Button>(Resource.Id.btnFinPartido).Enabled = false;
            }

            Conexion con = new Conexion();
            DatosPartido datosPartido = new DatosPartido(con);

            FindViewById<TextView>(Resource.Id.txtPuntuacionEquipoA).Text = estadoPartido.GolesA.ToString();
            FindViewById<TextView>(Resource.Id.txtPuntuacionEquipoB).Text = estadoPartido.GolesB.ToString();

            FindViewById<TextView>(Resource.Id.txtEquipoA).Text = datosPartido.Equipos[0].Nombre;
            FindViewById<TextView>(Resource.Id.txtEquipoB).Text = datosPartido.Equipos[1].Nombre;

            FindViewById<Button>(Resource.Id.btnGolA).Click += ClickBtnGolA;
            FindViewById<Button>(Resource.Id.btnGolB).Click += ClickBtnGolB;

            FindViewById<Button>(Resource.Id.btnAmarillaA).Click += ClickBtnAmarillaA;
            FindViewById<Button>(Resource.Id.btnAmarillaB).Click += ClickBtnAmarillaB;

            FindViewById<Button>(Resource.Id.btnRojaA).Click += ClickBtnRojaA;
            FindViewById<Button>(Resource.Id.btnRojaB).Click += ClickBtnRojaB;

            FindViewById<Button>(Resource.Id.btnCerrarSesion).Click += ClickBtnCerrarSesion;
            FindViewById<Button>(Resource.Id.btnFinPartido).Click += ClickBtnFinPartido;
            FindViewById<Button>(Resource.Id.btnResultadosParciales).Click += ClickBtnResultadosParciales;
        }

        private void ClickBtnResultadosParciales(object sender, EventArgs e)
        {
            new ReporteOcurrencia().EnviarResultadosParciales(estadoPartido);
            Toast.MakeText(ApplicationContext, "Resultados parciales enviados correctamente.", Android.Widget.ToastLength.Short).Show();
        }

        private void ClickBtnFinPartido(object sender, EventArgs e)
        {
            estadoPartido.FinalPartido = 1;
            var i = new Intent(this, typeof(ControladorSeleccionTiempo));
            StartActivity(i);
        }

        private void ClickBtnCerrarSesion(object sender, EventArgs e)
        {
            estadoPartido.Amarilla = 0;
            estadoPartido.AmarillasA = 0;
            estadoPartido.AmarillasB = 0;
            estadoPartido.Gol = 0;
            estadoPartido.GolesA = 0;
            estadoPartido.GolesB = 0;
            estadoPartido.IdEquipo = -1;
            estadoPartido.IdJugador = -1;
            estadoPartido.Roja = 0;
            estadoPartido.RojasA = 0;
            estadoPartido.RojasB = 0;

            var i = new Intent(this, typeof(ControladorInicioSesion));
            StartActivity(i);
        }

        private void ClickBtnGolA(object sender, EventArgs e)
        {
            estadoPartido.Gol = 1;
            MostrarMenuSeleccionEquipo(0);
        }

        private void ClickBtnGolB(object sender, EventArgs e)
        {
            estadoPartido.Gol = 1;
            MostrarMenuSeleccionEquipo(1);
        }

        private void ClickBtnAmarillaA(object sender, EventArgs e)
        {
            estadoPartido.Amarilla = 1;
            MostrarMenuSeleccionEquipo(0);
        }

        private void ClickBtnAmarillaB(object sender, EventArgs e)
        {
            estadoPartido.Amarilla = 1;
            MostrarMenuSeleccionEquipo(1);
        }

        private void ClickBtnRojaA(object sender, EventArgs e)
        {
            estadoPartido.Roja = 1;
            MostrarMenuSeleccionEquipo(0);
        }

        private void ClickBtnRojaB(object sender, EventArgs e)
        {
            estadoPartido.Roja = 1;
            MostrarMenuSeleccionEquipo(1);
        }

        private void MostrarMenuSeleccionEquipo(int indiceEq)
        {
            estadoPartido.IdEquipo = indiceEq;
            var i = new Intent(this, typeof(ControladorSeleccionJugador));
            StartActivity(i);
        }
    }
}