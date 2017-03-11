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
using DelegadoDeCampo.Modelo.Tablas;
using DelegadoDeCampo.Modelo.GestionOcurrencias;

namespace DelegadoDeCampo.Procesos.ControladorOcurrencias
{
    [Activity(Label = "Listado de Jugadores", Theme = "@android:style/Theme.NoTitleBar")]
    class ControladorSeleccionJugador : Activity
    {
        private List<string> _listaItems;
        private ListView _myLista;

        Jugadores jug;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IuSeleccionJugador);

            FindViewById<Button>(Resource.Id.btnAtras).Click += ClickBtnAtras;

            // Primero se obtiene el id que que se almacenó con anterioridad en la última tupla de la tabla 'Ocurrencias'
            int id = new ReporteOcurrencia().ObtenerIdEquipo();
            if (id != int.MinValue)
            {
                Toast.MakeText(this, "Se obtuvo el idEquipo = " + id, ToastLength.Short).Show();
            }
            else
                Toast.MakeText(this, "Ne se obtuvo el idEquipo", ToastLength.Short).Show();

            // Luego obtengo el listado de jugadores que pertenecen a ese equipo
            jug = new Jugadores();
            _listaItems = jug.ObtenerListadoJugadores(id);
            if (_listaItems.Count() > 0)
                Toast.MakeText(this, "La lista está llena.", ToastLength.Short).Show();
            else
                Toast.MakeText(this, "La lista está vacía.", ToastLength.Short).Show();

            _myLista = FindViewById<ListView>(Resource.Id.lvJugadores);
            _myLista.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, _listaItems);
            _myLista.ItemClick += ClickItemLista;


        }

        private void ClickItemLista(object sender, AdapterView.ItemClickEventArgs e)
        {
            bool ban = jug.AlmacenarNumJugador(_listaItems[e.Position]);
            if (ban)
            {
                Toast.MakeText(this, "Se ingresó el número de camiseta", ToastLength.Short).Show();
                var i = new Intent(this, typeof(ControladorSeleccionTiempo));
                StartActivity(i);
                Finish();
            }
            else
                Toast.MakeText(this, "No se ingresó el número de camiseta", ToastLength.Short).Show();
        }

        private void ClickBtnAtras(object sender, EventArgs e)
        {
            bool ban = jug.EliminarUltimoJugadorAlmacenado();
            if (ban)
            {
                Toast.MakeText(this, "Se eliminó el jugador ingresado", ToastLength.Short).Show();
                Finish();
            }
            else
                Toast.MakeText(this, "No se eliminó el jugador ingresado", ToastLength.Short).Show();
        }
    }
}