using Microsoft.AspNetCore.Authorization;
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

    public class InspectController : Controller
    {
        private readonly IInspectDomainService _inspectDomainService;

        public InspectController(IInspectDomainService inspectDomainService)
        {
            _inspectDomainService = inspectDomainService;
        }

        public IActionResult Index()
        {
            ViewData["Category"] = new SelectList(DefinitionHelper.GetInspectCategoryOption(), "TypeId", "TypeName");

            ViewData["Overdue"] = new SelectList(DefinitionHelper.GetInspectOption(), "TypeId", "TypeName");

            return View();
        }

        [HttpGet]
        public IActionResult Search([FromQuery] int categoryId, string partNo, int isOverdueInspect)
        {
            try
            {
                var res = _inspectDomainService.SelectList(categoryId, partNo, isOverdueInspect);

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
                        NextInspectDate = fe.NextInspectDate.ToString("yyyy-MM-dd")
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
                var res = _inspectDomainService.Update(new InspectsEntity
                {
                    PartNo = updateViewModel.PartNo,
                    SerialNo = updateViewModel.SerialNo,
                    CategoryId = updateViewModel.CategoryId,
                    UpdateUser = updateViewModel.UpdateUser,
                    UpdateDate = updateViewModel.UpdateDTE
                });

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
