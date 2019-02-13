using System.Collections.Generic;
using DataAccess.Dao.CostPlan;
using DataAccess.Dto;
using NUnit.Framework;

namespace DataAccess.Test.Dao
{
    [TestFixture]
    public class CostPlanDaoTest
    {
        private ICostPlanDao _costPlanDao;

        [SetUp]
        public void SetUp()
        {
            _costPlanDao = new CostPlanDao();
        }

        [Test]
        public void GetByMarketingYearTest()
        {
            int marketingYearId = 3;

            IList<CostPlanDto> results = _costPlanDao.GetByMarketingYear(marketingYearId);

            Assert.That(results, Is.Not.Empty);
        }
    }
}
