using DataAccess.Dto;
using System.Collections.Generic;

namespace DataAccess.Dao.HuntEquipmentPlan
{
    public interface IHuntEquipmentPlanDao
    {
        IList<HuntEquipmentPlanDto> GetHuntEquipmentPlan(int marketingYearId);
    }
}