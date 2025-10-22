using System.Linq.Expressions;
using Application.Common.Tools;
using Hangfire;

namespace Infrastructure.JobTools;

public class Job : IJob
{
    public void QueueJob(Expression<Action> action, TimeSpan delay)
    {
        BackgroundJob.Schedule(action, delay);
    }
}