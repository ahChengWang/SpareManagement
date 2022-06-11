using Microsoft.AspNetCore.Mvc;
using SpareManagement.DomainService;
using SpareManagement.DomainService.Entity;
using SpareManagement.Models;
using System;

namespace PersonalWeb.Controllers
{
    public class ReleaseController : Controller
    {
        private readonly ReleaseDomainService _releaseDomainService;

        public ReleaseController(ReleaseDomainService releaseDomainService)
        {
            _releaseDomainService = releaseDomainService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Update([FromForm] ReleaseViewModel releaseViewModel)
        {
            try
            {
                var _entity = new ReleaseEntity
                {
                    PartNo1 = releaseViewModel.PartNo1,
                    Count1 = releaseViewModel.Count1,
                    PartNo2 = releaseViewModel.PartNo2,
                    Count2 = releaseViewModel.Count2,
                    PartNo3 = releaseViewModel.PartNo3,
                    Count3 = releaseViewModel.Count3,
                    PartNo4 = releaseViewModel.PartNo4,
                    Count4 = releaseViewModel.Count4,
                    PartNo5 = releaseViewModel.PartNo5,
                    Count5 = releaseViewModel.Count5,
                    CreateUser = releaseViewModel.ReleaseUser,
                    CreateDate = releaseViewModel.ReleaseDate,
                    Memo = releaseViewModel.Memo ?? ""
                };

                var response = _releaseDomainService.Release(_entity);

                return Json(response);
                //return PartialView("_SearchPartial", res);
            }
            catch (Exception ex)
            {
                return Json($"錯誤：{ex.Message}");
            }
        }
    }
}
