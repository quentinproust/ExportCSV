using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Exporter.Mapping
{
    /// <summary>
    /// Interface for mapping data model.
    /// </summary>
    /// <typeparam name="T">Type of the data model class.</typeparam>
    public interface IMapCommand<T> where T : class
    {
        /// <summary>
        /// Get the title value for the csv column.
        /// </summary>
        string TitleProp { get; }
        /// <summary>
        /// Get the action that will retrieve the value from the model.
        /// </summary>
        Func<T, object> ValueProp { get; }
        /// <summary>
        /// The format function that will be used to format the value.
        /// </summary>
        Func<object, string> FormatProp { get; }

        /// <summary>
        /// Map the title.
        /// </summary>
        /// <param name="title">Title</param>
        /// <returns>Map command</returns>
        IMapCommand<T> Title(string title);
        
        /// <summary>
        /// Directly map a property.
        /// </summary>
        /// <typeparam name="TValue">The type of the property</typeparam>
        /// <param name="property">Lambda to retrieve the property</param>
        /// <returns>Map command</returns>
        IMapCommand<T> Value<TValue>(Expression<Func<T, TValue>> property);
        /// <summary>
        /// An action to get a value from data model.
        /// It allows complexe mapping from the user.
        /// </summary>
        /// <param name="valueGetter">The action that will get values</param>
        /// <returns>Map command</returns>
        IMapCommand<T> Compute(Func<T, string> valueGetter);

        /// <summary>
        /// Format the value that will be exported.
        /// </summary>
        /// <param name="formatter">format string. Can contain {0}.</param>
        /// <returns>Map command</returns>
        IMapCommand<T> Format(string formatter);
    }
}