using DataAccess.Config;

namespace DataAccess.Dao
{
    public class DaoBase
    {
        protected DbContext DbContext;
        public DaoBase()
        {
            DbContext = new DbContext();
        }
    }
}
