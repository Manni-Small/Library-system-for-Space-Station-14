// SPDX-License-Identifier: AGPL-3.0-or-later

// OPTIONAL EXAMPLE. Some books are items with the Book tag and paper, but should not be part of the library:
// cult tomes, heretic books, antagonist guides. Subscribe to LibraryBookExcludeEvent on their component and they
// are ignored by the library inventory and the book pinpointers. Replace SomeTomeComponent with your own.

using Content.Shared._Library;

namespace Content.Server._Library;

public sealed class ExcludeBooksExample : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        // SubscribeLocalEvent<SomeTomeComponent, LibraryBookExcludeEvent>(OnExclude);
    }

    // private void OnExclude(Entity<SomeTomeComponent> ent, ref LibraryBookExcludeEvent args)
    // {
    //     args.Excluded = true;
    // }
}
