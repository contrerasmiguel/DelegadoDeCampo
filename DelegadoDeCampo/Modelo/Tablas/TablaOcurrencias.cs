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
    class TablaOcurrencias
    {
        [PrimaryKey, AutoIncrement]
        public int idOcurrencia { get; set; }
        [MaxLength(25)]
        public string TipoDeOcurrencia { get; set; }
        public int NumCamisetaJug { get; set; }
        public int idEquipoJug { get; set; }
        [MaxLength(25)]
        public string NombreEquiJug { get; set; }
        public int MinutoOcu { get; set; }
    }
}