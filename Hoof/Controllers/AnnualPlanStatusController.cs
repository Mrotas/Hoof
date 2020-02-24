using System;
using System.Web.Mvc;
using Domain.AnnualPlanStatus;

namespace Hoof.Controllers
{
    public class AnnualPlanStatusController : Controller
    {
        private readonly IAnnualPlanStatusService _annualPlanStatusService;

        public AnnualPlanStatusController() : this(new AnnualPlanStatusService())
        {
        }

        public AnnualPlanStatusController(IAnnualPlanStatusService annualPlanStatusService)
        {
            _annualPlanStatusService = annualPlanStatusService;
        }

        [HttpPost]
        public JsonResult ApproveToNextStatus(int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _annualPlanStatusService.ApproveToNextStatus(marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RejectAnnualPlan(int marketingYearId)
        {
            string message = String.Empty;
            try
            {
                _annualPlanStatusService.RejectAnnualPlan(marketingYearId);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Json(message, JsonRequestBehavior.AllowGet);//RejectAnnualPlan
        }
    }
}