using System.Collections.Generic;
using DataAccess.Dao.GameHuntPlan;
using DataAccess.Dto;
using NUnit.Framework;

namespace DataAccess.Test.Dao
{
    [TestFixture]
    public class GameHuntPlanDaoTest
    {
        private IGameHuntPlanDao _gameHuntPlanDao;

        [SetUp]
        public void SetUp()
        {
            _gameHuntPlanDao = new GameHuntPlanDao();
        }

        [Test]
        public void GetAllTest()
        {
            IList<GameHuntPlanDto> results = _gameHuntPlanDao.GetAll();

            Assert.That(results, Is.Not.Empty);
        }

        [Test]
        public void GetByMarketingYearTest()
        {
            int marketingYearId = 3;

            IList<GameHuntPlanDto> results = _gameHuntPlanDao.GetByMarketingYear(marketingYearId);

            Assert.That(results, Is.Not.Empty);
        }
    }
}
