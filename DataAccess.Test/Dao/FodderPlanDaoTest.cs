using System.Collections.Generic;
using DataAccess.Dao.FodderPlan;
using DataAccess.Dto;
using NUnit.Framework;

namespace DataAccess.Test.Dao
{
    [TestFixture]
    public class FodderPlanDaoTest
    {
        private IFodderPlanDao _fodderPlanDao;

        [SetUp]
        public void SetUp()
        {
            _fodderPlanDao = new FodderPlanDao();
        }

        [Test]
        public void GetByMarketingYearTest()
        {
            int marketingYearId = 3;

            IList<FodderPlanDto> results = _fodderPlanDao.GetByMarketingYear(marketingYearId);

            Assert.That(results, Is.Not.Empty);
        }
    }
}
