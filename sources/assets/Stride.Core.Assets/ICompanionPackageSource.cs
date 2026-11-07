// Copyright (c) .NET Foundation and Contributors (https://dotnetfoundation.org/ & https://stride3d.net)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Stride.Core.Diagnostics;

namespace Stride.Core.Assets;

/// <summary>
/// Companion packages that the host adds to the sessions it loads, besides those the session's packages declare
/// (<see cref="PackageLoadParameters.CompanionPackageSource"/>). The editor uses it for the extensions the user installed;
/// the session itself knows nothing of them.
/// </summary>
/// <remarks>
/// Each companion goes through the same steps as a declared one: the toolkit of the host, the session's
/// <see cref="ICompanionPackageFilter"/> (asked with no declaring package), the dev redirect, the restore, and the
/// session's <see cref="IAssemblyLoadCheck"/> when its assemblies load. A companion that a package of the session already
/// brings (a project of the solution, or the companion of a package the game references) is not added a second time: the
/// session's own one loads.
/// </remarks>
[Experimental("STREXP002")]
public interface ICompanionPackageSource
{
    /// <summary>
    /// Folders (local NuGet feeds) where the restore of these companions also looks for packages, besides the NuGet sources
    /// configured for the session.
    /// </summary>
    IReadOnlyList<string> PackageSources { get; }

    /// <summary>
    /// The companions to add. Read once per session, at its first load: a change after that applies to the next session.
    /// </summary>
    /// <param name="log">The log of the session's load.</param>
    /// <returns>The companions, each with its name, version and kind; the toolkit when it is written for one.</returns>
    IReadOnlyList<CompanionPackage> GetCompanionPackages(ILogger log);
}
