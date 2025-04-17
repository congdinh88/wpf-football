using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ManageFootball.Models
{
    public partial class UpdatePageModel
    {
        
    }

    public class TestDatagrid
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public AutoCompleteModel SelectItemSuggest { get; set; }=new AutoCompleteModel();
    }
    public class AutoCompleteModel
    {
        public int Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }
    }
}
