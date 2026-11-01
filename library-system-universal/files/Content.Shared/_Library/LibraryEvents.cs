// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Shared._Library;

/// <summary>
///     Raised on a book while the library decides whether it is a library book. Set <see cref="Excluded"/> to keep
///     it out of the inventory and the book pinpointers. Subscribe to it on your own component, for example a
///     cult tome: <c>SubscribeLocalEvent&lt;MyTomeComponent, LibraryBookExcludeEvent&gt;(...)</c>.
/// </summary>
[ByRefEvent]
public record struct LibraryBookExcludeEvent(bool Excluded = false);

/// <summary>
///     Raised (not directed) when the library wants to message the owner of an ID card: an overdue book, or the
///     answer to a book request. The library has no messenger of its own. An adapter subscribes and delivers the
///     text to whatever your fork has, see <c>optional/NanoChatAdapter.cs</c>.
/// </summary>
[ByRefEvent]
public readonly record struct LibraryChatMessageEvent(EntityUid Card, uint SenderNumber, string SenderName, string Text);
