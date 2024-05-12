using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Attributes;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CompanyName.MyProjectName.Modules.Patients.Infrastructure.Jobs
{
    [Jobs(name: "scheduler", triggerGroup: "appointments", triggerName: "schedulertrigger")]
    public class SchedulerJob(ILogger<SchedulerJob> logger) : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            logger.LogInformation("Scheduler job started.");
            return Task.CompletedTask;
        }
    }
}
