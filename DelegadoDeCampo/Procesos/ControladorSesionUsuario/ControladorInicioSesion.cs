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
            SetContentView(Resource.Layout.IuInicioSesion);
            sesionUsuario = new SesionUsuario();
            FindViewById<Button>(Resource.Id.btnLogin).Click += ControladorInicioSesion_Click;

            // Por si no existe la base de datos
            var docsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            directorioBD = System.IO.Path.Combine(docsFolder, "db_adonet.db");

            /*if (!File.Exists(directorioBD))
            {
                string cad = string.Empty;
                cad = sesionUsuario.CrearBaseDeDatos();
                Toast.MakeText(this, cad, ToastLength.Short).Show();
                cad = sesionUsuario.EjecutarConsulta("CREATE TABLE Delegados (Username VARCHAR(20) PRIMARY KEY NOT NULL, Password VARCHAR(20) NOT NULL, Nombre VARCHAR(20) NOT NULL, Apellido VARCHAR(20) NOT NULL, idPartido INTEGER NOT NULL, Conectado BOOLEAN NOT NULL, FOREIGN KEY(idPartido) REFERENCES Partido(idPartido))");
                Toast.MakeText(this, cad + " (Tabla Delegados)", ToastLength.Short).Show();
                cad = sesionUsuario.EjecutarConsulta("CREATE TABLE Partidos (idPartido INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, idEquipoA INTEGER NOT NULL, idEquipoB INTEGER NOT NULL, ComienzoPartido BOOLEAN NOT NULL, FOREIGN KEY(idEquipoA) REFERENCES EquipoA(idEquipoA), FOREIGN KEY(idEquipoB) REFERENCES Equipo(idEquipoB))");
                Toast.MakeText(this, cad + " (Tabla Partidos)", ToastLength.Short).Show();
                cad = sesionUsuario.EjecutarConsulta("CREATE TABLE EquipoA (idEquipoA INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Nombre VARCHAR(20) NOT NULL)");
                Toast.MakeText(this, cad + " (Tabla EquipoA)", ToastLength.Short).Show();
                cad = sesionUsuario.EjecutarConsulta("CREATE TABLE EquipoB (idEquipoB INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Nombre VARCHAR(20) NOT NULL)");
                Toast.MakeText(this, cad + " (Tabla EquipoB)", ToastLength.Short).Show();
                cad = sesionUsuario.EjecutarConsulta("CREATE TABLE JugadoresA (NumCamiseta INTEGER PRIMARY KEY NOT NULL, NombreJug VARCHAR(20) NOT NULL, ApellidoJug VARCHAR(20) NOT NULL, idEquipoA INTEGER NOT NULL, FOREIGN KEY(idEquipoA) REFERENCES EquipoA(idEquipoA))");
                Toast.MakeText(this, cad + " (Tabla JugadoresA)", ToastLength.Short).Show();
                cad = sesionUsuario.EjecutarConsulta("CREATE TABLE JugadoresB (NumCamiseta INTEGER PRIMARY KEY NOT NULL, NombreJug VARCHAR(20) NOT NULL, ApellidoJug VARCHAR(20) NOT NULL, idEquipoB INTEGER NOT NULL, FOREIGN KEY(idEquipoB) REFERENCES EquipoA(idEquipoB))");
                Toast.MakeText(this, cad + " (Tabla JugadoresB)", ToastLength.Short).Show();

                cad = sesionUsuario.EjecutarConsulta("INSERT INTO Delegados (Username, Password, Nombre, Apellido, idPartido, Conectado) VALUES ('juanchomar', '1234', Juan, Martinez, 2, 0)");
                Toast.MakeText(this, cad, ToastLength.Short).Show();
            }*/
            string cad = string.Empty;
            cad = sesionUsuario.CrearBaseDeDatos();
            Toast.MakeText(this, cad, ToastLength.Short).Show();
            //cad = sesionUsuario.EjecutarConsulta("CREATE TABLE Delegados (Username VARCHAR(20) PRIMARY KEY NOT NULL, Password VARCHAR(20) NOT NULL, Nombre VARCHAR(20) NOT NULL, Apellido VARCHAR(20) NOT NULL, idPartido INTEGER NOT NULL, Conectado BOOLEAN NOT NULL, FOREIGN KEY(idPartido) REFERENCES Partido(idPartido))");
            ////Toast.MakeText(this, cad + " (Tabla Delegados)", ToastLength.Short).Show();
            //cad = sesionUsuario.EjecutarConsulta("CREATE TABLE Partidos (idPartido INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, idEquipoA INTEGER NOT NULL, idEquipoB INTEGER NOT NULL, ComienzoPartido BOOLEAN NOT NULL, FOREIGN KEY(idEquipoA) REFERENCES EquipoA(idEquipoA), FOREIGN KEY(idEquipoB) REFERENCES Equipo(idEquipoB))");
            ////Toast.MakeText(this, cad + " (Tabla Partidos)", ToastLength.Short).Show();
            //cad = sesionUsuario.EjecutarConsulta("CREATE TABLE EquipoA (idEquipoA INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Nombre VARCHAR(20) NOT NULL)");
            ////Toast.MakeText(this, cad + " (Tabla EquipoA)", ToastLength.Short).Show();
            //cad = sesionUsuario.EjecutarConsulta("CREATE TABLE EquipoB (idEquipoB INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Nombre VARCHAR(20) NOT NULL)");
            ////Toast.MakeText(this, cad + " (Tabla EquipoB)", ToastLength.Short).Show();
            //cad = sesionUsuario.EjecutarConsulta("CREATE TABLE JugadoresA (NumCamiseta INTEGER PRIMARY KEY NOT NULL, NombreJug VARCHAR(20) NOT NULL, ApellidoJug VARCHAR(20) NOT NULL, idEquipoA INTEGER NOT NULL, FOREIGN KEY(idEquipoA) REFERENCES EquipoA(idEquipoA))");
            ////Toast.MakeText(this, cad + " (Tabla JugadoresA)", ToastLength.Short).Show();
            //cad = sesionUsuario.EjecutarConsulta("CREATE TABLE JugadoresB (NumCamiseta INTEGER PRIMARY KEY NOT NULL, NombreJug VARCHAR(20) NOT NULL, ApellidoJug VARCHAR(20) NOT NULL, idEquipoB INTEGER NOT NULL, FOREIGN KEY(idEquipoB) REFERENCES EquipoA(idEquipoB))");
            ////Toast.MakeText(this, cad + " (Tabla JugadoresB)", ToastLength.Short).Show();

            //cad = sesionUsuario.EjecutarConsulta("INSERT INTO Delegados (Username, Password, Nombre, Apellido, idPartido, Conectado) VALUES ('juanchomar', '1234', 'Juan', 'Martinez', 2, 0)");
            ////Toast.MakeText(this, cad, ToastLength.Short).Show();

        }

        private void ControladorInicioSesion_Click(object sender, EventArgs e)
        {
            string usuario = FindViewById<EditText>(Resource.Id.txtUserName).Text;
            string clave = FindViewById<EditText>(Resource.Id.txtPass).Text;

            //string m = sesionUsuario.Iniciar(usuario, clave);
            if (usuario.Count() == 0 || clave.Count() == 0 || !sesionUsuario.Iniciar(usuario, clave))
            {
                FindViewById<TextView>(Resource.Id.login_error).Text = "¡Credenciales inválidas!";
                //Toast.MakeText(this, m, ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "El delegado inició sesión.", ToastLength.Short).Show();
                var i = new Intent(this, typeof(ControladorInicioPartido));
                StartActivity(i);
                Finish();
            }
        }
    }
}