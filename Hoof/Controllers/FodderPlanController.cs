using System.Web.Mvc;
using Domain.FodderPlan;
using Domain.FodderPlan.ViewModels;

namespace Hoof.Controllers
{
    public class FodderPlanController : Controller
    {
        private readonly IFodderPlanService _fodderPlanService;

        public FodderPlanController() : this(new FodderPlanService())
        {
        }

        public FodderPlanController(IFodderPlanService fodderPlanService)
        {
            _fodderPlanService = fodderPlanService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Plan(int marketingYearId)
        {
            FodderPlanBaseViewModel fodderPlanViewBaseModel = _fodderPlanService.GetFodderPlanViewModel(marketingYearId);
            return View(fodderPlanViewBaseModel);
        }
    }
}