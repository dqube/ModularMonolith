using System.Reflection;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;

namespace CompanyName.MyProjectName.BuildingBlocks.Jobs;

public static class Extensions
{
    public static IServiceCollection AddJobs(
        this IServiceCollection services,
        IConfiguration configuration,
        string modulePart,
        IEnumerable<Assembly> assemblies = null)
    {
        var section = configuration.GetSection("quartz");
        var options = section.BindOptions<JobOptions>();
        services.Configure<JobOptions>(section);
        ISchedulerFactory sf = new StdSchedulerFactory();
        IScheduler scheduler = sf.GetScheduler().Result;
        services.AddSingleton(scheduler);
        var types = assemblies.Where(x => x.FullName is not null && x.FullName.Contains(modulePart)).SelectMany(x => x.GetTypes()).ToArray();

        var jobTypes = types
            .Where(t => typeof(IJob).IsAssignableFrom(t) && !t.IsAbstract)
            .ToList();

        jobTypes.ForEach(job =>
                {
                    var attr = job.GetCustomAttribute<JobsAttribute>();
                    var jobKey = new JobKey(attr.Name);
                    var jobConfig = options.Jobs.FirstOrDefault(x => x.Key == attr.Name).Value;
                    services.AddQuartz(q =>
                    {
                        q.AddJob(job, jobKey);
                        q.AddTrigger(opts => opts
                        .ForJob(jobKey)
                        .WithIdentity(attr.TriggerName)
                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(jobConfig.Interval).RepeatForever()));
                    });
                });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        return services;
    }
}