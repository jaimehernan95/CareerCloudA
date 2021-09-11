using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Work_History")]
    public class ApplicantWorkHistoryPoco:IPoco
    {
        public Guid Applicant { get; set; }
        [Column("Company_Name")]
        public String CompanyName { get; set; }
        [Column("Country_Code")]
        public String CountryCode { get; set; }
        public String Location { get; set; }
        [Column("Job_Title")]
        public String JobTitle { get; set; }
        [Column("Job_Description")]
        public String JobDescription { get; set; }
        [Column("Start_Month")]
        public Int16 StartMonth { get; set; }
        [Column("Start_Year")]
        public Int32 StartYear { get; set; }
        [Column("End_Month")]
        public Int16 EndMonth { get; set; }
        [Column("End_Year")]
        public Int32 EndYear { get; set; }
        [Column("Time_Stamp")]
        [Timestamp]
        public Byte[] TimeStamp { get; set; }
        [Key] public Guid Id { get; set; }

        [NotMapped]
        public byte[] TimeStamp { get; set; }
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
        public virtual SystemCountryCodePoco SystemCountryCode { get; set; }
    }
}
