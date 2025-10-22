using System.Linq.Expressions;

namespace Application.Common.Tools;

public interface IJob
{
    void QueueJob(Expression<Action> action, TimeSpan delay);
}