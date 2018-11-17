using System.Collections.Generic;
using Domain.Hunt.ViewModels;

namespace Domain.Hunt
{
    public interface IHuntService
    {
        IList<HuntViewModel> GetAllHuntModels();
        IList<HuntViewModel> GetHuntViewModels(int huntsmanId);
    }
}