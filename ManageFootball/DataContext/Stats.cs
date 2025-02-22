using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

    public class StatsConfig : IEntityTypeConfiguration<Stats>
    {
        public void Configure(EntityTypeBuilder<Stats> builder)
        {
            builder.ToTable("Stats");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.YellowCard)
            .IsRequired(false);
            builder.Property(t => t.RedCard)
            .IsRequired(false);
            builder.Property(t => t.Goals)
            .IsRequired();

            builder.HasOne(s => s.Match)
            .WithMany(s => s.Stats)
            .HasForeignKey(s => s.MatchCode)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Team)
            .WithMany(s => s.Stats)
            .HasForeignKey(s => s.TeamId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Player)
            .WithMany(s => s.Stats)
            .HasForeignKey(s => s.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
