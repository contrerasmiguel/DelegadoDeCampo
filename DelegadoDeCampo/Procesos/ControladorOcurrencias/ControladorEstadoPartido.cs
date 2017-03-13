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
    [Activity(Label = "Estado del Partido", Theme = "@android:style/Theme.NoTitleBar")]
    class ControladorEstadoPartido : Activity
    {
        //private EstadoPartido estadoPartido;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IuMenuPrincipal);

            FindViewById<Button>(Resource.Id.btnGolA).Click += ClickBtnGolA;
            FindViewById<Button>(Resource.Id.btnGolB).Click += ClickBtnGolB;

            FindViewById<Button>(Resource.Id.btnAmarillaA).Click += ClickBtnAmarillaA;
            FindViewById<Button>(Resource.Id.btnAmarillaB).Click += ClickBtnAmarillaB;

            FindViewById<Button>(Resource.Id.btnRojaA).Click += ClickBtnRojaA;
            FindViewById<Button>(Resource.Id.btnRojaB).Click += ClickBtnRojaB;

            FindViewById<Button>(Resource.Id.btnCerrarSesion).Click += ClickBtnCerrarSesion;
            FindViewById<Button>(Resource.Id.btnFinPartido).Click += ClickBtnFinPartido;
            FindViewById<Button>(Resource.Id.btnResultadosParciales).Click += ClickBtnResultadosParciales;

            FindViewById<TextView>(Resource.Id.txtPuntuacionEquipoA).Text = new ReporteOcurrencia().PuntuacionEquipoA() + "";
            FindViewById<TextView>(Resource.Id.txtPuntuacionEquipoB).Text = new ReporteOcurrencia().PuntuacionEquipoB() + "";

            FindViewById<TextView>(Resource.Id.txtEquipoA).Text = new ReporteOcurrencia().ObtenerNombreEquipoA();
            FindViewById<TextView>(Resource.Id.txtEquipoB).Text = new ReporteOcurrencia().ObtenerNombreEquipoB();
        }

        private void ClickBtnResultadosParciales(object sender, EventArgs e)
        {
            string mensaje = new ReporteOcurrencia().EnviarResultadosParciales();
            Toast.MakeText(this, mensaje, ToastLength.Short).Show();
            Toast.MakeText(ApplicationContext, "Resultados parciales enviados correctamente.", Android.Widget.ToastLength.Short).Show();
        }

        private void ClickBtnFinPartido(object sender, EventArgs e)
        {
            Toast.MakeText(this, new ReporteOcurrencia().ReportarFinalizacionPartido(), ToastLength.Short).Show();
            FindViewById<Button>(Resource.Id.btnGolA).Enabled = false;
            FindViewById<Button>(Resource.Id.btnGolB).Enabled = false;

            FindViewById<Button>(Resource.Id.btnAmarillaA).Enabled = false;
            FindViewById<Button>(Resource.Id.btnAmarillaB).Enabled = false;

            FindViewById<Button>(Resource.Id.btnRojaA).Enabled = false;
            FindViewById<Button>(Resource.Id.btnRojaB).Enabled = false;

            FindViewById<Button>(Resource.Id.btnFinPartido).Enabled = false;
            FindViewById<Button>(Resource.Id.btnResultadosParciales).Enabled = false;
            Toast.MakeText(this, "Inhabilitados botones de Ocurrencias, Información Parcial y Cerrar Sesión", ToastLength.Short).Show();
        }

        private void ClickBtnCerrarSesion(object sender, EventArgs e)
        {
            Toast.MakeText(this, new ReporteOcurrencia().CerrarSesion(), ToastLength.Short).Show();
            var i = new Intent(this, typeof(ControladorInicioSesion));
            StartActivity(i);
            Finish();
        }

        private void ClickBtnGolA(object sender, EventArgs e)
        {
            ReporteOcurrencia ro = new ReporteOcurrencia();
            int idPartido = ro.ObtenerPartidoDelegado();
            int idEquipoA = ro.ObtenerIdEquipoA(idPartido);
            Toast.MakeText(this, ro.InformacionMomentaneaA(idEquipoA, "Gol"), ToastLength.Short).Show();
            MostrarMenuSeleccionEquipo();
        }

        private void ClickBtnGolB(object sender, EventArgs e)
        {
            ReporteOcurrencia ro = new ReporteOcurrencia();
            int idPartido = ro.ObtenerPartidoDelegado();
            int idEquipoB = ro.ObtenerIdEquipoB(idPartido);
            Toast.MakeText(this, ro.InformacionMomentaneaB(idEquipoB, "Gol"), ToastLength.Short).Show();
            MostrarMenuSeleccionEquipo();
        }

        private void ClickBtnAmarillaA(object sender, EventArgs e)
        {
            ReporteOcurrencia ro = new ReporteOcurrencia();
            int idPartido = ro.ObtenerPartidoDelegado();
            int idEquipoA = ro.ObtenerIdEquipoA(idPartido);
            Toast.MakeText(this, ro.InformacionMomentaneaA(idEquipoA, "Amarilla"), ToastLength.Short).Show();
            MostrarMenuSeleccionEquipo();
        }

        private void ClickBtnAmarillaB(object sender, EventArgs e)
        {
            ReporteOcurrencia ro = new ReporteOcurrencia();
            int idPartido = ro.ObtenerPartidoDelegado();
            int idEquipoB = ro.ObtenerIdEquipoB(idPartido);
            Toast.MakeText(this, ro.InformacionMomentaneaB(idEquipoB, "Amarilla"), ToastLength.Short).Show();
            MostrarMenuSeleccionEquipo();
        }

        private void ClickBtnRojaA(object sender, EventArgs e)
        {
            ReporteOcurrencia ro = new ReporteOcurrencia();
            int idPartido = ro.ObtenerPartidoDelegado();
            int idEquipoA = ro.ObtenerIdEquipoA(idPartido);
            Toast.MakeText(this, ro.InformacionMomentaneaA(idEquipoA, "Roja"), ToastLength.Short).Show();
            MostrarMenuSeleccionEquipo();
        }

        private void ClickBtnRojaB(object sender, EventArgs e)
        {
            ReporteOcurrencia ro = new ReporteOcurrencia();
            int idPartido = ro.ObtenerPartidoDelegado();
            int idEquipoB = ro.ObtenerIdEquipoB(idPartido);
            Toast.MakeText(this, ro.InformacionMomentaneaB(idEquipoB, "Roja"), ToastLength.Short).Show();
            MostrarMenuSeleccionEquipo();
        }

        private void MostrarMenuSeleccionEquipo()
        {
            var i = new Intent(this, typeof(ControladorSeleccionJugador));
            StartActivity(i);
            Finish();
        }
    }
}