// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Configuration;

namespace Content.Shared._Library;

[CVarDefs]
public sealed class LibraryCVars
{
    /// <summary>
    ///     Whether library consoles may upload scanned books to the persistent library archive.
    /// </summary>
    public static readonly CVarDef<bool> UploadsEnabled =
        CVarDef.Create("library.uploads_enabled", true, CVar.SERVER | CVar.SERVERONLY);

    /// <summary>
    ///     Maximum number of (non-deleted) books the library archive will hold, 0 for unlimited.
    /// </summary>
    public static readonly CVarDef<int> MaxBooks =
        CVarDef.Create("library.max_books", 5000, CVar.SERVER | CVar.SERVERONLY);
}
