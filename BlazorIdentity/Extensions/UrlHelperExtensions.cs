using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace BlazorIdentity.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            ActionContext actionContext = urlHelper.ActionContext;
            Microsoft.AspNetCore.Http.HttpContext httpContext = actionContext.HttpContext;
            Microsoft.AspNetCore.Http.HttpRequest request = httpContext.Request;
            var host = request.Host;
            var qsargs = new Dictionary<string, string>()
            {
                { "userId", WebUtility.UrlEncode(userId)},
                {"code", WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code)) }
            };
            var query = QueryHelpers.AddQueryString("confirmationEmail", qsargs);
            ////"https://localhost:44365/account/confirmationEmail/" + "904dbb28-72c1-44c4-87b3-940914d67ea2/Q2ZESjhIMG5NTlRycVR4TGhnS2w0Wk56dVM0dFNBTWw2clZiMzk2UDRiVDY0bjVRQmJRS24xeVVCTS9zckJ3M1o1MUpEeHJhc1loNEwrT0R2bmtKcTNLSU1tbmQzb0hYVDkrUnpQaWM3MXFPaVo0Z2g2NitYNXpsMGw3WEJjdDhGTzFlUEhLNjJzOE9QUnY2U3dhWDdrRnZ5c0RKdklaTmhwOFZjTjkzdUJrY1p4K25tTU1yWWZiTDJwTDFmOHdqbzJBMW01ZFBLeXhoMTlHTXV4aHN3WExnbEdiWmh0Q3plQlpNcEUzQjJuVlVPQVlmb01KbmMvSFljcWhpbmRnRE42NEZOdz09"
            var ub = new UriBuilder(scheme, host.Host)
            {
                Path = query,
                Port = host.Port.GetValueOrDefault(80)
            };
            return ub.ToString();
        }

        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            var host = urlHelper.ActionContext.HttpContext.Request.Host;
            var ub = new UriBuilder(scheme, host.Host)
            {
                Path = $"account/resetPassword/{WebUtility.UrlEncode(userId)}/{WebUtility.UrlEncode(code)}",
                Port = host.Port.GetValueOrDefault(80)
            };
            return ub.ToString();
        }
    }
}
