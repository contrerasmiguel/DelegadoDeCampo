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

using System.IO;

using DelegadoDeCampo.Modelo.GestionSesionUsuario;
using DelegadoDeCampo.Procesos.ControladorOcurrencias;

namespace DelegadoDeCampo.Procesos.ControladorSesionUsuario
{
    [Activity(Label = "Delegado de Campo", MainLauncher = true, Icon = "@drawable/delegado", Theme = "@android:style/Theme.NoTitleBar")]
    class ControladorInicioSesion : Activity
    {
        private SesionUsuario sesionUsuario;
        string directorioBD;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            sesionUsuario = SesionUsuario.Instancia;

            // Por si no existe la base de datos
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            directorioBD = System.IO.Path.Combine(docsFolder, "db_adonet.db");

            if (!File.Exists(directorioBD))
            {
                string cad = string.Empty;
                cad = sesionUsuario.CrearBaseDeDatos();
                Toast.MakeText(this, cad, ToastLength.Short)/*.Show()*/;
            }

            // De encontrarse que el usuario inició sesión previamente, saltar a la siguiente actividad.
            if (sesionUsuario.Conectado())
            {
                var intent = new Intent(this, typeof(ControladorInicioPartido));
                StartActivity(intent);
                Finish();
            }

            // Si el usuario no inició sesión, mostrarle vistas relacionadas al inicio de sesión.
            else
            {
                SetContentView(Resource.Layout.IuInicioSesion);
                FindViewById<Button>(Resource.Id.btnLogin).Click += ControladorInicioSesion_Click;
            }
        }

        private void ControladorInicioSesion_Click(object sender, EventArgs e)
        {
            string usuario = FindViewById<EditText>(Resource.Id.txtUserName).Text;
            string clave = FindViewById<EditText>(Resource.Id.txtPass).Text;

            //string m = sesionUsuario.Iniciar(usuario, clave);
            if (usuario.Count() == 0 || clave.Count() == 0 || !sesionUsuario.Iniciar(usuario, clave))
            {
                FindViewById<TextView>(Resource.Id.login_error).Text = "¡Credenciales inválidas!";
                //Toast.MakeText(this, m, ToastLength.Short)/*.Show()*/;
            }
            else
            {
                Toast.MakeText(this, "El delegado inició sesión.", ToastLength.Short)/*.Show()*/;
                var i = new Intent(this, typeof(ControladorInicioPartido));
                StartActivity(i);
                Finish();
            }
        }
    }
}