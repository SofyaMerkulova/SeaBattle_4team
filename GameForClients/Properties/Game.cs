using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameForClients.Properties
{
    public partial class Game : Form
    {
        private List<ForCounterShips> enemyShips = new List<ForCounterShips>();
        private HashSet<Point> enemyHits = new HashSet<Point>();
        public Game()
        {
            InitializeComponent();
            this.Load += Game_Start;
        }

        private void Game_Start(object sender, EventArgs e)
        {
            CreateGridWithLabels(panelPlayer, false);
            CreateGridWithLabels(panelEnemy, true);
        }

        private void CreateGridWithLabels(Panel panel, bool isEnemy)
        {
            int cellSize = 30;

            panel.Controls.Clear();

            string[] russianLetters = { "А", "Б", "В", "Г", "Д", "Е", "Ж", "З", "И", "К" };
            var font = new Font("Arial", 10, FontStyle.Bold);
            var textColor = Color.White;
            for (int col = 0; col < 10; col++)
            {

                Label label = new Label
                {
                    Text = russianLetters[col],
                    Font = font,
                    ForeColor = textColor,
                    BackColor = Color.Transparent,
                };
                label.Text = russianLetters[col];
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Size = new Size(cellSize, cellSize);
                label.Location = new Point((col + 1) * cellSize, 0);
                label.BackColor = Color.Transparent;
                panel.Controls.Add(label);
            }
            for (int row = 0; row < 10; row++)
            {
                Label label = new Label
                {
                    Font = font,
                    ForeColor = textColor,
                    BackColor = Color.Transparent,
                };
                label.Text = (row + 1).ToString();
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Size = new Size(cellSize, cellSize);
                label.Location = new Point(0, (row + 1) * cellSize);
                label.BackColor = Color.Transparent;
                panel.Controls.Add(label);
            }
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(cellSize, cellSize);
                    btn.Location = new Point((col + 1) * cellSize, (row + 1) * cellSize);
                    btn.Tag = new Point(col, row);

                    if (isEnemy)
                        btn.Click += ClickForMoves;

                    panel.Controls.Add(btn);
                }
            }
            panel.Width = (11 * cellSize);
            panel.Height = (11 * cellSize);
        }

        private void ClickForMoves(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            Point coords = (Point)clickedButton.Tag;

            clickedButton.BackColor = Color.Gray;
            clickedButton.Enabled = false;

            enemyHits.Add(coords);

            int aliveShips = enemyShips.Count(ship => ship.IsAlive(enemyHits));
            this.Text = $"Осталось кораблей противника: {aliveShips}";
        }
    }
}

    

