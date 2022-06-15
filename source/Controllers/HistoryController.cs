using Helper;
using Microsoft.AspNetCore.Mvc;
using SpareManagement.DomainService;
using SpareManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWeb.Controllers
{
    public class HistoryController : Controller
    {
        private readonly HistoryDomainService _historyDomainService;

        public HistoryController(HistoryDomainService historyDomainService)
        {
            _historyDomainService = historyDomainService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult History([FromQuery] int categoryId, string partNo)
        {
            try
            {
                var res = _historyDomainService.GetHistory(categoryId, partNo);

                var temp = new List<HistoryDetailViewModel>();

                if (res == null || !res.Any())
                {
                    return View(new HistoryViewModel
                    {
                        PartNo = partNo,
                        details = new List<HistoryDetailViewModel>()
                    });
                }

                res.ForEach(fe =>
                {
                    temp.Add(new HistoryDetailViewModel
                    {
                        Date = fe.UpdateTime.ToString("yyyy/MM/dd HH:mm:ss"),
                        Status = fe.Status.GetDescription(),
                        Quantity = fe.Quantity.ToString(),
                        Node = fe.Node?.ToString() ?? "",
                        Employee = fe.EmpName,
                        Memo = fe.Memo
                    });
                });


                return View(new HistoryViewModel
                {
                    PartNo = partNo,
                    details = temp
                });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new ErrorViewModel { Message = ex.Message });
            }

        }
    }
}
