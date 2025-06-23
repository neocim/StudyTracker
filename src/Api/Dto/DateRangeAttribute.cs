using System.ComponentModel.DataAnnotations;

namespace Api.Dto;

public class DateRangeAttribute : RangeAttribute
{
    public DateRangeAttribute()
        : base(typeof(DateOnly), DateOnly.FromDateTime(DateTime.Now).ToString(),
            DateOnly.MaxValue.ToString())
    {
    }
}