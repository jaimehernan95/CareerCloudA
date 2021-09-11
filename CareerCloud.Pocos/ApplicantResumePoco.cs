using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Resumes")]
    public class ApplicantResumePoco:IPoco
    {
        public Guid Applicant { get; set; }
        public String Resume { get; set; }
        [Column("Last_Updated")]
        public DateTime? LastUpdated { get; set; }
        [Key] public Guid Id { get; set; }
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
    }
}
