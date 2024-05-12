using Hangfire;
using System.Diagnostics;

namespace StoreApi.Hangfire.FakeApiSync
{
    public class BackgroundWorker : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            RecurringJob.AddOrUpdate<FakeStoreApiSyncProductsJob>("SyncProducts", job => job.Run(), Cron.Never);
            RecurringJob.TriggerJob("SyncProducts");

            return Task.CompletedTask;
        }


    }
}
