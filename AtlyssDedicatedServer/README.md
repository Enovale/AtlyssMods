# Atlyss Dedicated Server

This is a mod for ATLYSS that aims to allow for a dedicated server to be run, requiring no graphics and providing a console to the hoster.

# Instructions

1. Install the mod

2. Server side, run the game through the command line with `-batchmode -nographics` arguments.
The mod will automatically run itself as a server running on port 7777.
Don't forget to port forward and everything!

3. Client Side, go to Multiplayer, press Join, select the character slot you want to play with,
then press F5 and enter the server IP and password.

# Caveats

- Currently the server is hosted without Steam, and thus all players must have the mod installed to be able to play.
  - A steamworks option could be added, however, it would mean that the server hoster could not join the server on the same machine or on the same steam account.
- Currently the server has to make a dummy character to sit in spawn. 
  - The game has a dedicated "ServerOnly" mode that doesn't require the host to have a character loaded, but many parts of the game expect the host to exist. Most things work fine, but attempting to load into a dungeon puts you in a softlock. I don't think this is fixable but I'd love contributions to prove me wrong.

# Known Issues

- Server mode makes the game use AppData to store profiles, which will conflict when the game is updated with Cloud Save support
- No way to toggle "ServerOnly" mode
- No steamworks mode
- Dummy character has to be manually created / copied
- Lots of unneccesary reflection when publicized assemblies could be used
- Can't increase the maximum lobby size