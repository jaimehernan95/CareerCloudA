using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CareerCloud.EntityFrameworkDataAccess
{
    class CareerCloudContext : DbContext
    {
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            string _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;

            optionsBuilder.UseSqlServer(_connStr);

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducationPoco>
                (entity =>
                {
                    entity.HasOne(e => e.ApplicantProfile)
                    .WithMany(p => p.ApplicantEducations)
                    .HasForeignKey(e => e.Applicant);
                }
                );
            modelBuilder.Entity<ApplicantProfilePoco>
              (entity =>
              {
                  entity.HasOne(e => e.SecurityLogin)
                  .WithMany(p => p.ApplicantProfiles)
                  .HasForeignKey(e => e.Login);
              }
              );
            modelBuilder.Entity<ApplicantResumePoco>
          (entity =>
          {
              entity.HasOne(e => e.ApplicantProfile)
              .WithMany(p => p.ApplicantResumes)
              .HasForeignKey(e => e.Applicant);
          }
          );
            modelBuilder.Entity<ApplicantSkillPoco>
     (entity =>
     {
         entity.HasOne(e => e.ApplicantProfile)
         .WithMany(p => p.ApplicantSkills)
         .HasForeignKey(e => e.Applicant);
     }
     );
            modelBuilder.Entity<ApplicantJobApplicationPoco>
(entity =>
{
    entity.HasOne(e => e.ApplicantProfile)
    .WithMany(p => p.ApplicantJobApplications)
    .HasForeignKey(e => e.Applicant);

}
);
            modelBuilder.Entity<ApplicantJobApplicationPoco>
(entity =>
{
    entity.HasOne(e => e.CompanyJob)
        .WithMany(p => p.ApplicantJobApplications)
        .HasForeignKey(e => e.Job);

}
);

            modelBuilder.Entity<ApplicantResumePoco>
(entity =>
{
    entity.HasOne(e => e.ApplicantProfile)
        .WithMany(p => p.ApplicantResumes)
        .HasForeignKey(e => e.Applicant);

}
);
            modelBuilder.Entity<ApplicantWorkHistoryPoco>
(entity =>
{
    entity.HasOne(e => e.ApplicantProfile)
    .WithMany(p => p.ApplicantWorkHistories)
    .HasForeignKey(e => e.Applicant);

}
);
            modelBuilder.Entity<SecurityLoginsLogPoco>
(entity =>
{
    entity.HasOne(e => e.SecurityLogin)
    .WithMany(p => p.SecurityLoginsLogs)
    .HasForeignKey(e => e.Login);

}
);
            modelBuilder.Entity<SecurityLoginsRolePoco>
(entity =>
{
    entity.HasOne(e => e.SecurityLogin)
        .WithMany(p => p.SecurityLoginsRoles)
        .HasForeignKey(e => e.Login);

}
);
            modelBuilder.Entity<SecurityLoginsRolePoco>
(entity =>
{
    entity.HasOne(e => e.SecurityRole)
            .WithMany(p => p.SecurityLoginsRoles)
            .HasForeignKey(e => e.Role);

}
);
            modelBuilder.Entity<CompanyJobSkillPoco>
(entity =>
{
    entity.HasOne(e => e.CompanyJob)
                .WithMany(p => p.CompanyJobSkills)
                .HasForeignKey(e => e.Job);

}
);
            modelBuilder.Entity<CompanyJobPoco>
(entity =>
{
    entity.HasOne(e => e.CompanyProfile)
                .WithMany(p => p.CompanyJobs)
                .HasForeignKey(e => e.Company);

}
);
            modelBuilder.Entity<CompanyJobEducationPoco>
(entity =>
{
    entity.HasOne(e => e.CompanyJob)
                .WithMany(p => p.CompanyJobEducations)
                .HasForeignKey(e => e.Job);

}
);
            modelBuilder.Entity<CompanyJobDescriptionPoco>
(entity =>
{
    entity.HasOne(e => e.CompanyJob)
          .WithMany(p => p.CompanyJobDescriptions)
          .HasForeignKey(e => e.Job);

}
);

            modelBuilder.Entity<CompanyLocationPoco>
(entity =>
{
    entity.HasOne(e => e.CompanyProfile)
              .WithMany(p => p.CompanyLocations)
              .HasForeignKey(e => e.Company);


}
);


            modelBuilder.Entity<CompanyDescriptionPoco>
(entity =>
{
    entity.HasOne(e => e.SystemLanguageCode)
                      .WithMany(p => p.CompanyDescriptions)
                      .HasForeignKey(e => e.LanguageId);

}
);

            modelBuilder.Entity<CompanyJobEducationPoco>
(entity =>
{
    entity.HasOne(e => e.CompanyJob)
                      .WithMany(p => p.CompanyJobEducations)

                      .HasForeignKey(e => e.Job);

}
);
            modelBuilder.Entity<CompanyDescriptionPoco>
(entity =>
{
    entity.HasOne(e => e.CompanyProfile)
              .WithMany(p => p.CompanyDescriptions)

              .HasForeignKey(e => e.Company);
    entity.HasOne(s1 => s1.SystemLanguageCode)
    .WithMany(cd => cd.CompanyDescriptions)
    .HasForeignKey(f => f.LanguageId);


}

);
            modelBuilder.Entity<ApplicantProfilePoco>
(entity =>
{
    entity.HasOne(e => e.SystemCountry)
                  .WithMany(p => p.ApplicantProfiles)

                  .HasForeignKey(e => e.Country);

}

);
            modelBuilder.Entity<ApplicantWorkHistoryPoco>
(entity =>
{
    entity.HasOne(e => e.SystemCountryCode)
                      .WithMany(p => p.ApplicantWorkHistories)

                      .HasForeignKey(e => e.CountryCode);

}

);

            base.OnModelCreating(modelBuilder);
        }

        private void entity(EntityTypeBuilder<ApplicantProfilePoco> obj)
        {
            throw new NotImplementedException();
        }
    }
}