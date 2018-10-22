using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Entities.AnnualPlan;

namespace DataAccess.Dao.AnnualPlan.HuntEquipment
{
    public class HuntEquipmentPlanDao : DaoBase, IHuntEquipmentPlanDao
    {
        public List<HuntEquipmentPlan> GetHuntEquipmentPlan(int year)
        {
            var huntEquipmentPlan = new List<HuntEquipmentPlan>();
            using (var db = new DbContext())
            {
                huntEquipmentPlan = db.HuntEquipmentPlan.Where(x => x.Year == year).ToList();
            }

            return huntEquipmentPlan;
        }
    }
}
