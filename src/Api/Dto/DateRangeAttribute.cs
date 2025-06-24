using System.ComponentModel.DataAnnotations;

namespace Api.Dto;

// User can create date olny from `{CURRENT_DATE - 10 YEARS}` to `{DateOnly.Max}`
public class DateRangeAttribute : RangeAttribute
{
    public DateRangeAttribute()
        : base(typeof(DateOnly),
            DateOnly.FromDateTime(DateTime.Now.Subtract(TimeSpan.FromDays(3650)))
                .ToString(),
            DateOnly.MaxValue.ToString())
    {
    }
}