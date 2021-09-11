using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Skills")]
    public class ApplicantSkillPoco:IPoco
    {
        public Guid Applicant { get; set; }
        public String Skill { get; set; }
        [Column("Skill_Level")]
        public String SkillLevel { get; set; }
        [Column("Start_Month")]
        public Byte StartMonth { get; set; }
        [Column("Start_Year")]
        public Int32 StartYear { get; set; }
        [Column("End_Month")]
        public Byte EndMonth { get; set; }
        [Column("End_Year")]
        public Int32 EndYear { get; set; }
        [Column("Time_Stamp")]
        [Timestamp]
        public Byte[] TimeStamp { get; set; }
        [Key] public Guid Id { get; set; }

        [NotMapped]
        public byte[] TimeStamp { get; set; }
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
    }
}
