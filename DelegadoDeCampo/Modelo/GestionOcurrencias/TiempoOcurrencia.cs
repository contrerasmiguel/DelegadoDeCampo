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

using SQLite;
using DelegadoDeCampo.Modelo.Tablas;
using DelegadoDeCampo.Modelo.GestionSesionUsuario;

namespace DelegadoDeCampo.Modelo.GestionOcurrencias
{
    class TiempoOcurrencia
    {
        string directorioBD;
        string strConexión;

        public static int MINIMO = 0;
        public static int MAXIMO_DECENA = 12;
        public static int MAXIMO_UNIDAD = 9;

        private int minutoDecena;
        private int minutoUnidad;
        
        public int Minuto
        {
            get
            {
                return minutoDecena * 10 + minutoUnidad;
            }
        }

        public int MinutoDecena
        {
            get
            {
                return minutoDecena;
            }

            set
            {
                if (value >= MINIMO && value <= MAXIMO_DECENA)
                {
                    minutoDecena = value;
                }
            }
        }

        public int MinutoUnidad
        {
            get
            {
                return minutoUnidad;
            }

            set
            {
                if (value >= MINIMO && value <= MAXIMO_UNIDAD)
                {
                    minutoUnidad = value;
                }
            }
        }

        public TiempoOcurrencia()
        {
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            directorioBD = System.IO.Path.Combine(docsFolder, "db_adonet.db");
            strConexión = string.Format("Data Source={0};Version=3;", directorioBD);
        }


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


        public string CerrarSesion()
        {
            bool ban = EliminarUltimoJugadorAlmacenado();
            string mensaje = "No se eliminó el último jugador almacenado en la tabla 'Ocurrencias'.";

            // Si ban es verdadero, se obtiene el nombre del delegado conectado
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

                // Con ese nombre, se cambia el valor del campo 'Conectado' de ese delegado a falso.
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
            return mensaje;
        }


        public bool AlmacenarTiempoJugador(int tiempo)
        {
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                    var resultado1 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado1.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    // Se almacena el minuto de la ocurrencia en el campo 'MinutoOcu' de la última tupla
                    ocu[(ocu.Count() - 1)].MinutoOcu = tiempo;

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