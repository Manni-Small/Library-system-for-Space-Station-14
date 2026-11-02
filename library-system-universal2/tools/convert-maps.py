#!/usr/bin/env python3
# SPDX-License-Identifier: AGPL-3.0-or-later
"""
Optional. Replaces the premade story books and the empty random books on your maps with random books of the
library archive (BookRandomArchive), so the books lying around a station come from the archive.

    python3 tools/convert-maps.py Resources/Maps            # change the files
    python3 tools/convert-maps.py Resources/Maps --dry-run  # only count

Only "- proto: <name>" lines of the list below are touched. Add or remove names to fit your fork.
"""
import os
import re
import sys

REPLACE = """
BookRandomStory BookRandom BookNarsieLegend BookMedicalOfficer BookTruth BookNames BookIanOcean BookWatched
BookIanAntarctica BookIanRanch BookIanLostWolfPup BookIanCity BookAurora BookTemple BookSun BookSlothClownMMD
BookPossum BookMorgue BookJourney BookIanMountain BookIanDesert BookIanArctic BookEarth BookSlothClownSSS
BookSlothClownPranks BookMap BookJanitorTale BookInspiration BookCafe BookFeather BookRufus BookWorld BookStruck
""".split()

PATTERN = re.compile(r"^(\s*- proto: )(%s)$" % "|".join(REPLACE), re.M)


def main():
    args = [a for a in sys.argv[1:] if not a.startswith("--")]
    dry = "--dry-run" in sys.argv
    if not args:
        print(__doc__)
        return 1

    total = 0
    for root, _, files in os.walk(args[0]):
        for name in files:
            if not name.endswith(".yml"):
                continue
            path = os.path.join(root, name)
            with open(path, encoding="utf-8") as f:
                text = f.read()
            new, count = PATTERN.subn(r"\1BookRandomArchive", text)
            if count:
                total += count
                print(f"{count:4d}  {path}")
                if not dry:
                    with open(path, "w", encoding="utf-8") as f:
                        f.write(new)
    print(f"{total} spawns {'would be ' if dry else ''}replaced")
    return 0


if __name__ == "__main__":
    sys.exit(main())
