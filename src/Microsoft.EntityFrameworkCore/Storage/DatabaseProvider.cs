// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Reflection;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Microsoft.EntityFrameworkCore.Storage
{
    /// <summary>
    ///     <para>
    ///         The primary point where a database provider can tell EF that it has been selected for the current context.
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    /// </summary>
    /// <typeparam name="TOptionsExtension">
    ///     The type of options that the database provider will add to <see cref="DbContextOptions.Extensions" />
    ///     to identify that is has been selected (and to store its database specific settings).
    /// </typeparam>
    public class DatabaseProvider<TOptionsExtension> : IDatabaseProvider
        where TOptionsExtension : class, IDbContextOptionsExtension
    {
        /// <summary>
        ///     The unique name used to identify the database provider. This should be the same as the NuGet package name
        ///     for the providers runtime.
        /// </summary>
        public virtual string InvariantName => typeof(TOptionsExtension).GetTypeInfo().Assembly.GetName().Name;

        /// <summary>
        ///     Gets a value indicating whether this database provider has been selected for a given context.
        /// </summary>
        /// <param name="options"> The options for the context. </param>
        /// <returns> True if the database provider has been selected, otherwise false. </returns>
        public virtual bool IsConfigured(IDbContextOptions options)
            => Check.NotNull(options, nameof(options)).Extensions.OfType<TOptionsExtension>().Any();
    }
}
