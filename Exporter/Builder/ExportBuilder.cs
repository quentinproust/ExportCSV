using System;
using System.Collections.Generic;
using System.IO;
using Exporter.Mapping;

namespace Exporter.Builder
{
    /// <summary>
    /// Default Builder to output data from a dataSource according to the mapping configuration.
    /// </summary>
    /// <typeparam name="TData">The type of data from the dataSource.</typeparam>
    public class ExportBuilder<TData> : IExportBuilder<TData>
    {
        /// <summary>
        /// The mapping configuration.
        /// </summary>
        public ExportMapper<TData> Mapper { get; set; }
        /// <summary>
        /// The dataSource that will be used to populate the output.
        /// </summary>
        public IEnumerable<TData> DataSource { get; set; }

        /// <summary>
        /// Output data in the writer depending on the implementation.
        /// </summary>
        /// <param name="writer">The output writer</param>
        public void Output(TextWriter writer)
        {
            if (Mapper == null)
                throw new ArgumentException("No mapping defined. Please assign a mapper.");
            if(DataSource == null) return;

            // Write csv headers
            foreach (var map in Mapper.Mappings)
            {
                writer.Write(map.TitleProp);
                writer.Write(";");
            }

            writer.WriteLine();

            // Write values
            foreach (var modelValue in DataSource)
            {
                foreach (var map in Mapper.Mappings)
                {
                    var formattedValue = map.FormatProp == null
                                             ? map.ValueProp(modelValue).ToString()
                                             : map.FormatProp(map.ValueProp(modelValue));

                    writer.Write("\"");
                    writer.Write(formattedValue.Replace("\"", "\"\""));
                    writer.Write("\"");
                    writer.Write(";");
                }
                writer.WriteLine();
            }
        }
    }
}