using System;
using System.Collections.Generic;
using Domain.Labor.Models;
using Domain.Labor.ViewModels;

namespace Domain.Labor
{
    public interface ILaborService
    {
        IList<LaborModel> GetLaborModels(DateTime fromDate, DateTime toDate);
        LaborBaseViewModel GetLaborViewModel(int marketingYearId);
        void AddLabor(LaborViewModel model, int marketingYearId);
        void UpdateLabor(LaborViewModel model, int marketingYearId);
        void DeleteLabor(int id);
    }
}