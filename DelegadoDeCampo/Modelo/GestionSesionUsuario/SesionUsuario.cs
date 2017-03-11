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
using DelegadoDeCampo.Modelo.GestionSesionUsuario;
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
                    db.CreateTable<TablaDelegados>(CreateFlags.None); // Se crea la tabla Estudiantes
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

                        /*est.Nombre = "Fanny";
                        est.Apellido = "Tineo";
                        db.Insert(est);
                        est = new Estudiante();
                        est.Nombre = "Luis";
                        est.Apellido = "Nuñez";
                        db.Insert(est);*/
                    }
                    db.CreateTable<TablaPartidos>(CreateFlags.None); // Se crea la tabla Estudiantes
                    if (db.Table<TablaPartidos>().Count() == 0)
                    {
                        TablaPartidos t_par = new TablaPartidos();
                        t_par.idEquipoA = 1;
                        t_par.idEquipoB = 2;
                        t_par.ComienzoPartido = false;
                        db.Insert(t_par);
                    }
                    db.CreateTable<TablaOcurrencias>(CreateFlags.None); // Se crea la tabla Estudiantes
                    resultado = "Base de Datos y tabla Ocurrencias creada";
                    /*db.CreateTable<Estudiante>(CreateFlags.None); // Se crea la tabla Estudiantes
                    if (db.Table<Estudiante>().Count() == 0)
                    {
                        Estudiante est = new Estudiante();
                        est.Nombre = "Fanny";
                        est.Apellido = "Tineo";
                        db.Insert(est);
                        est = new Estudiante();
                        est.Nombre = "Luis";
                        est.Apellido = "Nuñez";
                        db.Insert(est);
                    }
                    resultado = "Base de Datos creada";*/

                    db.CreateTable<TablaJugadores>(CreateFlags.None); // Se crea la tabla Estudiantes
                    if (db.Table<TablaJugadores>().Count() == 0)
                    {
                        TablaJugadores t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 3;
                        t_ju.NombreJug = "Jose";
                        t_ju.ApellidoJug = "Guevara";
                        t_ju.idEquipoJug = 1;
                        db.Insert(t_ju);

                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 5;
                        t_ju.NombreJug = "Miguel";
                        t_ju.ApellidoJug = "Contreras";
                        t_ju.idEquipoJug = 1;
                        db.Insert(t_ju);

                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 11;
                        t_ju.NombreJug = "Brian";
                        t_ju.ApellidoJug = "Johnson";
                        t_ju.idEquipoJug = 1;
                        db.Insert(t_ju);

                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 4;
                        t_ju.NombreJug = "Fanny";
                        t_ju.ApellidoJug = "Tineo";
                        t_ju.idEquipoJug = 1;
                        db.Insert(t_ju);

                        //
                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 4;
                        t_ju.NombreJug = "Manuel";
                        t_ju.ApellidoJug = "Dun";
                        t_ju.idEquipoJug = 2;
                        db.Insert(t_ju);

                        t_ju = new TablaJugadores();
                        t_ju.NumCamiseta = 1;
                        t_ju.NombreJug = "Luis";
                        t_ju.ApellidoJug = "Nuñez";
                        t_ju.idEquipoJug = 2;
                        db.Insert(t_ju);
                    }
                    resultado = "Base de Datos y tabla 'Jugadores' creada";

                    db.CreateTable<TablaResultadosParciales>(CreateFlags.None); // Se crea la tabla Estudiantes
                    resultado = "Base de Datos y tabla 'Resultados Parciales' creada";
                }
            }
            catch (IOException ex)
            {
                resultado = string.Format("No se pudo crear la Base de Datos {0}", ex.Message);
            }
            return resultado;

            /*string resultado;
            try
            {
                SqliteConnection.CreateFile(directorioBD);
                resultado = "Base de Datos creada";
            }
            catch (Exception ex)
            {
                resultado = string.Format("No se pudo crear la Base de Datos {0}", ex.Message);
            }
            return resultado;*/
        }

        /*public string EjecutarConsulta(string consulta)
        {
            string resultado = "";
            try
            {
                using (var conn = new SqliteConnection(strConexión))
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = consulta;
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery(); // Se ejecuta la consulta
                        resultado = "Comando ejecutado satisfactoriamente ";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = string.Format("Error al ejecutar la consulta = {0}", ex.Message);
            }
            return resultado;
        }*/

        public bool Existe(string nombreUsuario, string clave)
        {
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado = db.Table<TablaDelegados>();
                    IEnumerable<TablaDelegados> tabla_del = resultado.ToList<TablaDelegados>();
                    List<TablaDelegados> del = tabla_del.ToList<TablaDelegados>();

                    for (int i = 0; i < del.Count(); i++)
                    {
                        if (del[i].Username.Equals(nombreUsuario) && (del[i].Password.Equals(clave)))
                        {
                            del[i].Conectado = true;
                            db.Update(del[i]);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;

            /*string mensaje = string.Empty; ;
            SqliteConnection cnn = new SqliteConnection(strConexión);
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = cnn;
            //Le asignamos la consulta para eliminar el registro
            cmd.CommandText = "SELECT * FROM Delegados WHERE Username = '" + nombreUsuario + "' && Password = '" + clave + "';";

            cnn.Open();
            try
            {
                mensaje = cmd.ExecuteNonQuery().ToString();
            }
            catch (Exception ex)
            {
                mensaje = "Error a la hora de ejecutar la consulta.";
                //MessageBox.Show(ex.Message,”Error”, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cnn.Close();
            return mensaje;*/
        }

        /*public void ActualizarEstadoDelegado(string nombreUsuario, int estado)
        {
            Conectar();
            command = new SqliteCommand("UPDATE Delegados SET Conectado = @Conectado WHERE Username = " + nombreUsuario + ";", conex);
            command.Parameters.Add("Conectado", DbType.Boolean);
            command.Parameters["Conectado"].Value = estado;
            command.ExecuteNonQuery();
            Desconectar();


            /*SqliteConnection cnn = new SqliteConnection(directorioBD);
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = cnn;

            string sql = "UPDATE Delegados SET Conectado = @Conectado WHERE Username = @Username;";
            cmd.Parameters["Conectado"].Value = 1;
        }*/

        public bool Iniciar(string nombreUsuario, string clave)
        {
            // Se comprueba si el delegado existe en la base de datos
            //string m = Existe(nombreUsuario, clave);
            if (Existe(nombreUsuario, clave))
            {
                return true;
            }
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

        //public void Cerrar()
        //{
        //    try
        //    {
        //        Conectar(); //conectarse a la db
        //        SqliteDataReader respuesta = null;
        //        respuesta = Buscar("SELECT * FROM Delegados WHERE Conectado = 1"); //Cadena de consulta
        //        //Y eso es todo, respuesta contiene los resultados ;)                

        //        /*while (respuesta.Read()) // Ingresamos al While, si es que existe
        //        {
        //            // Dejamos el contador en 2 o mas (significa que encontramos elementos)                   
        //            nombre = respuesta[1].ToString();
        //            count++;
        //        }*/

        //        // Se verifica si la consulta trajo la tupla esperada
        //        if (respuesta.Read())
        //        {
        //            command = new SqliteCommand("UPDATE Delegados SET Conectado = @Conectado WHERE Conectado = 1;", conex);
        //            command.Parameters.Add("Conectado", DbType.Boolean);
        //            command.Parameters["Conectado"].Value = 0;
        //            command.ExecuteNonQuery();
        //        }
        //        //sesionIniciada = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show("Se produjo el siguiente error: lblpersona()\n" + ex.Message.ToString(), "Error de aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        //Controlamos los errores
        //    }
        //    finally
        //    {
        //        Desconectar(); //Desconectamos si o si de la db.
        //    }
        //}
    }
}