using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.GameClass
{
    public class GameClassDao : IGameClassDao
    {
        public IList<GameClassDto> GetAll()
        {
            using (var db = new DbContext())
            {
                List<Entities.GameClass> gameClasses = db.GameClass.ToList();

                IList<GameClassDto> dtos = ToDtos(gameClasses);

                return dtos;
            }
        }

        private IList<GameClassDto> ToDtos(List<Entities.GameClass> entityList)
        {
            var dtos = new List<GameClassDto>();
            foreach (Entities.GameClass entity in entityList)
            {
                var dto = new GameClassDto
                {
                    Id = entity.Id,
                    ClassName = entity.ClassName
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
