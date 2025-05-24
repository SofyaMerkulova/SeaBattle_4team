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
            btnForAgainStartGame = new Button();
            btnForEndGame = new Button();
            lblGameName = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(100, 180, 220, 250);
            panel1.Controls.Add(lblGameName);
            panel1.Controls.Add(btnForAgainStartGame);
            panel1.Controls.Add(btnForEndGame);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1188, 631);
            panel1.TabIndex = 1;
            // 
            // btnForAgainStartGame
            // 
            btnForAgainStartGame.BackColor = Color.FromArgb(78, 124, 178);
            btnForAgainStartGame.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForAgainStartGame.FlatStyle = FlatStyle.Flat;
            btnForAgainStartGame.Font = new Font("Sitka Text", 22.2F, FontStyle.Bold, GraphicsUnit.Point);
            btnForAgainStartGame.ForeColor = Color.White;
            btnForAgainStartGame.Location = new Point(354, 478);
            btnForAgainStartGame.Name = "btnForAgainStartGame";
            btnForAgainStartGame.Size = new Size(450, 92);
            btnForAgainStartGame.TabIndex = 8;
            btnForAgainStartGame.Text = "Сыграть повторно";
            btnForAgainStartGame.UseVisualStyleBackColor = false;
            // 
            // btnForEndGame
            // 
            btnForEndGame.BackColor = Color.FromArgb(78, 124, 178);
            btnForEndGame.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForEndGame.FlatStyle = FlatStyle.Flat;
            btnForEndGame.Font = new Font("Sitka Text", 22.2F, FontStyle.Bold, GraphicsUnit.Point);
            btnForEndGame.ForeColor = Color.White;
            btnForEndGame.Location = new Point(354, 336);
            btnForEndGame.Name = "btnForEndGame";
            btnForEndGame.Size = new Size(450, 89);
            btnForEndGame.TabIndex = 7;
            btnForEndGame.Text = "Завершить игру";
            btnForEndGame.UseVisualStyleBackColor = false;
            // 
            // lblGameName
            // 
            lblGameName.AutoSize = true;
            lblGameName.BackColor = Color.White;
            lblGameName.Font = new Font("Sitka Text", 48F, FontStyle.Bold, GraphicsUnit.Point);
            lblGameName.ForeColor = Color.FromArgb(49, 56, 87);
            lblGameName.Location = new Point(265, 130);
            lblGameName.Name = "lblGameName";
            lblGameName.Size = new Size(694, 116);
            lblGameName.TabIndex = 9;
            lblGameName.Text = "Результат игры";
            // 
            // ForEndOfGame
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1188, 631);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ForEndOfGame";
            Text = "Итоги игры";
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