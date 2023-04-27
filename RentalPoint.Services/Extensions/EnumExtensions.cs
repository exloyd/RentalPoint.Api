using System;
using System.ComponentModel;
using System.Linq;

namespace RentalPoint.Services.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            if (value == null)
                return string.Empty;

            var field = value.GetType().GetField(value.ToString());
            if (field == null)
                return string.Empty;

            var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}