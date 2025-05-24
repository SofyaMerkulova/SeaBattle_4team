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
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(100, 180, 220, 250);
            panel2.Controls.Add(checkPassword);
            panel2.Controls.Add(btnForRegistration);
            panel2.Controls.Add(btnForEnter);
            panel2.Controls.Add(txtForPassword);
            panel2.Controls.Add(txtForLogin);
            panel2.Controls.Add(lblGameName);
            panel2.Controls.Add(lblPassword);
            panel2.Controls.Add(lblLogin);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1188, 631);
            panel2.TabIndex = 0;
            // 
            // checkPassword
            // 
            checkPassword.AutoSize = true;
            checkPassword.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            checkPassword.Location = new Point(1071, 333);
            checkPassword.Name = "checkPassword";
            checkPassword.Size = new Size(40, 45);
            checkPassword.TabIndex = 7;
            checkPassword.Text = "\r\n";
            checkPassword.UseVisualStyleBackColor = true;
            // 
            // btnForRegistration
            // 
            btnForRegistration.BackColor = Color.FromArgb(133, 143, 180);
            btnForRegistration.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForRegistration.FlatStyle = FlatStyle.Flat;
            btnForRegistration.Font = new Font("Sitka Text", 22.2F, FontStyle.Bold, GraphicsUnit.Point);
            btnForRegistration.ForeColor = Color.FromArgb(49, 56, 87);
            btnForRegistration.Location = new Point(698, 519);
            btnForRegistration.Name = "btnForRegistration";
            btnForRegistration.Size = new Size(436, 77);
            btnForRegistration.TabIndex = 6;
            btnForRegistration.Text = "Зарегистрироваться";
            btnForRegistration.UseVisualStyleBackColor = false;
            // 
            // btnForEnter
            // 
            btnForEnter.BackColor = Color.FromArgb(133, 143, 180);
            btnForEnter.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForEnter.FlatStyle = FlatStyle.Flat;
            btnForEnter.Font = new Font("Sitka Text", 22.2F, FontStyle.Bold, GraphicsUnit.Point);
            btnForEnter.ForeColor = Color.FromArgb(49, 56, 87);
            btnForEnter.Location = new Point(231, 519);
            btnForEnter.Name = "btnForEnter";
            btnForEnter.Size = new Size(235, 77);
            btnForEnter.TabIndex = 5;
            btnForEnter.Text = "Войти";
            btnForEnter.UseVisualStyleBackColor = false;
            // 
            // txtForPassword
            // 
            txtForPassword.AcceptsReturn = true;
            txtForPassword.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point);
            txtForPassword.Location = new Point(414, 327);
            txtForPassword.Name = "txtForPassword";
            txtForPassword.Size = new Size(634, 51);
            txtForPassword.TabIndex = 4;
            // 
            // txtForLogin
            // 
            txtForLogin.AcceptsReturn = true;
            txtForLogin.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point);
            txtForLogin.Location = new Point(414, 165);
            txtForLogin.Name = "txtForLogin";
            txtForLogin.Size = new Size(636, 51);
            txtForLogin.TabIndex = 3;
            // 
            // lblGameName
            // 
            lblGameName.AutoSize = true;
            lblGameName.BackColor = Color.FromArgb(133, 143, 180);
            lblGameName.Font = new Font("Sitka Text", 28.1999989F, FontStyle.Bold, GraphicsUnit.Point);
            lblGameName.ForeColor = Color.FromArgb(49, 56, 87);
            lblGameName.Location = new Point(462, 9);
            lblGameName.Name = "lblGameName";
            lblGameName.Size = new Size(345, 68);
            lblGameName.TabIndex = 2;
            lblGameName.Text = "Морской бой";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.BackColor = Color.White;
            lblPassword.Font = new Font("Sitka Text", 25.8F, FontStyle.Bold, GraphicsUnit.Point);
            lblPassword.ForeColor = Color.FromArgb(49, 56, 87);
            lblPassword.Location = new Point(14, 316);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(394, 62);
            lblPassword.TabIndex = 1;
            lblPassword.Text = "Введите пароль:";
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.BackColor = Color.White;
            lblLogin.Font = new Font("Sitka Text", 25.8F, FontStyle.Bold, GraphicsUnit.Point);
            lblLogin.ForeColor = Color.FromArgb(49, 56, 87);
            lblLogin.Location = new Point(12, 165);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(394, 62);
            lblLogin.TabIndex = 0;
            lblLogin.Text = "Введите   логин:";
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 192, 192);
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1188, 631);
            Controls.Add(panel2);
            ForeColor = SystemColors.InactiveCaptionText;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimizeBox = false;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Вход и Регистрация";
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
    }
}
