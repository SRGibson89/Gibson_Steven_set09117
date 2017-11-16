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
    /// Interaction logic for options.xaml
    /// </summary>
    public partial class options : Window
    {
        public int numberOfPlayers { get; set; }
        // int music = 0;
        public bool playbackground;
        
        
        public options(int numberplayers)
        {
            InitializeComponent();
            numberOfPlayers = numberplayers;
        }

        private void bntcancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            numberOfPlayers = int.Parse(cmbnoPlayers.Text);
            if (chkMusic.IsChecked == true)
            {
                playbackground = true;
            }
            if (chkMusic.IsChecked == false)
            {
                playbackground = false;
            }
            
            Close();
            
        }

        

        
    }
}
