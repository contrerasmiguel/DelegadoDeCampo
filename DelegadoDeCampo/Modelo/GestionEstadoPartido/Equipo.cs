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

namespace DelegadoDeCampo.Modelo.GestionEstadoPartido
{
    class Equipo
    {
        private string nombre;
        private List<Jugadores> jugadores;

        public string Nombre
        {
            get
            {
                return nombre;
            }
        }

        public List<Jugadores> Jugadores
        {
            get
            {
                return jugadores;
            }
        }

        public Equipo(string n)
        {
            nombre = n;
            jugadores = new List<Jugadores>();
        }
    }
}