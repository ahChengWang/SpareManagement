using Helper;
using Microsoft.AspNetCore.Mvc;
using SpareManagement.DomainService;
using SpareManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWeb.Controllers
{
    public class JigsController : Controller
    {
        private readonly JigsDomainService _jigsDomainService;


        public JigsController(JigsDomainService jigsDomainService)
        {
            _jigsDomainService = jigsDomainService;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Jigs([FromQuery] string partNo, string name, string purchaseId)
        {
            try
            {
                var res = _jigsDomainService.Get(partNo, name, purchaseId);

                if (!res.Any())
                    return Json("");

                List<JigsViewModel> detail = new List<JigsViewModel>();

                res.ForEach(fe =>
                {
                    detail.Add(new JigsViewModel
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

                JigsListViewModel response = new JigsListViewModel { Details = detail };

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
                var res = _jigsDomainService.GetDetail(partNo);

                List<JigsViewModel> detail = new List<JigsViewModel>();

                res.ForEach(fe =>
                {
                    detail.Add(new JigsViewModel
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

                JigsListViewModel response = new JigsListViewModel { Details = detail };

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
                var res = _jigsDomainService.GetEditData(partNo, serialNo);

                JigsEditViewModel response = new JigsEditViewModel
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
        public IActionResult Edit([FromForm] JigsEditViewModel jigsViewModel)
        {
            try
            {
                var _result = _jigsDomainService.UpdateInspectAndTemporary(
                    jigsViewModel.PartNo, jigsViewModel.SerialNo, jigsViewModel.InspectDate, jigsViewModel.IsTemporary, jigsViewModel.Placement);

                return RedirectToAction("Detail", "Jigs", new { partNo = jigsViewModel.PartNo });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorViewModel { Message = ex.Message });
            }
        }
    }
}
