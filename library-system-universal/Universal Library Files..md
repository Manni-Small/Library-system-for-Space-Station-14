is the same code made independent of OmuStation.

- **Layout:** the code lives in `Content.Shared/Server/Client/_Library`, and the prototypes and text in `_Library` folders. Nothing existing is overwritten.
- **Install:** `install.sh /path/to/fork` copies the files and applies a small `hooks.patch` for edits to existing files. I tested the script on a stripped copy of the base files and the hooks applied. The `README.md` lists the manual steps if a hook doesn’t fit, and covers the features, CVars, compatibility and what differs from the OmuStation version.
- **Three Omu dependencies replaced:**
    - **CVars:** they are now `library.uploads_enabled` and `library.max_books`.
    - **Heretic tomes:** other forks can exclude their own books with `LibraryBookExcludeEvent`.
    - **NanoChat:** the library raises `LibraryChatMessageEvent`. `optional/NanoChatAdapter.cs` delivers it for DeltaV and Goobstation style NanoChat. Without an adapter nothing is sent.
- **Extras:** `tools/convert-maps.py` swaps the old story-book spawns on a fork’s maps for random archive books.
- **Not compile-tested:** your OmuStation builds ran the library code as it grew. The universal package has never been compiled on anything, so a different fork may need a few fixes. The README lists the likely ones.
- **Machines not placed:** the console, scanner, binder and request console are not placed on any map in either release.
- **Pending uploads:** they are written to the server’s `UserData/library` folder until an admin accepts them, and declined ones are deleted. I put a note about this in the README for communities with licence concerns.