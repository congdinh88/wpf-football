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
    public class Team
    {
        public int Id { get; set; }
        public string Teams { get; set; }


        //Danh sách tới Match
        public virtual ICollection<Match> Matches1 { get; set; }
        public virtual ICollection<Match> Matches2 { get; set; }

        //Danh sách tới Player
        public virtual ICollection<Player> Players { get; set; }

        //Danh sách tới Score
        public virtual ICollection<Score> Scores { get; set; }

        public virtual ICollection<Stats> Stats { get; set; }

    }

    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();
        }

    }
}
