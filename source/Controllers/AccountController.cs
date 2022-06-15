using Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpareManagement.DomainService;
using SpareManagement.DomainService.Entity;
using SpareManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SpareManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountDomainService _accountDomainService;

        public AccountController(AccountDomainService accountDomainService)
        {
            _accountDomainService = accountDomainService;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login([FromForm] LoginViewModel loginModel)
        {
            var _result = _accountDomainService.Validate(new AccountEntity
            {
                AccountInfo = new AccountInfoEntity
                {
                    Account = loginModel.Account,
                    Password = loginModel.Password
                }
            });

            if (_result.IsValidate)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(_result.AccountInfo.Account)),
                    new Claim("Name", _result.AccountInfo.Name),
                    new Claim("Account", _result.AccountInfo.Account),
                    //new Claim(ClaimTypes.Role, user.Role)
                };
                //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                var principal = new ClaimsPrincipal(identity);
                //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                {
                    IsPersistent = false //IsPersistent = false：瀏覽器關閉立馬登出；IsPersistent = true 就變成常見的Remember Me功能
                }).Wait();

                return Json("");
            }

            return Json(_result.Message);
        }


        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        //登出 Action 記得別加上[Authorize]，不管用戶是否登入，都可以執行Logout
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Account");//導至登入頁
        }
    }
}
