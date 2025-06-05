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
            resources.ApplyResources(panel1, "panel1");
            panel1.BackColor = Color.FromArgb(100, 180, 220, 250);
            panel1.Controls.Add(lblForStartNewGame);
            panel1.Controls.Add(lblForID);
            panel1.Controls.Add(txtForID);
            panel1.Controls.Add(btnForJoinGame);
            panel1.Controls.Add(btnFor);
            panel1.Controls.Add(btnForStart);
            panel1.Name = "panel1";
            // 
            // lblForStartNewGame
            // 
            resources.ApplyResources(lblForStartNewGame, "lblForStartNewGame");
            lblForStartNewGame.BackColor = Color.White;
            lblForStartNewGame.ForeColor = Color.FromArgb(49, 56, 87);
            lblForStartNewGame.Name = "lblForStartNewGame";
            // 
            // lblForID
            // 
            resources.ApplyResources(lblForID, "lblForID");
            lblForID.BackColor = Color.White;
            lblForID.ForeColor = Color.FromArgb(49, 56, 87);
            lblForID.Name = "lblForID";
            // 
            // txtForID
            // 
            txtForID.AcceptsReturn = true;
            resources.ApplyResources(txtForID, "txtForID");
            txtForID.Name = "txtForID";
            // 
            // btnForJoinGame
            // 
            resources.ApplyResources(btnForJoinGame, "btnForJoinGame");
            btnForJoinGame.BackColor = Color.FromArgb(233, 243, 255);
            btnForJoinGame.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForJoinGame.ForeColor = Color.FromArgb(155, 194, 216);
            btnForJoinGame.Name = "btnForJoinGame";
            btnForJoinGame.UseVisualStyleBackColor = false;
            btnForJoinGame.Click += btnForJoinGame_Click_1;
            // 
            // btnFor
            // 
            resources.ApplyResources(btnFor, "btnFor");
            btnFor.BackColor = Color.FromArgb(233, 243, 255);
            btnFor.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnFor.ForeColor = Color.FromArgb(155, 194, 216);
            btnFor.Name = "btnFor";
            btnFor.UseVisualStyleBackColor = false;
            btnFor.Click += btnFor_Click;
            // 
            // btnForStart
            // 
            resources.ApplyResources(btnForStart, "btnForStart");
            btnForStart.BackColor = Color.FromArgb(233, 243, 255);
            btnForStart.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForStart.ForeColor = Color.FromArgb(155, 194, 216);
            btnForStart.Name = "btnForStart";
            btnForStart.UseVisualStyleBackColor = false;
            btnForStart.Click += btnForStart_Click;
            // 
            // Menu
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Menu";
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