namespace ServiceDesk.WinForms.Controls
{
    partial class LoginControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnLogin = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(0, 192, 0);
            btnLogin.FlatAppearance.BorderColor = Color.Silver;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLogin.Location = new Point(321, 393);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(128, 41);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.BackColor = Color.Red;
            panel1.Controls.Add(btnLogin);
            panel1.Location = new Point(469, 142);
            panel1.Name = "panel1";
            panel1.Size = new Size(471, 457);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.RoyalBlue;
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(359, 724);
            panel2.TabIndex = 2;
            // 
            // LoginControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "LoginControl";
            Size = new Size(1036, 724);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnLogin;
        private Panel panel1;
        private Panel panel2;
    }
}
