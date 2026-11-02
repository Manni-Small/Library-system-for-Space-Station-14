# Library system for Space Station 14

A tgstation style library for SS14: a persistent book archive, a console to search and print from it, a scanner
and a book binder, a librarian inventory that notices missing books, book pinpointers, a book request console and
an admin review queue for uploaded books.

Written for OmuStation. This folder is the same code made independent of OmuStation, so any SS14 fork can use it.

* Licence: AGPL-3.0-or-later (see the SPDX header of each file).
* Art: the scanner sprite (`files/Resources/Textures/_Library/Structures/Machines/library_machines.rsi`, with an added
  scan line animation) is from tgstation (`icons/obj/service/library.dmi`) under CC-BY-SA 3.0, the credit is in its
  `meta.json`. The console icons for the lathe are composed from the vanilla `computers.rsi`. The book binder uses the
  vanilla uniform printer sprite and its building animation, everything else reuses sprites your fork already has.
* Tested on: OmuStation only. This package is produced mechanically from the OmuStation version (namespaces,
  folders and three fork specific dependencies replaced, see "What is different from the OmuStation version").
  It has **not been compiled on any other fork**. Expect to fix a few names if your fork's engine or content
  APIs differ, see "Compatibility".

## Features

**Archive**
* Books are stored as one JSON file each in the server's `UserData/library` folder and live across rounds.
* The console searches the archive by title, author, category or id and prints a book on the book binder.
* Guidebooks are part of the archive, so a librarian can print one for anybody.
* Duplicate texts are rejected, copies with changed symbols count as the same text.

**Console, scanner, book binder**
* Link them with a network configurator: console `Binder` -> binder `Console`, console `Scanner` -> scanner `Console`.
* The binder turns a written sheet into a "print job" book and prints archive books. Both cost sheets of paper.
* The scanner digitises a book. The console uploads it with a category and a cover picked from covers, decors
  and icons. **Uploads are not added to the archive, they wait for admin review.**
* A "Blank book" tab prints an empty book with a title and a cover, it is not part of the archive.
* Access: the new `Library` access level plus Command opens the machines.

**Inventory and checkouts**
* A console registers the archive books standing in its room (rooms are found by flood fill, closed by airtight
  walls, windows and doors) a few seconds after it appears.
* Later books are added by scanning them. Only archive prints and guidebooks can be added. Print jobs and blank
  books never can, not even with the text of an archive book written into them.
* Books are tracked by entity. A registered book that leaves the room starts a 60 second countdown and is then
  marked **MISSING** until a librarian clears it by hand. Carrying a book around inside the room is fine.
* Checkout to the name on an ID card, with a number of minutes (0 means no return date).
* Overdue books and answered requests can be sent to the borrower's messenger, see `LibraryChatMessageEvent`.

**Book pinpointers**
* `BookPinpointer` lists the books of the room and points at the one you pick.
* `LibrarianUniversalPinpointer` finds any book on the station and has "Missing" and "Overdue" lists (with the borrower).

**Book request console**
* People ask for an archive book or a blank book (title and cover) from a console linked to the library console.
  Link the request console's `Requests` port to the library console's `Request inbox` port.
* The librarian sees the requests in a tab and accepts or denies them.

**Admin review**
* An admin tab on the console, only visible to admins, with the uploader's account name and character name.
* Review opens the book in its own window. Accept, decline and delete (also for archived books) all ask to confirm.
* Pending uploads are written as JSON files in the server's `UserData/library` folder until an admin accepts them,
  declined ones are deleted. If your community has licence concerns about uploaded text, read
  `LibraryArchiveSystem.cs` first and decide whether that fits your policy, and consider disabling uploads
  (`library.uploads_enabled false`).

**Console commands (admins):** `librarylist`, `librarydelete`, `libraryrestore`, `libraryshelves`, `librarystockshelves`.

**CVars**
| CVar | Default | Meaning |
|---|---|---|
| `library.uploads_enabled` | `true` | Whether consoles may send scanned books for review. |
| `library.max_books` | `5000` | Most books the archive holds, 0 for unlimited. |

## Install

1. Copy this folder anywhere and run `./install.sh /path/to/your/fork` (needs git and bash).
   It copies everything in `files/` into the repository and applies `hooks.patch`.
   If you prefer to do it by hand: copy the contents of `files/` over your repository root and make the edits of
   the next section.
2. Do the hooks that did not apply.
3. Build, run the YAML linter, start a server, check the console log for errors.

