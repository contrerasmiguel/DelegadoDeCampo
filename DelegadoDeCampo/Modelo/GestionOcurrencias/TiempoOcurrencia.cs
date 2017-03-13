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

        static TiempoOcurrencia instancia = null;

        public static TiempoOcurrencia Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new TiempoOcurrencia();
                }

                return instancia;
            }
        }

        private TiempoOcurrencia()
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