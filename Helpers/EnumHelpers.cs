using System;
using System.Collections.Generic;
using SoftRes.Models;

namespace SoftRes.Helpers
{
    public static class EnumHelpers
    {
        public static IEnumerable<TEnum> ToEnumerable<TEnum>()
        {
            foreach (object value in Enum.GetValues(typeof(TEnum)))
            {
                yield return (TEnum) value;
            }
        }
    }
}