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
    class Jugador
    {
        private Equipo equipo;
        private string nombre, apellido;
        private ushort numero;

        public string Apellido
        {
            get
            {
                return apellido;
            }

            set
            {
                apellido = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public ushort Numero
        {
            get
            {
                return numero;
            }

            set
            {
                numero = value;
            }
        }

        internal Equipo Equipo
        {
            get
            {
                return equipo;
            }

            set
            {
                equipo = value;
            }
        }

        public Jugador(string nom, string a, ushort num)
        {
            nombre = nom;
            apellido = a;
            numero = num;
        }
    }
}