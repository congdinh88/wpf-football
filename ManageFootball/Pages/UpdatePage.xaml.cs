using ManageFootball.ControlApp;
using ManageFootball.Templetes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
    /// 

    public partial class UpdatePage : Page
    {
        public ObservableCollection<DataItem> Items { get; set; } = new ObservableCollection<DataItem>();
        


        public UpdatePage()
        {
            InitializeComponent();
            DataContext = this;
            Items.Add(new DataItem { Column1 = "", Column2 = "", Column3 = "" });
            // Bắt sự kiện khi row edit kết thúc
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
    public class DataItem
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
    }
}
