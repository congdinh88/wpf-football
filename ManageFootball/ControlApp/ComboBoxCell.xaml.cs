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

namespace ManageFootball.ControlApp
{
    /// <summary>
    /// Interaction logic for ComboBoxCell.xaml
    /// </summary>
    public partial class ComboBoxCell : UserControl
    {
        public ComboBoxCell()
        {
            InitializeComponent();
        }
        // ItemsSource Binding
        public ObservableCollection<KeyValuePairModel> Items
        {
            get { return (ObservableCollection<KeyValuePairModel>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
      

        // SelectedKey Binding
        public string SelectedKey
        {
            get { return (string)GetValue(SelectedKeyProperty); }
            set { SetValue(SelectedKeyProperty, value); }
        }
        public static readonly DependencyProperty SelectedKeyProperty =
            DependencyProperty.Register("SelectedKey", typeof(string), typeof(ComboBoxCell), new PropertyMetadata(null));

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register(nameof(Items), typeof(ObservableCollection<KeyValuePairModel>),
            typeof(ComboBoxCell), new PropertyMetadata(null, OnItemsChanged));

        private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ComboBoxCell;
            if (control?.comboBox != null)
            {
                control.comboBox.ItemsSource = e.NewValue as ObservableCollection<KeyValuePairModel>;
            }
        }
    }

    public class KeyValuePairModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string FormattedValue
        {
            get => $"{Key}: {Value}";
        }
    }
}

