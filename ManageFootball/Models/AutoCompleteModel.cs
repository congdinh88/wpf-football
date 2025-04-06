using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFootball.Models
{
    public partial class AutoCompleteModel: ObservableObject
    {
        [ObservableProperty] private string _col1="1";
        [ObservableProperty] private string _col2 = "2";
        [ObservableProperty] private string _col3 ="3" ;

    }
}
