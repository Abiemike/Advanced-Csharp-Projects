using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace EntityFrameworkDataAccess
{
    public class CareerCloudContext:DbContext
    {


          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                              optionsBuilder.UseSqlServer("Data Source=ABEBE-TEGEGNE\\AB;Initial Catalog=JOB_PORTAL_DB;TrustServerCertificate=True;Integrated Security=True;");
            }
        }

        // Expose DbSet properties for each POCO class
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }



        // Define foreign key relationships using OnModelCreating method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<ApplicantEducationPoco>(entity =>
            {
                entity.HasOne(a => a.ApplicantProfile)
                .WithMany(a => a.ApplicantEducations)
                .HasForeignKey(a => a.Applicant);
            });

            modelBuilder.Entity<ApplicantJobApplicationPoco>(entity =>
            {
                entity.HasOne(a => a.ApplicantProfile)
                .WithMany(a => a.ApplicantJobApplications)
                .HasForeignKey(a => a.Applicant);
            });
            modelBuilder.Entity<ApplicantJobApplicationPoco>(entity =>
            {
                entity.HasOne(a => a.CompanyJob)
                .WithMany(a => a.ApplicantJobApplications)
                .HasForeignKey(a => a.Job);
            });

            modelBuilder.Entity<ApplicantProfilePoco>(entity =>
            {
                entity.HasOne(a => a.SecurityLogin)
                .WithMany(a => a.ApplicantProfiles)
                .HasForeignKey(a => a.Login);
            });
            modelBuilder.Entity<ApplicantProfilePoco>(entity =>
            {
                entity.HasOne(a => a.SystemCountryCode)
                .WithMany(a => a.ApplicantProfiles)
                .HasForeignKey(a => a.Country);
            });

            modelBuilder.Entity<ApplicantResumePoco>(entity =>
            {
                entity.HasOne(a => a.ApplicantProfile)
                .WithMany(a => a.ApplicantResumes)
                .HasForeignKey(a => a.Applicant);
            });

            modelBuilder.Entity<ApplicantSkillPoco>(entity =>
            {
                entity.HasOne(a => a.ApplicantProfile)
                .WithMany( a =>     a.ApplicantSkills)
                .HasForeignKey(a=> a.Applicant);
            });

            modelBuilder.Entity<ApplicantWorkHistoryPoco>(entity =>
            {
                entity.HasOne(a => a.ApplicantProfile)
                .WithMany(a => a.ApplicantWorkHistorys)
                .HasForeignKey(a => a .Applicant);
            });
            modelBuilder.Entity<ApplicantWorkHistoryPoco>(entity =>
            {
                entity.HasOne(a => a.SystemCountryCode)
                .WithMany(a => a.ApplicantWorkHistory)
                .HasForeignKey(a => a.CountryCode);
            });

            modelBuilder.Entity<CompanyDescriptionPoco>(entity =>
            {
                entity.HasOne(a => a.CompanyProfile)
                .WithMany(a => a.CompanyDescriptions)
                .HasForeignKey(a => a.Company);
            });
            modelBuilder.Entity<CompanyDescriptionPoco>(entity =>
            {
                entity.HasOne(a => a.SystemLanguageCode)
                .WithMany(a => a.CompanyDescription)
                .HasForeignKey(a => a.LanguageId);
            });

            modelBuilder.Entity<CompanyJobDescriptionPoco>(entity =>
            {
                entity.HasOne(a => a.CompanyJob)
                .WithMany(a => a.CompanyJobDescriptions)
                .HasForeignKey(a => a.Job);
            });

            modelBuilder.Entity<CompanyJobEducationPoco>(entity =>
            {
                entity.HasOne(a => a.CompanyJob)
                .WithMany(a => a.CompanyJobEducations)
                .HasForeignKey(a => a.Job);
            });

            modelBuilder.Entity<CompanyJobPoco>(entity =>
            {
                entity.HasOne(a => a.CompanyProfile)
                .WithMany(a => a.CompanyJobs)
                .HasForeignKey(a => a.Company);
            });

            modelBuilder.Entity<CompanyJobSkillPoco>(entity =>
            {
                entity.HasOne(a => a.CompanyJob)
                .WithMany(a => a.CompanyJobSkills)
                .HasForeignKey(a => a.Job);
            });

            modelBuilder.Entity<CompanyLocationPoco>(entity =>
            {
                entity.HasOne(a => a.CompanyProfile)
                .WithMany(a => a.CompanyLocations)
                .HasForeignKey(a => a.Company);
            });

            modelBuilder.Entity<SecurityLoginsLogPoco>(entity =>
            {
                entity.HasOne(      a => a.SecurityLogin)
                .WithMany(a => a.SecurityLoginsLogs)
                .HasForeignKey(a => a.Login);
            });

            modelBuilder.Entity<SecurityLoginsRolePoco>(entity =>
            {
                entity.HasOne(a => a.SecurityLogin)
                .WithMany(a => a.SecurityLoginsRoles)
                .HasForeignKey(a => a.Login);
            });
            modelBuilder.Entity<SecurityLoginsRolePoco>(entity =>
            {
                entity.HasOne(a => a.SecurityRole)
                .WithMany(a => a.SecurityLoginsRoles)
                .HasForeignKey(a => a.Role);
            });
            modelBuilder.Entity<CompanyLocationPoco>(entity =>
            {
                entity.HasOne(a => a.SystemCountryCode)
                .WithMany(a => a.CompanyLocation)
                .HasForeignKey(a => a.CountryCode);
            });

        }
    }
}


