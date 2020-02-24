using System.Collections.Generic;
using Domain.Catch.Models;
using Domain.Catch.ViewModels;

namespace Domain.Catch
{
    public interface ICatchService
    {
        IList<CatchViewModel> GetAllCatchesForCurrentMarketingYear();
        IList<CatchViewModel> GetCurrentMarketingYearCatchesByUserId(int huntsmanId);
        void Create(CatchCreateModel model, int huntsmanId);
    }
}