New folders (nothing existing is overwritten):

```
Content.Shared/_Library/   Content.Server/_Library/   Content.Client/_Library/
Resources/Prototypes/_Library/   Resources/Locale/en-US/_Library/
```

### Hooks (edits to files that already exist)

`hooks.patch` makes the generic ones, these are the edits in case it does not apply:

1. **Access**: add `Library` to your all-access group (`Resources/Prototypes/Access/service.yml`,
   next to `Service`). The access name text is in `_Library/library-access.ftl`.
2. **Librarian job** (`Resources/Prototypes/Roles/Jobs/Civilian/librarian.yml`):
   add `- Library` to the job's `access`, and to the starting gear's backpack
   `BookRandomArchive`, `BookPinpointer`, `LibrarianUniversalPinpointer` and `NetworkConfigurator`
   (replace the old `BookRandom`).
3. **Bookshelves**: add `- type: ArchiveBookshelf` to your bookshelf entity. When the round starts, about
   `LibraryConstants.ArchiveBooksPerStation` archive books are spread over the shelves (`librarystockshelves` does it by hand during a round).
4. **Guidebooks**: add the `LibraryGuidebook` tag to your guidebook base prototype (`BaseGuidebook`), so the
   library can list and print them. (Guidebooks are also found through their parent prototype.)
5. **Lathes**: put `LibraryPaperStatic` in the static recipes of the lathe that prints paper (autolathe or
   service lathe) and `LibraryBoardsStatic` in the circuit imprinter's, so the machine boards can be built.
6. **Random books**: `BookRandomArchive` is a book that becomes a random book of the archive. Use it wherever
   you had random story books (crates, lockers, maps): `python3 tools/convert-maps.py Resources/Maps`.

The library machines are **not placed on any map**. Spawn `ComputerLibrary`, `LibraryScanner`, `Bookbinder` and
`ComputerLibraryRequest` in your library and link them with a network configurator.

### Compatibility

Written against the OmuStation tree (a Goobstation / DeltaV / Wizards Den based fork). Things that are likely to
need a small edit on another fork:

* **Engine / content APIs**: it uses `AirtightComponent` from `Content.Server.Atmos.Components`, the server
  `Content.Server.Pinpointer.PinpointerSystem` (`TogglePinpointer`, `SetTarget`), `ItemSlotsSystem`,
  `SharedDeviceLinkSystem.GetLinkedSinks`, `UserInterfaceSystem`, `SharedIdCardSystem.TryFindIdCard`,
  `MaterialStorageSystem`, `RandomSpriteComponent`, `RandomMetadataSystem`, `PaperSystem.SetContent`.
  If one of those moved or was renamed in your engine version the compiler tells you where.
* **Prototypes it expects**: `BaseComputer`, `BaseMachinePowered`, `ConstructibleMachine`, `PinpointerBase`,
  `BookRandom`, `BaseCircuitboardRecipe` and the recipe categories used in `Recipes/Lathes/library.yml`,
  the `Paper` material, the books from `books_author.yml` listed in `LibrarySeedPrototype` seeds
  (remove lines of books your fork does not have), the sprites `Objects/Misc/books.rsi`,
  `Objects/Devices/pinpointer.rsi`, `Structures/Machines/computers.rsi`, `Structures/Machines/uniform_printer.rsi`
  (states `icon` and `building`, used by the book binder).
* **Existing book prototypes** with a `Book` tag and paper count as library books.
  Use `LibraryBookExcludeEvent` (see `optional/ExcludeBooksExample.cs`) to keep some out, like cult tomes.
* **Messenger**: nothing is sent unless an adapter subscribes to `LibraryChatMessageEvent`.
  `optional/NanoChatAdapter.cs` is one for DeltaV / Goobstation NanoChat.

## What is different from the OmuStation version

| OmuStation | This package |
|---|---|
| projects `Content.Omu.*` (`Content.Omu.Server/Library`, ...) | `Content.Server/_Library`, `Content.Shared/_Library`, `Content.Client/_Library`, namespaces `Content.*._Library` |
| `omu.library_uploads_enabled`, `omu.library_max_books` | `library.uploads_enabled`, `library.max_books` |
| heretic tomes excluded directly | `LibraryBookExcludeEvent` |
| NanoChat messages sent directly | `LibraryChatMessageEvent`, delivered by an optional adapter |
| prototypes in `_Omu` | prototypes in `_Library` |

Everything else is the same code.
