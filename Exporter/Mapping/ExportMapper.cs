using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Exporter.Mapping
{
    /// <summary>
    /// Create the configuration required to export data.
    /// </summary>
    /// <typeparam name="TData">The type of objects that will be exported</typeparam>
    public class ExportMapper<TData> : IExportMapper<TData>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ExportMapper()
        {
            Mappings = new List<IMapCommand<TData>>();
        }

        /// <summary>
        /// All mapping informations.
        /// </summary>
        public IList<IMapCommand<TData>> Mappings { get; private set; }
        
        /// <summary>
        /// Configure an implicite value.
        /// <see cref="IExportMapper{TData}.ImplicitMap{TValue}"/>
        /// </summary>
        /// <typeparam name="TValue"><see cref="IExportMapper{TData}.ImplicitMap{TValue}"/></typeparam>
        /// <param name="property"><see cref="IExportMapper{TData}.ImplicitMap{TValue}"/></param>
        /// <returns><see cref="IExportMapper{TData}.ImplicitMap{TValue}"/></returns>
        public ExportMapper<TData> ImplicitMap<TValue>(Expression<Func<TData, TValue>> property)
        {
            var mapping = new DefaultMapCommand<TData>()
                .Value(property);
            Mappings.Add(mapping);

            return this;
        }

        /// <summary>
        /// Configure an implicite value.
        /// <see cref="IExportMapper{TData}.ImplicitMap{TValue}"/>
        /// </summary>
        /// <param name="mapping"><see cref="IExportMapper{TData}.ImplicitMap{TValue}"/></param>
        /// <returns><see cref="IExportMapper{TData}.ImplicitMap{TValue}"/></returns>
        public ExportMapper<TData> Map(Func<DefaultMapCommand<TData>, DefaultMapCommand<TData>> mapping)
        {
            Mappings.Add(mapping(new DefaultMapCommand<TData>()));
            return this;
        }
    }
}