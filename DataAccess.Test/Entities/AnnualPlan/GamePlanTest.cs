using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Entities.AnnualPlan;
using NUnit.Framework;

namespace DataAccess.Test.Entities.AnnualPlan
{
    [TestFixture]
    [Explicit("Integration tests")]
    public class GamePlanTest
    {
        [Test]
        [Explicit("This test will impact on real Data")]
        public void GetData()
        {
            List<GamePlan> gamesPlan;
            using (var db = new DbContext())
            {
                gamesPlan = db.GamePlan.ToList();
            }

            Assert.That(gamesPlan, Is.Not.Null);
            Assert.That(gamesPlan[0].Id, Is.EqualTo(1));
            Assert.That(gamesPlan[0].Type, Is.EqualTo(4));
            Assert.That(gamesPlan[0].SubType, Is.EqualTo(11));
            Assert.That(gamesPlan[0].Class, Is.EqualTo(null));
            Assert.That(gamesPlan[0].Cull, Is.EqualTo(0));
            Assert.That(gamesPlan[0].Catch, Is.EqualTo(0));
            Assert.That(gamesPlan[0].Year, Is.EqualTo(2017));
        }
    }
}
