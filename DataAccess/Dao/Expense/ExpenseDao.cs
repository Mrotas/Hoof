using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Dto;

namespace DataAccess.Dao.Expense
{
    public class ExpenseDao : DaoBase, IExpenseDao
    {
        public IList<ExpenseDto> GetByMarketingYear(int marketingYearId)
        {
            using (var db = new DbContext())
            {
                Entities.MarketingYear marketingYear = DbContext.MarketingYear.Find(marketingYearId);
                List<Entities.Expense> expenses = db.Expense.Where(x => x.Date >= marketingYear.Start && x.Date <= marketingYear.End).ToList();

                IList<ExpenseDto> dtos = ToDtos(expenses);

                return dtos;
            }
        }

        public void Insert(ExpenseDto dto)
        {
            var entity = new Entities.Expense
            {
                Cost = dto.Cost,
                Description = dto.Description,
                Note = dto.Note,
                Date = dto.Date
            };

            using (var db = new DbContext())
            {
                db.Expense.Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(ExpenseDto dto)
        {
            using (var db = new DbContext())
            {
                Entities.Expense expense = db.Expense.Single(x => x.Id == dto.Id);

                expense.Cost = dto.Cost;
                expense.Description = dto.Description;
                expense.Note = dto.Note;
                expense.Date = dto.Date;

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new DbContext())
            {
                Entities.Expense expense = db.Expense.Single(x => x.Id == id);

                db.Expense.Remove(expense);
                db.SaveChanges();
            }
        }

        private IList<ExpenseDto> ToDtos(IList<Entities.Expense> entityList)
        {
            var dtos = new List<ExpenseDto>();
            foreach (Entities.Expense entity in entityList)
            {
                var dto = new ExpenseDto
                {
                    Id = entity.Id,
                    Cost = entity.Cost,
                    Description = entity.Description,
                    Note = entity.Note,
                    Date = entity.Date
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
