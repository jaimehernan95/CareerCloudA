using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CareerCloudCore.UnitTests.Assignment4
{
    [TestCategory("Assignment 4 Tests")]
    [TestClass]
    public class Assignment4Marking
    {
        private const string _assemblyUnderTest = "CareerCloud.EntityFrameworkDataAccess.dll";

        private SystemCountryCodePoco _systemCountry;
        private ApplicantEducationPoco _applicantEducation;
        private ApplicantProfilePoco _applicantProfile;
        private ApplicantJobApplicationPoco _applicantJobApplication;
        private CompanyJobPoco _companyJob;
        private CompanyProfilePoco _companyProfile;
        private CompanyDescriptionPoco _companyDescription;
        private SystemLanguageCodePoco _systemLangCode;
        private ApplicantResumePoco _applicantResume;
        private ApplicantSkillPoco _applicantSkills;
        private ApplicantWorkHistoryPoco _appliantWorkHistory;
        private CompanyJobEducationPoco _companyJobEducation;
        private CompanyJobSkillPoco _companyJobSkill;
        private CompanyJobDescriptionPoco _companyJobDescription;
        private CompanyLocationPoco _companyLocation;
        private SecurityLoginPoco _securityLogin;
        private SecurityLoginsLogPoco _securityLoginLog;
        private SecurityRolePoco _securityRole;
        private SecurityLoginsRolePoco _securityLoginRole;

        private Type[] _types;

        [TestInitialize]
        public void Init_Pocos()
        {
            EFGenericRepository<ApplicantEducationPoco> repo = new EFGenericRepository<ApplicantEducationPoco>();
            _types = Assembly.LoadFrom(_assemblyUnderTest).GetTypes();

            SystemCountry_Init();
            CompanyProfile_Init();
            SystemLangCode_Init();
            CompanyDescription_Init();
            CompanyJob_Init();
            CompanyJobEducation_Init();
            CompanyJobSkill_Init();
            CompanyJobDescription_Init();
            CompanyLocation_Init();
            SecurityLogin_Init();
            ApplicantProfile_Init();
            SecurityLoginLog_Init();
            SecurityRole_Init();
            SecurityLoginRole_Init();
            ApplicantEducation_Init();
            ApplicantResume_Init();
            ApplicantSkills_Init();
            AappliantWorkHistory_Init();
            ApplicantJobApplication_Init();
        }

        #region PocoInitialization
        private void ApplicantJobApplication_Init()
        {
            _applicantJobApplication = new ApplicantJobApplicationPoco()
            {
                Id = Guid.NewGuid(),
                ApplicationDate = Faker.Date.Recent(),
                Applicant = _applicantProfile.Id,
                Job = _companyJob.Id
            };
        }

        private void AappliantWorkHistory_Init()
        {
            _appliantWorkHistory = new ApplicantWorkHistoryPoco()
            {
                Id = Guid.NewGuid(),
                Applicant = _applicantProfile.Id,
                CompanyName = Truncate(Faker.Lorem.Sentence(), 150),
                CountryCode = _systemCountry.Code,
                EndMonth = 12,
                EndYear = 1999,
                JobDescription = Truncate(Faker.Lorem.Sentence(), 500),
                JobTitle = Truncate(Faker.Lorem.Sentence(), 50),
                Location = Faker.Address.StreetName(),
                StartMonth = 1,
                StartYear = 1999
            };
        }

        private void ApplicantSkills_Init()
        {
            _applicantSkills = new ApplicantSkillPoco()
            {
                Applicant = _applicantProfile.Id,
                Id = Guid.NewGuid(),
                EndMonth = 12,
                EndYear = 1999,
                Skill = Truncate(Faker.Lorem.Sentence(), 100),
                SkillLevel = Truncate(Faker.Lorem.Sentence(), 10),
                StartMonth = 1,
                StartYear = 1999
            };
        }

        private void ApplicantResume_Init()
        {
            _applicantResume = new ApplicantResumePoco()
            {
                Applicant = _applicantProfile.Id,
                Id = Guid.NewGuid(),
                Resume = Faker.Lorem.Paragraph(),
                LastUpdated = Faker.Date.Recent()
            };
        }

        private void ApplicantEducation_Init()
        {
            _applicantEducation = new ApplicantEducationPoco()
            {
                Id = Guid.NewGuid(),
                Applicant = _applicantProfile.Id,
                Major = Faker.Education.Major(),
                CertificateDiploma = Faker.Education.Major(),
                StartDate = Faker.Date.Past(3),
                CompletionDate = Faker.Date.Past(1),
                CompletionPercent = (byte)Faker.Number.RandomNumber(1)
            };
        }

        private void SecurityLoginRole_Init()
        {
            _securityLoginRole = new SecurityLoginsRolePoco()
            {
                Id = Guid.NewGuid(),
                Login = _securityLogin.Id,
                Role = _securityRole.Id
            };
        }

        private void SecurityRole_Init()
        {
            _securityRole = new SecurityRolePoco()
            {
                Id = Guid.NewGuid(),
                IsInactive = true,
                Role = Truncate(Faker.Company.Industry(), 50)

            };
        }

        private void SecurityLoginLog_Init()
        {
            _securityLoginLog = new SecurityLoginsLogPoco()
            {
                Id = Guid.NewGuid(),
                IsSuccesful = true,
                Login = _securityLogin.Id,
                LogonDate = Faker.Date.PastWithTime(),
                SourceIP = Faker.Internet.IPv4().PadRight(15)
            };
        }

        private void ApplicantProfile_Init()
        {
            _applicantProfile = new ApplicantProfilePoco
            {
                Id = Guid.NewGuid(),
                Login = _securityLogin.Id,
                City = Faker.Address.CityPrefix(),
                Country = _systemCountry.Code,
                Currency = "CAN".PadRight(10),
                CurrentRate = 71.25M,
                CurrentSalary = 67500,
                Province = Truncate(Faker.Address.Province(), 10).PadRight(10),
                Street = Truncate(Faker.Address.StreetName(), 100),
                PostalCode = Truncate(Faker.Address.CanadianZipCode(), 20).PadRight(20)
            };
        }

        private void SecurityLogin_Init()
        {
            _securityLogin = new SecurityLoginPoco()
            {
                Login = Faker.User.Email(),
                AgreementAccepted = Faker.Date.PastWithTime(),
                Created = Faker.Date.PastWithTime(),
                EmailAddress = Faker.User.Email(),
                ForceChangePassword = false,
                FullName = Faker.Name.FullName(),
                Id = Guid.NewGuid(),
                IsInactive = false,
                IsLocked = false,
                Password = Faker.User.Password(),
                PasswordUpdate = Faker.Date.Forward(),
                PhoneNumber = "555-416-9889",
                PrefferredLanguage = "EN".PadRight(10)
            };
        }

        private void CompanyLocation_Init()
        {
            _companyLocation = new CompanyLocationPoco()
            {
                Id = Guid.NewGuid(),
                City = Faker.Address.CityPrefix(),
                Company = _companyProfile.Id,
                CountryCode = _systemCountry.Code,
                Province = Faker.Address.ProvinceAbbreviation(),
                Street = Faker.Address.StreetName(),
                PostalCode = Faker.Address.CanadianZipCode()
            };
        }

        private void CompanyJobDescription_Init()
        {
            _companyJobDescription = new CompanyJobDescriptionPoco()
            {
                Id = Guid.NewGuid(),
                Job = _companyJob.Id,
                JobDescriptions = Truncate(Faker.Lorem.Paragraph(), 999),
                JobName = Truncate(Faker.Lorem.Sentence(), 99)
            };
        }

        private void CompanyJobSkill_Init()
        {
            _companyJobSkill = new CompanyJobSkillPoco()
            {
                Id = Guid.NewGuid(),
                Importance = 2,
                Job = _companyJob.Id,
                Skill = Truncate(Faker.Lorem.Sentence(), 100),
                SkillLevel = String.Concat(Faker.Lorem.Letters(10))
            };
        }

        private void CompanyJobEducation_Init()
        {
            _companyJobEducation = new CompanyJobEducationPoco()
            {
                Id = Guid.NewGuid(),
                Importance = 2,
                Job = _companyJob.Id,
                Major = Truncate(Faker.Lorem.Sentence(), 100)
            };
        }

        private void CompanyJob_Init()
        {
            _companyJob = new CompanyJobPoco()
            {
                Id = Guid.NewGuid(),
                Company = _companyProfile.Id,
                IsCompanyHidden = false,
                IsInactive = false,
                ProfileCreated = Faker.Date.Past()
            };
        }

        private void CompanyDescription_Init()
        {
            _companyDescription = new CompanyDescriptionPoco()
            {
                Id = Guid.NewGuid(),
                CompanyDescription = Faker.Company.CatchPhrase(),
                CompanyName = Faker.Company.CatchPhrasePos(),
                LanguageId = _systemLangCode.LanguageID,
                Company = _companyProfile.Id
            };
        }

        private void SystemLangCode_Init()
        {
            _systemLangCode = new SystemLanguageCodePoco()
            {
                LanguageID = String.Concat(Faker.Lorem.Letters(10)),
                NativeName = Truncate(Faker.Lorem.Sentence(), 50),
                Name = Truncate(Faker.Lorem.Sentence(), 50)
            };
        }

        private void CompanyProfile_Init()
        {
            _companyProfile = new CompanyProfilePoco()
            {
                CompanyWebsite = Faker.Internet.Host(),
                ContactName = Faker.Name.FullName(),
                ContactPhone = "416-555-8799",
                RegistrationDate = Faker.Date.Past(),
                Id = Guid.NewGuid(),
                CompanyLogo = new byte[10]
            };
        }

        private void SystemCountry_Init()
        {
            _systemCountry = new SystemCountryCodePoco
            {
                Code = String.Concat(Faker.Lorem.Letters(10)),
                Name = Truncate(Faker.Name.FullName(), 50)
            };
        }

        #endregion PocoInitialization

        [TestMethod]
        public void Assignment4_DeepDive_CRUD_Test()
        {
            SystemCountryCodeAdd();
            SystemCountryCodeCheck();
            SystemCountryCodeUpdate();
            SystemCountryCodeCheck();

            SystemLanguageCodeAdd();
            SystemLanguageCodeCheck();
            SystemLanguageCodeUpdate();
            SystemLanguageCodeCheck();

            CompanyProfileAdd();
            CompanyProfileCheck();
            CompanyProfileUpdate();
            CompanyProfileCheck();

            CompanyDescriptionAdd();
            CompanyDescriptionCheck();
            CompanyDescriptionUpdate();
            CompanyDescriptionCheck();

            CompanyJobAdd();
            CompanyJobCheck();
            CompanyJobUpdate();
            CompanyJobCheck();

            CompanyJobDescriptionAdd();
            CompanyJobDescriptionCheck();
            CompanyJobDescriptionUpdate();
            CompanyJobDescriptionCheck();

            CompanyLocationAdd();
            CompanyLocationCheck();
            CompanyLocationUpdate();
            CompanyLocationCheck();

            CompanyJobEducationAdd();
            CompanyJobEducationCheck();
            CompanyJobEducationUpdate();
            CompanyJobEducationCheck();

            CompanyJobSkillAdd();
            CompanyJobSkillCheck();
            CompanyJobSkillUpdate();
            CompanyJobSkillCheck();

            SecurityLoginAdd();
            SecurityLoginCheck();
            SecurityLoginUpdate();
            SecurityLoginCheck();

            SecurityLoginLogAdd();
            SecurityLoginLogCheck();
            SecurityLoginLogUpdate();
            SecurityLoginLogCheck();

            SecurityRoleAdd();
            SecurityRoleCheck();
            SecurityRoleUpdate();
            SecurityRoleCheck();

            SecurityLoginRoleAdd();
            SecurityLoginRoleCheck();

            ApplicantProfileAdd();
            ApplicantProfileCheck();
            ApplicantProfileUpdate();
            ApplicantProfileCheck();

            ApplicantEducationAdd();
            ApplicantEducationCheck();
            ApplicantEducationUpdate();
            ApplicantEducationCheck();

            ApplicantJobApplicationAdd();
            ApplicantJobApplicationCheck();
            ApplicantJobApplicationUpdate();
            ApplicantJobApplicationCheck();

            ApplicantResumeAdd();
            ApplicantResumeCheck();
            ApplicantResumeUpdate();
            ApplicantResumeCheck();

            ApplicantSkillAdd();
            ApplicantSkillCheck();
            ApplicantSkillUpdate();
            ApplicantSkillCheck();

            ApplicantWorkHistoryAdd();
            ApplicantWorkHistoryCheck();
            ApplicantWorkHistoryUpdate();
            ApplicantWorkHistoryCheck();

            #region Cleanup
            ApplicantWorkHistoryRemove();
            ApplicantSkillRemove();
            ApplicantResumeRemove();

            ApplicantJobApplicationRemove();
            ApplicantEducationRemove();
            ApplicantProfileRemove();

            SecurityLoginRoleRemove();
            SecurityRoleRemove();
            SecurityLoginLogRemove();
            SecurityLoginRemove();

            CompanyJobSkillRemove();
            CompanyJobEducationRemove();
            CompanyLocationRemove();
            CompanyJobDescRemove();
            CompanyJobRemove();
            CompanyDescriptionRemove();
            CompanyProfileRemove();

            SystemLanguageCodeRemove();
            SystemCountryCodeRemove();
            #endregion Cleanup
        }

        
        #region AddImplementation
        private void ApplicantWorkHistoryAdd()
        {
            EFGenericRepository<ApplicantWorkHistoryPoco> applicantWorkHistoryRepository = new EFGenericRepository<ApplicantWorkHistoryPoco>();
            applicantWorkHistoryRepository.Add(new ApplicantWorkHistoryPoco[] { _appliantWorkHistory });
        }

        private void ApplicantSkillAdd()
        {
            EFGenericRepository<ApplicantSkillPoco> applicantSkillRepository = new EFGenericRepository<ApplicantSkillPoco>();
            applicantSkillRepository.Add(new ApplicantSkillPoco[] { _applicantSkills });
        }

        private void ApplicantResumeAdd()
        {
            EFGenericRepository<ApplicantResumePoco> applicantResumeRepository = new EFGenericRepository<ApplicantResumePoco>();
            applicantResumeRepository.Add(new ApplicantResumePoco[] { _applicantResume });
        }

        private void ApplicantJobApplicationAdd()
        {
            EFGenericRepository<ApplicantJobApplicationPoco> applicantJobApplicationRepository = new EFGenericRepository<ApplicantJobApplicationPoco>();
            applicantJobApplicationRepository.Add(new ApplicantJobApplicationPoco[] { _applicantJobApplication });
        }

        private void ApplicantEducationAdd()
        {
            EFGenericRepository<ApplicantEducationPoco> applicantEducationRepository = new EFGenericRepository<ApplicantEducationPoco>();
            applicantEducationRepository.Add(new ApplicantEducationPoco[] { _applicantEducation });
        }

        private void ApplicantProfileAdd()
        {
            EFGenericRepository<ApplicantProfilePoco> applicantProfileRepository = new EFGenericRepository<ApplicantProfilePoco>();
            applicantProfileRepository.Add(new ApplicantProfilePoco[] { _applicantProfile });
        }

        private void SecurityLoginRoleAdd()
        {
            EFGenericRepository<SecurityLoginsRolePoco> securityLoginRoleRepository = new EFGenericRepository<SecurityLoginsRolePoco>();
            securityLoginRoleRepository.Add(new SecurityLoginsRolePoco[] { _securityLoginRole });
        }

        private void SecurityRoleAdd()
        {
            EFGenericRepository<SecurityRolePoco> securityRoleRepository = new EFGenericRepository<SecurityRolePoco>();
            securityRoleRepository.Add(new SecurityRolePoco[] { _securityRole });
        }

        private void SecurityLoginLogAdd()
        {
            EFGenericRepository<SecurityLoginsLogPoco> securityLoginLogRepository = new EFGenericRepository<SecurityLoginsLogPoco>();
            securityLoginLogRepository.Add(new SecurityLoginsLogPoco[] { _securityLoginLog });
        }

        private void SecurityLoginAdd()
        {
            EFGenericRepository<SecurityLoginPoco> securityLoginRepository = new EFGenericRepository<SecurityLoginPoco>();
            securityLoginRepository.Add(new SecurityLoginPoco[] { _securityLogin });
        }

        private void CompanyJobSkillAdd()
        {
            EFGenericRepository<CompanyJobSkillPoco> companyJobSkillRepository = new EFGenericRepository<CompanyJobSkillPoco>();
            companyJobSkillRepository.Add(new CompanyJobSkillPoco[] { _companyJobSkill });
        }

        private void CompanyJobEducationAdd()
        {
            EFGenericRepository<CompanyJobEducationPoco> companyJobEducationRepo = new EFGenericRepository<CompanyJobEducationPoco>();
            companyJobEducationRepo.Add(new CompanyJobEducationPoco[] { _companyJobEducation });
        }

        private void CompanyLocationAdd()
        {
            EFGenericRepository<CompanyLocationPoco> companyLocationRepo = new EFGenericRepository<CompanyLocationPoco>();
            companyLocationRepo.Add(new CompanyLocationPoco[] { _companyLocation });
        }

        private void CompanyJobDescriptionAdd()
        {
            EFGenericRepository<CompanyJobDescriptionPoco> companyJobDescRepo = new EFGenericRepository<CompanyJobDescriptionPoco>();
            companyJobDescRepo.Add(new CompanyJobDescriptionPoco[] { _companyJobDescription });
        }

        private void CompanyJobAdd()
        {
            EFGenericRepository<CompanyJobPoco> companyJobRepo = new EFGenericRepository<CompanyJobPoco>();
            companyJobRepo.Add(new CompanyJobPoco[] { _companyJob });
        }

        private void CompanyDescriptionAdd()
        {
            EFGenericRepository<CompanyDescriptionPoco> companyDescriptionRepo = new EFGenericRepository<CompanyDescriptionPoco>();
            companyDescriptionRepo.Add(new CompanyDescriptionPoco[] { _companyDescription });
        }

        private void CompanyProfileAdd()
        {
            EFGenericRepository<CompanyProfilePoco> companyProfileRepo = new EFGenericRepository<CompanyProfilePoco>();
            companyProfileRepo.Add(new CompanyProfilePoco[] { _companyProfile });
        }

        private void SystemLanguageCodeAdd()
        {
            EFGenericRepository<SystemLanguageCodePoco> systemLanguageCodeRepo = new EFGenericRepository<SystemLanguageCodePoco>();
            systemLanguageCodeRepo.Add(new SystemLanguageCodePoco[] { _systemLangCode });
        }

        private void SystemCountryCodeAdd()
        {
            EFGenericRepository<SystemCountryCodePoco> systemCountryCodeRepository = new EFGenericRepository<SystemCountryCodePoco>();
            systemCountryCodeRepository.Add(new SystemCountryCodePoco[] { _systemCountry });
        }


        #endregion AddImplementation

        #region CheckImplementation
        private void ApplicantSkillCheck()
        {
            EFGenericRepository<ApplicantSkillPoco> applicantSkillRepository = new EFGenericRepository<ApplicantSkillPoco>();
            ApplicantSkillPoco applicantSkillPoco = applicantSkillRepository.GetSingle(t => t.Id == _applicantSkills.Id);
            Assert.IsNotNull(applicantSkillPoco);
            Assert.AreEqual(_applicantSkills.Id, applicantSkillPoco.Id);
            Assert.AreEqual(_applicantSkills.Applicant, applicantSkillPoco.Applicant);
            Assert.AreEqual(_applicantSkills.Skill, applicantSkillPoco.Skill);
            Assert.AreEqual(_applicantSkills.SkillLevel, applicantSkillPoco.SkillLevel);
            Assert.AreEqual(_applicantSkills.StartMonth, applicantSkillPoco.StartMonth);
            Assert.AreEqual(_applicantSkills.StartYear, applicantSkillPoco.StartYear);
            Assert.AreEqual(_applicantSkills.EndMonth, applicantSkillPoco.EndMonth);
            Assert.AreEqual(_applicantSkills.EndYear, applicantSkillPoco.EndYear);
        }

        private void ApplicantResumeCheck()
        {
            EFGenericRepository<ApplicantResumePoco> applicantResumeRepository = new EFGenericRepository<ApplicantResumePoco>();
            ApplicantResumePoco applicantResumePoco = applicantResumeRepository.GetSingle(t => t.Id == _applicantResume.Id);
            Assert.IsNotNull(applicantResumePoco);
            Assert.AreEqual(_applicantResume.Id, applicantResumePoco.Id);
            Assert.AreEqual(_applicantResume.Applicant, applicantResumePoco.Applicant);
            Assert.AreEqual(_applicantResume.Resume, applicantResumePoco.Resume);
            Assert.AreEqual(_applicantResume.LastUpdated.Value.Date, applicantResumePoco.LastUpdated.Value.Date);
        }

        private void ApplicantJobApplicationCheck()
        {
            EFGenericRepository<ApplicantJobApplicationPoco> applicantJobApplicationRepository = new EFGenericRepository<ApplicantJobApplicationPoco>();
            ApplicantJobApplicationPoco applicantJobApplicationPoco = applicantJobApplicationRepository.GetSingle(t => t.Id == _applicantJobApplication.Id);
            Assert.IsNotNull(applicantJobApplicationPoco);
            Assert.AreEqual(_applicantJobApplication.Id, applicantJobApplicationPoco.Id);
            Assert.AreEqual(_applicantJobApplication.Applicant, applicantJobApplicationPoco.Applicant);
            Assert.AreEqual(_applicantJobApplication.Job, applicantJobApplicationPoco.Job);
            Assert.AreEqual(_applicantJobApplication.ApplicationDate.Date, applicantJobApplicationPoco.ApplicationDate.Date);
        }


        private void ApplicantEducationCheck()
        {
            EFGenericRepository<ApplicantEducationPoco> applicantEducationRepository = new EFGenericRepository<ApplicantEducationPoco>();
            ApplicantEducationPoco applicantEducationPoco = applicantEducationRepository.GetSingle(t => t.Id == _applicantEducation.Id);
            Assert.IsNotNull(applicantEducationPoco);
            Assert.AreEqual(_applicantEducation.Id, applicantEducationPoco.Id);
            Assert.AreEqual(_applicantEducation.Applicant, applicantEducationPoco.Applicant);
            Assert.AreEqual(_applicantEducation.Major, applicantEducationPoco.Major);
            Assert.AreEqual(_applicantEducation.CertificateDiploma, applicantEducationPoco.CertificateDiploma);
            Assert.AreEqual(_applicantEducation.StartDate.Value.Date, applicantEducationPoco.StartDate.Value.Date);
            Assert.AreEqual(_applicantEducation.CompletionDate.Value.Date, applicantEducationPoco.CompletionDate.Value.Date);
            Assert.AreEqual(_applicantEducation.CompletionPercent, applicantEducationPoco.CompletionPercent);
        }

        private void ApplicantProfileCheck()
        {
            EFGenericRepository<ApplicantProfilePoco> applicantProfileRepository = new EFGenericRepository<ApplicantProfilePoco>();
            ApplicantProfilePoco applicantProfilePoco = applicantProfileRepository.GetSingle(t => t.Id == _applicantProfile.Id);
            Assert.IsNotNull(applicantProfilePoco);
            Assert.AreEqual(_applicantProfile.Id, applicantProfilePoco.Id);
            Assert.AreEqual(_applicantProfile.Login, applicantProfilePoco.Login);
            Assert.AreEqual(_applicantProfile.CurrentSalary, applicantProfilePoco.CurrentSalary);
            Assert.AreEqual(_applicantProfile.CurrentRate, applicantProfilePoco.CurrentRate);
            Assert.AreEqual(_applicantProfile.Currency, applicantProfilePoco.Currency);
            Assert.AreEqual(_applicantProfile.Country, applicantProfilePoco.Country);
            Assert.AreEqual(_applicantProfile.Province, applicantProfilePoco.Province);
            Assert.AreEqual(_applicantProfile.Street, applicantProfilePoco.Street);
            Assert.AreEqual(_applicantProfile.City, applicantProfilePoco.City);
            Assert.AreEqual(_applicantProfile.PostalCode, applicantProfilePoco.PostalCode);
        }

        private void SecurityLoginRoleCheck()
        {
            EFGenericRepository<SecurityLoginsRolePoco> securityLoginRoleRepository = new EFGenericRepository<SecurityLoginsRolePoco>();
            SecurityLoginsRolePoco securityLoginsRolePoco = securityLoginRoleRepository.GetSingle(t => t.Id == _securityLoginRole.Id);
            Assert.IsNotNull(securityLoginsRolePoco);
            Assert.AreEqual(_securityLoginRole.Id, securityLoginsRolePoco.Id);
            Assert.AreEqual(_securityLoginRole.Login, securityLoginsRolePoco.Login);
            Assert.AreEqual(_securityLoginRole.Role, securityLoginsRolePoco.Role);
        }

        private void SecurityRoleCheck()
        {
            EFGenericRepository<SecurityRolePoco> securityRoleRepository = new EFGenericRepository<SecurityRolePoco>();
            SecurityRolePoco securityRolePoco = securityRoleRepository.GetSingle(t => t.Id == _securityRole.Id);
            Assert.IsNotNull(securityRolePoco);
            Assert.AreEqual(_securityRole.Id, securityRolePoco.Id);
            Assert.AreEqual(_securityRole.Role, securityRolePoco.Role);
            Assert.AreEqual(_securityRole.IsInactive, securityRolePoco.IsInactive);
        }

        private void SecurityLoginLogCheck()
        {
            EFGenericRepository<SecurityLoginsLogPoco> securityLoginLogRepository = new EFGenericRepository<SecurityLoginsLogPoco>();
            SecurityLoginsLogPoco securityLoginsLogPoco = securityLoginLogRepository.GetSingle(t => t.Id == _securityLoginLog.Id);
            Assert.IsNotNull(securityLoginsLogPoco);
            Assert.AreEqual(_securityLoginLog.Id, securityLoginsLogPoco.Id);
            Assert.AreEqual(_securityLoginLog.Login, securityLoginsLogPoco.Login);
            Assert.AreEqual(_securityLoginLog.SourceIP, securityLoginsLogPoco.SourceIP);
            Assert.AreEqual(_securityLoginLog.LogonDate.Date, securityLoginsLogPoco.LogonDate.Date);
        }

        private void SecurityLoginCheck()
        {
            EFGenericRepository<SecurityLoginPoco> securityLoginRepository = new EFGenericRepository<SecurityLoginPoco>();
            SecurityLoginPoco securityLoginPoco = securityLoginRepository.GetSingle(t => t.Id == _securityLogin.Id);
            Assert.IsNotNull(securityLoginPoco);
            Assert.AreEqual(_securityLogin.Id, securityLoginPoco.Id);
            Assert.AreEqual(_securityLogin.Login, securityLoginPoco.Login);
            Assert.AreEqual(_securityLogin.Password, securityLoginPoco.Password);
            Assert.AreEqual(_securityLogin.Created.Date, securityLoginPoco.Created.Date);
            Assert.AreEqual(_securityLogin.PasswordUpdate, securityLoginPoco.PasswordUpdate);
            Assert.AreEqual(_securityLogin.AgreementAccepted.Value.Date, securityLoginPoco.AgreementAccepted.Value.Date);
            Assert.AreEqual(_securityLogin.IsLocked, securityLoginPoco.IsLocked);
            Assert.AreEqual(_securityLogin.IsInactive, securityLoginPoco.IsInactive);
            Assert.AreEqual(_securityLogin.EmailAddress, securityLoginPoco.EmailAddress);
            Assert.AreEqual(_securityLogin.PhoneNumber, securityLoginPoco.PhoneNumber);
            Assert.AreEqual(_securityLogin.FullName, securityLoginPoco.FullName);
            Assert.AreEqual(_securityLogin.ForceChangePassword, securityLoginPoco.ForceChangePassword);
            Assert.AreEqual(_securityLogin.PrefferredLanguage, securityLoginPoco.PrefferredLanguage);
        }

        private void CompanyJobSkillCheck()
        {
            EFGenericRepository<CompanyJobSkillPoco> companyJobSkillRepository = new EFGenericRepository<CompanyJobSkillPoco>();
            CompanyJobSkillPoco companyJobSkillPoco = companyJobSkillRepository.GetSingle(t => t.Id == _companyJobSkill.Id);
            Assert.IsNotNull(companyJobSkillPoco);
            Assert.AreEqual(_companyJobSkill.Id, companyJobSkillPoco.Id);
            Assert.AreEqual(_companyJobSkill.Job, companyJobSkillPoco.Job);
            Assert.AreEqual(_companyJobSkill.Skill, companyJobSkillPoco.Skill);
            Assert.AreEqual(_companyJobSkill.SkillLevel, companyJobSkillPoco.SkillLevel);
            Assert.AreEqual(_companyJobSkill.Importance, companyJobSkillPoco.Importance);
        }


        private void CompanyJobEducationCheck()
        {
            EFGenericRepository<CompanyJobEducationPoco> companyJobEducationRepo = new EFGenericRepository<CompanyJobEducationPoco>();
            CompanyJobEducationPoco companyJobEducationPoco = companyJobEducationRepo.GetSingle(t => t.Id == _companyJobEducation.Id);
            Assert.IsNotNull(companyJobEducationPoco);
            Assert.AreEqual(_companyJobEducation.Id, companyJobEducationPoco.Id);
            Assert.AreEqual(_companyJobEducation.Job, companyJobEducationPoco.Job);
            Assert.AreEqual(_companyJobEducation.Major, companyJobEducationPoco.Major);
            Assert.AreEqual(_companyJobEducation.Importance, companyJobEducationPoco.Importance);
        }

        private void CompanyLocationCheck()
        {
            EFGenericRepository<CompanyLocationPoco> companyLocationRepo = new EFGenericRepository<CompanyLocationPoco>();
            CompanyLocationPoco companyLocationPoco = companyLocationRepo.GetSingle(t => t.Id == _companyLocation.Id);
            Assert.IsNotNull(companyLocationPoco);
            Assert.AreEqual(_companyLocation.Id, companyLocationPoco.Id);
            Assert.AreEqual(_companyLocation.Company, companyLocationPoco.Company);
            Assert.AreEqual(_companyLocation.CountryCode.PadRight(10), companyLocationPoco.CountryCode);
            Assert.AreEqual(_companyLocation.Province.PadRight(10), companyLocationPoco.Province);
            Assert.AreEqual(_companyLocation.Street, companyLocationPoco.Street);
            Assert.AreEqual(_companyLocation.City, companyLocationPoco.City);
            Assert.AreEqual(_companyLocation.PostalCode.PadRight(20), companyLocationPoco.PostalCode);
        }

        private void CompanyJobDescriptionCheck()
        {
            EFGenericRepository<CompanyJobDescriptionPoco> companyJobDescRepo = new EFGenericRepository<CompanyJobDescriptionPoco>();
            CompanyJobDescriptionPoco companyJobDescPoco = companyJobDescRepo.GetSingle(t => t.Id == _companyJobDescription.Id);
            Assert.IsNotNull(companyJobDescPoco);
            Assert.AreEqual(_companyJobDescription.Id, companyJobDescPoco.Id);
            Assert.AreEqual(_companyJobDescription.Job, companyJobDescPoco.Job);
            Assert.AreEqual(_companyJobDescription.JobDescriptions, companyJobDescPoco.JobDescriptions);
            Assert.AreEqual(_companyJobDescription.JobName, companyJobDescPoco.JobName);
        }

        private void CompanyJobCheck()
        {
            EFGenericRepository<CompanyJobPoco> companyJobRepo = new EFGenericRepository<CompanyJobPoco>();
            CompanyJobPoco companyJobPoco = companyJobRepo.GetSingle(t => t.Id == _companyJob.Id);
            Assert.IsNotNull(companyJobPoco);
            Assert.AreEqual(_companyJob.Id, companyJobPoco.Id);
            Assert.AreEqual(_companyJob.Company, companyJobPoco.Company);
            Assert.AreEqual(_companyJob.ProfileCreated, companyJobPoco.ProfileCreated);
            Assert.AreEqual(_companyJob.IsInactive, companyJobPoco.IsInactive);
            Assert.AreEqual(_companyJob.IsCompanyHidden, companyJobPoco.IsCompanyHidden);
        }

        private void CompanyDescriptionCheck()
        {
            EFGenericRepository<CompanyDescriptionPoco> companyDescriptionRepo = new EFGenericRepository<CompanyDescriptionPoco>();
            CompanyDescriptionPoco companyDescriptionPoco = companyDescriptionRepo.GetSingle(t => t.Id == _companyDescription.Id);
            Assert.IsNotNull(companyDescriptionPoco);
            Assert.AreEqual(_companyDescription.Id, companyDescriptionPoco.Id);
            Assert.AreEqual(_companyDescription.CompanyDescription, companyDescriptionPoco.CompanyDescription);
            Assert.AreEqual(_companyDescription.CompanyName, companyDescriptionPoco.CompanyName);
            Assert.AreEqual(_companyDescription.LanguageId, companyDescriptionPoco.LanguageId);
            Assert.AreEqual(_companyDescription.Company, companyDescriptionPoco.Company);
        }


        public void CompanyProfileCheck()
        {
            EFGenericRepository<CompanyProfilePoco> companyProfileRepo = new EFGenericRepository<CompanyProfilePoco>();
            CompanyProfilePoco companyProfilePoco = companyProfileRepo.GetSingle(t => t.Id == _companyProfile.Id);
            Assert.IsNotNull(companyProfilePoco);
            Assert.AreEqual(_companyProfile.CompanyWebsite, companyProfilePoco.CompanyWebsite);
            Assert.AreEqual(_companyProfile.ContactName, companyProfilePoco.ContactName);
            Assert.AreEqual(_companyProfile.ContactPhone, companyProfilePoco.ContactPhone);
            Assert.AreEqual(_companyProfile.RegistrationDate, companyProfilePoco.RegistrationDate);
            Assert.AreEqual(_companyProfile.Id, companyProfilePoco.Id);
        }

        private void ApplicantWorkHistoryCheck()
        {
            EFGenericRepository<ApplicantWorkHistoryPoco> applicantWorkHistoryRepository = new EFGenericRepository<ApplicantWorkHistoryPoco>();
            ApplicantWorkHistoryPoco applicantWorkHistoryPoco = applicantWorkHistoryRepository.GetSingle(t => t.Id == _appliantWorkHistory.Id);
            Assert.IsNotNull(applicantWorkHistoryPoco);
            Assert.AreEqual(_appliantWorkHistory.Id, applicantWorkHistoryPoco.Id);
            Assert.AreEqual(_appliantWorkHistory.Applicant, applicantWorkHistoryPoco.Applicant);
            Assert.AreEqual(_appliantWorkHistory.CompanyName, applicantWorkHistoryPoco.CompanyName);
            Assert.AreEqual(_appliantWorkHistory.CountryCode, applicantWorkHistoryPoco.CountryCode);
            Assert.AreEqual(_appliantWorkHistory.Location, applicantWorkHistoryPoco.Location);
            Assert.AreEqual(_appliantWorkHistory.JobTitle, applicantWorkHistoryPoco.JobTitle);
            Assert.AreEqual(_appliantWorkHistory.JobDescription, applicantWorkHistoryPoco.JobDescription);
            Assert.AreEqual(_appliantWorkHistory.StartMonth, applicantWorkHistoryPoco.StartMonth);
            Assert.AreEqual(_appliantWorkHistory.StartYear, applicantWorkHistoryPoco.StartYear);
            Assert.AreEqual(_appliantWorkHistory.EndMonth, applicantWorkHistoryPoco.EndMonth);
            Assert.AreEqual(_appliantWorkHistory.EndYear, applicantWorkHistoryPoco.EndYear);
        }

        public void SystemCountryCodeCheck()
        {
            EFGenericRepository<SystemCountryCodePoco> systemCountryCodeRepository = new EFGenericRepository<SystemCountryCodePoco>();
            SystemCountryCodePoco systemCountryCodePoco = systemCountryCodeRepository.GetSingle(t => t.Code == _systemCountry.Code);
            Assert.IsNotNull(systemCountryCodePoco);
            Assert.AreEqual(_systemCountry.Code, systemCountryCodePoco.Code);
            Assert.AreEqual(_systemCountry.Name, systemCountryCodePoco.Name);
        }

        private void SystemLanguageCodeCheck()
        {
            EFGenericRepository<SystemLanguageCodePoco> systemLanguageCodeRepo = new EFGenericRepository<SystemLanguageCodePoco>();
            SystemLanguageCodePoco systemLanguageCodePoco = systemLanguageCodeRepo.GetSingle(t => t.LanguageID == _systemLangCode.LanguageID);
            Assert.IsNotNull(systemLanguageCodePoco);
            Assert.AreEqual(systemLanguageCodePoco.LanguageID, _systemLangCode.LanguageID);
            Assert.AreEqual(systemLanguageCodePoco.NativeName, _systemLangCode.NativeName);
            Assert.AreEqual(systemLanguageCodePoco.Name, _systemLangCode.Name);
        }


        #endregion CheckImplementation

        #region CheckForeignKeyImplementation
        private void ApplicantSkillForeignKeyCheck()
        {
            EFGenericRepository<ApplicantSkillPoco> applicantSkillRepository = new EFGenericRepository<ApplicantSkillPoco>();
            ApplicantSkillPoco applicantSkillPoco =
                applicantSkillRepository.GetSingle(t => t.Id == _applicantSkills.Id
                , new Expression<Func<ApplicantSkillPoco, object>>[]{
                    c => c.ApplicantProfile
                });
            Assert.IsNotNull(applicantSkillPoco);
            Assert.AreEqual(_applicantSkills.Id, applicantSkillPoco.Id);
            Assert.AreEqual(_applicantSkills.Applicant, applicantSkillPoco.Applicant);
            Assert.AreEqual(_applicantSkills.Skill, applicantSkillPoco.Skill);
            Assert.AreEqual(_applicantSkills.SkillLevel, applicantSkillPoco.SkillLevel);
            Assert.AreEqual(_applicantSkills.StartMonth, applicantSkillPoco.StartMonth);
            Assert.AreEqual(_applicantSkills.StartYear, applicantSkillPoco.StartYear);
            Assert.AreEqual(_applicantSkills.EndMonth, applicantSkillPoco.EndMonth);
            Assert.AreEqual(_applicantSkills.EndYear, applicantSkillPoco.EndYear);
            Assert.IsNotNull(applicantSkillPoco.ApplicantProfile);
            Assert.AreEqual(_applicantProfile.Id, applicantSkillPoco.ApplicantProfile.Id);
        }
        private void ApplicantResumeForeignKeyCheck()
        {
            EFGenericRepository<ApplicantResumePoco> applicantResumeRepository = new EFGenericRepository<ApplicantResumePoco>();
            ApplicantResumePoco applicantResumePoco =
                applicantResumeRepository.GetSingle(t => t.Id == _applicantResume.Id
                , new Expression<Func<ApplicantResumePoco, object>>[]{
                    c => c.ApplicantProfile
                });
            Assert.IsNotNull(applicantResumePoco);
            Assert.AreEqual(_applicantResume.Id, applicantResumePoco.Id);
            Assert.AreEqual(_applicantResume.Applicant, applicantResumePoco.Applicant);
            Assert.AreEqual(_applicantResume.Resume, applicantResumePoco.Resume);
            Assert.AreEqual(_applicantResume.LastUpdated.Value.Date, applicantResumePoco.LastUpdated.Value.Date);

            Assert.IsNotNull(applicantResumePoco.ApplicantProfile);
            Assert.AreEqual(_applicantProfile.Id, applicantResumePoco.ApplicantProfile.Id);
        }
        private void ApplicantJobApplicationForeignKeyCheck()
        {
            EFGenericRepository<ApplicantJobApplicationPoco> applicantJobApplicationRepository = new EFGenericRepository<ApplicantJobApplicationPoco>();
            ApplicantJobApplicationPoco applicantJobApplicationPoco =
                applicantJobApplicationRepository.GetSingle(t => t.Id == _applicantJobApplication.Id
                , new Expression<Func<ApplicantJobApplicationPoco, object>>[]{
                    c => c.ApplicantProfile, c => c.CompanyJob
                });
            Assert.IsNotNull(applicantJobApplicationPoco);
            Assert.AreEqual(_applicantJobApplication.Id, applicantJobApplicationPoco.Id);
            Assert.AreEqual(_applicantJobApplication.Applicant, applicantJobApplicationPoco.Applicant);
            Assert.AreEqual(_applicantJobApplication.Job, applicantJobApplicationPoco.Job);
            Assert.AreEqual(_applicantJobApplication.ApplicationDate.Date, applicantJobApplicationPoco.ApplicationDate.Date);

            Assert.IsNotNull(applicantJobApplicationPoco.ApplicantProfile);
            Assert.AreEqual(_applicantProfile.Id, applicantJobApplicationPoco.ApplicantProfile.Id);
            Assert.IsNotNull(applicantJobApplicationPoco.CompanyJob);
            Assert.AreEqual(_companyJob.Id, applicantJobApplicationPoco.CompanyJob.Id);

        }
        private void ApplicantEducationForeignKeyCheck()
        {
            EFGenericRepository<ApplicantEducationPoco> applicantEducationRepository = new EFGenericRepository<ApplicantEducationPoco>();
            ApplicantEducationPoco applicantEducationPoco =
                applicantEducationRepository.GetSingle(t => t.Id == _applicantEducation.Id,
                new Expression<Func<ApplicantEducationPoco, object>>[]{
                    c => c.ApplicantProfile
                });
            Assert.IsNotNull(applicantEducationPoco);
            Assert.AreEqual(_applicantEducation.Id, applicantEducationPoco.Id);
            Assert.AreEqual(_applicantEducation.Applicant, applicantEducationPoco.Applicant);
            Assert.AreEqual(_applicantEducation.Major, applicantEducationPoco.Major);
            Assert.AreEqual(_applicantEducation.CertificateDiploma, applicantEducationPoco.CertificateDiploma);
            Assert.AreEqual(_applicantEducation.StartDate.Value.Date, applicantEducationPoco.StartDate.Value.Date);
            Assert.AreEqual(_applicantEducation.CompletionDate.Value.Date, applicantEducationPoco.CompletionDate.Value.Date);
            Assert.AreEqual(_applicantEducation.CompletionPercent, applicantEducationPoco.CompletionPercent);

            Assert.IsNotNull(applicantEducationPoco.ApplicantProfile);
            Assert.AreEqual(_applicantProfile.Id, applicantEducationPoco.ApplicantProfile.Id);
        }
        private void ApplicantProfileForeignKeyCheck()
        {
            EFGenericRepository<ApplicantProfilePoco> applicantProfileRepository = new EFGenericRepository<ApplicantProfilePoco>();
            ApplicantProfilePoco applicantProfilePoco =
                applicantProfileRepository.GetSingle(t => t.Id == _applicantProfile.Id,
                new Expression<Func<ApplicantProfilePoco, object>>[]{
                    c => c.ApplicantEducations, c => c.ApplicantJobApplications
                    , c => c.ApplicantResumes, c => c.ApplicantSkills
                    , c => c.ApplicantWorkHistorys, c => c.SystemCountryCode
                });
            Assert.IsNotNull(applicantProfilePoco);
            Assert.AreEqual(_applicantProfile.Id, applicantProfilePoco.Id);
            Assert.AreEqual(_applicantProfile.Login, applicantProfilePoco.Login);
            Assert.AreEqual(_applicantProfile.CurrentSalary, applicantProfilePoco.CurrentSalary);
            Assert.AreEqual(_applicantProfile.CurrentRate, applicantProfilePoco.CurrentRate);
            Assert.AreEqual(_applicantProfile.Currency, applicantProfilePoco.Currency);
            Assert.AreEqual(_applicantProfile.Country, applicantProfilePoco.Country);
            Assert.AreEqual(_applicantProfile.Province, applicantProfilePoco.Province);
            Assert.AreEqual(_applicantProfile.Street, applicantProfilePoco.Street);
            Assert.AreEqual(_applicantProfile.City, applicantProfilePoco.City);
            Assert.AreEqual(_applicantProfile.PostalCode, applicantProfilePoco.PostalCode);

            Assert.IsNotNull(applicantProfilePoco.SystemCountryCode);
            Assert.AreEqual(_systemCountry.Code, applicantProfilePoco.SystemCountryCode.Code);

            Assert.IsNotNull(applicantProfilePoco.ApplicantEducations);
            Assert.IsTrue(applicantProfilePoco.ApplicantEducations.Count > 0);
            Assert.AreEqual(_applicantEducation.Id, applicantProfilePoco.ApplicantEducations.FirstOrDefault().Id);

            Assert.IsNotNull(applicantProfilePoco.ApplicantJobApplications);
            Assert.IsTrue(applicantProfilePoco.ApplicantJobApplications.Count > 0);
            Assert.AreEqual(_applicantJobApplication.Id, applicantProfilePoco.ApplicantJobApplications.FirstOrDefault().Id);

            Assert.IsNotNull(applicantProfilePoco.ApplicantResumes);
            Assert.IsTrue(applicantProfilePoco.ApplicantResumes.Count > 0);
            Assert.AreEqual(_applicantResume.Id, applicantProfilePoco.ApplicantResumes.FirstOrDefault().Id);

            Assert.IsNotNull(applicantProfilePoco.ApplicantSkills);
            Assert.IsTrue(applicantProfilePoco.ApplicantSkills.Count > 0);
            Assert.AreEqual(_applicantSkills.Id, applicantProfilePoco.ApplicantSkills.FirstOrDefault().Id);

            Assert.IsNotNull(applicantProfilePoco.ApplicantWorkHistorys);
            Assert.IsTrue(applicantProfilePoco.ApplicantWorkHistorys.Count > 0);
            Assert.AreEqual(_appliantWorkHistory.Id, applicantProfilePoco.ApplicantWorkHistorys.FirstOrDefault().Id);
        }
        private void SecurityLoginRoleForeignKeyCheck()
        {
            EFGenericRepository<SecurityLoginsRolePoco> securityLoginRoleRepository = new EFGenericRepository<SecurityLoginsRolePoco>();
            SecurityLoginsRolePoco securityLoginsRolePoco =
                securityLoginRoleRepository.GetSingle(t => t.Id == _securityLoginRole.Id,
                new Expression<Func<SecurityLoginsRolePoco, object>>[]{
                    c => c.SecurityLogin, c => c.SecurityRole
                });
            Assert.IsNotNull(securityLoginsRolePoco);
            Assert.AreEqual(_securityLoginRole.Id, securityLoginsRolePoco.Id);
            Assert.AreEqual(_securityLoginRole.Login, securityLoginsRolePoco.Login);
            Assert.AreEqual(_securityLoginRole.Role, securityLoginsRolePoco.Role);

            Assert.IsNotNull(securityLoginsRolePoco.SecurityLogin);
            Assert.AreEqual(_securityLogin.Id, securityLoginsRolePoco.SecurityLogin.Id);

            Assert.IsNotNull(securityLoginsRolePoco.SecurityRole);
            Assert.AreEqual(_securityRole.Id, securityLoginsRolePoco.SecurityRole.Id);

        }
        private void SecurityRoleForeignKeyCheck()
        {
            EFGenericRepository<SecurityRolePoco> securityRoleRepository = new EFGenericRepository<SecurityRolePoco>();
            SecurityRolePoco securityRolePoco = securityRoleRepository.GetSingle(t => t.Id == _securityRole.Id,
                new Expression<Func<SecurityRolePoco, object>>[]{
                    c => c.SecurityLoginsRoles
                });
            Assert.IsNotNull(securityRolePoco);
            Assert.AreEqual(_securityRole.Id, securityRolePoco.Id);
            Assert.AreEqual(_securityRole.Role, securityRolePoco.Role);
            Assert.AreEqual(_securityRole.IsInactive, securityRolePoco.IsInactive);

            Assert.IsNotNull(securityRolePoco.SecurityLoginsRoles);
            Assert.IsTrue(securityRolePoco.SecurityLoginsRoles.Count > 0);
            Assert.AreEqual(_securityLoginRole.Id, securityRolePoco.SecurityLoginsRoles.FirstOrDefault().Id);
        }
        private void SecurityLoginLogForeignKeyCheck()
        {
            EFGenericRepository<SecurityLoginsLogPoco> securityLoginLogRepository = new EFGenericRepository<SecurityLoginsLogPoco>();
            SecurityLoginsLogPoco securityLoginsLogPoco
                = securityLoginLogRepository.GetSingle(t => t.Id == _securityLoginLog.Id,
                new Expression<Func<SecurityLoginsLogPoco, object>>[]{
                    c => c.SecurityLogin
                });
            Assert.IsNotNull(securityLoginsLogPoco);
            Assert.AreEqual(_securityLoginLog.Id, securityLoginsLogPoco.Id);
            Assert.AreEqual(_securityLoginLog.Login, securityLoginsLogPoco.Login);
            Assert.AreEqual(_securityLoginLog.SourceIP, securityLoginsLogPoco.SourceIP);
            Assert.AreEqual(_securityLoginLog.LogonDate.Date, securityLoginsLogPoco.LogonDate.Date);

            Assert.IsNotNull(securityLoginsLogPoco.SecurityLogin);
            Assert.AreEqual(_securityLogin.Id, securityLoginsLogPoco.SecurityLogin.Id);
        }
        private void SecurityLoginForeignKeyCheck()
        {
            EFGenericRepository<SecurityLoginPoco> securityLoginRepository = new EFGenericRepository<SecurityLoginPoco>();
            SecurityLoginPoco securityLoginPoco
                = securityLoginRepository.GetSingle(t => t.Id == _securityLogin.Id,
                new Expression<Func<SecurityLoginPoco, object>>[]{
                        c => c.ApplicantProfiles, c => c.SecurityLoginsLogs,
                        c => c.SecurityLoginsRoles
                });
            Assert.IsNotNull(securityLoginPoco);
            Assert.AreEqual(_securityLogin.Id, securityLoginPoco.Id);
            Assert.AreEqual(_securityLogin.Login, securityLoginPoco.Login);
            Assert.AreEqual(_securityLogin.Password, securityLoginPoco.Password);
            Assert.AreEqual(_securityLogin.Created.Date, securityLoginPoco.Created.Date);
            Assert.AreEqual(_securityLogin.PasswordUpdate, securityLoginPoco.PasswordUpdate);
            Assert.AreEqual(_securityLogin.AgreementAccepted.Value.Date, securityLoginPoco.AgreementAccepted.Value.Date);
            Assert.AreEqual(_securityLogin.IsLocked, securityLoginPoco.IsLocked);
            Assert.AreEqual(_securityLogin.IsInactive, securityLoginPoco.IsInactive);
            Assert.AreEqual(_securityLogin.EmailAddress, securityLoginPoco.EmailAddress);
            Assert.AreEqual(_securityLogin.PhoneNumber, securityLoginPoco.PhoneNumber);
            Assert.AreEqual(_securityLogin.FullName, securityLoginPoco.FullName);
            Assert.AreEqual(_securityLogin.ForceChangePassword, securityLoginPoco.ForceChangePassword);
            Assert.AreEqual(_securityLogin.PrefferredLanguage, securityLoginPoco.PrefferredLanguage);

            Assert.IsNotNull(securityLoginPoco.ApplicantProfiles);
            Assert.IsTrue(securityLoginPoco.ApplicantProfiles.Count > 0);
            Assert.AreEqual(_applicantProfile.Id, securityLoginPoco.ApplicantProfiles.FirstOrDefault().Id);

            Assert.IsNotNull(securityLoginPoco.SecurityLoginsLogs);
            Assert.IsTrue(securityLoginPoco.SecurityLoginsLogs.Count > 0);
            Assert.AreEqual(_securityLoginLog.Id, securityLoginPoco.SecurityLoginsLogs.FirstOrDefault().Id);

            Assert.IsNotNull(securityLoginPoco.SecurityLoginsRoles);
            Assert.IsTrue(securityLoginPoco.SecurityLoginsRoles.Count > 0);
            Assert.AreEqual(_securityLoginRole.Id, securityLoginPoco.SecurityLoginsRoles.FirstOrDefault().Id);
        }
        private void CompanyJobSkillForeignKeyCheck()
        {
            EFGenericRepository<CompanyJobSkillPoco> companyJobSkillRepository = new EFGenericRepository<CompanyJobSkillPoco>();
            CompanyJobSkillPoco companyJobSkillPoco
                = companyJobSkillRepository.GetSingle(t => t.Id == _companyJobSkill.Id,
                new Expression<Func<CompanyJobSkillPoco, object>>[]{
                        c => c.CompanyJob
                });
            Assert.IsNotNull(companyJobSkillPoco);
            Assert.AreEqual(_companyJobSkill.Id, companyJobSkillPoco.Id);
            Assert.AreEqual(_companyJobSkill.Job, companyJobSkillPoco.Job);
            Assert.AreEqual(_companyJobSkill.Skill, companyJobSkillPoco.Skill);
            Assert.AreEqual(_companyJobSkill.SkillLevel, companyJobSkillPoco.SkillLevel);
            Assert.AreEqual(_companyJobSkill.Importance, companyJobSkillPoco.Importance);

            Assert.IsNotNull(companyJobSkillPoco.CompanyJob);
            Assert.AreEqual(_companyJob.Id, companyJobSkillPoco.CompanyJob.Id);
        }
        private void CompanyJobEducationForeignKeyCheck()
        {
            EFGenericRepository<CompanyJobEducationPoco> companyJobEducationRepo = new EFGenericRepository<CompanyJobEducationPoco>();
            CompanyJobEducationPoco companyJobEducationPoco
                = companyJobEducationRepo.GetSingle(t => t.Id == _companyJobEducation.Id,
                new Expression<Func<CompanyJobEducationPoco, object>>[]{
                        c => c.CompanyJob
                });
            Assert.IsNotNull(companyJobEducationPoco);
            Assert.AreEqual(_companyJobEducation.Id, companyJobEducationPoco.Id);
            Assert.AreEqual(_companyJobEducation.Job, companyJobEducationPoco.Job);
            Assert.AreEqual(_companyJobEducation.Major, companyJobEducationPoco.Major);
            Assert.AreEqual(_companyJobEducation.Importance, companyJobEducationPoco.Importance);

            Assert.IsNotNull(companyJobEducationPoco.CompanyJob);
            Assert.AreEqual(_companyJob.Id, companyJobEducationPoco.CompanyJob.Id);
        }
        private void CompanyLocationForeignKeyCheck()
        {
            EFGenericRepository<CompanyLocationPoco> companyLocationRepo = new EFGenericRepository<CompanyLocationPoco>();
            CompanyLocationPoco companyLocationPoco
                = companyLocationRepo.GetSingle(t => t.Id == _companyLocation.Id,
                new Expression<Func<CompanyLocationPoco, object>>[]{
                        c => c.CompanyProfile, c => c.SystemCountryCode
                });
            Assert.IsNotNull(companyLocationPoco);
            Assert.AreEqual(_companyLocation.Id, companyLocationPoco.Id);
            Assert.AreEqual(_companyLocation.Company, companyLocationPoco.Company);
            Assert.AreEqual(_companyLocation.CountryCode.PadRight(10), companyLocationPoco.CountryCode);
            Assert.AreEqual(_companyLocation.Province.PadRight(10), companyLocationPoco.Province);
            Assert.AreEqual(_companyLocation.Street, companyLocationPoco.Street);
            Assert.AreEqual(_companyLocation.City, companyLocationPoco.City);
            Assert.AreEqual(_companyLocation.PostalCode.PadRight(20), companyLocationPoco.PostalCode);

            Assert.IsNotNull(companyLocationPoco.CompanyProfile);
            Assert.AreEqual(_companyProfile.Id, companyLocationPoco.CompanyProfile.Id);

            Assert.IsNotNull(companyLocationPoco.SystemCountryCode);
            Assert.AreEqual(_systemCountry.Code, companyLocationPoco.SystemCountryCode.Code);
        }
        private void CompanyJobDescriptionForeignKeyCheck()
        {
            EFGenericRepository<CompanyJobDescriptionPoco> companyJobDescRepo = new EFGenericRepository<CompanyJobDescriptionPoco>();
            CompanyJobDescriptionPoco companyJobDescPoco
                = companyJobDescRepo.GetSingle(t => t.Id == _companyJobDescription.Id,
                new Expression<Func<CompanyJobDescriptionPoco, object>>[]{
                        c => c.CompanyJob
                });
            Assert.IsNotNull(companyJobDescPoco);
            Assert.AreEqual(_companyJobDescription.Id, companyJobDescPoco.Id);
            Assert.AreEqual(_companyJobDescription.Job, companyJobDescPoco.Job);
            Assert.AreEqual(_companyJobDescription.JobDescriptions, companyJobDescPoco.JobDescriptions);
            Assert.AreEqual(_companyJobDescription.JobName, companyJobDescPoco.JobName);

            Assert.IsNotNull(companyJobDescPoco.CompanyJob);
            Assert.AreEqual(_companyJob.Id, companyJobDescPoco.CompanyJob.Id);
        }
        private void CompanyJobForeignKeyCheck()
        {
            EFGenericRepository<CompanyJobPoco> companyJobRepo = new EFGenericRepository<CompanyJobPoco>();
            CompanyJobPoco companyJobPoco
                = companyJobRepo.GetSingle(t => t.Id == _companyJob.Id,
                new Expression<Func<CompanyJobPoco, object>>[]{
                        c => c.CompanyProfile, c => c.CompanyJobDescriptions,
                        c => c.CompanyJobEducations, c => c.CompanyJobSkills
                });
            Assert.IsNotNull(companyJobPoco);
            Assert.AreEqual(_companyJob.Id, companyJobPoco.Id);
            Assert.AreEqual(_companyJob.Company, companyJobPoco.Company);
            Assert.AreEqual(_companyJob.ProfileCreated, companyJobPoco.ProfileCreated);
            Assert.AreEqual(_companyJob.IsInactive, companyJobPoco.IsInactive);
            Assert.AreEqual(_companyJob.IsCompanyHidden, companyJobPoco.IsCompanyHidden);

            Assert.IsNotNull(companyJobPoco.CompanyProfile);
            Assert.AreEqual(_companyProfile.Id, companyJobPoco.CompanyProfile.Id);

            Assert.IsNotNull(companyJobPoco.CompanyJobDescriptions);
            Assert.IsTrue(companyJobPoco.CompanyJobDescriptions.Count > 0);
            Assert.AreEqual(_companyJobDescription.Id, companyJobPoco.CompanyJobDescriptions.FirstOrDefault().Id);

            Assert.IsNotNull(companyJobPoco.CompanyJobEducations);
            Assert.IsTrue(companyJobPoco.CompanyJobEducations.Count > 0);
            Assert.AreEqual(_companyJobEducation.Id, companyJobPoco.CompanyJobEducations.FirstOrDefault().Id);

            Assert.IsNotNull(companyJobPoco.CompanyJobSkills);
            Assert.IsTrue(companyJobPoco.CompanyJobSkills.Count > 0);
            Assert.AreEqual(_companyJobSkill.Id, companyJobPoco.CompanyJobSkills.FirstOrDefault().Id);
        }
        private void CompanyDescriptionForeignKeyCheck()
        {
            EFGenericRepository<CompanyDescriptionPoco> companyDescriptionRepo = new EFGenericRepository<CompanyDescriptionPoco>();
            CompanyDescriptionPoco companyDescriptionPoco
                = companyDescriptionRepo.GetSingle(t => t.Id == _companyDescription.Id,
                new Expression<Func<CompanyDescriptionPoco, object>>[]{
                        c => c.CompanyProfile, c => c.SystemLanguageCode
                });
            Assert.IsNotNull(companyDescriptionPoco);
            Assert.AreEqual(_companyDescription.Id, companyDescriptionPoco.Id);
            Assert.AreEqual(_companyDescription.CompanyDescription, companyDescriptionPoco.CompanyDescription);
            Assert.AreEqual(_companyDescription.CompanyName, companyDescriptionPoco.CompanyName);
            Assert.AreEqual(_companyDescription.LanguageId, companyDescriptionPoco.LanguageId);
            Assert.AreEqual(_companyDescription.Company, companyDescriptionPoco.Company);

            Assert.IsNotNull(companyDescriptionPoco.CompanyProfile);
            Assert.AreEqual(_companyProfile.Id, companyDescriptionPoco.CompanyProfile.Id);

            Assert.IsNotNull(companyDescriptionPoco.SystemLanguageCode);
            Assert.AreEqual(_systemLangCode.LanguageID, companyDescriptionPoco.SystemLanguageCode.LanguageID);
        }
        public void CompanyProfileForeignKeyCheck()
        {
            EFGenericRepository<CompanyProfilePoco> companyProfileRepo = new EFGenericRepository<CompanyProfilePoco>();
            CompanyProfilePoco companyProfilePoco
                = companyProfileRepo.GetSingle(t => t.Id == _companyProfile.Id,
                new Expression<Func<CompanyProfilePoco, object>>[]{
                        c => c.CompanyDescriptions, c => c.CompanyJobs,
                        c => c.CompanyLocations
                });
            Assert.IsNotNull(companyProfilePoco);
            Assert.AreEqual(_companyProfile.CompanyWebsite, companyProfilePoco.CompanyWebsite);
            Assert.AreEqual(_companyProfile.ContactName, companyProfilePoco.ContactName);
            Assert.AreEqual(_companyProfile.ContactPhone, companyProfilePoco.ContactPhone);
            Assert.AreEqual(_companyProfile.RegistrationDate, companyProfilePoco.RegistrationDate);
            Assert.AreEqual(_companyProfile.Id, companyProfilePoco.Id);

            Assert.IsNotNull(companyProfilePoco.CompanyDescriptions);
            Assert.IsTrue(companyProfilePoco.CompanyDescriptions.Count > 0);
            Assert.AreEqual(_companyDescription.Id, companyProfilePoco.CompanyDescriptions.FirstOrDefault().Id);

            Assert.IsNotNull(companyProfilePoco.CompanyJobs);
            Assert.IsTrue(companyProfilePoco.CompanyJobs.Count > 0);
            Assert.AreEqual(_companyJob.Id, companyProfilePoco.CompanyJobs.FirstOrDefault().Id);

            Assert.IsNotNull(companyProfilePoco.CompanyLocations);
            Assert.IsTrue(companyProfilePoco.CompanyLocations.Count > 0);
            Assert.AreEqual(_companyLocation.Id, companyProfilePoco.CompanyLocations.FirstOrDefault().Id);
        }
        private void ApplicantWorkHistoryForeignKeyCheck()
        {
            EFGenericRepository<ApplicantWorkHistoryPoco> applicantWorkHistoryRepository = new EFGenericRepository<ApplicantWorkHistoryPoco>();
            ApplicantWorkHistoryPoco applicantWorkHistoryPoco
                = applicantWorkHistoryRepository.GetSingle(t => t.Id == _appliantWorkHistory.Id,
                new Expression<Func<ApplicantWorkHistoryPoco, object>>[]{
                        c => c.ApplicantProfile, c => c.SystemCountryCode
                });
            Assert.IsNotNull(applicantWorkHistoryPoco);
            Assert.AreEqual(_appliantWorkHistory.Id, applicantWorkHistoryPoco.Id);
            Assert.AreEqual(_appliantWorkHistory.Applicant, applicantWorkHistoryPoco.Applicant);
            Assert.AreEqual(_appliantWorkHistory.CompanyName, applicantWorkHistoryPoco.CompanyName);
            Assert.AreEqual(_appliantWorkHistory.CountryCode, applicantWorkHistoryPoco.CountryCode);
            Assert.AreEqual(_appliantWorkHistory.Location, applicantWorkHistoryPoco.Location);
            Assert.AreEqual(_appliantWorkHistory.JobTitle, applicantWorkHistoryPoco.JobTitle);
            Assert.AreEqual(_appliantWorkHistory.JobDescription, applicantWorkHistoryPoco.JobDescription);
            Assert.AreEqual(_appliantWorkHistory.StartMonth, applicantWorkHistoryPoco.StartMonth);
            Assert.AreEqual(_appliantWorkHistory.StartYear, applicantWorkHistoryPoco.StartYear);
            Assert.AreEqual(_appliantWorkHistory.EndMonth, applicantWorkHistoryPoco.EndMonth);
            Assert.AreEqual(_appliantWorkHistory.EndYear, applicantWorkHistoryPoco.EndYear);

            Assert.IsNotNull(applicantWorkHistoryPoco.ApplicantProfile);
            Assert.AreEqual(_applicantProfile.Id, applicantWorkHistoryPoco.ApplicantProfile.Id);

            Assert.IsNotNull(applicantWorkHistoryPoco.SystemCountryCode);
            Assert.AreEqual(_systemCountry.Code, applicantWorkHistoryPoco.SystemCountryCode.Code);
        }

        #endregion CheckForeignKeyImplementation

        #region UpdateImplementation
        public void CompanyProfileUpdate()
        {
            _companyProfile.CompanyWebsite = Faker.Internet.Host();
            _companyProfile.ContactName = Faker.Name.FullName();
            _companyProfile.ContactPhone = "999-555-8799";
            _companyProfile.RegistrationDate = Faker.Date.Past();
            EFGenericRepository<CompanyProfilePoco> companyProfileRepo = new EFGenericRepository<CompanyProfilePoco>();
            companyProfileRepo.Update(new CompanyProfilePoco[] { _companyProfile });
        }

        public void CompanyDescriptionUpdate()
        {
            _companyDescription.CompanyDescription = Faker.Company.CatchPhrase();
            _companyDescription.CompanyName = Faker.Company.CatchPhrasePos();
            EFGenericRepository<CompanyDescriptionPoco> repo = new EFGenericRepository<CompanyDescriptionPoco>();
            repo.Update(new CompanyDescriptionPoco[] { _companyDescription });
        }

        public void SecurityRoleUpdate()
        {
            _securityRole.IsInactive = true;
            _securityRole.Role = Truncate(Faker.Company.Industry(), 50);
            EFGenericRepository<SecurityRolePoco> repo = new EFGenericRepository<SecurityRolePoco>();
            repo.Update(new SecurityRolePoco[] { _securityRole });
        }

        public void CompanyJobUpdate()
        {
            _companyJob.IsCompanyHidden = true;
            _companyJob.IsInactive = true;
            _companyJob.ProfileCreated = Faker.Date.Past();
            EFGenericRepository<CompanyJobPoco> repo = new EFGenericRepository<CompanyJobPoco>();
            repo.Update(new CompanyJobPoco[] { _companyJob });

        }

        public void CompanyJobDescriptionUpdate()
        {
            _companyJobDescription.JobDescriptions = Truncate(Faker.Lorem.Paragraph(), 999);
            _companyJobDescription.JobName = Truncate(Faker.Lorem.Sentence(), 99);
            EFGenericRepository<CompanyJobDescriptionPoco> repo = new EFGenericRepository<CompanyJobDescriptionPoco>();
            repo.Update(new CompanyJobDescriptionPoco[] { _companyJobDescription });
        }

        public void CompanyLocationUpdate()
        {
            _companyLocation.City = Faker.Address.CityPrefix();
            _companyLocation.CountryCode = _systemCountry.Code;
            _companyLocation.Province = Faker.Address.ProvinceAbbreviation();
            _companyLocation.Street = Faker.Address.StreetName();
            _companyLocation.PostalCode = Faker.Address.CanadianZipCode();
            EFGenericRepository<CompanyLocationPoco> repo = new EFGenericRepository<CompanyLocationPoco>();
            repo.Update(new CompanyLocationPoco[] { _companyLocation });
        }

        public void CompanyJobEducationUpdate()
        {
            _companyJobEducation.Importance = 1;
            _companyJobEducation.Major = Truncate(Faker.Lorem.Sentence(), 100);
            EFGenericRepository<CompanyJobEducationPoco> repo = new EFGenericRepository<CompanyJobEducationPoco>();
            repo.Update(new CompanyJobEducationPoco[] { _companyJobEducation });
        }

        public void CompanyJobSkillUpdate()
        {
            _companyJobSkill.Importance = 1;
            _companyJobSkill.Skill = Truncate(Faker.Lorem.Sentence(), 100);
            _companyJobSkill.SkillLevel = String.Concat(Faker.Lorem.Letters(10));
            EFGenericRepository<CompanyJobSkillPoco> repo = new EFGenericRepository<CompanyJobSkillPoco>();
            repo.Update(new CompanyJobSkillPoco[] { _companyJobSkill });
        }

        public void SecurityLoginUpdate()
        {
            _securityLogin.Login = Faker.User.Email();
            _securityLogin.AgreementAccepted = Faker.Date.PastWithTime();
            _securityLogin.Created = Faker.Date.PastWithTime();
            _securityLogin.EmailAddress = Faker.User.Email();
            _securityLogin.ForceChangePassword = true;
            _securityLogin.FullName = Faker.Name.FullName();
            _securityLogin.IsInactive = true;
            _securityLogin.IsLocked = true;
            _securityLogin.Password = Faker.User.Password();
            _securityLogin.PasswordUpdate = Faker.Date.Forward();
            _securityLogin.PhoneNumber = "416-416-9889";
            _securityLogin.PrefferredLanguage = "FR".PadRight(10);
            EFGenericRepository<SecurityLoginPoco> repo = new EFGenericRepository<SecurityLoginPoco>();
            repo.Update(new SecurityLoginPoco[] { _securityLogin });
        }

        public void SecurityLoginLogUpdate()
        {
            _securityLoginLog.IsSuccesful = false;
            _securityLoginLog.LogonDate = Faker.Date.PastWithTime();
            _securityLoginLog.SourceIP = Faker.Internet.IPv4().PadRight(15);
            EFGenericRepository<SecurityLoginsLogPoco> repo = new EFGenericRepository<SecurityLoginsLogPoco>();
            repo.Update(new SecurityLoginsLogPoco[] { _securityLoginLog });
        }

        public void ApplicantProfileUpdate()
        {
            _applicantProfile.City = Faker.Address.CityPrefix();
            _applicantProfile.Currency = "US".PadRight(10);
            _applicantProfile.CurrentRate = 61.25M;
            _applicantProfile.CurrentSalary = 77500;
            _applicantProfile.Province = Truncate(Faker.Address.Province(), 10).PadRight(10);
            _applicantProfile.Street = Truncate(Faker.Address.StreetName(), 100);
            _applicantProfile.PostalCode = Truncate(Faker.Address.CanadianZipCode(), 20).PadRight(20);
            EFGenericRepository<ApplicantProfilePoco> repo = new EFGenericRepository<ApplicantProfilePoco>();
            repo.Update(new ApplicantProfilePoco[] { _applicantProfile });
        }

        public void ApplicantEducationUpdate()
        {
            _applicantEducation.Major = Faker.Education.Major();
            _applicantEducation.CertificateDiploma = Faker.Education.Major();
            _applicantEducation.StartDate = Faker.Date.Past(3);
            _applicantEducation.CompletionDate = Faker.Date.Past(1);
            _applicantEducation.CompletionPercent = (byte)Faker.Number.RandomNumber(1);
            EFGenericRepository<ApplicantEducationPoco> repo = new EFGenericRepository<ApplicantEducationPoco>();
            repo.Update(new ApplicantEducationPoco[] { _applicantEducation });

        }
        public void ApplicantJobApplicationUpdate()
        {
            _applicantJobApplication.ApplicationDate = Faker.Date.Recent();
            EFGenericRepository<ApplicantJobApplicationPoco> repo = new EFGenericRepository<ApplicantJobApplicationPoco>();
            repo.Update(new ApplicantJobApplicationPoco[] { _applicantJobApplication });
        }

        public void ApplicantResumeUpdate()
        {
            _applicantResume.Resume = Faker.Lorem.Paragraph();
            _applicantResume.LastUpdated = Faker.Date.Recent();
            EFGenericRepository<ApplicantResumePoco> repo = new EFGenericRepository<ApplicantResumePoco>();
            repo.Update(new ApplicantResumePoco[] { _applicantResume });
        }

        private void ApplicantSkillUpdate()
        {
            _applicantSkills.EndMonth = 12;
            _applicantSkills.EndYear = 1999;
            _applicantSkills.Skill = Truncate(Faker.Lorem.Sentence(), 100);
            _applicantSkills.SkillLevel = Truncate(Faker.Lorem.Sentence(), 10);
            _applicantSkills.StartMonth = 1;
            _applicantSkills.StartYear = 1999;
            EFGenericRepository<ApplicantSkillPoco> applicantSkillRepository = new EFGenericRepository<ApplicantSkillPoco>();
            applicantSkillRepository.Update(new ApplicantSkillPoco[] { _applicantSkills });
        }

        private void ApplicantWorkHistoryUpdate()
        {
            _appliantWorkHistory.CompanyName = Truncate(Faker.Lorem.Sentence(), 150);
            _appliantWorkHistory.EndMonth = 01;
            _appliantWorkHistory.EndYear = 2001;
            _appliantWorkHistory.JobDescription = Truncate(Faker.Lorem.Sentence(), 500);
            _appliantWorkHistory.JobTitle = Truncate(Faker.Lorem.Sentence(), 50);
            _appliantWorkHistory.Location = Faker.Address.StreetName();
            _appliantWorkHistory.StartMonth = 2;
            _appliantWorkHistory.StartYear = 1989;

            EFGenericRepository<ApplicantWorkHistoryPoco> repo = new EFGenericRepository<ApplicantWorkHistoryPoco>();
            repo.Update(new ApplicantWorkHistoryPoco[] { _appliantWorkHistory });
        }

        private void SystemCountryCodeUpdate()
        {
            _systemCountry.Name = Truncate(Faker.Name.FullName(), 50);
            EFGenericRepository<SystemCountryCodePoco> systemCountryCodeRepository = new EFGenericRepository<SystemCountryCodePoco>();
            systemCountryCodeRepository.Update(new SystemCountryCodePoco[] { _systemCountry });
        }

        private void SystemLanguageCodeUpdate()
        {
            _systemLangCode.NativeName = Truncate(Faker.Lorem.Sentence(), 50);
            _systemLangCode.Name = Truncate(Faker.Lorem.Sentence(), 50);
            EFGenericRepository<SystemLanguageCodePoco> systemLanguageCodeRepository = new EFGenericRepository<SystemLanguageCodePoco>();
            systemLanguageCodeRepository.Update(new SystemLanguageCodePoco[] { _systemLangCode });

        }
        #endregion UpdateImplementation

        #region RemoveImplementation
        private void SystemLanguageCodeRemove()
        {
            EFGenericRepository<SystemLanguageCodePoco> systemLanguageCodeRepo = new EFGenericRepository<SystemLanguageCodePoco>();
            systemLanguageCodeRepo.Remove(new SystemLanguageCodePoco[] { _systemLangCode });
            Assert.IsNull(systemLanguageCodeRepo.GetSingle(t => t.LanguageID == _systemLangCode.LanguageID));
        }

        private void CompanyProfileRemove()
        {
            EFGenericRepository<CompanyProfilePoco> companyProfileRepo = new EFGenericRepository<CompanyProfilePoco>();
            companyProfileRepo.Remove(new CompanyProfilePoco[] { _companyProfile });
            Assert.IsNull(companyProfileRepo.GetSingle(t => t.Id == _companyProfile.Id));
        }

        private void CompanyDescriptionRemove()
        {
            EFGenericRepository<CompanyDescriptionPoco> companyDescriptionRepo = new EFGenericRepository<CompanyDescriptionPoco>();
            companyDescriptionRepo.Remove(new CompanyDescriptionPoco[] { _companyDescription });
            Assert.IsNull(companyDescriptionRepo.GetSingle(t => t.Id == _companyDescription.Id));
        }

        private void CompanyJobRemove()
        {
            EFGenericRepository<CompanyJobPoco> companyJobRepo = new EFGenericRepository<CompanyJobPoco>();
            companyJobRepo.Remove(new CompanyJobPoco[] { _companyJob });
            Assert.IsNull(companyJobRepo.GetSingle(t => t.Id == _companyJob.Id));
        }

        private void CompanyJobDescRemove()
        {
            EFGenericRepository<CompanyJobDescriptionPoco> companyJobDescRepo = new EFGenericRepository<CompanyJobDescriptionPoco>();
            companyJobDescRepo.Remove(new CompanyJobDescriptionPoco[] { _companyJobDescription });
            Assert.IsNull(companyJobDescRepo.GetSingle(t => t.Id == _companyJobDescription.Id));
        }

        private void CompanyLocationRemove()
        {
            EFGenericRepository<CompanyLocationPoco> companyLocationRepo = new EFGenericRepository<CompanyLocationPoco>();
            companyLocationRepo.Remove(new CompanyLocationPoco[] { _companyLocation });
            Assert.IsNull(companyLocationRepo.GetSingle(t => t.Id == _companyLocation.Id));
        }

        private void CompanyJobEducationRemove()
        {
            EFGenericRepository<CompanyJobEducationPoco> companyJobEducationRepo = new EFGenericRepository<CompanyJobEducationPoco>();
            companyJobEducationRepo.Remove(new CompanyJobEducationPoco[] { _companyJobEducation });
            Assert.IsNull(companyJobEducationRepo.GetSingle(t => t.Id == _companyJobEducation.Id));
        }

        private void CompanyJobSkillRemove()
        {
            EFGenericRepository<CompanyJobSkillPoco> companyJobSkillRepository = new EFGenericRepository<CompanyJobSkillPoco>();
            companyJobSkillRepository.Remove(new CompanyJobSkillPoco[] { _companyJobSkill });
            Assert.IsNull(companyJobSkillRepository.GetSingle(t => t.Id == _companyJobSkill.Id));
        }

        private void SystemCountryCodeRemove()
        {
            EFGenericRepository<SystemCountryCodePoco> systemCountryCodeRepository = new EFGenericRepository<SystemCountryCodePoco>();
            systemCountryCodeRepository.Remove(new SystemCountryCodePoco[] { _systemCountry });
            Assert.IsNull(systemCountryCodeRepository.GetSingle(t => t.Code == _systemCountry.Code));
        }

        private void SecurityLoginRemove()
        {
            EFGenericRepository<SecurityLoginPoco> securityLoginRepository = new EFGenericRepository<SecurityLoginPoco>();
            securityLoginRepository.Remove(new SecurityLoginPoco[] { _securityLogin });
            Assert.IsNull(securityLoginRepository.GetSingle(t => t.Id == _securityLogin.Id));
        }

        private void SecurityLoginLogRemove()
        {
            EFGenericRepository<SecurityLoginsLogPoco> securityLoginLogRepository = new EFGenericRepository<SecurityLoginsLogPoco>();
            securityLoginLogRepository.Remove(new SecurityLoginsLogPoco[] { _securityLoginLog });
            Assert.IsNull(securityLoginLogRepository.GetSingle(t => t.Id == _securityLoginLog.Id));
        }

        private void SecurityRoleRemove()
        {
            EFGenericRepository<SecurityRolePoco> securityRoleRepository = new EFGenericRepository<SecurityRolePoco>();
            securityRoleRepository.Remove(new SecurityRolePoco[] { _securityRole });
            Assert.IsNull(securityRoleRepository.GetSingle(t => t.Id == _securityRole.Id));
        }

        private void SecurityLoginRoleRemove()
        {
            EFGenericRepository<SecurityLoginsRolePoco> securityLoginRoleRepository = new EFGenericRepository<SecurityLoginsRolePoco>();
            securityLoginRoleRepository.Remove(new SecurityLoginsRolePoco[] { _securityLoginRole });
            Assert.IsNull(securityLoginRoleRepository.GetSingle(t => t.Id == _securityLoginRole.Id));
        }

        private void ApplicantProfileRemove()
        {
            EFGenericRepository<ApplicantProfilePoco> applicantProfileRepository = new EFGenericRepository<ApplicantProfilePoco>();
            applicantProfileRepository.Remove(new ApplicantProfilePoco[] { _applicantProfile });
            Assert.IsNull(applicantProfileRepository.GetSingle(t => t.Id == _applicantProfile.Id));
        }

        private void ApplicantEducationRemove()
        {
            EFGenericRepository<ApplicantEducationPoco> applicantEducationRepository = new EFGenericRepository<ApplicantEducationPoco>();
            applicantEducationRepository.Remove(new ApplicantEducationPoco[] { _applicantEducation });
            Assert.IsNull(applicantEducationRepository.GetSingle(t => t.Id == _applicantEducation.Id));
        }

        private void ApplicantJobApplicationRemove()
        {
            EFGenericRepository<ApplicantJobApplicationPoco> applicantJobApplicationRepository = new EFGenericRepository<ApplicantJobApplicationPoco>();
            applicantJobApplicationRepository.Remove(new ApplicantJobApplicationPoco[] { _applicantJobApplication });
            Assert.IsNull(applicantJobApplicationRepository.GetSingle(t => t.Id == _applicantJobApplication.Id));
        }

        private void ApplicantResumeRemove()
        {
            EFGenericRepository<ApplicantResumePoco> applicantResumeRepository = new EFGenericRepository<ApplicantResumePoco>();
            applicantResumeRepository.Remove(new ApplicantResumePoco[] { _applicantResume });
            ApplicantResumePoco applicantResumePoco = applicantResumeRepository.GetSingle(t => t.Id == _applicantResume.Id);
            Assert.IsNull(applicantResumePoco);
        }

        private void ApplicantSkillRemove()
        {
            EFGenericRepository<ApplicantSkillPoco> applicantSkillRepository = new EFGenericRepository<ApplicantSkillPoco>();
            applicantSkillRepository.Remove(new ApplicantSkillPoco[] { _applicantSkills });
            Assert.IsNull(applicantSkillRepository.GetSingle(t => t.Id == _applicantSkills.Id));
        }

        private void ApplicantWorkHistoryRemove()
        {
            EFGenericRepository<ApplicantWorkHistoryPoco> applicantWorkHistoryRepository = new EFGenericRepository<ApplicantWorkHistoryPoco>();
            applicantWorkHistoryRepository.Remove(new ApplicantWorkHistoryPoco[] { _appliantWorkHistory });
            Assert.IsNull(applicantWorkHistoryRepository.GetSingle(t => t.Id == _appliantWorkHistory.Id));
        }

        #endregion RemoveImplementation

        private string Truncate(string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return str.Length <= maxLength ? str : str.Substring(0, maxLength);
        }

    }
}
