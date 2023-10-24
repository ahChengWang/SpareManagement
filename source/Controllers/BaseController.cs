using Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class BaseController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserEntity GetUserInfo()
        {
            try
            {
                var _userClaims = _httpContextAccessor.HttpContext.User.Claims;

                return new UserEntity
                {
                    Account = _userClaims.FirstOrDefault(m => m.Type == ClaimTypes.Sid)?.Value,
                    Name = _userClaims.FirstOrDefault(m => m.Type == ClaimTypes.Name)?.Value,
                };

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
