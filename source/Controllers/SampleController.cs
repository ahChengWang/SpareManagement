using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpareManagement.DomainService;
using SpareManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpareManagement.Controllers
{
    [Authorize]

    public class SampleController : Controller
    {
        private readonly ISampleDomainService _sampleDomainService;


        public SampleController(ISampleDomainService sampleDomainService)
        {
            _sampleDomainService = sampleDomainService;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Search([FromQuery] string partNo, string name, string purchaseId)
        {
            try
            {
                var res = _sampleDomainService.Get(partNo, name, purchaseId);

                if (!res.Any())
                    return Json("");

                List<SampleDetailViewModel> detail = new List<SampleDetailViewModel>();

                res.ForEach(fe =>
                {
                    detail.Add(new SampleDetailViewModel
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

                SampleViewModel response = new SampleViewModel { Details = detail };

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
                var res = _sampleDomainService.GetDetail(partNo);

                List<SampleDetailViewModel> detail = new List<SampleDetailViewModel>();

                res.ForEach(fe =>
                {
                    detail.Add(new SampleDetailViewModel
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

                SampleViewModel response = new SampleViewModel { Details = detail };

                if (!res.Any())
                    return Json("");

                return View(response);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorViewModel { Message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Edit([FromQuery] string partNo, string serialNo)
        {
            try
            {
                var res = _sampleDomainService.GetEditData(partNo, serialNo);

                SampleEditViewModel response = new SampleEditViewModel
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
                return RedirectToAction("Error", "Home", new ErrorViewModel { Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Edit([FromForm] SampleEditViewModel sampleViewModel)
        {
            try
            {
                var _result = _sampleDomainService.UpdateInspectAndTemporary(
                    sampleViewModel.PartNo, sampleViewModel.SerialNo, sampleViewModel.InspectDate, sampleViewModel.IsTemporary, sampleViewModel.Placement);

                return RedirectToAction("Detail", "Sample", new { partNo = sampleViewModel.PartNo });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorViewModel { Message = ex.Message });
            }
        }
    }
}
