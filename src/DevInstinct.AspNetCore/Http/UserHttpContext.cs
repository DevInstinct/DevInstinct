using DevInstinct.Patterns;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace DevInstinct.AspNetCore.Http
{
    public abstract class UserHttpContext<T> : IUserContext<T>
        where T : IEquatable<T>
    {
        protected UserHttpContext(IHttpContextAccessor httpContextAccessor)
        {
            HttpContext = httpContextAccessor.HttpContext;
        }

        public abstract T Id { get; }

        public ClaimsPrincipal ClaimsPrincipal => HttpContext.User;

        protected HttpContext HttpContext { get; }
    }
}
