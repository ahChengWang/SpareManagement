using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpareManagement.DomainService;
using SpareManagement.Helper;
using SpareManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpareManagement.Controllers
{
    [Authorize]

    public class ComponentsController : Controller
    {
        private readonly IComponentsDomainService _componentsDomainService;


        public ComponentsController(IComponentsDomainService componentsDomainService)
        {
            _componentsDomainService = componentsDomainService;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Components([FromQuery] string partNo, string name, string purchaseId)
        {
            try
            {
                var res = _componentsDomainService.Get(partNo, name, purchaseId);

                var tmp = new List<ComponentsViewModel>();

                res.ForEach(fe =>
                {
                    tmp.Add(new ComponentsViewModel
                    {
                        PartNo = fe.PartNo,
                        Name = fe.Name,
                        PurchaseId = fe.PurchaseId,
                        Spec = fe.Spec,
                        IsKeySpare = fe.IsKeySpare ? "是" : "否",
                        IsCommercial = fe.IsCommercial ? "是" : "否",
                        Equipment = fe.Equipment,
                        Placement = fe.Placement,
                        Inventory = fe.Inventory,
                        SafetyCount = fe.SafetyCount
                    });
                });

                ComponentsListViewModel response = new ComponentsListViewModel { Details = tmp };

                if (!res.Any())
                    return Json("");

                return PartialView("_Partial", response);
            }
            catch (Exception ex)
            {
                return Json(new { IsException = true, msg = $"錯誤：{ex.Message}" });
            }
        }
    }
}
