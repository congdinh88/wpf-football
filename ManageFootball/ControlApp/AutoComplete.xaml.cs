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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ManageFootball.ControlApp
{
    /// <summary>
    /// Interaction logic for AutoComplete.xaml
    /// </summary>

    public partial class AutoComplete : UserControl
    {

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AutoComplete),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty SuggestionsProperty =
            DependencyProperty.Register("Suggestions", typeof(System.Collections.IEnumerable), typeof(AutoComplete));

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(AutoComplete));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        // Thêm DependencyProperty mới
        public static readonly DependencyProperty IsEditingProperty =
            DependencyProperty.Register("IsEditing", typeof(bool), typeof(AutoComplete),
                new PropertyMetadata(false, OnIsEditingChanged));

        public bool IsEditing
        {
            get => (bool)GetValue(IsEditingProperty);
            set => SetValue(IsEditingProperty, value);
        }

        private static void OnIsEditingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as AutoComplete;
            if (!(bool)e.NewValue)
            {
                // Commit thay đổi khi kết thúc edit
                control.textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
            }
        }

        public System.Collections.IEnumerable Suggestions
        {
            get => (System.Collections.IEnumerable)GetValue(SuggestionsProperty);
            set => SetValue(SuggestionsProperty, value);
        }

        public string DisplayMemberPath
        {
            get => (string)GetValue(DisplayMemberPathProperty);
            set => SetValue(DisplayMemberPathProperty, value);
        }
        public AutoComplete()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
            suggestionDataGrid.SelectedItem = null;
            suggestionDataGrid.Focus();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Kết thúc edit mode
            IsEditing = false;
        }

        private void SuggestionDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (suggestionDataGrid.SelectedItem != null)
            {
                var selectedItem = suggestionDataGrid.SelectedItem;
                if (!string.IsNullOrEmpty(DisplayMemberPath))
                {
                    var prop = selectedItem.GetType().GetProperty(DisplayMemberPath);
                    Text = prop?.GetValue(selectedItem)?.ToString() ?? string.Empty;
                }
                popup.IsOpen = false;
            }
        }

        private void SuggestionDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SuggestionDataGrid_SelectionChanged(sender, null);
                e.Handled = true;
            }
        }
    }

    public class SuggestionItem
    {
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }
    }

}
