using System;
using System.Collections.Generic;
using Exporter.Builder;
using Exporter.Mapping;

namespace Exporter.Helpers
{
    /// <summary>
    /// Helper to create connect easyly the dataSource and the exporter to export data in the output stream.
    /// </summary>
    public static class ExporterHelper
    {
        /// <summary>
        /// Export the dataSource collection in the output stream based on the configuration.
        /// </summary>
        /// <typeparam name="T">The type of objects in the dataSource collection</typeparam>
        /// <param name="dataSource">A collection of object</param>
        /// <param name="configuration">The configuration to export in csv</param>
        public static IExportBuilder<T> Export<T>(this IEnumerable<T> dataSource, Action<ExportMapper<T>> configuration)
            where T : class
        {
            var mapper = new ExportMapper<T>();
            configuration(mapper);

            return Export(dataSource, mapper);
        }

        /// <summary>
        /// Export the dataSource collection in the output stream based on the configuration.
        /// </summary>
        /// <typeparam name="T">The type of objects in the dataSource collection</typeparam>
        /// <param name="dataSource">A collection of object</param>
        /// <param name="configuration">The configuration to export in csv</param>
        public static IExportBuilder<T> Export<T>(this IEnumerable<T> dataSource, ExportMapper<T> configuration)
            where T : class
        {
            return new ExportBuilder<T> {
                DataSource = dataSource,
                Mapper = configuration
            };
        }
    }
}
