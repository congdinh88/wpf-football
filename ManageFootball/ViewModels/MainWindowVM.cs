using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ManageFootball.ViewModels
{
    public partial class MainWindowVM:ObservableObject
    {
        [ObservableProperty] string text1;
        public MainWindowVM()
        {
            text1 = "Hello d";
            MessageBox.Show("Hello");
        }
    }
}
