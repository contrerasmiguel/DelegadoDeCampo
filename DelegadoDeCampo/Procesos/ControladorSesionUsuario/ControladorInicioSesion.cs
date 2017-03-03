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

using DelegadoDeCampo.Modelo.GestionSesionUsuario;
using DelegadoDeCampo.Procesos.ControladorOcurrencias;

namespace DelegadoDeCampo.Procesos.ControladorSesionUsuario
{
    [Activity(Label = "DelegadoInicioSesion", MainLauncher = true, Icon = "@drawable/delegado")]
    class ControladorInicioSesion : Activity
    {
        private SesionUsuario sesionUsuario;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.IuInicioSesion);
            sesionUsuario = new SesionUsuario();
            FindViewById<Button>(Resource.Id.btnLogin).Click += ControladorInicioSesion_Click;
        }

        private void ControladorInicioSesion_Click(object sender, EventArgs e)
        {
            var usuario = FindViewById<EditText>(Resource.Id.txtUserName).Text;
            var clave = FindViewById<EditText>(Resource.Id.txtPass).Text;

            if (usuario.Count() == 0 || clave.Count() == 0 || !sesionUsuario.Iniciar(usuario, clave))
            {
                FindViewById<TextView>(Resource.Id.login_error).Text = "¡Credenciales inválidas!";
            }
            else
            {
                var i = new Intent(this, typeof(ControladorInicioPartido));
                StartActivity(i);
            }
        }
    }
}