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

namespace DelegadoDeCampo.Modelo.GestionOcurrencias
{
    class TiempoOcurrencia
    {
        public static int MINIMO = 0;
        public static int MAXIMO_DECENA = 12;
        public static int MAXIMO_UNIDAD = 9;

        private int minutoDecena;
        private int minutoUnidad;
        
        public int Minuto
        {
            get
            {
                return minutoDecena * 10 + minutoUnidad;
            }
        }

        public int MinutoDecena
        {
            get
            {
                return minutoDecena;
            }

            set
            {
                if (value >= MINIMO && value <= MAXIMO_DECENA)
                {
                    minutoDecena = value;
                }
            }
        }

        public int MinutoUnidad
        {
            get
            {
                return minutoUnidad;
            }

            set
            {
                if (value >= MINIMO && value <= MAXIMO_UNIDAD)
                {
                    minutoUnidad = value;
                }
            }
        }
    }
}