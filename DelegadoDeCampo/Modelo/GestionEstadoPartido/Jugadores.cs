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
using DelegadoDeCampo.Modelo.Tablas;

using SQLite;
using DelegadoDeCampo.Modelo.GestionSesionUsuario;

namespace DelegadoDeCampo.Modelo.GestionEstadoPartido
{
    class Jugadores
    {
        private Equipo equipo;
        private string nombre, apellido;
        private ushort numero;

        string directorioBD;
        string strConexión;

        public string Apellido
        {
            get
            {
                return apellido;
            }

            set
            {
                apellido = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public ushort Numero
        {
            get
            {
                return numero;
            }

            set
            {
                numero = value;
            }
        }

        internal Equipo Equipo
        {
            get
            {
                return equipo;
            }

            set
            {
                equipo = value;
            }
        }

        /*public Jugadores(string nom, string a, ushort num)
        {
            nombre = nom;
            apellido = a;
            numero = num;
        }*/

        public Jugadores()
        {
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            directorioBD = System.IO.Path.Combine(docsFolder, "db_adonet.db");
            strConexión = string.Format("Data Source={0};Version=3;", directorioBD);
        }

        public string CerrarSesion()
        {
            bool ban = EliminarUltimoJugadorAlmacenado();
            string mensaje = "No se eliminó el último jugador almacenado en la tabla 'Ocurrencias'.";

            if (ban)
            {
                string nombreUsuario = string.Empty;
                try
                {
                    using (var db = new SQLiteConnection(directorioBD))
                    {
                        var resultado = db.Table<TablaDelegadoConectado>();
                        IEnumerable<TablaDelegadoConectado> tabla_delCo = resultado.ToList<TablaDelegadoConectado>();
                        List<TablaDelegadoConectado> delCo = tabla_delCo.ToList<TablaDelegadoConectado>();

                        nombreUsuario = delCo[(delCo.Count() - 1)].Username;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    using (var db = new SQLiteConnection(directorioBD))
                    {

                        var resultado = db.Table<TablaDelegados>();
                        IEnumerable<TablaDelegados> tabla_del = resultado.ToList<TablaDelegados>();
                        List<TablaDelegados> del = tabla_del.ToList<TablaDelegados>();

                        // Primero se verifica cuál delegado está conectado
                        for (int i = 0; i < del.Count(); i++)
                        {
                            if (del[i].Username.Equals(nombreUsuario))
                            {
                                del[i].Conectado = false;
                                mensaje = "El delegado cerró sesión.";
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                // Se elimina el delegado que inició sesión de la tabla "Delegado conectado"
                try
                {
                    using (var db = new SQLiteConnection(directorioBD))
                    {
                        var resultado = db.Table<TablaDelegadoConectado>();
                        IEnumerable<TablaDelegadoConectado> tabla_delCo = resultado.ToList<TablaDelegadoConectado>();
                        List<TablaDelegadoConectado> delCo = tabla_delCo.ToList<TablaDelegadoConectado>();

                        db.Delete(delCo[(delCo.Count() - 1)]);
                        mensaje += " Se eliminó el delegado conetado.";
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    mensaje += " Problemas a la hora de cerrar sesión.";
                }
            }
            // Se obtiene, primeramente, el nombre de usuario del delegado conectado que está asignado al partido
            
            return mensaje;
        }

        public List<string> ObtenerListadoJugadores(int id, string nombreEquipo)
        {
            List<string> jugadores = new List<string>();
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {

                    var resultado = db.Table<TablaJugadores>();
                    IEnumerable<TablaJugadores> tabla_jug = resultado.ToList<TablaJugadores>();
                    List<TablaJugadores> jug = tabla_jug.ToList<TablaJugadores>();

                    for (int i = 0; i < jug.Count(); i++)
                        if ((jug[i].idEquipoJug == id) && (jug[i].NombreEquiJug.Equals(nombreEquipo)))
                            jugadores.Add(jug[i].NumCamiseta.ToString() + ".- " + jug[i].NombreJug + " " + jug[i].ApellidoJug);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return jugadores;
        }

        /*private List<TablaJugadores> ObtenerTablaJugadores()
        {
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                    var resultado1 = db.Table<TablaJugadores>();
                    IEnumerable<TablaJugadores> tabla_ocu = resultado1.ToList<TablaJugadores>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    // Se obtiene la última tupla
                    int j;
                    for (j = 0; j < ocu.Count(); j++) ;

                    // 
                    db.Update(ocu[pos].NumCamisetaJug);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return int.MinValue;
        }*/

        public bool EliminarUltimoJugadorAlmacenado()
        {
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                    var resultado1 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado1.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    // Se elimina la última tupla almacenada de esta tabla
                    db.Delete(ocu[(ocu.Count() - 1)]);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool AlmacenarNumJugador(string cad)
        {
            // Se recorre la cadena de caracteres para obtener el número del jugador
            string cad1 = cad[0] + cad[1] + "";
            if (cad1.Contains('.'))
                cad1 = cad[0] + "";       
            try
            {
                int num = Int32.Parse(cad1);
                using (var db = new SQLiteConnection(directorioBD))
                {
                    // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                    var resultado1 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado1.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    // Se almacena el número de camiseta en el campo 'NumCamisetaJug' de la última tupla
                    ocu[(ocu.Count() - 1)].NumCamisetaJug = num;

                    db.Update(ocu[(ocu.Count() - 1)]);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}