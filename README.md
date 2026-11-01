# Library System for Space Station 14

YT demonstration video: https://www.youtube.com/watch?v=C4PSlJOp9LU&t=245s

A full library for the station, inspired by tgstation's library: a book archive that lasts between rounds, machines to print, scan and upload books, an inventory that notices when books go missing, pinpointers to find them, a request console for the crew, and a review queue so admins decide what ends up in the archive.

Made for OmuStation, and also will be made available as a package for any other SS14 fork if requested.

Manni-Small (Repo-Creator/Me) does not know coding because of that this was fully made using Claude AI. I will attempt to fix any problems you may stumble upon but I wanted to make you aware of my position on this. I just wanted a library system that actually utilizes the libraries other than being the room to grab curadrobe items.

For any inquiries: ''miunnakversicolor'' is my discord username.

## The archive

* Every book is saved as its own file in the server's `UserData/library` folder, so books stay between rounds.
* Search by title, author, category or id. Categories: Fiction, Non-fiction, Reference, Religion and Adult.
* Guidebooks are part of the archive. Ask the librarian and they can print one for you.
* Copies are caught: a text that is already in the archive cannot be uploaded again, even with a few symbols changed.
* A cap on the number of books (`library.max_books`) keeps the archive from growing forever. Can be changed to ''0'' make it infinite or you can just lower limit.

## The machines

| Machine                               | What it does                                                                                  |
| ------------------------------------- | --------------------------------------------------------------------------------------------- |
| **Book inventory management console** | The librarian's workstation: archive, inventory, checkouts, upload, blank books and requests. |
| **Book binder**                       | Binds a written sheet of paper into a book, and prints archive books. Costs sheets of paper.  |
| **Book scanner**                      | Digitises a book so it can be sent to the archive or add books to the inventory.              |
| **Book request console**              | Used by anyone to request books, mainly for personal use.                                     |

Machines are linked with a network configurator, and the librarian starts with one. Access needs the new **Library** access level or **Command** access. All four can be built from circuit boards printed at a lathe.

## Printing and uploading

* **Print from the archive:** pick a book in the console and the linked binder prints it, with its real text, author and cover.
* **Bind your own:** swipe an ID to be credited as the author, feed the binder a written sheet and it makes a book. These are *print jobs*: they are not in the archive.
* **Upload:** scan a book, give it a title, an author, a category and a cover, and send it to CentComm to be uploaded to the archive. Covers are built from a style, a decoration and an icon, each with its own color, with a live preview.
* **Blank books:** a console tab prints an empty book with a title for example "Lina's Diary" and a cover of your choice, to be used for whatever purpose you want. It is not part of the archive or the inventory unless it is uploaded, approved or scanned.

## Admin review

* Uploads never go straight into the archive. They wait for an admin review.
* Admins get an extra console tab that the players cannot see. It shows each pending book with the **account name and character name of the person who uploaded it**, so offenders can be dealt with.
* **Review** opens the book in its own window without printing it. **Accept** adds it to the archive, **Decline** deletes it for good, and nobody else ever sees it except the admins. Every action asks to confirm.
* The same tab lists the archive via a dropdown menu. Admins can delete a book from it later with a confirm, and restore it.
* Players are told in the archive tab that a missing book may be under review or declined by centcomm.

## The library's inventory

* Each console builds its own inventory from the books in the room it stands in. A room is found by flood fill: airtight walls, windows and doors close it, directional windows and windoors do not. A console only ever sees its own library.
* The first inventory is made a few seconds after the round starts. After that, books are added by **scanning them**, and only archive books can be added. Print jobs and blank books never can, not even with the text of an archive book written into them.
* Books are tracked as individual objects, not by title, so two copies of the same book are never mixed up.
* **Checkouts:** check a book out to the name on an ID card for a number of minutes. Put 0 for no return date: the book is still the library's property, it just never becomes overdue.
* **Missing books:** if a book of the inventory leaves the library room, a one minute countdown starts. If it is not checked out or returned in time, it is marked **MISSING** on the console. Carrying a book around inside the library is fine. The mark only goes away when a librarian clears it, so people cannot reset it by walking in and out. Books that are burned or destroyed are marked missing too, and a librarian can remove their entry.
* Librarians and command can **remove an entry** from the inventory (with confirm). The physical book stays. to dispose of it it can be disposed by burning or just can be sent to disposals.
* Heretic tomes and books that are not really library books are ignored. Per lore heretic books were an overlook.

## Pinpointers

* **Book pinpointer:** use it to see the books in your room, pick one, and the arrow points the way. Use it again to turn it off. It loses the book if you carry it too far outside of a room.
* **Librarian's universal pinpointer:** Finds any book on the station. It also has a **Missing** list, and an **Overdue** list that shows who took each book. For catching book thieves. Mainly added to create roleplay friction/dynamics between other players and the librarians.

## Book requests

* The **request console** has two tabs. One searches the archive and asks for a book, the other asks for a blank book with a title and a cover.
* The request goes to the library console it is linked to. The librarian sees every request, who sent it, can print the book or copy a blank book's title and cover into the printer, and **accepts or denies** it.
* The requester is told by message on the PDA (see below). The request console itself never shows other people's requests.

## Messages to the crew

When a PDA with NanoChat is available, the library messages the person directly:

* Overdue: *"Your book is overdue, please return it back to the library."*
* Accepted request: *"Your book request is ready, either come to library or request someone to pick it up on your behalf."*
* Denied request: *"Your book request was denied."*

On forks without NanoChat nothing is sent, or an adapter can send it to whatever messenger the fork has.

## Around the station

* Bookshelves are stocked with random books from the archive when the round starts, replacing the old hand-made
  story books, and the same goes for the random book spawns on the maps.
* The Librarian starts with a random archive book, a book pinpointer, the universal pinpointer and a network configurator.
* Books can burn.

## For server owners

| CVar | Default | |
|---|---|---|
| `omu.library_uploads_enabled` / `library.uploads_enabled` | `true` | Allow sending scanned books for review. |
| `omu.library_max_books` / `library.max_books` | `5000` | Most books in the archive, 0 for no limit. |

Admin commands: `librarylist`, `librarydelete`, `libraryrestore`, `libraryshelves`, `librarystockshelves`.

Uploaded books waiting for review are kept as files in `UserData/library` until an admin accepts them, and are deleted if declined. If your community has concerns about licensed or sensitive text, switch uploads off.

## Install

* **OmuStation:** apply `omustation-library-system.patch`.
* **Any other fork:** use the universal package: `./install.sh /path/to/your/fork`.
* **OTHER FORKS NEED TESTING**

Both come with a README. The code is AGPL-3.0-or-later.

## Status

Developed and tested on OmuStation. 

The universal package is generated from the same code and has not been compiled on other forks yet, so expect to fix a few names.

Made with Claude AI.
