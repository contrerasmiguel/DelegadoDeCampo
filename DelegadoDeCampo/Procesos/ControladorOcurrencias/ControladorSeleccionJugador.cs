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

using DelegadoDeCampo.Modelo.ComunicacionRemota;
using DelegadoDeCampo.Modelo.GestionEstadoPartido;

namespace DelegadoDeCampo.Procesos.ControladorOcurrencias
{
    [Activity(Label = "DelegadoSeleccionJugador")]
    class ControladorSeleccionJugador : Activity
    {
        private EstadoPartido persistencia;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IuSeleccionJugador);

            persistencia = new EstadoPartido(Application);

            Conexion con = new Conexion();
            DatosPartido datosPartido = new DatosPartido(con);

            int indiceEq = persistencia.IdEquipo;

            var listaJugadores = datosPartido.Equipos[indiceEq].Jugadores
                .Select(j => j.Numero + " - " + j.Nombre + " " + j.Apellido).ToList();

            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1
                , listaJugadores);

            FindViewById<ListView>(Resource.Id.lvJugadores).Adapter = adapter;
            FindViewById<ListView>(Resource.Id.lvJugadores).ItemClick += ClickItemLista;

            FindViewById<Button>(Resource.Id.btnAtras).Click += ClickBtnAtras;
        }

        private void ClickItemLista(object sender, AdapterView.ItemClickEventArgs e)
        {
            persistencia.IdJugador = e.Position;
            var i = new Intent(this, typeof(ControladorSeleccionTiempo));
            StartActivity(i);
        }

        private void ClickBtnAtras(object sender, EventArgs e)
        {
            Finish();
        }
    }
}