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

namespace DelegadoDeCampo.Modelo.GestionEstadoPartido
{
    class TablaPartidos
    {
        [PrimaryKey, AutoIncrement]
        public int idPartido { get; set; }
        public int idEquipoA { get; set; }
        public int idEquipoB { get; set; }
        public bool ComienzoPartido { get; set; }
    }
}