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

using System.Data; // Agregado manualmente.
using Android.Database.Sqlite;

using System.IO;
using SQLite;

using DelegadoDeCampo.Modelo.GestionEstadoPartido;
using DelegadoDeCampo.Modelo.Tablas;

namespace DelegadoDeCampo.Modelo.GestionSesionUsuario
{
    class SesionUsuario
    {
        private bool sesionIniciada;

        string directorioBD;
        string strConexión;

        public bool SesionIniciada
        {
            get
            {
                return sesionIniciada;
            }
        }
        public SesionUsuario()
        {
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            directorioBD = System.IO.Path.Combine(docsFolder, "db_adonet.db");
            strConexión = string.Format("Data Source={0};Version=3;", directorioBD);
        }

        public string CrearBaseDeDatos()
        {
            string resultado = "Base de Datos creada";
            try
            {
                using (var db = new SQLiteConnection(directorioBD)) // Se establece la conexión con la base de datos
                {
                    db.CreateTable<TablaDelegadoConectado>(CreateFlags.None); // Se crea la tabla "Delegado Conectado"
                    resultado = "Base de Datos y tabla 'Delegado Conectado' creada";

                    db.CreateTable<TablaDelegados>(CreateFlags.None); // Se crea la tabla "Delegados"
                    if (db.Table<TablaDelegados>().Count() == 0)
                    {
                        TablaDelegados t_de = new TablaDelegados();
                        t_de.Username = "juanchomar";
                        t_de.Password = "1234";
                        t_de.Nombre = "Juan";
                        t_de.Apellido = "Martinez";
                        t_de.idPartido = 1;
                        t_de.Conectado = false;
                        db.Insert(t_de);

                        t_de = new TablaDelegados();
                        t_de.Username = "davidher";
                        t_de.Password = "5678";
                        t_de.Nombre = "David";
                        t_de.Apellido = "Hernandez";
                        t_de.idPartido = 2;
                        t_de.Conectado = false;
                        db.Insert(t_de);
                    }

                    db.CreateTable<TablaEquipoA>(CreateFlags.None);
                    if (db.Table<TablaEquipoA>().Count() == 0)
                    {
                        TablaEquipoA t_equiA = new TablaEquipoA();
                        t_equiA.Nombre = "Venezuela";
                        db.Insert(t_equiA);

                        t_equiA = new TablaEquipoA();
                        t_equiA.Nombre = "Mexico";
                        db.Insert(t_equiA);
                    }

                    db.CreateTable<TablaEquipoB>(CreateFlags.None);
                    if (db.Table<TablaEquipoB>().Count() == 0)
                    {
                        TablaEquipoB t_equiB = new TablaEquipoB();
                        t_equiB.Nombre = "Colombia";
                        db.Insert(t_equiB);

                        t_equiB = new TablaEquipoB();
                        t_equiB.Nombre = "Argentina";
                        db.Insert(t_equiB);
                    }

                    db.CreateTable<TablaFinPartido>(CreateFlags.None);
                    resultado = "Base de Datos y tabla 'Fin del Partido' creada";

                    db.CreateTable<TablaJugadores>(CreateFlags.None); // Se crea la tabla 'Jugadores'
                    if (db.Table<TablaJugadores>().Count() == 0)
                    {
                        TablaJugadores t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 3;
                        t_ju.NombreJug = "Jose";
                        t_ju.ApellidoJug = "Guevara";
                        t_ju.idEquipoJug = 1;
                        t_ju.NombreEquiJug = "Venezuela";
                        db.Insert(t_ju);

                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 5;
                        t_ju.NombreJug = "Miguel";
                        t_ju.ApellidoJug = "Contreras";
                        t_ju.idEquipoJug = 1;
                        t_ju.NombreEquiJug = "Venezuela";
                        db.Insert(t_ju);

                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 11;
                        t_ju.NombreJug = "Brian";
                        t_ju.ApellidoJug = "Johnson";
                        t_ju.idEquipoJug = 1;
                        t_ju.NombreEquiJug = "Venezuela";
                        db.Insert(t_ju);

                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 4;
                        t_ju.NombreJug = "Fanny";
                        t_ju.ApellidoJug = "Tineo";
                        t_ju.idEquipoJug = 1;
                        t_ju.NombreEquiJug = "Venezuela";
                        db.Insert(t_ju);

                        //
                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 4;
                        t_ju.NombreJug = "Manuel";
                        t_ju.ApellidoJug = "Dun";
                        t_ju.idEquipoJug = 2;
                        t_ju.NombreEquiJug = "Argentina";
                        db.Insert(t_ju);

                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 1;
                        t_ju.NombreJug = "Luis";
                        t_ju.ApellidoJug = "Nuñez";
                        t_ju.idEquipoJug = 2;
                        t_ju.NombreEquiJug = "Argentina";
                        db.Insert(t_ju);

                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 10;
                        t_ju.NombreJug = "Javier";
                        t_ju.ApellidoJug = "Silva";
                        t_ju.idEquipoJug = 2;
                        t_ju.NombreEquiJug = "Argentina";
                        db.Insert(t_ju);

                        // 2 Mexico
                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 22;
                        t_ju.NombreJug = "Fernando";
                        t_ju.ApellidoJug = "Pacheco";
                        t_ju.idEquipoJug = 2;
                        t_ju.NombreEquiJug = "Mexico";
                        db.Insert(t_ju);

                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 15;
                        t_ju.NombreJug = "Nelson";
                        t_ju.ApellidoJug = "Rodríguez";
                        t_ju.idEquipoJug = 2;
                        t_ju.NombreEquiJug = "Mexico";
                        db.Insert(t_ju);

                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 10;
                        t_ju.NombreJug = "Cesar";
                        t_ju.ApellidoJug = "Blasco";
                        t_ju.idEquipoJug = 2;
                        t_ju.NombreEquiJug = "Mexico";
                        db.Insert(t_ju);

                        // 1 Colombia
                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 3;
                        t_ju.NombreJug = "Luis";
                        t_ju.ApellidoJug = "Martínez";
                        t_ju.idEquipoJug = 1;
                        t_ju.NombreEquiJug = "Colombia";
                        db.Insert(t_ju);

                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 9;
                        t_ju.NombreJug = "Edgar";
                        t_ju.ApellidoJug = "Maita";
                        t_ju.idEquipoJug = 1;
                        t_ju.NombreEquiJug = "Colombia";
                        db.Insert(t_ju);

                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 2;
                        t_ju.NombreJug = "Virgilio";
                        t_ju.ApellidoJug = "La Rosa";
                        t_ju.idEquipoJug = 1;
                        t_ju.NombreEquiJug = "Colombia";
                        db.Insert(t_ju);

                    }
                    resultado = "Base de Datos y tabla 'Jugadores' creada";

                    db.CreateTable<TablaOcurrencias>(CreateFlags.None);
                    resultado = "Base de Datos y tabla Ocurrencias creada";

                    db.CreateTable<TablaPartidos>(CreateFlags.None); // Se crea la tabla 'Partidos'
                    if (db.Table<TablaPartidos>().Count() == 0)
                    {
                        TablaPartidos t_par = new TablaPartidos();
                        t_par.idEquipoA = 1;
                        t_par.idEquipoB = 2;
                        t_par.ComienzoPartido = false;
                        db.Insert(t_par);

                        t_par = new TablaPartidos();
                        t_par.idEquipoA = 2;
                        t_par.idEquipoB = 1;
                        t_par.ComienzoPartido = false;
                        db.Insert(t_par);
                    }
                    resultado = "Base de Datos y tabla 'Partidos' creada";

                    db.CreateTable<TablaResultadosParciales>(CreateFlags.None);
                    resultado = "Base de Datos y tabla 'Resultados Parciales' creada";
                }
            }
            catch (IOException ex)
            {
                resultado = string.Format("No se pudo crear la Base de Datos {0}", ex.Message);
            }
            return resultado;
        }

