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
        public void GetTest()
        {
            int id = _regionDao.GetRegionId("Płytnica", 20, 10);
            
			Assert.That(id, Is.EqualTo(10));
        }
    }
}
