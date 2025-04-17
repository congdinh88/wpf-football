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
        public UpdatePage()
        {
            InitializeComponent();
        }
        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            int rowIndex = e.Row.GetIndex(); // Lấy index của dòng

            // Nếu là dòng cuối: Hiển thị "*"
            if (rowIndex == dataGrid.Items.Count - 1)
            {
                e.Row.Header = "*";
            }
            else // Các dòng khác: Hiển thị số thứ tự
            {
                e.Row.Header = (rowIndex + 1).ToString();
            }
        }
    }
}
