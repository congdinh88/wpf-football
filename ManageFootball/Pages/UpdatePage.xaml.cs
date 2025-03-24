using ManageFootball.ControlApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ManageFootball.Pages
{
    /// <summary>
    /// Interaction logic for UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Page
    {
        public ObservableCollection<UpdateInfo> Updates;
        public List<string> CodeList { get; set; } = new() { "M1", "M2", "M3" };
        public List<string> TeamList { get; set; } = new() { "Red", "Blue", "Green" };
        public List<string> NumberList { get; set; } = new() { "10", "20", "30" };
        public List<string> ChoiceList { get; set; } = new() { "Yes", "No", "Maybe" };
        public UpdatePage()
        {
            InitializeComponent();
            Updates = new ObservableCollection<UpdateInfo>();
            DataContext = this;
            dataGrid.ItemsSource = Updates;
        }
        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null)
            {
                int rowIndex = e.Row.GetIndex();
                if (rowIndex == grid.Items.Count - 1) // Kiểm tra hàng cuối
                {
                    e.Row.Header = "*";
                }
                else
                {
                    e.Row.Header = (rowIndex + 1).ToString(); // Số thứ tự bắt đầu từ 1
                }
            }
        }
       
    }
    public class UpdateInfo
    {
        public string Code { get; set; }
        public string Team { get; set; }
        public string Number { get; set; }
        public string Choice { get; set; }
        public string Time { get; set; }
    }

}
