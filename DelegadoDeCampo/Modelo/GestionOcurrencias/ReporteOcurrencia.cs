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
using System.Data;

using SQLite;
using DelegadoDeCampo.Modelo.GestionSesionUsuario;
using DelegadoDeCampo.Modelo.Tablas;

namespace DelegadoDeCampo.Modelo.GestionOcurrencias
{
    class ReporteOcurrencia
    {
        string directorioBD;
        string strConexión;

        int idEquipoA;
        int idEquipoB;

        /*string nombreEquipoA = string.Empty;
        string nombreEquipoB = string.Empty;*/

        // Equipo A
        int contGolesA;
        string MinutosGolesA = string.Empty;
        string CamisetasGolesA = string.Empty;

        int contAmarillasA;
        string MinutosAmarillasA = string.Empty;
        string CamisetasAmarillasA = string.Empty;

        int contRojasA;
        string MinutosRojasA = string.Empty;
        string CamisetasRojasA = string.Empty;

        // Equipo B
        int contGolesB;
        string MinutosGolesB = string.Empty;
        string CamisetasGolesB = string.Empty;

        int contAmarillasB;
        string MinutosAmarillasB = string.Empty;
        string CamisetasAmarillasB = string.Empty;

        int contRojasB;
        string MinutosRojasB = string.Empty;
        string CamisetasRojasB = string.Empty;

        public ReporteOcurrencia()
        {
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            directorioBD = System.IO.Path.Combine(docsFolder, "db_adonet.db");
            strConexión = string.Format("Data Source={0};Version=3;", directorioBD);
        }


        public int PuntuacionEquipoA()
        {
            int idPartido = ObtenerPartidoDelegado();
            int idEquipoA = ObtenerIdEquipoA(idPartido);
            string nombreA = string.Empty;

            int contA = int.MinValue;

            // Se obtiene el nombre del equipo
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                    var resultado1 = db.Table<TablaEquipoA>();
                    IEnumerable<TablaEquipoA> tabla_equiA = resultado1.ToList<TablaEquipoA>();
                    List<TablaEquipoA> equiA = tabla_equiA.ToList<TablaEquipoA>();

                    for (int i = 0; i < equiA.Count(); i++)
                        if (idEquipoA == equiA[i].idEquipoA)
                        {
                            nombreA = equiA[i].Nombre;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Se cuentan la cantidad de goles que lleva ese equipo
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado2 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado2.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    contA = 0;
                    for (int i = 0; i < ocu.Count(); i++)
                        if ((ocu[i].NombreEquiJug.Equals(nombreA)) && (ocu[i].TipoDeOcurrencia.Equals("Gol")))
                            contA++;
                    //if (i == ocu.Count())
                    //    mensaje += "Se obtuvieron los goles de Equipo A.";
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //mensaje += "Error al obtener los goles de Equipo A.";
            }
            return contA;
        }

        public string ObtenerNombreEquipoA()
        {
            int idPartido = ObtenerPartidoDelegado();
            int idEquipoA = ObtenerIdEquipoA(idPartido);

            // Se obtiene el nombre del equipo
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                    var resultado1 = db.Table<TablaEquipoA>();
                    IEnumerable<TablaEquipoA> tabla_equiA = resultado1.ToList<TablaEquipoA>();
                    List<TablaEquipoA> equiA = tabla_equiA.ToList<TablaEquipoA>();

                    for (int i = 0; i < equiA.Count(); i++)
                        if (idEquipoA == equiA[i].idEquipoA)
                            return equiA[i].Nombre;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
        }


        public string ObtenerNombreEquipoB()
        {
            int idPartido = ObtenerPartidoDelegado();
            int idEquipoB = ObtenerIdEquipoB(idPartido);

            // Se obtiene el nombre del equipo
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                    var resultado1 = db.Table<TablaEquipoB>();
                    IEnumerable<TablaEquipoB> tabla_equiB = resultado1.ToList<TablaEquipoB>();
                    List<TablaEquipoB> equiB = tabla_equiB.ToList<TablaEquipoB>();

                    for (int i = 0; i < equiB.Count(); i++)
                        if (idEquipoB == equiB[i].idEquipoB)
                            return equiB[i].Nombre;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
        }


