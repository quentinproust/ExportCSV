using System.Collections.Generic;
using System.IO;
using Exporter.Mapping;

namespace Exporter.Builder
{
    /// <summary>
    /// Builder to output data from a dataSource according to the mapping configuration.
    /// </summary>
    /// <typeparam name="TData">The type of data from the dataSource.</typeparam>
    public interface IExportBuilder<TData>
    {
        /// <summary>
        /// The mapping configuration.
        /// </summary>
        ExportMapper<TData> Mapper { get; set; }
        /// <summary>
        /// The dataSource that will be used to populate the output.
        /// </summary>
        IEnumerable<TData> DataSource { get; set; }

        /// <summary>
        /// Output data in the writer depending on the implementation.
        /// </summary>
        /// <param name="writer">The output writer</param>
        void Output(TextWriter writer);
    }
}
