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
            btnFor = new Button();
            btnForStars = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(100, 180, 220, 250);
            panel1.Controls.Add(btnFor);
            panel1.Controls.Add(btnForStars);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1188, 631);
            panel1.TabIndex = 0;
            // 
            // btnFor
            // 
            btnFor.BackColor = Color.FromArgb(233, 243, 255);
            btnFor.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnFor.FlatStyle = FlatStyle.Flat;
            btnFor.Font = new Font("Sitka Text", 22.2F, FontStyle.Bold, GraphicsUnit.Point);
            btnFor.ForeColor = Color.FromArgb(155, 194, 216);
            btnFor.Location = new Point(354, 319);
            btnFor.Name = "btnFor";
            btnFor.Size = new Size(450, 92);
            btnFor.TabIndex = 8;
            btnFor.Text = "Выход";
            btnFor.UseVisualStyleBackColor = false;
            // 
            // btnForStars
            // 
            btnForStars.BackColor = Color.FromArgb(233, 243, 255);
            btnForStars.FlatAppearance.BorderColor = Color.FromArgb(133, 143, 180);
            btnForStars.FlatStyle = FlatStyle.Flat;
            btnForStars.Font = new Font("Sitka Text", 22.2F, FontStyle.Bold, GraphicsUnit.Point);
            btnForStars.ForeColor = Color.FromArgb(155, 194, 216);
            btnForStars.Location = new Point(354, 154);
            btnForStars.Name = "btnForStars";
            btnForStars.Size = new Size(450, 89);
            btnForStars.TabIndex = 7;
            btnForStars.Text = "Начать игру";
            btnForStars.UseVisualStyleBackColor = false;
     
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
            MinimizeBox = false;
            Name = "Menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Меню";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnForStars;
        private Button btnFor;
    }
}