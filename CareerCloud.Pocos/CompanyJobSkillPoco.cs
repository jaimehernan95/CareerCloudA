using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Company_Job_Skills")]
    public class CompanyJobSkillPoco : IPoco
    {
        public Guid Job { get; set; }
        public String Skill { get; set; }
        [Column("Skill_Level")]
        public String SkillLevel { get; set; }
        public Int32 Importance { get; set; }
        [Column("Time_Stamp")]
        [Timestamp]
        public Byte[] TimeStamp { get; set; }
        [Key] public Guid Id { get; set; }

        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}
