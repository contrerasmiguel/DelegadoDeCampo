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

namespace DelegadoDeCampo.Modelo.GestionSesionUsuario
{
    class SesionUsuario
    {
        private bool sesionIniciada;

        public bool SesionIniciada
        {
            get
            {
                return sesionIniciada;
            }
        }

        public bool Iniciar(string nombreUsuario, string clave)
        {
            if (nombreUsuario == "prueba" && clave == "prueba")
            {
                sesionIniciada = false;
                return false;
            }
            else
            {
                sesionIniciada = true;
                return true;
            }
        }

        public void Cerrar()
        {
            sesionIniciada = false;
        }
    }
}