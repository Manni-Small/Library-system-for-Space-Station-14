# Library system for OmuStation

One patch with the whole library system: `omustation-library-system.patch`
(AGPL-3.0-or-later, generated against OmuStation commit `01744060d`, "Automated Changelog Update (#2103)").

## Apply

```
git switch -c feature/library-database
git apply --3way --index omustation-library-system.patch     # or: git apply omustation-library-system.patch
```

If your checkout is newer than that commit and a hunk fails, `--3way` leaves conflict markers in the files that
changed in the meantime. Rider can resolve them. Then build, run the YAML linter and start a server.

It touches about 127 files. The new code is in `Content.Omu.Server/Library`, `Content.Omu.Shared/Library` and
`Content.Omu.Client/Library`, new prototypes are in `Resources/Prototypes/_Omu`, text in
`Resources/Locale/en-US/_Omu/library/library.ftl`. The rest are edits to existing files:

* maps and fills: the premade story books and empty random books on the maps, in bookshelves, crates, lockers and
  toolboxes are replaced by `BookRandomArchive` (a random book from the archive). The Librarian's starting book too.
* the Librarian job gets `Library` access, a book pinpointer, the Librarian's universal pinpointer and a network configurator
* the `Library` access level, the library lathe recipes (paper and machine boards), the `LibraryGuidebook` tag,
  `ArchiveBookshelf` on bookshelves

## CVars

`omu.library_uploads_enabled` (default true) and `omu.library_max_books` (default 5000, 0 for no limit).

## After applying

The machines are not placed on any map. Spawn `ComputerLibrary`, `LibraryScanner` and `Bookbinder` (and
`ComputerLibraryRequest` for the request console) in the library and link them with a network configurator.
For the other forks there is `library-system-universal`.
