using System.Collections.Generic;
using Domain.Catch.Models;
using Domain.Catch.ViewModels;

namespace Domain.Catch
{
    public interface ICatchService
    {
        IList<CatchViewModel> GetAllCatches();
        IList<CatchViewModel> GetCatchesByHuntsmanId(int huntsmanId);
        void Create(CatchCreateModel model, int huntsmanId);
    }
}