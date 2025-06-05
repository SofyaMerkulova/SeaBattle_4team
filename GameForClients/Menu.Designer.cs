namespace GameForClients
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            panel1 = new Panel();
            lblForStartNewGame = new Label();
            lblForID = new Label();
            txtForID = new TextBox();
            btnForJoinGame = new Button();
            btnFor = new Button();
            btnForStart = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(100, 180, 220, 250);
            panel1.Controls.Add(lblForStartNewGame);
            panel1.Controls.Add(lblForID);
            panel1.Controls.Add(txtForID);
            panel1.Controls.Add(btnForJoinGame);
            panel1.Controls.Add(btnFor);
            panel1.Controls.Add(btnForStart);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1188, 631);
            panel1.TabIndex = 0;
            // 
            // lblForStartNewGame
            // 
            lblForStartNewGame.AutoSize = true;
            lblForStartNewGame.BackColor = Color.White;
            lblForStartNewGame.Font = new Font("Sitka Text", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point);
            lblForStartNewGame.ForeColor = Color.FromArgb(49, 56, 87);
            lblForStartNewGame.Location = new Point(411, 9);
            lblForStartNewGame.Name = "lblForStartNewGame";
            lblForStartNewGame.Size = new Size(331, 33);
            lblForStartNewGame.TabIndex = 12;
            lblForStartNewGame.Text = "Начните свою новую игру";
            // 
            // lblForID
            // 
            lblForID.AutoSize = true;
            lblForID.BackColor = Color.White;
            lblForID.Font = new Font("Sitka Text", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point);
            lblForID.ForeColor = Color.FromArgb(49, 56, 87);
            lblForID.Location = new Point(376, 174);
            lblForID.Name = "lblForID";
            lblForID.Size = new Size(408, 33);
            lblForID.TabIndex = 11;
            lblForID.Text = "Введите ID существующей игры ";
            // 
            // txtForID
            // 
            txtForID.AcceptsReturn = true;
            txtForID.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point);
            txtForID.Location = new Point(270, 219);
            txtForID.Name = "txtForID";
            txtForID.Size = new Size(636, 51);
            txtForID.TabIndex = 10;
            // 
            // btnForJoinGame
            // 
            btnForJoinGame.BackColor = Color.FromArgb(233, 243, 255);
            btnForJoinGame.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForJoinGame.FlatStyle = FlatStyle.Flat;
            btnForJoinGame.Font = new Font("Sitka Text", 22.2F, FontStyle.Bold, GraphicsUnit.Point);
            btnForJoinGame.ForeColor = Color.FromArgb(155, 194, 216);
            btnForJoinGame.Location = new Point(354, 276);
            btnForJoinGame.Name = "btnForJoinGame";
            btnForJoinGame.Size = new Size(450, 100);
            btnForJoinGame.TabIndex = 9;
            btnForJoinGame.Text = "Присоединиться";
            btnForJoinGame.UseVisualStyleBackColor = false;
            btnForJoinGame.Click += btnForJoinGame_Click_1;
            // 
            // btnFor
            // 
            btnFor.BackColor = Color.FromArgb(233, 243, 255);
            btnFor.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnFor.FlatStyle = FlatStyle.Flat;
            btnFor.Font = new Font("Sitka Text", 22.2F, FontStyle.Bold, GraphicsUnit.Point);
            btnFor.ForeColor = Color.FromArgb(155, 194, 216);
            btnFor.Location = new Point(354, 419);
            btnFor.Name = "btnFor";
            btnFor.Size = new Size(450, 92);
            btnFor.TabIndex = 8;
            btnFor.Text = "Выход";
            btnFor.UseVisualStyleBackColor = false;
            btnFor.Click += btnFor_Click;
            // 
            // btnForStart
            // 
            btnForStart.BackColor = Color.FromArgb(233, 243, 255);
            btnForStart.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForStart.FlatStyle = FlatStyle.Flat;
            btnForStart.Font = new Font("Sitka Text", 22.2F, FontStyle.Bold, GraphicsUnit.Point);
            btnForStart.ForeColor = Color.FromArgb(155, 194, 216);
            btnForStart.Location = new Point(354, 67);
            btnForStart.Name = "btnForStart";
            btnForStart.Size = new Size(450, 89);
            btnForStart.TabIndex = 7;
            btnForStart.Text = "Начать игру";
            btnForStart.UseVisualStyleBackColor = false;
            btnForStart.Click += btnForStart_Click;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1188, 631);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Меню";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnForStart;
        private Button btnFor;
        private Button btnForJoinGame;
        private TextBox txtForID;
        private Label lblForID;
        private Label lblForStartNewGame;
    }
}