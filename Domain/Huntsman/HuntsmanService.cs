using System.Collections.Generic;
using System.Linq;
using DataAccess.Dao.Huntsman;
using DataAccess.Dto;
using Domain.Huntsman.Models;

namespace Domain.Huntsman
{
    public class HuntsmanService : IHuntsmanService
    {
        private readonly IHuntsmanDao _huntsmanDao;

        public HuntsmanService() : this(new HuntsmanDao())
        {
        }

        public HuntsmanService(IHuntsmanDao huntsmanDao)
        {
            _huntsmanDao = huntsmanDao;
        }

        public IList<HuntsmanModel> GetAll()
        {
            IList<HuntsmanDto> huntsmanDtos = _huntsmanDao.GetAll();

            List<HuntsmanModel> huntsmanModels = huntsmanDtos.Select(x => new HuntsmanModel
            {
                Id = x.Id,
                Name = x.Name,
                LastName = x.LastName,
                Role = x.Role
            }).ToList();

            return huntsmanModels;
        }
    }
}
