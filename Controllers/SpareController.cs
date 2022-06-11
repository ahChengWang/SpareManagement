using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpareManagement.DomainService;
using SpareManagement.DomainService.Entity;
using SpareManagement.Helper;
using SpareManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWeb.Controllers
{
    public class SpareController : Controller
    {
        private readonly BasicInformationDomainService _basicInformationDomainService;

        public SpareController(BasicInformationDomainService basicInformationDomainService)
        {
            _basicInformationDomainService = basicInformationDomainService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(string partNo, string name, string purchaseId, string placement, string createTime)
        {
            try
            {
                var res = _basicInformationDomainService.Select(partNo, name, purchaseId, placement, createTime);

                if (!res.Any())
                    return Json("");

                var _detail = new List<BasicInfoDetailViewModel>();

                res.ForEach(fe =>
                {
                    _detail.Add(new BasicInfoDetailViewModel
                    {
                        PartNo = fe.PartNo,
                        Name = fe.Name,
                        PurchaseId = fe.PurchaseId,
                        Placement = fe.Placement,
                        CreateDate = fe.CreateDate.ToString("yyyy/MM/dd")
                    });
                });

                return PartialView("_Partial", new BasicInfoViewModel()
                {
                    Details = _detail
                });
            }
            catch (Exception ex)
            {
                return Json(new { IsException = true, msg = $"錯誤：{ex.Message}" });
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                ViewData["SpareType"] = new SelectList(DefinitionHelper.GetSpareType(), "TypeId", "TypeName");

                ViewData["NodeList"] = DefinitionHelper.GetNodeList();

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorViewModel { Message = ex.Message });
            }

        }

        [HttpPost]
        public IActionResult Create([FromForm] SpareViewModel spareViewModel)
        {
            try
            {
                var result = _basicInformationDomainService.Create(new BasicInfoEntity
                {
                    CategoryId = spareViewModel.CategoryId,
                    Name = spareViewModel.Name,
                    Spec = spareViewModel.Spec,
                    PurchaseId = spareViewModel.PurchaseId,
                    IsSpecial = spareViewModel.IsSpecial,
                    IsKeySpare = spareViewModel.IsKeySpare,
                    IsCommercial = spareViewModel.IsCommercial,
                    Equipment = spareViewModel.Equipment,
                    UseLocation = spareViewModel.UseLocation,
                    PurchaseDelivery = spareViewModel.PurchaseDelivery,
                    SafetyCount = spareViewModel.SafetyCount,
                    IsNeedInspect = spareViewModel.IsNeedInspect,
                    //InspectDate = spareViewModel.InspectDate,
                    InspectCycle = spareViewModel.InspectCycle,
                    Placement = spareViewModel.Placement,
                    CreateUser = spareViewModel.CreateUser,
                    Manager = spareViewModel.Manager,
                    Memo = spareViewModel.Memo
                });

                if (result != "")
                {
                    return Json(result);
                }

                return Json("");
                //return RedirectToAction("Index", "");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

    }
}
