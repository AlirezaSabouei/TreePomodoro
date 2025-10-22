using Application.Common.Tools;

namespace Infrastructure.Tools;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetUtcNow()
    {
        return DateTime.UtcNow;
    }
}
