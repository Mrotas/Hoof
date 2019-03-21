﻿using System.Collections.Generic;
using Domain.Hunt.Models;
using Domain.Hunt.ViewModels;

namespace Domain.Hunt
{
    public interface IHuntService
    {
        IList<HuntViewModel> GetAllHunts();
        IList<HuntViewModel> GetByMarketingYearId(int marketingYearId);
        IList<HuntViewModel> GetHuntsByHuntsmanId(int huntsmanId);
        void Create(HuntCreateModel model, int huntsmanId);
    }
}