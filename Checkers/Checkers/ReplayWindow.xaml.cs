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
using System.Windows.Shapes;

namespace Checkers
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ReplayWindow : Window
    {
        SingletonLists Game_List = SingletonLists.Instance;
        public int gameID,Speed;
        public ReplayWindow()
        {
            InitializeComponent();
            LoadGames();

        }

        private void LoadGames()
        {
            if (Game_List.GameList.Count() != 0)
            {
                foreach (History h in Game_List.GameList)
                {
                    cmbGame.Items.Add(h.ID); //populates the combobox with all the games on record
                }
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            gameID = int.Parse(cmbGame.Text);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            gameID = 0;
            this.Close();
        }
    }
}
