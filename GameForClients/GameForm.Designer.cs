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
            lblLogin = new Label();
            btnCopyId = new Button();
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
            resources.ApplyResources(panel2, "panel2");
            panel2.BackColor = Color.FromArgb(100, 180, 220, 250);
            panel2.Controls.Add(lblLogin);
            panel2.Controls.Add(btnCopyId);
            panel2.Controls.Add(panelEnemy);
            panel2.Controls.Add(panelPlayer);
            panel2.Controls.Add(btnExitFromGame);
            panel2.Controls.Add(btnForEnd);
            panel2.Controls.Add(lblEnemyName);
            panel2.Controls.Add(lblGamerName);
            panel2.Name = "panel2";
            // 
            // lblLogin
            // 
            resources.ApplyResources(lblLogin, "lblLogin");
            lblLogin.BackColor = Color.White;
            lblLogin.ForeColor = Color.FromArgb(49, 56, 87);
            lblLogin.Name = "lblLogin";
          
            // 
            // btnCopyId
            // 
            resources.ApplyResources(btnCopyId, "btnCopyId");
            btnCopyId.BackColor = Color.FromArgb(133, 143, 180);
            btnCopyId.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnCopyId.ForeColor = Color.FromArgb(49, 56, 87);
            btnCopyId.Name = "btnCopyId";
            btnCopyId.UseVisualStyleBackColor = false;
            btnCopyId.Click += btnCopyId_Click;
            // 
            // panelEnemy
            // 
            resources.ApplyResources(panelEnemy, "panelEnemy");
            panelEnemy.BackColor = Color.MidnightBlue;
            panelEnemy.Name = "panelEnemy";
            // 
            // panelPlayer
            // 
            resources.ApplyResources(panelPlayer, "panelPlayer");
            panelPlayer.BackColor = Color.MidnightBlue;
            panelPlayer.Name = "panelPlayer";
            // 
            // btnExitFromGame
            // 
            resources.ApplyResources(btnExitFromGame, "btnExitFromGame");
            btnExitFromGame.BackColor = Color.White;
            btnExitFromGame.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnExitFromGame.ForeColor = Color.FromArgb(49, 56, 87);
            btnExitFromGame.Name = "btnExitFromGame";
            btnExitFromGame.UseVisualStyleBackColor = false;
            btnExitFromGame.Click += btnExitFromGame_Click;
            // 
            // btnForEnd
            // 
            resources.ApplyResources(btnForEnd, "btnForEnd");
            btnForEnd.BackColor = Color.White;
            btnForEnd.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForEnd.ForeColor = Color.FromArgb(49, 56, 87);
            btnForEnd.Name = "btnForEnd";
            btnForEnd.UseVisualStyleBackColor = false;
            btnForEnd.Click += btnForEnd_Click;
            // 
            // lblEnemyName
            // 
            resources.ApplyResources(lblEnemyName, "lblEnemyName");
            lblEnemyName.BackColor = Color.FromArgb(133, 143, 180);
            lblEnemyName.ForeColor = Color.FromArgb(49, 56, 87);
            lblEnemyName.Name = "lblEnemyName";
            // 
            // lblGamerName
            // 
            resources.ApplyResources(lblGamerName, "lblGamerName");
            lblGamerName.BackColor = Color.FromArgb(133, 143, 180);
            lblGamerName.ForeColor = Color.FromArgb(49, 56, 87);
            lblGamerName.Name = "lblGamerName";
            // 
            // GameForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "GameForm";
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
        private Button btnCopyId;
        private Label lblLogin;
    }
}