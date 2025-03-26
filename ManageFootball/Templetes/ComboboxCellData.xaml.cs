using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace ManageFootball.Templetes
{
    public partial class ComboboxCellData: ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty]
        private string searchText;
        public ObservableCollection<string> NumberList { get; } = new()
        {
            "10", "20", "30", "40", "50"
        };
        public ICollectionView FilteredNumberList { get; }
        public ComboboxCellData()
        {
            FilteredNumberList = CollectionViewSource.GetDefaultView(NumberList);
            FilteredNumberList.Filter = FilterNumbers;
        }
        partial void OnSearchTextChanged(string value)
        {
            FilteredNumberList.Refresh();
        }
        private bool FilterNumbers(object item)
        {
            if (string.IsNullOrEmpty(SearchText))
                return true;
            return item.ToString().Contains(SearchText);
        }
    }
}
