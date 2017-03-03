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

using DelegadoDeCampo.Modelo.GestionOcurrencias;
using DelegadoDeCampo.Modelo.GestionEstadoPartido;

namespace DelegadoDeCampo.Procesos.ControladorOcurrencias
{
    [Activity(Label = "DelegadoInicioPartido")]
    class ControladorInicioPartido : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IuInicioPartido);
            new ReporteOcurrencia().ReportarInicioPartido();
            FindViewById<Button>(Resource.Id.btnIniciarPartido).Click += ControladorInicioPartido_Click;
        }

        private void ControladorInicioPartido_Click(object sender, EventArgs e)
        {
            EstadoPartido.Inicializar(Application);
            var i = new Intent(this, typeof(ControladorEstadoPartido));
            StartActivity(i);
            Finish();
        }
    }
}