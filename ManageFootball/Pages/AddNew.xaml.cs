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
    /// Interaction logic for AddNew.xaml
    /// </summary>
    public partial class AddNew : Page
    {
        public ObservableCollection<AddNewInfo> UpdatesInfo;
        public List<string> Team1List { get; set; } = new() { "Red", "Blue", "Green" };
        public List<string> Team2List { get; set; } = new() { "Red", "Blue", "Green" };
        public List<string> AddList { get; set; } = new() { "Sân 1", "Sân 2"};
        public AddNew()
        {
            InitializeComponent();
            UpdatesInfo = new ObservableCollection<AddNewInfo>();
            DataContext = this;
            dataGrid.ItemsSource = UpdatesInfo;
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
    public class AddNewInfo
    {
        public string Code { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string Add { get; set; }
        public DateOnly? Date1 { get; set; }
    }
}
