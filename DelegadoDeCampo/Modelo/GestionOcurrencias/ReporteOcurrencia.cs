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

        public ReporteOcurrencia()
        {
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            directorioBD = System.IO.Path.Combine(docsFolder, "db_adonet.db");
            strConexión = string.Format("Data Source={0};Version=3;", directorioBD);
        }

        public int ObtenerPartidoDelegado()
        {
            int idPartidoDel = int.MinValue;
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
                        if (del[i].Conectado)
                        {
                            idPartidoDel = del[i].idPartido;
                            return idPartidoDel;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return idPartidoDel;
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
                        {
                            if (par[j].idPartido == id)
                            {
                                par[j].ComienzoPartido = true;
                                db.Update(par[j]);
                                return true;
                            }
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

        public string InformacionParcial(int id, string ocurrencia)
        {
            string mensaje = "No se almacenó la información parcial.";
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                    var resultado1 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado1.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    TablaOcurrencias to = new TablaOcurrencias();
                    to.idEquipoJug = id;
                    to.TipoDeOcurrencia = ocurrencia;
                    to.NumCamisetaJug = 0;
                    to.MinutoOcu = 0;
                    db.Insert(to);
                    mensaje = "Se almacenó la información parcial.";
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
                    {
                        if (par[j].idPartido == idPartido)
                        {
                            return par[j].idEquipoA;
                        }
                    }
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
                    {
                        if (par[j].idPartido == idPartido)
                            return par[j].idEquipoB;
                    }
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


        public void ReportarGol(int idJugador, int idEquipo, int minuto)
        {

        }

        public void ReportarAmarilla(int idJugador, int idEquipo, int minuto)
        {

        }

        public void ReportarRoja(int idJugador, int idEquipo, int minuto)
        {

        }

        public void ReportarFinalizacionPartido(int minuto)
        {
        }

        public void EnviarResultadosParciales()
        {
            // Obtengo el id del partido del delegado conectado
            int idPartido = ObtenerPartidoDelegado();

            int idEquipoA = ObtenerIdEquipoA(idPartido);
            int idEquipoB = ObtenerIdEquipoB(ObtenerPartidoDelegado());

            string nombreEquipoA = string.Empty;
            string nombreEquipoB = string.Empty;

            int contGolesA;
            string MinutosGolesA = string.Empty;
            string CamisetasGolesA = string.Empty;

            int contGolesB;
            string MinutosGolesB = string.Empty;
            string CamisetasGolesB = string.Empty;

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
                            nombreEquipoA = equiA[i].Nombre;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                            nombreEquipoB = equiB[i].Nombre;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Se hace una comparación en la tabla Ocurrencias para saber la cantidad de goles que ha hecho el Equipo A
            try
            {
                using (var db = new SQLiteConnection(directorioBD))
                {
                    // Luego, con id, se accesa a la tabla Partidos y se cambia el campo 'ComienzoPartido' a verdadero
                    var resultado2 = db.Table<TablaOcurrencias>();
                    IEnumerable<TablaOcurrencias> tabla_ocu = resultado2.ToList<TablaOcurrencias>();
                    List<TablaOcurrencias> ocu = tabla_ocu.ToList<TablaOcurrencias>();

                    contGolesA = 0;
                    for (int i = 0; i < ocu.Count(); i++)
                        if ((ocu[i].NombreEquiJug == nombreEquipoA) && (ocu[i].TipoDeOcurrencia.Equals("Gol")))
                        {
                            contGolesA++;
                            MinutosGolesA += ocu[i].MinutoOcu.ToString();
                            CamisetasGolesA += ocu[i].NumCamisetaJug.ToString();
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}