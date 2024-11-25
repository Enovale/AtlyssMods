# Atlyss: Read Old Saves

This is a mod for the current Early Access build of ATLYSS that allows it to read save files from the old itch.io demo build.

# Instructions

1) Install the mod
2) Put your old save file into the profileCollections folder alongside the rest of your saves
3) Rename the save file to match the number to whatever slot you want your save to overwrite
4) Run the game
5) (Optional) If any of your current quests have kill progress that surpass the requirement, the game will kick you the next time you kill something, so abandon or turn in the quest before doing that.

### **This will completely overwrite whatever slot you set it to!**

# Known issues

- Weapon modifiers are removed when loaded as the data is not directly transferable, it would be a pain to make it properly transfer, and kiseff put the same limitation in the patreon build that updated the modifier system anyway.
- As I do not have access to any of them, there is no support for any patreon builds between itch.io and the full release. Aspects of your character such as left handedness, and weapon stone mods (might stone, etc) will not carry over even if they exist on your save.
  - I didn't realize how much information is stored in the binary format, honestly, the modern game can probably parse the old save with no modifications other than the index -> tagging modifier change (which can just be ignored)