using ManageFootball.ControlApp;
using ManageFootball.Templetes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ObservableCollection<DataSuggesList> MainDataList { get; set; }
        public ObservableCollection<UpdateInfo> Updates;
        public List<string> CodeList { get; set; } = new() { "M1", "M2", "M3" };
        public List<string> TeamList { get; set; } = new() { "Red", "Blue", "Green" };
        public List<string> NumberList { get; set; } = new() { "10", "20", "30" };
        public ObservableCollection<KeyValuePairModel> ChoiceList { get; set; }

        public UpdatePage()
        {
            InitializeComponent();
            ChoiceList = new ObservableCollection<KeyValuePairModel>
            {
                new KeyValuePairModel { Key = "G", Value = "Bàn thắng" },
                new KeyValuePairModel { Key = "Y", Value = "Thẻ vàng" },
                new KeyValuePairModel { Key = "R", Value = "Thẻ đỏ" }
            };
            MainDataList = new ObservableCollection<DataSuggesList>
            {
                new DataSuggesList { Col1 = "1", Col2 = "Item A", Col3 = "Desc A" },
                new DataSuggesList { Col1 = "2", Col2 = "Item B", Col3 = "Desc B" },
                new DataSuggesList { Col1 = "3", Col2 = "Item C", Col3 = "Desc C" }
            };
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
        private void ComboBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null)
            {
                comboBox.IsDropDownOpen = true; // Mở danh sách khi nhập ký tự
            }
        }
    }
    public class UpdateInfo: INotifyPropertyChanged
    {
        private string _test;
        public string Code { get; set; }
        public string Team { get; set; }
        public string Number { get; set; }
        public string Choice { get; set; }
        public string Time { get; set; }
        //public string Test { get; set; }
        public string Test
        {
            get => _test;
            set
            {
                _test = value;
                MessageBox.Show($"Test updated to: {_test}"); // Log để kiểm tra
                OnPropertyChanged(nameof(Test));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class KeyValuePairModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string FormattedValue => $"{Key}: {Value}";
    }
}
