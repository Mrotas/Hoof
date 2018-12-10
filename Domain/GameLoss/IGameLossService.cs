using System.Collections.Generic;
using Domain.GameLoss.Model;
using Domain.GameLoss.ViewModel;

namespace Domain.GameLoss
{
    public interface IGameLossService
    {
        List<GameLossViewModel> GetAllLossGames();
        void ReportLoss(GameLossModel model);
    }
}