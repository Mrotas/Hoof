using System;
using System.Collections.Generic;
using DataAccess.Context;
using DataAccess.Dao.Hunt;
using DataAccess.Dto;
using DataAccess.Entities;
using NUnit.Framework;

namespace DataAccess.Test.Dao
{
    [TestFixture]
    public class HuntDaoTest
    {
        private IHuntDao _huntDao;

        [SetUp]
        public void SetUp()
        {
            _huntDao = new HuntDao();
        }

        [Test]
        public void GetAllTest()
        {
            IList<HuntDto> results = _huntDao.GetAll();

            Assert.That(results, Is.Not.Empty);
        }

        [Test]
        public void GetByMarketingYearTest()
        {
            int marketingYearId = 2;

            IList<HuntDto> results = _huntDao.GetByMarketingYear(marketingYearId);

            Assert.That(results, Is.Not.Empty);
        }

        [Test]
        public void GetHuntsByDateRangeTest()
        {
            DateTime from = DateTime.Now.AddYears(-2);
            DateTime to = DateTime.Now;

            IList<HuntDto> results = _huntDao.GetHuntsByDateRange(from, to);

            Assert.That(results, Is.Not.Empty);
        }

        [Test]
        [Explicit("Integration test")]
        public void InsertData()
        {
            var hunt = new Hunt
            {
                HuntsmanId = 1,
                HuntedGameId = 1,
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
            Assert.That(insertedHunt.HuntedGameId, Is.EqualTo(hunt.HuntedGameId));
            Assert.That(insertedHunt.RegionId, Is.EqualTo(hunt.RegionId));
            Assert.That(insertedHunt.Date, Is.EqualTo(hunt.Date));
            Assert.That(insertedHunt.Shots, Is.EqualTo(hunt.Shots));
        }
    }
}