        public int PuntuacionEquipoB()
        {
            int idPartido = ObtenerPartidoDelegado();
            int idEquipoB = ObtenerIdEquipoB(idPartido);
            string nombreB = string.Empty;

            int contB = int.MinValue;

            // Se obtiene el nombre del equipo
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                    var resultado1 = db.Table<TablaEquipoB>();
                    IEnumerable<TablaEquipoB> tabla_equiB = resultado1.ToList<TablaEquipoB>();
                    List<TablaEquipoB> equiB = tabla_equiB.ToList<TablaEquipoB>();

                    for (int i = 0; i < equiB.Count(); i++)
                        if (idEquipoB == equiB[i].idEquipoB)
                        {
                            nombreB = equiB[i].Nombre;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Se cuentan la cantidad de goles que lleva ese equipo
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado2 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado2.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    contB = 0;
                    for (int i = 0; i < ocu.Count(); i++)
                        if ((ocu[i].NombreEquiJug.Equals(nombreB)) && (ocu[i].TipoDeOcurrencia.Equals("Gol")))
                            contB++;
                    //if (i == ocu.Count())
                    //    mensaje += "Se obtuvieron los goles de Equipo A.";
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //mensaje += "Error al obtener los goles de Equipo A.";
            }
            return contB;
        }


        public int ObtenerPartidoDelegado()
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

            // Luego, con ese nombre de usuario, busco el id del partido al que fue asignado y lo retorno
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
                            return del[i].idPartido;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return int.MinValue;
        }

