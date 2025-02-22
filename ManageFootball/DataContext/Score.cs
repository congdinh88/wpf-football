using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

    public class ScoreConfig : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder.ToTable("Scores");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
            .ValueGeneratedOnAdd();
            builder.Property(s => s.Criteria2)
            .IsRequired(false);
            builder.Property(s => s.Criteria3)
            .IsRequired(false);

            builder.HasOne(s => s.Match)
            .WithMany(s => s.Scores)
            .HasForeignKey(s => s.MatchCode)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Team)
            .WithMany(s => s.Scores)
            .HasForeignKey(s => s.TeamId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
