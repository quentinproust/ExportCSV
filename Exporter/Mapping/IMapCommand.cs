using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Exporter.Mapping
{
    /// <summary>
    /// Interface for mapping data model.
    /// </summary>
    /// <typeparam name="TData">Type of the data model class.</typeparam>
    public interface IMapCommand<TData>
    {
        /// <summary>
        /// Get the title value for the csv column.
        /// </summary>
        string TitleProp { get; }
        /// <summary>
        /// Get the action that will retrieve the value from the model.
        /// </summary>
        Func<TData, object> ValueProp { get; }
        /// <summary>
        /// The format function that will be used to format the value.
        /// </summary>
        Func<object, string> FormatProp { get; }
    }
}