using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpareManagement.DomainService;
using SpareManagement.DomainService.Entity;
using SpareManagement.Enum;
using SpareManagement.Helper;
using SpareManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpareManagement.Controllers
{
    [Authorize]

    public class ReturnController : BaseController
    {
        private readonly IReturnDomainService _returnDomainService;

        public ReturnController(IReturnDomainService returnDomainService,
            IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _returnDomainService = returnDomainService;
        }

        public IActionResult Index()
        {
            ViewData["Category"] = new SelectList(DefinitionHelper.GetInspectCategoryOption(), "TypeId", "TypeName");

            ViewData["Status"] = new SelectList(DefinitionHelper.GetReturnStatusOption(), "TypeId", "TypeName");

            return View();
        }

        [HttpGet]
        public IActionResult Search([FromQuery] int categoryId, string partNo, int status)
        {
            try
            {
                var res = _returnDomainService.SelectList(categoryId, partNo, status);

                if (!res.Any())
                    return Json("");

                List<InspectDetailViewModel> response = new List<InspectDetailViewModel>();

                res.ForEach(fe =>
                {
                    response.Add(new InspectDetailViewModel
                    {
                        PartNo = fe.PartNo,
                        Name = fe.Name,
                        SerialNo = fe.SerialNo,
                        Spec = fe.Spec,
                        Status = fe.Status.GetDescription(),
                        StatusId = (int)fe.Status,
                        PurchaseId = fe.PurchaseId,
                        CategoryId = categoryId
                    });
                });

                return PartialView("_Partial", new InspectSpareViewModel()
                {
                    Details = response
                });
            }
            catch (Exception ex)
            {
                return Json(new { IsException = true, msg = $"錯誤：{ex.Message}" });
            }
        }

        [HttpPatch]
        public IActionResult Update(InspectDetailViewModel updateViewModel)
        {
            try
            {
                var res = _returnDomainService.Update(new InspectsEntity
                {
                    PartNo = updateViewModel.PartNo,
                    SerialNo = updateViewModel.SerialNo,
                    CategoryId = updateViewModel.CategoryId,
                    Status = (StatusEnum)updateViewModel.StatusId,
                    UpdateUser = updateViewModel.UpdateUser,
                    UpdateDate = updateViewModel.UpdateDTE
                }, GetUserInfo());

                if (res != "")
                {
                    return Json(res);
                }
                return Json("");
            }
            catch (Exception ex)
            {
                return Json($"錯誤：{ex.Message}");
            }
        }
    }
}
