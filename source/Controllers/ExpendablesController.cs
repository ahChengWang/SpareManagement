using Microsoft.AspNetCore.Mvc;
using SpareManagement.DomainService;
using SpareManagement.Helper;
using SpareManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWeb.Controllers
{
    public class ExpendablesController : Controller
    {
        private readonly ExpendablesDomainService _expendablesDomainService;


        public ExpendablesController(ExpendablesDomainService expendablesDomainService)
        {
            _expendablesDomainService = expendablesDomainService;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Expendables([FromQuery] string partNo, string name, string purchaseId)
        {
            try
            {
                var res = _expendablesDomainService.Get(partNo, name, purchaseId);

                var response = new List<ExpendablesViewModel>();

                res.ForEach(fe =>
                {
                    response.Add(new ExpendablesViewModel
                    {
                        PartNo = fe.PartNo,
                        Name = fe.Name,
                        PurchaseId = fe.PurchaseId,
                        Spec = fe.Spec,
                        IsKeySpare = fe.IsKeySpare ? "是" : "否",
                        IsCommercial = fe.IsCommercial ? "是" : "否",
                        Placement = fe.Placement,
                        Inventory = fe.Inventory,
                        SafetyCount = fe.SafetyCount
                    });
                });

                ExpendablesListViewModel responseMain = new ExpendablesListViewModel { Details = response };

                if (!res.Any())
                    return Json("");

                return PartialView("_Partial", responseMain);
            }
            catch (Exception ex)
            {
                return Json(new { IsException = true, msg = $"錯誤：{ex.Message}" });
            }
        }
    }
}
