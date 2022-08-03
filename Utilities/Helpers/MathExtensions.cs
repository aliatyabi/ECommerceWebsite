using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Helpers
{
    public static class MathExtensions
    {
        //public static int Round(this int i, int nearest)
        //{
        //    if (nearest <= 0 || nearest % 10 != 0)
        //        throw new ArgumentOutOfRangeException("nearest", "Must round to a positive multiple of 10");

        //    return (i + 5 * nearest / 10) / nearest * nearest;
        //}
        public static double Round(this double value, int precision)
        {
            if (precision < -4 && precision > 15)
                throw new ArgumentOutOfRangeException("precision", "Must be and integer between -4 and 15");

            if (precision >= 0) return Math.Round(value, precision);
            else
            {
                precision = (int)Math.Pow(10, Math.Abs(precision));
                value = value + (5 * precision / 10);
                return Math.Round(value - (value % precision), 6);
            }
        }
    }
}
