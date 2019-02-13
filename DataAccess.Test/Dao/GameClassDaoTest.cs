using System.Collections.Generic;
using DataAccess.Dao.GameClass;
using DataAccess.Dto;
using NUnit.Framework;

namespace DataAccess.Test.Dao
{
    [TestFixture]
    public class GameClassDaoTest
    {
        private IGameClassDao _gameClassDao;

        [SetUp]
        public void SetUp()
        {
            _gameClassDao = new GameClassDao();
        }

        [Test]
        public void GetAllTest()
        {
            IList<GameClassDto> results = _gameClassDao.GetAll();

            Assert.That(results, Is.Not.Empty);
        }
    }
}
