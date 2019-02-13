using System.Collections.Generic;
using DataAccess.Dao.FieldPlan;
using DataAccess.Dto;
using NUnit.Framework;

namespace DataAccess.Test.Dao
{
    [TestFixture]
    public class FieldPlanDaoTest
    {
        private IFieldPlanDao _fieldPlanDao;

        [SetUp]
        public void SetUp()
        {
            _fieldPlanDao = new FieldPlanDao();
        }

        [Test]
        public void GetByMarketingYearTest()
        {
            int marketingYearId = 3;

            IList<FieldPlanDto> results = _fieldPlanDao.GetByMarketingYear(marketingYearId);

            Assert.That(results, Is.Not.Empty);
        }

    }
}
