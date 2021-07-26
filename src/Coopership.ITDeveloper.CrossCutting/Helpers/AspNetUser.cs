using Cooperchip.ITDeveloper.Domain.Interfaces;
using Coopership.ITDeveloper.CrossCutting.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Coopership.ITDeveloper.CrossCutting.Helpers
{
    public class AspNetUser : IUserInContext
    {
        private readonly IHttpContextAccessor _accessor;
        public AspNetUser(IHttpContextAccessor accessor) { _accessor = accessor; }
        public string Name => _accessor.HttpContext.User.Identity.Name;
        public Guid GetUserId() { return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty; }
        public string GetUserEmail() { return IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : ""; }
        public bool IsAuthenticated() { return _accessor.HttpContext.User.Identity.IsAuthenticated; }
        public string GetUserApelido() { return IsAuthenticated() ? _accessor.HttpContext.User.GetUserApelido() : ""; }
        public string GetUserDataNascimento() { return IsAuthenticated() ? _accessor.HttpContext.User.GetUserDataNascimento() : ""; }
        public string GetUserImgProfilePath() { return IsAuthenticated() ? _accessor.HttpContext.User.GetUserImgProfilePath() : ""; }
        public string GetUserNomeCompleto() { return IsAuthenticated() ? _accessor.HttpContext.User.GetUserNomeCompleto() : ""; }
        public IEnumerable<Claim> GetClaimsIdentity() { return _accessor.HttpContext.User.Claims; }
        public bool IsInRole(string role) { return _accessor.HttpContext.User.IsInRole(role); }
    }
}
