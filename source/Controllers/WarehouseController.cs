﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpareManagement.DomainService;
using SpareManagement.DomainService.Entity;
using SpareManagement.Models;
using System;

namespace SpareManagement.Controllers
{
    [Authorize]

    public class WarehouseController : BaseController
    {
        private readonly IWarehouseDomainService _warehouseDomainService;

        public WarehouseController(IWarehouseDomainService warehouseDomainService,
            IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _warehouseDomainService = warehouseDomainService;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Insert([FromForm] WarehouseInsertViewModel warehouseInsertViewModel)
        {

            try
            {
                var _entity = new WarehouseInsertEntity
                {
                    PartNo1 = warehouseInsertViewModel.PartNo1,
                    Count1 = warehouseInsertViewModel.Count1,
                    PartNo2 = warehouseInsertViewModel.PartNo2,
                    Count2 = warehouseInsertViewModel.Count2,
                    PartNo3 = warehouseInsertViewModel.PartNo3,
                    Count3 = warehouseInsertViewModel.Count3,
                    PartNo4 = warehouseInsertViewModel.PartNo4,
                    Count4 = warehouseInsertViewModel.Count4,
                    PartNo5 = warehouseInsertViewModel.PartNo5,
                    Count5 = warehouseInsertViewModel.Count5,
                    CreateUser = warehouseInsertViewModel.CreateUser,
                    CreateDate = warehouseInsertViewModel.CreateDate,
                    Memo = warehouseInsertViewModel.Memo ?? ""
                };

                var response = _warehouseDomainService.Insert(_entity, GetUserInfo());

                return Json(response);
            }
            catch (Exception ex)
            {
                return Json($"錯誤：{ex.Message}");
            }
        }
    }
}
