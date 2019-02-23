using DataAccess.Dto;
using System.Collections.Generic;

namespace DataAccess.Dao.HuntEquipmentPlan
{
    public interface IHuntEquipmentPlanDao
    {
        IList<HuntEquipmentPlanDto> GetByMarketingYear(int marketingYearId);
        void Insert(HuntEquipmentPlanDto dto);
        void Update(HuntEquipmentPlanDto dto);
    }
}