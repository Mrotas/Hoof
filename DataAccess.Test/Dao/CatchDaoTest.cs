using System;
using System.Collections.Generic;
using DataAccess.Dao.Catch;
using DataAccess.Dto;
using NUnit.Framework;

namespace DataAccess.Test.Dao
{
    [TestFixture]
    public class CatchDaoTest
    {
        private ICatchDao _catchDao;

        [SetUp]
        public void SetUp()
        {
            _catchDao = new CatchDao();
        }

        [Test]
        public void GetAllTest()
        {
            IList<CatchDto> results = _catchDao.GetAll();

            Assert.That(results, Is.Not.Empty);
        }

        [Test]
        public void GetByMarketingYearTest()
        {
            int marketingYearId = 1;

            IList<CatchDto> results = _catchDao.GetByMarketingYear(marketingYearId);

            Assert.That(results, Is.Not.Empty);
        }

        [Test]
        public void GetByDateRangeTest()
        {
            DateTime fromDate = DateTime.Now.AddYears(-3);
            DateTime toDate = DateTime.Now.AddYears(-2);

            IList<CatchDto> results = _catchDao.GetByDateRange(fromDate, toDate);

            Assert.That(results, Is.Not.Empty);
        }
    }
}
