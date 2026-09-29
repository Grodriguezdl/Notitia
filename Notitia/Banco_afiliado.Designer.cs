namespace Notitia
{
    partial class Banco_afiliado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Banco_afiliado));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.GboxInf = new Guna.UI2.WinForms.Guna2GroupBox();
            this.DgvBancovir = new System.Windows.Forms.DataGridView();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.GboxInf.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvBancovir)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // GboxInf
            // 
            this.GboxInf.Controls.Add(this.DgvBancovir);
            this.GboxInf.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.GboxInf.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GboxInf.ForeColor = System.Drawing.Color.White;
            this.GboxInf.Location = new System.Drawing.Point(122, 207);
            this.GboxInf.Name = "GboxInf";
            this.GboxInf.Size = new System.Drawing.Size(971, 295);
            this.GboxInf.TabIndex = 43;
            this.GboxInf.Text = "Banco Virtud";
            // 
            // DgvBancovir
            // 
            this.DgvBancovir.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.DgvBancovir.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvBancovir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvBancovir.Location = new System.Drawing.Point(0, 40);
            this.DgvBancovir.Name = "DgvBancovir";
            this.DgvBancovir.Size = new System.Drawing.Size(971, 255);
            this.DgvBancovir.TabIndex = 0;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.Black;
            this.guna2ControlBox1.Location = new System.Drawing.Point(1155, -2);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox1.TabIndex = 44;
            // 
            // Banco_afiliado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.GboxInf);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(1200, 600);
            this.MinimumSize = new System.Drawing.Size(1200, 600);
            this.Name = "Banco_afiliado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Banco_afiliado";
            this.GboxInf.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvBancovir)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2GroupBox GboxInf;
        private System.Windows.Forms.DataGridView DgvBancovir;
    }
}