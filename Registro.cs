﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibreriaProyecto
{
    public partial class Registro : Form
    {
        Form1 loginPrevio;
        public Registro(Form1 loginPrevio)
        {
            InitializeComponent();
            this.loginPrevio = loginPrevio;
        }

        private void confirmadoRegistroBtn_Click(object sender, EventArgs e)
        {
            if (DatosValidos())
            {
                using (SqlConnection conexion = new SqlConnection(@"Data Source=OSCAR-VICTUS;Initial Catalog=LibrosDB;Integrated Security=True;"))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("CrearUsuario", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Nombre", usuarioInput.Text);
                        cmd.Parameters.AddWithValue("@Contraseña", contraseñaInput.Text);
                        cmd.Parameters.AddWithValue("@Teléfono", telefonoInput.Text);
                        cmd.Parameters.AddWithValue("@Correo", correoInput.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Usuario registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loginPrevio.Show();
                this.Dispose();
            }
        }

        bool DatosValidos()
        {
            if (usuarioInput.TextLength > 0 &&
                contraseñaInput.TextLength > 0 &&
                telefonoInput.TextLength > 0 &&
                correoInput.TextLength > 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Rellene todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void cancelarRegistroBtn_Click(object sender, EventArgs e)
        {
            loginPrevio.Show();
            this.Dispose();
        }
    }
}
