using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace ManufacturingERP.Infrastructure.MultiTenancy;

public class TenantDbConnectionInterceptor : DbConnectionInterceptor
{
    private readonly ITenantProvider _tenant;

    public TenantDbConnectionInterceptor(ITenantProvider tenant)
    {
        _tenant = tenant;
    }

    public override async Task ConnectionOpenedAsync(
        DbConnection connection,
        ConnectionEndEventData eventData,
        CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrWhiteSpace(_tenant.Schema))
        {
            using var cmd = connection.CreateCommand();
            cmd.CommandText = $"SET search_path TO \"{_tenant.Schema}\";";
            await cmd.ExecuteNonQueryAsync(cancellationToken);
        }
    }
}