        public bool ReportarInicioPartido()
        {
            // Se obtiene el id del partido asignado al delegado conectado
            int id = ObtenerPartidoDelegado();

            if (id != int.MinValue)
            {
                try
                {
                    using (var db = new SQLiteConnection(directorioBD))
                    {
                        // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                        var resultado1 = db.Table<TablaPartidos>();
                        IEnumerable<TablaPartidos> tabla_par = resultado1.ToList<TablaPartidos>();
                        List<TablaPartidos> par = tabla_par.ToList<TablaPartidos>();

                        for (int j = 0; j < par.Count(); j++)
                            if (par[j].idPartido == id)
                            {
                                par[j].ComienzoPartido = true;
                                db.Update(par[j]);
                                return true;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return false;
        }


        public string InformacionMomentaneaA(int id, string ocurrencia)
        {
            string mensaje = "No se almacenó la información parcial.";
            string nombreA = string.Empty;

            // Se obtiene el nombre del equipo A
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado1 = db.Table<TablaEquipoA>();
                    IEnumerable<TablaEquipoA> tabla_equiA = resultado1.ToList<TablaEquipoA>();
                    List<TablaEquipoA> equiA = tabla_equiA.ToList<TablaEquipoA>();

                    for (int i = 0; i < equiA.Count(); i++)
                        if (id == equiA[i].idEquipoA)
                        {
                            nombreA = equiA[i].Nombre;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Almaceno toda la información de esa ocurrencia en la tabla "Ocurrencias"
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado1 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado1.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    TablaOcurrencias to = new TablaOcurrencias();
                    to.TipoDeOcurrencia = ocurrencia;
                    to.NumCamisetaJug = 0;
                    to.idEquipoJug = id;
                    to.NombreEquiJug = nombreA;
                    to.MinutoOcu = 0;
                    db.Insert(to);
                    mensaje = "Se almacenó la información momentánea A.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mensaje;
        }


        public string InformacionMomentaneaB(int id, string ocurrencia)
        {
            string mensaje = "No se almacenó la información parcial A.";
            string nombreB = string.Empty;

            // Se obtiene el nombre del Equipo B
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado1 = db.Table<TablaEquipoB>();
                    IEnumerable<TablaEquipoB> tabla_equiB = resultado1.ToList<TablaEquipoB>();
                    List<TablaEquipoB> equiB = tabla_equiB.ToList<TablaEquipoB>();

                    for (int i = 0; i < equiB.Count(); i++)
                        if (id == equiB[i].idEquipoB)
                        {
                            nombreB = equiB[i].Nombre;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Almaceno toda la información de esa ocurrencia en la tabla "Ocurrencias"
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado1 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado1.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    TablaOcurrencias to = new TablaOcurrencias();
                    to.TipoDeOcurrencia = ocurrencia;
                    to.NumCamisetaJug = 0;
                    to.idEquipoJug = id;
                    to.NombreEquiJug = nombreB;
                    to.MinutoOcu = 0;
                    db.Insert(to);
                    mensaje = "Se almacenó la información momentánea B.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mensaje;
        }


        public int ObtenerIdEquipoA(int idPartido)
        {
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                    var resultado1 = db.Table<TablaPartidos>();
                    IEnumerable<TablaPartidos> tabla_par = resultado1.ToList<TablaPartidos>();
                    List<TablaPartidos> par = tabla_par.ToList<TablaPartidos>();

                    for (int j = 0; j < par.Count(); j++)
                        if (par[j].idPartido == idPartido)
                            return par[j].idEquipoA;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return int.MinValue;
        }


        public int ObtenerIdEquipoB(int idPartido)
        {
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado1 = db.Table<TablaPartidos>();
                    IEnumerable<TablaPartidos> tabla_par = resultado1.ToList<TablaPartidos>();
                    List<TablaPartidos> par = tabla_par.ToList<TablaPartidos>();

                    for (int j = 0; j < par.Count(); j++)
                        if (par[j].idPartido == idPartido)
                            return par[j].idEquipoB;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return int.MinValue;
        }


        public int ObtenerIdEquipo()
        {
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                    var resultado1 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado1.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    return ocu[(ocu.Count() - 1)].idEquipoJug;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return int.MinValue;
        }


        public string ObtenerUltimoNombreEquipoAlmacenado()
        {
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                    var resultado1 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado1.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    return ocu[(ocu.Count() - 1)].NombreEquiJug;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
        }


        public void ReportarGol(int idJugador, int idEquipo, int minuto)
        {

        }

        public void ReportarAmarilla(int idJugador, int idEquipo, int minuto)
        {

        }

        public void ReportarRoja(int idJugador, int idEquipo, int minuto)
        {

        }


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


        public string ReportarFinalizacionPartido()
        {
            string mensaje = "Hay problemas XS";
            TablaFinPartido tfp = new TablaFinPartido();

            // Se accesa a la tabla "Resultados Parciales" y se obtiene la última tupla de esa tabla y se almacena en la tabla "Fin de Partido"
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado = db.Table<TablaResultadosParciales>();
                    IEnumerable<TablaResultadosParciales> tabla_resPar = resultado.ToList<TablaResultadosParciales>();
                    List<TablaResultadosParciales> resPar = tabla_resPar.ToList<TablaResultadosParciales>();

                    // EQUIPO A
                    tfp.GolesEquipoA = resPar[(resPar.Count() - 1)].GolesEquipoA;
                    tfp.MinutosGolesA = resPar[(resPar.Count() - 1)].MinutosGolesA;
                    tfp.CamisetasGolesA = resPar[(resPar.Count() - 1)].CamisetasGolesA;

                    tfp.AmarillasEquipoA = resPar[(resPar.Count() - 1)].AmarillasEquipoA;
                    tfp.MinutosAmarillasA = resPar[(resPar.Count() - 1)].MinutosAmarillasA;
                    tfp.CamisetasAmarillasA = resPar[(resPar.Count() - 1)].CamisetasAmarillasA;

                    tfp.RojasEquipoA = resPar[(resPar.Count() - 1)].RojasEquipoA;
                    tfp.MinutosRojasA = resPar[(resPar.Count() - 1)].MinutosRojasA;
                    tfp.CamisetasRojasA = resPar[(resPar.Count() - 1)].CamisetasRojasA;

                    tfp.idEquipoA = resPar[(resPar.Count() - 1)].idEquipoA;
                    tfp.NombreEquipoA = resPar[(resPar.Count() - 1)].NombreEquipoA;

                    // EQUIPO B
                    tfp.GolesEquipoB = resPar[(resPar.Count() - 1)].GolesEquipoB;
                    tfp.MinutosGolesB = resPar[(resPar.Count() - 1)].MinutosGolesB;
                    tfp.CamisetasGolesB = resPar[(resPar.Count() - 1)].CamisetasGolesB;

                    tfp.AmarillasEquipoB = resPar[(resPar.Count() - 1)].AmarillasEquipoB;
                    tfp.MinutosAmarillasB = resPar[(resPar.Count() - 1)].MinutosAmarillasB;
                    tfp.CamisetasAmarillasB = resPar[(resPar.Count() - 1)].CamisetasAmarillasB;

                    tfp.RojasEquipoB = resPar[(resPar.Count() - 1)].RojasEquipoB;
                    tfp.MinutosRojasB = resPar[(resPar.Count() - 1)].MinutosRojasB;
                    tfp.CamisetasRojasB = resPar[(resPar.Count() - 1)].CamisetasRojasB;

                    tfp.idEquipoB = resPar[(resPar.Count() - 1)].idEquipoB;
                    tfp.NombreEquipoB = resPar[(resPar.Count() - 1)].NombreEquipoB;

                    //mensaje = "Se almacenó la tupla en la tabla 'Fin del Partido'";
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
                    var resultado = db.Table<TablaFinPartido>();
                    IEnumerable<TablaFinPartido> tabla_finPar = resultado.ToList<TablaFinPartido>();
                    List<TablaFinPartido> finPar = tabla_finPar.ToList<TablaFinPartido>();

                    db.Insert(tfp);
                    mensaje = "Se almacenó la tupla en la tabla 'Fin del Partido'";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mensaje;
        }


        public string EnviarResultadosParciales()
        {
            string mensaje = string.Empty;
            // Obtengo el id del partido del delegado conectado
            int idPartido = ObtenerPartidoDelegado();

            idEquipoA = ObtenerIdEquipoA(idPartido);
            idEquipoB = ObtenerIdEquipoB(idPartido);

            string nombreEquipoA = string.Empty;
            string nombreEquipoB = string.Empty;

            // Obtengo los nombres de los equipos asignados a ese delegado
            // Nombre del Equipo A
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                    var resultado = db.Table<TablaEquipoA>();
                    IEnumerable<TablaEquipoA> tabla_equiA = resultado.ToList <TablaEquipoA>();
                    List<TablaEquipoA> equiA = tabla_equiA.ToList<TablaEquipoA>();

                    for (int i = 0; i < equiA.Count(); i++)
                        if (equiA[i].idEquipoA == idEquipoA)
                        {
                            nombreEquipoA = equiA[i].Nombre;
                            //mensaje = "Equipo A = "+ nombreEquipoA;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //mensaje = "No se obtuvo el nombre del del Equipo A.";
            }

            // Nombre del Equipo B
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                    var resultado1 = db.Table<TablaEquipoB>();
                    IEnumerable<TablaEquipoB> tabla_equiB = resultado1.ToList<TablaEquipoB>();
                    List<TablaEquipoB> equiB = tabla_equiB.ToList<TablaEquipoB>();

                    for (int i = 0; i < equiB.Count(); i++)
                        if (equiB[i].idEquipoB == idEquipoB)
                        {
                            nombreEquipoB = equiB[i].Nombre;
                            //mensaje += " Equipo B = " + nombreEquipoB;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //mensaje += " No se obtuvo el nombre del del Equipo B.";
            }

            // EQUIPO A
            // Se obtienen la cantidad de goles, los minutos de esos goles y el número de camiseta de los goleadores del Equipo A
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado2 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado2.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    contGolesA = 0;
                    int i;
                    for (i = 0; i < ocu.Count(); i++)
                        if ((ocu[i].NombreEquiJug.Equals(nombreEquipoA)) && (ocu[i].TipoDeOcurrencia.Equals("Gol")))
                        {
                            contGolesA++;
                            MinutosGolesA += ocu[i].MinutoOcu.ToString() + " ";
                            CamisetasGolesA += ocu[i].NumCamisetaJug.ToString() + " ";
                        }
                    //if (i == ocu.Count())
                    //    mensaje += "Se obtuvieron los goles de Equipo A.";
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //mensaje += "Error al obtener los goles de Equipo A.";
            }
            // Se obtienen la cantidad de amarillas, los minutos de esas amarillas y el número de camiseta de los jugadores del Equipo A
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    contAmarillasA = 0;
                    int i;
                    for (i = 0; i < ocu.Count(); i++)
                        if ((ocu[i].NombreEquiJug.Equals(nombreEquipoA)) && (ocu[i].TipoDeOcurrencia.Equals("Amarilla")))
                        {
                            contAmarillasA++;
                            MinutosAmarillasA += ocu[i].MinutoOcu.ToString() + " ";
                            CamisetasAmarillasA += ocu[i].NumCamisetaJug.ToString() + " ";
                        }
                    //if (i == ocu.Count())
                    //    mensaje += "Se obtuvieron las amarillas de Equipo A.";
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //mensaje += " Error al obtener las amarillas de Equipo A.";
            }
            // Se obtienen la cantidad de rojas, los minutos de esas rojas y el número de camiseta de los jugadores del Equipo A
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado2 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado2.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    contRojasA = 0;
                    int i;
                    for (i = 0; i < ocu.Count(); i++)
                        if ((ocu[i].NombreEquiJug.Equals(nombreEquipoA)) && (ocu[i].TipoDeOcurrencia.Equals("Roja")))
                        {
                            contRojasA++;
                            MinutosRojasA += ocu[i].MinutoOcu.ToString() + " ";
                            CamisetasRojasA += ocu[i].NumCamisetaJug.ToString() + " ";
                        }
                    //if (i == ocu.Count())
                    //    mensaje += "Se obtuvieron las rojas de Equipo A.";
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //mensaje += " Error al obtener las rojas de Equipo A.";
            }

            // EQUIPO B
            // Se obtienen la cantidad de goles, los minutos de esos goles y el número de camiseta de los goleadores del Equipo A
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado2 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado2.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    contGolesB = 0;
                    int i;
                    for (i = 0; i < ocu.Count(); i++)
                        if ((ocu[i].NombreEquiJug.Equals(nombreEquipoB)) && (ocu[i].TipoDeOcurrencia.Equals("Gol")))
                        {
                            contGolesB++;
                            MinutosGolesB += ocu[i].MinutoOcu.ToString() + " ";
                            CamisetasGolesB += ocu[i].NumCamisetaJug.ToString() + " ";
                        }
                    //if (i == ocu.Count())
                    //    mensaje += "Se obtuvieron los goles de Equipo B.";
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //mensaje += " Error al obtener los goles de Equipo B.";
            }
            // Se obtienen la cantidad de amarillas, los minutos de esas amarillas y el número de camiseta de los jugadores del Equipo A
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado2 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado2.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    contAmarillasB = 0;
                    int i;
                    for (i = 0; i < ocu.Count(); i++)
                        if ((ocu[i].NombreEquiJug.Equals(nombreEquipoB)) && (ocu[i].TipoDeOcurrencia.Equals("Amarilla")))
                        {
                            contAmarillasB++;
                            MinutosAmarillasB += ocu[i].MinutoOcu.ToString() + " ";
                            CamisetasAmarillasB += ocu[i].NumCamisetaJug.ToString() + " ";
                        }
                    //if (i == ocu.Count())
                    //    mensaje += "Se obtuvieron los amarillas de Equipo B.";
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //mensaje += " Error al obtener las amarillas de Equipo B.";
            }
            // Se obtienen la cantidad de tojas, los minutos de esas rojas y el número de camiseta de los jugadores del Equipo A
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    var resultado2 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado2.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    contRojasB = 0;
                    int i;
                    for (i = 0; i < ocu.Count(); i++)
                        if ((ocu[i].NombreEquiJug.Equals(nombreEquipoB)) && (ocu[i].TipoDeOcurrencia.Equals("Roja")))
                        {
                            contRojasB++;
                            MinutosRojasB += ocu[i].MinutoOcu.ToString() + " ";
                            CamisetasRojasB += ocu[i].NumCamisetaJug.ToString() + " ";
                        }
                    //if (i == ocu.Count())
                    //    mensaje += "Se obtuvieron las rojas de Equipo B.";
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //mensaje += " Error al obtener las rojas de Equipo B.";
            }

            // Se almacenan los valores en la tabla "Resultados Parciales"
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    TablaResultadosParciales trp = new TablaResultadosParciales();

                    // Equipo A
                    trp.GolesEquipoA = this.contGolesA;
                    trp.MinutosGolesA = this.MinutosGolesA;
                    trp.CamisetasGolesA = this.CamisetasGolesA;

                    trp.AmarillasEquipoA = this.contAmarillasA;
                    trp.MinutosAmarillasA = this.MinutosAmarillasA;
                    trp.CamisetasAmarillasA = this.CamisetasAmarillasA;

                    trp.RojasEquipoA = this.contRojasA;
                    trp.MinutosRojasA = this.MinutosRojasA;
                    trp.CamisetasRojasA = this.CamisetasRojasA;

                    trp.idEquipoA = this.idEquipoA;
                    trp.NombreEquipoA = nombreEquipoA;

                    // Equipo B
                    trp.GolesEquipoB = this.contGolesB;
                    trp.MinutosGolesB = this.MinutosGolesB;
                    trp.CamisetasGolesB = this.CamisetasGolesB;

                    trp.AmarillasEquipoB = this.contAmarillasB;
                    trp.MinutosAmarillasB = this.MinutosAmarillasB;
                    trp.CamisetasAmarillasB = this.CamisetasAmarillasB;

                    trp.RojasEquipoB = this.contRojasB;
                    trp.MinutosRojasB = this.MinutosRojasB;
                    trp.CamisetasRojasB = this.CamisetasRojasB;

                    trp.idEquipoB = this.idEquipoB;
                    trp.NombreEquipoB = nombreEquipoB;

                    db.Insert(trp);
                    mensaje += " Se insertó el resultado parcial";
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                mensaje += " No se insertó el resultado parcial";
            }
            return mensaje;
        }
    }
}