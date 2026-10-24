using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Persistence.Converters
{
    internal class UtcNullableDateTimeConverter : ValueConverter<DateTime?, DateTime?>
    {
        public UtcNullableDateTimeConverter()
            : base(
                  v => v.HasValue && v.Value.Kind == DateTimeKind.Local ? v.Value.ToUniversalTime(): v,
                  v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v
            )
        { }
    }
}
