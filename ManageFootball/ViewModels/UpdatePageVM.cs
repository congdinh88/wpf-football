using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using ManageFootball.Models;

namespace ManageFootball.ViewModels
{
    public partial class UpdatePageVM: ObservableObject
    {

        public ObservableCollection<UpdatePageModel> People { get; } = new ObservableCollection<UpdatePageModel>
        {
            new UpdatePageModel { Id = 1, Name = "John Doe", Email = "john@example.com" },
            new UpdatePageModel { Id = 2, Name = "Jane Smith", Email = "jane@test.com" },
            new UpdatePageModel { Id = 3, Name = "Bob Johnson", Email = "bob@domain.org" },
            new UpdatePageModel { Id = 4, Name = "Alice Brown", Email = "alice@mail.net" }
        };
        [ObservableProperty] public string selectedValue;
        [ObservableProperty] public string selectedPerson;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
