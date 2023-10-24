using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpareManagement.DomainService;
using SpareManagement.DomainService.Entity;
using SpareManagement.Helper;
using SpareManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpareManagement.Controllers
{
    [Authorize]

    public class SpareController : BaseController
    {
        private readonly IBasicInformationDomainService _basicInformationDomainService;

        public SpareController(IBasicInformationDomainService basicInformationDomainService,
            IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _basicInformationDomainService = basicInformationDomainService;
        }

        public IActionResult Index()
        {
            List<SpareType> _catOption = new List<SpareType>();
            _catOption.Add(new SpareType { TypeId = 0, TypeName = "" });
            _catOption.AddRange(DefinitionHelper.GetSpareType());

            ViewData["Category"] = new SelectList(_catOption, "TypeId", "TypeName");

            return View();
        }

        [HttpGet]
        public IActionResult Search(string categoryId, string partNo, string name, string purchaseId, string placement, string createTime)
        {
            try
            {
                var res = _basicInformationDomainService.Select(categoryId, partNo, name, purchaseId, placement, createTime);

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
                    UseLocation = string.Join(",", spareViewModel.UseLocation),
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

        [HttpGet]
        public IActionResult Edit([FromQuery] string partNo)
        {
            var res = _basicInformationDomainService.GetEditBasicInfo(new BasicInfoEntity { PartNo = partNo });

            ViewData["NodeList"] = DefinitionHelper.GetNodeList();

            return View(new SpareEditModel
            {
                PartNo = res.PartNo,
                Category = res.Category,
                SpareDesc = res.SpareDesc,
                Name = res.Name,
                Spec = res.Spec,
                PurchaseId = res.PurchaseId,
                IsSpecial = res.IsSpecial,
                IsKeySpare = res.IsKeySpare,
                IsCommercial = res.IsCommercial,
                Equipment = res.Equipment,
                UseLocation = res.UseLocation,
                PurchaseDelivery = res.PurchaseDelivery,
                SafetyCount = res.SafetyCount,
                IsNeedInspect = res.IsNeedInspect,
                InspectCycle = res.InspectCycle,
                Placement = res.Placement,
                CreateUser = res.CreateUser,
                Manager = res.Manager,
                Memo = res.Memo,
            });
        }


        [HttpPost]
        public IActionResult Edit([FromForm] SpareEditModel updModel)
        {
            try
            {
                var _res = _basicInformationDomainService.Update(new BasicInfoEntity
                {
                    PartNo = updModel.PartNo,
                    PurchaseId = updModel.PurchaseId,
                    SafetyCount = updModel.SafetyCount,
                    Placement = updModel.Placement,
                }, GetUserInfo());

                if (_res != "")
                {
                    return Json(_res);
                }

                return Json("");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }

    }
}
