using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Exporter.Mapping
{
    /// <summary>
    /// AutoTitleReader try to find the best title for a property.
    /// </summary>
    public class AutoTitleReader
    {
        /// <summary>
        /// Get the best title.
        /// </summary>
        /// <param name="propertyInfo">The property info of the field.</param>
        /// <returns>The title.</returns>
        public string GetTitle(PropertyInfo propertyInfo)
        {
            var attributes = propertyInfo.GetCustomAttributes(typeof (DisplayAttribute), true);
            if (attributes.Length > 0)
            {
                var display = attributes[0] as DisplayAttribute;
                if (display != null) return display.Name;
            }

            return propertyInfo.Name;
        }
    }
}
