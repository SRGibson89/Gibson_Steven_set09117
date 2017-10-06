using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int pturn;
        public int[,] Board = new int[9, 9];
        public string x = "";
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btngo_Click(object sender, RoutedEventArgs e)
        {
            txtbox.Clear();


            //build board inthe array
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    Board[i, j] = i;
                    //lstbox.Items.Add(Board[i, j]);

                }
            }

            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Board[i, j] = j;
                    //lstbox.Items.Add(Board[i, j]);

                }
            }

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    Board[i, j] = 9;

                    //lstbox.Items.Add(Board[i, j]);
                }
            }
            //blank Sqaures
            for (int i = 5; i < 6; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    Board[i, j] = 6;
                    j++;


                    //lstbox.Items.Add(Board[i, j]);
                }
                i++;
            }

            for (int i = 4; i < 6; i++)
            {
                for (int j = 2; j < 9; j++)
                {
                    Board[i, j] = 6;
                    j++;


                    //lstbox.Items.Add(Board[i, j]);
                }
                i++;
            }
            //Player 1 pieces in place on board
            for (int i = 6; i < 9; i++)
            {
                for (int j = 2; j < 9; j++)
                {
                    Board[i, j] = 1;
                    j++;


                    //lstbox.Items.Add(Board[i, j]);
                }
                i++;
            }

            for (int i = 7; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    Board[i, j] = 1;
                    j++;


                    //lstbox.Items.Add(Board[i, j]);
                }
                i++;
            }
            //player 2 pieces in place n board
            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    Board[i, j] = 2;
                    j++;


                    //lstbox.Items.Add(Board[i, j]);
                }
                i++;
            }

            for (int i = 2; i < 4; i++)
            {
                for (int j = 2; j < 9; j++)
                {
                    Board[i, j] = 2;
                    j++;


                    //lstbox.Items.Add(Board[i, j]);
                }
                i++;
            }
            //Print board

            for (int i = 1; i < Board.GetLength(0); i++)
            {
                for (int j = 1; j < Board.GetLength(1); j++)
                {
                    if (Board[i, j] == 9)
                    {
                        txtbox.Text = txtbox.Text + "X" + "\t";

                    }
                    else if (Board[i, j] == 1)
                    {
                        txtbox.Text = txtbox.Text + "W" + "\t";

                    }
                    else if (Board[i, j] == 2)
                    {
                        txtbox.Text = txtbox.Text + "B" + "\t";

                    }
                    else if (Board[i, j] == 6)
                    {
                        txtbox.Text = txtbox.Text + "O" + "\t";

                    }
                    //else
                    //{
                    //    txtbox.Text = txtbox.Text + "\t" + Board[i, j].ToString();
                    //}
                }
                txtbox.Text = txtbox.Text + "\n";
            }

        }
    }
}