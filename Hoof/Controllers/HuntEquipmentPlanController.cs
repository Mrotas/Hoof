using System.Web.Mvc;
using Domain.HuntEquipmentPlan;
using Domain.HuntEquipmentPlan.ViewModels;

namespace Hoof.Controllers
{
    public class HuntEquipmentPlanController : Controller
    {
        private readonly IHuntEquipmentPlanService _huntEquipmentPlanService;

        public HuntEquipmentPlanController(): this(new HuntEquipmentPlanService())
        {
        }

        public HuntEquipmentPlanController(IHuntEquipmentPlanService huntEquipmentPlanService)
        {
            _huntEquipmentPlanService = huntEquipmentPlanService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Plan(int marketingYearId)
        {
            HuntEquipmentPlanBaseViewModel huntEquipmentPlanBaseViewModel = _huntEquipmentPlanService.GetHuntEquipmentPlanViewModel(marketingYearId);
            return View(huntEquipmentPlanBaseViewModel);
        }
    }
}