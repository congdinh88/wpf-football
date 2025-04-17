using System;
using System.Collections;
using System.Collections.Generic;
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

namespace ManageFootball.ControlApp
{
    /// <summary>
    /// Interaction logic for AutoSuggestTextBox.xaml
    /// </summary>
    public partial class AutoSuggestTextBox : UserControl
    {
        public AutoSuggestTextBox()
        {
            InitializeComponent();
            DataContext = this;

            // Kéo thả sự kiện
            PART_DataGrid.MouseDoubleClick += (s, e) =>
            {
                if (PART_DataGrid.SelectedItem != null)
                {
                    SelectedItem = PART_DataGrid.SelectedItem;
                    IsDropDownOpen = false;
                }
            };
        }
        #region DependencyProperties

        // Dữ liệu gốc
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                nameof(ItemsSource),
                typeof(IEnumerable),
                typeof(AutoSuggestTextBox),
                new PropertyMetadata(null, OnItemsSourceChanged));

        // Thuộc tính để filter (ví dụ "Column2")
        public static readonly DependencyProperty FilterMemberPathProperty =
            DependencyProperty.Register(
                nameof(FilterMemberPath),
                typeof(string),
                typeof(AutoSuggestTextBox),
                new PropertyMetadata(null));

        // Text đang gõ
        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register(
                nameof(SearchText),
                typeof(string),
                typeof(AutoSuggestTextBox),
                new PropertyMetadata(string.Empty, OnSearchTextChanged));

        // Bật/tắt Popup
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register(
                nameof(IsDropDownOpen),
                typeof(bool),
                typeof(AutoSuggestTextBox),
                new PropertyMetadata(false));

        // Item đã chọn
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                nameof(SelectedItem),
                typeof(object),
                typeof(AutoSuggestTextBox),
                new PropertyMetadata(null));

        #endregion

        #region CLR Wrappers

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public string FilterMemberPath
        {
            get => (string)GetValue(FilterMemberPathProperty);
            set => SetValue(FilterMemberPathProperty, value);
        }

        public string SearchText
        {
            get => (string)GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        public bool IsDropDownOpen
        {
            get => (bool)GetValue(IsDropDownOpenProperty);
            set => SetValue(IsDropDownOpenProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        #endregion

        #region Filtering Logic

        private ICollectionView _view;
        public ICollectionView FilteredView => _view;

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (AutoSuggestTextBox)d;
            if (e.NewValue is IEnumerable src)
            {
                ctrl._view = CollectionViewSource.GetDefaultView(src);
                ctrl._view.Filter = ctrl.FilterPredicate;
            }
            else
            {
                ctrl._view = null;
            }
        }

        private static void OnSearchTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (AutoSuggestTextBox)d;
            ctrl._view?.Refresh();
            if (!string.IsNullOrEmpty(ctrl.SearchText))
                ctrl.IsDropDownOpen = true;
        }

        private bool FilterPredicate(object item)
        {
            if (string.IsNullOrEmpty(SearchText) || string.IsNullOrEmpty(FilterMemberPath))
                return true;

            var prop = item.GetType().GetProperty(FilterMemberPath);
            if (prop == null) return true;
            var value = prop.GetValue(item)?.ToString() ?? "";
            return value.IndexOf(SearchText, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        #endregion
    }
}
