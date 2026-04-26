using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace CareerCloud.UnitTests.Assignment3
{
    [TestCategory("Assignment 3 Marking") ]
    [TestClass]
    public class Assignment3Marking
    {
        private const string _assemblyUnderTest = "CareerCloud.BusinessLogicLayer.dll";
        private Type[] _types;

        [TestInitialize]
        public void Init_Tests()
        {
            ApplicantEducationLogic logic = new ApplicantEducationLogic(null);
            _types = Assembly.LoadFrom(_assemblyUnderTest).GetTypes();
        }

        [TestMethod]
        public void Assingment_3_Logic_Creation()
        {
            Assert.IsTrue(_types.Any(t => t.Name == "ApplicantEducationLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "ApplicantEducationLogic").FirstOrDefault().BaseType == typeof(BaseLogic<ApplicantEducationPoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "ApplicantJobApplicationLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "ApplicantJobApplicationLogic").FirstOrDefault().BaseType == typeof(BaseLogic<ApplicantJobApplicationPoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "ApplicantProfileLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "ApplicantProfileLogic").FirstOrDefault().BaseType == typeof(BaseLogic<ApplicantProfilePoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "ApplicantResumeLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "ApplicantResumeLogic").FirstOrDefault().BaseType == typeof(BaseLogic<ApplicantResumePoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "ApplicantSkillLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "ApplicantSkillLogic").FirstOrDefault().BaseType == typeof(BaseLogic<ApplicantSkillPoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "ApplicantWorkHistoryLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "ApplicantWorkHistoryLogic").FirstOrDefault().BaseType == typeof(BaseLogic<ApplicantWorkHistoryPoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "BaseLogic`1"));
            Assert.IsTrue(_types.Any(t => t.Name == "CompanyDescriptionLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "CompanyDescriptionLogic").FirstOrDefault().BaseType == typeof(BaseLogic<CompanyDescriptionPoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "CompanyJobDescriptionLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "CompanyJobDescriptionLogic").FirstOrDefault().BaseType == typeof(BaseLogic<CompanyJobDescriptionPoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "CompanyJobEducationLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "CompanyJobEducationLogic").FirstOrDefault().BaseType == typeof(BaseLogic<CompanyJobEducationPoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "CompanyJobLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "CompanyJobLogic").FirstOrDefault().BaseType == typeof(BaseLogic<CompanyJobPoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "CompanyJobSkillLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "CompanyJobSkillLogic").FirstOrDefault().BaseType == typeof(BaseLogic<CompanyJobSkillPoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "CompanyLocationLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "CompanyLocationLogic").FirstOrDefault().BaseType == typeof(BaseLogic<CompanyLocationPoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "CompanyProfileLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "CompanyProfileLogic").FirstOrDefault().BaseType == typeof(BaseLogic<CompanyProfilePoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "SecurityLoginLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "SecurityLoginLogic").FirstOrDefault().BaseType == typeof(BaseLogic<SecurityLoginPoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "SecurityLoginsLogLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "SecurityLoginsLogLogic").FirstOrDefault().BaseType == typeof(BaseLogic<SecurityLoginsLogPoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "SecurityLoginsRoleLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "SecurityLoginsRoleLogic").FirstOrDefault().BaseType == typeof(BaseLogic<SecurityLoginsRolePoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "SecurityRoleLogic"));
            Assert.IsTrue(_types.Where(t => t.Name == "SecurityRoleLogic").FirstOrDefault().BaseType == typeof(BaseLogic<SecurityRolePoco>));
            Assert.IsTrue(_types.Any(t => t.Name == "SystemCountryCodeLogic"));
            Assert.IsTrue(_types.Any(t => t.Name == "SystemLanguageCodeLogic"));
        }

        #region ApplicantEducation_Tests

        [TestMethod]
        public void ApplicantEducation_Major_GreaterThan2Chars_Add_Test()
        {
            Mock<IDataRepository<ApplicantEducationPoco>> moqRepo = new Mock<IDataRepository<ApplicantEducationPoco>>();
            ApplicantEducationLogic logic = new ApplicantEducationLogic(moqRepo.Object);
            try
            {
                logic.Add(new ApplicantEducationPoco[]
                    { new ApplicantEducationPoco() { Major = "BA" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 107));
            }
        }

        [TestMethod]
        public void ApplicantEducation_Major_GreaterThan2Chars_Update_Test()
        {
            Mock<IDataRepository<ApplicantEducationPoco>> moqRepo = new Mock<IDataRepository<ApplicantEducationPoco>>();
            ApplicantEducationLogic logic = new ApplicantEducationLogic(moqRepo.Object);
            try
            {
                logic.Update(new ApplicantEducationPoco[]
                    { new ApplicantEducationPoco() { Major = "BA" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 107));
            }
        }

        [TestMethod]
        public void ApplicantEducation_StartDate_LessThanToday_Add_Test()
        {
            Mock<IDataRepository<ApplicantEducationPoco>> moqRepo = new Mock<IDataRepository<ApplicantEducationPoco>>();
            ApplicantEducationLogic logic = new ApplicantEducationLogic(moqRepo.Object);
            try
            {
                logic.Add(new ApplicantEducationPoco[]
                    { new ApplicantEducationPoco() { StartDate = DateTime.Now + new TimeSpan(1,0,0) } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 108));
            }
        }

        [TestMethod]
        public void ApplicantEducation_StartDate_LessThanToday_Update_Test()
        {
            Mock<IDataRepository<ApplicantEducationPoco>> moqRepo = new Mock<IDataRepository<ApplicantEducationPoco>>();
            ApplicantEducationLogic logic = new ApplicantEducationLogic(moqRepo.Object);
            try
            {
                logic.Update(new ApplicantEducationPoco[]
                    { new ApplicantEducationPoco() { StartDate = DateTime.Now + new TimeSpan(1,0,0) } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 108));
            }
        }

        [TestMethod]
        public void ApplicantEducation_CompletionDate_NotGreaterThanStartDate_Add_Test()
        {
            Mock<IDataRepository<ApplicantEducationPoco>> moqRepo = new Mock<IDataRepository<ApplicantEducationPoco>>();
            ApplicantEducationLogic logic = new ApplicantEducationLogic(moqRepo.Object);
            try
            {
                logic.Add(new ApplicantEducationPoco[]
                    { new ApplicantEducationPoco() { StartDate = DateTime.Now + new TimeSpan(1,0,0), CompletionDate = DateTime.Now } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 109));
            }
        }

        [TestMethod]
        public void ApplicantEducation_CompletionDate_NotGreaterThanStartDate_Update_Test()
        {
            Mock<IDataRepository<ApplicantEducationPoco>> moqRepo = new Mock<IDataRepository<ApplicantEducationPoco>>();
            ApplicantEducationLogic logic = new ApplicantEducationLogic(moqRepo.Object);
            try
            {
                logic.Update(new ApplicantEducationPoco[]
                    { new ApplicantEducationPoco() { StartDate = DateTime.Now + new TimeSpan(1,0,0), CompletionDate = DateTime.Now } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 109));
            }
        }

        #endregion ApplicantEducation_Tests

        #region ApplicantJobApplication_Tests

        [TestMethod]
        public void ApplicantJobApplicant_ApplicationDate_CannotBeGreaterThanToday_Add_Test()
        {
            Mock<IDataRepository<ApplicantJobApplicationPoco>> moqRepo = new Mock<IDataRepository<ApplicantJobApplicationPoco>>();
            ApplicantJobApplicationLogic logic = new ApplicantJobApplicationLogic(moqRepo.Object);
            try
            {
                logic.Add(new ApplicantJobApplicationPoco[]
                    { new ApplicantJobApplicationPoco() { ApplicationDate = DateTime.Now + new TimeSpan(1,0,0,0) } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 110));
            }
        }


        [TestMethod]
        public void ApplicantJobApplicant_ApplicationDate_CannotBeGreaterThanToday_Update_Test()
        {
            Mock<IDataRepository<ApplicantJobApplicationPoco>> moqRepo = new Mock<IDataRepository<ApplicantJobApplicationPoco>>();
            ApplicantJobApplicationLogic logic = new ApplicantJobApplicationLogic(moqRepo.Object);
            try
            {
                logic.Update(new ApplicantJobApplicationPoco[]
                    { new ApplicantJobApplicationPoco() { ApplicationDate = DateTime.Now + new TimeSpan(1,0,0,0) } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 110));
            }
        }

        #endregion ApplicantJobApplication_Tests

        #region ApplicantProfile_Tests

        [TestMethod]
        public void ApplicantProfile_CurrentSalary_CannotBeNegative_Add_Test()
        {
            Mock<IDataRepository<ApplicantProfilePoco>> moqRepo = new Mock<IDataRepository<ApplicantProfilePoco>>();
            ApplicantProfileLogic logic = new ApplicantProfileLogic(moqRepo.Object);
            try
            {
                logic.Add(new ApplicantProfilePoco[]
                    { new ApplicantProfilePoco() { CurrentSalary = -1 } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 111));
            }
        }

        [TestMethod]
        public void ApplicantProfile_CurrentSalary_CannotBeNegative_Update_Test()
        {
            Mock<IDataRepository<ApplicantProfilePoco>> moqRepo = new Mock<IDataRepository<ApplicantProfilePoco>>();
            ApplicantProfileLogic logic = new ApplicantProfileLogic(moqRepo.Object);
            try
            {
                logic.Update(new ApplicantProfilePoco[]
                    { new ApplicantProfilePoco() { CurrentSalary = -1 } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 111));
            }
        }

        [TestMethod]
        public void ApplicantProfile_CurrentRate_CannotBeNegative_Add_Test()
        {
            Mock<IDataRepository<ApplicantProfilePoco>> moqRepo = new Mock<IDataRepository<ApplicantProfilePoco>>();
            ApplicantProfileLogic logic = new ApplicantProfileLogic(moqRepo.Object);
            try
            {
                logic.Add(new ApplicantProfilePoco[]
                    { new ApplicantProfilePoco() { CurrentRate = -1 } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 112));
            }
        }

        [TestMethod]
        public void ApplicantProfile_CurrentRate_CannotBeNegative_Update_Test()
        {
            Mock<IDataRepository<ApplicantProfilePoco>> moqRepo = new Mock<IDataRepository<ApplicantProfilePoco>>();
            ApplicantProfileLogic logic = new ApplicantProfileLogic(moqRepo.Object);
            try
            {
                logic.Update(new ApplicantProfilePoco[]
                    { new ApplicantProfilePoco() { CurrentRate = -1 } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 112));
            }
        }


        #endregion ApplicantProfile_Tests

        #region ApplicantResume_Tests
        [TestMethod]
        public void ApplicantResume_Resume_Must_Be_Supplied_Add_Test()
        {
            Mock<IDataRepository<ApplicantResumePoco>> moqRepo = new Mock<IDataRepository<ApplicantResumePoco>>();
            ApplicantResumeLogic logic = new ApplicantResumeLogic(moqRepo.Object);
            try
            {
                logic.Add(new ApplicantResumePoco[]
                    { new ApplicantResumePoco() { Id = Guid.NewGuid(), Resume = string.Empty } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 113));
            }
        }

        [TestMethod]
        public void ApplicantResume_Resume_Must_Be_Supplied_Update_Test()
        {
            Mock<IDataRepository<ApplicantResumePoco>> moqRepo = new Mock<IDataRepository<ApplicantResumePoco>>();
            ApplicantResumeLogic logic = new ApplicantResumeLogic(moqRepo.Object);
            try
            {
                logic.Update(new ApplicantResumePoco[]
                    { new ApplicantResumePoco() { Id = Guid.NewGuid(), Resume = string.Empty } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 113));
            }
        }

        #endregion ApplicantResume_Tests

        #region ApplicantSkill_Tests
        [TestMethod]
        public void ApplicantSkill_StartMonth_LessThan12_Add_Test()
        {
            Mock<IDataRepository<ApplicantSkillPoco>> moqRepo = new Mock<IDataRepository<ApplicantSkillPoco>>();
            ApplicantSkillLogic logic = new ApplicantSkillLogic(moqRepo.Object);
            try
            {
                logic.Add(new ApplicantSkillPoco[]
                    { new ApplicantSkillPoco() { Id = Guid.NewGuid(), StartMonth = 13 } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 101));
            }
        }

        [TestMethod]
        public void ApplicantSkill_StartMonth_LessThan12_Update_Test()
        {
            Mock<IDataRepository<ApplicantSkillPoco>> moqRepo = new Mock<IDataRepository<ApplicantSkillPoco>>();
            ApplicantSkillLogic logic = new ApplicantSkillLogic(moqRepo.Object);
            try
            {
                logic.Update(new ApplicantSkillPoco[]
                    { new ApplicantSkillPoco() { Id = Guid.NewGuid(), StartMonth = 13 } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 101));
            }
        }


        [TestMethod]
        public void ApplicantSkill_EndMonth_LessThan12_Add_Test()
        {
            Mock<IDataRepository<ApplicantSkillPoco>> moqRepo = new Mock<IDataRepository<ApplicantSkillPoco>>();
            ApplicantSkillLogic logic = new ApplicantSkillLogic(moqRepo.Object);
            try
            {
                logic.Add(new ApplicantSkillPoco[]
                    { new ApplicantSkillPoco() { Id = Guid.NewGuid(), StartMonth=12, EndMonth = 13 } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 102));
            }
        }

        [TestMethod]
        public void ApplicantSkill_EndMonth_LessThan12_Update_Test()
        {
            Mock<IDataRepository<ApplicantSkillPoco>> moqRepo = new Mock<IDataRepository<ApplicantSkillPoco>>();
            ApplicantSkillLogic logic = new ApplicantSkillLogic(moqRepo.Object);
            try
            {
                logic.Update(new ApplicantSkillPoco[]
                    { new ApplicantSkillPoco() { Id = Guid.NewGuid(),StartMonth = 12, EndMonth = 13 } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 102));
            }
        }

        [TestMethod]
        public void ApplicantSkill_StartYear_LessThan1900_Add_Test()
        {
            Mock<IDataRepository<ApplicantSkillPoco>> moqRepo = new Mock<IDataRepository<ApplicantSkillPoco>>();
            ApplicantSkillLogic logic = new ApplicantSkillLogic(moqRepo.Object);
            try
            {
                logic.Add(new ApplicantSkillPoco[]
                    { new ApplicantSkillPoco() { Id = Guid.NewGuid(),StartMonth = 12, EndMonth = 12, StartYear = 1899 } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 103));
            }
        }

        [TestMethod]
        public void ApplicantSkill_StartYear_LessThan1900_Update_Test()
        {
            Mock<IDataRepository<ApplicantSkillPoco>> moqRepo = new Mock<IDataRepository<ApplicantSkillPoco>>();
            ApplicantSkillLogic logic = new ApplicantSkillLogic(moqRepo.Object);
            try
            {
                logic.Update(new ApplicantSkillPoco[]
                    { new ApplicantSkillPoco() { Id = Guid.NewGuid(),StartMonth = 12, EndMonth = 12, StartYear = 1899 } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 103));
            }
        }

        [TestMethod]
        public void ApplicantSkill_EndYear_LessThanStartYear_Add_Test()
        {
            Mock<IDataRepository<ApplicantSkillPoco>> moqRepo = new Mock<IDataRepository<ApplicantSkillPoco>>();
            ApplicantSkillLogic logic = new ApplicantSkillLogic(moqRepo.Object);
            try
            {
                logic.Add(new ApplicantSkillPoco[]
                    { new ApplicantSkillPoco() { Id = Guid.NewGuid(),StartMonth = 12, EndMonth = 12, StartYear = 2017, EndYear = 2016 } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 104));
            }
        }

        [TestMethod]
        public void ApplicantSkill_EndYear_LessThanStartYear_Update_Test()
        {
            Mock<IDataRepository<ApplicantSkillPoco>> moqRepo = new Mock<IDataRepository<ApplicantSkillPoco>>();
            ApplicantSkillLogic logic = new ApplicantSkillLogic(moqRepo.Object);
            try
            {
                logic.Add(new ApplicantSkillPoco[]
                    { new ApplicantSkillPoco() { Id = Guid.NewGuid(),StartMonth = 12, EndMonth = 12, StartYear = 2017, EndYear = 2016 } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 104));
            }
        }

        #endregion ApplicantSkill_Tests

        #region ApplicantWorkHistory_Tests
        [TestMethod]
        public void ApplicantWorkHistory_CompanyName_GreaterThan2Chars_Add_Test()
        {
            Mock<IDataRepository<ApplicantWorkHistoryPoco>> moqRepo = new Mock<IDataRepository<ApplicantWorkHistoryPoco>>();
            ApplicantWorkHistoryLogic logic = new ApplicantWorkHistoryLogic(moqRepo.Object);
            try
            {
                logic.Add(new ApplicantWorkHistoryPoco[] { new ApplicantWorkHistoryPoco() { CompanyName = "AB" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 105));
            }
        }

        public void ApplicantWorkHistory_CompanyName_GreaterThan2Chars_Update_Test()
        {
            Mock<IDataRepository<ApplicantWorkHistoryPoco>> moqRepo = new Mock<IDataRepository<ApplicantWorkHistoryPoco>>();
            ApplicantWorkHistoryLogic logic = new ApplicantWorkHistoryLogic(moqRepo.Object);
            try
            {
                logic.Update(new ApplicantWorkHistoryPoco[] { new ApplicantWorkHistoryPoco() { CompanyName = "AB" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 105));
            }
        }

        #endregion ApplicantWorkHistory_Tests

        #region CompanyDescription_Tests

        [TestMethod]
        public void CompanyDescription_CompanyName_GreaterThan2Chars_Add()
        {
            Mock<IDataRepository<CompanyDescriptionPoco>> moqRepo = new Mock<IDataRepository<CompanyDescriptionPoco>>();
            CompanyDescriptionLogic logic = new CompanyDescriptionLogic(moqRepo.Object);
            try
            {
                logic.Add(new CompanyDescriptionPoco[] { new CompanyDescriptionPoco() { CompanyName = "AB" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 106));
            }
        }

        [TestMethod]
        public void CompanyDescription_CompanyName_GreaterThan2Chars_Update()
        {
            Mock<IDataRepository<CompanyDescriptionPoco>> moqRepo = new Mock<IDataRepository<CompanyDescriptionPoco>>();
            CompanyDescriptionLogic logic = new CompanyDescriptionLogic(moqRepo.Object);
            try
            {
                logic.Update(new CompanyDescriptionPoco[] { new CompanyDescriptionPoco() { CompanyName = "AB" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 106));
            }
        }

        [TestMethod]
        public void CompanyDescription_CompanyDescription_GreaterThan2Chars_Add()
        {
            Mock<IDataRepository<CompanyDescriptionPoco>> moqRepo = new Mock<IDataRepository<CompanyDescriptionPoco>>();
            CompanyDescriptionLogic logic = new CompanyDescriptionLogic(moqRepo.Object);
            try
            {
                logic.Add(new CompanyDescriptionPoco[] { new CompanyDescriptionPoco() { CompanyDescription = "AB" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 107));
            }
        }

        [TestMethod]
        public void CompanyDescription_CompanyDescription_GreaterThan2Chars_Update()
        {
            Mock<IDataRepository<CompanyDescriptionPoco>> moqRepo = new Mock<IDataRepository<CompanyDescriptionPoco>>();
            CompanyDescriptionLogic logic = new CompanyDescriptionLogic(moqRepo.Object);
            try
            {
                logic.Update(new CompanyDescriptionPoco[] { new CompanyDescriptionPoco() { CompanyDescription = "AB" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 107));
            }
        }

        #endregion CompanyDescription_Tests

        #region CompanyJobEducation_Tests

        [TestMethod]
        public void CompanyJobEducation_Major_CannotBeLessThanTwoCharacters_Add_Test()
        {
            Mock<IDataRepository<CompanyJobEducationPoco>> moqRepo = new Mock<IDataRepository<CompanyJobEducationPoco>>();
            CompanyJobEducationLogic logic = new CompanyJobEducationLogic(moqRepo.Object);
            try
            {
                logic.Add(new CompanyJobEducationPoco[] { new CompanyJobEducationPoco() { Major = "B" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 200));
            }
        }

        [TestMethod]
        public void CompanyJobEducation_Major_CannotBeLessThanTwoCharacters_Update_Test()
        {
            Mock<IDataRepository<CompanyJobEducationPoco>> moqRepo = new Mock<IDataRepository<CompanyJobEducationPoco>>();
            CompanyJobEducationLogic logic = new CompanyJobEducationLogic(moqRepo.Object);
            try
            {
                logic.Update(new CompanyJobEducationPoco[] { new CompanyJobEducationPoco() { Major = "B" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 200));
            }
        }

        [TestMethod]
        public void CompanyJobEducation_Importance_CannotBeNegative_Add_Test()
        {
            Mock<IDataRepository<CompanyJobEducationPoco>> moqRepo = new Mock<IDataRepository<CompanyJobEducationPoco>>();
            CompanyJobEducationLogic logic = new CompanyJobEducationLogic(moqRepo.Object);
            try
            {
                logic.Add(new CompanyJobEducationPoco[] { new CompanyJobEducationPoco() { Importance = -1} });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 201));
            }
        }

        [TestMethod]
        public void CompanyJobEducation_Importance_CannotBeNegative_Update_Test()
        {
            Mock<IDataRepository<CompanyJobEducationPoco>> moqRepo = new Mock<IDataRepository<CompanyJobEducationPoco>>();
            CompanyJobEducationLogic logic = new CompanyJobEducationLogic(moqRepo.Object);
            try
            {
                logic.Update(new CompanyJobEducationPoco[] { new CompanyJobEducationPoco() { Importance = -1 } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 201));
            }
        }

        #endregion CompanyJobEducation_Tests

        #region CompanyJobDescription_Tests()
        
        [TestMethod]
        public void CompanyJobDescription_JobName_CannotBeEmpty_Add_Test()
        {
            Mock<IDataRepository<CompanyJobDescriptionPoco>> moqRepo = new Mock<IDataRepository<CompanyJobDescriptionPoco>>();
            CompanyJobDescriptionLogic logic = new CompanyJobDescriptionLogic(moqRepo.Object);
            try
            {
                logic.Add(new CompanyJobDescriptionPoco[] { new CompanyJobDescriptionPoco()});
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 300));
            }
        }

        [TestMethod]
        public void CompanyJobDescription_JobName_CannotBeEmpty_Update_Test()
        {
            Mock<IDataRepository<CompanyJobDescriptionPoco>> moqRepo = new Mock<IDataRepository<CompanyJobDescriptionPoco>>();
            CompanyJobDescriptionLogic logic = new CompanyJobDescriptionLogic(moqRepo.Object);
            try
            {
                logic.Update(new CompanyJobDescriptionPoco[] { new CompanyJobDescriptionPoco() });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 300));
            }
        }

        [TestMethod]
        public void CompanyJobDescription_JobDescription_CannotBeEmpty_Add_Test()
        {
            Mock<IDataRepository<CompanyJobDescriptionPoco>> moqRepo = new Mock<IDataRepository<CompanyJobDescriptionPoco>>();
            CompanyJobDescriptionLogic logic = new CompanyJobDescriptionLogic(moqRepo.Object);
            try
            {
                logic.Add(new CompanyJobDescriptionPoco[] { new CompanyJobDescriptionPoco() });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 301));
            }
        }

        [TestMethod]
        public void CompanyJobDescription_JobDescription_CannotBeEmpty_Update_Test()
        {
            Mock<IDataRepository<CompanyJobDescriptionPoco>> moqRepo = new Mock<IDataRepository<CompanyJobDescriptionPoco>>();
            CompanyJobDescriptionLogic logic = new CompanyJobDescriptionLogic(moqRepo.Object);
            try
            {
                logic.Update(new CompanyJobDescriptionPoco[] { new CompanyJobDescriptionPoco() });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 301));
            }
        }

        #endregion CompanyJobDescription_Tests()

        #region CompanyJobSkill_Tests
        [TestMethod]
        public void CompanyJobSkill_Importance_CannotBeNegative_Add_Test()
        {
            Mock<IDataRepository<CompanyJobSkillPoco>> moqRepo = new Mock<IDataRepository<CompanyJobSkillPoco>>();
            CompanyJobSkillLogic logic = new CompanyJobSkillLogic(moqRepo.Object);
            try
            {
                logic.Add(new CompanyJobSkillPoco[] { new CompanyJobSkillPoco() {Importance = -1 } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 400));
            }
        }
        [TestMethod]
        public void CompanyJobSkill_Importance_CannotBeNegative_Update_Test()
        {
            Mock<IDataRepository<CompanyJobSkillPoco>> moqRepo = new Mock<IDataRepository<CompanyJobSkillPoco>>();
            CompanyJobSkillLogic logic = new CompanyJobSkillLogic(moqRepo.Object);
            try
            {
                logic.Update(new CompanyJobSkillPoco[] { new CompanyJobSkillPoco() { Importance = -1 } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 400));
            }
        }

        #endregion CompanyJobSkill_Tests

        #region CompanyLocation_Tests

        [TestMethod]
        public void CompanyLocation_CountryCode_CannotBeEmpty_Add_Test()
        {
            Mock<IDataRepository<CompanyLocationPoco>> moqRepo = new Mock<IDataRepository<CompanyLocationPoco>>();
            CompanyLocationLogic logic = new CompanyLocationLogic(moqRepo.Object);
            try
            {
                logic.Add(new CompanyLocationPoco[] { new CompanyLocationPoco()  });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 500));
            }
        }

        [TestMethod]
        public void CompanyLocation_CountryCode_CannotBeEmpty_Update_Test()
        {
            Mock<IDataRepository<CompanyLocationPoco>> moqRepo = new Mock<IDataRepository<CompanyLocationPoco>>();
            CompanyLocationLogic logic = new CompanyLocationLogic(moqRepo.Object);
            try
            {
                logic.Update(new CompanyLocationPoco[] { new CompanyLocationPoco() });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 500));
            }
        }


        [TestMethod]
        public void CompanyLocation_Province_CannotBeEmpty_Add_Test()
        {
            Mock<IDataRepository<CompanyLocationPoco>> moqRepo = new Mock<IDataRepository<CompanyLocationPoco>>();
            CompanyLocationLogic logic = new CompanyLocationLogic(moqRepo.Object);
            try
            {
                logic.Add(new CompanyLocationPoco[] { new CompanyLocationPoco() });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 501));
            }
        }

        [TestMethod]
        public void CompanyLocation_Province_CannotBeEmpty_Update_Test()
        {
            Mock<IDataRepository<CompanyLocationPoco>> moqRepo = new Mock<IDataRepository<CompanyLocationPoco>>();
            CompanyLocationLogic logic = new CompanyLocationLogic(moqRepo.Object);
            try
            {
                logic.Update(new CompanyLocationPoco[] { new CompanyLocationPoco() });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 501));
            }
        }

        [TestMethod]
        public void CompanyLocation_Street_CannotBeEmpty_Add_Test()
        {
            Mock<IDataRepository<CompanyLocationPoco>> moqRepo = new Mock<IDataRepository<CompanyLocationPoco>>();
            CompanyLocationLogic logic = new CompanyLocationLogic(moqRepo.Object);
            try
            {
                logic.Add(new CompanyLocationPoco[] { new CompanyLocationPoco() });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 502));
            }
        }

        [TestMethod]
        public void CompanyLocation_Street_CannotBeEmpty_Update_Test()
        {
            Mock<IDataRepository<CompanyLocationPoco>> moqRepo = new Mock<IDataRepository<CompanyLocationPoco>>();
            CompanyLocationLogic logic = new CompanyLocationLogic(moqRepo.Object);
            try
            {
                logic.Update(new CompanyLocationPoco[] { new CompanyLocationPoco() });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 502));
            }
        }

        [TestMethod]
        public void CompanyLocation_City_CannotBeEmpty_Add_Test()
        {
            Mock<IDataRepository<CompanyLocationPoco>> moqRepo = new Mock<IDataRepository<CompanyLocationPoco>>();
            CompanyLocationLogic logic = new CompanyLocationLogic(moqRepo.Object);
            try
            {
                logic.Add(new CompanyLocationPoco[] { new CompanyLocationPoco() });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 503));
            }
        }

        [TestMethod]
        public void CompanyLocation_City_CannotBeEmpty_Update_Test()
        {
            Mock<IDataRepository<CompanyLocationPoco>> moqRepo = new Mock<IDataRepository<CompanyLocationPoco>>();
            CompanyLocationLogic logic = new CompanyLocationLogic(moqRepo.Object);
            try
            {
                logic.Update(new CompanyLocationPoco[] { new CompanyLocationPoco() });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 503));
            }
        }

        [TestMethod]
        public void CompanyLocation_PostalCode_CannotBeEmpty_Add_Test()
        {
            Mock<IDataRepository<CompanyLocationPoco>> moqRepo = new Mock<IDataRepository<CompanyLocationPoco>>();
            CompanyLocationLogic logic = new CompanyLocationLogic(moqRepo.Object);
            try
            {
                logic.Add(new CompanyLocationPoco[] { new CompanyLocationPoco() });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 504));
            }
        }

        [TestMethod]
        public void CompanyLocation_PostalCode_CannotBeEmpty_Update_Test()
        {
            Mock<IDataRepository<CompanyLocationPoco>> moqRepo = new Mock<IDataRepository<CompanyLocationPoco>>();
            CompanyLocationLogic logic = new CompanyLocationLogic(moqRepo.Object);
            try
            {
                logic.Update(new CompanyLocationPoco[] { new CompanyLocationPoco() });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 504));
            }
        }
        #endregion CompanyLocation_Tests

        #region CompanyProfile_Tests
        [TestMethod]
        public void CompanyProfile_WebSite_Sufix_Add_Test()
        {
            Mock<IDataRepository<CompanyProfilePoco>> moqRepo = new Mock<IDataRepository<CompanyProfilePoco>>();
            CompanyProfileLogic logic = new CompanyProfileLogic(moqRepo.Object);
            try
            {
                logic.Add(new CompanyProfilePoco[] { new CompanyProfilePoco() { CompanyWebsite = "www.something.edu" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e )
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 600));
            }
        }

        [TestMethod]
        public void CompanyProfile_WebSite_Sufix_Update_Test()
        {
            Mock<IDataRepository<CompanyProfilePoco>> moqRepo = new Mock<IDataRepository<CompanyProfilePoco>>();
            CompanyProfileLogic logic = new CompanyProfileLogic(moqRepo.Object);
            try
            {
                logic.Update(new CompanyProfilePoco[] { new CompanyProfilePoco() { CompanyWebsite = "www.something.edu" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 600));
            }
        }

        [TestMethod]
        public void CompanyProfile_Phone_Format_Add_Test()
        {
            Mock<IDataRepository<CompanyProfilePoco>> moqRepo = new Mock<IDataRepository<CompanyProfilePoco>>();
            CompanyProfileLogic logic = new CompanyProfileLogic(moqRepo.Object);
            try
            {
                logic.Add(new CompanyProfilePoco[] { new CompanyProfilePoco() { } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 601));
            }
        }

        [TestMethod]
        public void CompanyProfile_Phone_Format_Update_Test()
        {
            Mock<IDataRepository<CompanyProfilePoco>> moqRepo = new Mock<IDataRepository<CompanyProfilePoco>>();
            CompanyProfileLogic logic = new CompanyProfileLogic(moqRepo.Object);
            try
            {
                logic.Update(new CompanyProfilePoco[] { new CompanyProfilePoco() { } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 601));
            }
        }

        [TestMethod]
        public void CompanyProfile_Phone_PrefixOnly_Format_Add_Test()
        {
            Mock<IDataRepository<CompanyProfilePoco>> moqRepo = new Mock<IDataRepository<CompanyProfilePoco>>();
            CompanyProfileLogic logic = new CompanyProfileLogic(moqRepo.Object);
            try
            {
                logic.Add(new CompanyProfilePoco[] { new CompanyProfilePoco() { ContactPhone = "416-" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 601));
            }
        }

        #endregion CompanyProfile_Tests

        #region SecurityLogins_Tests
        [TestMethod]
        public void SecurityLogins_PasswordRequired_Add_Test()
        {
            Mock<IDataRepository<SecurityLoginPoco>> moqRepo = new Mock<IDataRepository<SecurityLoginPoco>>();
            SecurityLoginLogic logic = new SecurityLoginLogic(moqRepo.Object);
            try
            {
                logic.Add(new SecurityLoginPoco[] { new SecurityLoginPoco() { Password = "" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 700));
            }
        }

        [TestMethod]
        public void SecurityLogins_PasswordExtendedCharacters_Add_Test()
        {
            Mock<IDataRepository<SecurityLoginPoco>> moqRepo = new Mock<IDataRepository<SecurityLoginPoco>>();
            SecurityLoginLogic logic = new SecurityLoginLogic(moqRepo.Object);
            try
            {
                logic.Add(new SecurityLoginPoco[] { new SecurityLoginPoco() { Password = "noextendedcharaters" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 701));
            }
        }

        [TestMethod]
        public void SecurityLogins_PhoneNumberIsNotEmpty_Add_Test()
        {
            Mock<IDataRepository<SecurityLoginPoco>> moqRepo = new Mock<IDataRepository<SecurityLoginPoco>>();
            SecurityLoginLogic logic = new SecurityLoginLogic(moqRepo.Object);
            try
            {
                logic.Add(new SecurityLoginPoco[] { new SecurityLoginPoco() {  } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 702));
            }
        }

        [TestMethod]
        public void SecurityLogins_PhoneNumberValidFormat_Add_Test()
        {
            Mock<IDataRepository<SecurityLoginPoco>> moqRepo = new Mock<IDataRepository<SecurityLoginPoco>>();
            SecurityLoginLogic logic = new SecurityLoginLogic(moqRepo.Object);
            try
            {
                logic.Add(new SecurityLoginPoco[] { new SecurityLoginPoco() { PhoneNumber = "416-" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 703));
            }
        }

        [TestMethod]
        public void SecurityLogins_EmailAddressValidFormat_Add_Test()
        {
            Mock<IDataRepository<SecurityLoginPoco>> moqRepo = new Mock<IDataRepository<SecurityLoginPoco>>();
            SecurityLoginLogic logic = new SecurityLoginLogic(moqRepo.Object);
            try
            {
                logic.Add(new SecurityLoginPoco[] { new SecurityLoginPoco() { EmailAddress = "notvalid" } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 704));
            }
        }

        [TestMethod]
        public void SecurityLogins_FullNameRequired_Add_Test()
        {
            Mock<IDataRepository<SecurityLoginPoco>> moqRepo = new Mock<IDataRepository<SecurityLoginPoco>>();
            SecurityLoginLogic logic = new SecurityLoginLogic(moqRepo.Object);
            try
            {
                logic.Add(new SecurityLoginPoco[] { new SecurityLoginPoco() {} });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 705));
            }
        }


        #endregion SecurityLogins_Tests

        #region SecurityRole_Tests
        [TestMethod]
        public void SecurityRole_RoleRequired_Add_Test()
        {
            Mock<IDataRepository<SecurityRolePoco>> moqRepo = new Mock<IDataRepository<SecurityRolePoco>>();
            SecurityRoleLogic logic = new SecurityRoleLogic(moqRepo.Object);
            try
            {
                logic.Add(new SecurityRolePoco[] { new SecurityRolePoco() { } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 800));
            }
        }

        [TestMethod]
        public void SecurityRole_RoleRequired_Update_Test()
        {
            Mock<IDataRepository<SecurityRolePoco>> moqRepo = new Mock<IDataRepository<SecurityRolePoco>>();
            SecurityRoleLogic logic = new SecurityRoleLogic(moqRepo.Object);
            try
            {
                logic.Update(new SecurityRolePoco[] { new SecurityRolePoco() { } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 800));
            }
        }

        #endregion SecurityRole_Tests

        #region SystemCountryCode_Tests

        [TestMethod]
        public void SystemCountryCode_MethodExists_Get_Test()
        {
            Type tCode = _types.Where(t => t.Name == "SystemCountryCodeLogic").FirstOrDefault();
            MemberInfo[] member = tCode.GetMember("Get");
            Assert.IsTrue(member.Count() > 0, "SystemCountryCodeLogic class is missing a Get method");
        }

        [TestMethod]
        public void SystemCountryCode_MethodExists_GetAll_Test()
        {
            Type tCode = _types.Where(t => t.Name == "SystemCountryCodeLogic").FirstOrDefault();
            MemberInfo[] member = tCode.GetMember("GetAll");
            Assert.IsTrue(member.Count() > 0, "SystemCountryCodeLogic class is missing a GetAll method");
        }

        [TestMethod]
        public void SystemCountryCode_MethodExists_Delete_Test()
        {
            Type tCode = _types.Where(t => t.Name == "SystemCountryCodeLogic").FirstOrDefault();
            MemberInfo[] member = tCode.GetMember("Delete");
            Assert.IsTrue(member.Count() > 0, "SystemCountryCodeLogic class is missing a Delete method");
        }

        [TestMethod]
        public void SystemCountryCode_CodeRequired_Add_Test()
        {
            Mock<IDataRepository<SystemCountryCodePoco>> moqRepo = new Mock<IDataRepository<SystemCountryCodePoco>>();
            SystemCountryCodeLogic logic = new SystemCountryCodeLogic(moqRepo.Object);
            try
            {
                logic.Add(new SystemCountryCodePoco[] { new SystemCountryCodePoco() { } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 900));
            }
        }

        [TestMethod]
        public void SystemCountryCode_CodeRequired_Update_Test()
        {
            Mock<IDataRepository<SystemCountryCodePoco>> moqRepo = new Mock<IDataRepository<SystemCountryCodePoco>>();
            SystemCountryCodeLogic logic = new SystemCountryCodeLogic(moqRepo.Object);
            try
            {
                logic.Update(new SystemCountryCodePoco[] { new SystemCountryCodePoco() { } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 900));
            }
        }

        [TestMethod]
        public void SystemCountryCode_NameRequired_Add_Test()
        {
            Mock<IDataRepository<SystemCountryCodePoco>> moqRepo = new Mock<IDataRepository<SystemCountryCodePoco>>();
            SystemCountryCodeLogic logic = new SystemCountryCodeLogic(moqRepo.Object);
            try
            {
                logic.Add(new SystemCountryCodePoco[] { new SystemCountryCodePoco() { } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 901));
            }
        }

        [TestMethod]
        public void SystemCountryCode_NameRequired_Update_Test()
        {
            Mock<IDataRepository<SystemCountryCodePoco>> moqRepo = new Mock<IDataRepository<SystemCountryCodePoco>>();
            SystemCountryCodeLogic logic = new SystemCountryCodeLogic(moqRepo.Object);
            try
            {
                logic.Update(new SystemCountryCodePoco[] { new SystemCountryCodePoco() { } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 901));
            }
        }
        #endregion SystemCountryCode_Tests

        #region SystemLanguageCode_Tests

        [TestMethod]
        public void SystemLanguageCode_MethodExists_Get_Test()
        {
            Type tCode = _types.Where(t => t.Name == "SystemLanguageCodeLogic").FirstOrDefault();
            MemberInfo[] member = tCode.GetMember("Get");
            Assert.IsTrue(member.Count() > 0, "SystemLanguageCodeLogic class is missing a Get method");
        }

        [TestMethod]
        public void SystemLanguageCode_MethodExists_GetAll_Test()
        {
            Type tCode = _types.Where(t => t.Name == "SystemLanguageCodeLogic").FirstOrDefault();
            MemberInfo[] member = tCode.GetMember("GetAll");
            Assert.IsTrue(member.Count() > 0, "SystemLanguageCodeLogic class is missing a GetAll method");
        }

        [TestMethod]
        public void SystemLanguageCode_MethodExists_Delete_Test()
        {
            Type tCode = _types.Where(t => t.Name == "SystemLanguageCodeLogic").FirstOrDefault();
            MemberInfo[] member = tCode.GetMember("Delete");
            Assert.IsTrue(member.Count() > 0, "SystemLanguageCodeLogic class is missing a Delete method");
        }

        [TestMethod]
        public void SystemLanguageCode_LanguageIdRequired_Add_Test()
        {
            Mock<IDataRepository<SystemLanguageCodePoco>> moqRepo = new Mock<IDataRepository<SystemLanguageCodePoco>>();
            SystemLanguageCodeLogic logic = new SystemLanguageCodeLogic(moqRepo.Object);
            try
            {
                logic.Add(new SystemLanguageCodePoco[] { new SystemLanguageCodePoco() { } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 1000));
            }
        }

        [TestMethod]
        public void SystemLanguageCode_LanguageIdRequired_Update_Test()
        {
            Mock<IDataRepository<SystemLanguageCodePoco>> moqRepo = new Mock<IDataRepository<SystemLanguageCodePoco>>();
            SystemLanguageCodeLogic logic = new SystemLanguageCodeLogic(moqRepo.Object);
            try
            {
                logic.Update(new SystemLanguageCodePoco[] { new SystemLanguageCodePoco() { } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 1000));
            }
        }

        [TestMethod]
        public void SystemLanguageCode_NameRequired_Add_Test()
        {
            Mock<IDataRepository<SystemLanguageCodePoco>> moqRepo = new Mock<IDataRepository<SystemLanguageCodePoco>>();
            SystemLanguageCodeLogic logic = new SystemLanguageCodeLogic(moqRepo.Object);
            try
            {
                logic.Add(new SystemLanguageCodePoco[] { new SystemLanguageCodePoco() { } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 1001));
            }
        }

        [TestMethod]
        public void SystemLanguageCode_NameRequired_Update_Test()
        {
            Mock<IDataRepository<SystemLanguageCodePoco>> moqRepo = new Mock<IDataRepository<SystemLanguageCodePoco>>();
            SystemLanguageCodeLogic logic = new SystemLanguageCodeLogic(moqRepo.Object);
            try
            {
                logic.Update(new SystemLanguageCodePoco[] { new SystemLanguageCodePoco() { } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 1001));
            }
        }
        [TestMethod]
        public void SystemLanguageCode_NativeNameRequired_Add_Test()
        {
            Mock<IDataRepository<SystemLanguageCodePoco>> moqRepo = new Mock<IDataRepository<SystemLanguageCodePoco>>();
            SystemLanguageCodeLogic logic = new SystemLanguageCodeLogic(moqRepo.Object);
            try
            {
                logic.Add(new SystemLanguageCodePoco[] { new SystemLanguageCodePoco() { } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 1002));
            }
        }

        [TestMethod]
        public void SystemLanguageCode_NativeNameRequired_Update_Test()
        {
            Mock<IDataRepository<SystemLanguageCodePoco>> moqRepo = new Mock<IDataRepository<SystemLanguageCodePoco>>();
            SystemLanguageCodeLogic logic = new SystemLanguageCodeLogic(moqRepo.Object);
            try
            {
                logic.Update(new SystemLanguageCodePoco[] { new SystemLanguageCodePoco() { } });
                Assert.Fail("No validaton exception generated");
            }
            catch (AggregateException e)
            {
                IEnumerable<ValidationException> exceptions = e.InnerExceptions.Cast<ValidationException>();
                Assert.IsTrue(exceptions.Any(ex => ex.Code == 1002));
            }
        }

        #endregion SystemLanguageCode_Tests

    }
}
