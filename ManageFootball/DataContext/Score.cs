using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ManageFootball.DataContext
{
    public class Score
    {
        public int Id { get; set; }

        //Ngoại khóa tới Match
        public string MatchCode { get; set; }
        public virtual Match Match { get; set; }

        //Ngoại khóa tới Team
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public int Sco { get; set; }
        public bool? Criteria2 { get; set; }
        public bool? Criteria3 { get; set; }
    }

    public class ScoreConfig : EntityTypeConfiguration<Score>
    {
        public ScoreConfig()
        {
            ToTable("Scores");
            HasKey(t => t.Id);
            Property(t => t.Id)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Criteria2)
            .IsOptional();
            Property(t => t.Criteria3)
            .IsOptional();

            HasRequired(m => m.Match)
            .WithMany(t => t.Scores)
            .HasForeignKey(m => m.MatchCode)
            .WillCascadeOnDelete(false);

            HasRequired(m => m.Team)
            .WithMany(t => t.Scores)
            .HasForeignKey(m => m.TeamId)
            .WillCascadeOnDelete(false);
        }

    }
}
