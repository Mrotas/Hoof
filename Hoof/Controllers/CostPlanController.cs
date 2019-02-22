using System.Web.Mvc;
using Domain.CostPlan;
using Domain.CostPlan.ViewModels;

namespace Hoof.Controllers
{
    public class CostPlanController : Controller
    {
        private readonly ICostPlanService _costPlanService;

        public CostPlanController() : this(new CostPlanService())
        {
        }

        public CostPlanController(ICostPlanService costPlanService)
        {
            _costPlanService = costPlanService;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Plan(int marketingYearId)
        {
            CostPlanBaseViewModel costPlanBaseViewModel = _costPlanService.GetCostPlanViewModel(marketingYearId);
            return View(costPlanBaseViewModel);
        }
    }
}