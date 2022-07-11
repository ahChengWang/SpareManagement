using Microsoft.AspNetCore.Mvc;
using SpareManagement.DomainService;
using SpareManagement.DomainService.Entity;
using SpareManagement.Helper;
using SpareManagement.Models;
using System;

namespace PersonalWeb.Controllers
{
    public class ReleaseController : Controller
    {
        private readonly IReleaseDomainService _releaseDomainService;

        public ReleaseController(IReleaseDomainService releaseDomainService)
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
                    Node1 = releaseViewModel.Node1,
                    PartNo2 = releaseViewModel.PartNo2,
                    Count2 = releaseViewModel.Count2,
                    Node2 = releaseViewModel.Node2,
                    PartNo3 = releaseViewModel.PartNo3,
                    Count3 = releaseViewModel.Count3,
                    Node3 = releaseViewModel.Node3,
                    PartNo4 = releaseViewModel.PartNo4,
                    Count4 = releaseViewModel.Count4,
                    Node4 = releaseViewModel.Node4,
                    PartNo5 = releaseViewModel.PartNo5,
                    Count5 = releaseViewModel.Count5,
                    Node5 = releaseViewModel.Node5,
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
