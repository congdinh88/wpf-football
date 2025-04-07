using CommunityToolkit.Mvvm.ComponentModel;
using ManageFootball.Models;
using Microsoft.IdentityModel.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFootball.ViewModels
{
    public partial class AutoCompleteVM:ObservableObject
    {
        [ObservableProperty] public string _searchText;
        [ObservableProperty] public AutoCompleteModel _selectedItem;
        [ObservableProperty] public ObservableCollection<AutoCompleteModel> _autoCompleteSuggest;
        public AutoCompleteVM() {

            AutoCompleteSuggest = new ObservableCollection<AutoCompleteModel>() {
                new AutoCompleteModel{Col1="1êde",Col2="dad2",Col3="3fc"},
                new AutoCompleteModel{Col1="Aaa",Col2="Bas",Col3="Caaa"}
            };

        }
    }
}
