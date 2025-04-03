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
        public ObservableCollection<DataItem> Items { get; set; } = new ObservableCollection<DataItem>
        {
            new DataItem(),
        };

        public ObservableCollection<SuggestionItem> Suggestions { get; set; } = new ObservableCollection<SuggestionItem>
        {
            new SuggestionItem { Col1 = "A1", Col2 = "B1", Col3 = "C1" },
            new SuggestionItem { Col1 = "A2", Col2 = "B2", Col3 = "C2" },
            new SuggestionItem { Col1 = "A3", Col2 = "B3", Col3 = "C3" }
        };

        public UpdatePage()
        {
            InitializeComponent();
            DataContext = this;

            // Bắt sự kiện khi row edit kết thúc
            MainDataGrid.RowEditEnding += (s, e) =>
            {
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    var lastItem = Items[^1];
                    if (!string.IsNullOrEmpty(lastItem.Column1) ||
                        !string.IsNullOrEmpty(lastItem.Column2) ||
                        !string.IsNullOrEmpty(lastItem.Column3))
                    {
                        Items.Add(new DataItem());
                    }
                }
            };
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
