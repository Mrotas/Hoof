using System.Collections.Generic;
using DataAccess.Dao.EmploymentPlan;
using DataAccess.Dto;
using NUnit.Framework;

namespace DataAccess.Test.Dao
{
    [TestFixture]
    public class EmploymentPlanDaoTest
    {
        private IEmploymentPlanDao _employmentPlanDao;

        [SetUp]
        public void SetUp()
        {
            _employmentPlanDao = new EmploymentPlanDao();
        }

        [Test]
        public void GetByMarketingYearTest()
        {
            int marketingYearId = 3;

            IList<EmploymentPlanDto> results = _employmentPlanDao.GetByMarketingYear(marketingYearId);

            Assert.That(results, Is.Not.Empty);
        }
    }
}
