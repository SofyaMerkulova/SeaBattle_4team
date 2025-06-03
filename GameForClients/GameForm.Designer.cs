namespace GameForClients
{
    partial class GameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            panel2 = new Panel();
            panelEnemy = new Panel();
            panelPlayer = new Panel();
            btnExitFromGame = new Button();
            btnForEnd = new Button();
            lblEnemyName = new Label();
            lblGamerName = new Label();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(100, 180, 220, 250);
            panel2.Controls.Add(panelEnemy);
            panel2.Controls.Add(panelPlayer);
            panel2.Controls.Add(btnExitFromGame);
            panel2.Controls.Add(btnForEnd);
            panel2.Controls.Add(lblEnemyName);
            panel2.Controls.Add(lblGamerName);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1013, 620);
            panel2.TabIndex = 1;
            // 
            // panelEnemy
            // 
            panelEnemy.Anchor = AnchorStyles.None;
            panelEnemy.BackColor = Color.MidnightBlue;
            panelEnemy.Location = new Point(463, 81);
            panelEnemy.Name = "panelEnemy";
            panelEnemy.Size = new Size(454, 454);
            panelEnemy.TabIndex = 10;
            // 
            // panelPlayer
            // 
            panelPlayer.Anchor = AnchorStyles.None;
            panelPlayer.BackColor = Color.MidnightBlue;
            panelPlayer.Location = new Point(3, 81);
            panelPlayer.Name = "panelPlayer";
            panelPlayer.Size = new Size(454, 454);
            panelPlayer.TabIndex = 9;
            // 
            // btnExitFromGame
            // 
            btnExitFromGame.BackColor = Color.White;
            btnExitFromGame.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnExitFromGame.FlatStyle = FlatStyle.Flat;
            btnExitFromGame.Font = new Font("Sitka Text", 16.1999989F, FontStyle.Bold, GraphicsUnit.Point);
            btnExitFromGame.ForeColor = Color.FromArgb(49, 56, 87);
            btnExitFromGame.Location = new Point(505, 541);
            btnExitFromGame.Name = "btnExitFromGame";
            btnExitFromGame.Size = new Size(344, 76);
            btnExitFromGame.TabIndex = 8;
            btnExitFromGame.Text = "Выйти из игры";
            btnExitFromGame.UseVisualStyleBackColor = false;
            btnExitFromGame.Click += btnExitFromGame_Click;
            // 
            // btnForEnd
            // 
            btnForEnd.BackColor = Color.White;
            btnForEnd.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForEnd.FlatStyle = FlatStyle.Flat;
            btnForEnd.Font = new Font("Sitka Text", 16.1999989F, FontStyle.Bold, GraphicsUnit.Point);
            btnForEnd.ForeColor = Color.FromArgb(49, 56, 87);
            btnForEnd.Location = new Point(66, 541);
            btnForEnd.Name = "btnForEnd";
            btnForEnd.Size = new Size(344, 76);
            btnForEnd.TabIndex = 7;
            btnForEnd.Text = "Завершить и сдаться";
            btnForEnd.UseVisualStyleBackColor = false;
            // 
            // lblEnemyName
            // 
            lblEnemyName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblEnemyName.AutoSize = true;
            lblEnemyName.BackColor = Color.FromArgb(133, 143, 180);
            lblEnemyName.Font = new Font("Sitka Text", 22.2F, FontStyle.Bold, GraphicsUnit.Point);
            lblEnemyName.ForeColor = Color.FromArgb(49, 56, 87);
            lblEnemyName.Location = new Point(463, 25);
            lblEnemyName.Name = "lblEnemyName";
            lblEnemyName.Size = new Size(361, 53);
            lblEnemyName.TabIndex = 3;
            lblEnemyName.Text = "Поле противника";
            // 
            // lblGamerName
            // 
            lblGamerName.AutoSize = true;
            lblGamerName.BackColor = Color.FromArgb(133, 143, 180);
            lblGamerName.Font = new Font("Sitka Text", 22.2F, FontStyle.Bold, GraphicsUnit.Point);
            lblGamerName.ForeColor = Color.FromArgb(49, 56, 87);
            lblGamerName.Location = new Point(27, 25);
            lblGamerName.Name = "lblGamerName";
            lblGamerName.Size = new Size(225, 53);
            lblGamerName.TabIndex = 2;
            lblGamerName.Text = "Ваше поле";
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1013, 620);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "GameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Игровые поля";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private Label lblGamerName;
        private Label lblEnemyName;
        private Button btnExitFromGame;
        private Button btnForEnd;
        private Panel panelEnemy;
        private Panel panelPlayer;
    }
}