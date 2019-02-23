using System.Web.Mvc;
using Domain.EmploymentPlan;
using Domain.EmploymentPlan.ViewModels;

namespace Hoof.Controllers
{
    public class EmploymentPlanController : Controller
    {
        private readonly IEmploymentPlanService _employmentPlanService;

        public EmploymentPlanController() : this(new EmploymentPlanService())
        {
        }

        public EmploymentPlanController(IEmploymentPlanService employmentPlanService)
        {
            _employmentPlanService = employmentPlanService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Plan(int marketingYearId)
        {
            EmploymentPlanBaseViewModel employmentPlanBaseViewModel = _employmentPlanService.GetEmploymentPlanViewModel(marketingYearId);
            return View(employmentPlanBaseViewModel);
        }
    }
}