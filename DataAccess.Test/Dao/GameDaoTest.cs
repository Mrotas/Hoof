﻿using System.Collections.Generic;
using DataAccess.Dao.Game;
using DataAccess.Entities;
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
        public void GetTest()
        {
            IList<Game> games = _gameDao.GetAll();

            Assert.That(games, Is.Not.Empty);
        }
    }
}