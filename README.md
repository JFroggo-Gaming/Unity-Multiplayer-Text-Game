
# Multiplayer-Text-Game

Unity Multiplayer Game(Short video) - https://youtu.be/G2mL8aH0m7Y        (08.03.2023)

Unity Multiplayer Game(Full presentation) - https://youtu.be/_EdskhqTgnc  (08.03.2023)

<code>1. DESCRIPTION:</code><br><br> A game created with the Unity Engine using the Mirror asset for network connections.
                <br>It is a text-based game in which the collective decisions of the players determine how the story unfolds.
                <br>Make decisions quickly before the sand flows from the hourglass! Otherwise, the story may take its course in the wrong way...
                <br>In the game we can create a lobby for 3 players - it is with them that we will make decisions and travel through the game world together.
                <br>Each player has the opportunity to find unique items during the journey. However, do not worry if your companion has found something interesting! 
                <br>Everything goes into your common inventory (After all, you have a common goal, right?).
                <br>In the game there is also a system of notifications (pings), which we can use to let other players know what option we want to vote for. 
                
<code>2. HOW TO PLAY:</code> <br><br>Required storage 220-300mb. To download the game, press the green button on the main page --> Download ZIP
                <br><br>The preffered resolution is full HD
                <br><br> If you use Unity Editor: Make sure that your build contains all scenes.
               	<br><br>Open 3 clients simulating each player. Only one player on the same network can host the game and create a lobby.
                <br>In this case, press the "Host Lobby" button in only one client. In the other two, press the "Join Lobby" button. 
                <br>The default network address when testing the game on the same device is "localhost".
                <br>If the address is incorrect: check if you are not using spaces and typed the address with a lowercase letter.<br>

<code>3. SCRIPTS:</code>     <br><br>Go to Assets - > Scripts
                List of folders:
                <br><br>**Audio** - Contains scripts related to the music player and describing how the music is played in the game.
                <br><br>**Graphics** - Contains scripts related to the options menu that help you adjust graphic settings.
                           				 Most of these scripts are easily available in the asset store. Several of them have been modified for the game.
                <br><br>**Inventory** - Contains scripts related to the definition of items and how they get into the inventory.
                <br><br>**Main Game Script** - Contains scripts describing the main mechanics of the game, such as voting,
                                   				 player recognition, the structure of the entire text game,
                                   				 ping system etc. Getting familiar with these scripts is crucial in understanding the rules of the game.
                <br><br>**Main Menu** - It contains scripts describing how players join the game, and deals with relevant UI related to options/exit/lobbies, etc.
                <br><br>**Network** - It contains scripts related to network aspects such as player definition, adding them to the player list, 
                          				updating their name, assigning lobby ownership, what happens when a player connects/disconnects from the game, etc.
