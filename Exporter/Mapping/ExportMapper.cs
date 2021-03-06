﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Exporter.Mapping
{
    /// <summary>
    /// Mapper to create the configuration of the mapping.
    /// Will define how data objects are exported to the ouput.
    /// </summary>
    /// <typeparam name="TData">The type of data objects</typeparam>
    public class ExportMapper<TData>
    {
        private readonly AutoTitleReader _autoTitleReader = new AutoTitleReader();

        /// <summary>
        /// Default ctor
        /// </summary>
        public ExportMapper()
        {
            Mappings = new List<MapCommand<TData>>();
        }

        /// <summary>
        /// The mapping that has already been defined.
        /// </summary>
        public IList<MapCommand<TData>> Mappings { get; private set; }
    
        /// <summary>
        /// Configure an implicite value.
        /// </summary>
        /// <typeparam name="TValue">Type of data to map</typeparam>
        /// <param name="property">The property that will be mapped</param>
        /// <returns>This</returns>
        /// <remarks>
        /// The implicit mapping cannot chain property. 
        /// You can do x => x.Something but not x => x.Something.PropertyOfSmthg as I retrieve some informations to map implicitly
        /// </remarks>
        public ExportMapper<TData> ImplicitMap<TValue>(Expression<Func<TData, TValue>> property)
        {
            // Automagically retrieve the title from the expression
            var memberExpression = property.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException(@"ImplicitMap cannot convert the parameter property to a MemberExpression. \n
Most of the time, it means you tried to chain properties like x => x.Something.SomeOtherThing but you can't do that. \n
You might want to use a more explicit declaration for your mapping");

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException(@"ImplicitMap cannot convert the parameter property to a PropertyInfo. \n
Most of the time, it means you tried to chain properties like x => x.Something.SomeOtherThing but you can't do that. \n
You might want to use a more explicit declaration for your mapping");

            var title = _autoTitleReader.GetTitle(propertyInfo);

            var mapping = new DefaultMapCommand<TData>()
                .Title(title)
                .Value(property);
            Mappings.Add(mapping);

            return this;
        }

        /// <summary>
        /// Configure an implicite value.
        /// </summary>
        /// <param name="mapping">Add manually a mapping command</param>
        /// <returns>This</returns>
        public ExportMapper<TData> Map(Func<DefaultMapCommand<TData>, DefaultMapCommand<TData>> mapping)
        {
            Mappings.Add(mapping(new DefaultMapCommand<TData>()));
            return this;
        }
    }
}
