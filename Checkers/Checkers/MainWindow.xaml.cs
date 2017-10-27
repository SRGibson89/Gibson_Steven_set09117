using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Move currentMove;
        private String winner;
        private String turn = "Black";
        public String playername1 = "White", playername2 = "Black";
        public int numPlayers = 2;
        System.Media.SoundPlayer winnermusic = new System.Media.SoundPlayer(@"Resources/Winner.wav");
        System.Media.SoundPlayer backgroundmusic = new System.Media.SoundPlayer(@"Resources/Background.wav");
        Stack Undo_Stack = new Stack();
        Stack Taken_Stack = new Stack();
        Stack Redo_Stack = new Stack();
        Stack ReTaken_Stack = new Stack();
        SingletonLists Game_List = SingletonLists.Instance;
        private int refid = 0;



        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Draughts";
            BuildBoard();
            newGame();
            changeTurn();

        }

        private void BuildBoard()
        {
            var BoardBlack = (SolidColorBrush)new BrushConverter().ConvertFromString("#CF9C63");
            var BoardWhite = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFFF7");


            for (int row = 1; row < 9; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    StackPanel stackPanel = new StackPanel();
                    if (row % 2 == 1)
                    {
                        if (column % 2 == 0)
                            stackPanel.Background = BoardWhite;
                        else
                            stackPanel.Background = BoardBlack;
                    }
                    else
                    {
                        if (column % 2 == 0)
                            stackPanel.Background = BoardBlack;
                        else
                            stackPanel.Background = BoardWhite;
                    }
                    Grid.SetRow(stackPanel, row);
                    Grid.SetColumn(stackPanel, column);
                    DraughtsBoard.Children.Add(stackPanel);
                }
            }
            Place_Markers();
        }

        private void Place_Markers()
        {
            var BoardBlack = (SolidColorBrush)new BrushConverter().ConvertFromString("#CF9C63");
            for (int row = 1; row < 9; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    StackPanel stackPanel = (StackPanel)GetGridElement(DraughtsBoard, row, column);
                    Button button = new Button();
                    button.Click += new RoutedEventHandler(Button_Click);
                    button.Height = 60;
                    button.Width = 60;
                    button.HorizontalAlignment = HorizontalAlignment.Center;
                    button.VerticalAlignment = VerticalAlignment.Center;
                    var WhiteBrush = new ImageBrush();
                    WhiteBrush.ImageSource = new BitmapImage(new Uri("Resources/WhiteMarker.png", UriKind.Relative));
                    var BlackBrush = new ImageBrush();
                    BlackBrush.ImageSource = new BitmapImage(new Uri("Resources/BlackMarker.png", UriKind.Relative));
                    switch (row)
                    {
                        case 1:
                            if (column % 2 == 1)
                            {

                                button.Background = WhiteBrush;
                                button.Name = "buttonWhite" + row + column;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 2:
                            if (column % 2 == 0)
                            {
                                button.Background = WhiteBrush;
                                button.Name = "buttonWhite" + row + column;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 3:
                            if (column % 2 == 1)
                            {
                                button.Background = WhiteBrush;
                                button.Name = "buttonWhite" + row + column;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 4:
                            if (column % 2 == 0)
                            {
                                button.Background = BoardBlack;
                                button.Name = "button" + row + column;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 5:
                            if (column % 2 == 1)
                            {
                                button.Background = BoardBlack;
                                button.Name = "button" + row + column;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 6:
                            if (column % 2 == 0)
                            {
                                button.Background = BlackBrush;
                                button.Name = "buttonBlack" + row + column;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 7:
                            if (column % 2 == 1)
                            {
                                button.Background = BlackBrush;
                                button.Name = "buttonBlack" + row + column;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        case 8:
                            if (column % 2 == 0)
                            {
                                button.Background = BlackBrush;
                                button.Name = "buttonBlack" + row + column;
                                stackPanel.Children.Add(button);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        UIElement GetGridElement(Grid grid, int row, int column)
        {
            for (int i = 0; i < grid.Children.Count; i++)
            {
                UIElement e = grid.Children[i];
                if (Grid.GetRow(e) == row && Grid.GetColumn(e) == column)
                    return e;
            }
            return null;
        }

        private void Cleaner()
        {
            for (int row = 1; row < 9; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    StackPanel stackPanel = (StackPanel)GetGridElement(DraughtsBoard, row, column);
                    DraughtsBoard.Children.Remove(stackPanel);
                }
            }
            Undo_Stack.Clear();
            Redo_Stack.Clear();
            Taken_Stack.Clear();
            ReTaken_Stack.Clear();
        }

        public void Button_Click(Object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel stackPanel = (StackPanel)button.Parent;
            int row = Grid.GetRow(stackPanel);
            int col = Grid.GetColumn(stackPanel);
            Console.WriteLine("Row: " + row + " Column: " + col);
            if (currentMove == null)
                currentMove = new Move();
            if (currentMove.markerBefore == null)
            {
                currentMove.markerBefore = new Marker(row, col);
                stackPanel.Background = Brushes.SkyBlue;
            }
            else
            {
                currentMove.markerAfter = new Marker(row, col);
                stackPanel.Background = Brushes.SkyBlue;
            }
            if ((currentMove.markerBefore != null) && (currentMove.markerAfter != null))
            {
                if (numPlayers == 1)
                {
                    if (CheckMove())
                    {
                        MakeMove();
                         AiTurn();
                        MessageBox.Show("boo");
                    
                    }

                }
                else if (numPlayers == 2)
                {
                    if (CheckMove())
                    {
                        MakeMove();
                        changeTurn();
                    }

                }
            }
        }

        private void changeTurn()
        {
            if (turn == "Black")
            {
                turn = "White";
                lblturn.Content = playername1 + " Turn!";
            }
            else if (turn == "White")
            {
                turn = "Black";
                lblturn.Content = playername2 + " Turn!";
            }
        }

        private void MakeMove()
        {
            if ((currentMove.markerBefore != null) && (currentMove.markerAfter != null))
            {
                Console.WriteLine("Marker1 " + currentMove.markerBefore.Row + ", " + currentMove.markerBefore.Column);
                Console.WriteLine("Marker2 " + currentMove.markerAfter.Row + ", " + currentMove.markerAfter.Column);
                StackPanel stackPanel1 = (StackPanel)GetGridElement(DraughtsBoard, currentMove.markerBefore.Row, currentMove.markerBefore.Column);
                StackPanel stackPanel2 = (StackPanel)GetGridElement(DraughtsBoard, currentMove.markerAfter.Row, currentMove.markerAfter.Column);
                DraughtsBoard.Children.Remove(stackPanel1);
                DraughtsBoard.Children.Remove(stackPanel2);
                Grid.SetRow(stackPanel1, currentMove.markerAfter.Row);
                Grid.SetColumn(stackPanel1, currentMove.markerAfter.Column);
                DraughtsBoard.Children.Add(stackPanel1);
                Grid.SetRow(stackPanel2, currentMove.markerBefore.Row);
                Grid.SetColumn(stackPanel2, currentMove.markerBefore.Column);
                DraughtsBoard.Children.Add(stackPanel2);

                KingMe(currentMove.markerAfter);
                addToUndo(currentMove.markerBefore, currentMove.markerAfter);
                AddToHistory(currentMove.markerBefore, currentMove.markerAfter);

                currentMove = null;

            }

            CheckforWinnner();
        }

        private void addToUndo(Marker markerbefore, Marker markerafter)
        {
            Undo_Stack.Push(markerbefore.Row);
            Undo_Stack.Push(markerbefore.Column);
            Undo_Stack.Push(markerbefore.Kinged);
            Undo_Stack.Push(markerafter.Row);
            Undo_Stack.Push(markerafter.Column);
            Undo_Stack.Push(markerafter.Kinged);

        }

        //Promtions
        private void KingMe(Marker marker)
        {
            StackPanel stackPanel = (StackPanel)GetGridElement(DraughtsBoard, marker.Row, marker.Column);

            if (stackPanel.Children.Count > 0)
            {
                var WhiteKingBrush = new ImageBrush();
                var BlackKingBrush = new ImageBrush();
                WhiteKingBrush.ImageSource = new BitmapImage(new Uri("Resources/WhiteKingMarker.png", UriKind.Relative));
                BlackKingBrush.ImageSource = new BitmapImage(new Uri("Resources/BlackKingMarker.png", UriKind.Relative));
                Button button = (Button)stackPanel.Children[0];
                //white marker gets to the other side of the board
                if (marker.Row == 8)
                {
                    if ((button.Name.Contains("White")) && (!button.Name.Contains("WhiteKing")))
                    {
                        button.Name = "White" + "WhiteKing" + marker.Row + marker.Column;
                        button.Background = WhiteKingBrush;
                        marker.Kinged = true;
                    }
                }
                //black marker gets to the other side of the board
                if (marker.Row == 1)
                {
                    if ((button.Name.Contains("Black")) && (!button.Name.Contains("BlackKing")))
                    {
                        button.Name = "Black" + "BlackKing" + marker.Row + marker.Column;
                        button.Background = BlackKingBrush;
                        marker.Kinged = true;
                    }
                }
            }
        }

        private void CheckforWinnner()
        {
            int Whites = 0, Blacks = 0;

            for (int row = 1; row < 9; row++)
            {

                for (int column = 0; column < 8; column++)
                {
                    StackPanel stackPanel = (StackPanel)GetGridElement(DraughtsBoard, row, column);
                    if (stackPanel.Children.Count > 0)
                    {
                        Button button = (Button)stackPanel.Children[0];
                        if (button.Name.Contains("White"))
                            Whites++;
                        if (button.Name.Contains("Black"))
                            Blacks++;
                    }
                }
            }

            if (Whites == 0)
            {
                winner = "Black";
                winnermusic.Play();
                var result = MessageBox.Show(playername2 + " has won!", "Winner", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    newGame();
                }
            }

            if (Blacks == 0)
            {
                winner = "White";
                winnermusic.Play();
                var result = MessageBox.Show(playername1 + " has won!", "Winner", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    newGame();
                }
            }

        }

        private Boolean CheckMove()
        {
            var BoardBlack = (SolidColorBrush)new BrushConverter().ConvertFromString("#CF9C63");
            StackPanel stackPanel1 = (StackPanel)GetGridElement(DraughtsBoard, currentMove.markerBefore.Row, currentMove.markerBefore.Column);
            StackPanel stackPanel2 = (StackPanel)GetGridElement(DraughtsBoard, currentMove.markerAfter.Row, currentMove.markerAfter.Column);
            Button button1 = (Button)stackPanel1.Children[0];
            Button button2 = (Button)stackPanel2.Children[0];
            stackPanel1.Background = BoardBlack;
            stackPanel2.Background = BoardBlack;

            if ((turn == "White") && (button1.Name.Contains("Black")))
            {
                currentMove.markerBefore = null;
                currentMove.markerAfter = null;
                showError("It is " + playername1 + " turn.");
                return false;
            }
            if ((turn == "Black") && (button1.Name.Contains("White")))
            {
                currentMove.markerBefore = null;
                currentMove.markerAfter = null;
                showError("It is " + playername2 + " turn.");
                return false;
            }
            if (button1.Equals(button2))
            {
                currentMove.markerBefore = null;
                currentMove.markerAfter = null;
                return false;
            }
            if (button1.Name.Contains("Black"))
            {
                return CheckMoveBlack(button1, button2);
            }
            else if (button1.Name.Contains("White"))
            {
                return CheckMoveWhite(button1, button2);
            }
            else
            {
                currentMove.markerBefore = null;
                currentMove.markerAfter = null;
                Console.WriteLine("False");
                return false;
            }
        }

        private bool CheckMoveWhite(Button button1, Button button2)
        {
            Checkers_Board currentBoard = GetBoard();
            List<Move> playerMoves = currentBoard.CheckJumps("White");

            if (playerMoves.Count > 0)
            {
                bool invalid = true;
                foreach (Move move in playerMoves)
                {
                    if (currentMove.Equals(move))
                        invalid = false;
                }
                if (invalid)
                {
                    showError("Jump must be taken");
                    currentMove.markerBefore = null;
                    currentMove.markerAfter = null;
                    Console.WriteLine("False");
                    return false;
                }
            }

            if (button1.Name.Contains("White"))
            {
                if (button1.Name.Contains("WhiteKing"))
                {
                    if ((currentMove.ValidMove("WhiteKing")) && (!button2.Name.Contains("White")) && (!button2.Name.Contains("Black")))
                        return true;
                    Marker middleMarker = currentMove.checkJumps("WhiteKing");
                    if ((middleMarker != null) && (!button2.Name.Contains("White")) && (!button2.Name.Contains("Black")))
                    {
                        StackPanel middleStackPanel = (StackPanel)GetGridElement(DraughtsBoard, middleMarker.Row, middleMarker.Column);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        if (middleButton.Name.Contains("Black"))
                        {
                            DraughtsBoard.Children.Remove(middleStackPanel);
                            Console.WriteLine("Button name" + middleButton.Name);
                            if (middleButton.Name.Contains("BlackKing"))
                            {
                                middleMarker.Kinged = true;
                            }
                            RemoveMarker(middleMarker);
                            return true;
                        }

                    }
                }
                else
                {
                    if ((currentMove.ValidMove("White")) && (!button2.Name.Contains("White")) && (!button2.Name.Contains("Black")))
                        return true;
                    Marker middleMarker = currentMove.checkJumps("White");
                    if ((middleMarker != null) && (!button2.Name.Contains("White")) && (!button2.Name.Contains("Black")))
                    {
                        StackPanel middleStackPanel = (StackPanel)GetGridElement(DraughtsBoard, middleMarker.Row, middleMarker.Column);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        if (middleButton.Name.Contains("Black"))
                        {
                            DraughtsBoard.Children.Remove(middleStackPanel);
                            Console.WriteLine("Button name" + middleButton.Name);
                            if (middleButton.Name.Contains("BlackKing"))
                            {
                                middleMarker.Kinged = true;
                            }
                            RemoveMarker(middleMarker);
                            return true;
                        }
                    }
                }
            }
            currentMove = null;
            showError("Invalid Move. Try Again.");
            return false;
        }

        private void RemoveMarker(Marker middleMove)
        {
            Taken_Stack.Push(middleMove.Row);
            Taken_Stack.Push(middleMove.Column);
            Taken_Stack.Push(middleMove.Kinged);

            Console.WriteLine("was it a king?" + middleMove.Kinged);
            var BoardBlack = (SolidColorBrush)new BrushConverter().ConvertFromString("#CF9C63");
            StackPanel stackPanel = new StackPanel();
            stackPanel.Background = BoardBlack;
            Button button = new Button();
            button.Click += new RoutedEventHandler(Button_Click);
            button.Height = 60;
            button.Width = 60;
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Background = BoardBlack;
            button.Name = "button" + middleMove.Row + middleMove.Column;
            stackPanel.Children.Add(button);
            Grid.SetColumn(stackPanel, middleMove.Column);
            Grid.SetRow(stackPanel, middleMove.Row);
            DraughtsBoard.Children.Add(stackPanel);
        }

        private bool CheckMoveBlack(Button button1, Button button2)
        {
            Checkers_Board currentBoard = GetBoard();
            List<Move> playerMoves = currentBoard.CheckJumps("Black");

            if (playerMoves.Count > 0)
            {
                bool invalid = true;
                foreach (Move move in playerMoves)
                {
                    if (currentMove.Equals(move))
                        invalid = false;
                }
                if (invalid)
                {
                    showError("Jump must be taken");
                    currentMove.markerBefore = null;
                    currentMove.markerAfter = null;
                    Console.WriteLine("False");
                    return false;
                }
            }

            if (button1.Name.Contains("Black"))
            {
                if (button1.Name.Contains("BlackKing"))
                {
                    if ((currentMove.ValidMove("BlackKing")) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("White")))
                        return true;
                    Marker middleMarker = currentMove.checkJumps("BlackKing");
                    if ((middleMarker != null) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("White")))
                    {
                        StackPanel middleStackPanel = (StackPanel)GetGridElement(DraughtsBoard, middleMarker.Row, middleMarker.Column);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        if (middleButton.Name.Contains("White"))
                        {
                            DraughtsBoard.Children.Remove(middleStackPanel);
                            Console.WriteLine("Button name" + middleButton.Name);
                            if (middleButton.Name.Contains("WhiteKing"))
                            {
                                middleMarker.Kinged = true;
                            }
                            RemoveMarker(middleMarker);
                            return true;
                        }
                    }
                }
                else
                {
                    if ((currentMove.ValidMove("Black")) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("White")))
                        return true;
                    Marker middleMarker = currentMove.checkJumps("Black");
                    if ((middleMarker != null) && (!button2.Name.Contains("Black")) && (!button2.Name.Contains("White")))
                    {
                        StackPanel middleStackPanel = (StackPanel)GetGridElement(DraughtsBoard, middleMarker.Row, middleMarker.Column);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        if (middleButton.Name.Contains("White"))
                        {
                            DraughtsBoard.Children.Remove(middleStackPanel);
                            Console.WriteLine("Button name" + middleButton.Name);
                            if (middleButton.Name.Contains("WhiteKing"))
                            {
                                middleMarker.Kinged = true;
                            }
                            RemoveMarker(middleMarker);
                            return true;
                        }
                    }
                }
            }
            currentMove = null;
            showError("Invalid Move. Try Again.");
            return false;
        }


        private Checkers_Board GetBoard()
        {
            Checkers_Board board = new Checkers_Board();
            for (int row = 1; row < 9; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    StackPanel stackPanel = (StackPanel)GetGridElement(DraughtsBoard, row, column);
                    if (stackPanel.Children.Count > 0)
                    {
                        Button button = (Button)stackPanel.Children[0];
                        if (button.Name.Contains("White"))
                        {
                            if (button.Name.Contains("King"))
                                board.SetState(row - 1, column, 3);
                            else
                                board.SetState(row - 1, column, 1);
                        }
                        else if (button.Name.Contains("Black"))
                        {
                            if (button.Name.Contains("King"))
                                board.SetState(row - 1, column, 4);
                            else
                                board.SetState(row - 1, column, 2);
                        }
                        else
                            board.SetState(row - 1, column, 0);

                    }
                    else
                    {
                        board.SetState(row - 1, column, -1);
                    }

                }
            }
            return board;
        }

        private void showError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("\b To Play \n" +
                "The object of the game is to capture all of your opponent's pieces or block them so they cannot be moved.\n" +
                "Pieces are always moved diagonally, 1 square at a time, towards the opponent's side of the board.\n" +
                "You play the entire game on the black squares, you do not need the white ones.\n\n" +
                "\b Capturing \n" +
                "You can capture an enemy piece by hopping over it.\n" +
                "Capturing is also done on the diagonal.\n" +
                "You have to jump from the square directly next to your target and land on the square just beyond it.\n" +
                "Your landing square must be vacant.\n" +
                "The piece captured is removed from the board.\n" +
                "If you are able to make a move that results in a capture then you must\n\n" +
                "\b To Win\n" +
                "Capture all your opponents pieces.", "How To Play");
        }

        private void option_Click(object sender, RoutedEventArgs e)
        {
            //options window to change settings
            options optionwindow = new options(numPlayers);
            optionwindow.ShowDialog();
            if (optionwindow.playbackground == true)
            {
                backgroundmusic.Play();

            }
            if (optionwindow.playbackground == false)
            {
                backgroundmusic.Stop();
            }
            if (numPlayers != optionwindow.numberOfPlayers)
            {
                var result = MessageBox.Show("Changing number of players will restart the game \nDo you want to continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    numPlayers = optionwindow.numberOfPlayers;
                    newGame();
                }

            }

            playername1 = optionwindow.txtp1.Text;
            playername2 = optionwindow.txtp2.Text;

        }

        private void newGame()
        {
            Cleaner();
            BuildBoard();
            NewReplay();
            if (winner == "White")
            {
                turn = "Black";
                lblturn.Content = playername2 + " Turn!";
            }
            else if ((winner == "Black") || winner == null)
            {
                turn = "White";
                lblturn.Content = playername1 + " Turn!";
            }
            if (numPlayers == 1)
            {
                turn = "Black";
                lblturn.Content = playername2 + " Turn!";
            }

            //MessageBox.Show("New Game Test");
            //MessageBox.Show("number of player"+numPlayers.ToString());
            //MessageBox.Show("player 1 name: " + playername1 +"\nPlayer 2 name: "+playername2);
        }

        private void NewReplay()
        {
            //new replay
            History GameHistory = new History();
            
            if (Game_List.GameList.Count == 0)
            {
                refid = refid + 1;
            }
            else
            {
                foreach (History h_loop in Game_List.GameList)
                {
                    if (h_loop.ID >= refid)
                    {
                        refid = h_loop.ID + 1;
                    }
                    else
                    {
                        GameHistory.ID = refid;
                    }
                }
                GameHistory.ID = refid;
            }
            GameHistory.ID = refid;
            Game_List.GameList.Add(GameHistory);
        }

        private void newGame_Click(object sender, RoutedEventArgs e)
        {
            newGame();
        }

        private void undo_Click(object sender, RoutedEventArgs e)
        {

            if (Undo_Stack.Count == 0)
            {
                showError("No Moves to undo");
            }
            else
            {
                bool afterKing = (bool)Undo_Stack.Pop();
                int afterColumn = (int)Undo_Stack.Pop();
                int afterRow = (int)Undo_Stack.Pop();
                bool beforeKing = (bool)Undo_Stack.Pop();
                int beforeColumn = (int)Undo_Stack.Pop();
                int beforeRow = (int)Undo_Stack.Pop();

                Console.WriteLine("After Column " + afterColumn
                                 + "\nAfter Row " + afterRow
                                 + "\nAfter King " + afterKing
                                 + "\nBefore Column " + beforeColumn
                                 + "\nBefore Row " + beforeRow
                                 + "\nBefore Kinged " + beforeKing);
                Marker markerBefore = new Marker(beforeRow, beforeColumn, beforeKing);
                Marker markerAfter = new Marker(afterRow, afterColumn, afterKing);
                undoMove(markerBefore, markerAfter);
                changeTurn();
            }
            //MessageBox.Show("Undo Test");
        }

        private void undoMove(Marker markerBefore, Marker markerAfter)
        {
            if ((markerBefore != null) && (markerAfter != null))
            {
                int jumpCheck = (markerBefore.Row - markerAfter.Row);

                Console.WriteLine("Jump check = " + jumpCheck);
                if ((jumpCheck == -2) || (jumpCheck == 2))
                {
                    if (turn == "Black")
                    {
                        bool king = (bool)Taken_Stack.Pop();
                        int column = (int)Taken_Stack.Pop();
                        int row = (int)Taken_Stack.Pop();


                        Marker takenMarker = new Marker(row, column, king);


                        Console.WriteLine("Kinged?" + takenMarker.Kinged);

                        Console.WriteLine("taken marker row :" + takenMarker.Row);
                        StackPanel middleStackPanel = (StackPanel)GetGridElement(DraughtsBoard, takenMarker.Row, takenMarker.Column);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        DraughtsBoard.Children.Remove(middleStackPanel);
                        addBlackButton(takenMarker);
                    }
                    if (turn == "White")
                    {
                        bool king = (bool)Taken_Stack.Pop();
                        int column = (int)Taken_Stack.Pop();
                        int row = (int)Taken_Stack.Pop();
                        Marker takenMarker = new Marker(row, column, king);
                        Console.WriteLine("Kinged?" + takenMarker.Kinged);
                        Console.WriteLine("taken marker row :" + takenMarker.Row);
                        StackPanel middleStackPanel = (StackPanel)GetGridElement(DraughtsBoard, takenMarker.Row, takenMarker.Column);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        DraughtsBoard.Children.Remove(middleStackPanel);
                        addWhiteButton(takenMarker);
                    }
                }
                Console.WriteLine("MarkerBefore " + markerBefore.Row + ", " + markerBefore.Column);
                Console.WriteLine("MarkerAfter " + markerAfter.Row + ", " + markerAfter.Column);
                StackPanel stackPanel1 = (StackPanel)GetGridElement(DraughtsBoard, markerBefore.Row, markerBefore.Column);
                StackPanel stackPanel2 = (StackPanel)GetGridElement(DraughtsBoard, markerAfter.Row, markerAfter.Column);
                Button buttonbefore = (Button)stackPanel1.Children[0];
                Button buttonafter = (Button)stackPanel2.Children[0];
                DraughtsBoard.Children.Remove(stackPanel1);
                DraughtsBoard.Children.Remove(stackPanel2);
                Grid.SetRow(stackPanel1, markerAfter.Row);
                Grid.SetColumn(stackPanel1, markerAfter.Column);
                DraughtsBoard.Children.Add(stackPanel1);
                Grid.SetRow(stackPanel2, markerBefore.Row);
                Grid.SetColumn(stackPanel2, markerBefore.Column);
                DraughtsBoard.Children.Add(stackPanel2);
                var WhiteBrush = new ImageBrush();
                WhiteBrush.ImageSource = new BitmapImage(new Uri("Resources/WhiteMarker.png", UriKind.Relative));
                var BlackBrush = new ImageBrush();
                BlackBrush.ImageSource = new BitmapImage(new Uri("Resources/BlackMarker.png", UriKind.Relative));
                if (markerAfter.Kinged == true && markerBefore.Kinged == false && buttonafter.Name.Contains("White"))
                {
                    buttonafter.Name = "White" + markerAfter.Row + markerAfter.Column;
                    buttonafter.Background = WhiteBrush;
                }
                else if (markerAfter.Kinged == true && markerBefore.Kinged == false && buttonafter.Name.Contains("Black"))
                {
                    buttonafter.Name = "Black" + markerAfter.Row + markerAfter.Column;
                    buttonafter.Background = BlackBrush;
                }
                AddToHistory(markerBefore, markerAfter);
                Redo_Stack.Push(markerAfter.Row);
                Redo_Stack.Push(markerAfter.Column);
                Redo_Stack.Push(markerAfter.Kinged);
                Redo_Stack.Push(markerBefore.Row);
                Redo_Stack.Push(markerBefore.Column);
                Redo_Stack.Push(markerBefore.Kinged);
            }
        }
        // this if for the undo if marker ws taken. readds them to the board
        private void addWhiteButton(Marker takenMarker)
        {
            ReTaken_Stack.Push(takenMarker.Row);
            ReTaken_Stack.Push(takenMarker.Column);
            ReTaken_Stack.Push(takenMarker.Kinged);
            var WhiteBrush = new ImageBrush();
            WhiteBrush.ImageSource = new BitmapImage(new Uri("Resources/WhiteMarker.png", UriKind.Relative));
            var BoardBlack = (SolidColorBrush)new BrushConverter().ConvertFromString("#CF9C63");
            StackPanel stackPanel = new StackPanel();
            stackPanel.Background = BoardBlack;
            Button button = new Button();
            button.Click += new RoutedEventHandler(Button_Click);
            button.Height = 60;
            button.Width = 60;
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Background = WhiteBrush;
            button.Name = "button" + "White" + takenMarker.Row + takenMarker.Column;
            if (takenMarker.Kinged == true)
            {
                var WhiteKingBrush = new ImageBrush();
                WhiteKingBrush.ImageSource = new BitmapImage(new Uri("Resources/WhiteKingMarker.png", UriKind.Relative));
                button.Background = WhiteKingBrush;
                button.Name = "button" + "White" + "WhiteKing" + takenMarker.Row + takenMarker.Column;
            }

            stackPanel.Children.Add(button);
            Grid.SetColumn(stackPanel, takenMarker.Column);
            Grid.SetRow(stackPanel, takenMarker.Row);
            DraughtsBoard.Children.Add(stackPanel);


        }
        private void addBlackButton(Marker takenMarker)
        {
            ReTaken_Stack.Push(takenMarker.Row);
            ReTaken_Stack.Push(takenMarker.Column);
            ReTaken_Stack.Push(takenMarker.Kinged);
            var BlackBrush = new ImageBrush();
            BlackBrush.ImageSource = new BitmapImage(new Uri("Resources/BlackMarker.png", UriKind.Relative));
            var BoardBlack = (SolidColorBrush)new BrushConverter().ConvertFromString("#CF9C63");
            StackPanel stackPanel = new StackPanel();
            stackPanel.Background = BoardBlack;
            Button button = new Button();
            button.Click += new RoutedEventHandler(Button_Click);
            button.Height = 60;
            button.Width = 60;
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Background = BlackBrush;
            button.Name = "button" + "Black" + takenMarker.Row + takenMarker.Column;
            if (takenMarker.Kinged == true)
            {
                var BlackKingBrush = new ImageBrush();
                BlackKingBrush.ImageSource = new BitmapImage(new Uri("Resources/BlackKingMarker.png", UriKind.Relative));
                button.Name = "button" + "Black" + "BlackKing" + takenMarker.Row + takenMarker.Column;
                button.Background = BlackKingBrush;
            }
            stackPanel.Children.Add(button);
            Grid.SetColumn(stackPanel, takenMarker.Column);
            Grid.SetRow(stackPanel, takenMarker.Row);
            DraughtsBoard.Children.Add(stackPanel);
        }
        //redo function
        private void redo_Click(object sender, RoutedEventArgs e)
        {
            if (Redo_Stack.Count == 0)
            {
                showError("No Moves to redo");
            }
            else
            {
                bool afterKing = (bool)Redo_Stack.Pop();
                int afterColumn = (int)Redo_Stack.Pop();
                int afterRow = (int)Redo_Stack.Pop();
                bool beforeKing = (bool)Redo_Stack.Pop();
                int beforeColumn = (int)Redo_Stack.Pop();
                int beforeRow = (int)Redo_Stack.Pop();

                Console.WriteLine("After Column " + afterColumn
                                 + "\nAfter Row " + afterRow
                                 + "\nAfter King " + afterKing
                                 + "\nBefore Column " + beforeColumn
                                 + "\nBefore Row " + beforeRow
                                 + "\nBefore Kinged " + beforeKing);
                Marker markerBefore = new Marker(beforeRow, beforeColumn, beforeKing);
                Marker markerAfter = new Marker(afterRow, afterColumn, afterKing);
                redoMove(markerBefore, markerAfter);
                changeTurn();
            }
        }

        private void redoMove(Marker markerBefore, Marker markerAfter)
        {
            if ((markerBefore != null) && (markerAfter != null))
            {
                int jumpCheck = (markerBefore.Row - markerAfter.Row);

                Console.WriteLine("Jump check = " + jumpCheck);
                if ((jumpCheck == -2) || (jumpCheck == 2))
                {
                    if (turn == "Black")
                    {
                        bool king = (bool)ReTaken_Stack.Pop();
                        int column = (int)ReTaken_Stack.Pop();
                        int row = (int)ReTaken_Stack.Pop();


                        Marker takenMarker = new Marker(row, column, king);


                        Console.WriteLine("Kinged?" + takenMarker.Kinged);

                        Console.WriteLine("taken marker row :" + takenMarker.Row);
                        StackPanel middleStackPanel = (StackPanel)GetGridElement(DraughtsBoard, takenMarker.Row, takenMarker.Column);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        DraughtsBoard.Children.Remove(middleStackPanel);
                        RemoveMarker(takenMarker);
                    }
                    if (turn == "White")
                    {
                        bool king = (bool)ReTaken_Stack.Pop();
                        int column = (int)ReTaken_Stack.Pop();
                        int row = (int)ReTaken_Stack.Pop();
                        Marker takenMarker = new Marker(row, column, king);
                        Console.WriteLine("Kinged?" + takenMarker.Kinged);
                        Console.WriteLine("taken marker row :" + takenMarker.Row);
                        StackPanel middleStackPanel = (StackPanel)GetGridElement(DraughtsBoard, takenMarker.Row, takenMarker.Column);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        DraughtsBoard.Children.Remove(middleStackPanel);
                        RemoveMarker(takenMarker);
                    }
                }
                Console.WriteLine("MarkerBefore " + markerBefore.Row + ", " + markerBefore.Column);
                Console.WriteLine("MarkerAfter " + markerAfter.Row + ", " + markerAfter.Column);
                StackPanel stackPanel1 = (StackPanel)GetGridElement(DraughtsBoard, markerBefore.Row, markerBefore.Column);
                StackPanel stackPanel2 = (StackPanel)GetGridElement(DraughtsBoard, markerAfter.Row, markerAfter.Column);
                Button buttonbefore = (Button)stackPanel1.Children[0];
                Button buttonafter = (Button)stackPanel2.Children[0];
                DraughtsBoard.Children.Remove(stackPanel1);
                DraughtsBoard.Children.Remove(stackPanel2);
                Grid.SetRow(stackPanel1, markerAfter.Row);
                Grid.SetColumn(stackPanel1, markerAfter.Column);
                DraughtsBoard.Children.Add(stackPanel1);
                Grid.SetRow(stackPanel2, markerBefore.Row);
                Grid.SetColumn(stackPanel2, markerBefore.Column);
                DraughtsBoard.Children.Add(stackPanel2);
                KingMe(markerBefore);
                var WhiteBrush = new ImageBrush();
                WhiteBrush.ImageSource = new BitmapImage(new Uri("Resources/WhiteMarker.png", UriKind.Relative));
                var BlackBrush = new ImageBrush();
                BlackBrush.ImageSource = new BitmapImage(new Uri("Resources/BlackMarker.png", UriKind.Relative));
                if (markerAfter.Kinged == true && markerBefore.Kinged == false && buttonafter.Name.Contains("White"))
                {
                    buttonafter.Name = "White" + markerAfter.Row + markerAfter.Column;
                    buttonafter.Background = WhiteBrush;
                }
                else if (markerAfter.Kinged == true && markerBefore.Kinged == false && buttonafter.Name.Contains("Black"))
                {
                    buttonafter.Name = "Black" + markerAfter.Row + markerAfter.Column;
                    buttonafter.Background = BlackBrush;
                }
                addToUndo(markerBefore, markerAfter);
                AddToHistory(markerBefore, markerAfter);
            }
        }
        //Replay function
        private void AddToHistory(Marker markerBefore,Marker markerAfter)
        {
            foreach (History h in Game_List.GameList)
                {
                    if (refid == h.ID)
                    {
                        //updates customer
                        try
                        {
                        h.turns.Enqueue(markerBefore.Row);
                        h.turns.Enqueue(markerBefore.Column);
                        h.turns.Enqueue(markerAfter.Row);
                        h.turns.Enqueue(markerAfter.Column);
                        h.Taken = Taken_Stack;
                           
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    //if ends
                }
                //froeach ends
            }
        }
        private void replayGame_Click(object sender, RoutedEventArgs e)
        {
            ReplayGame();
        }
        private void ReplayGame()
        {
            refid = 1;
            //Marker markerB = new Marker(1, 1);
            //Marker markerA = new Marker(2,1);
            foreach (History h in Game_List.GameList)
            {
                if (refid == h.ID)
                {
                    //updates customer
                    try
                    {
                       
                        int id = h.ID;
                        Console.WriteLine("GameID = " + id);
                        while (h.turns.Count!=0)
                        {
                            //int x =(int)h.turns.Dequeue();
                            // Console.WriteLine(x);

                            int rowb =(int)h.turns.Dequeue();
                            int columnb = (int)h.turns.Dequeue();
                            int rowa = (int)h.turns.Dequeue();
                            int columna = (int)h.turns.Dequeue();
                            Console.WriteLine("Row Before: "+rowb);
                            Console.WriteLine("Column Before: "+columnb);
                            Console.WriteLine("Row After: " + rowb);
                            Console.WriteLine("Column After: " + columnb);
                            Marker markerB = new Marker(rowb, columnb);
                            Marker markerA = new Marker(rowa, columna);

                           
                            wait(2000); 
                            ShowGame(markerB, markerA);
                        }

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    //if ends
                }
                //froeach ends
            }
        }

        private void ShowGame(Marker markerBefore, Marker markerAfter)
        {
           // Cleaner();

            if ((markerBefore != null) && (markerAfter != null))
            {

                int jumpCheck = (markerBefore.Row - markerAfter.Row);

                Console.WriteLine("Jump check = " + jumpCheck);
                if ((jumpCheck == -2) || (jumpCheck == 2))
                {
                    if (turn == "Black")
                    {
                        bool king = (bool)ReTaken_Stack.Pop();
                        int column = (int)ReTaken_Stack.Pop();
                        int row = (int)ReTaken_Stack.Pop();


                        Marker takenMarker = new Marker(row, column, king);


                        Console.WriteLine("Kinged?" + takenMarker.Kinged);

                        Console.WriteLine("taken marker row :" + takenMarker.Row);
                        StackPanel middleStackPanel = (StackPanel)GetGridElement(DraughtsBoard, takenMarker.Row, takenMarker.Column);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        DraughtsBoard.Children.Remove(middleStackPanel);
                        RemoveMarker(takenMarker);
                    }
                    if (turn == "White")
                    {
                        bool king = (bool)ReTaken_Stack.Pop();
                        int column = (int)ReTaken_Stack.Pop();
                        int row = (int)ReTaken_Stack.Pop();
                        Marker takenMarker = new Marker(row, column, king);
                        Console.WriteLine("Kinged?" + takenMarker.Kinged);
                        Console.WriteLine("taken marker row :" + takenMarker.Row);
                        StackPanel middleStackPanel = (StackPanel)GetGridElement(DraughtsBoard, takenMarker.Row, takenMarker.Column);
                        Button middleButton = (Button)middleStackPanel.Children[0];
                        DraughtsBoard.Children.Remove(middleStackPanel);
                        RemoveMarker(takenMarker);
                    }
                }
                Console.WriteLine("MarkerBefore " + markerBefore.Row + ", " + markerBefore.Column);
                Console.WriteLine("MarkerAfter " + markerAfter.Row + ", " + markerAfter.Column);
                StackPanel stackPanel1 = (StackPanel)GetGridElement(DraughtsBoard, markerBefore.Row, markerBefore.Column);
                StackPanel stackPanel2 = (StackPanel)GetGridElement(DraughtsBoard, markerAfter.Row, markerAfter.Column);
                Button buttonbefore = (Button)stackPanel1.Children[0];
                Button buttonafter = (Button)stackPanel2.Children[0];
                DraughtsBoard.Children.Remove(stackPanel1);
                DraughtsBoard.Children.Remove(stackPanel2);
                Grid.SetRow(stackPanel1, markerAfter.Row);
                Grid.SetColumn(stackPanel1, markerAfter.Column);
                DraughtsBoard.Children.Add(stackPanel1);
                Grid.SetRow(stackPanel2, markerBefore.Row);
                Grid.SetColumn(stackPanel2, markerBefore.Column);
                DraughtsBoard.Children.Add(stackPanel2);
                KingMe(markerBefore);
                var WhiteBrush = new ImageBrush();
                WhiteBrush.ImageSource = new BitmapImage(new Uri("Resources/WhiteMarker.png", UriKind.Relative));
                var BlackBrush = new ImageBrush();
                BlackBrush.ImageSource = new BitmapImage(new Uri("Resources/BlackMarker.png", UriKind.Relative));
                if (markerAfter.Kinged == true && markerBefore.Kinged == false && buttonafter.Name.Contains("White"))
                {
                    buttonafter.Name = "White" + markerAfter.Row + markerAfter.Column;
                    buttonafter.Background = WhiteBrush;
                }
                else if (markerAfter.Kinged == true && markerBefore.Kinged == false && buttonafter.Name.Contains("Black"))
                {
                    buttonafter.Name = "Black" + markerAfter.Row + markerAfter.Column;
                    buttonafter.Background = BlackBrush;
                }




            }

            CheckforWinnner();
            
        }


        private void wait(int ms)
        {

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(ms);
            timer.Start();

        }

        private void AiTurn()
        {
            currentMove = AI.GetMove(GetBoard());
            Console.WriteLine(currentMove);
            if (currentMove != null)
            {
                if (CheckMove())
                {
                    MakeMove();
                    
                }
            }
        }

        //end of program!      
    }
}