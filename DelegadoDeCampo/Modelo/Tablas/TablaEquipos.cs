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
    class TablaEquipos
    {
        [PrimaryKey, AutoIncrement]
        public int idEquipo { get; set; }
        [MaxLength(25)]
        public string Nombre { get; set; }
    }
}