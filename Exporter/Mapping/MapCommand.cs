using System;

namespace Exporter.Mapping
{
    /// <summary>
    /// Interface for mapping data model.
    /// </summary>
    /// <typeparam name="TData">Type of the data model class.</typeparam>
    public abstract class MapCommand<TData>
    {
        /// <summary>
        /// Get the title value for the csv column.
        /// </summary>
        public string TitleProp { get; protected set; }
        /// <summary>
        /// Get the action that will retrieve the value from the model.
        /// </summary>
        public Func<TData, object> ValueProp { get; protected set; }
        /// <summary>
        /// The format function that will be used to format the value.
        /// </summary>
        public Func<object, string> FormatProp { get; protected set; }
    }
}