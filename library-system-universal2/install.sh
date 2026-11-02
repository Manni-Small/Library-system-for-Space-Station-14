#!/usr/bin/env bash
# SPDX-License-Identifier: AGPL-3.0-or-later
# Copies the library system into a Space Station 14 fork and applies the small edits to existing files.
#   ./install.sh /path/to/your/fork
set -euo pipefail

TARGET="${1:?usage: ./install.sh /path/to/your/fork}"
HERE="$(cd "$(dirname "$0")" && pwd)"

if [ ! -d "$TARGET/Content.Shared" ] || [ ! -d "$TARGET/Resources/Prototypes" ]; then
    echo "$TARGET does not look like an SS14 repository (no Content.Shared or Resources/Prototypes)." >&2
    exit 1
fi

echo "Copying the new files..."
cp -r "$HERE/files/." "$TARGET/"

cd "$TARGET"
echo "Applying the edits to existing files (hooks.patch)..."
if git apply --check "$HERE/hooks.patch" 2>/dev/null; then
    git apply "$HERE/hooks.patch"
    echo "Hooks applied."
else
    echo "hooks.patch does not apply cleanly to this fork. Applying what fits, the rest is left as *.rej files." >&2
    echo "Finish the rest by hand, see the Hooks section of the README." >&2
    git apply --reject --whitespace=nowarn "$HERE/hooks.patch" || true
fi

cat <<'MSG'

Still to do by hand (see README.md):
  1. Give the Librarian job the Library access and the library tools (README, "Hooks").
  2. Add LibraryBoardsStatic / LibraryPaperStatic to a lathe's static recipes (README, "Hooks").
  3. Optionally copy optional/NanoChatAdapter.cs and run tools/convert-maps.py.
  4. Build, run the YAML linter, start a server.
MSG
