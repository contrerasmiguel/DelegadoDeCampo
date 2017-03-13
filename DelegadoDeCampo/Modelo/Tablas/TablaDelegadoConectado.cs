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

namespace DelegadoDeCampo.Modelo.Tablas
{
    class TablaDelegadoConectado
    {
        [MaxLength(25), PrimaryKey]
        public string Username { get; set; }
        [MaxLength(25)]
        public string Password { get; set; }
    }
}