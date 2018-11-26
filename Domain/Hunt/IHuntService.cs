using System.Collections.Generic;
using Domain.Hunt.Models;
using Domain.Hunt.ViewModels;

namespace Domain.Hunt
{
    public interface IHuntService
    {
        IList<HuntViewModel> GetAllHuntModels();
        IList<HuntViewModel> GetHuntViewModels(int huntsmanId);
        void Create(HuntCreateModel model, int huntsmanId);
    }
}