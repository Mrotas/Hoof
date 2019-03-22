using System.Collections.Generic;
using Domain.Huntsman.Models;

namespace Domain.Huntsman
{
    public interface IHuntsmanService
    {
        IList<HuntsmanModel> GetAll();
    }
}