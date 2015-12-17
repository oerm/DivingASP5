using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivingApp.Common.Convertor
{
    public class NullableDateTimeConverter : TypeConverter<DateTime?, DateTime>
    {
        const string DefaultDate = "01.01.1900";

        protected override DateTime ConvertCore(DateTime? source)
        {
            if (source.HasValue)
                return source.Value;
            else
                return DateTime.Parse(NullableDateTimeConverter.DefaultDate);
        }
    }
}
