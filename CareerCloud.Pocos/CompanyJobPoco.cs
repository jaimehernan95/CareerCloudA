using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Company_Jobs")]
    public class CompanyJobPoco : IPoco
    {
        public Guid Company { get; set; }
        [Column("Profile_Created")]
        public DateTime ProfileCreated { get; set; }
        [Column("Is_Inactive")]
        public Boolean IsInactive { get; set; }
        [Column("Is_Company_Hidden")]
        public Boolean IsCompanyHidden { get; set; }
        [Column("Time_Stamp")]
        [Timestamp]
        public Byte[] TimeStamp { get; set; }
        [Key] public Guid Id { get; set; }

        public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJobApplications { set; get; }
        public virtual ICollection<CompanyJobSkillPoco> CompanyJobSkills { set; get; }
        public virtual CompanyProfilePoco CompanyProfile { set; get; }
        public virtual ICollection<CompanyJobEducationPoco> CompanyJobEducations { set; get; }
        public virtual ICollection<CompanyJobDescriptionPoco> CompanyJobDescriptions { set; get; }
    }
}
