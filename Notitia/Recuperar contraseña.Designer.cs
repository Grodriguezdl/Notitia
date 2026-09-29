namespace Notitia
{
    partial class Recuperar_contraseña
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recuperar_contraseña));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.TxtUser = new RJCodeAdvance.RJControls.RJTextBox();
            this.rjButton1 = new RJCodeAdvance.RJControls.RJButton();
            this.lblResult = new System.Windows.Forms.Label();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // TxtUser
            // 
            this.TxtUser.BackColor = System.Drawing.SystemColors.Window;
            this.TxtUser.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.TxtUser.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(181)))), ((int)(((byte)(209)))));
            this.TxtUser.BorderRadius = 15;
            this.TxtUser.BorderSize = 2;
            this.TxtUser.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TxtUser.Location = new System.Drawing.Point(214, 253);
            this.TxtUser.Margin = new System.Windows.Forms.Padding(4);
            this.TxtUser.Multiline = false;
            this.TxtUser.Name = "TxtUser";
            this.TxtUser.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.TxtUser.PasswordChar = false;
            this.TxtUser.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.TxtUser.PlaceholderText = "";
            this.TxtUser.Size = new System.Drawing.Size(772, 31);
            this.TxtUser.TabIndex = 0;
            this.TxtUser.Texts = "";
            this.TxtUser.UnderlinedStyle = false;
            // 
            // rjButton1
            // 
            this.rjButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.rjButton1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.rjButton1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rjButton1.BorderRadius = 15;
            this.rjButton1.BorderSize = 0;
            this.rjButton1.FlatAppearance.BorderSize = 0;
            this.rjButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton1.ForeColor = System.Drawing.Color.White;
            this.rjButton1.Location = new System.Drawing.Point(460, 291);
            this.rjButton1.Name = "rjButton1";
            this.rjButton1.Size = new System.Drawing.Size(305, 40);
            this.rjButton1.TabIndex = 1;
            this.rjButton1.Text = "Verificar";
            this.rjButton1.TextColor = System.Drawing.Color.White;
            this.rjButton1.UseVisualStyleBackColor = false;
            this.rjButton1.Click += new System.EventHandler(this.rjButton1_Click);
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.Transparent;
            this.lblResult.Location = new System.Drawing.Point(312, 414);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(817, 145);
            this.lblResult.TabIndex = 2;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.Black;
            this.guna2ControlBox1.Location = new System.Drawing.Point(1191, -2);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox1.TabIndex = 6;
            this.guna2ControlBox1.Click += new System.EventHandler(this.guna2ControlBox1_Click);
            // 
            // Recuperar_contraseña
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1234, 661);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.rjButton1);
            this.Controls.Add(this.TxtUser);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(1234, 661);
            this.MinimumSize = new System.Drawing.Size(1234, 661);
            this.Name = "Recuperar_contraseña";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recuperar_contraseña";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private RJCodeAdvance.RJControls.RJTextBox TxtUser;
        private System.Windows.Forms.Label lblResult;
        private RJCodeAdvance.RJControls.RJButton rjButton1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
    }
}