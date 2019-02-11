using DataAccess.Dao.Region;
using NUnit.Framework;

namespace DataAccess.Test.Dao
{
    [TestFixture]
    public class RegionDaoTest
    {
        private IRegionDao _regionDao;

        [SetUp]
        public void SetUp()
        {
            _regionDao = new RegionDao();
        }

        [Test]
        public void GetRegionIdTest()
        {
            string city = "Płytnica";
            int circuit = 20;
            int district = 10;

            int id = _regionDao.GetRegionId(city, circuit, district);
            
			Assert.That(id, Is.EqualTo(10));
        }
    }
}