        public string ObtenerNombreDelegadoConectado()
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

            // Luego, con ese nombre de usuario que está conectado, se busca el nombre de ese delegado en la tabla "Delegados"
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {

                    var resultado = db.Table<TablaDelegados>();
                    IEnumerable<TablaDelegados> tabla_del = resultado.ToList<TablaDelegados>();
                    List<TablaDelegados> del = tabla_del.ToList<TablaDelegados>();

                    for (int i = 0; i < del.Count(); i++)
                    {
                        if (del[i].Username.Equals(nombreUsuario))
                            return del[i].Nombre;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
        }

        public bool Existe(string nombreUsuario, string clave)
        {
            bool ban = false;

            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado = db.Table<TablaDelegados>();
                    IEnumerable<TablaDelegados> tabla_del = resultado.ToList<TablaDelegados>();
                    List<TablaDelegados> del = tabla_del.ToList<TablaDelegados>();

                    for (int i = 0; i < del.Count(); i++)
                        if (del[i].Username.Equals(nombreUsuario) && (del[i].Password.Equals(clave)))
                        {
                            del[i].Conectado = true;
                            db.Update(del[i]);
                            ban = true;
                            break;
                            //return true;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Si ban es verdadero, se almacenan los datos en la tabla "Delgado Conectado" para, desde ahí, manejar la app con los valores de los campos de esa tabla
            if (ban)
            {
                try
                {
                    using (var db = new SQLiteConnection(directorioBD))
                    {
                        TablaDelegadoConectado tdc = new TablaDelegadoConectado();
                        tdc.Username = nombreUsuario;
                        tdc.Password = clave;
                        db.Insert(tdc);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return false;
        }

        public bool Iniciar(string nombreUsuario, string clave)
        {
            // Se comprueba si el delegado existe en la base de datos
            //string m = Existe(nombreUsuario, clave);
            if (Existe(nombreUsuario, clave))
                return true;
            return false;
            /*if (nombreUsuario == "prueba" && clave == "prueba")
            {
                sesionIniciada = true;
                return true;
            }
            else
            {
                sesionIniciada = false;
                return false;
            }*/
        }
    }
}