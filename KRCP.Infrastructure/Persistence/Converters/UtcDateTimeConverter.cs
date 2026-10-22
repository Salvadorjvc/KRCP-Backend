using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Infrastructure.Persistence.Converters
{
    //internal hace que la visibilidad sea justa y necesaria (solo para Infrastructure)
    internal class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
    {
        public UtcDateTimeConverter()
            :base(
                 //al guardar en SQL server: (if) si es local, la pasa a UTC
                 v => v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v,

                 //al leer de SQL Server: Fuerza el Kind a UTC para que el JSON mande la 'Z
                 v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            )
        { }
    }
}
