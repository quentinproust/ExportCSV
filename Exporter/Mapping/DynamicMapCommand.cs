using System;

namespace Exporter.Mapping
{
    /// <summary>
    /// Command to map dynamic object.
    /// </summary>
    public class DynamicMapCommand : MapCommand<dynamic>
    {
        /// <summary>
        /// Map the title.
        /// </summary>
        /// <param name="title">The title</param>
        /// <returns>This</returns>
        public DynamicMapCommand Title(string title)
        {
            TitleProp = title;
            return this;
        }

        /// <summary>
        /// Map a computation for complexe mapping.
        /// </summary>
        /// <param name="property">The computation</param>
        /// <returns>This</returns>
        public DynamicMapCommand Compute(Func<dynamic, string> property)
        {
            ValueProp = property;
            return this;
        }

        /// <summary>
        /// Map a property based on this string. Will use reflection to retrieve data.
        /// </summary>
        /// <param name="property">Property</param>
        /// <returns>This</returns>
        public DynamicMapCommand Map(string property)
        {
            throw new NotImplementedException();
            return this;
        }
    }
}
