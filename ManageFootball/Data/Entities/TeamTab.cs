using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ManageFootball.Data.Entities
{
    [Table("TeamTabls")]
    public class TeamTab
    {
        [Key] public string Code { get; set; }
        [Required] public string Name { get; set; }
    }
    public class TeamTabConfiguration : IEntityTypeConfiguration<TeamTab>
    {
        public void Configure(EntityTypeBuilder<TeamTab> builder)
        {
           
        }
    }
}
