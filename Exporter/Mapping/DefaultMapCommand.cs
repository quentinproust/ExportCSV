using System;
using System.Linq.Expressions;

namespace Exporter.Mapping
{
    /// <summary>
    /// Default Implementation of IMapCommand.
    /// </summary>
    public class DefaultMapCommand<T> : MapCommand<T>
    {
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
            var compiledFunc = property.Compile();
            ValueProp = model => compiledFunc.Invoke(model);

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
