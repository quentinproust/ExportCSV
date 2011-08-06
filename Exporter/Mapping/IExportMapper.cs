using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Exporter.Mapping
{
    /// <summary>
    /// Mapper to create the configuration of the mapping.
    /// Will define how data objects are exported to the ouput.
    /// </summary>
    /// <typeparam name="TData">The type of data objects</typeparam>
    public interface IExportMapper<TData> where TData: class 
    {
        /// <summary>
        /// The mapping that has already been defined.
        /// </summary>
        IList<IMapCommand<TData>> Mappings { get; }

        /// <summary>
        /// Add a mapping information in the Mappings.
        /// It will map the property with some defaults, 
        /// trying to find the informations that has not yet been defined on his own.
        /// </summary>
        /// <typeparam name="TValue">The type of the property to export.</typeparam>
        /// <param name="property"></param>
        /// <returns>The same mapper to continue the configuration</returns>
        /// <example>For an object with FullName property : Mapper.Map(x => x.FullName)</example>
        IExportMapper<TData> ImplicitMap<TValue>(Expression<Func<TData, TValue>> property);

        /// <summary>
        /// Add a mapping information in the Mappings.
        /// </summary>
        /// <param name="mapping">The mapping informations.</param>
        /// <returns>The same mapper to continue the configuration</returns>
        /// <example>
        /// For an object with Surname and Firstname that should be exported as FullName :
        /// Mapper.Map(m => m.Title("FullName")
        /// </example>
        IExportMapper<TData> Map(Func<IMapCommand<TData>, IMapCommand<TData>> mapping);
    }
}
