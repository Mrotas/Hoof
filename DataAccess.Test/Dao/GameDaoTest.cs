using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using DataAccess.Dao.Game;
using DataAccess.Dto;
using NUnit.Framework;

namespace DataAccess.Test.Dao
{
    [TestFixture]
    public class GameDaoTest
    {
        private IGameDao _gameDao;

        [SetUp]
        public void SetUp()
        {
            _gameDao = new GameDao();
        }

        [Test]
        public void GetAllTest()
        {
            IList<GameDto> results = _gameDao.GetAll();

            Assert.That(results, Is.Not.Empty);
        }

        [Test]
        public void GetByTypeTest()
        {
            int type = (int) GameType.Big;

            IList<GameDto> results = _gameDao.GetByType(type);

            Assert.That(results, Is.Not.Empty);
            Assert.That(results.All(x => x.Type == type), Is.True);
        }

        [Test]
        public void GetByTypeKindAndSubKindTest()
        {
            int type = (int)GameType.Big;
            int kind = (int)GameKind.Deer;
            int subKind = 3;

            IList<GameDto> results = _gameDao.Get(type, kind, subKind);

            Assert.That(results, Is.Not.Empty);
        }
    }
}
