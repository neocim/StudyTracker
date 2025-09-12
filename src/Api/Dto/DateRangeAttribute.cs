// Now i don't think i should put a limit on the range of dates, then i dont use it,
// but i left it if i need it in the future.

using System.ComponentModel.DataAnnotations;

namespace Api.Dto;

// User can create date olny from `{CURRENT_DATE - 10 YEARS}` to `{DateOnly.Max}`
public sealed class DateRangeAttribute : RangeAttribute
{
    public DateRangeAttribute()
        : base(typeof(DateOnly),
            DateOnly.FromDateTime(DateTime.Now.Subtract(TimeSpan.FromDays(3650)))
                .ToString(),
            DateOnly.MaxValue.ToString())
    {
    }
}