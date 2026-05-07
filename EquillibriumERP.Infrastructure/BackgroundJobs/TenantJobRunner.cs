using System;
using System.Threading.Tasks;
using EquillibriumERP.Infrastructure.MultiTenancy;

namespace EquillibriumERP.Infrastructure.BackgroundJobs;

public class TenantJobRunner
{
    /*private readonly TenantExecutionContext _tenantContext;

    public TenantJobRunner(TenantExecutionContext tenantContext)
    {
        _tenantContext = tenantContext;
    }*/

    public async Task RunAsync(string schema, Func<Task> job)
    {
       // await _tenantContext.RunAsync(schema, job);
    }
}