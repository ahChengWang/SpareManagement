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

    public class FixController : BaseController
    {
        private readonly IFixDomainService _fixDomainService;

        public FixController(IFixDomainService fixDomainService,
            IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _fixDomainService = fixDomainService;
        }

        public IActionResult Index()
        {
            ViewData["Category"] = new SelectList(DefinitionHelper.GetInspectCategoryOption(), "TypeId", "TypeName");

            return View();
        }

        [HttpGet]
        public IActionResult Search([FromQuery] int categoryId, string partNo)
        {
            try
            {
                var res = _fixDomainService.SelectList(categoryId, partNo);

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
                var res = _fixDomainService.Update(new InspectsEntity
                {
                    PartNo = updateViewModel.PartNo,
                    SerialNo = updateViewModel.SerialNo,
                    CategoryId = updateViewModel.CategoryId,
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
