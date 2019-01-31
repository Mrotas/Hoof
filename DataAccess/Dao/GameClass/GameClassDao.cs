using System.Collections.Generic;
using System.Linq;
using DataAccess.Dto;

namespace DataAccess.Dao.GameClass
{
    public class GameClassDao : DaoBase, IGameClassDao
    {
        public IList<GameClassDto> GetAll()
        {
            using (DbContext)
            {
                List<Entities.GameClass> gameClasses = DbContext.GameClass.ToList();

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
