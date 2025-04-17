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
    public partial class UpdatePageVM
    {

        public ObservableCollection<TestDatagrid> TestDatagrids { get;set; }
        public ObservableCollection<AutoCompleteModel> AutoCompleteModels { get; set; }
       
        public UpdatePageVM() { 
            TestDatagrids= new ObservableCollection<TestDatagrid>();
            AutoCompleteModels = new ObservableCollection<AutoCompleteModel>
            {
                new AutoCompleteModel { Col1 = 1, Col2 = "John Doe", Col3 = "john@example.com" },
                new AutoCompleteModel { Col1 = 2, Col2 = "Jane Smith", Col3 = "jane@test.com" },
                new AutoCompleteModel { Col1 = 3, Col2 = "Bob Johnson", Col3 = "bob@domain.org" },
                new AutoCompleteModel { Col1 = 4, Col2 = "Alice Brown", Col3 = "alice@mail.net" }
            };
        }
    }
}
