using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Dao.HuntEquipment
{
    public interface IHuntEquipmentPlanDao
    {
        List<HuntEquipmentPlan> GetHuntEquipmentPlan(int year);
    }
}