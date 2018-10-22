using System.Collections.Generic;
using DataAccess.Entities.AnnualPlan;

namespace DataAccess.Dao.AnnualPlan.HuntEquipment
{
    public interface IHuntEquipmentPlanDao
    {
        List<HuntEquipmentPlan> GetHuntEquipmentPlan(int year);
    }
}