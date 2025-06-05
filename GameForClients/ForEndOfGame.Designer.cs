namespace GameForClients
{
    partial class ForEndOfGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForEndOfGame));
            panel1 = new Panel();
            lblGameName = new Label();
            btnForAgainStartGame = new Button();
            btnForEndGame = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.BackColor = Color.FromArgb(100, 180, 220, 250);
            panel1.Controls.Add(lblGameName);
            panel1.Controls.Add(btnForAgainStartGame);
            panel1.Controls.Add(btnForEndGame);
            panel1.Name = "panel1";
            // 
            // lblGameName
            // 
            resources.ApplyResources(lblGameName, "lblGameName");
            lblGameName.BackColor = Color.White;
            lblGameName.ForeColor = Color.FromArgb(49, 56, 87);
            lblGameName.Name = "lblGameName";
            
            // 
            // btnForAgainStartGame
            // 
            resources.ApplyResources(btnForAgainStartGame, "btnForAgainStartGame");
            btnForAgainStartGame.BackColor = Color.FromArgb(78, 124, 178);
            btnForAgainStartGame.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForAgainStartGame.ForeColor = Color.White;
            btnForAgainStartGame.Name = "btnForAgainStartGame";
            btnForAgainStartGame.UseVisualStyleBackColor = false;
            btnForAgainStartGame.Click += btnForAgainStartGame_Click;
            // 
            // btnForEndGame
            // 
            resources.ApplyResources(btnForEndGame, "btnForEndGame");
            btnForEndGame.BackColor = Color.FromArgb(78, 124, 178);
            btnForEndGame.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForEndGame.ForeColor = Color.White;
            btnForEndGame.Name = "btnForEndGame";
            btnForEndGame.UseVisualStyleBackColor = false;
            btnForEndGame.Click += btnForEndGame_Click;
            // 
            // ForEndOfGame
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            MaximizeBox = false;
            Name = "ForEndOfGame";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnForAgainStartGame;
        private Button btnForEndGame;
        private Label lblGameName;
    }
}