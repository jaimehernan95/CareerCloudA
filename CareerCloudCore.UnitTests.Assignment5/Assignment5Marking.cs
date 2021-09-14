using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CareerCloudCore.UnitTests.Assignment5
{
    [TestClass]
    public class Assignment5Marking
    {
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

        [TestInitialize]
        public void Init_Pocos()
        {
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
                CompletionDate = Faker.Date.Forward(1),
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
                Password = "SoMePassWord#&@",
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
                CompanyWebsite = "www.humber.ca",
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
        public void Assignment6_DeepDive_CRUD_Test()
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
            //SecurityLoginRoleCheck();

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
            ApplicantWorkHistoryController controller = new ApplicantWorkHistoryController();
            ActionResult result = controller.PostApplicantWorkHistory(new ApplicantWorkHistoryPoco[] { _appliantWorkHistory });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void ApplicantSkillAdd()
        {
            ApplicantSkillController controller = new ApplicantSkillController();
            ActionResult result = controller.PostApplicantSkill(new ApplicantSkillPoco[] { _applicantSkills });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void ApplicantResumeAdd()
        {
            ApplicantResumeController controller = new ApplicantResumeController(); ;
            ActionResult result = controller.PostApplicantResume(new ApplicantResumePoco[] { _applicantResume });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void ApplicantJobApplicationAdd()
        {
            ApplicantJobApplicationController controller = new ApplicantJobApplicationController();
            ActionResult result = controller.PostApplicantJobApplication(new ApplicantJobApplicationPoco[] { _applicantJobApplication });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void ApplicantEducationAdd()
        {
            ApplicantEducationController controller = new ApplicantEducationController();
            ActionResult result = controller.PostApplicantEducation(new ApplicantEducationPoco[] { _applicantEducation });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void ApplicantProfileAdd()
        {
            ApplicantProfileController controller = new ApplicantProfileController();
            ActionResult result = controller.PostApplicantProfile(new ApplicantProfilePoco[] { _applicantProfile });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void SecurityLoginRoleAdd()
        {
            SecurityLoginsRoleController controller = new SecurityLoginsRoleController();
            ActionResult result = controller.PostSecurityLoginRole(new SecurityLoginsRolePoco[] { _securityLoginRole });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void SecurityRoleAdd()
        {
            SecurityRoleController controller = new SecurityRoleController();
            ActionResult result = controller.PostSecurityRole(new SecurityRolePoco[] { _securityRole });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void SecurityLoginLogAdd()
        {
            SecurityLoginsLogController controller = new SecurityLoginsLogController();
            ActionResult result = controller.PostSecurityLoginLog(new SecurityLoginsLogPoco[] { _securityLoginLog });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void SecurityLoginAdd()
        {
            SecurityLoginController controller = new SecurityLoginController();
            ActionResult result = controller.PostSecurityLogin(new SecurityLoginPoco[] { _securityLogin });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void CompanyJobSkillAdd()
        {
            CompanyJobSkillController controller = new CompanyJobSkillController();
            ActionResult result = controller.PostCompanyJobSkill(new CompanyJobSkillPoco[] { _companyJobSkill });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void CompanyJobEducationAdd()
        {
            CompanyJobEducationController controller = new CompanyJobEducationController();
            ActionResult result = controller.PostCompanyJobEducation(new CompanyJobEducationPoco[] { _companyJobEducation });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void CompanyLocationAdd()
        {
            CompanyLocationController controller = new CompanyLocationController();
            ActionResult result = controller.PostCompanyLocation(new CompanyLocationPoco[] { _companyLocation });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void CompanyJobDescriptionAdd()
        {
            CompanyJobsDescriptionController controller = new CompanyJobsDescriptionController();
            ActionResult result = controller.PostCompanyJobsDescription(new CompanyJobDescriptionPoco[] { _companyJobDescription });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void CompanyJobAdd()
        {
            CompanyJobController controller = new CompanyJobController();
            ActionResult result = controller.PostCompanyJob(new CompanyJobPoco[] { _companyJob });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void CompanyDescriptionAdd()
        {
            CompanyDescriptionController controller = new CompanyDescriptionController();
            ActionResult result = controller.PostCompanyDescription(new CompanyDescriptionPoco[] { _companyDescription });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void CompanyProfileAdd()
        {
            CompanyProfileController controller = new CompanyProfileController();
            ActionResult result = controller.PostCompanyProfile(new CompanyProfilePoco[] { _companyProfile });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void SystemLanguageCodeAdd()
        {
            SystemLanguageCodeController controller = new SystemLanguageCodeController();
            ActionResult result = controller.PostSystemLanguageCode(new SystemLanguageCodePoco[] { _systemLangCode });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void SystemCountryCodeAdd()
        {
            SystemCountryCodeController controller = new SystemCountryCodeController();
            ActionResult result = controller.PostSystemCountryCode(new SystemCountryCodePoco[] { _systemCountry });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }


        #endregion AddImplementation

        #region CheckImplementation
        private void ApplicantSkillCheck()
        {
            ApplicantSkillController controller = new ApplicantSkillController();
            var result = controller.GetApplicantSkill(_applicantSkills.Id);
            var applicantSkillResults = result as OkObjectResult;
            //var applicantSkillResult = result as OkNegotiatedContentResult<ApplicantSkillPoco>;

            Assert.IsNotNull(applicantSkillResults);
            Assert.IsNotNull(applicantSkillResults.Value);
            ApplicantSkillPoco applicantSkillResult = applicantSkillResults.Value as ApplicantSkillPoco;
            Assert.AreEqual(_applicantSkills.Id, applicantSkillResult.Id);
            Assert.AreEqual(_applicantSkills.Applicant, applicantSkillResult.Applicant);
            Assert.AreEqual(_applicantSkills.Skill, applicantSkillResult.Skill);
            Assert.AreEqual(_applicantSkills.SkillLevel, applicantSkillResult.SkillLevel);
            Assert.AreEqual(_applicantSkills.StartMonth, applicantSkillResult.StartMonth);
            Assert.AreEqual(_applicantSkills.StartYear, applicantSkillResult.StartYear);
            Assert.AreEqual(_applicantSkills.EndMonth, applicantSkillResult.EndMonth);
            Assert.AreEqual(_applicantSkills.EndYear, applicantSkillResult.EndYear);
        }

        private void ApplicantResumeCheck()
        {
            ApplicantResumeController controller = new ApplicantResumeController();
            var result = controller.GetApplicantResume(_applicantResume.Id);
            var contentResults = result as OkObjectResult;
           // var contentResult = result as OkNegotiatedContentResult<ApplicantResumePoco>;

            Assert.IsNotNull(contentResults);
            Assert.IsNotNull(contentResults.Value);
            Assert.AreEqual(200, contentResults.StatusCode);

            ApplicantResumePoco contentResult = contentResults.Value as ApplicantResumePoco;
            Assert.AreEqual(_applicantResume.Id, contentResult.Id);
            Assert.AreEqual(_applicantResume.Applicant, contentResult.Applicant);
            Assert.AreEqual(_applicantResume.Resume, contentResult.Resume);
            Assert.AreEqual(_applicantResume.LastUpdated.Value.Date, contentResult.LastUpdated.Value.Date);
        }

        private void ApplicantJobApplicationCheck()
        {
            ApplicantJobApplicationController controller = new ApplicantJobApplicationController();
            var result = controller.GetApplicantJobApplication(_applicantJobApplication.Id);
            var contentResults = result as OkObjectResult;
            //  var contentResult = result as OkNegotiatedContentResult<ApplicantJobApplicationPoco>;
            Assert.IsNotNull(contentResults);
            Assert.IsNotNull(contentResults.Value);
            Assert.AreEqual(200, contentResults.StatusCode);
            //Assert.IsNotNull(contentResult);
            //Assert.IsNotNull(contentResult.Content);
            ApplicantJobApplicationPoco contentResult = contentResults.Value as ApplicantJobApplicationPoco;
            Assert.AreEqual(_applicantJobApplication.Id, contentResult.Id);
            Assert.AreEqual(_applicantJobApplication.Applicant, contentResult.Applicant);
            Assert.AreEqual(_applicantJobApplication.Job, contentResult.Job);
            Assert.AreEqual(_applicantJobApplication.ApplicationDate.Date, contentResult.ApplicationDate.Date);
        }


        private void ApplicantEducationCheck()
        {
            ApplicantEducationController controller = new ApplicantEducationController();
            var result = controller.GetApplicantEducation(_applicantEducation.Id);
            var contentResults = result as OkObjectResult;
            
            //var contentResult = result as OkNegotiatedContentResult<ApplicantEducationPoco>;

            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

            ApplicantEducationPoco contentResult = contentResults.Value as ApplicantEducationPoco;
            Assert.AreEqual(_applicantEducation.Id, contentResult.Id);
            Assert.AreEqual(_applicantEducation.Applicant, contentResult.Applicant);
            Assert.AreEqual(_applicantEducation.Major, contentResult.Major);
            Assert.AreEqual(_applicantEducation.CertificateDiploma, contentResult.CertificateDiploma);
            Assert.AreEqual(_applicantEducation.StartDate.Value.Date, contentResult.StartDate.Value.Date);
            Assert.AreEqual(_applicantEducation.CompletionDate.Value.Date, contentResult.CompletionDate.Value.Date);
            Assert.AreEqual(_applicantEducation.CompletionPercent, contentResult.CompletionPercent);
        }

        private void ApplicantProfileCheck()
        {
            ApplicantProfileController controller = new ApplicantProfileController();
            var result = controller.GetApplicantProfile(_applicantProfile.Id);
            var contentResults = result as OkObjectResult;
            //  var contentResult = result as OkNegotiatedContentResult<ApplicantProfilePoco>;

            //  Assert.IsNotNull(contentResult);
            //  Assert.IsNotNull(contentResult.Content);
            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

            ApplicantProfilePoco contentResult = contentResults.Value as ApplicantProfilePoco;
            Assert.AreEqual(_applicantProfile.Id, contentResult.Id);
            Assert.AreEqual(_applicantProfile.Login, contentResult.Login);
            Assert.AreEqual(_applicantProfile.CurrentSalary, contentResult.CurrentSalary);
            Assert.AreEqual(_applicantProfile.CurrentRate, contentResult.CurrentRate);
            Assert.AreEqual(_applicantProfile.Currency, contentResult.Currency);
            Assert.AreEqual(_applicantProfile.Country, contentResult.Country);
            Assert.AreEqual(_applicantProfile.Province, contentResult.Province);
            Assert.AreEqual(_applicantProfile.Street, contentResult.Street);
            Assert.AreEqual(_applicantProfile.City, contentResult.City);
            Assert.AreEqual(_applicantProfile.PostalCode, contentResult.PostalCode);
        }

        private void SecurityLoginRoleCheck()
        {
            SecurityLoginsRoleController controller = new SecurityLoginsRoleController();
            var result = controller.GetSecurityLoginsRole(_securityLoginRole.Id);
            //var contentResult = result as OkNegotiatedContentResult<SecurityLoginsRolePoco>;
            var contentResults = result as OkObjectResult;
            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

            SecurityLoginsRolePoco contentResult = contentResults.Value as SecurityLoginsRolePoco;
            //Assert.IsNotNull(contentResult);
            //Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_securityLoginRole.Id, contentResult.Id);
            Assert.AreEqual(_securityLoginRole.Login, contentResult.Login);
            Assert.AreEqual(_securityLoginRole.Role, contentResult.Role);
        }

        private void SecurityRoleCheck()
        {
            SecurityRoleController controller = new SecurityRoleController();
            var result = controller.GetSecurityRole(_securityRole.Id);
            //var contentResult = result as OkNegotiatedContentResult<SecurityRolePoco>;
            var contentResults = result as OkObjectResult;
            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

            SecurityRolePoco contentResult = contentResults.Value as SecurityRolePoco;
           // Assert.IsNotNull(contentResult);
           // Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_securityRole.Id, contentResult.Id);
            Assert.AreEqual(_securityRole.Role, contentResult.Role);
            Assert.AreEqual(_securityRole.IsInactive, contentResult.IsInactive);
        }

        private void SecurityLoginLogCheck()
        {
            SecurityLoginsLogController controller = new SecurityLoginsLogController();
            var result = controller.GetSecurityLoginLog(_securityLoginLog.Id);
            //  var contentResult = result as OkNegotiatedContentResult<SecurityLoginsLogPoco>;
            var contentResults = result as OkObjectResult;
            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

            SecurityLoginsLogPoco contentResult = contentResults.Value as SecurityLoginsLogPoco;

          //  Assert.IsNotNull(contentResult);
          //  Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_securityLoginLog.Id, contentResult.Id);
            Assert.AreEqual(_securityLoginLog.Login, contentResult.Login);
            Assert.AreEqual(_securityLoginLog.SourceIP, contentResult.SourceIP);
            Assert.AreEqual(_securityLoginLog.LogonDate.Date, contentResult.LogonDate.Date);
        }

        private void SecurityLoginCheck()
        {
            SecurityLoginController controller = new SecurityLoginController();
            var result = controller.GetSecurityLogin(_securityLogin.Id);
            // var contentResult = result as OkNegotiatedContentResult<SecurityLoginPoco>;
            var contentResults = result as OkObjectResult;
            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

            SecurityLoginPoco contentResult = contentResults.Value as SecurityLoginPoco;

            // Fields changed by SecurityLoginLogic commented out
          //  Assert.IsNotNull(contentResult);
         //   Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_securityLogin.Id, contentResult.Id);
            Assert.AreEqual(_securityLogin.Login, contentResult.Login);
            // ########## Not checked because the logic layer overwrites these properties
            //Assert.AreEqual(_securityLogin.Password, securityLoginPoco.Password);
            //Assert.AreEqual(_securityLogin.Created.Date, securityLoginPoco.Created.Date);
            //Assert.AreEqual(_securityLogin.PasswordUpdate, securityLoginPoco.PasswordUpdate);
            Assert.AreEqual(_securityLogin.AgreementAccepted.Value.Date, contentResult.AgreementAccepted.Value.Date);
            // ########## Not checked because the logic layer overwrites these properties
            //Assert.AreEqual(_securityLogin.IsLocked, securityLoginPoco.IsLocked);
            //Assert.AreEqual(_securityLogin.IsInactive, securityLoginPoco.IsInactive);
            Assert.AreEqual(_securityLogin.EmailAddress, contentResult.EmailAddress);
            Assert.AreEqual(_securityLogin.PhoneNumber, contentResult.PhoneNumber);
            Assert.AreEqual(_securityLogin.FullName, contentResult.FullName);
            // ########## Not checked because the logic layer overwrites these properties
            //Assert.AreEqual(_securityLogin.ForceChangePassword, securityLoginPoco.ForceChangePassword);
            Assert.AreEqual(_securityLogin.PrefferredLanguage, contentResult.PrefferredLanguage);
        }

        private void CompanyJobSkillCheck()
        {
            CompanyJobSkillController controller = new CompanyJobSkillController();
            var result = controller.GetCompanyJobSkill(_companyJobSkill.Id);
            //    var contentResult = result as OkNegotiatedContentResult<CompanyJobSkillPoco>;

            var contentResults = result as OkObjectResult;
            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

            CompanyJobSkillPoco contentResult = contentResults.Value as CompanyJobSkillPoco;
          //  Assert.IsNotNull(contentResult);
          //  Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_companyJobSkill.Id, contentResult.Id);
            Assert.AreEqual(_companyJobSkill.Job, contentResult.Job);
            Assert.AreEqual(_companyJobSkill.Skill, contentResult.Skill);
            Assert.AreEqual(_companyJobSkill.SkillLevel, contentResult.SkillLevel);
            Assert.AreEqual(_companyJobSkill.Importance, contentResult.Importance);
        }

        private void CompanyJobEducationCheck()
        {
            CompanyJobEducationController controller = new CompanyJobEducationController();
            var result = controller.GetCompanyJobEducation(_companyJobEducation.Id);
            // var contentResult = result as OkNegotiatedContentResult<CompanyJobEducationPoco>;

            var contentResults = result as OkObjectResult;
            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

            CompanyJobEducationPoco contentResult = contentResults.Value as CompanyJobEducationPoco;
         //   Assert.IsNotNull(contentResult);
          //  Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_companyJobEducation.Id, contentResult.Id);
            Assert.AreEqual(_companyJobEducation.Job, contentResult.Job);
            Assert.AreEqual(_companyJobEducation.Major, contentResult.Major);
            Assert.AreEqual(_companyJobEducation.Importance, contentResult.Importance);
        }

        private void CompanyLocationCheck()
        {
            CompanyLocationController controller = new CompanyLocationController();
            var result = controller.GetCompanyLocation(_companyLocation.Id);
            //  var contentResult = result as OkNegotiatedContentResult<CompanyLocationPoco>;
            var contentResults = result as OkObjectResult;
            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

           CompanyLocationPoco contentResult = contentResults.Value as CompanyLocationPoco;
            //   Assert.IsNotNull(contentResult);
            //    Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_companyLocation.Id, contentResult.Id);
            Assert.AreEqual(_companyLocation.Company, contentResult.Company);
            Assert.AreEqual(_companyLocation.CountryCode.PadRight(10), contentResult.CountryCode);
            Assert.AreEqual(_companyLocation.Province.PadRight(10), contentResult.Province);
            Assert.AreEqual(_companyLocation.Street, contentResult.Street);
            Assert.AreEqual(_companyLocation.City, contentResult.City);
            Assert.AreEqual(_companyLocation.PostalCode.PadRight(20), contentResult.PostalCode);
        }

        private void CompanyJobDescriptionCheck()
        {
            CompanyJobsDescriptionController controller = new CompanyJobsDescriptionController();
            var result = controller.GetCompanyJobsDescription(_companyJobDescription.Id);
            //  var contentResult = result as OkNegotiatedContentResult<CompanyJobDescriptionPoco>;
            var contentResults = result as OkObjectResult;
            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

            CompanyJobDescriptionPoco contentResult = contentResults.Value as CompanyJobDescriptionPoco;
            //     Assert.IsNotNull(contentResult);
            //    Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_companyJobDescription.Id, contentResult.Id);
            Assert.AreEqual(_companyJobDescription.Job, contentResult.Job);
            Assert.AreEqual(_companyJobDescription.JobDescriptions, contentResult.JobDescriptions);
            Assert.AreEqual(_companyJobDescription.JobName, contentResult.JobName);
        }

        private void CompanyJobCheck()
        {
            CompanyJobController controller = new CompanyJobController();
            var result = controller.GetCompanyJob(_companyJob.Id);
            //  var contentResult = result as OkNegotiatedContentResult<CompanyJobPoco>;
            var contentResults = result as OkObjectResult;
            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

            CompanyJobPoco contentResult = contentResults.Value as CompanyJobPoco;
            //  Assert.IsNotNull(contentResult);
            //  Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_companyJob.Id, contentResult.Id);
            Assert.AreEqual(_companyJob.Company, contentResult.Company);
            Assert.AreEqual(_companyJob.ProfileCreated, contentResult.ProfileCreated);
            Assert.AreEqual(_companyJob.IsInactive, contentResult.IsInactive);
            Assert.AreEqual(_companyJob.IsCompanyHidden, contentResult.IsCompanyHidden);
        }

        private void CompanyDescriptionCheck()
        {
            CompanyDescriptionController controller = new CompanyDescriptionController();
            ActionResult result = controller.GetCompanyDescription(_companyDescription.Id);
            // var contentResult = result as OkNegotiatedContentResult<CompanyDescriptionPoco>;

            var contentResults = result as OkObjectResult;
            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

            CompanyDescriptionPoco contentResult = contentResults.Value as CompanyDescriptionPoco;
            // Assert.IsNotNull(contentResult);
            //   Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_companyDescription.Id, contentResult.Id);
            Assert.AreEqual(_companyDescription.CompanyDescription, contentResult.CompanyDescription);
            Assert.AreEqual(_companyDescription.CompanyName, contentResult.CompanyName);
            Assert.AreEqual(_companyDescription.LanguageId, contentResult.LanguageId);
            Assert.AreEqual(_companyDescription.Company, contentResult.Company);
        }


        public void CompanyProfileCheck()
        {
            CompanyProfileController controller = new CompanyProfileController();
            ActionResult result = controller.GetCompanyProfile(_companyProfile.Id);
            //   var contentResult = result as OkNegotiatedContentResult<CompanyProfilePoco>;
            var contentResults = result as OkObjectResult;
            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

            CompanyProfilePoco contentResult = contentResults.Value as CompanyProfilePoco;

            //   Assert.IsNotNull(contentResult);
            //   Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_companyProfile.CompanyWebsite, contentResult.CompanyWebsite);
            Assert.AreEqual(_companyProfile.ContactName, contentResult.ContactName);
            Assert.AreEqual(_companyProfile.ContactPhone, contentResult.ContactPhone);
            Assert.AreEqual(_companyProfile.RegistrationDate, contentResult.RegistrationDate);
            Assert.AreEqual(_companyProfile.Id, contentResult.Id);
        }

        private void ApplicantWorkHistoryCheck()
        {
            ApplicantWorkHistoryController controller = new ApplicantWorkHistoryController();
            ActionResult result = controller.GetApplicantWorkHistory(_appliantWorkHistory.Id);
            //     var contentResult = result as OkNegotiatedContentResult<ApplicantWorkHistoryPoco>;
            var contentResults = result as OkObjectResult;
            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

           ApplicantWorkHistoryPoco contentResult = contentResults.Value as ApplicantWorkHistoryPoco;

            //     Assert.IsNotNull(contentResult);
            //     Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_appliantWorkHistory.Id, contentResult.Id);
            Assert.AreEqual(_appliantWorkHistory.Applicant, contentResult.Applicant);
            Assert.AreEqual(_appliantWorkHistory.CompanyName, contentResult.CompanyName);
            Assert.AreEqual(_appliantWorkHistory.CountryCode, contentResult.CountryCode);
            Assert.AreEqual(_appliantWorkHistory.Location, contentResult.Location);
            Assert.AreEqual(_appliantWorkHistory.JobTitle, contentResult.JobTitle);
            Assert.AreEqual(_appliantWorkHistory.JobDescription, contentResult.JobDescription);
            Assert.AreEqual(_appliantWorkHistory.StartMonth, contentResult.StartMonth);
            Assert.AreEqual(_appliantWorkHistory.StartYear, contentResult.StartYear);
            Assert.AreEqual(_appliantWorkHistory.EndMonth, contentResult.EndMonth);
            Assert.AreEqual(_appliantWorkHistory.EndYear, contentResult.EndYear);
        }

        public void SystemCountryCodeCheck()
        {
            SystemCountryCodeController controller = new SystemCountryCodeController();
            ActionResult result = controller.GetSystemCountryCode(_systemCountry.Code);
            //   var contentResult = result as OkNegotiatedContentResult<SystemCountryCodePoco>;

            var contentResults = result as OkObjectResult;
            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

           SystemCountryCodePoco contentResult = contentResults.Value as SystemCountryCodePoco;
            //  Assert.IsNotNull(contentResult);
            //    Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_systemCountry.Code, contentResult.Code);
            Assert.AreEqual(_systemCountry.Name, contentResult.Name);
        }

        private void SystemLanguageCodeCheck()
        {
            SystemLanguageCodeController controller = new SystemLanguageCodeController();
            ActionResult result = controller.GetSystemLanguageCode(_systemLangCode.LanguageID);
            //   var contentResult = result as OkNegotiatedContentResult<SystemLanguageCodePoco>;
            var contentResults = result as OkObjectResult;
            Assert.IsNotNull(contentResults);
            Assert.AreEqual(200, contentResults.StatusCode);
            Assert.IsNotNull(contentResults.Value);

            SystemLanguageCodePoco contentResult = contentResults.Value as SystemLanguageCodePoco;
            //     Assert.IsNotNull(contentResult);
            //     Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.LanguageID, _systemLangCode.LanguageID);
            Assert.AreEqual(contentResult.NativeName, _systemLangCode.NativeName);
            Assert.AreEqual(contentResult.Name, _systemLangCode.Name);
        }


        #endregion CheckImplementation

        #region UpdateImplementation
        public void CompanyProfileUpdate()
        {
            _companyProfile.CompanyWebsite = "www.humber.com";
            _companyProfile.ContactName = Faker.Name.FullName();
            _companyProfile.ContactPhone = "999-555-8799";
            _companyProfile.RegistrationDate = Faker.Date.Past();
            CompanyProfileController controller = new CompanyProfileController();
            ActionResult result = controller.PutCompanyProfile(new CompanyProfilePoco[] { _companyProfile });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        public void CompanyDescriptionUpdate()
        {

            _companyDescription.CompanyDescription = Faker.Company.CatchPhrase();
            _companyDescription.CompanyName = Faker.Company.CatchPhrasePos();
            CompanyDescriptionController controller = new CompanyDescriptionController();
            ActionResult result = controller.PutCompanyDescription(new CompanyDescriptionPoco[] { _companyDescription });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        public void SecurityRoleUpdate()
        {
            _securityRole.IsInactive = true;
            _securityRole.Role = Truncate(Faker.Company.Industry(), 50);
            SecurityRoleController controller = new SecurityRoleController();
            ActionResult result = controller.PutSecurityRole(new SecurityRolePoco[] { _securityRole });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        public void CompanyJobUpdate()
        {
            _companyJob.IsCompanyHidden = true;
            _companyJob.IsInactive = true;
            _companyJob.ProfileCreated = Faker.Date.Past();
            CompanyJobController controller = new CompanyJobController();
            ActionResult result = controller.PutCompanyJob(new CompanyJobPoco[] { _companyJob });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        public void CompanyJobDescriptionUpdate()
        {
            _companyJobDescription.JobDescriptions = Truncate(Faker.Lorem.Paragraph(), 999);
            _companyJobDescription.JobName = Truncate(Faker.Lorem.Sentence(), 99);
            CompanyJobsDescriptionController controller = new CompanyJobsDescriptionController();
            ActionResult result = controller.PutCompanyJobsDescription(new CompanyJobDescriptionPoco[] { _companyJobDescription });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        public void CompanyLocationUpdate()
        {
            _companyLocation.City = Faker.Address.CityPrefix();
            _companyLocation.CountryCode = _systemCountry.Code;
            _companyLocation.Province = Faker.Address.ProvinceAbbreviation();
            _companyLocation.Street = Faker.Address.StreetName();
            _companyLocation.PostalCode = Faker.Address.CanadianZipCode();
            CompanyLocationController controller = new CompanyLocationController();
            ActionResult result = controller.PutCompanyLocation(new CompanyLocationPoco[] { _companyLocation });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        public void CompanyJobEducationUpdate()
        {
            _companyJobEducation.Importance = 1;
            _companyJobEducation.Major = Truncate(Faker.Lorem.Sentence(), 100);
            CompanyJobEducationController controller = new CompanyJobEducationController();
            ActionResult result = controller.PutCompanyJobEducation(new CompanyJobEducationPoco[] { _companyJobEducation });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        public void CompanyJobSkillUpdate()
        {
            _companyJobSkill.Importance = 1;
            _companyJobSkill.Skill = Truncate(Faker.Lorem.Sentence(), 100);
            _companyJobSkill.SkillLevel = String.Concat(Faker.Lorem.Letters(10));
            CompanyJobSkillController controller = new CompanyJobSkillController();
            ActionResult result = controller.PutCompanyJobSkill(new CompanyJobSkillPoco[] { _companyJobSkill });
            Assert.IsInstanceOfType(result, typeof(OkResult));
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
            _securityLogin.Password = "SoMePassWord@&@";
            _securityLogin.PasswordUpdate = Faker.Date.Forward();
            _securityLogin.PhoneNumber = "416-416-9889";
            _securityLogin.PrefferredLanguage = "FR".PadRight(10);
            SecurityLoginController controller = new SecurityLoginController();
            ActionResult result = controller.PutSecurityLogin(new SecurityLoginPoco[] { _securityLogin });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        public void SecurityLoginLogUpdate()
        {
            _securityLoginLog.IsSuccesful = false;
            _securityLoginLog.LogonDate = Faker.Date.PastWithTime();
            _securityLoginLog.SourceIP = Faker.Internet.IPv4().PadRight(15);
            SecurityLoginsLogController controller = new SecurityLoginsLogController();
            ActionResult result = controller.PutSecurityLoginLog(new SecurityLoginsLogPoco[] { _securityLoginLog });
            Assert.IsInstanceOfType(result, typeof(OkResult));
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
            ApplicantProfileController controller = new ApplicantProfileController();
            ActionResult result = controller.PutApplicantProfile(new ApplicantProfilePoco[] { _applicantProfile });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        public void ApplicantEducationUpdate()
        {
            _applicantEducation.Major = Faker.Education.Major();
            _applicantEducation.CertificateDiploma = Faker.Education.Major();
            _applicantEducation.StartDate = Faker.Date.Past(3);
            _applicantEducation.CompletionDate = Faker.Date.Forward(1);
            _applicantEducation.CompletionPercent = (byte)Faker.Number.RandomNumber(1);
            ApplicantEducationController controller = new ApplicantEducationController();
            ActionResult result = controller.PutApplicantEducation(new ApplicantEducationPoco[] { _applicantEducation });
            Assert.IsInstanceOfType(result, typeof(OkResult));

        }

        public void ApplicantJobApplicationUpdate()
        {
            _applicantJobApplication.ApplicationDate = Faker.Date.Recent();
            ApplicantJobApplicationController controller = new ApplicantJobApplicationController();
            ActionResult result = controller.PutApplicantJobApplication(new ApplicantJobApplicationPoco[] { _applicantJobApplication });
            Assert.IsInstanceOfType(result, typeof(OkResult));

        }

        public void ApplicantResumeUpdate()
        {
            _applicantResume.Resume = Faker.Lorem.Paragraph();
            _applicantResume.LastUpdated = Faker.Date.Recent();
            ApplicantResumeController controller = new ApplicantResumeController();
            ActionResult result = controller.PutApplicantResume(new ApplicantResumePoco[] { _applicantResume });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void ApplicantSkillUpdate()
        {
            _applicantSkills.EndMonth = 12;
            _applicantSkills.EndYear = 1999;
            _applicantSkills.Skill = Truncate(Faker.Lorem.Sentence(), 100);
            _applicantSkills.SkillLevel = Truncate(Faker.Lorem.Sentence(), 10);
            _applicantSkills.StartMonth = 1;
            _applicantSkills.StartYear = 1999;
            ApplicantSkillController controller = new ApplicantSkillController();
            ActionResult result = controller.PutApplicantSkill(new ApplicantSkillPoco[] { _applicantSkills });
            Assert.IsInstanceOfType(result, typeof(OkResult));
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

            ApplicantWorkHistoryController controller = new ApplicantWorkHistoryController();
            ActionResult result = controller.PutApplicantWorkHistory(new ApplicantWorkHistoryPoco[] { _appliantWorkHistory });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void SystemCountryCodeUpdate()
        {
            _systemCountry.Name = Truncate(Faker.Name.FullName(), 50);

            SystemCountryCodeController controller = new SystemCountryCodeController();
            ActionResult result = controller.PutSystemCountryCode(new SystemCountryCodePoco[] { _systemCountry });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        private void SystemLanguageCodeUpdate()
        {
            _systemLangCode.NativeName = Truncate(Faker.Lorem.Sentence(), 50);
            _systemLangCode.Name = Truncate(Faker.Lorem.Sentence(), 50);

            SystemLanguageCodeController controller = new SystemLanguageCodeController();
            ActionResult result = controller.PutSystemLanguageCode(new SystemLanguageCodePoco[] { _systemLangCode });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
        #endregion UpdateImplementation


        #region RemoveImplementation
        private void SystemLanguageCodeRemove()
        {
            SystemLanguageCodeController controller = new SystemLanguageCodeController();
            var result = controller.DeleteSystemLanguageCode(new SystemLanguageCodePoco[] { _systemLangCode });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetSystemLanguageCode(_systemLangCode.LanguageID);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void CompanyProfileRemove()
        {
            CompanyProfileController controller = new CompanyProfileController();
            var result = controller.DeleteCompanyProfile(new CompanyProfilePoco[] { _companyProfile });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetCompanyProfile(_companyProfile.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void CompanyDescriptionRemove()
        {
            CompanyDescriptionController controller = new CompanyDescriptionController();
            var result = controller.DeleteCompanyDescription(new CompanyDescriptionPoco[] { _companyDescription });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetCompanyDescription(_companyDescription.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void CompanyJobRemove()
        {
            CompanyJobController controller = new CompanyJobController();
            var result = controller.DeleteCompanyJob(new CompanyJobPoco[] { _companyJob });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetCompanyJob(_companyJob.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void CompanyJobDescRemove()
        {
            CompanyJobsDescriptionController controller = new CompanyJobsDescriptionController();
            var result = controller.DeleteCompanyJobsDescription(new CompanyJobDescriptionPoco[] { _companyJobDescription });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetCompanyJobsDescription(_companyJobDescription.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void CompanyLocationRemove()
        {
            CompanyLocationController controller = new CompanyLocationController();
            var result = controller.DeleteCompanyLocation(new CompanyLocationPoco[] { _companyLocation });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetCompanyLocation(_companyLocation.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void CompanyJobEducationRemove()
        {
            CompanyJobEducationController controller = new CompanyJobEducationController();
            var result = controller.DeleteCompanyJobEducation(new CompanyJobEducationPoco[] { _companyJobEducation });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetCompanyJobEducation(_companyJobEducation.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void CompanyJobSkillRemove()
        {
            CompanyJobSkillController controller = new CompanyJobSkillController();
            var result = controller.DeleteCompanyJobSkill(new CompanyJobSkillPoco[] { _companyJobSkill });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetCompanyJobSkill(_companyJobSkill.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void SystemCountryCodeRemove()
        {
            SystemCountryCodeController controller = new SystemCountryCodeController();
            ActionResult result = controller.DeleteSystemCountryCode(new SystemCountryCodePoco[] { _systemCountry });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetSystemCountryCode(_systemCountry.Code);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        private void SecurityLoginRemove()
        {
            SecurityLoginController controller = new SecurityLoginController();
            ActionResult result = controller.DeleteSecurityLogin(new SecurityLoginPoco[] { _securityLogin });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetSecurityLogin(_securityLogin.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void SecurityLoginLogRemove()
        {
            SecurityLoginsLogController controller = new SecurityLoginsLogController();
            ActionResult result = controller.DeleteSecurityLoginLog(new SecurityLoginsLogPoco[] { _securityLoginLog });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetSecurityLoginLog(_securityLoginLog.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void SecurityRoleRemove()
        {
            SecurityRoleController controller = new SecurityRoleController();
            ActionResult result = controller.DeleteSecurityRole(new SecurityRolePoco[] { _securityRole });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetSecurityRole(_securityRole.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void SecurityLoginRoleRemove()
        {
            SecurityLoginsRoleController controller = new SecurityLoginsRoleController();
            ActionResult result = controller.DeleteSecurityLoginRole(new SecurityLoginsRolePoco[] { _securityLoginRole });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetSecurityLoginsRole(_securityLoginRole.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void ApplicantProfileRemove()
        {
            ApplicantProfileController controller = new ApplicantProfileController();
            ActionResult result = controller.DeleteApplicantProfile(new ApplicantProfilePoco[] { _applicantProfile });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetApplicantProfile(_securityLoginRole.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void ApplicantEducationRemove()
        {
            ApplicantEducationController controller = new ApplicantEducationController();
            ActionResult result = controller.DeleteApplicantEducation(new ApplicantEducationPoco[] { _applicantEducation });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetApplicantEducation(_applicantEducation.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void ApplicantJobApplicationRemove()
        {
            ApplicantJobApplicationController controller = new ApplicantJobApplicationController();
            ActionResult result = controller.DeleteApplicantJobApplication(new ApplicantJobApplicationPoco[] { _applicantJobApplication });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetApplicantJobApplication(_applicantEducation.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void ApplicantResumeRemove()
        {
            ApplicantResumeController controller = new ApplicantResumeController();
            ActionResult result = controller.DeleteApplicantResume(new ApplicantResumePoco[] { _applicantResume });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetApplicantResume(_applicantResume.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void ApplicantSkillRemove()
        {
            ApplicantSkillController controller = new ApplicantSkillController();
            ActionResult result = controller.DeleteApplicantSkill(new ApplicantSkillPoco[] { _applicantSkills });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetApplicantSkill(_applicantSkills.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private void ApplicantWorkHistoryRemove()
        {

            ApplicantWorkHistoryController controller = new ApplicantWorkHistoryController();
            ActionResult result = controller.DeleteApplicantWorkHistory(new ApplicantWorkHistoryPoco[] { _appliantWorkHistory });
            Assert.IsInstanceOfType(result, typeof(OkResult));

            result = controller.GetApplicantWorkHistory(_appliantWorkHistory.Id);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        #endregion RemoveImplementation


        private string Truncate(string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return str.Length <= maxLength ? str : str.Substring(0, maxLength);
        }
    }
}
