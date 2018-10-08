using System;
using System.Collections.Generic;
using DataAccess.Context;
using DataAccess.Dao.Hunt;
using DataAccess.Entities;
using NUnit.Framework;

namespace DataAccess.Test.Dao
{
    [TestFixture]
    [Explicit("Integration tests")]
    public class HuntDaoTest
    {
        private IHuntDao _huntDao;

        [SetUp]
        public void SetUp()
        {
            _huntDao = new HuntDao();
        }

        [Test]
        [Explicit("This test will impact on real Data")]
        public void GetData()
        {
            IList<Hunt> result = _huntDao.GetAll();

            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        [Explicit("This test will impact on real Data")]
        public void InsertData()
        {
            var hunt = new Hunt
            {
                HuntsmanId = 1,
                GameId = 1,
                RegionId = 1,
                Date = DateTime.Now,
                Shots = 1
            };

            Hunt insertedHunt;
            using (var db = new DbContext())
            {
                db.Hunt.Add(hunt);
                insertedHunt = db.Hunt.Find(hunt.Id);
            }

            Assert.That(insertedHunt, Is.Not.Null);
            Assert.That(insertedHunt.Id, Is.EqualTo(hunt.Id));
            Assert.That(insertedHunt.HuntsmanId, Is.EqualTo(hunt.HuntsmanId));
            Assert.That(insertedHunt.GameId, Is.EqualTo(hunt.GameId));
            Assert.That(insertedHunt.RegionId, Is.EqualTo(hunt.RegionId));
            Assert.That(insertedHunt.Date, Is.EqualTo(hunt.Date));
            Assert.That(insertedHunt.Shots, Is.EqualTo(hunt.Shots));
        }
    }
}
