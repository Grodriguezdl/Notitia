using IBM.Data.DB2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notitia
{
    public partial class Cambiar_datos : Form
    {
        int clickop = 0;
        string[] Zona = new string[28];
        string[] departamentos = {
    "Guatemala", "Baja Verapaz", "Alta Verapaz", "El Progreso",
    "Izabal", "Zacapa", "Chimaltenango", "Sacatepéquez",
    "Escuintla", "Santa Rosa", "Sololá", "Totonicapán",
    "Quetzaltenango", "Suchitepéquez", "Retalhuleu", "San Marcos",
    "Huehuetenango", "Quiché", "Baja Verapaz", "Alta Verapaz",
    "El Progreso", "Izabal", "Zacapa", "Chimaltenango",
    "Sacatepéquez", "Escuintla", "Santa Rosa", "Sololá",
    "Totonicapán", "Quetzaltenango", "Suchitepéquez", "Retalhuleu",
    "San Marcos", "Huehuetenango", "Quiché"
};

        private Dictionary<string, List<string>> municipiosPorDepartamento = new Dictionary<string, List<string>>
{
    { "Guatemala", new List<string> { "Ciudad de Guatemala", "Mixco", "Villa Nueva", "San Juan Sacatepéquez", "Santa Catarina Pinula", "San Miguel Petapa", "San José del Golfo", "Palencia", "Chinautla", "Fraijanes", "Ciudad Quetzal" } },
    { "Sacatepéquez", new List<string> { "Antigua Guatemala", "Ciudad Vieja", "San Lucas Sacatepéquez", "Santa Lucía Milpas Altas", "San Bartolomé Milpas Altas", "Alotenango", "Pastores", "San Antonio Aguas Calientes", "San Pedro Las Huertas" } },
    { "Chimaltenango", new List<string> { "Chimaltenango", "San Martín Jilotepeque", "Parramos", "El Tejar", "Santa Apolonia", "San José Poaquil", "San Juan Comalapa", "San Andrés Itzapa", "Acatenango" } },
    { "Escuintla", new List<string> { "Escuintla", "Santa Lucía Cotzumalguapa", "La Democracia", "San Vicente Pacaya", "San José", "El Naranjo", "Guanagaste" } },
    { "Santa Rosa", new List<string> { "Santa Rosa de Lima", "Cuilapa", "Barberena", "San Juan Tecuaco", "San Rafael Las Flores", "Casillas", "Pueblo Nuevo Viñas" } },
    { "Solalá", new List<string> { "Solalá", "San José Chacaya", "San Juan La Laguna", "San Pedro La Laguna", "Santa Catarina Palopó", "Santiago Atitlán" } },
    { "Totonicapán", new List<string> { "Totonicapán", "San Cristóbal Totonicapán", "Momostenango", "San Andrés Xecul", "Santa María Chiquimula" } },
    { "Quetzaltenango", new List<string> { "Quetzaltenango", "Coatepeque", "San Carlos Sija", "San Miguel Siguila", "Cajolá", "Olintepeque", "Sibilia" } },
    { "San Marcos", new List<string> { "San Marcos", "San Pedro Sacatepéquez", "Tejutla", "Tajumulco", "Nuevo Progreso", "El Tumbador" } },
    { "Huehuetenango", new List<string> { "Huehuetenango", "Chiantla", "Jacaltenango", "La Libertad", "San Sebastián Huehuetenango", "Todos Santos Cuchumatán", "Santa Eulalia" } },
    { "Alta Verapaz", new List<string> { "Cobán", "San Pedro Carchá", "Santa Cruz Verapaz", "San Juan Chamelco", "Tactic", "Purulhá" } },
    { "Baja Verapaz", new List<string> { "Salamá", "Rabinal", "Granados", "San Miguel Chicaj", "El Chol" } },
    { "Quiché", new List<string> { "Santa Cruz del Quiché", "Chiché", "San Pedro Jocopilas", "Cunen", "Joyabaj", "Sacapulas" } },
    { "Jalapa", new List<string> { "Jalapa", "San Pedro Pinula", "San Luis Jilotepeque", "Mataquescuintla", "El Progreso" } },
    { "Suchitepéquez", new List<string> { "Mazatenango", "San Francisco Zapotitlán", "Santo Domingo Suchitepéquez", "San Antonio Suchitepéquez" } },
    { "Retalhuleu", new List<string> { "Retalhuleu", "San Sebastián", "Santa Cruz Muluá", "Champerico" } },
    { "Petén", new List<string> { "Flores", "San Benito", "Santa Elena", "La Libertad", "Poptún" } },
    { "Izabal", new List<string> { "Puerto Barrios", "Morales", "Livingston" } },
    { "Chiquimula", new List<string> { "Chiquimula", "Olopa", "San José La Arada" } }
};
        public Cambiar_datos()
        {
            InitializeComponent();
            MostrarSegunClasificacion(Sesion.IDClasificacionActual);

        }
        public void MostrarSegunClasificacion(string clasificacion)
        {

            // Hacer visible el botón solo si es Administrador (1) o Empleado (2)
            BtnVistade.Visible = clasificacion == "1" || clasificacion == "2";
            LblUsuario.Text = Sesion.NombreUsuarioActual; // Mostrar nombre de usuario en el label
        }

        private void ocultar()
        {
            Pnltransferir.Visible = false;
            Pnlsolicituddeproductos.Visible = false;
            Pnladminempleado.Visible = false;
        }
        private void visible()
        {
            if (Pnltransferir.Visible == true)
                Pnltransferir.Visible = false;
            if (Pnlsolicituddeproductos.Visible == true)
                Pnlsolicituddeproductos.Visible = false;
            if (Pnladminempleado.Visible == true)
                Pnladminempleado.Visible = false;
        }
        private void mostrar(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                visible();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Cambiar_datos_Load(object sender, EventArgs e)
        {
            Pnltransferir.Visible = false;
            Pnlsolicituddeproductos.Visible = false;
            Pnladminempleado.Visible = false;
            CmbEstadociv.Items.Add("Casado");
            CmbEstadociv.Items.Add("Soltero");
            CmbPep.Items.Add("Si");
            CmbPep.Items.Add("No");

            // Rellenar zona
            for (int f = 0; f <= 27; f++)
            {
                Zona[f] = "Z." + f.ToString();
            }
            CmbZona.Items.AddRange(Zona);

            // Rellenar departamentos
            CmbDepartamento.Items.AddRange(departamentos);
            CmbMunicipios.Items.Clear(); // Asegurarse de que esté vacío al inicio

            CargarDatosUsuario();
            CargarDatosDireccion();
        }

        private void CargarDatosUsuario()
        {
            string idUsuario = Sesion.IDUsuarioActual; // Obtener IDUsuario del usuario actual
            string query = "SELECT Correo_Electronico, Estado_Civil, DPI, Número_de_telefono, Profesion_Oficio, Politicamente_Expuesto FROM Usuarios WHERE IDUsuario = @idUsuario";

            try
            {
                using (var connection = new DB2Connection(Conexion.conexion))
                {
                    using (var command = new DB2Command(query, connection))
                    {
                        command.Parameters.Add("@idUsuario", idUsuario);
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TxtCorreo_electronico.Texts = reader["Correo_Electronico"].ToString();
                                CmbEstadociv.SelectedItem = reader["Estado_Civil"].ToString();
                                TxtNit.Texts = reader["DPI"].ToString();
                                TxtTelefono.Texts = reader["Número_de_telefono"].ToString();
                                TxtProfesion.Texts = reader["Profesion_Oficio"].ToString();

                                // Asegúrate de que los elementos "Si" y "No" estén agregados en el ComboBox
                                string pepValue = reader["Politicamente_Expuesto"].ToString();
                                if (pepValue == "Si" || pepValue == "No")
                                {
                                    CmbPep.SelectedItem = pepValue; // Esto seleccionará "Si" o "No"
                                }
                                else
                                {
                                    CmbPep.SelectedItem = null; // En caso de que no sea válido
                                }
                            }
                        }
                    }
                }
            }
            catch (DB2Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }



        private void BtnResumen_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Resumen().ShowDialog(); ocultar();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            string url = "https://docs.google.com/forms/d/e/1FAIpQLSd8v1Il3EAmvqdOk7wKimSV3EiSvkU8S_6DFFhBmYWyPJHNcw/viewform?vc=0&c=0&w=1&flr=0&pli=1";

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void rjCircularPictureBox1_Click(object sender, EventArgs e)
        {
            clickop++;
            if (clickop % 2 == 0)
            {
                PnlOpcionusuario.Visible = false;
            }
            else
            {
                PnlOpcionusuario.Visible = true;
            }
        }

        private void LblUsuario_Click(object sender, EventArgs e)
        {
            clickop++;
            if (clickop % 2 == 0)
            {
                PnlOpcionusuario.Visible = false;
            }
            else
            {
                PnlOpcionusuario.Visible = true;
            }
        }

        private void Btncontra_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Cambiar_Contraseña().ShowDialog();
        }

        private void btnActualizacion_Click(object sender, EventArgs e)
        {
            this.Invalidate();
            this.Update();
        }

        private void BtnTransferir_Click(object sender, EventArgs e)
        {
            mostrar(Pnltransferir);
        }

        private void Btnotrascuentas_Click(object sender, EventArgs e)
        {
            this.Hide();
            new A_otras_cuentas().ShowDialog(); ocultar();
        }

        private void BtnHaciaotropais_Click(object sender, EventArgs e)
        {
           
        }

        private void BtnHaciaotrosbancos_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Hacia_otros_bancos().ShowDialog(); ocultar();
        }

        private void BtnInformacion_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Información().ShowDialog(); ocultar();
        }

        private void BtnSolicituddeproductos_Click(object sender, EventArgs e)
        {
            mostrar(Pnlsolicituddeproductos);
        }

        private void BtnCuenta_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Cuenta().ShowDialog(); ocultar();
        }

        private void BtnPrestamo_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Solicitud_Prestamo().ShowDialog(); ocultar();
        }

        private void BtnDivisa_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Cambio_de_divisa().ShowDialog(); ocultar();
        }

        private void BtnTransacciones_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Historial_transacciones().ShowDialog(); ocultar();
        }

        private void BtnVistade_Click(object sender, EventArgs e)
        {
            mostrar(Pnladminempleado);
        }

        private void BtnUsuarios_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Usuarios_vista().ShowDialog(); ocultar();
        }

        private void BtnSolprod_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Solicitud_de_productos().ShowDialog(); ocultar();
        }

        private void BtnPrestamos_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Solicitudes_prestamo().ShowDialog(); ocultar();
        }

        private void rjButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login().ShowDialog(); ocultar();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblHoraactual.Text = "Hora actual:" + DateTime.Now.ToString("hh:mm:ss");
        }

        private void PnlOpcionusuario_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            ActualizarDatosUsuario();
        }

        private void ActualizarDatosUsuario()
        {
            string idUsuario = Sesion.IDUsuarioActual; // Obtener IDUsuario del usuario actual
            string query = "UPDATE Usuarios SET Correo_Electronico = @correo, Estado_Civil = @estadoCivil, DPI = @dpi, Número_de_telefono = @telefono, Profesion_Oficio = @profesion, Politicamente_Expuesto = @pep WHERE IDUsuario = @idUsuario";

            try
            {
                using (var connection = new DB2Connection(Conexion.conexion))
                {
                    using (var command = new DB2Command(query, connection))
                    {
                        command.Parameters.Add("@correo", TxtCorreo_electronico.Texts);
                        command.Parameters.Add("@estadoCivil", CmbEstadociv.SelectedItem.ToString());
                        command.Parameters.Add("@dpi", TxtNit.Texts);
                        command.Parameters.Add("@telefono", TxtTelefono.Texts);
                        command.Parameters.Add("@profesion", TxtProfesion.Texts);
                        command.Parameters.Add("@pep", CmbPep.SelectedItem.ToString());
                        command.Parameters.Add("@idUsuario", idUsuario);

                        connection.Open();
                        int filasAfectadas = command.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Datos actualizados correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron registros para actualizar.");
                        }
                    }
                }
            }
            catch (DB2Exception ex)
            {
                MessageBox.Show($"Error al actualizar datos: {ex.Message}");
            }
        }
        private void CargarDatosDireccion()
        {
            string idUsuario = Sesion.IDUsuarioActual;
            string query = "SELECT Direccion, Departamento, Zona, Municipio FROM Direccion WHERE IDUsuario = @idUsuario";

            try
            {
                using (var connection = new DB2Connection(Conexion.conexion))
                {
                    using (var command = new DB2Command(query, connection))
                    {
                        command.Parameters.Add("@idUsuario", idUsuario);
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TxtDireccion.Texts = reader["Direccion"].ToString();
                                CmbDepartamento.SelectedItem = reader["Departamento"].ToString();
                                CmbZona.SelectedItem = reader["Zona"].ToString();
                                CmbMunicipios.SelectedItem = reader["Municipio"].ToString();
                            }
                        }
                    }
                }
            }
            catch (DB2Exception ex)
            {
                MessageBox.Show($"Error al cargar dirección: {ex.Message}");
            }
        }

        private void ActualizarDatosDireccion()
        {
            string idUsuario = Sesion.IDUsuarioActual;
            string query = "UPDATE Direccion SET Direccion = @direccion, Departamento = @departamento, Zona = @zona, Municipio = @municipio WHERE IDUsuario = @idUsuario";

            try
            {
                using (var connection = new DB2Connection(Conexion.conexion))
                {
                    using (var command = new DB2Command(query, connection))
                    {
                        command.Parameters.Add("@direccion", TxtDireccion.Texts);
                        command.Parameters.Add("@departamento", CmbDepartamento.SelectedItem.ToString());
                        command.Parameters.Add("@zona", CmbZona.SelectedItem.ToString());
                        command.Parameters.Add("@municipio", CmbMunicipios.SelectedItem.ToString());
                        command.Parameters.Add("@idUsuario", idUsuario);

                        connection.Open();
                        int filasAfectadas = command.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Dirección actualizada correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron registros para actualizar.");
                        }
                    }
                }
            }
            catch (DB2Exception ex)
            {
                MessageBox.Show($"Error al actualizar dirección: {ex.Message}");
            }
        }

        private void CmbDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbDepartamento.SelectedItem != null)
            {
                string departamentoSeleccionado = CmbDepartamento.SelectedItem.ToString();
                if (municipiosPorDepartamento.ContainsKey(departamentoSeleccionado))
                {
                    CmbMunicipios.Items.Clear(); // Limpiar municipios anteriores
                    CmbMunicipios.Items.AddRange(municipiosPorDepartamento[departamentoSeleccionado].ToArray());
                }
            }
        }

        private void BtnGuardardireccion_Click(object sender, EventArgs e)
        {
            ActualizarDatosDireccion();
        }
    }
}
