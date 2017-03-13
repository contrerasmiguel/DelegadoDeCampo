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

using DelegadoDeCampo.Modelo.GestionEstadoPartido;
using DelegadoDeCampo.Modelo.GestionOcurrencias;
using DelegadoDeCampo.Procesos.ControladorSesionUsuario;

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

            FindViewById<Button>(Resource.Id.btnCerrarSesionJ).Click += ClickBtnCerrarSesion;
            FindViewById<Button>(Resource.Id.btnAtras).Click += ClickBtnAtras;

            // Primero se obtiene el id que que se almacen� con anterioridad en la �ltima tupla de la tabla 'Ocurrencias'
            int id = new ReporteOcurrencia().ObtenerIdEquipo();
            if (id != int.MinValue)
            {
                Toast.MakeText(this, "Se obtuvo el idEquipo = " + id, ToastLength.Short).Show();
            }
            else
                Toast.MakeText(this, "NO se obtuvo el idEquipo", ToastLength.Short).Show();

            // Se hace el mismo procedimiento para obtener el nombre almacenado en esa tupla
            string nombre = new ReporteOcurrencia().ObtenerUltimoNombreEquipoAlmacenado();
            if (nombre != string.Empty)
            {
                Toast.MakeText(this, "Se obtuvo el Nombre = " + nombre, ToastLength.Short).Show();
            }
            else
                Toast.MakeText(this, "NO se obtuvo el nombre", ToastLength.Short).Show();

            // Luego obtengo el listado de jugadores que pertenecen a ese equipo
            jug = new Jugadores();
            _listaItems = jug.ObtenerListadoJugadores(id, nombre);
            if (_listaItems.Count() > 0)
                Toast.MakeText(this, "La lista est� llena.", ToastLength.Short).Show();
            else
                Toast.MakeText(this, "La lista est� vac�a.", ToastLength.Short).Show();

            _myLista = FindViewById<ListView>(Resource.Id.lvJugadores);
            _myLista.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, _listaItems);
            _myLista.ItemClick += ClickItemLista;
        }

        private void ClickBtnCerrarSesion(object sender, EventArgs e)
        {
            Toast.MakeText(this, jug.CerrarSesion(), ToastLength.Short).Show();
            var i = new Intent(this, typeof(ControladorInicioSesion));
            StartActivity(i);
            Finish();
        }

        private void ClickItemLista(object sender, AdapterView.ItemClickEventArgs e)
        {
            bool ban = jug.AlmacenarNumJugador(_listaItems[e.Position]);
            if (ban)
            {
                Toast.MakeText(this, "Se ingres� el n�mero de camiseta", ToastLength.Short).Show();
                var i = new Intent(this, typeof(ControladorSeleccionTiempo));
                StartActivity(i);
                Finish();
            }
            else
                Toast.MakeText(this, "No se ingres� el n�mero de camiseta", ToastLength.Short).Show();
        }

        private void ClickBtnAtras(object sender, EventArgs e)
        {
            bool ban = jug.EliminarUltimoJugadorAlmacenado();
            if (ban)
            {
                Toast.MakeText(this, "Se elimin� el jugador ingresado", ToastLength.Short).Show();
                var i = new Intent(this, typeof(ControladorEstadoPartido));
                StartActivity(i);
                Finish();
            }
            else
                Toast.MakeText(this, "No se elimin� el jugador ingresado", ToastLength.Short).Show();
        }
    }
}