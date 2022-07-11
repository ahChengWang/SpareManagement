using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpareManagement.DomainService;
using SpareManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWeb.Controllers
{
    [Authorize]

    public class WirePanelController : Controller
    {
        private readonly IWirePanelDomainService _wirePanelDomainService;


        public WirePanelController(IWirePanelDomainService wirePanelDomainService)
        {
            _wirePanelDomainService = wirePanelDomainService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult WirePanel([FromQuery] string partNo, string name, string purchaseId)
        {
            try
            {
                var res = _wirePanelDomainService.Get(partNo, name, purchaseId);

                if (!res.Any())
                    return Json("");

                List<WirePanelViewModel> detail = new List<WirePanelViewModel>();

                res.ForEach(fe =>
                {
                    detail.Add(new WirePanelViewModel
                    {
                        PartNo = fe.PartNo,
                        Name = fe.Name,
                        PurchaseId = fe.PurchaseId,
                        Spec = fe.Spec,
                        IsKeySpare = fe.IsKeySpare ? "是" : "否",
                        IsCommercial = fe.IsCommercial ? "是" : "否",
                        Equipment = fe.Equipment,
                        //Placement = fe.Placement,
                        Count = fe.Count,
                        Inventory = fe.Inventory,
                        SafetyCount = fe.SafetyCount,
                        Memo = fe.Memo,
                    });
                });

                WirePanelListViewModel response = new WirePanelListViewModel { Details = detail };

                return PartialView("_Partial", response);
            }
            catch (Exception ex)
            {
                return Json(new { IsException = true, msg = $"錯誤：{ex.Message}" });
            }
        }

        [HttpGet]
        public IActionResult Detail([FromQuery] string partNo)
        {
            try
            {
                var res = _wirePanelDomainService.GetDetail(partNo);

                if (!res.Any())
                    return Json("");

                List<WirePanelViewModel> detail = new List<WirePanelViewModel>();

                res.ForEach(fe =>
                {
                    detail.Add(new WirePanelViewModel
                    {
                        PartNo = fe.PartNo,
                        Name = fe.Name,
                        SerialNo = fe.SerialNo,
                        Status = fe.Status.GetDescription(),
                        StatusId = (int)fe.Status,
                        PurchaseId = fe.PurchaseId,
                        Spec = fe.Spec,
                        Placement = fe.Placement,
                        Inventory = fe.Inventory,
                        IsNeedInspect = fe.IsNeedInspect ? "是" : "否",
                        InspectDate = fe.InspectDate?.ToString("yyyy-MM-dd") ?? "",
                        NextInspectDate = fe.NextInspectDate?.ToString("yyyy-MM-dd") ?? "",
                        IsOverdueInspect = fe.IsOverdueInspect ? "是" : "否",
                        IsTemporary = fe.IsTemporary ? "是" : "否"
                    });
                });

                WirePanelListViewModel response = new WirePanelListViewModel { Details = detail };

                return View(response);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorViewModel { Message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Edit([FromQuery] string partNo, string SerialNo)
        {
            try
            {
                var res = _wirePanelDomainService.GetEditData(partNo: partNo, serialNo: SerialNo);

                WirePanelEditViewModel response = new WirePanelEditViewModel
                {
                    PartNo = res.PartNo,
                    SerialNo = res.SerialNo,
                    Name = res.Name,
                    InspectDate = res.InspectDate?.ToString("yyyy-MM-dd") ?? null,
                    Placement = res.Placement,
                    IsTemporary = res.IsTemporary
                };

                return View(response);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorViewModel
                {
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult Edit([FromForm] WirePanelEditViewModel wirePanelViewModel)
        {
            try
            {
                _wirePanelDomainService.UpdateInspectAndTemporary(
                wirePanelViewModel.PartNo,
                wirePanelViewModel.SerialNo,
                wirePanelViewModel.InspectDate,
                wirePanelViewModel.IsTemporary,
                wirePanelViewModel.Placement);

                return RedirectToAction("Detail", "WirePanel", new { partNo = wirePanelViewModel.PartNo });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorViewModel { Message = ex.Message });
            }
        }
    }
}
