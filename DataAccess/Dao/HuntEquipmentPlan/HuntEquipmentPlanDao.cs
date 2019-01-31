﻿using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.HuntEquipmentPlan
{
    public class HuntEquipmentPlanDao : DaoBase, IHuntEquipmentPlanDao
    {
        public IList<HuntEquipmentPlanDto> GetHuntEquipmentPlan(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                List<Entities.HuntEquipmentPlan> huntEquipmentPlan = db.HuntEquipmentPlan.Where(x => x.MarketingYearId == marketingYearId).ToList();

                IList<HuntEquipmentPlanDto> dtos = ToDtos(huntEquipmentPlan);

                return dtos;
            }
        }

        private IList<HuntEquipmentPlanDto> ToDtos(IList<Entities.HuntEquipmentPlan> entityList)
        {
            var dtos = new List<HuntEquipmentPlanDto>();
            foreach (Entities.HuntEquipmentPlan entity in entityList)
            {
                var dto = new HuntEquipmentPlanDto
                {
                    Id = entity.Id,
                    Type = entity.Type,
                    Count = entity.Count,
                    MarketingYearId = entity.MarketingYearId
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}