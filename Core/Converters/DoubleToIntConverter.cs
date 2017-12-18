using Core.Interfaces;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Core.Converters
{
    //public class DoubleToIntConverter : IValueConverter
    public class DoubleToIntConverter : IBindingTypeConverter, IDoubleToIntConverter
    {
        //public object Convert(object value, Type targetType,
        //                      object parameter, CultureInfo culture)
        //{
        //    double multiplier;

        //    if (!Double.TryParse(parameter as string, out multiplier))
        //        multiplier = 1;

        //    return (int)Math.Round(multiplier * (double)value);
        //}

        //public object ConvertBack(object value, Type targetType,
        //                          object parameter, CultureInfo culture)
        //{
        //    double divider;

        //    if (!Double.TryParse(parameter as string, out divider))
        //        divider = 1;

        //    return ((double)(int)value) / divider;
        //}

        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            if (fromType == typeof(double))
            {
                return 100; // any number other than 0 signifies conversion is possible.
            }
            return 0;
        }
        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            try
            {
                double multiplier;

                if (!Double.TryParse(conversionHint as string, out multiplier))
                    multiplier = 1;

                result = (int)Math.Round(multiplier * (double)from);
            }
            catch (Exception ex)
            {
                //this.Log().WarnException("Couldn't convert object to type: " + toType, ex);
                result = null;
                return false;
            }

            return true;
        }
    }
}
