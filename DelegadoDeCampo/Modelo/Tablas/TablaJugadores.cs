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
    class TablaJugadores
    {
        [PrimaryKey, AutoIncrement]
        public int idJugador { get; set; }
        public int NumCamiseta { get; set; }
        [MaxLength(25)]
        public string NombreJug { get; set; }
        [MaxLength(25)]
        public string ApellidoJug { get; set; }
        public int idEquipoJug { get; set; }
        [MaxLength(25)]
        public string NombreEquiJug { get; set; }
    }
}