using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ManageFootball.DataContext
{
    public class Match
    {
        public string Code { get; set; }
        public DateTime Time { get; set; }
        public string Addresse { get; set; }

        //Ngoại khóa tới Team1 
        public int TeamId1 { get; set; }
        public virtual Team Team1 { get; set; }

        //Ngoại khóa tới Team2
        public int TeamId2 { get; set; }
        public virtual Team Team2 { get; set; }

        //Danh sách tới Score
        public virtual ICollection<Score> Scores { get; set; }
        public virtual ICollection<Stats> Stats { get; set; }
    }
    public class MatchConfig : EntityTypeConfiguration<Match>
    {
        public MatchConfig()
        {
            ToTable("Matches");
            HasKey(t => t.Code);


            HasRequired(m => m.Team1)
            .WithMany(t => t.Matches1)
            .HasForeignKey(m => m.TeamId1)
            .WillCascadeOnDelete(false);

            HasRequired(m => m.Team2)
            .WithMany(t => t.Matches2)
            .HasForeignKey(m => m.TeamId2)
            .WillCascadeOnDelete(false);
        }
    }
}
