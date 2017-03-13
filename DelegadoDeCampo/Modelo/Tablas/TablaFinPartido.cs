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
    class TablaFinPartido
    {
        [PrimaryKey, AutoIncrement]
        public int idResultParcial { get; set; }

        // Equipo A
        public int GolesEquipoA { get; set; }
        [MaxLength(25)]
        public string MinutosGolesA { get; set; }
        [MaxLength(25)]
        public string CamisetasGolesA { get; set; }

        public int AmarillasEquipoA { get; set; }
        [MaxLength(25)]
        public string MinutosAmarillasA { get; set; }
        [MaxLength(25)]
        public string CamisetasAmarillasA { get; set; }

        public int RojasEquipoA { get; set; }
        [MaxLength(25)]
        public string MinutosRojasA { get; set; }
        [MaxLength(25)]
        public string CamisetasRojasA { get; set; }

        public int idEquipoA { get; set; }
        [MaxLength(25)]
        public string NombreEquipoA { get; set; }

        // Equipo B
        public int GolesEquipoB { get; set; }
        [MaxLength(25)]
        public string MinutosGolesB { get; set; }
        [MaxLength(25)]
        public string CamisetasGolesB { get; set; }

        public int AmarillasEquipoB { get; set; }
        [MaxLength(25)]
        public string MinutosAmarillasB { get; set; }
        [MaxLength(25)]
        public string CamisetasAmarillasB { get; set; }

        public int RojasEquipoB { get; set; }
        [MaxLength(25)]
        public string MinutosRojasB { get; set; }
        [MaxLength(25)]
        public string CamisetasRojasB { get; set; }

        public int idEquipoB { get; set; }
        [MaxLength(25)]
        public string NombreEquipoB { get; set; }

        public int idPartido { get; set; }
    }
}