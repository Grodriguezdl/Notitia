using IBM.Data.DB2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notitia
{
    public partial class Banco_afiliado : Form
    {
        public Banco_afiliado()
        {
            InitializeComponent();
            CargarCuentasBancarias();
            DgvBancovir.DefaultCellStyle.ForeColor = Color.Black;

        }
        private void CargarCuentasBancarias()
        {

            try
            {
                using (var connection = new DB2Connection(Conexion.conexionBancovir))
                {
                    connection.Open();
                    string query = "SELECT * FROM Cuentas_Bancarias";
                    using (var command = new DB2Command(query, connection))
                    {
                        using (var adapter = new DB2DataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            DgvBancovir.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (DB2Exception ex)
            {
                MessageBox.Show("Error de conexión a DB2: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}

