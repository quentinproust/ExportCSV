using System.Collections.Generic;

namespace Exporter.Mapping
{
    /// <summary>
    /// Mapper to create the configuration of the mapping.
    /// Will define how data objects are exported to the ouput.
    /// </summary>
    /// <typeparam name="TData">The type of data objects</typeparam>
    public interface IExportMapper<TData>
    {
        /// <summary>
        /// The mapping that has already been defined.
        /// </summary>
        IList<IMapCommand<TData>> Mappings { get; }
    }
}
