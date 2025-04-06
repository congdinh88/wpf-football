using CommunityToolkit.Mvvm.Input;
using ManageFootball.Models;
using ManageFootball.Pages;
using ManageFootball.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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
    /// 

    public partial class AutoComplete : UserControl
    {
        private ListCollectionView _collectionView;

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(AutoComplete),
                new PropertyMetadata(null, OnItemsSourceChanged));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(AutoComplete),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register(nameof(SelectedValuePath), typeof(string), typeof(AutoComplete),
                new PropertyMetadata(""));

        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register(nameof(SearchText), typeof(string), typeof(AutoComplete),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty IsPopupOpenProperty =
            DependencyProperty.Register(nameof(IsPopupOpen), typeof(bool), typeof(AutoComplete),
                new PropertyMetadata(false));
        public AutoComplete()
        {
            InitializeComponent();
            Loaded += (s, e) => InitializeCollectionView();
        }
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public string SelectedValuePath
        {
            get => (string)GetValue(SelectedValuePathProperty);
            set => SetValue(SelectedValuePathProperty, value);
        }

        public string SearchText
        {
            get => (string)GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        public bool IsPopupOpen
        {
            get => (bool)GetValue(IsPopupOpenProperty);
            set => SetValue(IsPopupOpenProperty, value);
        }

        public IEnumerable FilteredItems => _collectionView;

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AutoComplete control)
            {
                control.InitializeCollectionView();
            }
        }

        private void InitializeCollectionView()
        {
            if (ItemsSource == null) return;

            _collectionView = new ListCollectionView((IList)ItemsSource);
            _collectionView.Filter = item =>
            {
                if (string.IsNullOrEmpty(SearchText)) return true;

                var prop = item.GetType().GetProperty(SelectedValuePath);
                var value = prop?.GetValue(item)?.ToString() ?? "";
                return value.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
            };

            PART_DataGrid.ItemsSource = _collectionView;
        }

        private void TogglePopup(object sender, RoutedEventArgs e)
        {
            IsPopupOpen = !IsPopupOpen;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PART_DataGrid.SelectedItem != null)
            {
                SelectedItem = PART_DataGrid.SelectedItem;
                var prop = SelectedItem.GetType().GetProperty(SelectedValuePath);
                SearchText = prop?.GetValue(SelectedItem)?.ToString() ?? "";
                PART_TextBox.Text = SearchText;
                IsPopupOpen = false;
            }
        }

        private void HandleDataGridKey(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    IsPopupOpen = false;
                    e.Handled = true;
                    break;

                case Key.Enter:
                    if (PART_DataGrid.SelectedItem != null)
                    {
                        ApplySelection();
                        e.Handled = true;
                    }
                    break;
            }
        }
        private void ApplySelection()
        {
            if (PART_DataGrid.SelectedItem == null) return;

            SelectedItem = PART_DataGrid.SelectedItem;
            var prop = SelectedItem.GetType().GetProperty(SelectedValuePath);
            SearchText = prop?.GetValue(SelectedItem)?.ToString() ?? "";
            IsPopupOpen = false;
            PART_TextBox.Focus();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!IsPopupOpen) return;

            switch (e.Key)
            {
                case Key.Down:
                    MoveSelection(1);
                    e.Handled = true;
                    break;

                case Key.Up:
                    MoveSelection(-1);
                    e.Handled = true;
                    break;
            }
        }

        private void MoveSelection(int direction)
        {
            if (PART_DataGrid.Items.Count == 0) return;

            var newIndex = PART_DataGrid.SelectedIndex + direction;
            newIndex = Math.Clamp(newIndex, 0, PART_DataGrid.Items.Count - 1);

            PART_DataGrid.SelectedIndex = newIndex;
            PART_DataGrid.ScrollIntoView(PART_DataGrid.SelectedItem);
        }
    }

}
