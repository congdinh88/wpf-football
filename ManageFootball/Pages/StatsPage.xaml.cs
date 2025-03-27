using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ManageFootball.Pages
{
    /// <summary>
    /// Interaction logic for StatsPage.xaml
    /// </summary>
    public partial class StatsPage : Page
    {
        public ObservableCollection<RanksVM> Ranks;
        public ObservableCollection<GoalsVM> Goals;
        public ObservableCollection<CardVM> Card;
        public StatsPage()
        {
            InitializeComponent();
            Ranks = new ObservableCollection<RanksVM> 
            {
               new RanksVM { Team = "T1", Match = 1, Win = 1, Draw = 1, Loss = 1, Goal = 1, GoalConced = 2, GoalDifference = -1, Score = 1 },
               new RanksVM { Team = "T1", Match = 1, Win = 1, Draw = 1, Loss = 1, Goal = 1, GoalConced = 2, GoalDifference = -1, Score = 1 }
            };
            Goals = new ObservableCollection<GoalsVM>
            {
                new GoalsVM{Team= "T1", FullName= "Nguyễn Văn A", Number=10, Goals=5},
                new GoalsVM{Team= "T1", FullName= "Nguyễn Văn A", Number=10, Goals=5}
            };

            Card = new ObservableCollection<CardVM>
            {
                new CardVM{Team= "T1", YellowCard= 5, RedCard= 1},
                new CardVM{Team= "T1", YellowCard= 5, RedCard= 1}
            };

            DataContext = this;
            dataGridRank.ItemsSource = Ranks;
            dataGridTopScorer.ItemsSource = Goals;
            dataGridCard.ItemsSource = Card;
        }
        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            if (grid != null)
            {
                int rowIndex = e.Row.GetIndex();
                e.Row.Header = (rowIndex + 1).ToString();
            }
        }
    }
    public class RanksVM
    {
        public string? Team { get; set; }
        public int Match { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Loss { get; set; }
        public int Goal { get; set; }
        public int GoalConced { get; set; }
        public int GoalDifference { get; set; }
        public int Score {  get; set; }
    }
    public class GoalsVM
    {
        public string? Team { get; set; }
        public string? FullName { get; set; }
        public int Number { get; set; }
        public int Goals { get; set; }
    }

    public class CardVM
    {
        public string? Team { get; set; }
        public int YellowCard { get; set; }
        public int RedCard { get; set; }
    }
}
