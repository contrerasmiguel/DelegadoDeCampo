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

            FindViewById<TextView>(Resource.Id.txtPuntuacionEquipoA).Text = ReporteOcurrencia.Instancia.PuntuacionEquipoA() + "";
            FindViewById<TextView>(Resource.Id.txtPuntuacionEquipoB).Text = ReporteOcurrencia.Instancia.PuntuacionEquipoB() + "";

            FindViewById<TextView>(Resource.Id.txtEquipoA).Text = ReporteOcurrencia.Instancia.ObtenerNombreEquipoA();
            FindViewById<TextView>(Resource.Id.txtEquipoB).Text = ReporteOcurrencia.Instancia.ObtenerNombreEquipoB();
        }

        private void ClickBtnResultadosParciales(object sender, EventArgs e)
        {
            string mensaje = ReporteOcurrencia.Instancia.EnviarResultadosParciales();
            Toast.MakeText(this, mensaje, ToastLength.Short)/*.Show()*/;
            Toast.MakeText(ApplicationContext, "Resultados parciales enviados correctamente.", Android.Widget.ToastLength.Short).Show();
        }

        private void ClickBtnFinPartido(object sender, EventArgs e)
        {
            Toast.MakeText(this, ReporteOcurrencia.Instancia.ReportarFinalizacionPartido(), ToastLength.Short)/*.Show()*/;
            FindViewById<Button>(Resource.Id.btnGolA).Enabled = false;
            FindViewById<Button>(Resource.Id.btnGolB).Enabled = false;

            FindViewById<Button>(Resource.Id.btnAmarillaA).Enabled = false;
            FindViewById<Button>(Resource.Id.btnAmarillaB).Enabled = false;

            FindViewById<Button>(Resource.Id.btnRojaA).Enabled = false;
            FindViewById<Button>(Resource.Id.btnRojaB).Enabled = false;

            FindViewById<Button>(Resource.Id.btnFinPartido).Enabled = false;
            FindViewById<Button>(Resource.Id.btnResultadosParciales).Enabled = false;

            // Parche para evitar que el usuario habilite los botones destruyendo la actividad y volviéndola a abrir. 
            SesionUsuario.Instancia.CerrarSesion();

            Toast.MakeText(this, "Final del partido reportado correctamente.", ToastLength.Short).Show();
        }

        private void ClickBtnCerrarSesion(object sender, EventArgs e)
        {
            Toast.MakeText(this, SesionUsuario.Instancia.CerrarSesion(), ToastLength.Short)/*.Show()*/;
            var i = new Intent(this, typeof(ControladorInicioSesion));
            StartActivity(i);
            Finish();
        }

        private void ClickBtnGolA(object sender, EventArgs e)
        {
            ReporteOcurrencia ro = ReporteOcurrencia.Instancia;
            int idPartido = ro.ObtenerPartidoDelegado();
            int idEquipoA = ro.ObtenerIdEquipoA(idPartido);
            Toast.MakeText(this, ro.InformacionMomentaneaA(idEquipoA, "Gol"), ToastLength.Short)/*.Show()*/;
            MostrarMenuSeleccionEquipo();
        }

        private void ClickBtnGolB(object sender, EventArgs e)
        {
            ReporteOcurrencia ro = ReporteOcurrencia.Instancia;
            int idPartido = ro.ObtenerPartidoDelegado();
            int idEquipoB = ro.ObtenerIdEquipoB(idPartido);
            Toast.MakeText(this, ro.InformacionMomentaneaB(idEquipoB, "Gol"), ToastLength.Short)/*.Show()*/;
            MostrarMenuSeleccionEquipo();
        }

        private void ClickBtnAmarillaA(object sender, EventArgs e)
        {
            ReporteOcurrencia ro = ReporteOcurrencia.Instancia;
            int idPartido = ro.ObtenerPartidoDelegado();
            int idEquipoA = ro.ObtenerIdEquipoA(idPartido);
            Toast.MakeText(this, ro.InformacionMomentaneaA(idEquipoA, "Amarilla"), ToastLength.Short)/*.Show()*/;
            MostrarMenuSeleccionEquipo();
        }

        private void ClickBtnAmarillaB(object sender, EventArgs e)
        {
            ReporteOcurrencia ro = ReporteOcurrencia.Instancia;
            int idPartido = ro.ObtenerPartidoDelegado();
            int idEquipoB = ro.ObtenerIdEquipoB(idPartido);
            Toast.MakeText(this, ro.InformacionMomentaneaB(idEquipoB, "Amarilla"), ToastLength.Short)/*.Show()*/;
            MostrarMenuSeleccionEquipo();
        }

        private void ClickBtnRojaA(object sender, EventArgs e)
        {
            ReporteOcurrencia ro = ReporteOcurrencia.Instancia;
            int idPartido = ro.ObtenerPartidoDelegado();
            int idEquipoA = ro.ObtenerIdEquipoA(idPartido);
            Toast.MakeText(this, ro.InformacionMomentaneaA(idEquipoA, "Roja"), ToastLength.Short)/*.Show()*/;
            MostrarMenuSeleccionEquipo();
        }

        private void ClickBtnRojaB(object sender, EventArgs e)
        {
            ReporteOcurrencia ro = ReporteOcurrencia.Instancia;
            int idPartido = ro.ObtenerPartidoDelegado();
            int idEquipoB = ro.ObtenerIdEquipoB(idPartido);
            Toast.MakeText(this, ro.InformacionMomentaneaB(idEquipoB, "Roja"), ToastLength.Short)/*.Show()*/;
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