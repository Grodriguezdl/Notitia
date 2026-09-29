using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Notitia
{
    public partial class Cambio_de_divisa : Form
    {
        private const string ApiKey = "619da854ca87fdf8d97249fa";
        int clickop = 0;
        int venta = 0;
        int compra = 1;
        int US = 0;
        int Q = 1;
        int clickbtn1 = 1;
        decimal valor;
        public Cambio_de_divisa()
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
        private void BtnResumen_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Resumen().ShowDialog(); ocultar();
        }


        private void Cambio_de_divisa_Load(object sender, EventArgs e)
        {
            Pnltransferir.Visible = false;
            Pnlsolicituddeproductos.Visible = false;
            Pnladminempleado.Visible = false;
            Cmbcompr.Items.Add("Agencia");
            Cmbcompr.Items.Add("En Linea");
            Cmbcompr.SelectedIndex = 0;
            Cmbcompr.SelectedIndexChanged += Cmbcompr_SelectedIndexChanged;
        }

        private void Rbtncompra_CheckedChanged(object sender, EventArgs e)
        {
            if (Rbtncompra.Checked == true)
            {
                compra = 1;
                venta = 0;
            }
        }

        private void RbtnVenta_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtnVenta.Checked == true)
            {
                compra = 0;
                venta = 1;
            }
        }

        private void Btnconv_Click(object sender, EventArgs e)
        {
            clickbtn1++;
            if (clickbtn1 % 2 == 0)
            {
                US = 1;
                Q = 0;
                Lbl2.Text = "USD-Dolár Estadounidense";
                Lbl1.Text = "GTQ-Quetzal";
                Lbl2.Location = new System.Drawing.Point(847, 675);
                Lbl1.Location = new System.Drawing.Point(1180, 676);

            }
            else
            {
                Q = 1;
                US = 0;
                Lbl2.Text = "GTQ-Quetzal";
                Lbl1.Text = "USD-Dolár Estadounidense";
                Lbl2.Location = new System.Drawing.Point(884, 676);
                Lbl1.Location = new System.Drawing.Point(1137, 676);

            }
            if (US == 0 && Q == 1)
            {


                txtmon1.Texts = "Q" + TxtMont.Texts;

            }
            if (US == 1 && Q == 0)
            {
                txtmon1.Texts = "$" + TxtMont.Texts;
            }
        }

        private void btncan_Click(object sender, EventArgs e)
        {

            if (US == 0 && Q == 1)
            {


                txtmon1.Texts = "Q" + TxtMont.Texts;

            }
            if (US == 1 && Q == 0)
            {
                txtmon1.Texts = "$" + TxtMont.Texts;
            }
            if (double.TryParse(TxtMont.Texts, out double valor))
            {
                if (US == 1 && Q == 0)
                {
                    if (compra == 1 && Cmbcompr.SelectedIndex == 0)
                    {
                        TxtMon2.Texts = "Q" + (valor * 7.52).ToString();
                    }
                    if (compra == 1 && Cmbcompr.SelectedIndex == 1)
                    {
                        TxtMon2.Texts = "Q" + (valor * 7.54).ToString();
                    }
                    if (venta == 1 && Cmbcompr.SelectedIndex == 0)
                    {
                        TxtMon2.Texts = "Q" + (valor * 7.90).ToString();
                    }
                    if (venta == 1 && Cmbcompr.SelectedIndex == 1)
                    {
                        TxtMon2.Texts = "Q" + (valor * 7.88).ToString();
                    }
                }
                if (US == 0 && Q == 1)
                {
                    if (compra == 1 && Cmbcompr.SelectedIndex == 0)
                    {
                        TxtMon2.Texts = "$" + (valor / 7.52).ToString();
                    }
                    if (compra == 1 && Cmbcompr.SelectedIndex == 1)
                    {
                        TxtMon2.Texts = "$" + (valor / 7.54).ToString();
                    }
                    if (venta == 1 && Cmbcompr.SelectedIndex == 0)
                    {
                        TxtMon2.Texts = "$" + (valor / 7.90).ToString();
                    }
                    if (venta == 1 && Cmbcompr.SelectedIndex == 1)
                    {
                        TxtMon2.Texts = "$" + (valor / 7.88).ToString();
                    }

                }
            }
            
        }


        private void TxtMont__TextChanged(object sender, EventArgs e)
        {

            if (US == 0 && Q == 1)
            {


                txtmon1.Texts = "Q" + TxtMont.Texts;

            }



        }

     
    private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Cmbcompr_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            this.Hide();
            new Cambiar_datos().ShowDialog();
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

        private void BtnPrestamo_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Solicitud_Prestamo().ShowDialog(); ocultar();
        }

        private void BtnCuenta_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Cuenta().ShowDialog(); ocultar();
        }

        private void BtnDivisa_Click(object sender, EventArgs e)
        {
            this.Invalidate();
            this.Update();
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
            new Login().ShowDialog();
        }

        private void Pnltransferir_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtmon1__TextChanged(object sender, EventArgs e)
        {
            if (US == 0 && Q == 1)
            {


                txtmon1.Texts = "Q" + TxtMont.Texts;

            }
            if (US == 1 && Q == 0)
            {
                txtmon1.Texts = "$" + TxtMont.Texts;
            }
        }

        private void TxtMon2__TextChanged(object sender, EventArgs e)
        {
            if (US == 0 && Q == 1)
            {


                txtmon1.Texts = "Q" + TxtMont.Texts;

            }
            if (US == 1 && Q == 0)
            {
                txtmon1.Texts = "$" + TxtMont.Texts;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            this.lblHoraactual.Text = "Hora actual:" + DateTime.Now.ToString("hh:mm:ss");
        }
    }
}
