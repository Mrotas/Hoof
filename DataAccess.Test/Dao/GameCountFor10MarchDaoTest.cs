using System.Collections.Generic;
using DataAccess.Dao.GameCountFor10March;
using DataAccess.Dto;
using NUnit.Framework;

namespace DataAccess.Test.Dao
{
    [TestFixture]
    public class GameCountFor10MarchDaoTest
    {
        private IGameCountFor10MarchDao _countFor10MarchDao;

        [SetUp]
        public void SetUp()
        {
            _countFor10MarchDao = new GameCountFor10MarchDao();
        }
        
        [Test]
        public void GetByMarketingYearTest()
        {
            int marketingYearId = 2;

            IList<GameCountFor10MarchDto> results = _countFor10MarchDao.GetByMarketingYear(marketingYearId);

            Assert.That(results, Is.Not.Empty);
        }
    }
}
