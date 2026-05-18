using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CRUDASPDocente6133261
{
    public partial class Default : System.Web.UI.Page
    {
        string rutaDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\AcessDB\IndelCs6133261.accdb";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpiarCampos();
                MostrarTodosLosEstudiantes();
            }
        }

        private OleDbConnection ConectarDB()
        {
            return new OleDbConnection(rutaDB);
        }

        private void MostrarTodosLosEstudiantes()
        {
            try
            {
                using (OleDbConnection conn = ConectarDB())
                {
                    conn.Open();
                    string query = "SELECT * FROM Estudiantes ORDER BY Id";
                    OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvEstudiantes.DataSource = dt;
                    gvEstudiantes.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al mostrar datos: " + ex.Message;
            }
        }

        private void BuscarEstudiantePorId(int id)
        {
            try
            {
                using (OleDbConnection conn = ConectarDB())
                {
                    conn.Open();
                    string query = "SELECT * FROM Estudiantes WHERE Id = @Id";
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    OleDbDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtNombre.Text = reader["Nombre"].ToString();

                        string genero = reader["Genero"].ToString();
                        if (genero == "Masculino")
                            rbMasculino.Checked = true;
                        else if (genero == "Femenino")
                            rbFemenino.Checked = true;

                        txtEmail.Text = reader["Email"].ToString();
                        txtCiudad.Text = reader["Ciudad"].ToString();
                        lblMensaje.Text = "";
                    }
                    else
                    {
                        lblMensaje.Text = "No se encontró ningún estudiante con ID: " + id;
                        LimpiarCampos();
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al buscar: " + ex.Message;
            }
        }

        private void LimpiarCampos()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            rbMasculino.Checked = false;
            rbFemenino.Checked = false;
            txtEmail.Text = "";
            txtCiudad.Text = "";
            lblMensaje.Text = "";
        }

        private string ObtenerGeneroSeleccionado()
        {
            if (rbMasculino.Checked)
                return "Masculino";
            else if (rbFemenino.Checked)
                return "Femenino";
            else
                return "";
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            txtId.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                int id;
                if (int.TryParse(txtId.Text, out id))
                {
                    BuscarEstudiantePorId(id);
                }
                else
                {
                    lblMensaje.Text = "Por favor ingrese un ID válido";
                }
            }
            else
            {
                lblMensaje.Text = "Por favor ingrese un ID para buscar";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtCiudad.Text) || string.IsNullOrEmpty(ObtenerGeneroSeleccionado()))
            {
                lblMensaje.Text = "Por favor complete todos los campos";
                return;
            }

            try
            {
                using (OleDbConnection conn = ConectarDB())
                {
                    conn.Open();
                    string query = "INSERT INTO Estudiantes (Nombre, Genero, Email, Ciudad) VALUES (@Nombre, @Genero, @Email, @Ciudad)";
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Genero", ObtenerGeneroSeleccionado());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);

                    int resultado = cmd.ExecuteNonQuery();

                    if (resultado > 0)
                    {
                        lblMensaje.Text = "Estudiante guardado correctamente";
                        LimpiarCampos();
                        MostrarTodosLosEstudiantes();
                    }
                    else
                    {
                        lblMensaje.Text = "Error al guardar el estudiante";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar: " + ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                lblMensaje.Text = "Por favor busque un estudiante primero para actualizar";
                return;
            }

            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtEmail.Text) ||
                string.IsNullOrEmpty(txtCiudad.Text) || string.IsNullOrEmpty(ObtenerGeneroSeleccionado()))
            {
                lblMensaje.Text = "Por favor complete todos los campos";
                return;
            }

            try
            {
                using (OleDbConnection conn = ConectarDB())
                {
                    conn.Open();
                    string query = "UPDATE Estudiantes SET Nombre = @Nombre, Genero = @Genero, Email = @Email, Ciudad = @Ciudad WHERE Id = @Id";
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Genero", ObtenerGeneroSeleccionado());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(txtId.Text));

                    int resultado = cmd.ExecuteNonQuery();

                    if (resultado > 0)
                    {
                        lblMensaje.Text = "Estudiante actualizado correctamente";
                        LimpiarCampos();
                        MostrarTodosLosEstudiantes();
                    }
                    else
                    {
                        lblMensaje.Text = "No se encontró el estudiante para actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al actualizar: " + ex.Message;
            }
        }

        // Botón Delete - Eliminar estudiante
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                lblMensaje.Text = "Por favor ingrese el ID del estudiante a eliminar";
                return;
            }

            try
            {
                using (OleDbConnection conn = ConectarDB())
                {
                    conn.Open();
                    string query = "DELETE FROM Estudiantes WHERE Id = @Id";
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(txtId.Text));

                    int resultado = cmd.ExecuteNonQuery();

                    if (resultado > 0)
                    {
                        lblMensaje.Text = "Estudiante eliminado correctamente";
                        LimpiarCampos();
                        MostrarTodosLosEstudiantes();
                    }
                    else
                    {
                        lblMensaje.Text = "No se encontró el estudiante con ID: " + txtId.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar: " + ex.Message;
            }
        }

        // Botón Show - Mostrar todos los estudiantes
        protected void btnShow_Click(object sender, EventArgs e)
        {
            MostrarTodosLosEstudiantes();
        }

        // Evento para seleccionar una fila del GridView
        protected void gvEstudiantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el ID de la fila seleccionada
            GridViewRow row = gvEstudiantes.SelectedRow;
            if (row != null)
            {
                string id = row.Cells[0].Text;
                txtId.Text = id;
                BuscarEstudiantePorId(Convert.ToInt32(id));
            }
        }
    }
}