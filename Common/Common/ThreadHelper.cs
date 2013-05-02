using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using System.Web;

namespace Common
{
    public static class ThreadHelper
    {
        public static string GetClaim(string claimType)
        {
            var context = HttpContext.Current;
            return context != null ? context.User.Get(claimType) : Thread.CurrentPrincipal.Get(claimType);
        }

        public static T GetClaim<T>(string claimType)
        {
            var context = HttpContext.Current;
            return context != null ? context.User.Get<T>(claimType) : Thread.CurrentPrincipal.Get<T>(claimType);
        }

        public static IReadOnlyList<string> GetAllClaims(string claimType)
        {
            var context = HttpContext.Current;
            return context != null ? context.User.GetAll(claimType) : Thread.CurrentPrincipal.GetAll(claimType);
        }

        public static void RunAs(IPrincipal principal, Action action)
        {
            RunAs(principal, () =>
            {
                action();
                return 0;
            });
        }

        public static T RunAs<T>(IPrincipal principal, Func<T> func)
        {
            var originalThread = Thread.CurrentPrincipal;
            var originalContext = HttpContext.Current != null ? HttpContext.Current.User : null;

            try
            {
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null) HttpContext.Current.User = principal;

                return func();
            }
            finally
            {
                Thread.CurrentPrincipal = originalThread;
                if (HttpContext.Current != null) HttpContext.Current.User = originalContext;
            }
        }
    }
}
