using Application.Common.Tools;

namespace Infrastructure.Tools;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetNow()
    {
        return DateTime.UtcNow;
    }
}
