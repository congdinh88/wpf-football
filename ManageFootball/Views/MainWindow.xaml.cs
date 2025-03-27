using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ManageFootball.Pages;

namespace ManageFootball
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new MatchPage());
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView.SelectedItem is ListViewItem selectedItem)
            {
                string pageName = selectedItem.Tag.ToString();
                mainFrame.Navigate(new Uri(pageName, UriKind.Relative));
            }
        }
    }
}