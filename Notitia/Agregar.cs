using IBM.Data.DB2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notitia
{
    public partial class Agregar : Form
    {
        public Agregar()
        {
            InitializeComponent();
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Agregar_Load(object sender, EventArgs e)
        {
            if (RbtnNotitia.Checked == true)
            {
                 GboxAgregarpor.Visible = true;
                PnlNumerocuenta.Visible = true;
                Pnlceloid.Visible = false;
                Txtidcu.Texts = "";
                GboxAgregarpor.Location = new Point(407, 134);
                PnlNumerocuenta.Location = new Point(407, 263);
                TxtNumcuenta.Texts = "";
           
                Pnlbancytip.Visible = false;
            }
            if (RbtnNumcuenta.Checked == true)
            {
                PnlNumerocuenta.Visible = true;
                Pnlceloid.Visible = false;
            }
            Cmbdestino.Items.Add("Banco Virtud");

        }

        private void RbtnIdcel_CheckedChanged(object sender, EventArgs e)
        {
            PnlNumerocuenta.Visible = false;
            Pnlceloid.Visible = true;
            Pnlceloid.Location = new System.Drawing.Point(61, 257);
            Txtidcu.Texts = "";
        }

        private void RbtnNumcuenta_CheckedChanged(object sender, EventArgs e)
        {
            PnlNumerocuenta.Visible = true;
            Pnlceloid.Visible = false;
        }

        private void RbtnNotitia_CheckedChanged(object sender, EventArgs e)
        {
            
            Pnlbancytip.Visible = false;
            GboxAgregarpor.Visible = true;
            PnlNumerocuenta.Visible = true;
            Pnlceloid.Visible = false;
            Txtidcu.Texts = "";
            GboxAgregarpor.Location = new Point(407, 134);
            PnlNumerocuenta.Location = new Point(407, 263);
            TxtNumcuenta.Texts = "";
        }

        private void RbtnOtrosbancos_CheckedChanged(object sender, EventArgs e)
        {
            
            GboxAgregarpor.Visible = false;
            PnlNumerocuenta.Visible = true;
            Pnlceloid.Visible = false;
            Txtidcu.Texts = "";
            GboxAgregarpor.Location = new Point(0, 0);
            PnlNumerocuenta.Location = new Point(61, 257);
            TxtNumcuenta.Texts = "";
            Pnlbancytip.Visible = true;
            
        }

        private void AgregarAfiliadas()
        {
            string numeroCuentaIngresada = TxtNumcuenta.Texts.Trim();
            string nombreCuenta = "";
            decimal saldoCuenta = 0;

            // Verificar si el número de cuenta existe en BancoVir
            using (var conexionBancoVir = new DB2Connection(Conexion.conexionBancovir))
            {
                conexionBancoVir.Open();

                string query = "SELECT Nombre_de_Cuenta, Saldo FROM Cuentas_Bancarias WHERE Numero_de_Cuenta = @NumeroCuenta";
                using (var command = new DB2Command(query, conexionBancoVir))
                {
                    command.Parameters.Add(new DB2Parameter("@NumeroCuenta", numeroCuentaIngresada));

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nombreCuenta = reader["Nombre_de_Cuenta"].ToString();
                            saldoCuenta = Convert.ToDecimal(reader["Saldo"]);
                        }
                        else
                        {
                            MessageBox.Show("Ingresar una cuenta válida");
                            return; // Salir si no se encontró la cuenta
                        }
                    }
                }
            }

            // Verificar si la cuenta pertenece al usuario actual
            using (var conexionDbexpo = new DB2Connection(Conexion.conexion))
            {
                conexionDbexpo.Open();

                string queryVerificacion = "SELECT COUNT(*) FROM Cuentas_Bancarias WHERE Numero_de_Cuenta = @NumeroCuenta AND IDUsuario = @IDUsuario";
                using (var commandVerificacion = new DB2Command(queryVerificacion, conexionDbexpo))
                {
                    commandVerificacion.Parameters.Add(new DB2Parameter("@NumeroCuenta", numeroCuentaIngresada));
                    commandVerificacion.Parameters.Add(new DB2Parameter("@IDUsuario", Sesion.IDUsuarioActual));

                    int count = Convert.ToInt32(commandVerificacion.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Ese id/número de cuenta pertenece a una de sus cuentas");
                        return; // Salir si la cuenta pertenece al usuario actual
                    }
                }
            }

            // Obtener el siguiente ID para Afiliadas
            string nuevoIdAfiliada = GetNextIdAfiliadas();

            // Insertar nuevo registro en Afiliadas
            using (var conexion = new DB2Connection(Conexion.conexion))
            {
                conexion.Open();

                string insertQuery = "INSERT INTO Afiliadas (IDAfiliada, Numero_de_cuenta, Banco, Saldo, IDUsuario) VALUES (@IDAfiliada, @Numero_cuenta, @Banco, @Saldo, @IDUsuario)";
                using (var insertCommand = new DB2Command(insertQuery, conexion))
                {
                    insertCommand.Parameters.Add(new DB2Parameter("@IDAfiliada", nuevoIdAfiliada));
                    insertCommand.Parameters.Add(new DB2Parameter("@Numero_cuenta", numeroCuentaIngresada));
                    insertCommand.Parameters.Add(new DB2Parameter("@Banco", "Banco Virtud"));
                    insertCommand.Parameters.Add(new DB2Parameter("@Saldo", saldoCuenta));
                    insertCommand.Parameters.Add(new DB2Parameter("@IDUsuario", Sesion.IDUsuarioActual));

                    insertCommand.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Registro agregado exitosamente.");
        }

        private string GetNextIdAfiliadas()
        {
            int nextId = 1;

            using (var conexion = new DB2Connection(Conexion.conexion))
            {
                conexion.Open();

                string query = "SELECT COUNT(*) FROM Afiliadas";
                using (var command = new DB2Command(query, conexion))
                {
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        nextId = count + 1;
                    }
                }
            }

            return nextId.ToString();
        }


        private void RbtnAhorro_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void RbtnMonetario_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void CboxAfiliar_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            if (RbtnOtrosbancos.Checked)
            {
                AgregarAfiliadas();
            }
            else if (RbtnNotitia.Checked)
            {
                AgregarAfiliadasNotitia();
            }
        }

        private void AgregarAfiliadasNotitia()
        {
            string idCuentaIngresada = Txtidcu.Texts.Trim();
            string numeroCuentaIngresada = TxtNumcuenta.Texts.Trim();
            string nombreCuenta = "";
            decimal saldoCuenta = 0;

            if (string.IsNullOrEmpty(idCuentaIngresada) && string.IsNullOrEmpty(numeroCuentaIngresada))
            {
                MessageBox.Show("Debe ingresar el ID de la cuenta o el número de cuenta.");
                return;
            }

            if (!string.IsNullOrEmpty(idCuentaIngresada) && !string.IsNullOrEmpty(numeroCuentaIngresada))
            {
                MessageBox.Show("Debe ingresar solo uno: el ID de la cuenta o el número de cuenta.");
                return;
            }

            string query;
            if (!string.IsNullOrEmpty(idCuentaIngresada))
            {
                query = "SELECT Nombre_de_Cuenta, Numero_de_Cuenta, Saldo, IDUsuario FROM Cuentas_Bancarias WHERE IDCuenta = @IDCuenta";
            }
            else
            {
                query = "SELECT Nombre_de_Cuenta, Numero_de_Cuenta, Saldo, IDUsuario FROM Cuentas_Bancarias WHERE Numero_de_Cuenta = @NumeroCuenta";
            }

            using (var conexion = new DB2Connection(Conexion.conexion))
            {
                conexion.Open();

                using (var command = new DB2Command(query, conexion))
                {
                    if (!string.IsNullOrEmpty(idCuentaIngresada))
                    {
                        command.Parameters.Add("@IDCuenta", idCuentaIngresada);
                    }
                    else
                    {
                        command.Parameters.Add("@NumeroCuenta", numeroCuentaIngresada);
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Verificar si la cuenta pertenece al usuario actual
                            string idUsuarioCuenta = reader["IDUsuario"].ToString();
                            if (idUsuarioCuenta == Sesion.IDUsuarioActual)
                            {
                                MessageBox.Show("Ese ID o número de cuenta pertenece a una de sus cuentas.");
                                return;
                            }

                            nombreCuenta = reader["Nombre_de_Cuenta"].ToString();
                            numeroCuentaIngresada = reader["Numero_de_Cuenta"].ToString();
                            saldoCuenta = Convert.ToDecimal(reader["Saldo"]);
                        }
                        else
                        {
                            MessageBox.Show("Ingresar una cuenta válida.");
                            return;
                        }
                    }
                }

                string nuevoIdAfiliadaNotitia = GetNextIdAfiliadasNot(conexion);

                string insertQuery = "INSERT INTO Afiliadas_notitia (IDAfiliadas_not, Numero_de_cuenta, Nombre_cuenta, Saldo, IDUsuario) VALUES (@IDAfiliadas_not, @Numero_cuenta, @Nombre_cuenta, @Saldo, @IDUsuario)";
                using (var insertCommand = new DB2Command(insertQuery, conexion))
                {
                    insertCommand.Parameters.Add("@IDAfiliadas_not", nuevoIdAfiliadaNotitia);
                    insertCommand.Parameters.Add("@Numero_cuenta", numeroCuentaIngresada);
                    insertCommand.Parameters.Add("@Nombre_cuenta", nombreCuenta);
                    insertCommand.Parameters.Add("@Saldo", saldoCuenta);
                    insertCommand.Parameters.Add("@IDUsuario", Sesion.IDUsuarioActual);

                    insertCommand.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Registro agregado exitosamente.");
        }

        private string GetNextIdAfiliadasNot(DB2Connection conexion)
        {
            int nextId = 1;

            string query = "SELECT MAX(CAST(IDAfiliadas_not AS INTEGER)) FROM Afiliadas_notitia";
            using (var command = new DB2Command(query, conexion))
            {
                var result = command.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    nextId = Convert.ToInt32(result) + 1;
                }
            }

            return nextId.ToString();
        }

    }
}
    
