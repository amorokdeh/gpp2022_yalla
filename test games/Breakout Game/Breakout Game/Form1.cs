using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout_Game
{
    public partial class Form1 : Form
    {

        bool goLeft;
        bool goRight;
        bool isGameOver;

        int score;
        int ballx;
        int bally;
        int playerSpeed;

        Random rnd = new Random();

        PictureBox[] blockArray;

        public Form1()
        {
            InitializeComponent();
            placeBlocks();
        }

        private void setupGame() {

            isGameOver = false;
            score = 0;
            ballx = 5;
            bally = 5;
            playerSpeed = 12;

            ball.Left = 298;
            ball.Top = 248;

            player.Left = 273;
            player.Top = 416;
            txtScore.Text = "Score: " + score;

            gameTimer.Start();

            foreach (Control x in this.Controls) {
                if (x is PictureBox && (String)x.Tag == "blocks") { 
                    x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                }
            }
        }

        private void placeBlocks()
        {

            blockArray = new PictureBox[12];

            int a = 0;
            int top = 50;
            int left = 70;

            for (int i = 0; i < blockArray.Length; i++) { 
                blockArray[i] = new PictureBox();
                blockArray[i].Height = 32;
                blockArray[i].Width = 70;
                blockArray[i].Tag = "blocks";
                blockArray[i].BackColor = Color.White;

                if (a == 5) {
                    top = top + 50;
                    left = 70;
                    a = 0;
                }

                if (a < 5) {
                    a++;
                    blockArray[i].Left = left;
                    blockArray[i].Top = top;
                    this.Controls.Add(blockArray[i]);
                    left = left + 100;
                }
            }

            setupGame();
        }

        private void gameOver(string message) {

            isGameOver = true;
            gameTimer.Stop();
            txtScore.Text = "Score: " + score + " " + message;
        }

        private void removeBlocks() {

            foreach (PictureBox x in blockArray) { 
                this.Controls.Remove(x);
            }
        }
        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;

            if (goLeft == true && player.Left > 15) {  
                player.Left -= playerSpeed;
            }

            if (goRight == true && player.Left < 530)
            {
                player.Left += playerSpeed;
            }

            ball.Left += ballx;
            ball.Top += bally;

            if (ball.Left < 0 || ball.Left > 600) {
                ballx = -ballx;
            }

            if (ball.Top < 0)
            {
                bally = -bally;
            }

            //collide with player
            if (ball.Bounds.IntersectsWith(player.Bounds)) {
                ball.Top = 397;
                bally = -bally;
            }

            //collide with blocks
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (String)x.Tag == "blocks")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds)) {
                        score += 1;
                        bally = -bally;
                        this.Controls.Remove(x);
                    }
                }
            }

            //win
            if (score == 12) {
                gameOver("You win :) Press Enter to restart the game");
            }

            //lose
            if (ball.Top > 530) {
                gameOver("You lose :( Press Enter to restart the game");
            }

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { 
            
                goLeft = true;

            }

            if (e.KeyCode == Keys.Right)
            {

                goRight = true;

            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                removeBlocks();
                placeBlocks();
            }

        }
    }
}
