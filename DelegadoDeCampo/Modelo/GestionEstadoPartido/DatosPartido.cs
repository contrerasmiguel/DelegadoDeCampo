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

namespace DelegadoDeCampo.Modelo.GestionEstadoPartido
{
    class DatosPartido
    {
        private Equipo[] equipos;

        public Equipo[] Equipos
        {
            get
            {
                return equipos;
            }
        }

        public DatosPartido(Conexion c)
        {
            CargarDatosPartido(c);
        }

        private void CargarDatosPartido(Conexion c)
        {
            equipos = new Equipo[2];

            equipos[0] = new Equipo("Venezuela");
            equipos[1] = new Equipo("Colombia");

            List<Jugadores> jugadoresVenezuela = new List<Jugadores>()
            {
                  //new Jugadores("Pedro", "Johnson", 5),
                  //new Jugadores("Juan", "Marmol", 6),
                  //new Jugadores("José", "Diesel", 8),
                  //new Jugadores("Jesús", "González", 10),
                  //new Jugadores("Pablo", "Hill", 18),
                  //new Jugadores("Jeremías", "Rosberg", 11),
                  //new Jugadores("James", "Cuadrado", 4),
                  //new Jugadores("Phil", "Valles", 22),
                  //new Jugadores("Junior", "Villaroel", 1),
                  //new Jugadores("Tyron", "Villa", 17)
            };

            jugadoresVenezuela.ForEach(j => j.Equipo = equipos[0]);
            jugadoresVenezuela.ForEach(j => equipos[0].Jugadores.Add(j));

            List<Jugadores> jugadoresColombia = new List<Jugadores>()
            {
                  //new Jugadores("Carlos", "Diaz", 1),
                  //new Jugadores("Samuel", "López", 3),
                  //new Jugadores("Ernesto", "Ramírez", 5),
                  //new Jugadores("Alejandro", "Astudillo", 7),
                  //new Jugadores("William", "Manrique", 9),
                  //new Jugadores("Alfredo", "Graterol", 8),
                  //new Jugadores("Fernando", "Sánchez", 6),
                  //new Jugadores("Alberto", "Peña", 4),
                  //new Jugadores("Domingo", "Ruiz", 2),
                  //new Jugadores("Antonio", "Rivas", 12)
            };

            jugadoresColombia.ForEach(j => j.Equipo = equipos[1]);
            jugadoresColombia.ForEach(j => equipos[1].Jugadores.Add(j));

        }
    }
}