// SPDX-License-Identifier: AGPL-3.0-or-later

// OPTIONAL. Not part of the normal install, copy it to Content.Server/_Library/ only if your fork has the
// DeltaV / Goobstation style NanoChat (Content.Server._DV.CartridgeLoader.Cartridges). It needs the
// DeliverAnonymousMessage method of Goobstation's NanoChatCartridgeSystem (a message from a sender without a card).
// Without an adapter the library still works, it just does not send the overdue and request messages.

using Content.Server._DV.CartridgeLoader.Cartridges;
using Content.Shared._DV.NanoChat;
using Content.Shared._Library;

namespace Content.Server._Library;

/// <summary>
///     Delivers the library's messages (overdue books, answered book requests) to the NanoChat of the ID card.
/// </summary>
public sealed class LibraryNanoChatAdapter : EntitySystem
{
    [Dependency] private readonly NanoChatCartridgeSystem _nanoChat = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<LibraryChatMessageEvent>(OnMessage);
    }

    private void OnMessage(ref LibraryChatMessageEvent ev)
    {
        if (!TryComp<NanoChatCardComponent>(ev.Card, out var chat))
            return;

        _nanoChat.DeliverAnonymousMessage((ev.Card, chat), ev.SenderNumber, ev.SenderName, ev.Text);
    }
}
