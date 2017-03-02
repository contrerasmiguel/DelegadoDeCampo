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

namespace DelegadoDeCampo.Modelo.ComunicacionRemota
{
    class Conexion
    {
        private bool conexionAbierta;

        public bool Abrir(string nombreUsuario, string clave)
        {
            conexionAbierta = true;
            return true;
        }

        public void Cerrar()
        {
            if (conexionAbierta)
            {
                conexionAbierta = false;
            }
        }

        public string EnviarSolicitud(string solicitud)
        {
            return "RESPUESTA SOLICITUD";
        }
    }
}