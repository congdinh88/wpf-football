using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ManageFootball.DataContext
{
    public class Stats
    {
        public int Id { get; set; }

        public string MatchCode { get; set; }
        public virtual Match Match { get; set; }

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public bool? YellowCard { get; set; }
        public bool? RedCard { get; set; }
        public bool? Goals { get; set; }
        public string Time { get; set; }
    }

    public class StatsConfig : EntityTypeConfiguration<Stats>
    {
        public StatsConfig()
        {
            ToTable("Stats");
            HasKey(t => t.Id);

            Property(t => t.YellowCard)
            .IsOptional();
            Property(t => t.RedCard)
            .IsOptional();
            Property(t => t.Goals)
            .IsOptional();

            HasRequired(m => m.Match)
            .WithMany(t => t.Stats)
            .HasForeignKey(t => t.MatchCode)
            .WillCascadeOnDelete(false);

            HasRequired(m => m.Team)
            .WithMany(t => t.Stats)
            .HasForeignKey(t => t.TeamId)
            .WillCascadeOnDelete(false);

            HasRequired(m => m.Player)
            .WithMany(t => t.Stats)
            .HasForeignKey(t => t.PlayerId)
            .WillCascadeOnDelete(false);
        }
    }
}
