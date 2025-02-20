using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace ManageFootball.DataContext
{
    public class AppDbContext: DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public AppDbContext() : base("name=DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TeamConfig());
            modelBuilder.Configurations.Add(new MatchConfig());
            modelBuilder.Configurations.Add(new PlayerConfig());
            modelBuilder.Configurations.Add(new ScoreConfig());
            modelBuilder.Configurations.Add(new StatsConfig());
            b
    }
}
