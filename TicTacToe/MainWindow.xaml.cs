using System;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private Button[] buttons;
        private string currentPlayer;
        private bool gameOver;
        private string AiPlayer;

        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[] { Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9 };
            currentPlayer = "X";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Content = currentPlayer;
            button.IsEnabled = false;

            if (IsWinningMoveForCurrentPlayer())
            {
                MessageBox.Show($"{currentPlayer} wins!");
                gameOver = true;
                DisableButtons();
            }
            else if (IsGameDraw())
            {
                MessageBox.Show("Draw!");
                gameOver = true;
                DisableButtons();
            }
            else
            {
                if (currentPlayer == "X")
                {     
                    MakeAIMove();
                }
                else
                {
                    currentPlayer = "X";
                }
            }
        }

        private void MakeAIMove()
        {
            int bestScore = int.MinValue;
            int bestMove = -1;

            for (int i = 0; i < 9; i++)
            {
                if (buttons[i].Content == "" || buttons[i].Content == null)
                {
                    buttons[i].Content = "O";

                    int score = Minimax(false);

                    buttons[i].Content = "";


                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = i;
                    }
                }
            }

            buttons[bestMove].Content = "O";
            buttons[bestMove].IsEnabled = false;

            if (IsWinningMoveForCurrentPlayer("O"))
            {
                MessageBox.Show("AI wins!");
                gameOver = true;
                DisableButtons();
            }


        }

        private int Minimax(bool isMaximizing)
        {
            if (IsWinningMoveForCurrentPlayer("X"))
            {
                return -10;
            }
            else if (IsWinningMoveForCurrentPlayer("O"))
            {
                return 10;
            }
            else if (IsGameDraw())
            {
                return 0;
            }

            if (isMaximizing)
            {
                int bestScore = int.MinValue;

                for (int i = 0; i < 9; i++)
                {
                    if (buttons[i].Content == "" || buttons[i].Content == null)
                    {
                        buttons[i].Content = "O";

                        int score = Minimax(false);

                        buttons[i].Content = "";

                        bestScore = Math.Max(score, bestScore);
                    }
                }

                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;

                for (int i = 0; i < 9; i++)
                {
                    if (buttons[i].Content == "" || buttons[i].Content == null)
                    {
                        buttons[i].Content = "X";

                        int score = Minimax(true);

                        buttons[i].Content = "";

                        bestScore = Math.Min(score, bestScore);
                    }
                }

                return bestScore;
            }
        }
        private bool IsWinningMoveForCurrentPlayer(string player)
        {
            // Check rows
            for (int i = 0; i < 9; i += 3)
            {
                if (buttons[i].Content == player && buttons[i + 1].Content == player && buttons[i + 2].Content == player)
                    return true;
            }
            // Check columns
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i].Content == player && buttons[i + 3].Content == player && buttons[i + 6].Content == player)
                    return true;
            }

            // Check diagonals
            if (buttons[0].Content == player && buttons[4].Content == player && buttons[8].Content == player)
                return true;

            if (buttons[2].Content == player && buttons[4].Content == player && buttons[6].Content == player)
                return true;

            return false;
        }

        private bool IsWinningMoveForCurrentPlayer()
        {
            return IsWinningMoveForCurrentPlayer(currentPlayer);
        }

        private bool IsGameDraw()
        {
            // Check if all squares are filled and no one has won
            foreach (Button button in buttons)
            {
                if (button.Content == null || button.Content == "")
                    return false;
            }

            if (IsWinningMoveForCurrentPlayer())
                return false;

            return true;
        }

        private void DisableButtons()
        {
            foreach (Button button in buttons)
            {
                button.IsEnabled = false;
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button button in buttons)
            {
                button.Content = null;
                button.IsEnabled = true;
            }
            gameOver = false;


        }
    }
}