using System.Collections.Generic;
using DataAccess.Dto;

namespace DataAccess.Dao.Loss
{
    public interface ILossDao
    {
        IList<LossDto> GetAll();
        IList<LossDto> GetByMarketingYear(int marketingYearId);
        void Insert(LossGameDto lossGameDto, LossDto lossDto);
    }
}