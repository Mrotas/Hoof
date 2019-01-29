using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Entities;
using NUnit.Framework;

namespace DataAccess.Test.Entities
{
    [TestFixture]
    [Explicit("Integration tests")]
    public class GameHuntPlanTest
    {
        [Test]
        [Explicit("This test will impact on real Data")]
        public void GetData()
        {
            List<GameHuntPlan> gamesPlan;
            using (var db = new DbContext())
            {
                gamesPlan = db.GameHuntPlan.ToList();
            }

            Assert.That(gamesPlan, Is.Not.Null);
            Assert.That(gamesPlan[0].Id, Is.EqualTo(1));
            Assert.That(gamesPlan[0].GameId, Is.EqualTo(1));
            Assert.That(gamesPlan[0].Class, Is.EqualTo(null));
            Assert.That(gamesPlan[0].Cull, Is.EqualTo(0));
            Assert.That(gamesPlan[0].Catch, Is.EqualTo(0));
            Assert.That(gamesPlan[0].MarketingYearId, Is.EqualTo(2017));
        }
    }
}
