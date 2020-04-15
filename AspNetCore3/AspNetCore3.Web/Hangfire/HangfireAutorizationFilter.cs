using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace AspNetCore3.Web.Hangfire
{
    public class HangfireAutorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
