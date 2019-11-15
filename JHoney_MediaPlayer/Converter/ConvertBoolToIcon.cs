﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace JHoney_MediaPlayer.Converter
{
    class ConvertBoolToIcon : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        /// <summary>
        /// Converts an Int32 zero-based index to a one-based number.
        /// </summary>
        /// <param name="value">Int32 zero-based index.</param>
        /// <param name="targetType">Ignored.</param>
        /// <param name="parameter">Ignored.</param>
        /// <param name="culture">Ignored.</param>
        /// <returns>Int32 one-based number.</returns>
        /// <exception cref="FormatException">Incorrect format.</exception>
        /// <exception cref="InvalidCastException">Unsupported convversion.</exception>
        /// <exception cref="OverflowException">Out of range of Int32.</exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return MahApps.Metro.IconPacks.PackIconMaterialKind.ShuffleVariant;
            }
            else
            {
                return MahApps.Metro.IconPacks.PackIconMaterialKind.ShuffleDisabled;
            }

        }

        /// <summary>
        /// Converts an Int32 one-based number to a zero-based index.
        /// </summary>
        /// <param name="value">Int32 one-based number.</param>
        /// <param name="targetType">Ignored.</param>
        /// <param name="parameter">Ignored.</param>
        /// <param name="culture">Ignored.</param>
        /// <returns>Int32 zero-based index.</returns>
        /// <exception cref="FormatException">Incorrect format.</exception>
        /// <exception cref="InvalidCastException">Unsupported convversion.</exception>
        /// <exception cref="OverflowException">Out of range of Int32.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value);
        }
    }
}
