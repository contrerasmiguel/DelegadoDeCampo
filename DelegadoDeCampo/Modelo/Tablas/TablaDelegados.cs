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

using SQLite;

namespace DelegadoDeCampo.Modelo.GestionSesionUsuario
{
    class TablaDelegados
    {
        [MaxLength(25), PrimaryKey]
        public string Username { get; set; }
        [MaxLength(25)]
        public string Password { get; set; }
        [MaxLength(25)]
        public string Nombre { get; set; }
        [MaxLength(25)]
        public string Apellido { get; set; }
        public int idPartido { get; set; }
        public bool Conectado { get; set; }
    }
}