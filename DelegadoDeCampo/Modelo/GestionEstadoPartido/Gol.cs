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
    class Gol : Ocurrencia
    {
        private Jugador jugador;

        internal Jugador Jugador
        {
            get
            {
                return jugador;
            }

            set
            {
                jugador = value;
            }
        }
    }
}