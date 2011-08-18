using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Exporter.Mapping
{
    /// <summary>
    /// Default Implementation of IMapCommand.
    /// </summary>
    public class DefaultMapCommand<T> : IMapCommand<T>
    {
        private readonly AutoTitleReader _autoTitleReader = new AutoTitleReader();

        /// <summary>
        /// The title for the current command.
        /// </summary>
        public string TitleProp { get; private set; }
        /// <summary>
        /// The function that will retrieve the value from data objects.
        /// </summary>
        public Func<T, object> ValueProp { get; private set; }
        /// <summary>
        /// The format function that will be used to format the value.
        /// </summary>
        public Func<object, string> FormatProp { get; private set; }

        /// <summary>
        /// Configure the title.
        /// If you use Value(x => x.SomeProperty) and Title is not yet defined,
        /// it will try to get a good title. It could be the name of the property, an attribute [Display("My property")].
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>The command to continue the configuration</returns>
        public DefaultMapCommand<T> Title(string title)
        {
            TitleProp = title;
            return this;
        }

        /// <summary>
        /// Configure an simple value to export.
        /// </summary>
        /// <typeparam name="TValue">The type of value to export.</typeparam>
        /// <param name="property">The property</param>
        /// <returns>The command to continue the configuration</returns>
        public DefaultMapCommand<T> Value<TValue>(Expression<Func<T, TValue>> property)
        {
            var memberExpression = property.Body as MemberExpression;
            if (memberExpression == null)
                return this;

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if(propertyInfo == null)
                return this;

            if (TitleProp == null)
                TitleProp = _autoTitleReader.GetTitle(propertyInfo);

            ValueProp = model => propertyInfo.GetValue(model, null);

            return this;
        }

        /// <summary>
        /// Configure a computation that will generate a value.
        /// For example, it could create a fullname with Command.Compute(x => x.Firstname + " " + x.Surname).
        /// </summary>
        /// <param name="valueGetter">The computation</param>
        /// <returns>The command to continue the configuration</returns>
        public DefaultMapCommand<T> Compute(Func<T, string> valueGetter)
        {
            ValueProp = valueGetter;
            return this;
        }

        /// <summary>
        /// Format the value that will be exported.
        /// </summary>
        /// <param name="formatter">format string. Can contain {0}.</param>
        /// <returns>Map command</returns>
        public DefaultMapCommand<T> Format(string formatter)
        {
            if (String.IsNullOrEmpty(formatter)) return this;

            if(formatter.Contains("{0}"))
                FormatProp = property => String.Format(formatter, property);
            else
                FormatProp = property => String.Format("{0:"+formatter+"}", property);
            return this;
        }
    }
}
