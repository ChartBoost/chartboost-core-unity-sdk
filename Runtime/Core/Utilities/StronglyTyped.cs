using System;
using System.ComponentModel;
using System.Globalization;

namespace Chartboost.Core.Utilities
{
    /// <summary>
    /// Interface to make values strongly-typed with help of TypeConverters.
    /// </summary>
    /// <typeparam name="T">Inner type</typeparam>
    public interface IStronglyTyped<out T>
    {
        /// <summary>
        /// Inner value.
        /// </summary>
        T Value { get; }
    }

    public abstract class StronglyTyped<TInnerType> : IStronglyTyped<TInnerType>
    {
        public TInnerType Value { get; }

        public StronglyTyped(TInnerType value)
        {
            Value = value;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Value?.ToString();
        }

        /// <summary>
        /// Implicit mapping to `string`.
        /// </summary>
        public static implicit operator string(StronglyTyped<TInnerType> obj)
        {
            return obj?.ToString();
        }
    }

    /// <summary>
    /// Generic type converter for converting `string` to `TValue` (and other way around).
    /// </summary>
    public class StringTypeConverter<TValue> : TypeConverter
    {
        /// <inheritdoc />
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) 
            => sourceType == typeof(string);

        /// <inheritdoc />
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) 
            => destinationType == typeof(string);

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                null => null,
                string stringValue => CreateInstance(stringValue),
                _ => throw new NotSupportedException($"Can't convert `{value.GetType().Name}` to `{typeof(TValue)}`")
            };
        }

        /// <summary>
        /// Creates instance of `TValue` from string value.
        /// </summary>
        protected TValue CreateInstance(string value) => CreateInstanceInternal(value);

        /// <summary>
        /// Creates instance of `TValue` from string value.
        /// </summary>
        protected virtual TValue CreateInstanceInternal(string value)
        {
            if (typeof(IStronglyTyped<string>).IsAssignableFrom(typeof(TValue)))
                return (TValue)Activator.CreateInstance(typeof(TValue), value);
            var typeConverter = TypeDescriptor.GetConverter(typeof(TValue));
            return (TValue)typeConverter.ConvertFromString(value);
        }

        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            if (destinationType == typeof(string))
                return ((TValue)value)?.ToString();
            throw new NotSupportedException($"Can't convert `{typeof(TValue)}` to `{destinationType.Name}`");
        }
    }
}
