using System;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Infrastructure.Persistence
{
    /// <summary>
    /// Hard safety guard to prevent EF Core from using incorrect DbContexts
    /// during design-time or runtime operations.
    /// </summary>
    public static class EfContextGuard
    {
        /// <summary>
        /// Validates that only TenantDbContext is being used for EF operations.
        /// </summary>
        public static void Validate(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var contextName = context.GetType().Name;

            if (!string.Equals(contextName, "TenantDbContext", StringComparison.Ordinal))
            {
                throw new InvalidOperationException(
                    $"🚨 EF GUARD BLOCKED EXECUTION: {contextName} is not allowed. " +
                    $"Only TenantDbContext can be used in this infrastructure.");
            }
        }
    }
}