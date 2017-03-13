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

using System.Data;
using System.IO;

using SQLite;

using DelegadoDeCampo.Modelo.GestionSesionUsuario;
using DelegadoDeCampo.Modelo.Tablas;

namespace DelegadoDeCampo.Modelo.GestionEstadoPartido
{
    class EstadoPartido
    {
        private Application app;
        private int idEquipo, idJugador, golesA, golesB, amarillasA, amarillasB, rojasA
            , rojasB, gol, amarilla, roja, finalPartido;

        public int IdEquipo
        {
            get
            {
                return idEquipo;
            }
            set
            {
                idEquipo = value;
                AlmacenarDatos(app);
            }
        }

        public int IdJugador
        {
            get
            {
                return idJugador;
            }
            set
            {
                idJugador = value;
                AlmacenarDatos(app);
            }
        }

        public int Gol
        {
            get
            {
                return gol;
            }
            set
            {
                gol = value;
                AlmacenarDatos(app);
            }
        }

        public int Amarilla
        {
            get
            {
                return amarilla;
            }
            set
            {
                amarilla = value;
                AlmacenarDatos(app);
            }
        }

        public int Roja
        {
            get
            {
                return roja;
            }
            set
            {
                roja = value;
                AlmacenarDatos(app);
            }
        }

        public int GolesA
        {
            get
            {
                return golesA;
            }
            set
            {
                golesA = value;
                AlmacenarDatos(app);
            }
        }

        public int GolesB
        {
            get
            {
                return golesB;
            }
            set
            {
                golesB = value;
                AlmacenarDatos(app);
            }
        }

        public int AmarillasA
        {
            get
            {
                return amarillasA;
            }
            set
            {
                amarillasA = value;
                AlmacenarDatos(app);
            }
        }

        public int AmarillasB
        {
            get
            {
                return amarillasB;
            }
            set
            {
                amarillasB = value;
                AlmacenarDatos(app);
            }
        }

        public int RojasA
        {
            get
            {
                return rojasA;
            }
            set
            {
                rojasA = value;
                AlmacenarDatos(app);
            }
        }

        public int RojasB
        {
            get
            {
                return rojasB;
            }
            set
            {
                rojasB = value;
                AlmacenarDatos(app);
            }
        }

        public int FinalPartido
        {
            get
            {
                return finalPartido;
            }
            set
            {
                finalPartido = value;
                AlmacenarDatos(app);
            }
        }

        string directorioBD;
        string strConexión;

        public EstadoPartido()
        {
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            directorioBD = System.IO.Path.Combine(docsFolder, "db_adonet.db");
            strConexión = string.Format("Data Source={0};Version=3;", directorioBD);
        }

        /*public EstadoPartido(Application app)
        {
            this.app = app;
            CargarDatos(app);
        }*/

        public string CerrarSesion()
        {
            // Se obtiene, primeramente, el nombre de usuario del delegado conectado que está asignado al partido
            string nombreUsuario = string.Empty;
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado = db.Table<TablaDelegadoConectado>();
                    IEnumerable<TablaDelegadoConectado> tabla_delCo = resultado.ToList<TablaDelegadoConectado>();
                    List<TablaDelegadoConectado> delCo = tabla_delCo.ToList<TablaDelegadoConectado>();

                    nombreUsuario = delCo[0].Username;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            string mensaje = string.Empty;
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

                    db.Delete(delCo[0]);
                    mensaje += " Se eliminó el delegado conetado.";
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                mensaje += " Problemas a la hora de cerrar sesión.";
            }
            return mensaje;
        }

        public static void Inicializar(Application app)
        {
            var prefs = Application.Context.GetSharedPreferences(app.PackageName, FileCreationMode.Private);
            var prefEditor = prefs.Edit();

            prefEditor.PutString("IdSeleccionEquipo", "-1");
            prefEditor.PutString("IdSeleccionTiempo", "-1");
            prefEditor.PutString("Gol", "0");
            prefEditor.PutString("Amarilla", "0");
            prefEditor.PutString("Roja", "0");

            prefEditor.PutString("GolesA", "0");
            prefEditor.PutString("AmarillasA", "0");
            prefEditor.PutString("RojasA", "0");

            prefEditor.PutString("GolesB", "0");
            prefEditor.PutString("AmarillasB", "0");
            prefEditor.PutString("RojasB", "0");

            prefEditor.PutString("FinalPartido", "0");

            prefEditor.Commit();
        }

        private void CargarDatos(Application app)
        {
            var prefs = Application.Context.GetSharedPreferences(app.PackageName, FileCreationMode.Private);

            idEquipo = int.Parse(prefs.GetString("IdSeleccionEquipo", string.Empty));
            idJugador = int.Parse(prefs.GetString("IdSeleccionTiempo", string.Empty));

            gol = int.Parse(prefs.GetString("Gol", string.Empty));
            amarilla = int.Parse(prefs.GetString("Amarilla", string.Empty));
            roja = int.Parse(prefs.GetString("Roja", string.Empty));

            golesA = int.Parse(prefs.GetString("GolesA", string.Empty));
            amarillasA = int.Parse(prefs.GetString("AmarillasA", string.Empty));
            rojasA = int.Parse(prefs.GetString("RojasA", string.Empty));

            golesB = int.Parse(prefs.GetString("GolesB", string.Empty));
            amarillasB = int.Parse(prefs.GetString("AmarillasB", string.Empty));
            rojasB = int.Parse(prefs.GetString("RojasB", string.Empty));

            finalPartido = int.Parse(prefs.GetString("FinalPartido", string.Empty));
        }

        public void AlmacenarDatos(Application app)
        {
            var prefs = Application.Context.GetSharedPreferences(app.PackageName, FileCreationMode.Private);
            var prefEditor = prefs.Edit();

            prefEditor.PutString("IdSeleccionEquipo", idEquipo.ToString());
            prefEditor.PutString("IdSeleccionTiempo", idEquipo.ToString());
            prefEditor.PutString("Gol", gol.ToString());
            prefEditor.PutString("Amarilla", amarilla.ToString());
            prefEditor.PutString("Roja", roja.ToString());

            prefEditor.PutString("GolesA", golesA.ToString());
            prefEditor.PutString("AmarillasA", amarillasA.ToString());
            prefEditor.PutString("RojasA", rojasA.ToString());

            prefEditor.PutString("GolesB", golesB.ToString());
            prefEditor.PutString("AmarillasB", amarillasB.ToString());
            prefEditor.PutString("RojasB", rojasB.ToString());
            prefEditor.PutString("FinalPartido", finalPartido.ToString());

            prefEditor.Commit();
        }
    }
}