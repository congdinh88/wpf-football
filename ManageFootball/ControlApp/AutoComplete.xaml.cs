using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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

namespace ManageFootball.ControlApp
{
    /// <summary>
    /// Interaction logic for AutoComplete.xaml
    /// </summary>
    public partial class AutoComplete : UserControl
    {
        public ObservableCollection<DataSuggesList> dataSuggesList { get; set; }
        public ObservableCollection<DataSuggesList> filteredData { get; set; }

        // Thuộc tính phụ thuộc cho ItemsSource
        public static readonly DependencyProperty DataListProperty =
            DependencyProperty.Register("DataList", typeof(IEnumerable), typeof(AutoComplete), new PropertyMetadata(null));

        public IEnumerable DataList
        {
            get { return (IEnumerable)GetValue(DataListProperty); }
            set { SetValue(DataListProperty, value); }
        }

        public string SelectedColumn
        {
            get { return (string)GetValue(SelectedColumnProperty); }
            set { SetValue(SelectedColumnProperty, value); }
        }

        public static readonly DependencyProperty SelectedColumnProperty =
            DependencyProperty.Register("SelectedColumn", typeof(string), typeof(AutoComplete));
        public AutoComplete()
        {
            InitializeComponent();
            dataGrid.ItemsSource= dataSuggesList;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = textBox.Text.ToLower();
            if (string.IsNullOrEmpty(query))
            {
                popup.IsOpen = false;
                return;
            }
            var selectedProperty = typeof(DataSuggesList).GetProperty(SelectedColumn, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (selectedProperty != null)
            {
                var matches = dataSuggesList.Where(item =>
                {
                    var value = selectedProperty.GetValue(item)?.ToString();
                    return value != null && value.ToLower().Contains(query);
                }).ToList();

                if (matches.Any())
                {
                    filteredData = new ObservableCollection<DataSuggesList>(matches);
                    dataGrid.ItemsSource = filteredData;
                    popup.IsOpen = true;
                }
                else
                {
                    popup.IsOpen = false;
                }
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem is DataSuggesList selectedItem)
            {
                var selectedProperty = selectedItem.GetType().GetProperty(SelectedColumn);
                if (selectedProperty != null)
                {
                    textBox.Text = selectedProperty.GetValue(selectedItem)?.ToString();
                }
                popup.IsOpen = false;
            }
        }
    }
    public class DataSuggesList
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
    }
}
