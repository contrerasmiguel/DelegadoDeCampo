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

            List<Jugador> jugadoresVenezuela = new List<Jugador>()
            {
                  new Jugador("Pedro", "Johnson", 5),
                  new Jugador("Juan", "Marmol", 6),
                  new Jugador("José", "Diesel", 8),
                  new Jugador("Jesús", "González", 10),
                  new Jugador("Pablo", "Hill", 18),
                  new Jugador("Jeremías", "Rosberg", 11),
                  new Jugador("James", "Cuadrado", 4),
                  new Jugador("Phil", "Valles", 22),
                  new Jugador("Junior", "Villaroel", 1),
                  new Jugador("Tyron", "Villa", 17)
            };

            jugadoresVenezuela.ForEach(j => j.Equipo = equipos[0]);
            jugadoresVenezuela.ForEach(j => equipos[0].Jugadores.Add(j));

            List<Jugador> jugadoresColombia = new List<Jugador>()
            {
                  new Jugador("Carlos", "Diaz", 1),
                  new Jugador("Samuel", "López", 3),
                  new Jugador("Ernesto", "Ramírez", 5),
                  new Jugador("Alejandro", "Astudillo", 7),
                  new Jugador("William", "Manrique", 9),
                  new Jugador("Alfredo", "Graterol", 8),
                  new Jugador("Fernando", "Sánchez", 6),
                  new Jugador("Alberto", "Peña", 4),
                  new Jugador("Domingo", "Ruiz", 2),
                  new Jugador("Antonio", "Rivas", 12)
            };

            jugadoresColombia.ForEach(j => j.Equipo = equipos[1]);
            jugadoresColombia.ForEach(j => equipos[1].Jugadores.Add(j));

        }
    }
}