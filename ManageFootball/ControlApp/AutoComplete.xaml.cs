using ManageFootball.Pages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private DataGrid parentDataGrid;
        public AutoComplete()
        {
            InitializeComponent();
            FilteredData = new ObservableCollection<DataSuggesList>();
            Loaded += AutoComplete_Loaded;
        }

        private void AutoComplete_Loaded(object sender, RoutedEventArgs e)
        {
            // Tìm DataGrid cha khi AutoComplete được tải
            parentDataGrid = FindParentDataGrid(this);
            //if (parentDataGrid != null)
            //{
            //    MessageBox.Show("Found parent DataGrid during initialization");
            //}
            //else
            //{
            //    MessageBox.Show("Could not find parent DataGrid during initialization");
            //}
        }
        // Dữ liệu từ bên ngoài (MainWindow, Page,...) truyền vào
        public ObservableCollection<DataSuggesList> DataSuggesList
        {
            get { return (ObservableCollection<DataSuggesList>)GetValue(DataSuggesListProperty); }
            set { SetValue(DataSuggesListProperty, value); }
        }

        public static readonly DependencyProperty DataSuggesListProperty =
            DependencyProperty.Register("DataSuggesList", typeof(ObservableCollection<DataSuggesList>), typeof(AutoComplete),
                new PropertyMetadata(null, OnDataSuggesListChanged));

        private static void OnDataSuggesListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AutoComplete control)
            {
                control.FilterData();
            }
        }

        public ObservableCollection<DataSuggesList> FilteredData { get; set; }

        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string), typeof(AutoComplete),
                new PropertyMetadata("", OnSearchTextChanged));

        private static void OnSearchTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AutoComplete control)
            {
                control.FilterData();
            }
        }

        public string SelectedColumn
        {
            get { return (string)GetValue(SelectedColumnProperty); }
            set { SetValue(SelectedColumnProperty, value); }
        }

        public static readonly DependencyProperty SelectedColumnProperty =
            DependencyProperty.Register("SelectedColumn", typeof(string), typeof(AutoComplete));

        private void FilterData()
        {
            if (DataSuggesList == null || string.IsNullOrEmpty(SearchText))
            {
                popup.IsOpen = false;
                return;
            }

            var selectedProperty = typeof(DataSuggesList).GetProperty(SelectedColumn, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (selectedProperty != null)
            {
                var matches = DataSuggesList.Where(item =>
                {
                    var value = selectedProperty.GetValue(item)?.ToString();
                    return value != null && value.ToLower().Contains(SearchText.ToLower());
                }).ToList();

                FilteredData.Clear();
                foreach (var item in matches)
                {
                    FilteredData.Add(item);
                }

                popup.IsOpen = matches.Any();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
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
                    var matches = DataSuggesList?.Where(item =>
                    {
                        var value = selectedProperty.GetValue(item)?.ToString();
                        return value != null && value.ToLower().Contains(query);
                    }).ToList();

                    if (matches != null && matches.Any())
                    {
                        var filteredData = new ObservableCollection<DataSuggesList>(matches);
                        dataGrid.ItemsSource = filteredData;
                        popup.IsOpen = true;
                    }
                    else
                    {
                        popup.IsOpen = false;
                    }
                }
            }
        }


        private DataGrid FindParentDataGrid(DependencyObject child)
        {
            DependencyObject parent = child;
            while (parent != null)
            {
                //MessageBox.Show($"Current parent in FindParentDataGrid: {parent.GetType().Name}");
                if (parent is DataGrid)
                {
                    return parent as DataGrid;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
                MessageBox.Show($"Current parent: {parent?.GetType().Name}");
            }
            return parent as T;
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (dataGrid.SelectedItem is DataSuggesList selectedItem)
            {
                var selectedProperty = selectedItem.GetType().GetProperty(SelectedColumn);
                if (selectedProperty != null)
                {
                    SearchText = selectedProperty.GetValue(selectedItem)?.ToString();
                }
                popup.IsOpen = false;

                var cell = FindParent<DataGridCell>(this);
                if (cell != null && cell.DataContext is UpdateInfo updateInfo)
                {
                    updateInfo.Test = SearchText;
                    //MessageBox.Show($"Directly set Test to: {updateInfo.Test}");
                }
                else
                {
                    //MessageBox.Show("Could not find DataGridCell");
                }

                if (parentDataGrid != null)
                {
                    parentDataGrid.BeginEdit();
                    parentDataGrid.CommitEdit(DataGridEditingUnit.Cell, true);
                    parentDataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                    //MessageBox.Show("DataGrid committed edit");
                }
                else
                {
                    //MessageBox.Show("Parent DataGrid not found (not initialized)");
                }
            }
        }

        private void BtnPopup_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = DataSuggesList;
            popup.IsOpen = !popup.IsOpen;
        }
    }

    public class DataSuggesList
    {
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }
    }

}
