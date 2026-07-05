using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Punto.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            frmPrincipal principal = new frmPrincipal();
            this.Hide();
            principal.Show();

            string usuario = txtUser.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("El usuario y la contraseña no pueden estar vacíos.");
                return;
            }

            try
            {
                Conexion conexion = new Conexion();

                using (MySqlConnection cn = conexion.ObtenerConexion())
                {
                    string sql = @"SELECT nombre_completo FROM usuarios WHERE username = @usuario AND password = @password";

                    using (MySqlCommand cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@password", password);

                        object resultado = cmd.ExecuteScalar();

                        if (resultado != null)
                        {
                            MessageBox.Show("Bienvenido " + resultado.ToString());

                            frmProductos productos = new frmProductos();
                            productos.Show();

                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }


        }
    }
}
