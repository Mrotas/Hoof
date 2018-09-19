using System.Collections.Generic;
using System.Linq;
using DataAccess.Config;
using DataAccess.Entities;
using NUnit.Framework;

namespace DataAccess.Test.Entities
{
    [TestFixture]
    [Explicit("Integration tests")]
    public class HuntsmanTest
    {
        [Test]
        [Explicit("This test will impact on real Data")]
        public void GetData()
        {
            List<Huntsman> huntsman;
            using (var db = new DbContext())
            {
                huntsman = db.Huntsman.ToList();
            }

            Assert.That(huntsman, Is.Not.Empty);
        }

        [Test]
        [Explicit("This test will impact on real Data")]
        public void InsertData()
        {
            var huntsman = new Huntsman
            {
                Name = "Marian",
                LastName = "Buda",
                Rank = "Nadleśniczy"
            };

            Huntsman insertedHuntsman;
            using (var db = new DbContext())
            {
                db.Huntsman.Add(huntsman);
                db.SaveChanges();
                insertedHuntsman = db.Huntsman.Find(huntsman.Id);
            }

            Assert.That(insertedHuntsman, Is.Not.Null);
        }
    }
}
