namespace GameForClients
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            panel2 = new Panel();
            checkPassword = new CheckBox();
            btnForRegistration = new Button();
            btnForEnter = new Button();
            txtForPassword = new TextBox();
            txtForLogin = new TextBox();
            lblGameName = new Label();
            lblPassword = new Label();
            lblLogin = new Label();
            btnForRu = new Button();
            btnForEn = new Button();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.BackColor = Color.FromArgb(100, 180, 220, 250);
            panel2.Controls.Add(btnForEn);
            panel2.Controls.Add(btnForRu);
            panel2.Controls.Add(checkPassword);
            panel2.Controls.Add(btnForRegistration);
            panel2.Controls.Add(btnForEnter);
            panel2.Controls.Add(txtForPassword);
            panel2.Controls.Add(txtForLogin);
            panel2.Controls.Add(lblGameName);
            panel2.Controls.Add(lblPassword);
            panel2.Controls.Add(lblLogin);
            panel2.Name = "panel2";
            // 
            // checkPassword
            // 
            resources.ApplyResources(checkPassword, "checkPassword");
            checkPassword.Name = "checkPassword";
            checkPassword.UseVisualStyleBackColor = true;
            // 
            // btnForRegistration
            // 
            resources.ApplyResources(btnForRegistration, "btnForRegistration");
            btnForRegistration.BackColor = Color.FromArgb(133, 143, 180);
            btnForRegistration.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForRegistration.ForeColor = Color.FromArgb(49, 56, 87);
            btnForRegistration.Name = "btnForRegistration";
            btnForRegistration.UseVisualStyleBackColor = false;
            // 
            // btnForEnter
            // 
            resources.ApplyResources(btnForEnter, "btnForEnter");
            btnForEnter.BackColor = Color.FromArgb(133, 143, 180);
            btnForEnter.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForEnter.ForeColor = Color.FromArgb(49, 56, 87);
            btnForEnter.Name = "btnForEnter";
            btnForEnter.UseVisualStyleBackColor = false;
            // 
            // txtForPassword
            // 
            txtForPassword.AcceptsReturn = true;
            resources.ApplyResources(txtForPassword, "txtForPassword");
            txtForPassword.Name = "txtForPassword";
            // 
            // txtForLogin
            // 
            txtForLogin.AcceptsReturn = true;
            resources.ApplyResources(txtForLogin, "txtForLogin");
            txtForLogin.Name = "txtForLogin";
            // 
            // lblGameName
            // 
            resources.ApplyResources(lblGameName, "lblGameName");
            lblGameName.BackColor = Color.FromArgb(133, 143, 180);
            lblGameName.ForeColor = Color.FromArgb(49, 56, 87);
            lblGameName.Name = "lblGameName";
            // 
            // lblPassword
            // 
            resources.ApplyResources(lblPassword, "lblPassword");
            lblPassword.BackColor = Color.White;
            lblPassword.ForeColor = Color.FromArgb(49, 56, 87);
            lblPassword.Name = "lblPassword";
            // 
            // lblLogin
            // 
            resources.ApplyResources(lblLogin, "lblLogin");
            lblLogin.BackColor = Color.White;
            lblLogin.ForeColor = Color.FromArgb(49, 56, 87);
            lblLogin.Name = "lblLogin";
            // 
            // btnForRu
            // 
            resources.ApplyResources(btnForRu, "btnForRu");
            btnForRu.BackColor = Color.FromArgb(133, 143, 180);
            btnForRu.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForRu.ForeColor = Color.FromArgb(49, 56, 87);
            btnForRu.Name = "btnForRu";
            btnForRu.UseVisualStyleBackColor = false;
            btnForRu.Click += btnForRu_Click;
            // 
            // btnForEn
            // 
            resources.ApplyResources(btnForEn, "btnForEn");
            btnForEn.BackColor = Color.FromArgb(133, 143, 180);
            btnForEn.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForEn.ForeColor = Color.FromArgb(49, 56, 87);
            btnForEn.Name = "btnForEn";
            btnForEn.UseVisualStyleBackColor = false;
            btnForEn.Click += btnForEn_Click;
            // 
            // Login
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 192, 192);
            Controls.Add(panel2);
            ForeColor = SystemColors.InactiveCaptionText;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Login";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion


        private Panel panel2;
        private Label lblPassword;
        private Label lblLogin;
        private TextBox txtForLogin;
        private Label lblGameName;
        private TextBox txtForPassword;
        private Button btnForEnter;
        private Button btnForRegistration;
        private CheckBox checkPassword;
        private Button btnForEn;
        private Button btnForRu;
    }
}
